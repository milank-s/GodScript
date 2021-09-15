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
    float wordsLastFrame;

    [Header("floats")]
    public float words;
    public float pages = 10;
    public float pagesDone;
    public float books;

    [Header("ints")]

    public int pagesWritten = 0;
    public int booksWritten = 1;
    public int wordsWritten;
    
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

    public void WriteWord(){
        words += Mathf.Clamp(1, 0, Mathf.Floor(pages * Constants.WORDSPERPAGE));
    }
    public void Step(){

        year += Time.deltaTime * 0.1f;
        writers.CalculateOutput();
        bookBinders.CalculateOutput();
        paperMakers.CalculateOutput();

        //calculate page production
        pages += paperMakers.output; 
        //add word production clamped by pages
        words += Mathf.Clamp(writers.output, 0, Mathf.Floor(pages * Constants.WORDSPERPAGE));

        //calculate word delta
        int wordsCompleted = (int)Mathf.Floor(Mathf.Clamp(words - wordsLastFrame, 0, 100000f));

        //calculate pages filled
        int pagesCompleted = (int)Mathf.Floor(words/Constants.WORDSPERPAGE);
        
        //subtract pages and words consumed this frame
        //add to pageCounter
        pages -= pagesCompleted;
        words -= pagesCompleted * Constants.WORDSPERPAGE;
        pagesDone += pagesCompleted;
        
        //calculate bookbinding production
        books += Mathf.Clamp(bookBinders.output, 0, Mathf.Floor(pagesDone / Constants.PAGESPERBOOK));
        int booksCompleted = (int) Mathf.Floor(books);
        pagesDone -= booksCompleted * Constants.PAGESPERBOOK;

        UIManager.i.unboundPages.SetText(pagesDone.ToString());
        UIManager.i.emptyPages.SetText(pages.ToString());
        UIManager.i.words.SetText(words.ToString());

        //fire events if words, pages, or books were completed;
        if(wordsCompleted != 0){

            for(int i = 0; i < wordsCompleted; i++){
                ScriptManager.i.WriteWord();
                OnWordCompleted.Invoke();
            }
            
            wordsLastFrame = words;
            wordsWritten += wordsCompleted;
            UIManager.i.UpdateWordCount(wordsWritten);
        }

        if(pagesCompleted != 0){
            
            for(int i = 0; i < pagesCompleted; i++){
                ScriptManager.i.ClearPage();
                wordsLastFrame = 0;
                OnPageCompleted.Invoke();
            }

            pagesWritten += pagesCompleted;
            UIManager.i.UpdatePageCount(pagesWritten);
        }

        if(booksCompleted != 0){
            
            booksWritten += booksCompleted;
            books = 0;
            OnBookCompleted.Invoke();
            UIManager.i.UpdateBookCount(booksWritten);
        }
    }
    
}
