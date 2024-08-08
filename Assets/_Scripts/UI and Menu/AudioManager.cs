using UnityEngine;

[DefaultExecutionOrder(-98)]

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public static AudioSource MusicSource;

    [SerializeField] private AudioClip menuMusic, gameMusic;

    private void Awake()
    {
        instance = this;

        MusicSource = GetComponentInChildren<AudioSource>();
    }
    void Start()
    {
        
    }

    public void PlayMusic()
    {
        MusicSource.clip = menuMusic;
        MusicSource.Play();
    }

    public void SourcePlayClip(AudioSource _audioSource, AudioClip _clip)
    {
        _audioSource.clip = _clip;
        _audioSource.Play();
    }    

}
