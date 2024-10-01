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
                inventory.CurrentCoin = inventory.CurrentCoin + coinValue;
                inventory.TotalCoin = inventory.TotalCoin + coinValue;
                print("Player inventory has " + inventory.TotalCoin + " total coin(s) and " + inventory.CurrentCoin + " current coin(s)");
                Invoke("DeleteCoin",2);
            }
        }
    }
    
    private void DeleteCoin()
    {
        gameObject.SetActive(false);
    }
}
