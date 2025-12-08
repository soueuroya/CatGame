using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private PlayerHealth playerHealth;
    public int damage = 1;
    private bool isDead = false;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        if (isDead)
        {
            return;
        }

        animator.SetTrigger("Attack");
    }

    public void SetIsDead(bool _isDead)
    {
        isDead = _isDead;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && !isDead)
        {
            playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            // if collision object is not hidding

            if (playerHealth != null && Movement.Instance != null && !Movement.Instance.IsHiding())
            {
                bool isRight = transform.position.x > collision.transform.position.x;
                playerHealth.TakeDamage(damage, isRight);
            }
        }
    }
}
