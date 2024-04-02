using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class Choice : MonoBehaviour
{
    [SerializeField] Shapes shape;
    [SerializeField] Button button;
    private Image _image;
    
    public static event Action<Shapes> onChoiceMade; 
    
    void Awake(){
        _image = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    void Start(){
        button.onClick.AddListener(MakeChoice);
        LoadSprite();
    }

    public void UpdateChoice(Shapes shapeToSet, string newHexCode = null){
        // set choice
        shape = shapeToSet;

        // set image
        LoadSprite(newHexCode);
    }

    public void LoadSprite(string newHexCode = null){
        _image.sprite = ResourceLoader.GetSprite(shape);
    }

    public void MakeChoice(){
        onChoiceMade?.Invoke(shape);
    }
}
