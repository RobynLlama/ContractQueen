using System.Collections;
using UnityEngine;
using YAPYAP;

namespace ContractQueen.Behaviors;

public class FrogContractBehavior : MonoBehaviour
{
  public NetworkPuppetProp Owner { get; protected set; }
  public bool HasBeenCounted { get; protected set; } = false;

  private void Awake()
  {
    Owner = gameObject.GetComponent<NetworkPuppetProp>();

    if (Owner == null)
    {
      ContractQueenPlugin.Log.LogError("Unable to establish owner in FrogContractBehavior");
      return;
    }
  }

  private IEnumerator DelayedFrogCheck()
  {
    yield return new WaitForEndOfFrame();

    ContractQueenPlugin.Log.LogDebug($"Check valid status for a frog: {gameObject.GetInstanceID()} | State | {Owner.CurrentState.state}");

    if (gameObject.name == "NpcFrog (1)" || gameObject.name == "NpcFrog (2)")
    {
      ContractQueenPlugin.Log.LogDebug($"Invalidating a unique frog: {gameObject.name}");
      Count();
      yield break;
    }

    if (GameManager.Instance.currentGameState != GameManager.GameState.Lobby)
      yield break;

    //The other two states are `BeingHeld` and `InInventory` so those are both
    //valid ways to bring a frog home
    if (Owner.CurrentState.state == PropState.Idle)
    {
      ContractQueenPlugin.Log.LogDebug($"Invalidating an IDLE frog: {Owner.GetInstanceID()}");
      Count();
    }
  }

  private void OnEnable()
  {
    if (HasBeenCounted)
      return;

    StartCoroutine(DelayedFrogCheck());
  }

  public void Count()
  {
    HasBeenCounted = true;
  }
}
