using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-98)]

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public const string MASTER_KEY = "masterVolume";
    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "SFXVolume";

    private float masterVolume;
    public float MasterVolume => masterVolume;

    private float musicVolume;
    public float MusicVolume => musicVolume;

    private float sfxVolume;
    public float SfxVolume => sfxVolume;


    public static AudioSource MusicSource;
    [SerializeField] private AudioClip menuMusic, gameMusic;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        MusicSource = GetComponentInChildren<AudioSource>();

        masterVolume = PlayerPrefs.GetFloat(MASTER_KEY, 1f);
        musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);

    }
    void Start()
    {


    }

    public void PlayBackgroundMusic()
    {
        MusicSource.clip = menuMusic;
        MusicSource.Play();
    }

    public void SourcePlayClips(AudioSource _audioSource, AudioClip _clip)
    {
        _audioSource.clip = _clip;
        _audioSource.Play();
    }

    public void SourcePlayClips(AudioSource _audioSource, List<AudioClip> _clips)
    {
        int _clip = Random.Range(0, _clips.Count);

        _audioSource.clip = _clips[_clip];
        _audioSource.Play();
    }

}
