using Assets._Scripts;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-100)] 
// Voglio che sia il primo in modo da inizializzare subito le variabili statiche che userò in giro
    public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public InputSystem_Actions inputActions;
    private Player activePlayer;


    public UnityEvent<Player> OnActivePlayerChanged;

    public Player ActivePlayer { get => activePlayer; set {
            SetActivePlayer(value);
} }

    private void SetActivePlayer(Player value) {
        activePlayer = value;
        OnActivePlayerChanged?.Invoke(value);
    }

    private void Awake() {

        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        }
        else {
            Instance = this;
        }
        DontDestroyOnLoad(this);
        inputActions = new InputSystem_Actions();
        inputActions.Enable();
      //  Cursor.lockState = CursorLockMode.Locked;

    }

    
}
