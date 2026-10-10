---
name: lycans-wolf-transformation
description: Use when reading, modifying, or debugging wolf transformation / untransformation in the LycansNewRoles mod — manual transform, Warlock / Village Idiot curse, Spellbook TransformWolf, Possessor, Necromancer, Reverting (Infected), Beast / Voodoo / Cultist hunts, Eclipse, or any new effect that forces a player into (or out of) wolf form. Covers the wolf-state properties, every transform / untransform entry point, which side effects each path does or does not run, gating rules, and a checklist for new features.
---

# Lycans Wolf Transformation & Untransformation

> The repo is an ILSpy extract and cannot be built. Base-game classes (`PlayerController`, `GameManager`, the dawn/dusk logic) are **not** in the repo. Anything attributed to "base game" below is inferred from how the mod hooks it — verify before relying on it.

## Core concepts

| Concept | Where | Meaning |
|---|---|---|
| `PlayerController.IsWolf` | base game, `[Networked]` | **Currently in wolf form.** This is the form, not the team. |
| `PlayerController.Role` | base game | **Team.** `(int)Role == 1` = wolf team, `0` = villager. A villager can be `IsWolf` (curse, spellbook) and a wolf-team player is usually *not* `IsWolf`. Never use one as a proxy for the other. |
| `PlayerController.TransformedNight` | base game, networked | The player has already manually transformed this night. Only consulted by the Eclipse manual toggle in `LocalInputPatches.cs`. Set by `TransformClass.TransformPrefix`, `BeastManager`, and the Necromancer revive path; **not** set by `Rpc_Forced_Transform`. |
| `PlayerController.WolfDelay` | base game `TickTimer` (set via `Traverse`) | Post-transformation lockout (`GameManager.Instance.TransformationTime` seconds). While running: primary action is blocked (`LocalInputPatches.cs`), and trap / molotov / purifier logic treats the wolf as "just transformed" (`TrapTriggerPatch.cs`, `MolotovFire.cs`, `PurifierFire.cs`). Set only on the **state authority**. |
| `PlayerCustom.IsWolfPup` | networked | Wolf-team player that is not allowed to transform (`TransformPrefix` refuses). |
| `PlayerCustom.WolfDaysWithoutTransformation` | local int | Incremented at each day start for living, non-pup wolf-team players; reset to 0 by `OnCharacterChangedPatch` whenever `IsWolf` becomes true **from any source**, and by the `PlayerCustom` reset. |
| `PlayerCustom.TransformationTimer` | local `Stopwatch` | Restarted when `IsWolf` becomes true, reset when false. Used for the "recently transformed" check in `RpcKillPatch.cs` (Ghost spawn needs ≥ 15 s). |
| Game state | `GameManager.State.Current` / `LocalGameState` | `0 Off, 1 Pregame, 2 Play, 3 Transition, 4 Meeting, 5 EndGame`. Night is `GameManager.LightingManager.IsNight`, separate from state. |

## Entry points that make a player a wolf

| Path | Code | Notes |
|---|---|---|
| **Manual transform** | `PlayerController.Rpc_TransformWolf` → `TransformPatch.cs` → `TransformClass.TransformPrefix` / `TransformPostfix` | The only path that runs the full set of side effects (see table below). Sent from the input-authority client. |
| **Eclipse event** | `LocalInputPatches.cs` (calls `Rpc_TransformWolf` / `Rpc_TransformBack`) | Allowed during the **day** when `EventType.Eclipse`, state Play, wolf-team, `CanMoveAnimation`; `+2 s` extra `WolfDelay` in `TransformPrefix`. Transform is also gated by `TransformedNight`. |
| **Warlock curse / Village Idiot curse (type 0)** | `Rpc_Activate_Primary_Role_Power` sets `target.CurseDormant = true` (only if the target is villager-team and neither caster nor target is a wolf). Night start in `GiveNewRolesPatch.cs` arms `CurseTimer` (1–20 s). The server per-tick loop in `PlayerCustom` fires `Rpc_Forced_Transform(Index, 0)` when `CurseTimer` expires. | Loop requires state Play, alive, no Beast/Cultist/Voodoo, and **`CanMoveAnimation` true** (otherwise retries in 1 s). `Rpc_Forced_Transform` type 0 then arms a **30–60 s** `CurseTimer`; when it expires with `CurseDormant == false` the server sets `IsWolf = false` + `AddDetransformation()` (curse wears off). |
| **Spellbook `TransformWolf` (type 1)** | `AccessorySpellbook.cs` → `Rpc_Forced_Transform(.., 1)` | Removed from the effect pool during the day and for Zombies. No timed revert is armed by the mod for this type. |
| **Other forced transforms (type ≥ 2)** | `Rpc_Forced_Transform(.., type)` | Type is a free int; only `0` has extra behaviour (curse timer + `NALES_UI_WARLOCK_CURSE` message). Any new type gets only the minimal path below. |
| **Possessor** | `TransformPrefix` (Possessor branch) | The Possessor is teleported to the target's position; the target is teleported to `(999,999,999)`, gets `ClearEffects()` and `EffectPossessed`. `Possessed` makes the target `IsOutOfTheWorld`. |
| **Necromancer revive** | `PlayerCustom` Necromancer case | Revived wolf: `IsDead=false`, `Hunger=Max`, `WolfDelay`, `TransformedNight`, `IsWolf`, `EffectResurrected` (300 s, sets `ResurrectedByNecromancer`), `AddTransformation()`. On first use the Necromancer itself becomes `Role = Wolf` + `IsWolf = true`. |
| **Beast** | `BeastManager.BeastActiveChanged` | Beast gets a 7 s `WolfDelay`, `TransformedNight`, `IsWolf`. |

