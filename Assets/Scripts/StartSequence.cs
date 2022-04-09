using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSequence : Sequence
{
    public SpriteObject background;
    public SpriteObject star;
    public TextObject title;
    public override IEnumerator SequenceBody(){

        yield return StartCoroutine(UIManager.i.year.Reveal(1));
        
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(star.Reveal(2));
        yield return StartCoroutine(background.Reveal(1));

        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(UIManager.i.booksTotal.Reveal(1));

        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(UIManager.i.currentPage.Reveal(1));
        
        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(title.Reveal(1));

        Main.manager.running = true;

    }
}
