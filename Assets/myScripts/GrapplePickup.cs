using UnityEngine;

public class GrapplePickup : MonoBehaviour
{
    public int GrappleValue = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();

            if (inventory != null)
            {
                inventory.Grapple = inventory.Grapple + GrappleValue;
                print("Player has picked up the grapple");
                Invoke("DeleteGrapple", 2);
            }
        }
    }

    private void DeleteGrapple()
    {
        gameObject.SetActive(false);
    }
}
