using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GoodHiding goodHide;
    public FakeHiding badHide;
    public int damage = 1;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(damage);
        }
    }

    private void IfHiding()
    {
        if (goodHide.enabled == true)
        {
            damage = 0;
        }
        else if (badHide.enabled == true)
        {
            damage = 0;
        }
    }
}
