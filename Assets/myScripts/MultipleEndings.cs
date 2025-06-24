using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultipleEndings : MonoBehaviour
{
    bool isColliding = false;
    bool isActive = true;
    PlayerInventory inventory;
    bool goldEnding = false;

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

    } 

    private void EndingScene()
    {

    }









}
