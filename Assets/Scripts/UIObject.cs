using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObject : MonoBehaviour
{
    public bool visible;

   public virtual void Awake(){
       StartCoroutine(Show(1, visible));
   }

   public void Enable(){
        StartCoroutine(Show(1, true));
   }
   public virtual IEnumerator Show(float time = 1, bool b = true){

        bool isVisible = visible;
        visible = b;

        if(b && !isVisible){
            yield return StartCoroutine(Reveal(time));
        }else{
            yield break;
        }
   }

   
   public virtual IEnumerator Reveal(float time){
        yield return null;
   }
}
