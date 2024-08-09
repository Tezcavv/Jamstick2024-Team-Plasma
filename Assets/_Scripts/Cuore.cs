using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class Cuore : Organo
{
    public List<AudioClip> heartBeatClips;
    public List<AudioSource> heartBeatSources;
    public int source = -1;

    public void PlayBeatClip()
    {
        if (source < 2)
            source += 1;
        else
            source = 0;

        int clip = Random.Range(0, 5);

        heartBeatSources[source].clip = heartBeatClips[clip];
        heartBeatSources[source].Play();

        Debug.Log("event triggered");
    }

}
