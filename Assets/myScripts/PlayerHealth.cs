using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

//Player does not take damage when entering an enemy hiding spot due to TakeDamage no longer having a damage indicator

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

    public static PlayerHealth Instance;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
        if(currentHealth <= 0 && !isDead)
        {
            //playerMovement.enabled = false;
            isDead = true;
            Die();
        }
        else if (!isDead)
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

    public void Die()
    {
        playerMovement.SetIsDead(true);
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
