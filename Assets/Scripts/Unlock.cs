using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public enum Unlocks{CHURCH, STUDIES, BOOKBINDERS, PRINTING_PRESS, LIBRARY, GARDENS, PONDS, RELIQUARY, CRYPTS, SCRIPT}


public class Unlock : MonoBehaviour
{
    public bool unlocked;
    public Unlocks unlockType;
    public UIObject[] visuals;

    public virtual void Start(){

        Main.manager.SetupUnlock(this);
    }

    public void Lock(bool b){
        Show(!b);
    }

    public void Enable(){
        Show(true);
        
        PlayerPrefs.SetInt(unlockType.ToString(), 0);
    }

    public void Show(bool b){
        
        if(b){
            StartCoroutine(Reveal());
        }
    }

    public virtual IEnumerator Reveal(){
        foreach(UIObject o in visuals){
            yield return StartCoroutine(o.Show());
        }

    }
}
