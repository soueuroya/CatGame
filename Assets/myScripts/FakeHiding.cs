using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeHiding : MonoBehaviour
{
    public SpriteRenderer playerSr;
    public Movement playerMovement;
    public PlayerHealth playerHealth;
    public int damage = 3;
    public int holdTime = 2;


    
    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D (Collider2D collision)
    {
        if(collision.gameObject.tag == "FakeHide")
        {
            if (Input.GetKey(KeyCode.W))
            {
                Sike();
            }
        }
    }

    private void Sike()
    {
        playerSr.enabled = false;
        playerMovement.enabled = false;
        StartCoroutine(HoldForDamage());
    }

    IEnumerator HoldForDamage()
    {
        yield return new WaitForSeconds(holdTime); 
        playerHealth.TakeDamage(damage);
    }

}