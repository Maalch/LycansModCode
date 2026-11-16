---
name: lycans-gamereference-update
user-invocable: false
description: "Use when updating this repository's root gameReference.json for a new Lycans version by reviewing mod source code, relevant git commits, the local translations resource, and official patch notes when available."
---

# Updating the Game Reference

Use this skill to bring the repository-root `gameReference.json` up to date with a specific Lycans game version. It is a hand-maintained, French, player-facing reference; use the mod's source code and git history as the main evidence. The root `LycansNewRoles.resources.translations.json` contains `en` and `fr` dictionaries and is the local source for translated names and wording. Official patch notes are useful when provided or available, but are not required to proceed when code and version history are clear.

This repository is an ILSpy extract, not the complete original project. Some base-game behavior and dependencies are absent, and decompiled methods can be noisy. Do not infer behavior that the available code, translations, or patch notes cannot establish. The project guidance in `.github/instructions/lycans-mod.instructions.md` also notes that `gameReference.json` is not generated from code and that its legacy claims and omissions should be cross-checked.

## Principles

- Document **current, player-visible gameplay**: roles, powers, items, effects, win conditions, and rules. Include a bug fix only when it changes a gameplay behavior the reference describes; describe the resulting behavior, not the fix.
- Skip refactors, non-gameplay bug fixes, performance changes, visual/audio/particle work, UI polish, and keybinding-only changes.
- Keep descriptions concise and qualitative, matching neighboring French entries. Include numbers only when they define the mechanic and are supported by code or an authoritative note.
- Use the French translation values for in-game names and terminology. Paraphrase descriptions into clean French; remove markup, input-key tokens, and interpolation placeholders such as `<color>`, `#SECONDARYROLEPOWER`, and `{0}`. Do not turn dynamic UI strings into unsupported gameplay claims.
- Describe the current behavior, not the change: no changelog wording, version numbers, or appended notes such as “V377: …”.
- Edit only `gameReference.json` for a reference update. Do not update translations, C# code, or unrelated project files as part of this task.
- If code, translations, notes, or the existing reference disagree—or the extract does not establish a mechanic—ask the user rather than guessing.

## Workflow

1. **Establish the target version and commit range.**
   - Check `git status` and the current branch/`HEAD`; do not include unrelated working-tree changes.
   - If the user supplies a version, commit, range, or patch note, use that as the target. Otherwise identify the latest explicit game-version commit/tag and confirm the local history actually includes it. Do not assume the newest arbitrary commit is a game release or that `_meta.version` is current.
   - Find the previous reference update with `git --no-pager log --oneline -- gameReference.json`. Review relevant commits since that update through the target version; do not blindly include unrelated commits. If the requested “latest” version cannot be established from the available history, ask before editing.
   - Useful commands: `git --no-pager log --oneline -12`, `git --no-pager log --oneline -- gameReference.json`, `git --no-pager log --stat <last-reference-commit>..<target-commit>`, and `git --no-pager show <commit>`. If local history may be behind the requested remote version, verify it without pulling or changing the worktree.

2. **Triage relevant changes before editing.** Classify each gameplay-relevant code change and each available patch-note item as *document*, *skip*, or *unclear*. Skip non-gameplay changes; ask about unclear behavior or conflicting sources. A patch note can identify the intended player-facing change, while the implementation establishes exact triggers, targets, durations, conditions, and values.

