using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUI : MonoBehaviour
{

    public int level;
    [SerializeField] TextMeshProUGUI levelText;

    void OnEnable(){
        LevelHandler.onLevelStart += OnNewLevelStart;
        LevelHandler.onNextLevel += SetLevel;
    }

    void OnDisable(){
        LevelHandler.onLevelStart -= OnNewLevelStart;
        LevelHandler.onNextLevel -= SetLevel;
    }

    void OnNewLevelStart(){
        SetLevel();
    }

    void SetLevel(int levelToSet = 1){
        string _string = "LEVEL\n" + levelToSet;
        levelText.text = _string;
    }
}
