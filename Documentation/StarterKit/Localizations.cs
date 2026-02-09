using YapLocalizer;

namespace MyMod.Locales;

internal static class Locale
{
  internal const string MyContractName = "MY_CONTRACT_KEY";
  internal const string MyContractDesc = "MY_CONTRACT_DESC";

  static Locale()
  {
    ModLocalizedText name = new(MyContractName);
    ModLocalizedText desc = new(MyContractDesc);

    name.SetLocalization(UnityEngine.SystemLanguage.English, "My Awesome Contract");
    desc.SetLocalization(UnityEngine.SystemLanguage.English, "{0} of {1} Things Done");
  }
}
