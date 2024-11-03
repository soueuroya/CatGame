using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    // Prevent non-singleton constructor use.
    protected AudioManager() { }
    public static AudioManager Instance;

    // Component references
    public AudioMixer masterMixer { get; private set; }
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip uiAccept;
    [SerializeField] AudioClip uiFail;
    [SerializeField] AudioClip uiClick;
    [SerializeField] AudioClip uiType;
    [SerializeField] AudioClip uiPopup;
    [SerializeField] AudioClip uiToken;
    [SerializeField] AudioClip uiBigAccept;
    [SerializeField] AudioClip cardHover;
    [SerializeField] AudioClip cardDrag;
    [SerializeField] AudioClip cardDefensePlay;
    [SerializeField] AudioClip cardAbilityPlay;
    [SerializeField] AudioClip cardAttackPlay;
    [SerializeField] AudioClip passTurn;
    [SerializeField] AudioClip deckShuffle;
    [SerializeField] AudioClip cardDraw;
    [SerializeField] AudioClip energyLoss;
    [SerializeField] AudioClip launch;
    [SerializeField] AudioClip fight;
    [SerializeField] AudioClip defeat;
    [SerializeField] AudioClip victory;
    [SerializeField] AudioClip brutal;
    [SerializeField] AudioClip doctorra;
    [SerializeField] AudioClip gogirl;
    [SerializeField] AudioClip bluecollar;
    [SerializeField] AudioClip sinderella;
    [SerializeField] AudioClip ko;
    [SerializeField] AudioClip versus;
    private float delay = 0;

    #region Initialization
    private void OnValidate()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    // Singleton awake
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this); // AudioManager should stay always loaded
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }
    }

    private void Update()
    {
        if (delay > 0)
        {
            delay -= Time.deltaTime;

            if (delay < 0)
            {
                delay = 0;
            }
        }
    }

    private void Start()
    {
        if (masterMixer == null)
        {
            masterMixer = Resources.Load<AudioMixer>("MasterMixer");
        }

        if (!SafePrefs.HasKey(Constants.Prefs.masterVolume))
        {
            masterMixer.SetFloat(Constants.Prefs.masterVolume, Mathf.Log10(0.5f) * 20);
        }
        else
        {
            float volume = SafePrefs.GetFloat(Constants.Prefs.masterVolume);
            masterMixer.SetFloat(Constants.Prefs.masterVolume, Mathf.Log10(volume) * 20);
        }

        if (!SafePrefs.HasKey(Constants.Prefs.musicCapVolume))
        {
            masterMixer.SetFloat(Constants.Prefs.musicCapVolume, Mathf.Log10(0.5f) * 20);
        }
        else
        {
            float volume = SafePrefs.GetFloat(Constants.Prefs.musicCapVolume);
            masterMixer.SetFloat(Constants.Prefs.musicCapVolume, Mathf.Log10(volume) * 20);
        }

        if (!SafePrefs.HasKey(Constants.Prefs.sfxCapVolume))
        {
            masterMixer.SetFloat(Constants.Prefs.sfxCapVolume, Mathf.Log10(0.5f) * 20);
        }
        else
        {
            float volume = SafePrefs.GetFloat(Constants.Prefs.sfxCapVolume);
            masterMixer.SetFloat(Constants.Prefs.sfxCapVolume, Mathf.Log10(volume) * 20);
        }
    }
    #endregion Initialization

    #region Public Methods
    public void FadeTo(string _exposedParam, float _fadeDuration, float _fadeToValue, Action _callback = null)
    {
        StopAllCoroutines();//make dictionary of coroutines and string to only cancel fading for each mixer, not all mixers.
        StartCoroutine(StartFade(masterMixer, _exposedParam, _fadeDuration, _fadeToValue, _callback));
    }

    public void PlayAccept()
    {
        audioSource.PlayOneShot(uiAccept);
    }
    public void PlayBigAccept()
    {
        audioSource.PlayOneShot(uiBigAccept);
    }

    public void PlayFail()
    {
        audioSource.PlayOneShot(uiFail);
    }

    public void PlayClick()
    {
        audioSource.PlayOneShot(uiClick);
    }

    public void PlayType()
    {
        if (delay > 0)
        {
            return;
        }
        delay = 0.05f;
        audioSource.PlayOneShot(uiType);
    }

    public void PlayPopup()
    {
        audioSource.PlayOneShot(uiPopup);
    }

    public void PlayToken()
    {
        audioSource.PlayOneShot(uiToken);
    }

    public void PlayCardDrag()
    {
        audioSource.PlayOneShot(cardDrag);
    }

    public void PlayDefenseCardPlay()
    {
        audioSource.PlayOneShot(cardDefensePlay);
    }

    public void PlayAbilityCardPlay()
    {
        audioSource.PlayOneShot(cardAbilityPlay);
    }

    public void PlayAttackCardPlay()
    {
        audioSource.PlayOneShot(cardAttackPlay);
    }

    public void PlayPassTurn()
    {
        audioSource.PlayOneShot(passTurn);
    }

    public void PlayDeckShuffle()
    {
        audioSource.PlayOneShot(deckShuffle);
    }

    public void PlayEnergyLoss()
    {
        audioSource.PlayOneShot(energyLoss);
    }

    public void PlayLaunch()
    {
        audioSource.PlayOneShot(launch);
    }

    public void PlayFight()
    {
        audioSource.PlayOneShot(fight);
    }

    public void PlayDefeat()
    {
        audioSource.PlayOneShot(defeat);
    }

    public void PlayVictory()
    {
        audioSource.PlayOneShot(victory);
    }

    public void PlayKO()
    {
        audioSource.PlayOneShot(ko);
    }

    public void PlayVersus()
    {
        audioSource.PlayOneShot(versus);
    }

    public void PlaySinderella()
    {
        audioSource.PlayOneShot(sinderella);
    }

    public void PlayDoctorRa()
    {
        audioSource.PlayOneShot(doctorra);
    }

    public void PlayGoGirl()
    {
        audioSource.PlayOneShot(gogirl);
    }

    public void PlayBlueCollar()
    {
        audioSource.PlayOneShot(bluecollar);
    }

    public void PlayBrutal()
    {
        audioSource.PlayOneShot(brutal);
    }
    public void PlayCardDraw()
    {
        if (delay > 0)
        {
            return;
        }
        delay = 0.1f;
        audioSource.PlayOneShot(cardDraw);
    }
    #endregion Public Methods

    #region Private Helpers
    private static IEnumerator StartFade(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume, Action _callback = null)
    {
        float currentTime = 0;
        float currentVol;
        audioMixer.GetFloat(exposedParam, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 200);
        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 200);
            yield return null;
        }
        _callback?.Invoke();
        yield break;
    }

    public void SetVolume(string exposedParam, float targetVolume)
    {
        SetVolume(masterMixer, exposedParam, targetVolume);
    }

    private void SetVolume(AudioMixer audioMixer, string exposedParam, float targetVolume)
    {
        audioMixer.SetFloat(exposedParam, Mathf.Log10(Mathf.Clamp(targetVolume, 0.0001f, 1)) * 200);
    }
    #endregion Private Helpers
}
