using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public enum Unlocks{STUDIES, BOOKBINDERS, PRINTING_PRESS, LIBRARY, GARDENS, PONDS, RELIQUARY, CRYPTS}


public class Unlock : MonoBehaviour
{

    bool unlocked;
    public Unlocks unlock;
    public Transform visualRoot;
    public Resource output;



    
}
