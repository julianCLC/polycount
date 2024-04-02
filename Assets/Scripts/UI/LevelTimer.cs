using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{

    Slider slider;
    bool timerActive = false;
    float timer;
    float timerDelta = 1; // rate of change on timer
    public float maxTimer;
    public float additionalTime = 10;

    public static event Action onTimerEnd;

    void Awake(){
        slider = GetComponentInChildren<Slider>();
    }

    void OnEnable(){
        LevelHandler.onLevelStart += Initialize;
        LevelHandler.onLevelLose += OnGameOver;

        LevelHandler.onNextLevel += OnNextLevel;

        LevelHandler.onPauseEnter += OnPauseEnter;
        LevelHandler.onPauseExit += OnPauseExit;
    }

    void OnDisable(){
        LevelHandler.onLevelStart -= Initialize;
        LevelHandler.onLevelLose -= OnGameOver;

        LevelHandler.onNextLevel -= OnNextLevel;
        LevelHandler.onPauseEnter -= OnPauseEnter;
        LevelHandler.onPauseExit -= OnPauseExit;
    }

    // Update is called once per frame
    void Update()
    {
        if(timerActive){
            timer -= timerDelta * Time.deltaTime;
            slider.value = timer/maxTimer;

            if(timer <= 0){
                timer = 0;
                timerActive = false;
                onTimerEnd?.Invoke();
            }
        }
    }

    void Initialize(){

        timer = maxTimer;
        slider.value = 1;

        timerActive = true;
    }

    void OnGameOver(int level){
        // ignore level
        timerActive = false;
    }

    void OnNextLevel(int level){
        timer += additionalTime;
        timer = Mathf.Min(timer, maxTimer);
    }

    public void SetTimerDelta(float newTimerDelta){
        timerDelta = newTimerDelta;
    }

    void OnPauseEnter(){ToggleTimer(false);}
    void OnPauseExit(){ToggleTimer(true);}
    public void ToggleTimer(bool newBool){
        timerActive = newBool;
    }
}
