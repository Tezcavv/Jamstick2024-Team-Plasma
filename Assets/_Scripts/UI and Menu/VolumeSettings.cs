using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer VolumesMixer;

    //public Slider masterSlider;
    private float masterVolume;

    public Slider musicSlider_pause;
    public Slider musicSlider_main;
    private float musicVolume;

    public Slider SFXSlider_pause;
    public Slider SFXSlider_main;
    private float SFXVolume;

    public const string MIXER_MASTER = "MasterVolume";
    public const string MIXER_MUSIC = "MusicVolume";
    public const string MIXER_SFX = "SFXVolume";

    private void Awake()
    {

        //masterSlider.onValueChanged.AddListener(SetMasterVolume);

        musicSlider_pause.onValueChanged.AddListener(SetMusicVolume);
        SFXSlider_pause.onValueChanged.AddListener(SetSFXVolume); 

        musicSlider_main.onValueChanged.AddListener(SetMusicVolume);
        SFXSlider_main.onValueChanged.AddListener(SetSFXVolume);

        //masterVolume = 1f;
        //musicVolume = musicSlider_pause.value;
        //SFXVolume = SFXSlider_pause.value;
    }

    void Start()
    {
        LoadVolumes();
    }

    public void LoadVolumes()
    {
        List<float> volumes = AudioManager.instance.LoadVolumesFromPref();

        masterVolume = volumes[0];
        musicSlider_pause.value = volumes[1];
        SFXSlider_pause.value = volumes[2];
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = value;
        musicSlider_pause.value = musicVolume;
        musicSlider_main.value = musicVolume;

        VolumesMixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20 - 15);
    }

    public void SetSFXVolume(float value)
    {
        SFXVolume = value;
        SFXSlider_pause.value = SFXVolume;
        SFXSlider_main.value = SFXVolume;

        VolumesMixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
    }

    public void SetMasterVolume(float value)
    {
        VolumesMixer.SetFloat(MIXER_MASTER, Mathf.Log10(value) * 20);
    }

    void OnDisable()
    {
        List<float> volumes = new List<float>() { masterVolume, musicVolume, SFXVolume };

        AudioManager.instance.SaveVolumesToPref(volumes);
    }
}
