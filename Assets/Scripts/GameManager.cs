using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    float time;

    void Update()
    {
        GameProgress.i.Step();

        // if(Time.time > time){
        //     MonkManager.i.AddMonk();
        //     time = Time.time + 0.025f;
        // }
        
    }
}
