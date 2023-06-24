using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider; 
    public GameObject helpPanel;
    public Scrollbar scrollbar;

    public void ToggleMusic()
    {
        float volume = musicSlider.value > 0 ? 0 : 1; 
        AudioManager.instance.SetMusicVolume(volume);

        if (musicSlider.value > 0)
        {
            musicSlider.value = 0; 
        }
        else
        {
            musicSlider.value = 1; 
        }
    }

    public void ToggleSfx()
    {
        float volume = sfxSlider.value > 0 ? 0 : 1; 
        AudioManager.instance.SetSFXVolume(volume);

        if (sfxSlider.value > 0)
        {
            sfxSlider.value = 0; 
        }
        else
        {
            sfxSlider.value = 1; 
        }
    }

    public void OpenHelpPanel()
    {
        AudioManager.instance.PlaySFX("Button Click");
        helpPanel.SetActive(true);
        scrollbar.value = 1f; 
    }

    public void CloseHelpPanel()
    {
        AudioManager.instance.PlaySFX("Button Click");
        helpPanel.SetActive(false);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Game Menu");
        AudioManager.instance.PlaySFX("Button Click");
        Resources.UnloadUnusedAssets();
    }
}
