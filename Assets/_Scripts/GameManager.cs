
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-100)] 
// Voglio che sia il primo in modo da inizializzare subito le variabili statiche che userò in giro
public class GameManager : MonoBehaviour 
{

    public static GameManager Instance;
    public InputSystem_Actions inputActions;
    private PlayerBrain activePlayer;


    public UnityEvent<PlayerBrain> OnActivePlayerChanged;

    public PlayerBrain ActivePlayer { get => activePlayer; set {SetActivePlayer(value); } }

    private void SetActivePlayer(PlayerBrain value) {
        UnsubscribeToPlayerEvents();
        activePlayer = value;
        OnActivePlayerChanged?.Invoke(value);
        SubscribeToPlayerEvents();
    }

    private void SubscribeToPlayerEvents() {
        
    }

    private void UnsubscribeToPlayerEvents() {
    }

    private void Start() {
        UI_Manager.instance.onResume.AddListener(OnResume);
        UI_Manager.instance.onPause.AddListener(OnPause);
    }

    private void OnResume() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnPause() {
        Cursor.lockState = CursorLockMode.None;
    }

    private void Awake() {

        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        else {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        inputActions = new InputSystem_Actions();
        inputActions.Enable();

    }


    
}
