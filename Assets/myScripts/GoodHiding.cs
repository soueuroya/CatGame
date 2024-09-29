using UnityEngine;

public class GoodHiding : MonoBehaviour
{
    bool playerIn = false;
    public bool overrideIn = false;
    private void Update()
    {
        if (playerIn || overrideIn)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Movement.Instance.ToggleHidding(true);
                overrideIn = true;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                overrideIn = false;
                Movement.Instance.ToggleHidding(false);
            }
        }
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