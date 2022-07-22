using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceStudy : Sequence
{

    public override IEnumerator SequenceBody(){

        yield return new WaitForSeconds(1);

        Main.manager.Unlock(Unlocks.STUDIES);
    
        UIManager.i.AddToFeed("The monk dipped his pen in the inkwell and began to transcribe them");
        

        if(Main.monks.monks.Count == 0){
           Monk m = Main.monks.CreateMonk();
            JobManager.i.jobs[Profession.writer].TryAssignWorker(1);
        }

        yield return null;
    }
}
