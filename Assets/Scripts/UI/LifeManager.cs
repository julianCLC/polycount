using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public static event Action onZeroHearts;
    public SingleHeart[] heartPrefabs;
    int hearts = 3;

    void OnEnable(){
        LevelHandler.onLevelStart += InitializeHearts;
        LevelHandler.onWrongAnswer += RemoveLife;
        
    }

    void OnDisable(){
        LevelHandler.onLevelStart -= InitializeHearts;
        LevelHandler.onWrongAnswer -= RemoveLife;
        
    }

    public void InitializeHearts(){
        foreach(SingleHeart heart in heartPrefabs){
            heart.SetHeartImage(HeartState.Full);
            hearts = 3;
        }
    }

    public void RemoveLife(){
        if(hearts > 0){ hearts--; }
        heartPrefabs[hearts].SetHeartImage(HeartState.Empty);
        
        if(hearts <= 0){
            onZeroHearts?.Invoke();
        }
    }
}
