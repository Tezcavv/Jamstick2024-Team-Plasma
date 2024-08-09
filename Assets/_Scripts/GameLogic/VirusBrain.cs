using UnityEngine.Events;
using System.Collections;
using UnityEngine;


public class VirusBrain : MonoBehaviour {

    public PlayerBrain player;

    public void BecomePlayer() {
        GameManager.Instance.ActivePlayer = player;
        player.gameObject.SetActive(true);
        gameObject.SetActive(false);
        GetComponentInParent<CharacterController>().enabled = true;
    }


}
