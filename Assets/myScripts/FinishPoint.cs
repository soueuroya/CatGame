using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            print("Key Down");
            SaveInventory(collision);
            UnlockNewLevel();
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            print("Key Up");
            return;
        }
        //Was trying to get the E key and player colliding to allow them to go to the next level
        //However it is requiring multiple presses sometimes. Without the GetKeyUp it send the player multiple levels forward since its a constant update.
    }
    void UnlockNewLevel()
    {
        int temp = PlayerPrefs.GetInt("ReachedIndex");
        int temp2 = PlayerPrefs.GetInt("UnlockedLevel");

        if (SceneManager.GetActiveScene().buildIndex + 1 >= SceneManager.sceneCountInBuildSettings)
        {
            SaveManager.Instance.ResetData();
            SceneManager.LoadScene(0);
        }
        else if (SceneManager.GetActiveScene().buildIndex <= PlayerPrefs.GetInt("ReachedIndex") + 1)
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
