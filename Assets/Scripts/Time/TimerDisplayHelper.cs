using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerDisplayHelper : MonoBehaviour
{
    public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        GamePlayTimer.Instance.UnSubscribeTimer(SubscribeTimer);
        GamePlayTimer.Instance.SubscribeTimer(SubscribeTimer);
    }

    //private void OnDisable()
    //{
    //    GamePlayTimer.Instance.UnSubscribeTimer(SubscribeTimer);
    //}

    public void SubscribeTimer(int hour, int mint, int sec)
    {
        // string remainingTimeText =
        //     string.Format("{0:D2}:{1:D2}:{2:D2}", hour.ToString(), mint.ToString(), sec.ToString());  

        string remainingTimeText =
            string.Format("{0:00}:{1:00}:{2:00}", hour, mint, sec);

        text.text = remainingTimeText;
    }
}