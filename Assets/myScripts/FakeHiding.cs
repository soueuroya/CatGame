using UnityEngine;

public class FakeHiding : MonoBehaviour
{
    private PlayerHealth playerHealth;
    public int damage = 3;
    public float holdTime = 0.5f;
    bool playerIn;
    public Animator animator;
    public bool IsShadow;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerIn && playerHealth != null)
        {
            if (Input.GetKeyDown(KeyCode.W) && IsShadow)
            {
                Movement.Instance.ToggleHidding(true); // locks the player right away
                Invoke("CauseDeath", holdTime);
                animator.SetTrigger("IntoDeathShadow");
            }
            if (Input.GetKeyDown(KeyCode.W) && !IsShadow)
            {
                Movement.Instance.ToggleHidding(true); // locks the player right away
                Invoke("CauseDeath", holdTime);
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
        Movement.Instance.ToggleHidding(false);
        playerHealth.InstantDie();
    }
}