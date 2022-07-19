using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineExtensions;

public class Sequence : MonoBehaviour
{
    protected bool skipped;
    CoroutineController routine;
    public virtual IEnumerator SequenceBody(){
        
        yield return null;
    }
    public IEnumerator PlaySequence(){

        yield return null; 
        
        CoroutineExtensions.StartCoroutineEx(this, SequenceBody(), out routine);

        while(!skipped && routine.state == CoroutineState.Running){
            
            if(Input.GetKeyDown(KeyCode.Space)){
                skipped = true;
            }

            yield return null;
        }
    }

    public IEnumerator Wait(IEnumerator r){
        
        CoroutineController c;
        CoroutineExtensions.StartCoroutineEx(this, r, out c);

        while(!skipped && c.state == CoroutineState.Running){
            
            if(Input.GetKeyDown(KeyCode.Space)){
                yield break;
            }

            yield return null;
        }
    }

    public IEnumerator Pause(float time){
        float t = 0;
        while(t < time){
            
            t += Time.deltaTime;

            if(Input.GetKeyDown(KeyCode.Space)){
                yield break;
            }
            yield return null;
        }
    }
}
