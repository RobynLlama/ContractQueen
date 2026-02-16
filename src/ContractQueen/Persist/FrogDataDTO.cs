using System;
using UnityEngine;

namespace ContractQueen.Persist;

/// <summary>
/// A DTO for storing data about an individual frog
/// </summary>
[Serializable]
public class FrogDataDTO()
{
  /// <summary>
  /// The MUD Token needed to recreate this frog's MUD
  /// </summary>
  [SerializeField]
  public string MUDToken = string.Empty;

  /// <summary>
  /// Has this frog been counted by the quest system before?
  /// </summary>
  [SerializeField]
  public bool HasBeenCounted = false;
}
