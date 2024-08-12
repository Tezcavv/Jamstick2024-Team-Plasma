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

    [SerializeField] private int MaxInfectionLevel = 100;

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

    public GameObject HitVFX;

    public AudioSource UiSource;

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
        pauseAction.started += EscPauseGame;
    }

    #region [BUTTONS]

    public void StartGame()
    {
        AudioManager.instance.PlayUiSound();
        SceneManager.LoadScene(1);

        SetMenuOff(mainMenu);
        SetMenuActive(gameUI);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitGame()
    {
        AudioManager.instance.PlayUiSound();

        Application.Quit();
    }

    public void OpenOptions()
    {
        AudioManager.instance.PlayUiSound();

        SetMenuActive(optionsMenu);
    }

    public void CloseOptions()
    {
        AudioManager.instance.PlayUiSound();

        SetMenuOff(optionsMenu);
    }

    public void ResumeGame()
    {
        AudioManager.instance.PlayUiSound();

        TogglePause();
        onResume?.Invoke();
    }

    public void BackToMain()
    {
        AudioManager.instance.PlayUiSound();

        TogglePause();
        SetMenuOff(gameUI);
        SetMenuActive(mainMenu);

        SceneManager.LoadScene(0);

        //Time.timeScale = 0f;
    }

    #endregion

    public void EscPauseGame(InputAction.CallbackContext context)
    {
        if (!mainMenu.activeInHierarchy)
        {
            TogglePause();
            onPause?.Invoke();
        }
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
    public void HitEffectsRoutine(Vector3 pos)
    {
        IEnumerator hitMsg()
        {
            var effect = Instantiate(HitVFX, pos, Quaternion.Euler(0f, 0f , 0f));

            AudioManager.instance.PlayHitSound();
            SetMenuActive(HitTXT);
            yield return new WaitForSeconds(1f);
            SetMenuOff(HitTXT);

            Destroy(effect.gameObject);

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
        if (_infectionLevel >= MaxInfectionLevel)
        {
            StartCoroutine(WinningRoutine());
        }
        else
        {
            FindFirstObjectByType<PlayerRespawner>().SpawnPlayer();
        }
    }

    public IEnumerator WinningRoutine()
    {
        infectedCells++;

        SaveScoreToPref();

        yield return new WaitForSeconds(1f);

        SetMenuActive(WinPanel);

        AudioManager.instance.PlayWinSound();

        yield return new WaitForSeconds(3f);

        FindFirstObjectByType<Cuore>().HeartInfectionLevel = 0;

        SetMenuOff(WinPanel);
        SetMenuOff(gameUI);
        SetMenuActive(mainMenu);

        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene(0);

    }
}
