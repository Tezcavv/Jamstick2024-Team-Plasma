using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.TextCore.Text;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public TMP_Text gameTitle;
    public TMP_Text infectedCellsText;

    private void Awake()
    {
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
        infectedCellsText.text = $"infected cells\n{UI_Manager.instance.infectedCells.ToString()}";
    }
}

