using System;
using System.Collections.Generic;

namespace ContractQueen.ContractManager;

public class ContractBundleComparer : IComparer<ContractBundle>
{

  public static readonly IComparer<ContractBundle> NameOrdinal = new ContractBundleComparer();

  public int Compare(ContractBundle x, ContractBundle y)
  {
    if (ReferenceEquals(x, y)) return 0;
    if (x == null) return -1;
    if (y == null) return 1;

    return StringComparer.Ordinal.Compare(x.Name, y.Name);
  }
}
