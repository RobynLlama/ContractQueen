using System.Linq;
using BepInEx;
using BepInEx.Logging;
using ContractQueen.ContractManager;
using ContractQueen.Contracts;
using ContractQueen.Patches;
using HarmonyLib;
using YapLocalizer;

namespace ContractQueen;

[BepInAutoPlugin]
[BepInDependency("com.github.darmuh.yaplocalizer")]
[BepInDependency(FrogDataLib.FrogDataPlugin.Id)]
public partial class ContractQueenPlugin : BaseUnityPlugin
{
  internal static ManualLogSource Log { get; private set; }

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
    patcher.PatchAll(typeof(NetworkPuppetPropPatches));
    patcher.PatchAll(typeof(FrogStateMachinePatches));

    Log.LogInfo($"Patch count: {patcher.GetPatchedMethods().Count()}");

    ContractsModule contracts = new(Id);
    contracts.RegisterContract("RescueFrogsQuest", RescueFrogsTask.Create());

#if DEBUG
    //Adds debug contracts for debugging
    contracts.RegisterContract("RescueFrogsQuest2", RescueFrogsTask.Create());
    contracts.RegisterContract("RescueFrogsQuest3", RescueFrogsTask.Create());
    contracts.RegisterContract("RescueFrogsQuest4", RescueFrogsTask.Create());
    contracts.RegisterContract("RescueFrogsQuest5", RescueFrogsTask.Create());
    contracts.RegisterContract("RescueFrogsQuest6", RescueFrogsTask.Create());
#endif

  }
}
