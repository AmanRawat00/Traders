using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] musicSounds;
    public Sound[] sfxSounds;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        GameObject musicSliderObject = GameObject.FindWithTag("MusicSlider");
        if (musicSliderObject != null)
        {
            Slider musicSlider = musicSliderObject.GetComponent<Slider>();
            if (musicSlider != null)
            {
                musicSource.volume = musicSlider.value;
            }         
        }
       
        GameObject sfxSliderObject = GameObject.FindWithTag("SfxSlider");
        if (sfxSliderObject != null)
        {
            Slider sfxSlider = sfxSliderObject.GetComponent<Slider>();
            if (sfxSlider != null)
            {
                sfxSource.volume = sfxSlider.value;
            }       
        }
       
    }

    public void PlayMusic(string soundName)
    {
        Sound sound = GetSound(soundName, musicSounds);
        if (sound != null)
        {
            musicSource.clip = sound.clip;
            musicSource.loop = sound.loop;
            musicSource.volume = sound.volume;
            musicSource.pitch = sound.pitch;
            musicSource.Play();
        }
    }

    public void PlaySFX(string soundName)
    {
        Sound sound = GetSound(soundName, sfxSounds);
        if (sound != null)
        {
            sfxSource.PlayOneShot(sound.clip, sound.volume);
            sfxSource.pitch = sound.pitch;
        }
    }

    private Sound GetSound(string soundName, Sound[] sounds)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name == soundName)
            {
                return sound;
            }
        }
        Debug.LogWarning("Sound not found: " + soundName);
        return null;
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void StopSFX()
    {
        sfxSource.Stop();
    }

}
