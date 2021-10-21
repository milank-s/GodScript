using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSequence : Sequence
{
    public SpriteRenderer background;
    public SpriteRenderer star;
    public IEnumerator SequenceBody(){
        yield return UIManager.i.currentPage.Reveal();
        yield return UIManager.i.booksTotal.Reveal();
    }
}
