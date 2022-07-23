using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceStudy : Sequence
{

    public override IEnumerator SequenceBody(){

        
        yield return Wait(Pause(1f));

        Main.manager.Unlock(Unlocks.STUDIES);
    
        ScriptWriter.i.WriteInsert("The monk dipped his pen in the inkwell and began to transcribe them");
        
        yield return Wait(Pause(2f));

        if(Main.monks.monks.Count == 0){
           Monk m = Main.monks.CreateMonk();
            JobManager.i.jobs[Profession.writer].TryAssignWorker(1);
        }

        yield return null;
    }
}
