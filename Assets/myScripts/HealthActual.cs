using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Please fix any names and let me know the scripts I need to create or add to for this to work

public class HealthActual : MonoBehaviour 
{

    public int maxHealth;
    public int currentHealth;
    public float blink;
    public float immuned;
    public float Player;
    //public Renderer modelRenderer1; Wait for the art?
    //public Renderer modelRenderer2;
    private float blinkTime = 0.1f;
    private float immunedTime;

    public Transform respawnTarget;
    private bool respawning;
    private Vector3 respawnLocation;

    public bool isDead = false;

    
    void Start() 
    {
        currentHealth = maxHealth;

        respawnLocation = respawnTarget.transform.position;
    }

    
    void Update() 
    {
        if (immundedTime > 0)
        {
    

            immundedTime -= Time.deltaTime;
            blinkTime -= Time.deltaTime;

            if (blinkTime <= 0)
            {
                modelRenderer1.enabled = !modelRenderer1.enabled;
                modelRenderer2.enabled = !modelRenderer2.enabled;

                blinkTime = blink;
            }
            if (immundedTime <= 0)
            {
                modelRenderer1.enabled = true;
                modelRenderer2.enabled = true;   
            }
        }
    }    

    public void DamagePlayer(int Hurt, Vector3 direction)
    {
        if (immundedTime <= 0)
        {
            currentHealth -= Hurt;

            if (currentHealth <= 0)
            {
                //Respawn(); Need a script for this?
            }
            else
            {

                immundedTime = immuned;
                modelRenderer1.enabled = false;
                modelRenderer2.enabled = false;

                blinkTime = blink;
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
            respawnTarget.gameObject.SetActive(false);
            //FindObjectOfType<SoundEffects>().DeathSound(); Do they need new scripts, also are the namings correct?
            //FindObjectOfType<GameManager>().CameraAfterDeath();
        }
    }

    public void Respawn()// New scripts?
    {
        isDead = true;
        FindObjectOfType<GameManager>().EndGame();
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
        isDead = false;
        respawnTarget.transform.position = respawnLocation;
        currentHealth = maxHealth;
 //     Alive();   ??
    }


}

