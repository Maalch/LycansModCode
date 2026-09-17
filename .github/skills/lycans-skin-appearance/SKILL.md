---
name: lycans-skin-appearance
description: Use when reading, modifying, or debugging player appearance in the LycansNewRoles mod — skin selection, skin color, player color, camouflage-driven appearance overrides, flashing, hats, or wolf color. Covers PlayerCustom's networked appearance properties, the local render-cache fields, LycansUtility's mesh-swap/recolor helpers, and the CharacterSkin data model.
---

# Lycans Player Appearance System (Skin / Skin Color / Color)

## Data model — three independent networked axes

| Property (on `PlayerCustom`) | Meaning | Set via |
|---|---|---|
| `SkinIndex` | Which skin (0 = base vanilla model, >0 = index into `Plugin.Skins`, the mod's added skins) | `Rpc_Change_Skin` |
| `SkinColorIndex` | Which texture/recolor variant of that skin | `Rpc_Change_Skin_Color` |
| `ColorIndex` | The player's outfit/name color (also used for skin 0's plain recolor and minimap/UI) | `Rpc_Change_Color` |

All three are `[Networked]` ints on `PlayerCustom.cs`. They represent the player's **real, chosen** appearance and are what gets persisted to `PlayerPrefs` (`FavoriteSkin`, `FavoriteSkinColor`, `FavoriteColor`).

## Local render cache — why it's separate from the networked values

`PlayerCustom` also has three **non-networked** fields used purely to avoid redundant mesh swaps/material updates on the local client:

```csharp
public int CurrentSkinIndex = 0;
public int? CurrentSkinColorIndex = null;
public int? CurrentColorIndex = null;
```

These track "what is actually instantiated/applied right now", not "what the player picked". They are normally equal to the real values, but they intentionally **diverge during camouflage**: the real `SkinIndex` stays whatever the player picked, while `CurrentSkinIndex` gets forced to `0` so the displayed mesh reverts to the plain default model (see below).

## `PlayerCustom.UpdateModelIfNeeded(bool hasCamouflage = false)`

The single entry point that reconciles "what should be shown" with "what is shown". Two independent blocks:

1. **Mesh swap** — `int num = hasCamouflage ? 0 : playerCustom.SkinIndex;`
   If `num != CurrentSkinIndex`, calls `LycansUtility.UpdateVillagerSkin(...)` to destroy the old `SkinnedMeshRenderer` and instantiate `Plugin.Skins[num].SkinMeshRenderer`, rebinding bones to the metarig. Updates `CurrentSkinIndex = num`.
2. **Color/texture refresh** — runs when color info was never applied yet, when the real `SkinColorIndex`/`ColorIndex` changed, or when `IsFlashing` is true (a temporary `FlashPlayer()` color needs to be reapplied over). Calls `LycansUtility.UpdateVillagerSkinColor(...)`.

## `LycansUtility` helpers

- `UpdateVillagerSkin(SkinnedMeshRenderer current, int skinIndex, PlayerController)` — destroys/replaces the mesh GameObject, rebinds bones/rootBone by name lookup against the metarig, reassigns `villagerMeshRenderer`/`skinnedMeshRenderer` fields via `Traverse`. Returns the new renderer.
- `UpdateVillagerSkinColor(SkinnedMeshRenderer, int skinIndex, int skinColorIndex, int playerColorIndex, PlayerCustom)`:
  - `skinIndex == 0` → vanilla path: `material.mainTexture = ColorManager.GetTexture(playerColorIndex)`, plain `material.color` (with overrides below).
  - `skinIndex != 0` → modded skin path: `material.SetTexture("_SkinTexture", Plugin.Skins[skinIndex].SkinTextures[skinColorIndex])` and `"_TopTexture"` from `TopTextures`.
  - Special-state overrides applied on top regardless of skin index: `Petrified` → `SkinColorPetrified`, `HasZombieColor` → `SkinColorZombieHuman`/`SkinColorZombieWolf`, `Poison` → `SkinColorPoison`. Wolf mesh renderer color is set alongside villager mesh in these branches.
- `UpdateVillagerHat(SkinnedMeshRenderer, int hatIndex)` — independent of skin; toggles children under `metarig/.../HatsContainer/Hats`.

## `CharacterSkin` (per-skin static data, in `Plugin.Skins`)

```csharp
GameObject Metarig, SkinMeshRenderer;
Shader DefaultShader;
Avatar Avatar;
bool IsOriginalSkin;
List<Texture> SkinTextures, TopTextures;
Texture SkinPetrified, SkinZombified, SkinPoisoned;
```

`Plugin.Skins[0]` is the original/vanilla skin. Additional entries are the mod's custom skins, registered in `Plugin.cs` (search `characterSkin.SkinMeshRenderer =`).

## Camouflage interaction

`PlayerCustom.Camouflage` (networked bool, set by `CamouflageEffect`) and the Shadow/Spotter role fog mechanics feed into `CamouflageLevelForPovPlayer` (computed per-observer in `PlayerCustom.UpdateVisible`). When `CamouflageLevelForPovPlayer > 0`:
- A camouflage shader (`CamouflageLevel1/2/3Shader`) is applied to the villager/wolf materials.
- `UpdateModelIfNeeded(hasCamouflage: true)` is called so the mesh reverts to skin `0` for observers who can't see through it (see gotcha above for the color-argument mismatch).

## Other appearance touch points

- `PlayerCustom.FlashPlayer(Color)` — temporary flash; checks `CurrentSkinIndex == 0` to decide between `material.color = color` (vanilla) vs `material.SetColor("_SkinTexture", color)` (modded skin), then sets `IsFlashing = true` so `UpdateModelIfNeeded` restores the real color shortly after.
- `PlayerCustom.UpdateWolfColor()` — resets the wolf mesh material color to `WolfColor` (which itself accounts for Beast/zombie recoloring).
- `PlayerCustom.PlayerVisualToShowForPovPlayer()` — returns a *different* `PlayerCustom` (not `this`) when the local pov player is `Confused` or when `IllusionTarget` is set; `UpdateModelIfNeeded`/`UpdateIllusion` use this to decide whose skin/colors to actually render, which is why `playerCustom` and `this` are not always the same instance in that code.

## When adding new appearance-affecting features

- Always change `SkinIndex`/`SkinColorIndex`/`ColorIndex` through the existing `Rpc_Change_*` RPCs (state-authority write + `UpdateModelIfNeeded()`), never set the networked property directly on a remote client.
- If a new effect needs to hide/override a player's real skin (like camouflage does), follow the `hasCamouflage` pattern: compute an effective skin index locally, pass it consistently to *both* the mesh-swap and the color call, and don't let `playerCustom.SkinIndex` leak into the color call in the overridden case.
- Any new per-state color override (like petrified/zombie/poison) belongs in `LycansUtility.UpdateVillagerSkinColor`, gated on the same `PlayerCustom` flags pattern.
