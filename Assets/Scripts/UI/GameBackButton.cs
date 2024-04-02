using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBackButton : MonoBehaviour
{
    [SerializeField] Button backButton;
    public static event Action onBackButton;

    void Awake(){
        backButton = GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        backButton.onClick.AddListener(OnButtonDown);
    }

    void OnButtonDown(){
        SoundManager.instance.PlaySound("ButtonSFX");
        onBackButton?.Invoke();
    }
}
