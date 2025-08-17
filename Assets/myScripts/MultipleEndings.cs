using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
            print("Key Down");
            CheckInventory();
            EndingScene();
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

    private void CheckInventory()
    {
        if (inventory != null)
        {
            if (inventory.CurrentCoin >= 23)
            {
                endState = 0;
                SaveManager.Instance?.SaveGoodEnding();
            }
            else if (inventory.CurrentCoin >= 8)
            {
                endState = 1;
            }
            else
            {
                endState = 2;
            }
        }
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
