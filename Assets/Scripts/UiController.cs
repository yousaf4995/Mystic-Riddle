using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiController : MonoBehaviour
{
    public UIButton pausebtn;

    [Header("Scripts")]
    [SerializeField] private PauseScreen pauseScreen;
    [SerializeField] private GamePlayInfoPanel gamePlayInfoPanel;

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
      
    }
    public void initialize()
    {
        pausebtn.onClick.RemoveListener(PauseBtnClick);
        pausebtn.onClick.AddListener(PauseBtnClick);

        pauseScreen.Initialized();
        gamePlayInfoPanel.Initialized();
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
        pauseScreen.DisplayPauseScreen();
        Time.timeScale = 0.000001f;

    }
}
