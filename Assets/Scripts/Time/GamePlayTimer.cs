using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayTimer : Singleton<GamePlayTimer>
{
    // in seonds
    public float gameplayTime = 180;

    //  [Space(5)] public TMP_Text timerText;
    private float updateInterval = 1;
    private float lastUpdatedTime = 0;
    public float elapedTime = 0;

    public Action onTimerComplete;
    public Action onTimerStarted;
    public Action<int, int, int> OnTimerTick;
    public bool startTimer = false;

    public Action<float> onTimeAdded;
    public Action<float> onTimeDeducted;

    public bool forwardTimer;

    void Start()
    {
        Init();
    }

    void Init()
    {
        elapedTime = gameplayTime;
        lastUpdatedTime = elapedTime;

        startTimer = true;
        onTimerStarted?.Invoke();
    }

    private void Update()
    {
        if (!startTimer)
            return;


        if (forwardTimer)
        {
            ForwardTimer();
        }
        else
        {
            BackwardTimer();
        }
    }

    void ForwardTimer()
    {
        elapedTime += Time.deltaTime;

        //   for optimization purpose , call after 1 second not by every frame

        if (elapedTime < (lastUpdatedTime + updateInterval))
        {
            return;
        }

        lastUpdatedTime = elapedTime;

        if (elapedTime < gameplayTime)
        {
            OnTimerTick?.Invoke(GetLeftHours(elapedTime), GetLeftMinutes(elapedTime),
                GetLeftSeconds(elapedTime));

            // timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", GetLeftHours(elapedTime), GetLeftMinutes(elapedTime),
            //     GetLeftSeconds(elapedTime));
        }
        else
        {
            elapedTime = 0;
            // timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", GetLeftHours(elapedTime), GetLeftMinutes(elapedTime),
            //     GetLeftSeconds(elapedTime));
            startTimer = false;
            onTimerComplete?.Invoke();
        }
    }

    void BackwardTimer()
    {
        elapedTime -= Time.deltaTime;

        //   for optimization purpose , call after 1 second not by every frame

        if (elapedTime > (lastUpdatedTime - updateInterval))
        {
            return;
        }

        lastUpdatedTime = elapedTime;

        if (elapedTime > 0)
        {
            OnTimerTick?.Invoke(GetLeftHours(elapedTime), GetLeftMinutes(elapedTime),
                GetLeftSeconds(elapedTime));

            // timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", GetLeftHours(elapedTime), GetLeftMinutes(elapedTime),
            //     GetLeftSeconds(elapedTime));
        }
        else
        {
            elapedTime = 0;
            // timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", GetLeftHours(elapedTime), GetLeftMinutes(elapedTime),
            //     GetLeftSeconds(elapedTime));
            startTimer = false;
            onTimerComplete?.Invoke();
        }
    }
    public bool IsTimeCompleted()
    {
        return (elapedTime <= 0);
    }

    public void RestartTimer() // You ca n Use - negative mark if you need to deduct timer 
    {
        Init();
    }

    public void AddTime(float timeToAdd) // You ca n Use - negative mark if you need to deduct timer 
    {
        elapedTime += timeToAdd;
        onTimeAdded?.Invoke(timeToAdd);
    }

    public void DeductTime(float timeToMinus)
    {
        elapedTime -= timeToMinus;
        onTimeDeducted?.Invoke(timeToMinus);
    }

    public void PauseGameTimer(bool pauseTimer = false)
    {
        startTimer = !pauseTimer;
    }

    public int GetRemainingSeconds()
    {
        //  return (int)(totalTime-elapedTime);
        return (int)(elapedTime);
    }

    public void SubscribeTimer(Action<int, int, int> onTimerTick)
    {
        OnTimerTick += onTimerTick;
    }

    public void UnSubscribeTimer(Action<int, int, int> onTimerTick)
    {
        OnTimerTick -= onTimerTick;
    }

    public void SubscribeTimerComplete(Action onTimerComp)
    {
        onTimerComplete += onTimerComp;
    }

    public void UnSubscribeTimerComplete(Action onTimerComp)
    {
        onTimerComplete -= onTimerComp;
    }

    public void SubscribeTimerStarted(Action _onTimerStarted)
    {
        onTimerStarted += _onTimerStarted;
    }

    public void UnSubscribeTimerStarted(Action _onTimerStarted)
    {
        onTimerStarted -= _onTimerStarted;
    }

    private int GetLeftHours(float currentTime)
    {
        return Mathf.FloorToInt((float)currentTime / 3600f);
    }

    private int GetLeftMinutes(float currentTime)
    {
        return Mathf.FloorToInt(((float)currentTime % 3600f) / 60f);
    }

    private int GetLeftSeconds(float currentTime)
    {
        return (int)currentTime % 60;
    }
}

