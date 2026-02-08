using System;
using System.Linq;
using BepInEx;
using BepInEx.Logging;
using ContractQueen.ContractManager;
using ContractQueen.Contracts;
using ContractQueen.Patches;
using HarmonyLib;
using UnityEngine.SceneManagement;
using YapLocalizer;

namespace ContractQueen;

[BepInAutoPlugin]
public partial class ContractQueenPlugin : BaseUnityPlugin
{
  internal static ManualLogSource Log { get; private set; } = null!;

  public const string contractName = "CONTRACT_QUEEN_TEST_CONTRACT_NAME";
  public const string contractDesc = "CONTRACT_QUEEN_TEST_CONTRACT_DESC";

  private void Awake()
  {
    Log = Logger;

    Log.LogMessage("Loading localized contract data");

    var loc = new ModLocalizedText(contractName);
    loc.SetLocalization(UnityEngine.SystemLanguage.English, "Rescue Frogs");

    loc = new ModLocalizedText(contractDesc);
    loc.SetLocalization(UnityEngine.SystemLanguage.English, "Rescued {0}/{1} Frogs");

    Harmony patcher = new(Id);
    patcher.PatchAll(typeof(DungeonTasksPatches));
    patcher.PatchAll(typeof(PawnInventoryPatches));

    Log.LogInfo($"Patch count: {patcher.GetPatchedMethods().Count()}");
    SceneManager.sceneLoaded += OnSceneChange;

    //Debug contracts are not included except in debug builds
#if DEBUG
    ContractsModule contracts = new(Id);
    contracts.RegisterContract("RescueFrogsQuest1", RescueFrogsTask.Factory());
    contracts.RegisterContract("RescueFrogsQuest2", RescueFrogsTask.Factory());
    contracts.RegisterContract("RescueFrogsQuest3", RescueFrogsTask.Factory());
    contracts.RegisterContract("RescueFrogsQuest4", RescueFrogsTask.Factory());
    contracts.RegisterContract("RescueFrogsQuest5", RescueFrogsTask.Factory());
    contracts.RegisterContract("RescueFrogsQuest6", RescueFrogsTask.Factory());
#endif

  }

  private bool MainMenuSeen = false;

  private void OnSceneChange(Scene arg0, LoadSceneMode arg1)
  {
    if (MainMenuSeen)
      return;

    if (arg0.name.Equals("menu", StringComparison.OrdinalIgnoreCase))
    {
      ContractsRegistry.Lock();
      MainMenuSeen = true;
    }
  }
}
