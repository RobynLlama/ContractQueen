using ContractQueen.Persist;
using UnityEngine;
using YAPYAP;

namespace ContractQueen.Behaviors;

public class FrogContractBehavior : MonoBehaviour
{
  public NetworkPuppetProp Owner { get; protected set; }
  public FrogDataDTO Data;
  public bool HasBeenCounted => Data.HasBeenCounted;

  private void Awake()
  {
    Owner = gameObject.GetComponent<NetworkPuppetProp>();

    if (Owner == null)
    {
      ContractQueenPlugin.Log.LogError("Unable to establish owner in FrogContractBehavior");
      return;
    }
  }

  public void Count() =>
    Data.HasBeenCounted = true;
}
