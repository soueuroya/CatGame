using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinpickup : MonoBehaviour
{
    public int coinValue = 1;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.tag == "Player")
        {
            IInventory inventory = other.GetComponent<IInventory>();

            if(inventory != null)
            {
                inventory.Coin = inventory.Coin + coinValue;
                print("Player inventory has " + inventory.Coin + " coin(s)");
                Invoke("DeleteCoin",2);

            
                
            }

        }
    }
    

    
    private void DeleteCoin()
    {
        gameObject.SetActive(false);
    }
}
