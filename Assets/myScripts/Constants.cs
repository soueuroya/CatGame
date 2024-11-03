using UnityEngine;

public sealed class Constants
{
    public class Prefs {
        public static string currentID = "currentID";
        public static string currentProof = "currentProof";
        public static string proofExpireDate = "proofExpireDate";
        public static string masterVolume = "masterVolume";
        public static string musicCapVolume = "musicCapVolume";
        public static string sfxCapVolume = "sfxCapVolume";
        public static string screenModeKey = "screenModeKey";
        public static string selectedTheme = "selectedTheme";
        public static string reMatch = "reMatch";
    }

    public class GameSettings{
        public static float transitionAnimationTime = 0.5f;
        public static int attackSkipID = -1;
        public static int defenseSkipID = -2;
        public static float opponentPlayCardDelay = 2.0f;
        public static float playerPlayCardDelay = 1f;
    }

    public class Colors
    {
        public static Color idleUiColor = new Color(1f, 0f, 0f, 1f);
        public static Color selectedUiColor = new Color(0.55f, 1f, 0.4f, 1f);
    }
}
