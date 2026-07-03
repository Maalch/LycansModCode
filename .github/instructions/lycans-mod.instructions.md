---
description: "Use when adding new roles, effects, items, patches, or any code to the LycansNewRoles BepInEx mod. Covers Harmony patch conventions, Fusion networking, effect system, role system, balancing constants, translation keys, and project structure."
applyTo: "**/*.cs"
---

# LycansNewRoles Mod – Coding Conventions

## Project Structure

| Folder | Contents |
|--------|----------|
| `LycansNewRoles/` | Core mod files: patches, managers, PlayerCustom, Plugin |
| `LycansNewRoles.NewEffects/` | Custom effects (all extend `CustomEffect`) |
| `LycansNewRoles.NewItems/` | Custom item definitions |
| `LycansNewRoles.NewItems.Accessories/` | Accessory items |
| `LycansNewRoles.NewMaps/` | Custom map logic |
| `LycansNewRoles.NewPrimaryRoles/` | Components and logic for new primary roles |
| `LycansNewRoles.SecondaryRoles/` | Harmony patches implementing secondary role behaviours |
| `LycansNewRoles.PowerObjects/` | Power object implementations |
| `LycansNewRoles.Sabotages/` | Sabotage logic |
| `LycansNewRoles.Stats/` | Stats tracking |

## Harmony Patches

- Attribute: `[HarmonyPatch(typeof(TargetClass), "MethodName")]`
- Class modifier: `internal class`
- Class name: `[DescriptiveName]Patch` — be specific (e.g. `SecondaryRoleEngineerBetterDoorKickPatch`, not `DoorPatch`)
- All patch methods must be `private static`
- **Prefix** returns `bool` — return `false` to skip the original; return `true` to let it run
- **Postfix** returns `void`
- Use `__instance` to access the patched instance; parameter names must match the original method signature exactly
- Use `PlayerCustomRegistry.GetPlayer(playerRef)` to retrieve the mod's `PlayerCustom` data for a player

```csharp
[HarmonyPatch(typeof(TargetClass), "MethodName")]
internal class MyFeaturePatch
{
    private static bool Prefix(PlayerRef actor, TargetClass __instance)
    {
        PlayerCustom player = PlayerCustomRegistry.GetPlayer(actor);
        if (player.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothEngineer)
        {
            // custom logic
            return false; // skip original if needed
        }
        return true;
    }
}
```

## Role System

### Adding a new primary role

1. Add the enum value to `PlayerCustom.PlayerNewPrimaryRole` in `PlayerCustom.cs`
2. Add corresponding logic/components under `LycansNewRoles.NewPrimaryRoles/`
3. Check role via `player.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.MyRole`

### Adding a new secondary role

1. Add the enum value to `PlayerCustom.PlayerSecondaryRole` — prefix with `Both` (e.g. `BothMyRole`)
2. Implement behaviour as one or more Harmony patches under `LycansNewRoles.SecondaryRoles/`
3. Check role via `player.SecondaryRole == PlayerCustom.PlayerSecondaryRole.BothMyRole`

### Adding a new role power

1. Add the enum value to `PlayerCustom.PlayerPrimaryRolePower`
2. The `None` value must remain first

## Effect System

- All custom effects extend `CustomEffect` (which extends `Effect`)
- File name: `[EffectName]Effect.cs` in `LycansNewRoles.NewEffects/`
- Class must be decorated with `[NetworkBehaviourWeaved(N)]` where N matches networked property count
- Register new effects in `Plugin.NewEffects` list in `Plugin.cs`

Required overrides:

```csharp
[NetworkBehaviourWeaved(0)]
public class MyEffect : CustomEffect
{
    public override string CustomEffectName => "LycansNewRoles.EffectMyEffect";
    public override string TranslateKey    => "NALES_EFFECT_MYEFFECT";
    public override Color  Color           => Color.white;
    public override EffectType CustomEffectType => (EffectType)1;
}
```

- `CustomEffectName` pattern: `"LycansNewRoles.Effect[Name]"`
- `TranslateKey` pattern: `"NALES_EFFECT_[NAME_UPPERCASE]"`
- Add the translation string to `LycansNewRoles.resources.translations.json`

## Fusion Networking

- Networked classes must extend `NetworkBehaviour` and be decorated with `[NetworkBehaviourWeaved(N)]`
- Networked properties use `[Networked]` + `[NetworkedWeaved(offset, size)]` attributes and `unsafe` keyword
- Always check `((SimulationBehaviour)this).HasStateAuthority` before mutating networked state
- Use `TickTimer.CreateFromSeconds(Runner, duration)` for server-side timers
- RPCs must follow the game's existing naming convention (`Rpc_MethodName`)

```csharp
[Networked]
[NetworkedWeaved(0, 1)]
public unsafe TickTimer MyTimer { get; set; }
```

## Balancing Constants

- All numeric tuning values (durations, ranges, thresholds) belong in `BalancingValues.cs` as `public const`
- Naming pattern: `[Feature][Property]` — e.g. `EffectDurationBomb`, `AgentEliminationRange`
- Never hard-code magic numbers in patch or component files; always reference `BalancingValues`

## Namespaces

| Code location | Namespace |
|---------------|-----------|
| `LycansNewRoles/` | `LycansNewRoles` |
| `LycansNewRoles.NewEffects/` | `LycansNewRoles.NewEffects` |
| `LycansNewRoles.NewPrimaryRoles/` | `LycansNewRoles.NewPrimaryRoles` |
| `LycansNewRoles.SecondaryRoles/` | `LycansNewRoles.SecondaryRoles` |
| Others | Match folder name |

## Logging

Use the static logger from `Plugin`: `Plugin.Logger.LogInfo("...")` / `LogWarning` / `LogError`.

## Translation Keys

All user-facing strings must have a translation key in `LycansNewRoles.resources.translations.json`.  
Key patterns:
- Effects: `NALES_EFFECT_[NAME]`
- Roles: `NALES_ROLE_[NAME]`
- Items: `NALES_ITEM_[NAME]`
- UI/Other: `NALES_[CATEGORY]_[NAME]`
