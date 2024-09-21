using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 3;

    public SpriteRenderer playerSr;
    public Movement playerMovement;

    [SerializeField] private HealthActual healthActual;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (healthActual.immunedTime > 0) // prevent damage if blinking
        {
            return;
        }

        healthActual.DamagePlayer(amount, Vector3.zero);
        health -= amount;
        if(health <= 0)
        {
            playerSr.enabled = false;
            playerMovement.enabled = false;
        }
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
