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
    public GameObject WinPanel;
    public GameObject DeadTXT;
    public GameObject HitTXT;



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

    Coroutine deathRoutine;
    Coroutine hitRoutine;

    public void DeadEffectsRoutine()
    {

        IEnumerator deadMsg()
        {
            AudioManager.instance.PlayDeathSound();

            SetMenuActive(DeadTXT);
            yield return new WaitForSeconds(1.5f);
            SetMenuOff(DeadTXT);
            deathRoutine = null;
        }

        if(deathRoutine == null)
            deathRoutine = StartCoroutine(deadMsg());

    }
    public void HitEffectsRoutine()
    {
        IEnumerator hitMsg()
        {
            AudioManager.instance.PlayHitSound();
            SetMenuActive(HitTXT);
            yield return new WaitForSeconds(1f);
            SetMenuOff(HitTXT);
            hitRoutine = null;

        }

        if (hitRoutine == null)
            hitRoutine = StartCoroutine(hitMsg());
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

        SetMenuActive(WinPanel);

        AudioManager.instance.PlayWinSound();

        Debug.Log("ORGAN IS INFECTED!!!");

        yield return new WaitForSeconds(1f);

        Debug.Log("YOU WIN!!!");

        yield return new WaitForSeconds(1f);

        SetMenuOff(WinPanel);
        SetMenuOff(gameUI);
        SetMenuActive(mainMenu);

        SceneManager.LoadScene(0);

    }
}
