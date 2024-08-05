using System.Collections;
using UnityEngine;


[DefaultExecutionOrder(-100)] 
// Voglio che sia il primo in modo da inizializzare subito le variabili statiche che userò in giro
    public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public InputSystem_Actions inputActions;


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

        
    }
}
