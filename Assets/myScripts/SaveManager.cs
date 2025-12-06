using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
            if (PlayerPrefs.HasKey("level" + i + "coins"))
            {
                totalCoinsForLevel += PlayerPrefs.GetInt("level" + i + "coins");
            }
        }

        return totalCoinsForLevel;
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }

    public void SaveGoodEnding()
    {
        PlayerPrefs.SetInt("GoodEnding", 1);
        PlayerPrefs.SetInt("Background", 0);
    }
}
