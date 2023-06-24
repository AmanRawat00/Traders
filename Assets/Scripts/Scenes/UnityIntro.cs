using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class UnityIntro : MonoBehaviour
{
    public Image backgroundImage;
    public Image fadeInImage;
    public float colorTransitionDuration = 2.5f;
    public float fadeInDuration = 1.5f;
    public string nextSceneName;

    private void Start()
    {
        AudioManager.instance.PlayMusic("Game Opening");
        StartCoroutine(TransitionSequence());
    }

    private IEnumerator TransitionSequence()
    {
        Color initialColor = Color.black;
        Color targetColor = Color.white;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / colorTransitionDuration;
            backgroundImage.color = Color.Lerp(initialColor, targetColor, t);
            yield return null;
        }

        fadeInImage.color = Color.clear;
        float fadeInT = 0f;
        while (fadeInT < 1f)
        {
            fadeInT += Time.deltaTime / fadeInDuration;
            fadeInImage.color = Color.Lerp(Color.clear, Color.white, fadeInT);
            yield return null;
        }
         
        yield return new WaitForSeconds(2.5f);

        SceneManager.LoadScene(nextSceneName);
    }

}

