using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private PlayerHealth playerHealth;
    public int damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            // if collision object is not hidding

            if (playerHealth != null && Movement.Instance != null && !Movement.Instance.IsHidding())
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
