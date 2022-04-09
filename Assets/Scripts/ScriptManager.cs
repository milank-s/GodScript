 
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

    public int pagesWritten = 0;
    public int booksWritten = 1;
    public int wordsWritten;

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
        pagesWritten ++;
        pagesDone++;
    }

    public void FinishBook(){
        Debug.Log("finished book");

        OnBookCompleted.Invoke();
        Resources.GetResource(ResourceType.booksDone).AddOutput(1);
        Resources.GetResource(ResourceType.books).AddOutput(-1);
    }

    public void FinishWord(){
        names --;
        wordsWritten ++;
        OnWordCompleted.Invoke();
    }

    public void WriteLetter(){
        lettersToWrite ++;
    }
    
    public void Step(){

        //calculate page production

        pages += paperMakers.output; 

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
        books += Mathf.Clamp(bookBinders.output, 0, Mathf.Floor(pagesDone / Constants.PAGESPERBOOK));
        int booksCompleted = (int) Mathf.Floor(books);
        pagesDone -= booksCompleted * Constants.PAGESPERBOOK;
        // do something visually with the amount of pages in the stack?

        UIManager.i.currentPage.SetText(pagesDone.ToString());
        UIManager.i.emptyPages.SetText(pages.ToString());
        UIManager.i.names.SetText(Mathf.Floor(names).ToString());

        Resources.GetResource(ResourceType.booksDone).SetOutput(booksWritten);
        //  UIManager.i.UpdatePrayerCount((int)Mathf.Floor(theism));
    }
    
}
