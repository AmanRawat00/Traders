using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOpening : MonoBehaviour
{
    public Slider loadingBar;
    public TextMeshProUGUI loadingText;
    public TextMeshProUGUI continueText;

    private bool isLoadingComplete = false;

    private void Start()
    {
        loadingBar.value = 0f;
        loadingText.text = "Loading...";
        continueText.gameObject.SetActive(false);

        StartCoroutine(SimulateLoading());
    }

    private IEnumerator SimulateLoading()
    {
        float progress = 0f;
        while (progress < 1f)
        {
            progress += Time.deltaTime;
            loadingBar.value = progress;
            yield return null;
        }

        isLoadingComplete = true;
        loadingText.gameObject.SetActive(false); 
        continueText.gameObject.SetActive(true);
        loadingBar.gameObject.SetActive(false); 
    }

    private void Update()
    {
        if (isLoadingComplete)
        {
            if (Input.GetMouseButtonDown(0) || Input.anyKeyDown || Input.touchCount > 0)
            {
                SceneManager.LoadScene("Game Menu");
                Resources.UnloadUnusedAssets();
            }
        }
    }
}
