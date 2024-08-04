using System.Net.Mail;
using UnityEngine;

public class GlobuloBiancoMovement : MonoBehaviour
{
    public GlobuliBianchiParams gParams;
    private Rigidbody rb;
    private PlayerMovement player; 
    //TODO meglio avere una classe marker

    private void Awake() {
       player = FindAnyObjectByType<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        Vector3 movement = (player.transform.position - rb.transform.position).normalized;
        rb.linearVelocity = new Vector3(movement.x,rb.linearVelocity.y,movement.z) * gParams.movementSpeed;
    }

}
