 
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
    public float letters;
    public float words;
    public float pagesDone;
    public float books;

    [Header("ints")]

    int pagesCompleted;

    
    Queue<string> wordQueue;
    

    public void Awake(){
        i = this;
    }

    void Start(){
        Resources.writers.OnWholeNumberDelta += WriterOutput;
    }

    public void FinishPage(){
        
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
        Resources.GetResource(ResourceType.pagesWritten).AddOutput(Constants.PAGESPERBOOK);
    }

    public void FinishWord(){
        
        Resources.names.Decrement();
        Resources.Increment(ResourceType.wordsDone);
        OnWordCompleted.Invoke();
    }

    public void WriterOutput(int delta){

        if(Resources.names.amount > 0 && Resources.pages.amount > 0){
            for(int i = 0; i < delta; i++){
                if(Resources.pages.amount >= 1){
                    //continue writing while we still have pages
                    
                    ScriptWriter.i.WriteScriptLetter();
                }
            }
        }
    }

    public void BindBooks(){

        //output doesnt accumulate as a resource
        //how to calculate bookbinders working over time?
        int booksCompleted = (int) Mathf.Floor(Mathf.Clamp(bookBinders.output, 0, Mathf.Floor(Resources.GetResource(ResourceType.pagesWritten).amount / Constants.PAGESPERBOOK)));

        for(int i = 0; i < booksCompleted; i++){
            FinishBook();
        }
    }

    public void Step(){
 
        BindBooks();
        

        //calculate word delta
        // int writerOutput = (int)Mathf.Floor(Mathf.Clamp(letters + writers.output - lettersLastFrame, 0, Resources.names.amount));
        // int lettersWritten = 0;

        // if(Resources.names.amount > 0 && Resources.pages.amount > 0){
        //     for(int i = 0; i < writerOutput; i++){
        //         if(Resources.pages.amount >= 1){
        //             //continue writing while we still have pages
                    
        //             ScriptWriter.i.WriteLetter();
        //             lettersWritten ++;
        //             letters ++;
        //             lettersLastFrame = letters;
        //         }
        //     }

            
        //     letters += writers.output % 1;
        // }
        
        //  UIManager.i.UpdatePrayerCount((int)Mathf.Floor(theism));
    }
    
}
