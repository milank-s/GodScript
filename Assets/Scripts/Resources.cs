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
    booksDone = 5,
    pagesWritten = 6, 
    writers = 7
}

public class Resources
{
    public static Dictionary<ResourceType, Resource> resourceRefs;
    
    public static Resource names;
    public static Resource writers;
    public static Resource pages;
    public static Resource books;
    public Resources(){

        resourceRefs = new Dictionary<ResourceType, Resource>();

        foreach(int i in System.Enum.GetValues(typeof(ResourceType))){
            Resource r = new Resource((ResourceType)i);
            resourceRefs.Add(r.resourceType, r);

            SetResourceRef(r, (ResourceType)i);
        }
    }

      public void Step(){
        foreach(int i in System.Enum.GetValues(typeof(ResourceType))){
            
            Resource r;
            if(resourceRefs.TryGetValue((ResourceType) i, out r)){
                r.Step();
            }
        }
    }

    public void Save(){
        foreach(int i in System.Enum.GetValues(typeof(ResourceType))){
            
            Resource r;
            if(resourceRefs.TryGetValue((ResourceType) i, out r)){
                PlayerPrefs.SetFloat(r.ToString(), r.amount);
            }
        }
    }

    public static void Increment(ResourceType resourceType){
        Resource r;

        if(resourceRefs.TryGetValue(resourceType, out r)){
            r.Increment();
        }
    }

    public static void Decrement(ResourceType resourceType){
        Resource r;

        if(resourceRefs.TryGetValue(resourceType, out r)){
            r.Decrement();
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
            
            case ResourceType.writers:
            writers = r;
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
