using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer VolumesMixer;
    //public Slider masterSlider;
    public Slider musicSlider;
    public Slider SFXSlider;

    public const string MIXER_MASTER = "MasterVolume";
    public const string MIXER_MUSIC = "MusicVolume";
    public const string MIXER_SFX = "SFXVolume";

    private void Awake()
    {
        //masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);

    }

    void Start()
    {
        //musicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 1f);
        //masterSlider.value = PlayerPrefs.GetFloat(AudioManager.MASTER_KEY, 1f);
        //SFXSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 1f);
    }

    void OnDisable()
    {
        SavePrefsToAudioManager();
    }

    public void SetSFXVolume(float value)
    {
        VolumesMixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
    }

    public void SetMusicVolume(float value)
    {
        VolumesMixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
    }

    public void SetMasterVolume(float value)
    {
        VolumesMixer.SetFloat(MIXER_MASTER, Mathf.Log10(value) * 20);
    }
    public void SavePrefsToAudioManager()
    {
        //PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, musicSlider.value);
        //PlayerPrefs.SetFloat(AudioManager.SFX_KEY, SFXSlider.value);
        //PlayerPrefs.SetFloat(AudioManager.MASTER_KEY, masterSlider.value);
    }
}
