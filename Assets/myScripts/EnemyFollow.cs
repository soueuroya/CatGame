using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{

    public GameObject player;
    public float speed;
    public Rigidbody2D rbenemy;
    public int holdTime = 1;
    public float Maxright;
    public float Maxleft;
    public float minimumDistance;
    public float maximumDistance;
    Vector2 initialPosition;

    private bool isFacingRight = true;

    bool isChasing = false;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isChasing) // if enemy is chasing
        {
            if (player.transform.position.x > transform.position.x)
            {
                rbenemy.velocity = Vector2.right * speed;
            }
            else
            {
                rbenemy.velocity = Vector2.right * -speed;
            }
        }
        else // if enemy is not chasing
        {
            if (isFacingRight)
            {
                rbenemy.velocity = Vector2.right * speed;
            }
            else
            {
                rbenemy.velocity = Vector2.right * -speed;
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

        if (Vector2.Distance(transform.position, player.transform.position) < minimumDistance) // if player gets too close
        {
            isChasing = true;
        }
        else if (Vector2.Distance(transform.position, player.transform.position) > maximumDistance) // if player gets too far
        {
            isChasing = false; //Is this the right way to make the enemy go back to its route?
        }

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
