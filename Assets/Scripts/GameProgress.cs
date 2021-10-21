using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{

    public static GameProgress i;
    public float year = 1000;
    public enum Stage{intro}

    
    public void Awake(){
        i = this;
    }


    public void BeginStage(){

    }

}
