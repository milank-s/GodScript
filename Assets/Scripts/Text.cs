using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Text: MonoBehaviour
{
    
    public TextMeshPro text;

    public float lifeTime = 1;
    float alpha = 1;


    public void Update(){

        Color c = text.color;
        c.a = alpha;
        text.color = c;

        alpha -= Time.deltaTime/lifeTime;
        
        if(alpha < 0){
            Destroy(gameObject);
        }
    }
    public virtual void SetText(string t){
       text.text = t;
    }

}
