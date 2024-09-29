using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour, IInventory
{
  public int Key { get => _Key; set => _Key = value; }

  private int _Key = 0;

  public int CurrentCoin { get => _CurrentCoin; set => _CurrentCoin = value; }

  private int _CurrentCoin = 0;

  public int TotalCoin { get => _TotalCoin; set => _TotalCoin = value; }

  private int _TotalCoin = 0;

    private void Start()
    {
        _TotalCoin = SaveManager.Instance.LoadCoinForLevel(SceneManager.GetActiveScene().buildIndex);
    }
}


