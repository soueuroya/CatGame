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
    public bool isFacingRight = false;
    bool isChasing = false;
    bool takingDamage = false;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    // Start is called before the first frame update

    public Animator animator;
    private float attackRate = 1.0f;
    private bool canAttack = false;
    private bool canMove = false;
    private bool isFlipping = false;
    private bool isDead = false;

    void Start()
    {
        initialPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead || takingDamage || !IsGrounded())
        {
            return;
        }

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
                    if (transform.position.x < initialPosition.x + Maxright && isFacingRight && !isFlipping)
                    {
                        rbenemy.velocity = new Vector2(speed, rbenemy.velocity.y);
                    }
                }
                else
                {
                    if (transform.position.x > initialPosition.x - Maxleft && !isFacingRight && !isFlipping)
                    {
                        rbenemy.velocity = new Vector2(-speed, rbenemy.velocity.y);
                    }
                }
            }
            else
            {
                rbenemy.velocity = new Vector2(0, rbenemy.velocity.y);
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
                    rbenemy.velocity = new Vector2(speed, rbenemy.velocity.y);
                }
                else
                {
                    rbenemy.velocity = new Vector2(-speed, rbenemy.velocity.y);
                }
            }
            else
            {
                rbenemy.velocity = new Vector2(0, rbenemy.velocity.y);
            }

            if (transform.position.x > initialPosition.x + Maxright && isFacingRight && !isFlipping)
            {
                Invoke("Flip", holdTime);
                rbenemy.velocity = Vector2.zero;
                animator.SetBool("Moving", false);
                isFlipping = true;
                canMove = false;
            }
            else if (transform.position.x < initialPosition.x - Maxleft && !isFacingRight && !isFlipping)
            {
                Invoke("Flip", holdTime);
                rbenemy.velocity = Vector2.zero;
                animator.SetBool("Moving", false);
                isFlipping = true;
                canMove = false;
            }
        }

        if (Vector2.Distance(transform.position, Movement.Instance.transform.position) < minimumDistance && !Movement.Instance.IsHiding()) // if player gets too close
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
        else if (Vector2.Distance(transform.position, Movement.Instance.transform.position) > maximumDistance || Movement.Instance.IsHiding()) // if player gets too far
        {
            isChasing = false; //Is this the right way to make the enemy go back to its route?
        }

    }

    public void SetIsDead(bool _isDead)
    {
        isDead = _isDead;
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
        Invoke("AllowAttack", 1);

        if (takingDamage)
        {
            return;
        }

        animator.SetTrigger("Attack");
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
        animator.SetBool("Moving", true);
        canMove = true;
        isFlipping = false;
    }

    public void SetTakingDamage(bool _takingDamage)
    {
        takingDamage = _takingDamage;
    }

    public void Knockback(bool isRight)
    {
        if (isRight)
        {
            rbenemy.AddForce(new Vector2(-60f, 75f), ForceMode2D.Impulse);
        }
        else
        {
            rbenemy.AddForce(new Vector2(60f, 75f), ForceMode2D.Impulse);
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}