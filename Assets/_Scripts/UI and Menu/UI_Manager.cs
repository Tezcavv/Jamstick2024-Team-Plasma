using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.TextCore.Text;
using UnityEngine.InputSystem;
using System;
using UnityEngine.Events;

[DefaultExecutionOrder(-99)]

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;

    public int infectedCells = 666;

    public bool isPaused = false;

    public GameObject mainMenu;
    public GameObject gameUI;
    public GameObject pauseMenu;
    public GameObject optionsMenu;


    InputAction pauseAction;

    public UnityEvent onPause;
    public UnityEvent onResume;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        Time.timeScale = 0f;

        pauseAction = GameManager.Instance.inputActions.UI.Pause;
        pauseAction.started += PauseGame;
    }


    private void Start()
    {
        //infectedCells = GameManager.instance ? GameManager.instance.infectedCells : 0;

    }

    public void StartGame()
    {
        SetMenuOff(mainMenu);
        SetMenuActive(gameUI);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame(InputAction.CallbackContext context) {
        if(!mainMenu.activeInHierarchy ) {
            TogglePause();
            onPause?.Invoke();
        }
    }

    public void OpenOptions()
    {
        SetMenuActive(optionsMenu);
    }

    public void CloseOptions()
    {
        SetMenuOff(optionsMenu);
    }

    public void ResumeGame()
    {
        TogglePause();
        onResume?.Invoke();
    }

    public void BackToMain()
    {
        TogglePause();

        SetMenuOff(gameUI);
        SetMenuActive(mainMenu);

        Time.timeScale = 0f;
    }

    public void TogglePause()
    {
        if (!isPaused)
        {
            isPaused = true;
            SetMenuActive(pauseMenu);

            Time.timeScale = 0f;
        }
        else
        {
            isPaused = false;
            SetMenuOff(pauseMenu);

            Time.timeScale = 1f;
        }
    }

    public void SetMenuActive(GameObject _menu)
    {
        _menu.SetActive(true);
    }

    public void SetMenuOff(GameObject _menu)
    {
        _menu.SetActive(false);
    }
}
