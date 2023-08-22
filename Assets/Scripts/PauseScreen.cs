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

        CardDataWrapper wrapper = GameController.Instance.ProgressionController.CardData;
        wrapper.cardDataArray = GameController.GamePlayController.cardsInGamePlay.ToArray();
        if (wrapper.cardDataArray == null || wrapper.cardDataArray.Length < 1)
        {
            print("No data to save ");
            return;

        }

        GameController.ProgressionController.SaveGameData(wrapper, () =>
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
