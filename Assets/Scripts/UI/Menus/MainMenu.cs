using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button optionsButton;
    

    public static event Action onPlayGame;

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(OnPlayButtonDown);
        optionsButton.onClick.AddListener(OnOptionsButtonDown);
    }

    void OnPlayButtonDown(){
        onPlayGame?.Invoke();
        // SoundManager.instance.PlaySound("ButtonSFX");
    }

    void OnOptionsButtonDown(){
        SoundManager.instance.PlaySound("ButtonSFX");
    }

}
