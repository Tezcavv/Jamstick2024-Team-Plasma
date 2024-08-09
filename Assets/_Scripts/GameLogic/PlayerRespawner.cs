using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerRespawner : MonoBehaviour
{

    public float respawnTimer = 2f;
    public GameObject spawnPoint;

    private PlayerMovement player;

    private void Awake() {
        player = FindAnyObjectByType<PlayerMovement>();
    }

    public void SpawnPlayer() {

        StartCoroutine(SpawnPlayerCo());

    }

    private IEnumerator SpawnPlayerCo() {
        yield return new WaitForSeconds(respawnTimer);
        player.transform.position = spawnPoint.transform.position;
        player.gameObject.SetActive(true);
    }
}
