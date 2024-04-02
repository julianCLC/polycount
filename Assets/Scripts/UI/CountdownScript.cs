using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownScript : MonoBehaviour
{
    void CountSound1(){
        SoundManager.instance.PlaySound("CountdownSFX1");
    }

    void CountSound2(){
        SoundManager.instance.PlaySound("CountdownSFX2");
    }
}
