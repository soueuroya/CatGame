using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Dropdown))]
public class ScreenModeDropdown : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown;

    private void OnValidate()
    {
        if (dropdown == null)
        {
            dropdown = GetComponent<TMP_Dropdown>();
        }
    }

    void Start()
    {
        // Clear the current options in case there are any
        dropdown.ClearOptions();

        // Get all available screen modes and add them to the dropdown
        foreach (FullScreenMode mode in System.Enum.GetValues(typeof(FullScreenMode)))
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData(mode.ToString()));
        }

        // Set the currently active screen mode/saved as the default selection
        int currentMode = (int)Screen.fullScreenMode;
        dropdown.value = SafePrefs.GetInt(Constants.Prefs.screenModeKey, currentMode);

        // Add a listener to the dropdown for when the value changes
        dropdown.onValueChanged.AddListener(delegate { OnDropdownValueChanged(); });
    }

    // Function called when the dropdown value changes
    void OnDropdownValueChanged()
    {
        if ((int)Screen.fullScreenMode != dropdown.value)
        {
            // Apply the selected screen mode
            AudioManager.Instance.PlayAccept();
            ApplyScreenMode(dropdown.value);
        }
    }

    // Function to apply the selected screen mode
    void ApplyScreenMode(int modeIndex)
    {
        // Store current mode index
        int prevModeIndex = (int)Screen.fullScreenMode;

        // Apply the selected screen mode to the screen
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, (FullScreenMode)modeIndex);

        SafePrefs.GetInt(Constants.Prefs.screenModeKey, modeIndex); // On ACCEPT

        //// Ask for confirmation
        //PopupManager.Instance.CreateCountdownPopup(
        //    error: "",
        //    title: "Display settings changed",
        //    primaryButtonText: "CONFIRM",
        //    primaryCallback: () => {
        //        SafePrefs.GetInt(Constants.Prefs.screenModeKey, modeIndex); // On ACCEPT
        //    },
        //    secondaryButtonText: "CANCEL",
        //    secondaryCallback: () => {
        //        Rollback(prevModeIndex); // On Rollback
        //    },
        //    message: "Accept changes?",
        //    countdown: 10);
    }

    void Rollback(int prevModeIndex)
    {
        AudioManager.Instance.PlayFail();
        // clear the dropdown listeners
        dropdown.onValueChanged.RemoveAllListeners();

        // Update dropdown
        dropdown.value = prevModeIndex;

        // Add dropdown listeners again
        dropdown.onValueChanged.AddListener(delegate { OnDropdownValueChanged(); });

        // Apply the selected screen mode to the screen
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, (FullScreenMode)prevModeIndex);
    }
}