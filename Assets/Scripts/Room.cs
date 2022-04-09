using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public enum Rooms{CHURCH, STUDIES, BOOKBINDERS, PRINTING_PRESS, LIBRARY, GARDENS, PONDS, RELIQUARY, CRYPTS}


public class Room : MonoBehaviour
{
    public bool unlocked;
    public Rooms roomType;
    public Transform visualRoot;

    public virtual void Awake(){
        Main.manager.SetupUnlock(this);
    }

    public void Lock(bool b){
        Show(b);
    }

    public void Unlock(){
        Show(true);
        PlayerPrefs.SetInt(roomType.ToString(), 0);
    }

    public void Show(bool b){
        visualRoot.gameObject.SetActive(b);
    }
}
