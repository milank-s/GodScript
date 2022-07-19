using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceStudy : Sequence
{

    public GameObject prayerButton;

    public override IEnumerator SequenceBody(){

        yield return new WaitForSeconds(1);
        Main.manager.UnlockRoom(Rooms.STUDIES);

        Debug.Log("study sequence");
        
        prayerButton.SetActive(true);

        if(Main.monks.monks.Count == 0){
           Monk m = Main.monks.CreateMonk();
            JobManager.i.jobs[Profession.writer].TryAssignWorker(1);
        }

        yield return null;
    }
}
