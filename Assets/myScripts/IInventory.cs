using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory
{
    int Key { get; set; }
    int CurrentCoin { get; set; }
    int TotalCoin { get; set; }
}
