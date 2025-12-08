using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Hide", 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            CancelInvoke("Hide");
            Hide();
        }

    }

    private void Hide()
    {
        LeanTween.value(canvasGroup.alpha, 0f, 1f).setOnComplete(() => { gameObject.SetActive(false); }).setOnUpdate((float value) => { canvasGroup.alpha = value; });
    }



}
