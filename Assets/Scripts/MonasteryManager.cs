using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum Profession {prayer, writer, papermaker, bookbinder}

public class Job
{
    public Profession jobType;
    public List<Monk> employees;
    public float output;
    public float productivity = 1;

    public void Setup(){
        switch(jobType){
            case Profession.prayer:
                productivity = Constants.PRAYER_PRODUCTIVITY;
            break;

            case Profession.writer:
                productivity = Constants.WRITER_PRODUCTIVITY;
            break;

            case Profession.papermaker:
                productivity = Constants.PAPERMAKER_PRODUCTIVITY;
            break;

            case Profession.bookbinder:
                productivity = Constants.BOOKBINDER_PRODUCTIVITY;
            break;
        }
    }
    public Monk PopMonk(){
        Monk m = employees[employees.Count -1];
        employees.Remove(m);
        return m;
    }
    public Job(Profession p){
        employees = new List<Monk>();
        jobType = p;
        Setup();
    }
    public virtual void CalculateOutput(){
        output = productivity * employees.Count * Time.deltaTime;
    }
    
}
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
    
    public List<Job> jobs;
    public Job prayers;
    public Job writers;
    public Job bookBinders;
    public Job paperMakers;

    public void Awake(){
        i = this;
        prayers = new Job(Profession.prayer);
        writers = new Job(Profession.writer);
        bookBinders = new Job(Profession.bookbinder);
        paperMakers = new Job(Profession.papermaker);
        jobs = new List<Job>();
        jobs.Add(prayers);
        jobs.Add(writers);
        jobs.Add(paperMakers);
        jobs.Add(bookBinders);
    }

    public void RemoveMonk(Monk m){
        Job j = jobs[(int)m.job];
        j.employees.Remove(m);
        UIManager.i.UpdateProfession(j);
    }

    public void AddMonk(Monk m){
        jobs[0].employees.Add(m);
        UIManager.i.UpdateProfession(jobs[0]);
    }
    public void AssignWorker(Profession profession, int i){
        if(i > 0){
            if(prayers.employees.Count > 0){
                Monk m = jobs[0].PopMonk();
                m.job = profession;
                jobs[(int)profession].employees.Add(m);
                UIManager.i.AddToFeed(m.name + " became a " + m.job);
            }
        }else{
            if(jobs[(int)profession].employees.Count > 0){
                
                Monk m = jobs[(int)profession].PopMonk();
                m.job = Profession.prayer;
                jobs[0].employees.Add(m);
            }
        }
        
        UIManager.i.UpdateProfession(jobs[(int)profession]);
        UIManager.i.UpdateProfession(jobs[0]);
    }

    public void DiscoverNames(int n){
        UIManager.i.AddToFeed("A name of God was discovered");
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
        if(jobs[0].employees.Count > 0){
            float n = Mathf.Floor(names);
            UIManager.i.AddToFeed(jobs[0].employees[0].name + " prayed");
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

        prayers.CalculateOutput();
        writers.CalculateOutput();
        bookBinders.CalculateOutput();
        paperMakers.CalculateOutput();

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
