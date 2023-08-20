using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlayInfoPanel : MonoBehaviour
{
    public TMP_Text correctScoreText;
    public TMP_Text inCorrectScoreText;

    //
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
        SetCorrectCardsMacth(0);
        SetInCorrectCardsMacth(0);
    }

    public void SetCorrectCardsMacth(int score)
    {
        correctScoreText.text = score + " / " + GameController.ProgressionController.MaxCardToPlay;
    }

    public void SetInCorrectCardsMacth(int score)
    {
        inCorrectScoreText.text = score + " / " + GameController.ProgressionController.MaxCardToPlay;
    }
}
