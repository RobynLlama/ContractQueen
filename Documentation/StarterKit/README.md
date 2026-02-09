# 📦 Starter Kit Overview

The files in this directory provide a complete, boilerplate implementation of a custom contract. You can drop these into your project and rename the classes to get your first contract running in minutes.

## Included Files

| File | Purpose |
| --- | --- |
| **`Plugin.cs`** | Demonstrates how to "talk" to Contract Queen during your mod's setup. |
| **`NewContract.cs`** | A skeleton `GameplayTaskSO` including a static Factory and subscription logic. |
| **`NewEvent.cs`** | A simple event wrapper to show how to trigger progress from anywhere in the game. |
| **`Localization.cs`** | An example of how to handle the `name` and `description` keys for the UI. |

---

## Quick Start

### 1. The "Awake" Registration

To ensure **Contract Queen** can deterministically sort your contracts, you must register them during your plugin's `Awake()` or `Start()` method.

> 👑 The Queen's Advice
>
> Once the title screen is visible, Contract Queen freezes the contract list and refuses any additional modification

### 2. Customizing your Logic

1. **Define the Goal:** Open `NewContract.cs` and change the return value of `CalculateTargetProgress()` or adjust the serialized field.
2. **Hook up the Event:** In `NewEvent.cs`, I've defined an event for you. No contract progress will be made unless this event is called. You may need to write patches to existing game objects to implement logic around calling events or there may be existing events you can borrow.

## 👑 The Queen's Advice on Factories

The `NewContract.Factory()` method in the starter kit is the recommended way to generate your task.

- **Avoid `new`:** Because `GameplayTaskSO` is a ScriptableObject, creating it via `new` can cause confuse Unity. We're in the Unity environment so we play by the Unity rules, where we must.
- **Use `ScriptableObject.CreateInstance<T>()`:** The factory handles this for you, ensuring the object is memory-managed correctly by the engine.
- **Parameterization:** You can pass variables into your Factory (like `int difficulty`) to create multiple variations of the same task with different rewards, targets or logic.

I strongly recommend that you consider your GameplayTaskSO as a template or a base and reuse it as much as possible with parameterization
