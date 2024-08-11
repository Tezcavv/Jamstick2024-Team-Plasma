using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer VolumesMixer;

    //public Slider masterSlider;
    private float masterVolume;

    public Slider musicSlider;
    public Slider musicSlider2;
    private float musicVolume;

    public Slider SFXSlider;
    public Slider SFXSlider2;
    private float SFXVolume;

    public const string MIXER_MASTER = "MasterVolume";
    public const string MIXER_MUSIC = "MusicVolume";
    public const string MIXER_SFX = "SFXVolume";

    private void Awake()
    {

        //masterSlider.onValueChanged.AddListener(SetMasterVolume);

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume); 

        musicSlider2.onValueChanged.AddListener(SetMusicVolume);
        SFXSlider2.onValueChanged.AddListener(SetSFXVolume);

        

        //masterVolume = 1f;
        //musicVolume = musicSlider.value;
        //SFXVolume = SFXSlider.value;
    }

    void Start()
    {
        LoadVolumes();
    }

    public void LoadVolumes()
    {
        List<float> volumes = AudioManager.instance.LoadVolumesFromPref();

        masterVolume = volumes[0];
        musicSlider.value = volumes[1];
        SFXSlider.value = volumes[2];
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = value;
        musicSlider.value = musicVolume;
        musicSlider2.value = musicVolume;

        VolumesMixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20 - 20);
    }

    public void SetSFXVolume(float value)
    {
        SFXVolume = value;
        SFXSlider.value = SFXVolume;
        SFXSlider2.value = SFXVolume;

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
