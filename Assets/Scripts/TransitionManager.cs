using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class TransitionManager : MonoBehaviour
{
    public Image whiteImage;
    public Image blackImage;
    public float whiteFadeOutDuration = 2f;
    public float blackFadeOutDuration = 2f;
    public float blackFadeInDuration = 2f;
    public event Action OnTransitionDone;

    private bool transitionCompleted = false; 

    public void StartFadeOutWhite()
    {
        if (!transitionCompleted)
        {
            StartCoroutine(FadeOutWhite());
        }
    }

    private IEnumerator FadeOutWhite()
    {
        whiteImage.gameObject.SetActive(true);
        Color startColor = whiteImage.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);
        float startTime = Time.time;

        while (Time.time < startTime + whiteFadeOutDuration)
        {
            float t = (Time.time - startTime) / whiteFadeOutDuration;
            whiteImage.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        Destroy(whiteImage.gameObject);

        if (!transitionCompleted)
        {
            transitionCompleted = true; 
            OnTransitionDone?.Invoke();
        }
    }

    public void StartFadeOutBlack()
    {
        if (!transitionCompleted)
        {
            StartCoroutine(FadeOutBlack());
        }
    }

    private IEnumerator FadeOutBlack()
    {
        blackImage.gameObject.SetActive(true);
        Color startColor = blackImage.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);
        float startTime = Time.time;

        while (Time.time < startTime + blackFadeOutDuration)
        {
            float t = (Time.time - startTime) / blackFadeOutDuration;
            blackImage.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        blackImage.color = endColor;

        if (!transitionCompleted)
        {
            transitionCompleted = true; 
            OnTransitionDone?.Invoke();
        }
    }

    public void StartFadeInBlack()
    {
        if (!transitionCompleted)
        {
            StartCoroutine(FadeInBlack());
        }
    }

    private IEnumerator FadeInBlack()
    {
        blackImage.gameObject.SetActive(true);
        Color startColor = new Color(blackImage.color.r, blackImage.color.g, blackImage.color.b, 0f);
        Color endColor = Color.black;
        float startTime = Time.time;

        while (Time.time < startTime + blackFadeInDuration)
        {
            float t = (Time.time - startTime) / blackFadeInDuration;
            blackImage.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        blackImage.color = endColor;

        if (!transitionCompleted)
        {
            transitionCompleted = true; 
            OnTransitionDone?.Invoke();
        }
    }

    public void DeactivateBlackImage()
    {
        if (blackImage != null)
        {
            blackImage.gameObject.SetActive(false);
        }
    }

    public void ResetTransition()
    {
            transitionCompleted = false;       
    }
}
