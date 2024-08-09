using System;
using System.Collections;
using UnityEngine;

public class VirusSpawner : MonoBehaviour
{
    public float SpawnRate = 1.5f;
    public GameObject virusPrefab;

    private BoxCollider _collider;
    private void Awake() {
        _collider = GetComponent<BoxCollider>();
    }

    private void Start() {
        StartCoroutine(SpawnVirusCo());
    }

    public IEnumerator SpawnVirusCo() {

        
        while ( gameObject.activeSelf ) {

            SpawnVirus();
            yield return new WaitForSeconds(SpawnRate);

        }


    }

    private void SpawnVirus() {
        float minX = transform.position.x - _collider.bounds.extents.x;
        float maxX = transform.position.x + _collider.bounds.extents.x;
        float spawnX = UnityEngine.Random.Range(minX, maxX);

        Instantiate(virusPrefab, new Vector3(spawnX, transform.position.y, transform.position.z),transform.rotation);

    }
}
