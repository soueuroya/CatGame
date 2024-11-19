using UnityEngine;
using UnityEngine.SceneManagement;

public class InstaKillSmash : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;

        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(buildIndex);
        }
    }
}
