using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseLevel : MonoBehaviour
{
    // Holds data on a level
    Shapes answer;

    int level;
    // int shapeAmount;
    int[] shapeRatio;
    
    float currentTime;
    float timeIncrease;


    protected void NextLevel(){
        level++;
    }

    protected virtual void LevelSetup(){}

    

}
