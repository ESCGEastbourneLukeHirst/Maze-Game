using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] Slider volumeSlider = null;
    [SerializeField] Slider SFXSlider = null;

    [SerializeField] AudioClip MainaudioClip = null;
    [SerializeField] AudioClip SFXudioClip = null;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    public Toggle musicToggle;



    private void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }

        else
        {
            Load();
        }

        if (!PlayerPrefs.HasKey("SFXVolume"))
        {
            PlayerPrefs.SetFloat("SFXVolume", 1);
            LoadSFX();
        }

        else
        {
            LoadSFX();
        }
    }

    public void ChangeVolumeMusic(float Volume)
    {
        volumeSlider.value = Volume;
        musicSource.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }


    public void ChangeVolumeSFX()
    {
        sfxSource.volume = SFXSlider.value;
        SaveSFX();
    }

    private void LoadSFX()
    {
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }

    private void SaveSFX()
    {
        PlayerPrefs.SetFloat("SFXVolume", SFXSlider.value);
    }
    public void ToggleMusic(int logic)
    {
        if (musicToggle.isOn == true)
        {
            logic = 1;
            volumeSlider.interactable = true;
            ChangeVolumeMusic(PlayerPrefs.GetFloat("SaveSFX"));
        }
        else if (musicToggle.isOn == false)
        {
            logic = 0;
            volumeSlider.interactable = false;
            musicSource.volume = PlayerPrefs.GetFloat("musicVolume", 0);
        }

        PlayerPrefs.SetInt("musicToggleSaveKey", logic);
    }
    public void EasyMode()
    {
        PlayerPrefs.SetString("DifficultySave", "Easy");
    }
    public void MediumMode()
    {
        PlayerPrefs.SetString("DifficultySave", "Normal");
    }
    public void HardMode()
    {
        PlayerPrefs.SetString("DifficultySave", "Hard");
    }


}
