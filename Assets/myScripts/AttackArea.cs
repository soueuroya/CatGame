using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<EnemyHealth>() != null)
        {
            EnemyHealth currentHealth = collider.GetComponent<EnemyHealth>();
            bool isRight = transform.position.x > collider.transform.position.x;
            currentHealth.TakeDamage(damage, isRight);
        }
    }
}
