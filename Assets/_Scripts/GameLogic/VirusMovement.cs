using DG.Tweening;
using UnityEngine;

public class VirusMovement : MonoBehaviour
{
    public VirusParams virusParams;

    private Cuore cuore;

    private void Start() {
        cuore = FindAnyObjectByType<Cuore>();
    }

    private void Update() {
        transform.parent.position += new Vector3(0, 0, Time.deltaTime * virusParams.movementSpeed);
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "VirusDestroyer")
            Destroy(transform.parent.gameObject);
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.TryGetComponent(out GlobuloBiancoMovement globuloBianco)) {
            Destroy(transform.parent.gameObject);
        }
    }
}
