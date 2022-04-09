using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Resource", order = 1)]
public class ResourceData : ScriptableObject
{

    public ResourceType resourceType;
    public int amount;
    
}
