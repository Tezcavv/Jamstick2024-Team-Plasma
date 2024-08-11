using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.TextCore.Text;
using UnityEngine.InputSystem;
using System;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;
using NUnit.Framework;
using System.Collections.Generic;

[DefaultExecutionOrder(-99)]

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;

    private int infectedCells = 0;
    public int InfectedCells => infectedCells;

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
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        //Time.timeScale = 0f;

        infectedCells = PlayerPrefs.GetInt("infected_cells", 0);

        pauseAction = GameManager.Instance.inputActions.UI.Pause;
        pauseAction.started += PauseGame;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);

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

        SceneManager.LoadScene(0);

        //Time.timeScale = 0f;
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

    public void SaveScoreToPref()
    {
        PlayerPrefs.SetInt("infected_cells", InfectedCells);
    }

    public void SaveVolumesToPref(List<float> volumes)
    {
        PlayerPrefs.SetFloat(AudioManager.MASTER_KEY, volumes[0]);
        PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, volumes[1]);
        PlayerPrefs.SetFloat(AudioManager.SFX_KEY, volumes[2]);
    }

    public void WinCondition(int _infectionLevel)
    {
        if (_infectionLevel >= 100)
        {
            StartCoroutine(WinningRoutine());
        }
        else
        {
            FindObjectOfType<PlayerRespawner>().SpawnPlayer();
        }
    }

    public IEnumerator WinningRoutine()
    {
        infectedCells++;

        SaveScoreToPref();

        Debug.Log("ORGAN IS INFECTED!!!");

        yield return new WaitForSeconds(1f);

        Debug.Log("YOU WIN!!!");

        yield return new WaitForSeconds(1f);

        SetMenuOff(gameUI);
        SetMenuActive(mainMenu);

        SceneManager.LoadScene(0);

    }
}
