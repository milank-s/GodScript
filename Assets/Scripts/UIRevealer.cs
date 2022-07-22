using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRevealer : UIObject
{
    public List<UIObject> children;

    public override IEnumerator Reveal(float time){

        foreach(UIObject o in children){
            o.Enable();
        }

        yield break;
    }
}
