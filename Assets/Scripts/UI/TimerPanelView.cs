using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimerPanelView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float totalGameTime;
    private float _actualTime;
    private bool _timerRunning;
    private Coroutine _timerCoroutine;
    public void Start()
    {
        GameManager.inst.OnRestartGameplay += InitializeValues;
        GameManager.inst.OnResumedGameplay += StartTimer;
        GameManager.inst.OnPausedGameplay += StopTimer;
        InitializeValues();
    }
    private void InitializeValues()
    {
        _actualTime = 0;
        StartTimer();
    }
    public void StartTimer()
    {
        if(_timerCoroutine == null)
        {
            _timerRunning = true;
            _timerCoroutine = StartCoroutine(DoTimer());
        }
    }
    private IEnumerator DoTimer()
    {
        while(_timerRunning)
        {
            yield return new WaitForSeconds(1f);

            _actualTime += 1f;
            TimeSpan time = TimeSpan.FromSeconds(_actualTime);
            if(_actualTime >= totalGameTime)
            {
                OnTimeIsOver();
            }

            SetTimerText(string.Format("{0}:{1}",
                            time.Minutes,
                            time.Seconds ));
            yield return null;
        }
    }
    private void OnTimeIsOver()
    {
        GameManager.inst.EndGame(true);
    }
    private void StopTimer()
    {
        StopAllCoroutines();
        _timerCoroutine = null;
        _timerRunning = false;
    }
    private void SetTimerText(string text)
    {
        timerText.text = text;
    }
    private void OnDestroy()
    {
        GameManager.inst.OnRestartGameplay -= InitializeValues;
        GameManager.inst.OnResumedGameplay -= StartTimer;
        GameManager.inst.OnPausedGameplay -= StopTimer;
    }
}
