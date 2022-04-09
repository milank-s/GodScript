using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCounter : Counter
{
    public ResourceType resourceType;
    Resource r;
    public void Awake(){
        title.SetText(resourceType.ToString());
        r = Resources.GetResource(resourceType);
        r.OnWholeNumberChanged += SetAmount;
    }

    public override IEnumerator Reveal(float time = 1){
        
        yield return StartCoroutine(title.Reveal(1));
        yield return new WaitForSeconds(0.25f);
        yield return StartCoroutine(base.Reveal(1));
    }
    public override void SetText(string t){
        float amount = float.Parse(t);
        text.text = amount.ToString("F0");
    }
}
