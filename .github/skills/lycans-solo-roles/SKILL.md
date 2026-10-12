---
name: lycans-solo-roles
description: Use when reading, modifying, balancing, or debugging Solo Roles (PlayerNewPrimaryRole) in the LycansNewRoles mod — Village Idiot, Agent, Spy, Scientist, Lover, Beast, Mercenary, Voodoo (+ Zombie), Kidnapper, Cultist — or adding a new one. Covers assignment (normal + draft), shared progress / difficulty / win-check machinery, hunt-phase managers (Beast / Cultist / Voodoo), per-role abilities, win conditions, balancing constants, side effects on other systems (wolf transform, hunger, meetings, UI) and a checklist for new solo roles.
---

# Lycans Solo Roles

> The repo is an ILSpy extract and cannot be built. Base-game classes (`PlayerController`, `GameManager`, `LightingManager`…) are **not** in the repo. Anything attributed to the base game is inferred from how the mod hooks it. Many `PlayerCustom` RPC bodies are decompiler-noisy; the facts below were read from the cited methods. Line numbers are approximate (`~`) and drift — search by method name.

## Core concepts

| Concept | Where | Meaning |
|---|---|---|
| `PlayerCustom.NewPrimaryRole` | `PlayerCustom.cs` enum `PlayerNewPrimaryRole` | `None, VillageIdiot, Agent, Spy, Scientist, Lover, Beast, Voodoo, Mercenary, Kidnapper, Cultist, Zombie, Traitor`. `Traitor` is wolf-camp (not a solo role); `Zombie` is never assigned at start (`IsNewPrimaryRoleDisabled`) – it is created by Voodoo. `GetPrimaryRoleTarget` maps everything except Traitor to `SoloRole`. |
| Team vs role | `PlayerController.Role` | Solo-role holders are `Role == 0` (villager) **except the Wolf-side Lover** (`Role == 1`). They never receive a wolf/villager `PrimaryRolePower` and never become Angel / Ghost / Specter on death (`RpcKillPatch.cs` only does that for `NewPrimaryRole == None` villagers and non-Lover wolves/Traitors). |
| `PrimaryRolePowerRemainingUses` / `PrimaryRolePowerCooldownTimer` | `PlayerCustom` | Generic ability gate. `PrimaryRolePowerCooldownTimerExpired` does `RemainingUses++` for VillageIdiot, Scientist, Beast, Voodoo, Kidnapper, Cultist. `GiveNewPrimaryRole` sets `RemainingUses = 1` for the same six. Agent / Spy / Lover have no cooldown ability; Mercenary uses the timer as the "next contract" delay. |
| `SoloRoleObjectiveCount` / `SoloRoleObjectiveTarget` | networked ints | Progress / goal. Changing the count runs `SoloRoleObjectiveCountChanged` → refreshes the role description and (server) calls `GameManager.CheckForEndGame()`. **Village Idiot reuses `SoloRoleObjectiveTarget` as the selected power (0 curse / 1 bomb / 2 sabotage held item).** |
| `SoloRoleHalfDayProgress` | float | Fraction of the goal gained in the current half-day; feeds diminishing returns. Incremented only by `AddSoloRoleProgress(amount, goal)`; **reset to 0 for every living player at each Transition exit** (`GiveNewRolesPatch.cs`). |
| `NewPrimaryRoleUniqueBool` | networked bool | Role-specific flag: Spy = target in range, Mercenary = hunt mode, Cultist = hunt active, Kidnapper = silence toggled, Lover = telepathy toggled. `ClearEffectsPatch` resets it for some roles – do not treat it as persistent. |
| `PrimaryRoleTargetRef` | networked `PlayerRef` | Spy / Mercenary current target; Voodoo / Kidnapper cast target. Cleared for all solo players when the game goes to EndGame. |
| `PlayerCustomRegistry.GetSpecificNewPrimaryRole(role)` | `PlayerCustomRegistry.cs` | **Returns the first match only.** Fine for single-holder roles; wrong for Agent (2), Lover (2) and Zombie (many) – use `Where(...)` there. |
| `IsOutOfTheWorld` | `PlayerCustom.cs` | `Possessed \|\| Kidnapped \|\| (CultistActive && Cultist)`. Used by target pickers (Spy, Mercenary, Cultist spirits, Tournament…). |

