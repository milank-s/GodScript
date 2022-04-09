﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : TextObject
{
    public TextObject title;
    
    public override void Start(){
        base.Start();
    }
    public override IEnumerator Reveal(float time = 1){
        
        yield return StartCoroutine(title.Reveal(1));
        yield return new WaitForSeconds(0.25f);
        yield return StartCoroutine(base.Reveal(1));
    }
    public override void SetText(string t){
        float amount = float.Parse(t);
        text.text = amount.ToString("F0");
    }
}
