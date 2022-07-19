using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceStudy : Sequence
{

    public GameObject prayerButton;
    
    public override IEnumerator SequenceBody(){


        Main.manager.UnlockRoom(Rooms.STUDIES);

        yield return new WaitForSeconds(1);

        prayerButton.SetActive(true);

        yield return null;
    }
}
