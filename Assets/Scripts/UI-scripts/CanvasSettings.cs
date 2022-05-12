using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSettings : MonoBehaviour
{
    public GameObject canvasMainMenu;
    public GameObject canvasOptions;
    public Slider musicSlider;
    public Slider soundSlider;
    public Toggle music;
    public Toggle sound;

    private void Start()
    {        
        if (PlayerPrefs.HasKey("MusicSlider"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicSlider", musicSlider.value);
        }
        else
        {
            musicSlider.value = 3f;
        }

        if (PlayerPrefs.HasKey("SoundSlider"))
        {
            soundSlider.value = PlayerPrefs.GetFloat("SoundSlider", soundSlider.value);
        }
        else
        {
            soundSlider.value = 3f;
        }

        if (PlayerPrefs.HasKey("MusicToggle"))
        {
            if (PlayerPrefs.GetInt("MusicToggle") == 0)
                music.isOn = false;
            else
                music.isOn = true;
        }
        else
        {
            music.isOn = true;
            PlayerPrefs.SetInt("MusicToggle", 1);
        }

        if (PlayerPrefs.HasKey("SoundToggle"))
        {
            if (PlayerPrefs.GetInt("SoundToggle") == 0)
                sound.isOn = false;
            else
                sound.isOn = true;
        }

        else
        {
           sound.isOn = true;
           PlayerPrefs.SetInt("SoundToggle", 1);
        }
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("MusicSlider", musicSlider.value);
        PlayerPrefs.SetFloat("SoundSlider", soundSlider.value);
        gameObject.SetActive(false);
        canvasOptions.SetActive(true);
    }

    public void Back()
    {
        canvasOptions.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ResetHiScore()
    {
        PlayerPrefs.DeleteKey("MyLevel");
        PlayerPrefs.DeleteKey("MusicSlider");
        PlayerPrefs.DeleteKey("SoundSlider");
        PlayerPrefs.DeleteKey("DiffSlider");
    }

    public void Music()
    {
        if (music.isOn)
            PlayerPrefs.SetInt("MusicToggle", 1);
        else
            PlayerPrefs.SetInt("MusicToggle", 0);
    }

    public void Sound()
    {
        if (sound.isOn)
            PlayerPrefs.SetInt("SoundToggle", 1);
        else
            PlayerPrefs.SetInt("SoundToggle", 0);
    }
}

