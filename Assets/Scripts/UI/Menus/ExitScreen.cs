using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitScreen : MonoBehaviour
{
    [SerializeField] Button yesButton;
    [SerializeField] Button noButton;

    public static event Action onConfirmExit;
    public static event Action onReturnGame;

    // Start is called before the first frame update
    void Start()
    {
        yesButton.onClick.AddListener(YesButtonDown);
        noButton.onClick.AddListener(NoButtonDown);
    }

    void YesButtonDown(){
        onConfirmExit?.Invoke();
        gameObject.SetActive(false);
        SoundManager.instance.PlaySound("ButtonSFX");
    }

    void NoButtonDown(){
        onReturnGame?.Invoke();
        gameObject.SetActive(false);
        SoundManager.instance.PlaySound("ButtonSFX");
    }
}
