using System.Net.Mail;
using UnityEngine;

public class GlobuloBiancoMovement : MonoBehaviour {
   
    
    public GlobuliBianchiParams gParams;
    private Rigidbody rb;
    private PlayerMovement player;
    public SphereCollider attackRange;
    //TODO meglio avere una classe marker

    private void Awake() {
        player = FindAnyObjectByType<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        Collider[] result = Physics.OverlapSphere(transform.position, attackRange.radius);
        foreach (var item in result)
        {
            if (item.CompareTag("Player")) {
                Vector3 movement = (player.transform.position - rb.transform.position).normalized;
                rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z) * gParams.movementSpeed;
            }
        }
    }

    
}