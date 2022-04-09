using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Resource
{
    public delegate void NewValueEvent(int a);
    public ResourceType resourceType;
    public float amount;

    public NewValueEvent OnWholeNumberChanged;

    public Resource(ResourceType r){
        resourceType = r;
        if(PlayerPrefs.HasKey(r.ToString())){
            amount = PlayerPrefs.GetFloat(r.ToString());
        }else{
            PlayerPrefs.SetFloat(r.ToString(), amount);
        }
    }

    public void UpdateValue(){
        if(OnWholeNumberChanged != null){
            OnWholeNumberChanged.Invoke((int)amount);
        }
    }

    public void AddOutput(float diff){
       
        float prevAmount = Mathf.Floor(amount);
        float newAmount = prevAmount + diff;
        
        if(Mathf.Floor(newAmount) > prevAmount){
            UpdateValue();
        }

        amount = newAmount;
    }

    public void SetOutput(float a){
       
        amount = a;
        
        UpdateValue();
        
    }
}
