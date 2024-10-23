using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public int keyValue = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();

            if(inventory != null)
            {
                inventory.Key = inventory.Key + keyValue;
                print("Player inventory has " + inventory.Key + " key in it");
                Invoke("DeleteKey",2);
            }

        }
    }
    private void DeleteKey()
    {
        gameObject.SetActive(false);
    }
}
