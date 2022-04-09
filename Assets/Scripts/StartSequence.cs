using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSequence : Sequence
{
    public SpriteObject background;
    public SpriteObject star;
    public TextObject title;
    public override IEnumerator SequenceBody(){

        yield return Wait(UIManager.i.year.Reveal(1));
        
        yield return Wait(Pause(0.5f));

        yield return Wait(star.Reveal(2));
        yield return Wait(background.Reveal(1));

        yield return Wait(Pause(0.5f));


        //these should be written to the script. 
        UIManager.i.AddToFeed("Writing in the study late one night");
        UIManager.i.AddToFeed("a monk looked out to see a new star");
        UIManager.i.AddToFeed("and began to compose new praise of God");

        while(UIManager.i.feed.typing && !skipped){
            yield return null;
        }


        yield return Wait(UIManager.i.booksTotal.Reveal(1));

        yield return Wait(Pause(0.25f));

        yield return Wait(UIManager.i.currentPage.Reveal(1));
        
        yield return Wait(Pause(0.25f));

        yield return Wait(title.Reveal(1));

        Main.manager.running = true;
    }
}
