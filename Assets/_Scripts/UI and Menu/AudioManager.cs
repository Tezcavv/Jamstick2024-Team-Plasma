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

    public void SaveVolumesToPref(List<float> volumes)
    {
        PlayerPrefs.SetFloat(MASTER_KEY, volumes[0]);
        PlayerPrefs.SetFloat(MUSIC_KEY, volumes[1]);
        PlayerPrefs.SetFloat(SFX_KEY, volumes[2]);
    }

    public List<float> LoadVolumesFromPref()
    {
        List<float> volumes = new List<float>();

        volumes.Add(PlayerPrefs.GetFloat(MASTER_KEY, 1f));
        volumes.Add(PlayerPrefs.GetFloat(MUSIC_KEY, 1f));
        volumes.Add(PlayerPrefs.GetFloat(SFX_KEY, 1f));

        return volumes;
    }

}
