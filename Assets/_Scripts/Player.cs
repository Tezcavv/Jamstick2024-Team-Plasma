using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets._Scripts {
    public class Player : MonoBehaviour {

        public UnityEvent onDeath;
        public UnityEvent onInfezione;
        public UnityEvent onRespawn;


        private void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.TryGetComponent(out GlobuloBiancoMovement g)) {
                gameObject.SetActive(false);
                onDeath.Invoke();
            }
        }


        private void OnTriggerEnter(Collider other) {
            if(other.gameObject.TryGetComponent(out Organo organo)) {
                print("INFEZIONE");
                gameObject.SetActive(false);
                onInfezione.Invoke();

            }
        }
    }
}