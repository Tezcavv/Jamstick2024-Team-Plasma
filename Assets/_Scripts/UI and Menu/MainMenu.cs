using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.TextCore.Text;

public class MainMenu : MonoBehaviour
{

    private TextMeshPro gameTitle;
    private TextMeshPro infectedCellsText;

    private int infectedCells = 0;

    void Start()
    {
        gameTitle.text = $"GAME TITLE";

        MainMenuInit();
    }

    private void MainMenuInit()
    {
        //infectedCells = GameManager.Instance ? GameManager.Instance.totalInfectedCells : 0;
        infectedCellsText.text = $"INFECTED CELLS = {infectedCells.ToString()}";
    }

    void Update()
    {

    }
    private void OnEnable()
    {
        MainMenuInit();

    }
}

