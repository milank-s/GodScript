using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteObject : UIObject
{
   
   SpriteRenderer r;
   public override void Start(){
       r = GetComponent<SpriteRenderer>();

       if(!visible){
           r.color = new Color(1,1,1,0);
       }
   }

   public override IEnumerator Reveal(float time){
       yield return StartCoroutine(Effects.FadeIn(time, this.r));
   }
}