## What the two main routes actually do

`Rpc_Forced_Transform` (`PlayerCustom.cs`) is a **static RPC** that runs on every peer. It is intentionally lean and is **not** equivalent to a manual transform.

| Effect of transforming | Manual (`Rpc_TransformWolf`) | `Rpc_Forced_Transform` |
|---|---|---|
| Refuse for: wolf pup, Beast, Cultist, Voodoo | all four | Beast, Cultist, Voodoo only (**no pup check, no alive check, no `IsOutOfTheWorld` check**) |
| Possessor swap / Sneak double delay / Eclipse +2 s | yes | no |
| `WolfDelay` (state authority only) | yes (`TransformationTime`, scaled) | yes (`TransformationTime`) |
| `IsWolf = true`, `MovementAction = 0`, `CanMoveAnimation = false`, `IsZooming = false` | yes | yes |
| `TransformedNight = true` | yes | **no** |
| Sound + smoke particles | yes | yes |
| Held item cancelled / non-kept effects stripped | via `UpdateWolf` | via `UpdateWolf` |
| Hunger refill to max, `IsAiming = false` | yes (postfix, state authority) | **no** |
| Wolves-Ritual / Avatar-kill / Rage-event `Empowered` or `Weakened`, Tenacity / Hubris | yes (postfix) | **no** |
| `EffectSpiritResistance` (6 s) | yes | **no** |
| Scientist progress | yes | **no** |
| Per-player stats `"Transform"` action | yes | **no** |
| `GameManagerCustom.AddTransformation()` | yes | yes |
| "Transform back" UI hint (`WolfRevert`) | yes (input authority) | **no** |

If a new forced-transform feature must feel like a real transformation, either extend `Rpc_Forced_Transform` or call `TransformClass.TransformPrefix/Postfix` (the `EffectOnPlayer.ForceTransform` case in `Rpc_Effect_On_Player` already does this) — but the Prefix has its own gates and uses `HasInputAuthority` / `HasStateAuthority` checks.

## Reactions to `IsWolf` changing (run on every peer)

`OnCharacterChangedPatch` (Harmony postfix on `PlayerController.OnCharacterChanged`):
- Always: `UpdateMoveSpeed`, `UpdateVisibility`, `UpdateScaleAndPitch`, `UpdateWolfColor`, spotter light update, illusion/visibility refresh for the observed player, Scientist target arrow.
- When becoming wolf: `TransformationTimer.Restart()`, `WolfDaysWithoutTransformation = 0`.
- When becoming human: `TransformationTimer.Reset()`; **server only** removes `RevertingEffect`, `ExorcismEffect`, `TenacityEffect`, `HubrisEffect`.

`UpdateWolfPatch` (replaces `PlayerController.UpdateWolf`):
- While `IsWolf`: cancels the held item and **removes every active effect that is not a `CustomEffect` with `KeepOnWolfTransformation == true`** (default `false`; vanilla effects are removed too). Removed effects run `Despawned` / `RemoveEffectFromPlayer` *while the player is already a wolf*.
- Then: wolf lighting for the observed player, model swap, collider, camera anchor.

Consequence for effect design: an effect that must survive transformation must override `KeepOnWolfTransformation => true` (see `SpiritResistanceEffect`, `EmpoweredEffect`, `TenacityEffect`…). A `Despawned` hook can therefore fire because of the transformation itself, not because of expiry.

## Untransform paths