## Assignment

Normal game start (`GiveNewRolesPatch.cs`, "Game start: solo roles"), after wolves are picked:

1. Pool = `Plugin.CustomConfig.SoloRoleActive` entries that are `true`, excluding `None`, `Traitor`, `Zombie`.
2. Loop `SoloRolesCount` times: take a random role from the pool and a random **villager without a solo role**; remove both from their lists (each role is used at most once).
3. **Agent** → a second villager is also made Agent and `Lover` is removed from the pool. **Lover** → if `LoverWolfReplacesVillager`, a second villager is promoted to wolf (`Role = 1`) and becomes the wolf Lover; otherwise an **existing wolf** becomes the wolf Lover. `Agent` is removed from the pool.
4. Then `TraitorsCount` Traitors and `WolfPupsCount` pups are taken from the remaining plain villagers.

Draft mode (`DraftManager.cs`, `DraftPlayerMainRole.Solo`): each solo slot is offered up to 3 enabled solo roles (`OfferedSoloRoles`, `SelectedSoloRole`). `Lover` is removed from the pool when `WolvesCount < 2 && !LoverWolfReplacesVillager`. When only one Agent/Lover was picked, a partner is pulled from villagers (Agent, or villager Lover) / wolves (wolf Lover when not replacing), preferring players with `DraftWantsSecondAgent` / `DraftWantsSecondLover`. A Lover's `Role` is chosen opposite to the existing partner, otherwise 50/50.

Role initialisation (`PlayerCustom.GiveNewPrimaryRole`, then the per-round setup at ~`PlayerCustom.cs:16470`):
- VillageIdiot: `SoloRoleObjectiveTarget` = 0 if a Warlock exists, else 1 if a Bomber exists, else 2 if a Saboteur exists, else random 0–2 (i.e. the first power that exists in the game).
- Mercenary: target = `MercenaryTotalObjective(...)`, gun unloaded, first cooldown = `2 × DayDuration`.
- Kidnapper: cooldown 45 s, 0 uses.

Allowed secondary roles per solo role: `GetAvailableSecondaryRoles` (`PlayerCustom.cs` ~5410). Solo roles never get Politician, Sherif or Telepath. Missing from each role's list: VillageIdiot – Metabolic; Agent – Teleporter, Gambler; Spy – Merchant; Scientist – Gambler, Medium, Astral, Merchant; Lover – Metabolic, Medium; Beast – Illusionist, Metabolic, Scavenger; Voodoo – Infected; Mercenary – Sprinter, Teleporter, Medium, Actor, Carabineer, Imitator, Merchant; Kidnapper / Cultist – Metabolic, Medium.

## Shared machinery

