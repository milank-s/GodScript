using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobUI : MonoBehaviour
{
    public Profession profession;
    public Counter counter;

    public void TryAddWorker(int i){
        //call some function on monastery manager to see if its valid
        MonasteryManager.i.AssignWorker(profession, i);
    }
}
