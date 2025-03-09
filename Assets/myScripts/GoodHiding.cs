using UnityEngine;



public class GoodHiding : MonoBehaviour
{
    bool playerIn = false;
    public bool overrideIn = false;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerIn || overrideIn)
        {
            if (Input.GetKeyDown(KeyCode.S) && !overrideIn) //IsHiding
            {
                Movement.Instance.ToggleHidding(true);
                overrideIn = true;
                animator.SetTrigger("Hide");
            }
            else if (Input.GetKeyDown(KeyCode.W) && overrideIn) //UnHiding
            {
                overrideIn = false;
                animator.SetTrigger("UnHide");
                Invoke("UnHide", 0.5f);

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