### Server per-second loop — `GameManagerCustom.FixedUpdateNetwork` (every `EachSecondGlobalTimer`)
Only while `LycansUtility.GameActuallyInPlay`, for each living player:
- **Village Idiot** (night only): boredom (`PrimaryRolePowerCurrentMaterials`, 0–10000) += `200 − Σ reductions`; every other living player within 30 m (40 m if in wolf form, × `DistanceMultiplierByMap`) reduces it by `3000·(1 − d/range) / (alive−1)` (×2 if that player is in wolf form); −400 more if the Idiot is in wolf form.
- **Spy**: if `PrimaryRoleTargetRef` is within 25 m (× map): `NewPrimaryRoleUniqueBool = true`, gain 20 / 34 (Spy still) / 58 (target visible) / 110 (visible + still) × `SpyPercentage/100` ÷ `SoloRoleDifficulty` × diminishing(12).
- **Scientist**: max over living wolf-form players within 40 m (× map) of `ScientistUtility.GetBasePower` (25 + 55·(1−d/range), × map multiplier, × 0.2 if that player's `Role != Wolf`, × `ScientistResearchSpeed/100`, ÷ difficulty; ×4 if visible, ×0.4 more if starving), × diminishing(7). A Possessed Scientist gets a flat 40 (capped at goal − count − 10).
- **Cultist**: `CultistChargeGainForSkullsCount(skulls)` (0/20/33/42/50/56+) × `CultistSpeed/100` × `CultistChargeGainMultiplierForPlayersAmount(alive)` (0.5 at ≤ 6 … 1.0 at 15) ÷ difficulty; goal 10000.
- **Energized** players: cooldown timers of Scientist, Beast, Voodoo, Kidnapper, Cultist (and Deceiver / Warlock) tick 1 extra second per second (Mercenary is not included).

### Per-tick (`PlayerCustom.FixedUpdateNetwork`, server)
Spy: `SpyGiveNewTarget` when there is no target / the target is dead (prefers non-wolf players within 80 m not already in `PrimaryRolePowerPlayersList`, falls back to anyone valid). Mercenary: when the cooldown is not running and the target is none / kidnapped → `MercenaryGiveNewTarget` (prefers players not in `MercenaryTargetsAlreadyHit`; also loads the gun).

### Difficulty & diminishing returns
- `GameManagerCustom.UpdateSoloRoleDifficulty` (each new day > 1): `clamp(1 + 0.2·(day−1) − 1.5·dead/total, 0.5, 1.75)`. Divides Spy / Scientist / Cultist gains and multiplies the Mercenary objective.
- `BalancingValues.SoloRoleDiminishingReturnsMultiplier(halfDayProgress, k) = 1/(1 + k·halfDayProgress)` (Spy k = 12, Scientist k = 7).

### Win-check — `CheckForEndgamePatch` (replaces `GameManager.CheckForEndGame`, server)
`flag` = "a solo objective is complete/pending, so Villagers must not win yet". Order:
1. Voodoo-hunt resolution if `VoodooTriggered` (see Voodoo).
2. **Return immediately (no end) while a Beast / Cultist / Voodoo hunt is active.**
3. Village Idiot (dead + `RoleDeathUniqueBool`) → Idiot wins.
4. Agent, Spy, Scientist, Kidnapper, Voodoo, then flags for Beast / Mercenary / Cultist, then Lovers, then the default wolf-vs-villager logic. Spy / Scientist / Kidnapper-goal / Voodoo-count only **end the game when `LocalGameState == 4` (Meeting)**; outside a meeting they only set `flag`.
5. Default: `num5` wolves (+ Traitors) vs `num6` the rest; a Wolf-side Lover counts as 2 on the villager side; `num5 == 0 && !flag` → Villagers win; `num6 == 0` → Wolves win; at a meeting Downed/Kidnapped wolves are subtracted from `num5`, then `num5 >= num6` → Wolves win; `Score >= MaxScore` → Villagers win.

`PlayerCustom.Rpc_End_Game(runner, winnerIndex)` picks the banner from the winner's `NewPrimaryRole`: Traitor → Wolves, Lover → Lovers, None → Wolves / Villagers by `Role`, anything else → `OtherSoloRole`; Voodoo-triggered games → Voodoo or Survivors. For a Mercenary winner every player in `MercenaryTargetsAlreadyHit` still alive is killed (effect index 11). Per-role `Stats.Victorious` is set in the same method (~`PlayerCustom.cs:18540`). It also clears `Kidnapped` on everyone.

### Phase hooks (`GiveNewRolesPatch.cs`)
- **Meeting enter**: a Lover whose partner is dead/missing dies (`LOVER_DEATH`); `SoloRolesProgressPanel.UpdateSoloRoles()`.
- **Meeting exit → Play (new day)**: `ManageSoloRolesHunt` (Mercenary hunt arming, Cultist hunt start), Mercenary gun reloaded, Kidnapper cooldown forced to ≥ 15 s with 0 uses, `GameManagerCustom.NewDay()`, event roll (`EventsManager.CanGenerateEvent` returns false while a Mercenary hunt or Cultist hunt flag is set).
- **Transition exit**: if `IsNight` → a Mercenary in hunt mode who is alive **wins**; Village Idiot boredom reset to 2500; Beast activation check; Zombie resurrection (only if the Voodoo is alive and no Beast is active); Spy target cleared; `SoloRoleHalfDayProgress = 0`.
- **`Rpc_Transition` postfix** (`NewPrimaryRoleNecromancerKillAtDawnPatch`): while `IsNight` (night ending) Zombies and Necromancer-resurrected players die; a Cultist or Voodoo whose hunt is still active is killed (`HUNT_FAILED`) and the hunt ends.
- **Game end / pregame**: `BeastManager`, `CultistManager`, `VoodooManager` `.Reset()`; `ResurrectedByNecromancer`, `HasZombieColor` cleared.

### Hunt managers (networked singletons)
| Manager | State | Effects while active |
|---|---|---|
| `BeastManager` | `BeastActive`, `BeastKills`, `BeastKillsObjective` | Day/night cycle **frozen** (`LightingManagerFixedUpdateNetworkPatch`); red fog (end 65); no wolf may transform (`Rpc_Forced_Transform`, `TransformPrefix`); sabotages off; normal endgame suspended. |
| `CultistManager` | `CultistActive`, `CultistCaptures`, `CultistCapturesObjective` | Time ×1.2; teal fog; hunger stops (`CheckHungerPatch`); wolves cannot transform; endgame suspended. |
| `VoodooManager` | `VoodooActive`, `VoodooTriggered` | Time ×0.5; purple fog; hunger stops; endgame suspended. **`ActivateVoodoo()` has no caller in the extract** (and `BalancingValues.VoodooRequiredZombiesForHunt` is unused) – the hunt looks like dead/legacy code here; `gameReference.json` does not mention it either. Treat `VoodooActive` branches as unreachable unless a caller is found. |

`ActivateBeast()` / `ActivateVoodoo()` both set every living wolf's `IsWolf = false` and refill its hunger (see the `lycans-wolf-transformation` skill).

## Roles

### Village Idiot
- **Win**: dies **during a Meeting** (`RpcKillPatch`: `LocalGameState == 4` ⇒ `RoleDeathUniqueBool = true`) → `CheckForEndGame` ends the game for him.
- **Ability** (`RemainingUses > 0`, Idiot and target human, target alive): the power is `SoloRoleObjectiveTarget`. The role key without a target (`Rpc_Activate_Primary_Role_Power_Without_Target`) cycles 0 → 1 → 2 → 0. 0 = give a villager-team target a dormant **Warlock curse** (`CurseDormant`, cooldown `VillageIdiotCurseCooldown` 120 s); 1 = **dormant bomb** (`BombDormant`, `BombGiverRef`, 110 s); 2 = **sabotage the target's held item** (`Sabotaged`, 75 s). Ranges come from `InteractionsManager` (curse 10 m, others 6 m). See the wolf-transformation skill for the curse lifecycle.
- **Hunger** (night, human): `PrimaryRolePowerCurrentMaterials × 0.0002`/s (boredom 10000 ≈ 2/s). Each loot item collected at night: boredom −1700 (`RpcCollectPatch`). `VillageIdiotCanTransform` exists in `GameConfig` but no consumer was found.
- **Killing him as a plain villager kills the killer** (`RpcKillPatch`, `KILLED_VILLAGE_IDIOT`); wolves / other solo roles are exempt. His completed sabotages do not count (`OnSabotageCompleted` is skipped for him – fake sabotages). Excluded from Detective "NotWolf" intel. Reach extensions in `LongerPlayerReachPatch` / `SabotageRaycastDistancePatch`.

### Agent (two players)
- **Win**: at a Meeting, alive count ≤ `AgentMaxSurvivorsToWin` (2 for ≤ 7 players, 3 for 8–12, 4 for 13+) and **exactly one Agent alive and not kidnapped**.
- **Ability**: at **night**, interact with the other Agent (≤ 2.5 m, visible, not Paranoia, not petrified) → `Rpc_Activate_Primary_Role_Power_With_Target` kills him (`OTHER_AGENT`) and gives the killer `EffectSneaky` 8 s. No cooldown, no `RemainingUses`.
- No passive loop. Both Agents are `NewPrimaryRole.Agent`; never use `GetSpecificNewPrimaryRole(Agent)` for logic that needs both.

### Spy
- **Win**: `SoloRoleObjectiveCount ≥ SpyGoal` (always 10000), alive, not kidnapped, **ends the game at a Meeting** (sets `flag` earlier). Progress is shown to everyone at meetings once ≥ 25 % (`UISoloRolesProgressPanel`).
- **Target**: `PrimaryRoleTargetRef`, assigned automatically (`SpyGiveNewTarget`), cleared at each Transition exit and when dead. The target is coloured (`PlayerColorInListForSpy`) and shown on the minimap. No ability key.
- Progress only accrues in the per-second loop; 25 m default range, bonuses for line of sight / standing still, diminishing returns (k = 12) within a half-day.

### Scientist
- **Win**: `SoloRoleObjectiveCount ≥ ScientistGoal` (`500 × player count`), alive, not kidnapped, ends the game at a Meeting.
- **Progress sources**: per-second loop (nearby wolf-form players) plus bursts when a wolf **transforms** within 40 m (`TransformClass.TransformPostfix`) or **kills** within 30 m (`RpcKillPatch`, not during meetings): `GetBasePower × 5 (×4 if visible) × diminishing(7)`. Map multiplier `ScientistPowerMultiplierByMap`.
- **Ability** (`RemainingUses`, cooldown 60 s): `GiveScientistGadget` – random allowed gadget among Smoke, Grenade, Sleeping Gas, Phasing Diamond (respects `GadgetsAvailability`). Sees all wolf-form players on the minimap.

### Lover (two players: villager-side + wolf-side)
- **Win**: both alive and neither kidnapped, and (alive count == 2) or (Meeting and alive ≤ 4). Banner = Lovers.
- **Link**: when either dies (`RpcKillPatch`) the partner dies too (`LOVER_DEATH`, same killer); also re-checked at Meeting enter. `FindLoverPartner()` = the other Lover.
- **Ability**: the role key toggles **Telepathy** (`NewPrimaryRoleUniqueBool` + `EffectTelepathy`, private channel). A Wolf Lover's kills heal the partner (`+0.4 × MaxHunger`, `PlayerCustom.cs` ~14443).
- **Hunger**: a villager Lover loses hunger **only at night**, `(0.5 + 0.02 × alive) × LoverVillagerHungerSpeed/100` per second, and **not at all** while the partner is in wolf form or kidnapped; `Satiated` does not protect a villager Lover (`CheckHungerPatch`). The Wolf Lover follows wolf rules.
- **Visuals**: pink fog / colour grading (`LoverFogColor`, end 55, `ColorAdjustmentManager`), partner highlighted on the minimap / lists.
- The Wolf Lover is counted as 2 villagers in the default endgame maths and is excluded from wolf-power assignment.

### Beast
- **Phase 1 – marking**: interact (≤ 7 m, `LocalGameState == 2`, `RemainingUses > 0`, human Beast, human non-wolf target) → `BeastMark = true`, use consumed, cooldown `BeastMarkCooldown = round(63 − 3·playerCount)` s (expiry grants the next use). Progress is hidden: meetings show `? / N` (`UISoloRolesProgressPanel`). Marked players are highlighted for the Beast.
- **Activation**: at Transition exit into night, if every other player is dead / Zombie / `BeastMark` / Kidnapped (and not `CultistActive`; Beast alive and not kidnapped) → `BeastManager.ActivateBeast()`.
- **Phase 2 – hunt**: `BeastActiveChanged`: objective `BeastTargetAmount(alive − 1) = ceil(0.6 × (alive−1))`; Beast hunger max, 7 s `WolfDelay`, `TransformedNight`, `IsWolf = true`, immobile while awakening. Each wolf-style kill on a non-Zombie (`PlayerCustom.cs` ~14400) → `BeastKills++` and feeds `+0.15 × MaxHunger`. `BeastKillsChanged` ends the game for the Beast when `BeastKills ≥ objective` **or** only one player is alive.
- **Beast dies while active** (`RpcKillPatch`): `BeastActive = false`, `IsNight = true`, `TimeOfDay = 6`, forced `Rpc_Transition` (the normal cycle resumes; the Beast is dead).
- **Side effects**: heartbeat audio for nearby players (`BeastManager.Update`), hunger drain 0.75 (vs 1.5) when burning, Carabineer knockback ×0.6 on the Beast. Constants: kill range 3, speed 1.25, health loss 0.0115/s, trap 0.025/s, shot 0.1, bomb 0.25, door damage bonus 50, sleeping-gas stacks ×0.5, negative-effect duration ×0.5, health gain per kill 0.15.

### Mercenary
- **Phase 1 – contracts**: `PrimaryRoleTargetRef` is assigned by `MercenaryGiveNewTarget` (when the cooldown is not running). The target is coloured and has a target icon (`PlayerMercenaryTargetIconComponent`). The Mercenary has a silent **rifle** (`IsGunLoaded`, reloaded each new day and on each new target): `Rpc_Custom_Shot_With_Target` puts the hit player to sleep (`EffectAsleep`: 30 s human / 8 s wolf), unloads the gun, and **if the hit player is the target**: `+200` progress, target cleared, `PrimaryRolePowerCooldownTimer = 90 s`, target added to `MercenaryTargetsAlreadyHit`. Killing the target by any means as the Mercenary also gives +200 / 90 s (`OnDeadChangedPatch`); if someone else kills the target, a new one is drawn.
- **Progress**: also `+ScoreValue` for every loot item he collects (`RpcCollectPatch`). Goal `MercenaryTotalObjective = round((Day + Night duration) × LootSpawnRate × MercenaryPercentage/100 × SoloRoleDifficulty × 18)`, recomputed at each new day while the goal is not reached.
- **Phase 2 – hunt**: at the **start of a day** (`ManageSoloRolesHunt`), if alive, not kidnapped and `Count ≥ Target`: `NewPrimaryRoleUniqueBool = true`, `EffectEscaping` (3600 s: speed ×1.1, regen) and `EffectSneaky` 10 s. Anyone interacting with him (`InteractPatch` → `Rpc_Assassinate`, ≤ 2.5 m) kills him (`MERCENARY_HUNT_KILL`). **No random events** while the hunt flag is set.
- **Win**: survive until the next **night** (Transition exit with `IsNight` and `NewPrimaryRoleUniqueBool`). Reaching the goal alone only sets `flag` in `CheckForEndGame` (blocks a Villager win).
- Quirk: `ManageSoloRolesHunt` `return`s right after arming the Mercenary hunt, so a Cultist hunt that would start the same day is skipped.

### Voodoo (and Zombie)
- **Reanimate**: interact on a **dead, non-wolf-form, non-Zombie** player ≤ 10 m (`RemainingUses > 0`, 2 s cast via `PrimaryRoleActionTimer`). Result (~`PlayerCustom.cs:10694`): target `Role = 0`, `NewPrimaryRole = Zombie`, powers and secondary role removed (stats `MainRoleChanges` logged); use consumed; cooldown `VoodooReanimationCooldown(zombies, players) = round(VoodooBaseCooldown × lerp(1, 0.5, lootProgress) × (1 + 0.5·zombies) × 0.85)` (base 200 s for ≤ 9 players … 100 s at 15). If it is already night (and no Beast) the Zombie is revived at once; otherwise at the next Transition exit into night.
- **Strengthen**: interact on a living Zombie ≤ 2.5 m → `EffectStrengthened` 40 s (extra speed ×1.15, running drain 12/s instead of 25/s).
- **Zombie**: moves ×1.05 (×1.2 during a hunt); hunger is only drained by running (25/s, 12 strengthened) and regenerates 4/s otherwise (`CheckHungerPatch`); interact ≤ 2.5 m on a non-Voodoo / non-Zombie: a **human** target dies (`BY_ZOMBIE`) and the Voodoo's remaining cooldown is multiplied by 0.3 (min 1 s); a **wolf-form** target is Downed 6 s and knocked back, gains Resilience 18 s, and the Zombie dies. Zombies die at dawn (`Rpc_Transition` postfix). Many players share `NewPrimaryRole.Zombie`, so never rely on `GetSpecificNewPrimaryRole(Zombie)`.
- **Win** (`CheckForEndGame`): Voodoo alive and not kidnapped, `(Voodoo + Zombie count — dead ones included) ≥ living non-Voodoo non-Zombie` **and** `> living wolves + Traitors`; ends the game only at a Meeting. Night revival requires the Voodoo to be alive.
- The `VoodooActive` / `VoodooTriggered` hunt: see the hunt-manager table (unreachable in the extract).

### Kidnapper
- **Abduct**: interact (≤ 5 m, `LocalGameState == 2`, Kidnapper human, target alive / human / not Dying / not already kidnapped, `RemainingUses > 0`) → 3 s `PrimaryRoleActionTimer`; at completion (~`PlayerCustom.cs:10743`) the target gets `Kidnapped = true`, `SoloRoleObjectiveCount++`, curse / bomb dormant states cleared, sound ≤ 7 m, use consumed, cooldown `KidnapperFinalCooldown(abducts, goal, alive, total) = base(total) × lerp(0.5, 2, abducts/goal) × lerp(0.15, 1.8, alive/total)` (base 65–90 s, 60 at 12 players). First cooldown 45 s; each new day the cooldown is forced to ≥ 15 s with 0 uses (expiry grants the use).
- **Silence**: the role key toggles `EffectKidnapperSilence` (`NewPrimaryRoleUniqueBool`): kidnapped players' voice volume 0.4 → 0.15, costs the Kidnapper extra hunger 0.5/s (0.3/s in meetings).
- **Kidnapped state** (`KidnappedChanged`, `IsOutOfTheWorld`): effects cleared, curse / bomb dormant cleared, `TeleportToVoid()`, camera follows the Kidnapper, special icon / colour in lists, vote counts hidden (`KidnapperShowPlayerVotesCountsPatch`) and excluded from the "everyone voted" skip check (`CheckSkipVotePatch`), excluded from target lists and endgame counts. Released (teleported to a random teleporter when in Play) when the Kidnapper **dies or despawns**, when a hunt starts (`ActivateVoodoo`), and at game end. A kidnapped Beast / Spy / Scientist / Mercenary / Voodoo / Cultist cannot win.
- **Win**: alive and any of: no other free living player (immediate); at a Meeting ≤ 1 other free living player; at a Meeting with `Count ≥ KidnapperTargetAmount` (3 for ≤ 11 players, 4 otherwise; reaching it outside a meeting only sets `flag`).

### Cultist
- **Skulls**: ability = place a `CultistSkull` (1 s cast, ~`PlayerCustom.cs:10749`; cooldown `CultistSkullCreationCooldown(skulls + 1)`: 20 / 20 / 40 / 80 / 125 / 175 s). Placement constants in `BalancingValues`: ≥ 10 m from other skulls, ≥ 3 m from corpses, outside `CultistSkullForbiddenAreasByMapId` (radius 10). Other players can destroy a skull (`InteractPatch` → `CultistSkull.Rpc_Destroy_Skull`, constant `CultistSkullDestructionCooldownMultiplication` 0.5). Skulls despawn when the Cultist dies or the game leaves Play. Charge comes from the per-second loop (more skulls = faster, with diminishing steps).
- **Goal 10000** (`CultistGoal`): before the hunt it only sets `flag`. At the next **day start** `ManageSoloRolesHunt` resets `SoloRoleObjectiveCount = 0`, calls `ActivateCultist()`, sets `NewPrimaryRoleUniqueBool = true`, `TeleportToVoid()`s the Cultist (dormant states / effects cleared), spawns a player-controlled `GameObjectCultistSpirit` (`PlayerSummonedSpiritComponent`) at the map spawn and applies 8 s paralysis.
- **Hunt**: `CultistCapturesObjective = ceil(0.8 × (alive − 1))` (`CultistTargetAmount`), Cultist hunger refilled, time ×1.2. `CultistManager.FixedUpdateNetwork` spawns a `CultistSkullSpirit` every `max(2, 8 − 0.4 × free living players)` s (25 % at a teleporter, else at a random free player); spirits move at 2.25 (0.75 when slowed by a wall), live 45 s and **capture** on contact (clear effects, `EffectCaptured` 3600 s, `CultistCaptures++`; they ignore dead, Cultist, already captured, Necromancer-resurrected and out-of-world players). The Cultist's own spirit also captures (`Rpc_Spirit_Attack`, 2 s capture cooldown). Free players rescue captives with `Rpc_Save` (2 s `SurvivalistSaveTimer` in code; `gameReference.json` says 3 s). `CultistCapturesChanged` ends the game for the Cultist at the objective.
- **Fail**: the hunt is still running when the night ends (`Rpc_Transition` postfix) → Cultist killed (`HUNT_FAILED`), hunt off.

## Other places that special-case solo roles

- `PlayerCustom`: `GetPrimaryRoleTarget`, `GetNewPrimaryRoleString` (Lover → `_VILLAGER` / `_WOLF`), colours `NewPrimaryRole*Color`, list colours `PlayerColorInListFor*`, `Rpc_End_Game` scoring block (~`PlayerCustom.cs:18596`).
- `UpdateRoleUtility.cs` (role description + live status text), `UIRoleDescription.cs`, `ShowRoleDescriptionPatch.cs`, `UILastGameSummaryPanel/Kill.cs`, `PlayerStats.cs` (`MainRoleInitial`, `MainRoleChanges`, death types `OTHER_AGENT`, `LOVER_DEATH`, `KILLED_VILLAGE_IDIOT`, `MERCENARY_HUNT_KILL`, `BY_BEAST`, `BY_ZOMBIE`, `HUNT_FAILED`).
- Detective intel `WolvesAndSoloRolesRemaining` counts living players with a `NewPrimaryRole` other than None / Traitor / Zombie.
- Minimap: `MinimapComponent` legend (Spy / Mercenary target, Lover), `MinimapPlayerComponent` colours.
- Config: `GameConfig.SoloRolesCount`, `SoloRoleActive` (per-role toggles), `MercenaryPercentage`, `SpyPercentage`, `ScientistResearchSpeed`, `CultistSpeed`, `LoverVillagerHungerSpeed`, `LoverWolfReplacesVillager`; UI built in `GameConfigAddNewSettingsPatch.cs`.
- `gameReference.json` (`mainRoles`, camp `solo`) and `.github/instructions/lycans-mod.instructions.md` hold the French player-facing names and the enum ↔ role mapping; keep them in sync.

## Checklist for a new solo role

1. Add the enum value to `PlayerNewPrimaryRole`; update `GetPrimaryRoleTarget`, `GetNewPrimaryRoleString`, the colour switch, `GetAvailableSecondaryRoles`, `UpdateRoleUtility` description / status keys, `UIRoleDescription`, and the `Rpc_End_Game` scoring / `Stats.Victorious` switches (~`PlayerCustom.cs:18540–18610`).
2. Decide whether it is unique (`GetSpecificNewPrimaryRole`) or multi-holder (`Where`, like Agent / Lover / Zombie) and whether assignment needs a special case in both `GiveNewRolesPatch` and `DraftManager`.
3. If it has a hunt-like phase, follow the manager pattern (networked singleton, `Reset()` on pregame / end, gate on it in `CheckForEndgamePatch`, `TransformPrefix`, `Rpc_Forced_Transform`, `CheckHungerPatch`, lighting / fog patches) and define how it fails (`Rpc_Transition` postfix).
4. Add the win check to `CheckForEndgamePatch` (immediate vs Meeting-only; whether it must set `flag` to block a Villager victory) and make it alive / not-`Kidnapped` aware.
5. Use `AddSoloRoleProgress` + `SoloRoleHalfDayProgress` for passive progress; divide by `SoloRoleDifficulty` where it should scale.
6. If it uses the generic ability gate, add it to `GiveNewPrimaryRole`, the exclusion list in `GivePrimaryRolePower`, and `PrimaryRolePowerCooldownTimerExpired`; add it to the Energized cooldown list if appropriate.
7. Respect `IsOutOfTheWorld` / `Kidnapped` / Zombie / `ResurrectedByNecromancer` in every target picker.
8. Add the `SoloRoleActive` config toggle, translation keys (`NALES_ROLE_*`, `NALES_ROLE_DESCRIPTION_*`, `NALES_UI_*`, EN + FR), balancing constants in `BalancingValues.cs`, the `gameReference.json` entry and the mapping in the instructions file.
9. Test with: minimum lobbies, Draft mode, Lover wolf replacing villager on/off, Kidnapper combined with each hunt role, Necromancer-resurrected players, dead / kidnapped targets, meeting entry mid-action, and every endgame path (Villagers, Wolves, each solo role).
