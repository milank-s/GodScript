using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextObject : UIObject
{
    
    public TextMeshProUGUI text;
    protected string textCached;
    public override void Start()
    {
        textCached = text.text;
        base.Start();
        
    }

    public override void Show(bool b)
    {
        base.Show(b);

        if(!b){
            text.text = "";
        }
    }
    public virtual void SetText(string t){
        if(visible){
            text.text = t;
        }else{
            textCached = t;
        }
    }

    public virtual void SetAmount(int i){
        if(visible){
            text.text = i.ToString();
        }else{
            textCached = i.ToString();  
        }
    }

    public override IEnumerator Reveal(float time = 1)
    {   
        yield return StartCoroutine(Effects.Type(time, text, textCached));
        Show(true);
    }
}
