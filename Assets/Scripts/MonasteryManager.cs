using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonasteryManager : MonoBehaviour
{
    public float year = 856;
    public static MonasteryManager i;
    public UnityEvent OnWordCompleted;
    public UnityEvent OnPageCompleted;
    public UnityEvent OnBookCompleted;
    float lettersLastFrame;

    
    [Header("floats")]
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
    
    public Job prayers => jobs[Profession.prayer].job;
    public Dictionary<Profession, Jobsite> jobs;

    Job writers => jobs[Profession.writer].job;
    Job paperMakers => jobs[Profession.papermaker].job;
    Job bookBinders => jobs[Profession.bookbinder].job;

    //LETS MOVE FROM INSTANTIATING ALL THE DIFF JOBS HERE TO HAVE DIFF JOB OBJECTS
    //THAT INSTANTIATE THEMSELVES
    //ALSO WHAT IF THE PLAYERS UNLOCK JOBS IN DIFF ORDERS
    //AND THEN THE JOBS ARRAY GETS FUCKED UP?
    
    public void Awake(){
        i = this;
        jobs = new Dictionary<Profession, Jobsite>();
    }

    public void AddMonk(Monk m){
        jobs[Profession.prayer].AddMonk(m);
       
        MonasteryVisuals.i.TryAddBuilding();
    }

    public void DiscoverNames(int n){
        UIManager.i.AddToFeed(jobs[0].job.employees[Random.Range(0, jobs[0].job.employees.Count)].name + " heard a name of God");
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

        booksWritten ++;
        books --;
        OnBookCompleted.Invoke();
        UIManager.i.UpdateBookCount(booksWritten);
    }

    public void FinishWord(){
        names --;
        wordsWritten ++;
        OnWordCompleted.Invoke();
    }

    public void Pray(){
        if(jobs[0].job.employees.Count > 0){
            float n = Mathf.Floor(names);
            names += Constants.PRAYER_PRODUCTIVITY * 1;
            if(Mathf.Floor(names) > n){
                DiscoverNames((int)n);
            }

        }else{
            UIManager.i.AddToFeed("There is no one to pray");
        }
    }

    public void WriteLetter(){
        lettersToWrite ++;
    }
    public void Step(){

        year += Time.deltaTime * 0.1f;

        //calculate page production
        float n = Mathf.Floor(names);
        names += prayers.output;

        if(Mathf.Floor(names) > n){
            DiscoverNames((int)n);
        }

        pages += paperMakers.output; 

        //calculate word delta
        int lettersCompleted = (int)Mathf.Floor(Mathf.Clamp(letters + lettersToWrite + writers.output - lettersLastFrame, 0, names));
        int lettersWritten = 0;

        if(names >= 1 && pages >= 1){
            for(int i = 0; i < lettersCompleted; i++){
                if(pages >= 1){
                    //continue writing while we still have pages
                    
                    ScriptManager.i.WriteLetter();
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

        UIManager.i.UpdateWordCount(wordsWritten);
        UIManager.i.UpdatePageCount(pagesWritten);
        //  UIManager.i.UpdatePrayerCount((int)Mathf.Floor(theism));
    }
    
}
