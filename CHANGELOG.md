# Changelog

## v0.3.2

- Added new changelogs to the top of this file instead
- Fixed the mod for Hotfix5 (method names were changed)
- The mod will not work on versions of the game earlier than Hotfix5 now

## v0.3.1

- Fixes lazy initialization of the frog database that prevented loading save data on the first load of each session

## v0.3.0

- Uses FrogDataLib to track which frogs have been rescued
- Adds XMLDocs to Thunderstore package for dependents

## v0.2.1

- Changes timing of contract insertion
- Now freezes main game contracts when freezing internal list of custom content
- Fix: Now uses the proper factory pattern for frog quests (addresses a warning in Player.log)

## v0.2.0

- Fixes: Inconsistency in how frogs are marked as counted.
- Known Issue: Frogs in the player's inventory during a load are always marked as new just to be safe.

## v0.1.1

- I forgor to depend on Yapalizer, wups

## v0.1.0

- Initial release
