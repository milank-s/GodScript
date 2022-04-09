using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Resource
{
    public delegate void NewValueEvent(float a);
    public ResourceType resourceType;
    public float amount;

    public NewValueEvent OnWholeNumberChanged;

    public void Increment(){
        if(OnWholeNumberChanged != null){
            OnWholeNumberChanged.Invoke(amount);
        }
    }

    public void AddOutput(float diff){
       
        float prevAmount = Mathf.Floor(amount);
        float newAmount = prevAmount + diff;
        if(Mathf.Floor(amount) > prevAmount){
            Increment();
        }
    }
}
