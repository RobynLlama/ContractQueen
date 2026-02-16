using System;
using System.Collections.Generic;
using ContractQueen.Behaviors;
using FrogDataLib.DataManagement;
using UnityEngine;
using YAPYAP.Npc.Frog;

namespace ContractQueen.Persist;

[Serializable]
public class FrogDatabaseDTO : FrogDataModel
{
  [SerializeField]
  internal Dictionary<AssetMUD, FrogDataDTO> AssetLookupTable = [];

  [SerializeField]
  public List<FrogDataDTO> FrogData;

  public FrogDatabaseDTO()
  {
    FrogData = [];
  }

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
    ContractQueenPlugin.Log.LogDebug($"Created a DTO for frog: {mud.GetDigest()}");
    return dto;
  }

  public override void OnAfterSerialize()
  {
    ContractQueenPlugin.Log.LogDebug($"Received serialization callback, {FrogData.Count} items in list");
    AssetLookupTable.Clear();

    //Map assets to their MUD token for use in the load callback
    foreach (var item in FrogData)
      AssetLookupTable[new(item.MUDToken)] = item;
  }

  public override void OnBeforeSerialize()
  {
    //this should clean up any unused references and refresh all
    //frog mud tokens

    ContractQueenPlugin.Log.LogDebug("Saving all FrogContractBehaviors");
    FrogData.Clear();

    var behaviors = GameObject.FindObjectsByType<FrogContractBehavior>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
    foreach (var item in behaviors)
    {
      item.Data.MUDToken = new AssetMUD(item.GetComponent<FrogStateMachine>()).Identifier;
      FrogData.Add(item.Data);
    }

  }
}
