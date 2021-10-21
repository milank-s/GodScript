using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextObject : UIObject
{
    
    [SerializeField] protected TextMeshProUGUI text;

    public virtual void SetText(string t){
        text.text = t;
    }

    public override IEnumerator Reveal()
    {
        string t = text.text;
        text.text = "";
        
        for(int i = 0; i < t.Length; i++){
            text.text += t[i];
            yield return Random.Range(0.1f, 0.2f);
        }

        Show(true);
    }
}
