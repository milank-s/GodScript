using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : TextObject
{

    public void Awake(){
    }
    
    public override void SetText(string t){
        float amount = float.Parse(t);
        text.text = amount.ToString("F0");
    }
}
