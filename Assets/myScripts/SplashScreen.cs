using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    [SerializeField] IntroMovie introMovie;

    // Start is called before the first frame update
    void Start()
    {
        //Invoke("Hide", 3);
        // if game has not started intro movie yet, play it
        
        if (SceneController.Instance.logoAnimationPlayed)
        {

        }
        else
        {
            SceneController.Instance.logoAnimationPlayed = true;
            introMovie.PlayVideo( () => { Invoke("Hide", 0.25f); });
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.anyKey)
        //{
        //    CancelInvoke("Hide");
        //    Hide();
        //}
    }

    private void Hide()
    {
        LeanTween.value(canvasGroup.alpha, 0f, 1f).setOnComplete(() => { gameObject.SetActive(false); }).setOnUpdate((float value) => { canvasGroup.alpha = value; });
    }
}