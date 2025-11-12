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
        if ((playerIn || overrideIn) && Movement.Instance.CanHide())
        {
            if (Input.GetKeyDown(KeyCode.S) && !overrideIn && IsBox) //IsHiding
            {
                Movement.Instance.ToggleHiding(true);
                Movement.Instance.HideSprite();
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
                Movement.Instance.ToggleHiding(true);
                overrideIn = true;
                Movement.Instance.AnimateShadow();

                LeanTween.moveX(Movement.Instance.gameObject, transform.position.x, 0.5f).setOnComplete(value => {
                Movement.Instance.gameObject.transform.position = new Vector3(transform.position.x, Movement.Instance.gameObject.transform.position.y, Movement.Instance.gameObject.transform.position.z);
                });
            }
            else if (Input.GetKeyDown(KeyCode.W) && overrideIn && !IsBox && Movement.Instance.IsHiding()) //UnHiding
            {
                overrideIn = false;
                Movement.Instance.ShowSprite();
                Movement.Instance.AnimateExitShadow();
            }
        }
    }

    private void UnHide()
    {
        Movement.Instance.ShowSprite();
        Movement.Instance.ToggleHiding(false);
    }

    void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerIn = true;
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
}