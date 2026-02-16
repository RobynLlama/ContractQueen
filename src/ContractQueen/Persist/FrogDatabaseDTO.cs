using System;
using System.Collections.Generic;
using ContractQueen.Behaviors;
using FrogDataLib.DataManagement;
using UnityEngine;
using YAPYAP.Npc.Frog;

namespace ContractQueen.Persist;

/// <summary>
/// A DTO for storing and managing the list of individual frog DTOs
/// </summary>
[Serializable]
public class FrogDatabaseDTO : FrogDataModel
{
  internal Dictionary<AssetMUD, FrogDataDTO> AssetLookupTable = [];

  /// <summary>
  /// A list of individual frog data
  /// </summary>
  [SerializeField]
  public List<FrogDataDTO> FrogData;

  /// <summary>
  /// Creates a new 
  /// </summary>
  public FrogDatabaseDTO()
  {
    FrogData = [];
  }

  /// <summary>
  /// Returns the DTO for a given MUD or creates a new one
  /// </summary>
  /// <param name="mud">The Mostly Unique ID of the DTO</param>
  /// <returns></returns>
  public FrogDataDTO GetFrogData(AssetMUD mud)
  {
    if (AssetLookupTable.TryGetValue(mud, out var data))
    {
      ContractQueenPlugin.Log.LogDebug($"Mapped a DTO for frog: {mud.GetDigest()}");
      return data;
    }


    var dto = new FrogDataDTO()
    {
      MUDToken = mud.Identifier
    };

    FrogData.Add(dto);
    ContractQueenPlugin.Log.LogDebug($"Created a new DTO for a frog");
    return dto;
  }

  /// <inheritdoc/>
  public override void OnAfterSerialize()
  {
    ContractQueenPlugin.Log.LogDebug($"Received serialization callback, {FrogData.Count} items in list");
    AssetLookupTable.Clear();

    //Map assets to their MUD token for use in the load callback
    foreach (var item in FrogData)
      AssetLookupTable[new(item.MUDToken)] = item;
  }

  /// <inheritdoc/>
  public override void OnBeforeSerialize()
  {
    //this should clean up any unused references and refresh all
    //frog mud tokens

    ContractQueenPlugin.Log.LogDebug("Saving all FrogContractBehaviors");
    FrogData.Clear();

    var behaviors = GameObject.FindObjectsByType<FrogContractBehavior>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
    foreach (var item in behaviors)
    {
      var mud = new AssetMUD(item.GetComponent<FrogStateMachine>());

      item.Data.MUDToken = mud.Identifier;
      ContractQueenPlugin.Log.LogDebug($"Saved a DTO for frog:{mud.GetDigest()}");
      FrogData.Add(item.Data);
    }

  }
}
