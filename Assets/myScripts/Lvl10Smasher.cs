using UnityEngine;

public class Lvl10Smasher : MonoBehaviour
{
    private PlayerHealth playerHealth;
    public static PauseMenu Instance;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            // if collision object is not hidding

            if (playerHealth != null && Movement.Instance != null && !Movement.Instance.IsHidding())
            {
                PauseMenu.Instance.Restart();
            }
        }
    }
}