| Path | Code | Notes |
|---|---|---|
| Manual | `PlayerController.Rpc_TransformBack` (`TransformChangesPatch.cs`) | Prefix **blocks** if `ResurrectedByNecromancer`; if Possessor with a possessed target; if `ForcedTransformation` is set; if `Role != Wolf`. Postfix (state authority): Sneak teleports back to its anchor + clears effects + `EffectSneaky`; if now human: `AddDetransformation()`, remove `DetectedEffect`, stats `"Untransform"`. |
| Dawn / end of night | base game (not in repo) | The mod only relies on it (per-night state such as `TransformedNight` is presumably reset there). |
| Curse expiry | `PlayerCustom` per-tick loop | See curse row above. Requires state Play and no Beast/Cultist/Voodoo. |
| Reverting (Infected) | `ApplyEffectToPlayer(.., "LycansNewRoles.EffectReverting")` when a wolf kills a `BothInfected` target; `RevertingEffect` sets `PlayerCustom.Reverting`; `RevertingChanged` untransforms on the state authority when `Reverting` falls to false and `IsWolf` is still true (also refills hunger, `AddDetransformation()`). | Not applied to Necromancer-resurrected players or Possessors holding a possessed target. |
| Beast / Voodoo activation | `BeastManager.ActivateBeast`, `VoodooManager.ActivateVoodoo` | Forces every living wolf to human form and refills hunger. While either is active nothing may transform (`Rpc_Forced_Transform` and `TransformPrefix` both return). |
| Death of a villager-team wolf | `RpcKillPatch.cs` | If `IsWolf && Role != Wolf && not Beast` → `IsWolf = false` before marking dead. |

## Gating rules to respect (collected from existing code)

- **Never transform when:** `BeastManager.BeastActive`, `CultistManager.CultistActive`, `VoodooManager.VoodooActive`, `IsWolfPup` (manual path only), player dead, Zombie / `ResurrectedByNecromancer`.
- **Out of the world:** `PlayerCustom.IsOutOfTheWorld` = `Possessed || Kidnapped || (CultistActive && Cultist role)`. Kidnapped players are teleported to `(999,999,999)` and have their effects cleared; possessed targets likewise. Transforming them interferes with kidnapper/possessor logic.
- **Movement safety:** `PlayerController.CanMoveAnimation` (maintained by `PlayerCustom.UpdateCanMoveAnimation` → `CanMoveCustom()`) is false during actions (trap disarm, power casting, Asleep, Petrified, Downed, Tiny, Jump…). The curse loop waits for it; `Rpc_Forced_Transform` does not.
- **Authority:** `WolfDelay`, hunger, effect and stat side effects belong to the **state authority**; `HasInputAuthority` is used only for local UI/sound. `Rpc_Forced_Transform` is invoked from the server but executes on all peers — keep state writes behind `HasStateAuthority`, as it does for `WolfDelay`.
- **`Role` vs `IsWolf`:** only wolf-team players may transform back manually; curse / spellbook wolves are villager-team and cannot.

## Effects and flags that interact with wolf form

- `CustomEffect.KeepOnWolfTransformation` — see above.
- `ClearEffects()` is called on kidnap, possession, cultist capture, Sneak transform-back and similar cleanup; it despawns effects regardless of their remaining time.
- `CurseDormant` / `CurseTimer` are cleared on death (`OnDeadChangedPatch`), kidnap, possession, cultist capture / skull hunt, and the `PlayerCustom` reset.
- `EffectReverting` (15 s), `EffectResurrected` (300 s), `EffectSpiritResistance` (15 s default) are registered in `Plugin.cs` via `AddEffectToList`.

## Checklist for a new "force wolf form" feature

1. Decide whether it should be a **real** transformation (all side effects) or a **minimal** one; pick `TransformClass.TransformPrefix/Postfix` vs. extending `Rpc_Forced_Transform`, and document the choice.
2. Gate on: Beast / Cultist / Voodoo, `IsDead`, `IsOutOfTheWorld`, `IsWolfPup`, Zombie / Necromancer-resurrected, game state `Play`, and whether night is required.
3. Decide how to behave when `CanMoveAnimation` is false (retry like the curse loop vs. transform immediately).
4. Trigger it on the server and route state changes through the state authority; don't rely on a client-side `Despawned` / `Changed` callback to drive gameplay logic.
5. Define how it ends (curse timer, Reverting, dawn, manual transform back) and whether `Rpc_TransformBack` must be blocked or allowed; the prefix already blocks `Role != Wolf`, Necromancer-resurrected, possessing Possessors and `ForcedTransformation`.
6. Any effect applied before transforming must either set `KeepOnWolfTransformation => true` or tolerate being removed by `UpdateWolf` at transformation time.
7. Remember `OnCharacterChangedPatch` resets `WolfDaysWithoutTransformation` for **any** transformation source.
8. Add stats (`PlayerStats` actions), translation keys (`LycansNewRoles.resources.translations.json`, EN + FR) and `gameReference.json` entries when the behaviour is player-visible.
9. Test with: Beast, Voodoo and Cultist active; kidnapped / possessed targets; dead target; Eclipse day; target mid-action (trap disarm, casting); Infected-reverting wolf; Necromancer-resurrected wolf; wolf pup.
