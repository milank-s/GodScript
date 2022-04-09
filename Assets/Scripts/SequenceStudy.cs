using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceStudy : Sequence
{
    
    public override IEnumerator SequenceBody(){

        UIManager.i.AddToFeed("Writing in the study late one night");
        UIManager.i.AddToFeed("a monk looked out to see a new start");
        UIManager.i.AddToFeed("and began to compose new praise of God");

        while(UIManager.i.feed.typing || Input.GetKeyDown(KeyCode.Space)){
            yield return null;
        }

        Main.manager.UnlockRoom(Rooms.STUDIES);

        yield return null;
    }
}
