using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PVPSetting : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;
    public TextMeshProUGUI loadingText;

    private void Start()
    {
        loadingScreen.SetActive(false); 
    }

    public void LoadPVPGameWithPlayers(int players)
    {
        PlayerPrefs.SetInt("NumberOfPlayers", players);
        AudioManager.instance.PlaySFX("Button Click");

        loadingScreen.SetActive(true); 
        AudioManager.instance.StopMusic();
        StartCoroutine(DelayedLoadGameAsync());
    }

    private System.Collections.IEnumerator DelayedLoadGameAsync()
    {
        yield return new WaitForSeconds(0.1f);

        StartCoroutine(LoadGameAsync());
    }

    private System.Collections.IEnumerator LoadGameAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game"); 
        asyncLoad.allowSceneActivation = false; 

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            loadingBar.value = progress;
            loadingText.text = "Loading... " + (progress * 100f).ToString("F0") + "%";

            if (progress >= 1f)
            {
                asyncLoad.allowSceneActivation = true; 
            }

            yield return null;
        }
       
    }

    public void LoadLocalGameModeScene()
    {
        SceneManager.LoadScene("Local Game Mode");
        AudioManager.instance.PlaySFX("Button Click");
        Resources.UnloadUnusedAssets();
    }
}
