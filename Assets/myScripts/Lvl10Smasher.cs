using UnityEngine;

public class Lvl10Smasher : MonoBehaviour
{
    public static PauseMenu Instance;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PauseMenu.Instance.Restart();
        }
    }
}
