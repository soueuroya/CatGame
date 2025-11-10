using UnityEngine;

public class FakeHiding : MonoBehaviour
{
    private PlayerHealth playerHealth;
    public int damage = 3;
    private float holdTimeBox = 1.5f;
    private float holdTimeShadow = 3.5f;
    bool playerIn;
    public Animator animator;
    public bool IsShadow;
    public float idleRate = 35f;


    private void Start()
    {
        if (IsShadow)
        {
            InvokeRepeating("Idle", 1, idleRate);
        }
        
    }

    private void Update()
    {
        if (playerIn && playerHealth != null)
        {
            if (Input.GetKeyDown(KeyCode.S) && IsShadow)
            {
                Movement.Instance.ToggleHidding(true); // locks the player right away
                Invoke("CauseDeath", holdTimeShadow);
                animator.SetTrigger("IntoDeathShadow");
            }
            if (Input.GetKeyDown(KeyCode.S) && !IsShadow)
            {
                Movement.Instance.ToggleHidding(true); // locks the player right away
                Invoke("CauseDeath", holdTimeBox);
                animator.SetTrigger("IntoDeathBox");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIn = true;
            playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIn = false;
        }
    }

    private void CauseDeath()
    {
        playerIn = false;
        //Movement.Instance.ToggleHidding(false);
        playerHealth.InstantDie();
    }


    private void Idle()
    {
        animator.SetTrigger("IsIdle");
    }

}