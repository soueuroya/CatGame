using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Venting : MonoBehaviour
{
    public SpriteRenderer playerSr;
    public Movement playerMovement;


    void OnTriggerStay2D (Collider2D collision)
    {
        if(collision.gameObject.tag == "Vent")
        {
            if (Input.GetKey(KeyCode.W))
            {
                Hiding();
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.S))
            {
                NotHiding();
            }
        }
    }

    private void Hiding()
    {
        playerSr.enabled = false;
        playerMovement.enabled = false;
    }

    private void NotHiding()
    {
        playerSr.enabled = true;
        playerMovement.enabled = true;
    }


}
