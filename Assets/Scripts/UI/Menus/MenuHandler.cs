using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] GameObject MainMenuUI;
    [SerializeField] GameObject background;

    void OnEnable(){
        GameManager.onEnterMainMenu += OnEnterMainMenu;
        GameManager.onEnterGame += OnExitMenu;
        
    }

    void OnDisable(){
        GameManager.onEnterMainMenu -= OnEnterMainMenu;
        GameManager.onEnterGame -= OnExitMenu;
    }

    void OnEnterMainMenu(){
        MainMenuUI.SetActive(true);
        background.SetActive(true);
        
    }
    
    void OnExitMenu(){
        MainMenuUI.SetActive(false);
        background.SetActive(false);
    }
}
