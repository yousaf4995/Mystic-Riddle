using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    public GameObject startPanel;
    [Space]
    public UIButton easyButton;
    public UIButton mediumButton;
    public UIButton hardButton;
    [Space]
    public Slider cardSizeSpawner;
    public TMPro.TMP_Text cardSpawnInfoTxt;
    public UIButton plaGameButton;

    GameController GameController
    {
        get
        {
            var gc = GameController.Instance;

            if (!gc || gc == null)
                gc = FindAnyObjectByType<GameController>();

            return gc;

        }
    }

    void Start()
    {
        
    }

    public void DisplayStartScreen(bool enable)
    {
        startPanel.SetActive(enable);
    }


    public void Initialized()
    {
        easyButton.onClick.RemoveListener(EasyGameType);
        easyButton.onClick.AddListener(EasyGameType);

        mediumButton.onClick.RemoveListener(MediumGameType);
        mediumButton.onClick.AddListener(MediumGameType);

        hardButton.onClick.RemoveListener(HardGameType);
        hardButton.onClick.AddListener(HardGameType);

        cardSizeSpawner.onValueChanged.AddListener(OnCardSpawnChnage);

        plaGameButton.onClick.RemoveListener(StartPlayGame);
        plaGameButton.onClick.AddListener(StartPlayGame);
       
        DisplayStartScreen(true);
    }

    private void EasyGameType()
    {
        GameType(2, 2);
        StartPlayGame();
    }
    private void MediumGameType()
    {
        GameType(3, 2);
        StartPlayGame();
    }
    private void HardGameType()
    {
        GameType(5, 4);
        StartPlayGame();
    }

    private void OnCardSpawnChnage(float slideValue)
    {
        GameType((int)slideValue, (int)slideValue);
        cardSpawnInfoTxt.text = slideValue + "x" + slideValue;
    }

    void GameType(int rows, int colums)
    {
        GameController.GamePlayController.rows = rows;
        GameController.GamePlayController.colums = colums;
    }

    void StartPlayGame()
    {
        DisplayStartScreen(false);
        GameController.GamePlayController.InitializeGame();
    }
}
