using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeHiding : MonoBehaviour
{
    private Movement playerMovement;
    private PlayerHealth playerHealth;
    public int damage = 3;
    public int holdTime = 2;


    
    // Update is called once per frame
    void Update()
    {
        
    }



    void OnTriggerStay2D (Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.W))
            {
                playerMovement = collision.gameObject.GetComponent<Movement>();
                playerHealth = collision.gameObject.GetComponent<playerHealth>();
                Sike();
            }
        }
    }

    private void Sike()
    {
        playerMovement.enabled = false;
        StartCoroutine(HoldForDamage());
    }

    IEnumerator HoldForDamage()
    {
        yield return new WaitForSeconds(holdTime); 
        playerHealth.TakeDamage(damage);
    }

}