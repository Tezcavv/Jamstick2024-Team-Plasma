using DG.Tweening;
using System;
using System.Linq;
using System.Net.Mail;
using UnityEngine;

public class GlobuloBiancoMovement : MonoBehaviour {
   
    
    public GlobuliBianchiParams gParams;
    private Rigidbody rb;
    private PlayerMovement player;
    public SphereCollider attackRange;
    //TODO meglio avere una classe marker 
    Vector3 startPos;

    private void Awake() {
        player = FindAnyObjectByType<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    private void FixedUpdate() {
        Collider[] result = Physics.OverlapSphere(transform.position, attackRange.radius);
        if (result.Length < 0) return; 

        GameObject toFollow;
        toFollow = result.FirstOrDefault(c => IsPlayer(c))?.gameObject;
        if (toFollow == null) {
            ReturnToStartPoint();
            return;
        }

        Vector3 movement = (toFollow.transform.position - rb.transform.position).normalized;
        rb.linearVelocity = new Vector3(movement.x, 0, movement.z) * gParams.movementSpeed;
        //rb.DOLookAt(toFollow.transform.position, Time.fixedDeltaTime);

    }

    private void ReturnToStartPoint() {

        if (Vector3.Distance(startPos, rb.transform.position) < 0.05f) return;

        Vector3 movement = (startPos- rb.transform.position).normalized;
        rb.linearVelocity = new Vector3(movement.x, 0, movement.z) * gParams.movementSpeed;
    }

    private bool IsPlayer(Collider c) {
       
        if(c.gameObject == null) return false;

        return c.gameObject.GetComponentInChildren<PlayerBrain>(false) != null;

        


    }
}