using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Slider))]
public class BrightnessSlider : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI label;
    [SerializeField] Image foregroundFade;
    private const string BrightnessPrefKey = "BrightnessLevel";

    private void OnValidate()
    {
        if (slider == null)
        {
            slider = GetComponent<Slider>();
        }
    }

    private void Start()
    {
        // Load the saved brightness level or set a default
        float brightness = PlayerPrefs.HasKey(BrightnessPrefKey) ? PlayerPrefs.GetFloat(BrightnessPrefKey) : 1f;
        slider.value = brightness;
        UpdateBrightness(brightness);

        // Update label and add listener
        label.text = $"{(int)(brightness * 100)}%";
        slider.onValueChanged.AddListener(OnBrightnessChange);
    }

    private void OnDestroy()
    {
        slider.onValueChanged.RemoveListener(OnBrightnessChange);
    }

    private void OnBrightnessChange(float value)
    {
        UpdateBrightness(value);
        PlayerPrefs.SetFloat(BrightnessPrefKey, value);
        PlayerPrefs.Save();
        label.text = $"{(int)(value * 100)}%";
    }

    private void UpdateBrightness(float value)
    {
        // Adjust the brightness in your game
        foregroundFade.color = Color.black * (1 - (value));
    }
}
