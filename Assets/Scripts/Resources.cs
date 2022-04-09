using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ResourceType{
    names = 0, 
    pages = 1, 
    books = 2
}

public class Resources
{
    public static Dictionary<ResourceType, Resource> resourceRefs;
    
    public Resources(){

        resourceRefs = new Dictionary<ResourceType, Resource>();

        foreach(int i in System.Enum.GetValues(typeof(ResourceType))){
            Resource r = new Resource();
            r.resourceType = (ResourceType)i;
            resourceRefs.Add(r.resourceType, r);
        }
    }

    public static Resource GetResource(ResourceType p){
        
        Resource r;

        if(resourceRefs.TryGetValue(p, out r)){
            return r;
        }

        return null;
    }
}
