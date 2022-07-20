using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSequence : Sequence
{
    public SpriteObject background;
    public SpriteObject star;
    public TextObject title;
    public ResourceCounter names;
    public GameObject prayerButton;

    public override IEnumerator SequenceBody(){

        yield return Wait(UIManager.i.year.Reveal(1));
        
        yield return Wait(Pause(0.5f));

        yield return Wait(star.Reveal(2));
        yield return Wait(background.Reveal(1));

        yield return Wait(Pause(0.5f));

        yield return Wait(UIManager.i.booksTotal.Reveal(1));

        yield return Wait(Pause(0.25f));

        yield return Wait(UIManager.i.currentPage.Reveal(1));
        
        yield return Wait(Pause(0.25f));

        yield return Wait(title.Reveal(1));


        //these should be written to the script. 
        
        UIManager.i.AddToFeed("Sitting in the study late one night a monk looked out to see a new star and began to pray");
 
        
        yield return Wait(Pause(2.5f));

        prayerButton.SetActive(true);

        while(Resources.GetResource(ResourceType.names).amount < 1 && !skipped){
            yield return null; 
        }
    

        yield return StartCoroutine(names.Reveal(1));
        
        UIManager.i.AddToFeed("As he prayed he began to see God's manifold names");

         
        while(Resources.GetResource(ResourceType.names).amount < 10 && !skipped){
            yield return null;
        }

        
    }
}
