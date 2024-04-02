using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{

    public LevelHandler levelHandler;

    public static event Action onEnterGame;
    public static event Action onEnterMainMenu;

    public static GameManager Instance {get; private set;}

    void Awake(){
        if(Instance != null && Instance != this){ Destroy(this); }
        else{ Instance = this; }

        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable(){
        MainMenu.onPlayGame += StartGame;
        LevelHandler.onLevelExit += OnLevelExit;
    }

    void OnDisable(){
        MainMenu.onPlayGame -= StartGame;
        LevelHandler.onLevelExit -= OnLevelExit;
    }

    public void StartGame(){
        onEnterGame?.Invoke();
    }

    void OnLevelExit(){
        // leave game mode
        // go back to menu
        onEnterMainMenu?.Invoke();
    }


}

public enum Shapes {
    Circle,
    Square,
    Triangle
}
