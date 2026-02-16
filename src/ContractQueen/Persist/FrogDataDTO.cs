using System;
using UnityEngine;

namespace ContractQueen.Persist;

[Serializable]
public class FrogDataDTO()
{
  [SerializeField]
  public string MUDToken = string.Empty;

  [SerializeField]
  public bool HasBeenCounted = false;
}
