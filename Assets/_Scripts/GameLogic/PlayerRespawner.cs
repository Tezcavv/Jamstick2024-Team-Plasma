using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerRespawner : MonoBehaviour
{

    public float respawnTimer = 0.5f;
    public GameObject spawnPoint;

    public GameObject player;

    private void Awake() {
    }

    public void SpawnPlayer() {

        StartCoroutine(SpawnPlayerCo());

    }

    private IEnumerator SpawnPlayerCo() {
        yield return new WaitForSeconds(respawnTimer);
        //player.transform.position = spawnPoint.transform.position;
        Instantiate(player, spawnPoint.transform.position, player.transform.rotation);
    }
}
