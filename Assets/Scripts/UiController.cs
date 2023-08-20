using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiController : MonoBehaviour
{
    public TMP_Text correctScoreText;
    public TMP_Text inCorrectScoreText;

    int currentScore = 0;
    int maxCardsToPlay = 0;
    // Start is called before the first frame update
    void Start()
    {

    }
   public void initialize(GamePlayController gamePlayController)
    {
        correctScoreText.text = "";
    }
    // Update is called once per frame
    void Update()
    {

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
}
