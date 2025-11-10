using UnityEngine;



public class GoodHiding : MonoBehaviour
{
    bool playerIn = false;
    public bool overrideIn = false;
    public Animator animator;
    public bool IsBox;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerIn || overrideIn)
        {
            if (Input.GetKeyDown(KeyCode.S) && !overrideIn && IsBox) //IsHiding
            {
                Movement.Instance.ToggleHidding(true);
                overrideIn = true;
                animator.SetTrigger("Hide");
            }
            else if (Input.GetKeyDown(KeyCode.W) && overrideIn && IsBox) //UnHiding
            {
                overrideIn = false;
                animator.SetTrigger("UnHide");
                Invoke("UnHide", 0.5f);

            }

            if (Input.GetKeyDown(KeyCode.S) && !overrideIn && !IsBox) //IsHiding
            {
                Movement.Instance.ToggleHidding(true);
                overrideIn = true;
                animator.SetTrigger("HideInShadow");
            }
            else if (Input.GetKeyDown(KeyCode.W) && overrideIn && !IsBox) //UnHiding
            {
                overrideIn = false;
                animator.SetTrigger("ExitHideInShadow");
                Invoke("ExitHideInShadow", 0.5f);

            }
        }
    }

    private void UnHide()
    {
        Movement.Instance.ToggleHidding(false);
    }

    void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIn = false;
        }
    }
}