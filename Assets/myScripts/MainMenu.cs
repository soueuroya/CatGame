using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject goodBackground;

    public void Awake()
    {
        if (PlayerPrefs.HasKey("GoodEnding"))
        {
            if (PlayerPrefs.GetInt("GoodEnding") == 1)
            {
                goodBackground.SetActive(true);
            }
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
