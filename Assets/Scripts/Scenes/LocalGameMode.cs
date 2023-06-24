using UnityEngine;
using UnityEngine.SceneManagement;
public class LocalGameMode : MonoBehaviour
{
    public void LoadLocalPVP()
    {
        SceneManager.LoadScene("PVP Setting");
        AudioManager.instance.PlaySFX("Button Click");
        Resources.UnloadUnusedAssets();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Game Menu");
        AudioManager.instance.PlaySFX("Button Click");
        Resources.UnloadUnusedAssets();
    }
}
