﻿using System;
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

            Destroy(transform.parent.gameObject);
            FindAnyObjectByType<PlayerRespawner>().SpawnPlayer();
            onDeath.Invoke();

            // [DAVIDE] - Ho aggiunto la prossima riga di codice perchè non mi funziona l'invoke dell'evento: attiva il messaggio di morte //
            //
            UI_Manager.instance.DeadEffectsRoutine();
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.TryGetComponent(out Organo organo)) {

        }
    }

    public void BecomeVirus() {
        gameObject.SetActive(false);
        virusBrain.gameObject.SetActive(true);
        GetComponentInParent<CharacterController>().enabled = false;
    }
}
