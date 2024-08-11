using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class Cuore : Organo
{

    private int heartInfectionLevel = 0;
    public int HeartInfectionLevel { get { return heartInfectionLevel; } set { heartInfectionLevel = value; } }

    [SerializeField] private int heartInfectionRate = 20;

    public List<AudioClip> heartBeatClips;
    public List<AudioSource> heartBeatSources;
    public int source = -1;

    public void PlayBeatClip()
    {
        if (source < 2)
            source += 1;
        else
            source = 0;

        AudioManager.instance.SourcePlayClips(heartBeatSources[source], heartBeatClips);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered");

        if (other.gameObject.GetComponentInChildren<PlayerBrain>() != null)
        {
            var player = other.gameObject.GetComponentInChildren<PlayerBrain>();

            //player.onDeath.Invoke();
            Destroy(other.transform.parent.gameObject);
            //other.gameObject.SetActive(false);

            GetInfection();

            UI_Manager.instance.WinCondition(HeartInfectionLevel);
        }
    }

    public void GetInfection()
    {
        HeartInfectionLevel += heartInfectionRate;
        UI_Manager.instance.gameUI.GetComponentInChildren<GameUI>().UpdateInfectionFilling(HeartInfectionLevel);

        Debug.Log(HeartInfectionLevel);
         
    }

}
