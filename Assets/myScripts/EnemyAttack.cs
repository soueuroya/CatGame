using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private PlayerHealth playerHealth;
    public int damage = 1;

    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

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
