using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleHeart : MonoBehaviour
{
    public Sprite fullHeart, emptyHeart;
    Image heartImage;

    private void Awake(){
        heartImage = GetComponent<Image>();
    }
 
    public void SetHeartImage(HeartState _state){
        if(_state == HeartState.Empty){
            heartImage.sprite = emptyHeart;
        }
        else if(_state == HeartState.Full){
            heartImage.sprite = fullHeart;
        }
    }
}

public enum HeartState{
    Empty = 0,
    Full = 1
}
