using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    public int heartValue = 1;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            IInventory inventory = other.GetComponent<IInventory>();
            if(inventory != null)
            {
                inventory.Heart = inventory.Heart + heartValue;
                print("Player has " + inventory.Heart + " heart(s)");
                Invoke("DeleteHeart",2);
            }

            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(heartValue);
            }

        }
    }
    private void DeleteHeart()
    {
        gameObject.SetActive(false);
    }
}
