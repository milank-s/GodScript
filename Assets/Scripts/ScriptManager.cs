 
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScriptManager : MonoBehaviour
{
    public static ScriptManager i;
    public UnityEvent OnWordCompleted;
    public UnityEvent OnPageCompleted;
    public UnityEvent OnBookCompleted;
    float lettersLastFrame;
    
    Job prayers => JobManager.i.jobs[Profession.prayer].job;
    Job writers => JobManager.i.jobs[Profession.writer].job;
    Job paperMakers => JobManager.i.jobs[Profession.papermaker].job;
    Job bookBinders => JobManager.i.jobs[Profession.bookbinder].job;

    [Header("floats")]

    //replace this with resources
    public float names;
    public float letters;
    public float words;
    public float pages = 10;
    public float pagesDone;
    public float books;

    [Header("ints")]

    float lettersToWrite;
    int pagesCompleted;
    

    public void Awake(){
        i = this;
        
    }

    public void FinishPage(){
        pages --;
        letters = 0;
        lettersLastFrame = 0;

        OnPageCompleted.Invoke();
        Resources.pages.Decrement();
        Resources.Increment(ResourceType.pagesDone);
        Resources.Increment(ResourceType.pagesWritten);
    }

    public void FinishBook(){
        Debug.Log("finished book");

        OnBookCompleted.Invoke();
        Resources.Increment(ResourceType.booksDone);
        Resources.Decrement(ResourceType.books);
    }

    public void FinishWord(){
        
        Resources.names.Decrement();
        Resources.Increment(ResourceType.wordsDone);
        OnWordCompleted.Invoke();
    }

    public void Step(){
 
        //calculate word delta
        int lettersCompleted = (int)Mathf.Floor(Mathf.Clamp(letters + lettersToWrite + writers.output - lettersLastFrame, 0, names));
        int lettersWritten = 0;

        if(names > 0 && pages > 0){
            for(int i = 0; i < lettersCompleted; i++){
                if(pages >= 1){
                    //continue writing while we still have pages
                    
                    ScriptWriter.i.WriteLetter();
                    lettersWritten ++;
                    letters ++;
                    lettersLastFrame = letters;
                }
            }

            letters += writers.output % 1;
        }

        lettersToWrite = 0;
        
        //calculate bookbinding production
        books += Mathf.Clamp(bookBinders.output, 0, Mathf.Floor(Resources.GetResource(ResourceType.pagesWritten).amount / Constants.PAGESPERBOOK));
        int booksCompleted = (int) Mathf.Floor(books);
        
        Resources.GetResource(ResourceType.pagesWritten).AddOutput(-booksCompleted * Constants.PAGESPERBOOK);
        

        //  UIManager.i.UpdatePrayerCount((int)Mathf.Floor(theism));
    }
    
}
