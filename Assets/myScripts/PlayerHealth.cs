using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 3;
    public float blink;
    public float immuned;
    public SpriteRenderer modelRenderer1;
    public float immunedTime;
    private bool respawning;
    private Vector3 respawnLocation;
    public bool isDead = false;

    public Sprite emptyHeart;
    public Sprite fullHeart;
    public Image[] hearts;

    public SpriteRenderer playerSr;
    public Movement playerMovement;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        respawnLocation = transform.position;
    }

    public void TakeDamage(int amount)
    {
        if (immunedTime > 0) // prevent damage if blinking
        {
            return;
        }

        DamagePlayer(amount, Vector3.zero);
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
        if (immunedTime > 0)
        immunedTime -= Time.deltaTime;

        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
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

    void DamagePlayer(int Hurt, Vector3 direction)
    {
        if (immunedTime <= 0)
        {
            health -= Hurt;

            if (health <= 0)
            {
                Respawn();// Need a script for this?
            }
            else
            {

                immunedTime = immuned;
                immuned = 1;
                modelRenderer1.enabled = false;
                StartCoroutine(BlinkWhileImmune());
            }
        }
    }

    public void Alive() 
    {

    }

    public void Dead()
    {
        if (isDead == true)
        {
            //FindObjectOfType<SoundEffects>().DeathSound(); Do they need new scripts, also are the namings correct? in here its better to have a new scrips, yes the name is ok
            //FindObjectOfType<GameManager>().CameraAfterDeath();
        }
    }

    public void Respawn()// New scripts? no need for new scripts for this one
    {
        isDead = true;
        transform.position = respawnLocation;
        //FindObjectOfType<GameManager>().EndGame();
        Dead();
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
