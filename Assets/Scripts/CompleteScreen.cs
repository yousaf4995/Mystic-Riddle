using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteScreen : MonoBehaviour
{
    [Header("Pause")]
    public GameObject completePanel;
    public UIButton playGameBtn;

    [Space]
    public InfoTexts gameCompleteInfoTxts;
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
        playGameBtn.onClick.RemoveListener(PlayGameBtnClick);
        playGameBtn.onClick.AddListener(PlayGameBtnClick);

    }
    // Pause Region
    void PlayGameBtnClick()
    {
         completePanel.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void DisplayCompleteScreen()
    {
        completePanel.SetActive(true);
        PopulateProgressionDta();

    }

    void PopulateProgressionDta()
    {
      
    gameCompleteInfoTxts.correctCardsInfoTxt.text = 
            GameController.ProgressionController.CorrectCardsScore
        +" / " +
              GameController.ProgressionController.MaxCardToPlay;

        gameCompleteInfoTxts.inCorrectCardsInfoTxt.text =
          GameController.ProgressionController.inCorrectCardsScore
      + " / " +
            GameController.ProgressionController.MaxCardToPlay;

    }
}
