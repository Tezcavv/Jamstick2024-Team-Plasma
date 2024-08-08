using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.TextCore.Text;
using UnityEngine.InputSystem;
using System;

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

        pauseAction = GameManager.Instance.inputActions.UI.Pause;
        pauseAction.started += PauseGame;
    }


    private void Start()
    {
        //infectedCells = GameManager.instance ? GameManager.instance.infectedCells : 0;

        //SetMenuActive(mainMenu);
    }

    public void StartGame()
    {
        SetMenuOff(mainMenu);
        SetMenuActive(gameUI);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame(InputAction.CallbackContext context) {
        if(!mainMenu.activeInHierarchy ) {
            TogglePause();
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
    }

    public void BackToMain()
    {
        TogglePause();

        SetMenuOff(gameUI);
        SetMenuActive(mainMenu);
    }

    public void TogglePause()
    {
        if (!isPaused)
        {
            isPaused = true;
            SetMenuActive(pauseMenu);

            Time.timeScale = 0;
        }
        else
        {
            isPaused = false;
            SetMenuOff(pauseMenu);

            Time.timeScale = 1;
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
