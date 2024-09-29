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
                playerMovement = collision.gameObject.GetComponent<Movement>();
                playerMovement.enabled = false;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.S))
            {
                playerMovement = collision.gameObject.GetComponent<Movement>();
                playerMovement.enabled = true;
            }
        }
    }
}
