using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class IntroMovie : MonoBehaviour
{
    public enum IntroVideoType
    {
        LogoAnimation,
        GameIntro,
        GoldEnding,
        CardboardEnding,
        SilverEnding
    }

    [Header("References")]
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private RawImage videoImage;
    [SerializeField] private Image foregroundFade;
    [SerializeField] private AudioClip introClip;
    [SerializeField] private bool level1Intro;
    [SerializeField] private RenderTexture videoRT;
    [SerializeField] private bool isEnding;

    [Header("Video Selection")]
    [SerializeField] private IntroVideoType videoType;

    [Header("Fade Settings")]
    private float fadeInDuration = 1f;
    private float fadeOutDuration = 1f;

    private Action onAnimationFinished;
    private bool hasFinished;
    private bool fadeOutTriggered;

    private void Awake()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    private void Start()
    {
        if (isEnding)
        {
            PlayVideo(null);
        }
    }

    private void OnDestroy()
    {
        videoPlayer.loopPointReached -= OnVideoFinished;
    }

    public void PlayVideo(Action onAnimationFinished)
    {
        this.onAnimationFinished = onAnimationFinished;
        hasFinished = false;
        fadeOutTriggered = false;

        ClearRenderTexture();
        SetVideoFromStreamingAssets();

        if (level1Intro)
        {
            LeanTween.color(foregroundFade.rectTransform, Color.black, fadeOutDuration)
                .setOnComplete(StartVideo);
        }
        else
        {
            StartVideo();
        }
    }

    // ----------------------------------------------------
    // StreamingAssets video selection
    // ----------------------------------------------------
    private void SetVideoFromStreamingAssets()
    {
        videoPlayer.source = VideoSource.Url;

        string fileName = videoType switch
        {
            IntroVideoType.LogoAnimation => "LogoAnimation.mp4",
            IntroVideoType.GameIntro => "IntroMovie.mp4",
            IntroVideoType.GoldEnding => "Golden Crown.mp4",
            IntroVideoType.CardboardEnding => "cardoard.mp4",
            IntroVideoType.SilverEnding => "silver crown.mp4",
            _ => "LogoAnimation.mp4"
        };

        string fullPath = Path.Combine(Application.streamingAssetsPath, fileName);

#if UNITY_EDITOR || UNITY_STANDALONE
        videoPlayer.url = fullPath;
#else
        // WebGL / mobile requires proper URL formatting
        videoPlayer.url = fullPath.Replace("\\", "/");
#endif
    }

    private void StartVideo()
    {
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Play();

        if (level1Intro)
        {
            Color transparent = Color.black;
            transparent.a = 0f;

            MusicManager.Instance.StartMusic(introClip);
            LeanTween.color(foregroundFade.rectTransform, transparent, fadeInDuration);
        }
    }

    private void Update()
    {
        if (hasFinished)
            return;

        if (Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            if (level1Intro)
            {
                TriggerFadeOut();
            }
            else
            {
                CancelInvoke(nameof(TriggerFadeOut));
                Invoke(nameof(TriggerFadeOut), 1f);
            }
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        if (level1Intro)
        {
            TriggerFadeOut();
        }
        else
        {
            CancelInvoke(nameof(TriggerFadeOut));
            Invoke(nameof(TriggerFadeOut), 1f);
        }
    }

    public void TriggerFadeOut()
    {
        if (fadeOutTriggered)
            return;

        fadeOutTriggered = true;

        if (!isEnding)
        {
            LeanTween.cancel(foregroundFade.rectTransform);
            LeanTween.color(foregroundFade.rectTransform, Color.black, fadeOutDuration)
                .setOnComplete(Finish);
        }
        else
        {
            Finish();
        }
    }

    private void Finish()
    {
        if (hasFinished)
            return;

        hasFinished = true;

        if (videoPlayer.isPlaying)
            videoPlayer.Stop();

        ClearRenderTexture();

        videoImage.color = new Color(0f, 0f, 0f, 0f);

        if (level1Intro)
        {
            MusicManager.Instance.StopMusic();
        }

        onAnimationFinished?.Invoke();
        onAnimationFinished = null;
    }

    private void ClearRenderTexture()
    {
        if (videoRT == null)
            return;

        RenderTexture active = RenderTexture.active;
        RenderTexture.active = videoRT;
        GL.Clear(true, true, Color.black);
        RenderTexture.active = active;
    }
}
