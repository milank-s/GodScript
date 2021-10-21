using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObject : MonoBehaviour
{
    public bool visible;
   public Transform visualRoot;

   public virtual void Start(){
       Show(visible);
   }
   public void Show(bool b){
       visualRoot.gameObject.SetActive(b);
   }
   
   
   public virtual IEnumerator Reveal(){
       yield return null;
   }
}
