using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{

    public static GameProgress i;
    public float year = 1000;
    public enum Stage{intro}

    public Stage stage;
    public void Awake(){
        i = this;
    }

    [Header("Sequences")]
    public Sequence startSequence;

    void Start(){
        StartCoroutine(BeginStage());
    }


    void Update(){
        
        UIManager.i.year.SetText(GameProgress.i.year.ToString("F0") + " AD");

    }
    public IEnumerator BeginStage(){
        Stage s = stage;

        yield return StartCoroutine(startSequence.SequenceBody());
    }
    

}
