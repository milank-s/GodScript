using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : MonoBehaviour
{
    
    public virtual IEnumerator SequenceBody(){
        yield return null;
    }
}
