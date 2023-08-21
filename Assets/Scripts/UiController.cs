using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiController : Singleton<UiController>
{
    public UIButton pausebtn;

    [Header("Scripts")]
    [SerializeField] private PauseScreen pauseScreen;
    [SerializeField] private CompleteScreen completeScreen;
    [SerializeField] private GamePlayInfoPanel gamePlayInfoPanel;
    public PauseScreen PauseScreen { get => pauseScreen; set => pauseScreen = value; }
    public CompleteScreen CompleteScreen { get => completeScreen; set => completeScreen = value; }
    public GamePlayInfoPanel GamePlayInfoPanel { get => gamePlayInfoPanel; set => gamePlayInfoPanel = value; }


    // private 
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

    // Start is called before the first frame update
    void Start()
    {
      //  initialize();
    }
    public void initialize()
    {
        pausebtn.onClick.RemoveListener(PauseBtnClick);
        pausebtn.onClick.AddListener(PauseBtnClick);

        PauseScreen.Initialized();
        GamePlayInfoPanel.Initialized();
        completeScreen.Initialized();
    }

    public void UpdateScore(bool isCorrectScore)
    {
        if (isCorrectScore)
            CorrectScore();
        else
            InCorrectScore();
    }

    void CorrectScore()
    {

    }
    void InCorrectScore()
    {

    }

    void PauseBtnClick()
    {
        PauseScreen.DisplayPauseScreen();
        Time.timeScale = 0.000001f;

    }
}
