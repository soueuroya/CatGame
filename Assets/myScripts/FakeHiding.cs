using UnityEngine;

public class FakeHiding : MonoBehaviour
{
    private PlayerHealth playerHealth;
    public int damage = 3;
    public int holdTime = 2;
    bool playerIn;

    private void Update()
    {
        if (playerIn && playerHealth != null)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Movement.Instance.ToggleHidding(true); // locks the player right away
                Invoke("CauseDamage", holdTime);
                // play the animation.duration
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

    private void CauseDamage()
    {
        playerIn = false;
        Movement.Instance.ToggleHidding(false);
        playerHealth.TakeDamage(damage);
    }
}