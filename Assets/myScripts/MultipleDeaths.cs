using UnityEngine;
using UnityEngine.UI;

public class MultipleDeaths : MonoBehaviour
{

    public Image imageDisplay;
    public Sprite[] sprites;

    public GameObject gameObject;

    public static MultipleDeaths Instance;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RandomNumber()
    {
        gameObject.SetActive(true);
        int randomNumber = Random.Range(1, 5);
        Debug.Log("Random number between 1 and 5: " + randomNumber);
        imageDisplay.sprite = sprites[randomNumber];
        
    }
}
