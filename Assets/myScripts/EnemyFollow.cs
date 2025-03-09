using System.Collections;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rbenemy;
    public int holdTime = 1;
    public float Maxright;
    public float Maxleft;
    public float minimumDistance;
    public float maximumDistance;
    Vector2 initialPosition;
    private bool isFacingRight = false;
    bool isChasing = false;

    // Start is called before the first frame update

    private EnemyAttack enemyAttack;
    public Animator animator;
    private float attackRate = 1.0f;
    private bool canAttack = false;
    private bool canMove = false;

    void Start()
    {
        initialPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isChasing) // if enemy is chasing
        {
            animator.SetBool("Chasing", true);
            if (canAttack)
            {
                canAttack = false;
                Invoke("Attack", attackRate);
            }

            if (canMove)
            {

                if (Movement.Instance.transform.position.x > transform.position.x)
                {
                    rbenemy.velocity = Vector2.right * speed;
                }
                else
                {
                    rbenemy.velocity = Vector2.right * -speed;
                }
            }
            else
            {
                rbenemy.velocity = Vector2.zero;
            }
        }
        else // if enemy is not chasing
        {
            animator.SetBool("Chasing", false);

            if (!canAttack)
            {
                canAttack=true;
                CancelInvoke("Attack");
            }

            if (canMove)
            {

                if (isFacingRight)
                {
                    rbenemy.velocity = Vector2.right * speed;
                }
                else
                {
                    rbenemy.velocity = Vector2.right * -speed;
                }
            }
            else
            {
                rbenemy.velocity = Vector2.zero;
            }

            if (transform.position.x > initialPosition.x + Maxright && isFacingRight)
            {
                StartCoroutine(StayInPlace());
                Flip();
            }
            else if (transform.position.x < initialPosition.x - Maxleft && !isFacingRight)
            {
                StartCoroutine(StayInPlace());
                Flip();
            }
        }

        if (Vector2.Distance(transform.position, Movement.Instance.transform.position) < minimumDistance && !Movement.Instance.IsHidding()) // if player gets too close
        {
            if ((isFacingRight && Movement.Instance.transform.position.x > transform.position.x) || (!isFacingRight && Movement.Instance.transform.position.x < transform.position.x))
            {
                isChasing = true;
            }
            else if (!isChasing)
            {
                isChasing = false;
            }
            else
            {
                Flip();
            }
        }
        else if (Vector2.Distance(transform.position, Movement.Instance.transform.position) > maximumDistance || Movement.Instance.IsHidding()) // if player gets too far
        {
            isChasing = false; //Is this the right way to make the enemy go back to its route?
        }

    }

    public void StartWalk()
    {
        canMove = true;
    }

    public void StopWalk()
    {
        canMove = false;
    }
    
    private void Attack()
    {
        animator.SetTrigger("Attack");
        Invoke("AllowAttack", 1);
    }

    private void AllowAttack()
    {
        canAttack = true;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    IEnumerator StayInPlace()
    {
        yield return new WaitForSeconds(holdTime); 
    }
}
