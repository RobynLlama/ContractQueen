# GameplayTaskSO Overview

This is your cheat-sheet for what parts of the scriptable task object perform what function and what of them are required for custom content. This document contains the minimum fields and methods needed to create working contracts

## Important Members

| Member Name | Category | Type | Description |
| --- | --- | --- | --- |
| **`nameLocalisationKey`** | Field | `string` | Key used for the contract's display name. |
| **`descriptionLocalisationKey`** | Field | `string` | Key for flavor text; supports `{0}/{1}` progress tokens. |
| **`pointValue`** | Field | `int` | The amount of "Chaos" rewarded upon completion. |
| **`CanBeCreated()`** | Method | `bool` | Logic to determine if the task can spawn/be active. |
| **`CalculateTargetProgress()`** | Method | `int` | Defines the threshold (Max Value) for completion. |
| **`SubscribeToProgressEvents()`** | Method | `void` | Used to hook game events to `runtimeTask.AdvanceProgress()`. |
| **`UnsubscribeFromProgressEvents()`** | Method | `void` | Essential cleanup to unhook events and prevent memory leaks. |

### Fields

> 👑 The Queen's Advice
>
> The lower casing on these fields is both intentional and important as they are private/protected members of the GameplayTaskSO.
> Your extending class will need to either have a factory to set these or you will need to assign them in the editor (not featured in this guide)

- `nameLocalisationKey:` The key used to look up the localized name of this item
- `descriptionLocalisationKey:` The key used to look up the description of your contract, this can include the identifiers `{0}` and `{1}` which represent the contract's current count and required count for completion. Example: "Rescued {0}/{1} Frogs"
- `pointValue:` This is the amount of chaos completing this task rewards

### Override Methods

#### Creation Logic

`bool CanBeCreated()`:

This method is used by the Task system to determine if a new instance of your contract can be created. You can use this to limit the times a quest can ever be completed or stop your task from appearing before a day or quota threshold has been passed. The sky is really the limit, this method is pretty powerful for creating interesting contracts that seem to respond to the group's progress.

#### Progress Tracking

`int CalculateTargetProgress()`:

This method should return the threshold at which a task is completed. If your contract is to "Rescue 5 frogs" then this method should return 5, because that's when the contract is considered completed.

> 👑 The Queen's Advice
>
> Don't just return 5 from your `CalculateTargetProgress()` call, store it as a value that can be serialized in your SO. This allows you to reuse your GameplayTaskSO with either serialized assets and values set in the editor or a factory that sets it when created live.
>
> ```cs
> [SerializeField]
> private int frogCount = 5;
> ```
>
> ```cs
>
> public static RescueFrogsTask Factory()
>  {
>    var frog = new RescueFrogsTask(int count)
>    {
>      nameLocalisationKey = ContractQueenPlugin.contractName,
>      descriptionLocalisationKey = ContractQueenPlugin.contractDesc,
>      pointValue = 50 * count,
>      frogCount = count
>    };
>
>    return frog;
>  }
> ```

#### Progress Events

`void SubscribeToProgressEvents(GameplayTask runtimeTask)` and `void UnsubscribeFromProgressEvents(GameplayTask runtimeTask)`: These critical methods must be present at runtime or your contract will not have any logic and never be completed.

```cs
public override void SubscribeToProgressEvents(GameplayTask runtimeTask)
  {
    var del = (FrogContractBehavior frog) =>
    {
      ContractQueenPlugin.Log.LogMessage("Counted a frog for a quest");
      runtimeTask.AdvanceProgress();
    };

    runtimeTask.SetProgressHandler(del);
    Events.FrogCountedEvent += del;
  }
```

In this example subscription used in the frog collecting contract shipped with Contract Queen, I create a local delegate that captures the given GameplayTask. The delegate logs a message for debugging and then advances the contract progress by 1 each time its called.

The next important step here is to assign that delegate to both the runtimeTask's handler (for later use) and to an event that can fire when the task should update. You can include more logic in the delegate than I do but my event `FrogCounted` already filters out frogs that have been previously counted so no other logic was needed. When responding to events you don't have full control over you may need to s whether or not to increment progress and by how much.

> 👑 The Queen's Advice
>
> Use the passed-in value for `SetProgressHandler(int amount = 1)` to advance the progress by more than 1 rather than calling it multiple times

```cs
public override void UnsubscribeFromProgressEvents(GameplayTask runtimeTask)
  {
    if (runtimeTask.GetProgressHandler() is not Action<FrogContractBehavior> value)
      return;

    Events.FrogCountedEvent -= value;
    runtimeTask.SetProgressHandler(null);
  }
```

Finally, when `UnsubscribeFromProgressEvents(GameplayTask runtimeTask)` is called because the runtimeTask's tracking value is greater than or equal to the value returned by `CalculateTargetProgress()`, we cast the generic stored delegate back into the exact type we passed in above and then remove all subscriptions and the runtimeTask is disposed of.
