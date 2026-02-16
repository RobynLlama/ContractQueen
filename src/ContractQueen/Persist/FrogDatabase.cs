using FrogDataLib.DataManagement;

namespace ContractQueen.Persist;

internal static class FrogDatabase
{
  internal static FrogDataContainerSimple<FrogDatabaseDTO> container = new(ContractQueenPlugin.Id);
  internal static FrogDatabaseDTO Database => container.Data;
}
