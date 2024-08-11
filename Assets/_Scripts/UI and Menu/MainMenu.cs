using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.TextCore.Text;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;

    public TMP_Text gameTitle;
    public TMP_Text infectedCellsText;

    private void Awake()
    {
        instance = this;
        gameTitle.text = $"infectous";
    }

    void Start()
    {
        MainMenuInit();
    }

    void Update()
    {

    }

    private void OnEnable()
    {
        MainMenuInit();
    }

    private void MainMenuInit()
    {
        AudioManager.instance.PlayBackgroundMusic();

        infectedCellsText.text = $"infected cells\n{UI_Manager.instance.InfectedCells}";
    }
}

