using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class PlayerBrain : MonoBehaviour {

    public VirusBrain virusBrain;

    public UnityEvent onDeath;
    public UnityEvent onInfezione;
    public UnityEvent onRespawn;

    private void OnEnable() {
        GameManager.Instance.ActivePlayer = this;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.TryGetComponent(out GlobuloBiancoMovement g)) {

            //logica morte
            gameObject.SetActive(false);
            onDeath.Invoke();
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.TryGetComponent(out Organo organo)) {

            //logica infezione / eventi
            Destroy(gameObject);
            onInfezione.Invoke();
            FindAnyObjectByType<PlayerRespawner>().SpawnPlayer();

        }
    }

    public void BecomeVirus() {
        gameObject.SetActive(false);
        virusBrain.gameObject.SetActive(true);
        GetComponentInParent<CharacterController>().enabled = false;
    }
}
