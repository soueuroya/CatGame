using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodHiding : MonoBehaviour
{

    private Movement playerMovement;

    void OnTriggerStay2D (Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
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
        playerMovement.enabled = false;
    }

    private void NotHiding()
    {
        playerMovement.enabled = true;
    }
}
