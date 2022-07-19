﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public enum Rooms{CHURCH, STUDIES, BOOKBINDERS, PRINTING_PRESS, LIBRARY, GARDENS, PONDS, RELIQUARY, CRYPTS}


public class Room : MonoBehaviour
{
    public bool unlocked;
    public Rooms roomType;

    public UIObject[] ui;
    public Transform visualRoot;

    public virtual void Start(){
        Main.manager.SetupUnlock(this);
    }

    public void Lock(bool b){
        Show(!b);
    }

    public void Unlock(){
        Show(true);
        Debug.Log(roomType + " unlocked");
        PlayerPrefs.SetInt(roomType.ToString(), 0);
    }

    public void Show(bool b){
        
        visualRoot.gameObject.SetActive(b);

        if(b){
            StartCoroutine(Reveal());
        }
    }

    public virtual IEnumerator Reveal(){
        foreach(UIObject o in visualRoot.GetComponentsInChildren<UIObject>()){
            yield return StartCoroutine(o.Reveal(1f));
        }

        foreach(UIObject o in ui){
            yield return StartCoroutine(o.Reveal(0.5f));
        }
    }
}
