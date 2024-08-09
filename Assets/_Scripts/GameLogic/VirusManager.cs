using System;
using UnityEngine;
using UnityEngine.Events;

public class VirusManager : MonoBehaviour
{
    public VirusBrain virusBrain;
    public PlayerBrain playerBrain;

    private void Awake() {
        virusBrain = GetComponentInChildren<VirusBrain>(true);
        playerBrain = GetComponentInChildren<PlayerBrain>(true);

    }

    private void Start() {
        GameManager.Instance.OnActivePlayerChanged .AddListener( ManageVirus);
    }

    private void ManageVirus(PlayerBrain p) {
        if(GetComponentInChildren<PlayerBrain>(true) == p) {
        
            virusBrain.gameObject.SetActive(false);
            playerBrain.gameObject.SetActive(true);

        }
    }
}
