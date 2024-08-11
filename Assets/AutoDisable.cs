using System;
using System.Collections;
using UnityEngine;

public class AutoDisable : MonoBehaviour
{
    public float after;
    private void Start() {
        StartCoroutine(DisableCo());
    }

    private IEnumerator DisableCo() {
        yield return new WaitForSeconds(after);
        gameObject.SetActive(false);
    }


}
