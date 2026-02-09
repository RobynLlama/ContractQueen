//These using directives need to be at the top of your cs file
using ContractQueen.ContractManager;
using MyMod.Contracts;

//This code should go in your plugin somewhere and be called in your Awake()

private void RegisterContracts()
{
  //If you're not using the template plugin then this should be
  //changed to your plugin's GUID.

  ContractsModule contracts = new(Id);

  //The name of your contract here won't be shown to users but will be
  //used for sorting. Keep them descriptive so you can remember what
  //they do later
  contracts.RegisterContract("ExampleContract", MyNewContract.CreateNew());
}
