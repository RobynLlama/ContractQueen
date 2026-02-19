using System;
using UnityEngine;

namespace ContractQueen.Persist;

/// <summary>
/// A DTO for storing data about an individual frog
/// </summary>
public class FrogDataDTO()
{
  /// <summary>
  /// The MUD Token needed to recreate this frog's MUD
  /// </summary>
  public string MUDToken = string.Empty;

  /// <summary>
  /// Has this frog been counted by the quest system before?
  /// </summary>
  public bool HasBeenCounted = false;
}
