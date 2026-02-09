# Contract Queen Documentation

The Contract Queen Documentation contains everything you need to understand creating and registering contracts in YAPYAP.

> 👑 The Queen's Advice
>
> Sections like this contain tips, advice and best practice information related to the current topic.

## Quick Links

- **[StarterKit](./StarterKit/):** Contains plug-n-play sample that you can drop into an existing project
- **[TaskReference](./GameplayTaskSO.md):** An overview of the GameplayTask scriptable object your contracts must inherit from and the `runtimeTask` item you will be sent in your update events
- **[Multiplayer Notes](./TaskSyncing.md):** Notes or guidelines on what I've noticed works and doesn't work when triggering or modifying task state in multiplayer

## Why Contract Queen?

### The Problem: Non-Deterministic IDs

In the vanilla YAPYAP game, contracts are assigned an integer ID based on their index in the global contract list. This works fine if you never want to remove contracts and only have a single source of truth for new contracts (the developers) but doesn't work once modders get involved.

### The Solution: The Contract Queen Protocol

Other solutions to this problem are cumbersome or require modders to "claim" ID ranges like in old Minecraft mods. Luckily, we live in the future and **Contract Queen** can dynamically and deterministically assign IDs to every contract so long as all players have the same contract mods installed.

#### Namespace & Contract Key

Instead of relying on file names or load order, authors must provide:

- **Namespace:** Your mod name or unique handle (e.g., `FrogsUnlimited`).
- **Contract Key:** A unique name for that specific task (e.g., `RescueFiveFrogs`).

#### Deterministic Alphabetical Sorting

Before the game starts, Contract Queen gathers every registered custom contract and sorts them:

- First by **Namespace** (Alphabetical)
- Then by **Contract Key** (Alphabetical)

This creates a predictable, "fixed" sequence that will be identical on every machine running the same set of mods.

#### Protecting Vanilla YAPYAP Integrity

To ensure we don't break the base game's logic or saved progression, all custom contracts are appended to the **end** of the existing vanilla contract list. We never "inject" between vanilla IDs; we only extend the horizon. This should allow new contracts to be added to existing vanilla-only saves, but a new save is generally recommended for any changes in contracts.

### Example of Sorting Logic

| Namespace | Contract Key | Resulting Order |
| --- | --- | --- |
| `AlphaMod` | `Z_LastTask` | 1st Custom ID |
| `ZebraMod` | `A_FirstTask` | 2nd Custom ID |

*Even if `ZebraMod` loads its files into memory first, the Alphabetical Sort ensures `AlphaMod` always takes the earlier ID slot.*
