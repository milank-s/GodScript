using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCounter : Counter
{
    public ResourceType resourceType;
    Resource r;
    public override void Awake(){
        //title.SetText(resourceType.ToString());
        r = Resources.GetResource(resourceType);
        r.OnWholeNumberChanged += SetAmount;

        base.Awake();
    }

    public void SetResourceAmount(){
        SetAmount((int)r.amount);
    }
}
