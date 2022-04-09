using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextObject : UIObject
{
    
    public TextMeshProUGUI text;
    string textCached;
    public override void Start()
    {
        textCached = text.text;
        base.Start();
        
        if(!visible){
            text.text = "";
        }
    }
    public virtual void SetText(string t){
        text.text = t;
        textCached = t;
    }

    public virtual void SetAmount(int i){
        text.text = i.ToString();
    }

    public override IEnumerator Reveal(float time = 1)
    {   
        
        yield return StartCoroutine(Effects.Type(time, text, textCached));

        Show(true);
    }
}
