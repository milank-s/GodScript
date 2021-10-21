using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    public static GameProgress i;
    public float year = 1000;
    public enum Stage{intro}

    public bool running = false;

    public Stage stage;
    public void Awake(){
        i = this;
    }

    [Header("Sequences")]
    public Sequence startSequence;

    void Start(){
        StartCoroutine(GameLoop());
    }

    public void Step(){
        if(running){
            UIManager.i.year.SetText(GameProgress.i.year.ToString("F0") + " AD");
            MonkManager.i.Step();
            MonasteryManager.i.Step();
        }
    }
    public IEnumerator GameLoop(){
        Stage s = stage;

        yield return StartCoroutine(UIManager.i.year.Reveal(1));

        yield return StartCoroutine(startSequence.SequenceBody());

        running = true;
    }
    

}
