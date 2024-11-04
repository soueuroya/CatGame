using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SaveInventory(collision);
            UnlockNewLevel();
        }
    }

    void UnlockNewLevel()
    {
        int temp = PlayerPrefs.GetInt("ReachedIndex"); 

        if (SceneManager.GetActiveScene().buildIndex + 1 >= SceneManager.sceneCountInBuildSettings)
        {
            SaveManager.Instance.ResetData();
            SceneManager.LoadScene(0);
        }
        else if (SceneManager.GetActiveScene().buildIndex<=PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
            SceneController.Instance.NextLevel();
        }

    }

    private void SaveInventory(Collider2D collision)
    {
        PlayerInventory inventory = collision.GetComponent<PlayerInventory>();
        SaveManager.Instance?.SaveCoinForLevel(inventory.CurrentCoin, SceneManager.GetActiveScene().buildIndex);
    }
}
