using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CountSound1(){
        SoundManager.instance.PlaySound("CountdownSFX1");
    }

    void CountSound2(){
        SoundManager.instance.PlaySound("CountdownSFX2");
    }
}
