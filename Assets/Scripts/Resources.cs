using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ResourceType{
    names = 0, 
    pages = 1, 
    books = 2,
    pagesDone = 3,
    wordsDone = 4,
    booksDone = 5
}

public class Resources
{
    public static Dictionary<ResourceType, Resource> resourceRefs;
    
    Resource names;
    Resource pages;
    Resource books;
    public Resources(){

        resourceRefs = new Dictionary<ResourceType, Resource>();

        foreach(int i in System.Enum.GetValues(typeof(ResourceType))){
            Resource r = new Resource((ResourceType)i);
            resourceRefs.Add(r.resourceType, r);

            SetResourceRef(r, (ResourceType)i);
        }
    }

    void SetResourceRef(Resource r, ResourceType i){
        switch(i){
            case ResourceType.names:
            names = r;
            break;

            case ResourceType.pages:
            pages = r;
            break;

            case ResourceType.books:
            books = r;
            break;
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
