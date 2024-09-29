using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    // level1 -  4 coins
    // level2 -  2 coins
    // level3 -  1 coin
    // level4 -  3 coins

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveCoinForLevel(int coins, int level)
    {
        int currentCoinsInLevel = PlayerPrefs.GetInt("level" + level + "coins");

        if (coins > currentCoinsInLevel)
        {
            PlayerPrefs.SetInt("level" + level + "coins", coins);
        }
    }

    public int LoadCoinForLevel(int level)
    {
        int totalCoinsForLevel = 0;

        for (int i = 1; i < level; i++)
        {
            totalCoinsForLevel += PlayerPrefs.GetInt("level" + i + "coins");
        }

        return totalCoinsForLevel;
    }
}
