using ContractQueen.Persist;
using UnityEngine;
using YAPYAP;

namespace ContractQueen.Behaviors;

/// <summary>
/// A monobehavior attached to frogs for counting them when
/// they are rescued
/// </summary>
public class FrogContractBehavior : MonoBehaviour
{

  /// <summary>
  /// Which prop owns this contract behavior
  /// </summary>
  public NetworkPuppetProp Owner { get; protected set; }

  /// <summary>
  /// The DTO for saving this frog's state
  /// </summary>
  public FrogDataDTO Data;

  /// <summary>
  /// If this frog has been counted
  /// </summary>
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

  /// <summary>
  /// Runs after the DTO is set
  /// </summary>
  public void PostAwake()
  {
    Owner.interactStr = $"{Data.Name} ({Data.Persona})";
    Owner._interactKey = string.Empty;
  }

  /// <summary>
  /// Set counted so this frog won't be counted again
  /// </summary>
  public void Count() =>
    Data.HasBeenCounted = true;
}
