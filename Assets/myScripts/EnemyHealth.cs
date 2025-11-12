using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public Animator animator;
    public float blink;
    public float immuned;
    public float immunedTime;
    public SpriteRenderer enemySr;
    public bool isDead = false;
    public EnemyFollow enemyFollow;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (immunedTime > 0)
        {
            immunedTime -= Time.deltaTime;
        }
    }

    public void TakeDamage(int amount, bool isRight)
    {
        if (currentHealth <= 0)
        {
            return;
        }

        // reduce health
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Invoke("DeleteEnemy",1f);
            animator.SetTrigger("Dead");
        }
        else if (!isDead)
        {
            // trigger blinking only if not dead
            immunedTime = immuned;
            immuned = 0.5f;
            enemySr.enabled = false;
            animator.SetTrigger("Damage");
            animator.SetBool("TakingDamage", true);
            StartCoroutine(BlinkWhileImmune());
            enemyFollow.SetTakingDamage(true);
            enemyFollow.Knockback(isRight);
        }
    }

    IEnumerator BlinkWhileImmune()
    {
        while (immunedTime > 0)
        {
            enemySr.enabled = !enemySr.enabled;
            yield return new WaitForSeconds(blink); // Wait for blink duration before toggling
        }

        // Make sure the player is visible when immunity ends
        enemySr.enabled = true;
        enemyFollow.SetTakingDamage(false);
        animator.SetBool("TakingDamage", false);
    }


    private void DeleteEnemy()
    {
        gameObject.SetActive(false);
    }
    public void Alive() 
    {
        currentHealth = 3;
    }
}
