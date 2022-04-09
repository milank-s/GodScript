using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrayerSequence : Sequence
{
    
    public override IEnumerator SequenceBody(){

        Main.manager.UnlockRoom(Rooms.STUDIES);

        yield return null;
    }
}
