using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Resource
{
    public delegate void NewValueEvent(int a);
    public ResourceType resourceType;
    public float amount;
    float delta;
    float prevAmount;
    public NewValueEvent OnWholeNumberChanged;
    public NewValueEvent OnWholeNumberDelta;

    public Resource(ResourceType r){
        resourceType = r;
        string s = System.Enum.GetName(typeof(ResourceType), r);
        
        if(PlayerPrefs.HasKey(s)){
            amount = PlayerPrefs.GetFloat(s);
        }else{            
            PlayerPrefs.SetFloat(s, amount);
        }
    }

    public void UpdateValue(){
        
        delta = Mathf.Floor(amount) - Mathf.Floor(prevAmount);

        if(OnWholeNumberChanged != null){
            OnWholeNumberChanged.Invoke((int)amount);
        }

        if(OnWholeNumberDelta != null){
            OnWholeNumberDelta.Invoke((int)delta);
        }
    }

    public void Step(){

        if(Mathf.Floor(amount) != Mathf.Floor(prevAmount)){
            UpdateValue();
        }

        prevAmount = amount;
    }

    public void Increment(){
        amount ++;
    }

    public void Decrement(){
        amount --;
    }

    public void AddOutput(float diff){
        amount += diff;
    }

    public void SetOutput(float a){
        amount = a;
    }
}
