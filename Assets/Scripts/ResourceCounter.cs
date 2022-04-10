using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCounter : Counter
{
    public ResourceType resourceType;
    Resource r;
    public void Awake(){
        //title.SetText(resourceType.ToString());
        r = Resources.GetResource(resourceType);
        r.OnWholeNumberChanged += SetAmount;
    }

    public void SetResourceAmount(){
        SetAmount((int)r.amount);
    }
}
