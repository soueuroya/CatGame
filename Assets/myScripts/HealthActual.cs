using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthActual : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public float blink;
    public float immuned;
    public float Player;
    public Renderer modelRenderer1;
    public Renderer modelRenderer2;
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
        
    }
}


//Testting