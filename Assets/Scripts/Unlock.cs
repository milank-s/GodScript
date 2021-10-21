using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public enum Unlocks{CHURCH, STUDIES, BOOKBINDERS, PRINTING_PRESS, LIBRARY, GARDENS, PONDS, RELIQUARY, CRYPTS}


public class Unlock : MonoBehaviour
{

    public bool unlocked;
    public Unlocks unlock;
    public Transform visualRoot;
    public Resource output;


    public virtual void Start(){
        Lock(unlocked);
    }

    public void Lock(bool b){
        Show(b);
    }
    public void Show(bool b){
        visualRoot.gameObject.SetActive(b);
    }

    
}
