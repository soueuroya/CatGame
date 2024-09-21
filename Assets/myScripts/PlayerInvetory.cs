using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IInventory
{
  public int Key { get => _Key; set => _Key = value; }

  private int _Key = 0;

  public int Heart { get => _Heart; set => _Heart = value; }

  private int _Heart = 3;

  public int Coin { get => _Coin; set => _Coin = value; }

  private int _Coin = 0;
}


