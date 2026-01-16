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
        if ((playerIn && playerHealth != null) && Movement.Instance.CanHide())
        {
            if (Input.GetKeyDown(KeyCode.E) && IsShadow)
            {
                Movement.Instance.ToggleHiding(true); // locks the player right away
                Movement.Instance.AnimateShadow();
                Invoke("CauseDeath", holdTimeShadow);
                animator.SetTrigger("IntoDeathShadow");
                LeanTween.moveX(Movement.Instance.gameObject, transform.position.x, 0.5f).setOnComplete(value => {
                    Movement.Instance.DarkenPlayer();
                    Movement.Instance.gameObject.transform.position = new Vector3(transform.position.x, Movement.Instance.gameObject.transform.position.y, Movement.Instance.gameObject.transform.position.z);
                    Invoke("HidePlayer", 0.46f);
                });
            }
            if (Input.GetKeyDown(KeyCode.E) && !IsShadow)
            {
                Movement.Instance.ToggleHiding(true); // locks the player right away
                Movement.Instance.HideSprite();
                Invoke("CauseDeath", holdTimeBox);
                animator.SetTrigger("IntoDeathBox");
            }
        }
    }

    private void HidePlayer()
    {
        Movement.Instance.HideSprite();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIn = true;
            playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            Movement.Instance.SetCanLookDown(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIn = false;
            Movement.Instance.SetCanLookDown(true);
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