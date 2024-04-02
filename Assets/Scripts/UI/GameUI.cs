using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    [SerializeField] Button backButton;

    // Start is called before the first frame update
    void Start()
    {
        backButton.onClick.AddListener(OnBackButtonDown);
    }

    void OnBackButtonDown(){
        SoundManager.instance.PlaySound("ButtonSFX");
    }
}
