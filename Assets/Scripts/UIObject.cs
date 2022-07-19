using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObject : MonoBehaviour
{
    public bool visible;

   public virtual void Start(){
       Show(visible);
   }
   public virtual void Show(bool b){
       
   }
   
   public virtual void FadeIn(){
        StartCoroutine(Reveal(1));
   }
   
   public virtual IEnumerator Reveal(float time){
       yield return new WaitForSeconds(time);
   }
}
