using UnityEngine;
using UnityEngine.SceneManagement;

public class InstaKillSmash : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;

        if (collision.gameObject.tag == "Player")
        {
            //SceneManager.LoadScene(buildIndex);
            collision.gameObject.GetComponent<PlayerHealth>().InstantDie();

            var movingObstacleSaw = gameObject.GetComponent<MovingObstacleSaw>();
            if (movingObstacleSaw != null)
            {
                movingObstacleSaw.StopMoving();
            }

            var movingObstacleSlab = gameObject.GetComponent<MovingObstacleSlab>();
            if (movingObstacleSlab != null)
            {
                movingObstacleSlab.StopMoving();
            }

            var movingObstacleSpike = gameObject.GetComponent<MovingObstacleSpike>();
            if (movingObstacleSpike != null)
            {
                movingObstacleSpike.StopMoving();
            }
        }
    }
}
