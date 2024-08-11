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
        toFollow = result.FirstOrDefault(c => CheckGameObject(c))?.gameObject;
        if (toFollow == null) {
            ReturnToStartPoint();
            return;
        }

        Vector3 movement = (toFollow.transform.position - rb.transform.position).normalized;
        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z) * gParams.movementSpeed;
        rb.DOLookAt(toFollow.transform.position, Time.fixedDeltaTime);

    }

    private void ReturnToStartPoint() {
        Vector3 movement = (startPos- rb.transform.position).normalized;
        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z) * gParams.movementSpeed;
    }

    private bool CheckGameObject(Collider c) {
       
        if(c.gameObject == null) return false;

        if(c.gameObject.GetComponentInChildren<PlayerBrain>(false) == null) return false;

        return true;


    }
}