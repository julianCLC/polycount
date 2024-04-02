using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;


public class RetryScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] Button retryButton;
    [SerializeField] Button exitButton;
    public static event Action onRetry;
    public static event Action onConfirmExit;

    void Start(){
        retryButton.onClick.AddListener(OnRetryDown);
        exitButton.onClick.AddListener(OnExitDown);
    }

    void OnEnable(){
        LevelHandler.onLevelLose += SetHighScore;
    }

    void OnDisable(){
        LevelHandler.onLevelLose -= SetHighScore;
    }

    public void OnRetryDown(){
        gameObject.SetActive(false);
        // SoundManager.instance.PlaySound("ButtonSFX");

        onRetry?.Invoke();
    }

    public void OnExitDown(){
        gameObject.SetActive(false);
        SoundManager.instance.PlaySound("ButtonSFX");

        onConfirmExit?.Invoke();
    }

    public void SetHighScore(int levelToSet){
        string _string = "High Score: " + (levelToSet - 1);
        levelText.text = _string;
    }
}
