using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.PlayMusic("Main Track");
    }

    public void LoadSettingScene()
    {
        SceneManager.LoadScene("Setting");
        AudioManager.instance.PlaySFX("Button Click");
        Resources.UnloadUnusedAssets();
    }

    public void LoadLocalGameModeScene()
    {
        SceneManager.LoadScene("Local Game Mode");
        AudioManager.instance.PlaySFX("Button Click");
        Resources.UnloadUnusedAssets();
    }
}
