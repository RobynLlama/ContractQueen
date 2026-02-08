using System;
using UnityEngine.SceneManagement;
using YAPYAP;

namespace ContractQueen.Events;

public static partial class EventManager
{
  internal static void OnSceneChange(Scene arg0, LoadSceneMode arg1)
  {
    ContractQueenPlugin.Log.LogMessage("Clearing seen item IDs");
    //clear seen items
    SeenItemIDs.Clear();
  }
}
