using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageObject : UIObject
{
    public Image image;

   public override void Awake(){
       base.Awake();
       image.enabled = false;
   }
   
   public override IEnumerator Reveal(float time){
       yield return base.Reveal(time);
       
       image.enabled = true;
   }
}
