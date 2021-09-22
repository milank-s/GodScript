using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Resource
{
    public float amount;

    public UnityEvent OnWholeNumberChanged;

    public void Increment(){
        amount++;
        OnWholeNumberChanged.Invoke();
    }

    public void ChangeValue(float diff){

    }
}
