using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.TextCore.Text;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;

    public int infectedCells = 666;

    public bool isPaused = false;

    public GameObject mainMenu;
    public GameObject gameUI;
    public GameObject pauseMenu;

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
    }

    private void Start()
    {
        //infectedCells = GameManager.instance ? GameManager.instance.infectedCells : 0;

        SetMenuActive(mainMenu);
    }

    public void StartGame()
    {
        //Time.timeScale = 1.0f;

        SetMenuOff(mainMenu);
        SetMenuActive(gameUI);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetMenuActive(GameObject _menu)
    {
        _menu.SetActive(true);
    }

    public void SetMenuOff(GameObject _menu)
    {
        _menu.SetActive(false);
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
}
