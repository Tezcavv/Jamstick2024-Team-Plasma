using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.TextCore.Text;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;

    public TMP_Text infectedCellsText;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        MainMenuInit();
    }

    private void OnEnable()
    {
        MainMenuInit();
    }

    private void MainMenuInit()
    {
        
        infectedCellsText.text = $"TOTAL ORGANS INFECTED - {UI_Manager.instance.InfectedCells}";
    }
}

