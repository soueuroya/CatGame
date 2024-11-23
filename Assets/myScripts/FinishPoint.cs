using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    bool isColliding = false;
    bool isActive = true;
    PlayerInventory inventory;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isActive && isColliding)
        {
            print("Key Down");
            SaveInventory();
            UnlockNewLevel();
            isActive = false;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            print("Key Up");
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isColliding = true;
            inventory = collision.GetComponent<PlayerInventory>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isColliding = false;
        }
    }

    void UnlockNewLevel()
    {

        if (SceneManager.GetActiveScene().buildIndex + 1 >= SceneManager.sceneCountInBuildSettings)
        {
            SaveManager.Instance.ResetData();
            SceneManager.LoadScene(0);
        }
        else if (SceneManager.GetActiveScene().buildIndex <= PlayerPrefs.GetInt("ReachedIndex") + 1)
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel") + 1);

            PlayerPrefs.Save();
            SceneController.Instance.NextLevel();
        }

    }

    private void SaveInventory()
    {
        SaveManager.Instance?.SaveCoinForLevel(inventory.CurrentCoin, SceneManager.GetActiveScene().buildIndex);
    }
}
