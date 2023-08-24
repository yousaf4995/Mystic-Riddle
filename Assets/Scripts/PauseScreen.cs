using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    [Header("Pause")]
    public GameObject pausePanel;
    public UIButton resumebtn;
    public UIButton saveGameButton;
    public UIButton replayButton;

    [Space]
    public InfoTexts gamePauseInfoTxts;
    GameManager GameController
    {
        get
        {
            var gc = GameManager.Instance;

            if (!gc || gc == null)
                gc = FindAnyObjectByType<GameManager>();

            return gc;

        }
    }

    void Start()
    {

    }

    public void Initialized()
    {
        resumebtn.onClick.RemoveListener(ResumeGame);
        resumebtn.onClick.AddListener(ResumeGame);

        saveGameButton.onClick.RemoveListener(SaveGame);
        saveGameButton.onClick.AddListener(SaveGame);

        replayButton.onClick.RemoveListener(RePlayGame);
        replayButton.onClick.AddListener(RePlayGame);
    }

    public void DisplayPauseScreen()
    {
        pausePanel.SetActive(true);
        PopulateProgressionDta();
    }

    void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        GameController.GamePlayTimer.PauseGameTimer(false);
    }

    void RePlayGame()
    {
        GameController.ProgressionController.CardData = null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Time.timeScale = 1;
    }
    //save game
    void SaveGame()
    {
        GameController.GamePlayController.SaveGame(
            () =>
            {
                Debug.Log("Data Saved Successfuly");
                GameController.Toast.ShowToast("Game Saved Successfuly");
                SoundManager.Instance.PlayCorrectSound();
            });
    }
    void PopulateProgressionDta()
    {

        gamePauseInfoTxts.correctCardsInfoTxt.text =
                GameController.ProgressionController.CardData.correctCardsPlayed
            + " / " +
                  GameController.ProgressionController.CardData.maxCardToPlay;

        gamePauseInfoTxts.attemptsCardsInfoTxt.text = GameController.ProgressionController.CardData.attemptsCounter.ToString();

    }
}
