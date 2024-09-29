using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public float blink;
    public float immuned;
    public float immunedTime;
    private bool respawning;
    private Vector3 respawnLocation;
    public bool isDead = false;
    public int currentHealth;
    
    public SpriteRenderer playerSr;
    public Movement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        respawnLocation = transform.position;
        UpdateHealthUI();
    }

    public void TakeDamage(int amount)
    {
        if (immunedTime > 0) // prevent damage if blinking
        {
            return;
        }

        // reduce health
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            playerMovement.enabled = false;
            isDead = true;
            Respawn();
        }
        else
        {
            // trigger blinking only if not dead
            immunedTime = immuned;
            immuned = 1;
            playerSr.enabled = false;
            StartCoroutine(BlinkWhileImmune());
        }
        UpdateHealthUI();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (immunedTime > 0)
        {
            immunedTime -= Time.deltaTime;
        }

        
    }

    void UpdateHealthUI()
    {
        UIManager.Instance?.UpdateHearts(currentHealth);
    }

    IEnumerator BlinkWhileImmune()
    {
        while (immunedTime > 0)
        {
            playerSr.enabled = !playerSr.enabled;
            yield return new WaitForSeconds(blink); // Wait for blink duration before toggling
        }

        // Make sure the player is visible when immunity ends
        playerSr.enabled = true;
    }

    public void UnlockMovement() 
    {
        playerMovement.enabled = true;
        isDead = false;
        currentHealth = 3;
        UpdateHealthUI();
    }

    public void Respawn()
    {
        transform.position = respawnLocation;

        RespawnAnimation();
        
        Invoke("UnlockMovement", 2); // reviving player after 2 seconds
    }

    private void RespawnAnimation()
    {
        playerSr.enabled = true;
        immunedTime = 0;
        // play waking up animation
    }

    public void CheckPoint(Vector3 newLocation)
    {
        respawnLocation = newLocation;
    }

    public void CurrentSpawnPoint(Vector3 newSpawn)
    {
        respawnLocation = newSpawn;
    }

    //public void SelectedContinue() Not currently relevant
    //{
    //    Respawn();
    //}

}