3. **Trace the implementation.**
   - Search the relevant role, power, item, effect, manager, patch, and balancing code under `LycansNewRoles\`, `LycansNewRoles.NewPrimaryRoles\`, `LycansNewRoles.NewEffects\`, `LycansNewRoles.NewItems\`, `LycansNewRoles.SecondaryRoles\`, and related project folders. Check callers, role/config gates, and related effects—not just a declaration or translation string.
   - Use `.github/instructions/lycans-mod.instructions.md` to navigate the role enum ↔ reference sections, then verify the mapping against live code. Before describing a role or power as removed/legacy, check its enum uses and relevant assignment/utility switches; absence from one list is not proof it is unused.
   - Consult `LycansNewRoles.resources.translations.json` for the exact `fr` display name and available `translationKey`; the `en` dictionary can help identify a key. Preserve the existing category's field conventions and do not invent a key when none exists.
   - If official version-specific patch notes are available, use them as supporting context and cross-check them against the code. If they conflict, ask the user. If none are available, proceed from verified code/history and say so in the final report.

4. **Locate and update entries in the root `gameReference.json`.** Search by French name, id, and relevant translation key. Check related entries and cross-mentions—especially `relatedItems`, `gameRules`, effects, accessories, and gadgets—so descriptions do not contradict each other. Edit existing entries in place and avoid duplicates.

5. **Update metadata and any required mappings.** Set `_meta.version` from the target game version (for example, `v0.377` becomes `"0.377"`) and `_meta.lastUpdated` to today in `YYYY-MM-DD` format. Keep `_meta.sourceTranslationsFile` accurate for this repository; use `LycansNewRoles.resources.translations.json` when it is the translation source for the update.

6. **Validate and review.** Parse the JSON with PowerShell:
   `$null = Get-Content .\gameReference.json -Raw | ConvertFrom-Json -ErrorAction Stop`
   Then run `git --no-pager diff --check -- gameReference.json` and review `git --no-pager diff -- gameReference.json`. Do not run `dotnet build` for this extracted repository; the project guidance says it is not buildable as a standalone project.

7. **Report the result.** State the target version and commit range, the entries changed, relevant changes skipped and why, whether official patch notes were available, and any unresolved assumptions. Do not put commit hashes or patch-note citations into the JSON descriptions.

## Sections In The Root `gameReference.json`

| Section | Content | Notes |
|---|---|---|
| `_meta` | Reference description, version, date, translation source, notes | Keep provenance truthful; bump version and date for each update |
| `camps` | Villageois, Loups, Solo and their `roles` lists | Keep role names in sync when a main role is added or removed |
| `mainRoles` | Base, elite, special, duo, and solo roles | Follow neighboring fields (`camp`, `type`, descriptions, `relatedItems`, optional `subRoles`); `translationKey` is not present on every entry |
| `wolfPowers`, `villagerPowers`, `elitePowers` | Wolf powers, villager jobs, elite powers | Preserve each section's existing entry shape and cross-references |
| `secondaryRoles` | Secondary roles | Keep `descriptionVillager` and `descriptionWolf` consistent with the general description when present |
| `deadRoles` | Roles granted after death | Verify role availability and conditions in code |
| `accessories` | Accessories | May include `tinkererEffect` and a translation key |
| `gadgets` | Usable items | Use the existing `translationKey` and description pattern |
| `potionEffects`, `statusEffects` | Potion/scroll effects and broader player states | Keep overlapping entries consistent; use `tutorial`; `potionEffects` also has `type` and `durationSeconds` |
| `events` | Random game events | Describe gameplay effects, not visual presentation |
| `mayor` | Mayor abilities | Keep each ability grounded in implementation |
| `gameRules` | General game mechanics in `details` arrays | Use for rules that are not specific to one role or item |
| `gameLogValueMappings` | Maps game-log values to reference entries | Preserve the existing mapping shape and compatibility data; this mapping is not a substitute for checking mod code |

## Adding / Removing / Renaming Entries

- **New role or power**: add it to the section that matches the code and the existing schema. Use the French in-game name and `translationKey` from the local resource when available; follow neighboring optional fields rather than requiring every field on every category. Add or update relevant `relatedItems`, `camps[].roles`, and `gameLogValueMappings` entries.
- **Removed or replaced role/power**: verify that it is no longer active in code before removing or marking it legacy. Retain old game-log mappings when needed for historical values; set `legacy` or `replacedBy` only when supported by the code/history.
- **Renamed role or power**: keep the id stable unless the existing data model or code requires otherwise; update the French name, translation key, related references, and mapping keys as applicable. Preserve old mapping values for compatibility when warranted.
- **New effect**: add to `statusEffects`, and also `potionEffects` only if the implementation makes it obtainable from a potion or scroll. Record type, duration, and tutorial details only when supported by code or notes.
- **New general mechanic**: add it to the relevant `gameRules[].details` entry, or add a new rule entry when the mechanic is a separate system.

## Final Checklist

- [ ] Target version and comparison range are clear; working-tree changes were not accidentally included
- [ ] Relevant commits and source code were reviewed; each change was triaged
- [ ] Official patch notes were checked when available, and any conflict was resolved with the user
- [ ] Descriptions state verified current behavior in French, without markup or changelog wording
- [ ] Related descriptions, variants, tutorials, ids, role lists, and mappings remain consistent
- [ ] `_meta.version`, `_meta.lastUpdated`, and translation-source provenance are accurate
- [ ] JSON parses and the focused diff contains only intended reference changes
- [ ] Final report lists documented/skipped items, sources, and unresolved assumptions
