using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class IntroMovie : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private Image image;
    [SerializeField] private AudioClip introClip;
    
    [Header("Fade Settings")]
    private float initialFadeDuration = 2.5f;
    private float fadeInDuration = 1f;
    private float fadeOutDuration = 1f;

    private Action onAnimationFinished;
    private bool hasFinished;
    private bool fadeOutTriggered;

    private void Awake()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
        //videoPlayer.prepareCompleted += OnVideoPrepared;
    }

    private void OnDestroy()
    {
        videoPlayer.loopPointReached -= OnVideoFinished;
        //videoPlayer.prepareCompleted -= OnVideoPrepared;
    }

    public void PlayVideo(Action onAnimationFinished)
    {
        this.onAnimationFinished = onAnimationFinished;
        hasFinished = false;
        fadeOutTriggered = false;

#if UNITY_WEBGL

        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = "https://heistofwhiskers.s3.us-east-1.amazonaws.com/IntroMovie.mp4";

#endif
        videoPlayer.Prepare();
        LeanTween.color(image.rectTransform, Color.black, initialFadeDuration)
            .setOnComplete(() =>
            {
                Invoke("StartVideo", 1.5f);
            });
    }

    private void StartVideo()
    {
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Play();
        Color transparent = Color.black;
        transparent.a = 0f;
        MusicManager.Instance.StartMusic(introClip);

        LeanTween.color(image.rectTransform, transparent, fadeInDuration)
        .setOnComplete(() =>
        {

        });
    }

    //private void OnVideoPrepared(VideoPlayer vp)
    //{
    //    // Schedule automatic fade-out near the end (simple & less accurate)
    //    float fadeStartTime = Mathf.Max(0f, 52);
    //
    //    LeanTween.delayedCall(fadeStartTime, () => { TriggerFadeOut(false); });
    //}

    private void Update()
    {
        // Cut video short on ANY input
        if (hasFinished)
            return;

        if (Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            TriggerFadeOut(true);
        }
    }

    /// <summary>
    /// Can be called externally at any time to fade to black and end the video.
    /// </summary>
    public void TriggerFadeOut(bool forced = false)
    {
        if (fadeOutTriggered)
            return;

        fadeOutTriggered = true;

        if (forced)
        {
            LeanTween.cancel(image.rectTransform);
            LeanTween.color(image.rectTransform, Color.black, fadeOutDuration)
                .setOnComplete(Finish);
        }
        else
        {
            Finish();
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        Finish();
    }

    private void Finish()
    {
        if (hasFinished)
            return;

        hasFinished = true;

        if (videoPlayer.isPlaying)
            videoPlayer.Stop();

        MusicManager.Instance.StopMusic();
        onAnimationFinished?.Invoke();
        onAnimationFinished = null;
    }
}
