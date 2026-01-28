using UnityEngine;
using UnityEngine.SceneManagement;

public class MultipleEndings : MonoBehaviour
{
    bool isColliding = false;
    bool isActive = true;
    PlayerInventory inventory;
    int endState = -1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isActive && isColliding)
        {
            if (CheckInventory())
            {
                EndingScene();
                isActive = false;
            }
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

    private bool CheckInventory()
    {
        if (inventory != null)
        {
            if (inventory.TotalCoin >= 24)
            {
                endState = 0;
                SaveManager.Instance?.SaveGoodEnding();
            }
            else if (inventory.TotalCoin >= 10)
            {
                endState = 1;
            }
            else
            {
                endState = 2;
            }
        }

        return inventory.Key > 0;
    } 

    private void EndingScene()
    {
        switch (endState)
        {
            case 2:
                SceneManager.LoadScene("CardboardEnding");
                break;

            case 1:
                SceneManager.LoadScene("SilverEnding");
                break;

            case 0:
                SceneManager.LoadScene("GoldEnding");
                break;

            default:
                SceneManager.LoadScene("CardboardEnding");
                break;
        }
    }
}