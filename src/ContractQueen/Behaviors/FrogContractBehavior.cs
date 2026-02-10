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

    if (gameObject.name == "NpcFrog (1)" || gameObject.name == "NpcFrog (2)")
      Count();
  }

  public void Count()
  {
    HasBeenCounted = true;
  }
}
