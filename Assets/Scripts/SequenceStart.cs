using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceStart : Sequence
{
    public SpriteObject background;
    public SpriteObject star;
    public TextObject title;
    public ResourceCounter names;
    public UIRevealer prayerButton;

    public override IEnumerator SequenceBody(){

        //all of these things should be codified as an unlock
        //so the sequence can be skipped if the appropriate save data is there
        //without player losing functionality

        yield return Wait(UIManager.i.year.Show());
        
        yield return Wait(Pause(0.5f));

        yield return Wait(star.Show(2));
        yield return Wait(background.Show(1));

        yield return Wait(Pause(0.5f));

        yield return Wait(UIManager.i.booksTotal.Show(1));

        yield return Wait(Pause(0.25f));

        yield return Wait(UIManager.i.currentPage.Show(1));
        
        yield return Wait(Pause(0.25f));

        yield return Wait(title.Show(1));


        UIManager.i.AddToFeed("Sitting in the study late one night a monk looked out to see a new star and began to pray");
 
        
        yield return Wait(Pause(2.5f));

        prayerButton.Enable();

        while(Resources.GetResource(ResourceType.names).amount < 1 && !skipped){
            yield return null; 
        }
    

        yield return StartCoroutine(names.Show(1));
        
        UIManager.i.AddToFeed("As he prayed he began to see God's manifold names");

         
        while(Resources.GetResource(ResourceType.names).amount < 5 && !skipped){
            yield return null;
        }

        
        Main.manager.Unlock(Unlocks.SCRIPT);
        
    }
}
