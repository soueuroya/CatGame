using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultipleDeaths : MonoBehaviour
{
    public PlayerHealth currentHealth;

    public Image imageDisplay;
    public Sprite[] sprites;
    int health;

    void Start()
    {
        currentHealth = currentHealth.GetComponent<PlayerHealth>();
    }

    public void DeathScreen()
    {
        //PlayerHealth.currentHealth;
        //if (currentHealth <= 0)
        {
            RandomNumber();
        }
    }

    public void RandomNumber()
    {
        int randomNumber = Random.Range(1, 5);
        Debug.Log("Random number between 1 and 5: " + randomNumber);
        imageDisplay.sprite = sprites[randomNumber];
        
    }
}
