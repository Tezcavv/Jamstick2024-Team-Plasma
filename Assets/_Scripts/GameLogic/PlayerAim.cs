using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerAim : MonoBehaviour {

    [Header("Cinemachine")]
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    public GameObject CinemachineCameraTarget;
    [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
    public float CameraAngleOverride = 0.0f;
    [Tooltip("How far in degrees can you move the camera up")]
    public float TopClamp = 70.0f;
    [Tooltip("How far in degrees can you move the camera down")]
    public float BottomClamp = -30.0f;
    [Tooltip("For locking the camera position on all axis")]
    public bool LockCameraPosition = false;

    private const float _threshold = 0.01f;

    InputAction aimAction;
    InputAction shootAction;
    float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    public PlayerParams playerParams;
    

    private void Awake() {
        
        aimAction = GameManager.Instance.inputActions.Player.Look;
        shootAction = GameManager.Instance.inputActions.Player.Attack;
        _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;

       
    }

    private void OnEnable() {
        shootAction.started += CheckForPlayer;
    }

    private void CheckForPlayer(InputAction.CallbackContext context) {
        Vector3 rayOrigin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0 ));

        RaycastHit[] hits = Physics.BoxCastAll(rayOrigin, new Vector3(3, 3, 1), Camera.main.transform.forward, Quaternion.identity, playerParams.BoxCastRange, LayerMask.GetMask("VirusBrain"));

        if (hits.Length == 0)
            return;
        GameObject virus = hits.OrderBy(hit => hit.distance).First().collider.gameObject;
        virus.GetComponent<VirusBrain>().BecomePlayer();

        //disattivo il Playerbrain
        GetComponent<PlayerBrain>().BecomeVirus();
           
        
    }

    private void LateUpdate() {
        CameraRotation();
    }


    private void CameraRotation() {
        // if there is an input and camera position is not fixed
        if (aimAction.ReadValue<Vector2>().sqrMagnitude >= _threshold && !LockCameraPosition) {
            //Don't multiply mouse input by Time.deltaTime;
            //float deltaTimeMultiplier = 1;

            _cinemachineTargetYaw += aimAction.ReadValue<Vector2>().x;
            _cinemachineTargetPitch += aimAction.ReadValue<Vector2>().y;
        }

        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        // Cinemachine will follow this target
        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
            _cinemachineTargetYaw, 0.0f);
    }

    private float ClampAngle(float lfAngle, float lfMin, float lfMax) {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
    private void OnDisable() {

        shootAction.started -= CheckForPlayer;
    }
}
