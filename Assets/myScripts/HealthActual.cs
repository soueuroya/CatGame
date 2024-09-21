using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Please fix any names and let me know the scripts I need to create or add to for this to work

public class HealthActual : MonoBehaviour 
{

    public int maxHealth;
    public int currentHealth;
    public float blink;
    public float immuned;
    public SpriteRenderer modelRenderer1;
    public float immunedTime;

    private bool respawning;
    private Vector3 respawnLocation;

    public Image[] hearts;
    public bool isDead = false;

    
    void Start() 
    {
        currentHealth = maxHealth;
        respawnLocation = transform.position;
    }
    
    private void Update()
    {
        if (immunedTime > 0)
        immunedTime -= Time.deltaTime;
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


    public void DamagePlayer(int Hurt, Vector3 direction)
    {
        if (immunedTime <= 0)
        {
            currentHealth -= Hurt;

            if (currentHealth <= 0)
            {
                Respawn();// Need a script for this?
            }
            else
            {

                immunedTime = immuned;
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

