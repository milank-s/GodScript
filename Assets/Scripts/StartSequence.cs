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
        
        yield return Wait(Pause(2));

        yield return Wait(star.Reveal(2));
        yield return Wait(background.Reveal(1));

        yield return Wait(Pause(2));

        yield return Wait(UIManager.i.booksTotal.Reveal(1));

        yield return Wait(Pause(2));

        yield return Wait(UIManager.i.currentPage.Reveal(1));
        
        yield return Wait(Pause(2));

        yield return Wait(title.Reveal(1));

        Main.manager.running = true;
    }
}
