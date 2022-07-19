using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceStudy : Sequence
{

    public GameObject prayerButton;

    public override IEnumerator SequenceBody(){

        Main.manager.UnlockRoom(Rooms.STUDIES);

        prayerButton.SetActive(true);

        if(Main.monks.monks.Count == 0){
           Monk m = Main.monks.CreateMonk();
            JobManager.i.AssignJob(m, Profession.writer);
        }

        yield return null;
    }
}
