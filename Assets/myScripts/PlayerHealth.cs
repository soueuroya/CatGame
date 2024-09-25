using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public float blink;
    public float immuned;
    public SpriteRenderer modelRenderer1;
    public float immunedTime;
    private bool respawning;
    private Vector3 respawnLocation;
    public bool isDead = false;
    public int currentHealth;
    

    public Sprite emptyHeart;
    public Sprite fullHeart;
    public Image[] hearts;

    public SpriteRenderer playerSr;
    public Movement playerMovement;
    IInventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<IInventory>();
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
            playerSr.enabled = false;
            playerMovement.enabled = false;
            Respawn();
        }
        else
        {
            // trigger blinking only if not dead
            immunedTime = immuned;
            immuned = 1;
            modelRenderer1.enabled = false;
            StartCoroutine(BlinkWhileImmune());
        }

        inventory.Heart--; // lose a heart on the inventory
        UpdateHealthUI();
    }

    public void Heal(int amount)
    {
        inventory.Heart += amount;
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
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    IEnumerator BlinkWhileImmune()
    {
        while (immunedTime > 0)
        {
            modelRenderer1.enabled = !modelRenderer1.enabled;
            yield return new WaitForSeconds(blink); // Wait for blink duration before toggling
        }

        // Make sure the player is visible when immunity ends
        modelRenderer1.enabled = true;
    }

    public void Alive() 
    {
        playerSr.enabled = true;
        playerMovement.enabled = true;
        modelRenderer1.enabled = true;
        inventory.Heart = 3; // reset back to 3
        isDead = false;
        currentHealth = 3;
        UpdateHealthUI();
    }


    public void Respawn()
    {
        isDead = true;
        transform.position = respawnLocation;

        Invoke("Alive", 2); // reviving player after 2 seconds
    }

    public void CheckPoint(Vector3 newLocation)
    {
        respawnLocation = newLocation;
    }

    public void CurrentSpawnPoint(Vector3 newSpawn)
    {
        respawnLocation = newSpawn;
    }

    public void SelectedContinue()
    {
        Respawn();
    }

}
