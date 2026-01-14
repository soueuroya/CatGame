using UnityEngine;
using UnityEngine.UI;

public class CreditsAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform cat;
    [SerializeField] private Image box;

    private void Start()
    {
        AnimateCat();
        FadeInBox();
    }

    private void AnimateCat()
    {
        LeanTween.value(cat.anchoredPosition.y, 0f, 6f).setDelay(5).setLoopPingPong().setOnUpdate((float value) => {
            cat.anchoredPosition = new Vector2(0f, value); 
        });
    }

    private void FadeInBox()
    {
        LeanTween.alpha(box.rectTransform, 1f, 5f).setDelay(10f).setLoopPingPong();
    }
}
