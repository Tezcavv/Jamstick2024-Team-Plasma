using UnityEngine;

public class VirusMovement : MonoBehaviour
{
    public VirusParams virusParams;

    private void Update() {
        transform.parent.position += new Vector3(0, 0, Time.deltaTime * virusParams.movementSpeed);
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "VirusDestroyer")
            Destroy(transform.parent.gameObject);
    }
}
