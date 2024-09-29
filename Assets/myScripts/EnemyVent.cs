using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVent : MonoBehaviour
{
    public SpriteRenderer playerSr;
    public Movement playerMovement;
    public PlayerHealth playerHealth;
    public int damage = 3;
    public int holdTime;


    
    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D (Collider2D collision)
    {
        if(collision.gameObject.tag == "VentE")
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
    //    StartCoroutine(HoldForDamage());
        playerHealth.TakeDamage(damage);
    }

    //IEnumerator HoldForDamage()
    //{
    //    yield return new WaitForSeconds(holdTime); 
    //}

}