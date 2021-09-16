using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{

    public static UIManager i;

    [Header ("Feeds")]
    [SerializeField] TextScroll feed;

    [Header ("Monk Data")]
    [SerializeField] JobUI monkCount;
    [SerializeField] JobUI writers;
    [SerializeField] JobUI pagers;
    [SerializeField] JobUI bookers;
    

    [Header ("Resources")]  
    public Counter theismAmount;    
    public Counter names;    
    public Counter emptyPages;     
    public Counter currentPage;    

    
    [Header ("Progress")]
    [SerializeField] TextObject year;    
    [SerializeField] Counter wordsTotal;    
    [SerializeField] Counter pagesTotal;    
    [SerializeField] Counter booksTotal;    

    [SerializeField] List<JobUI> jobUI;

    public void AddToFeed(string t){
        feed.AddText(t);
    }
    public void Awake(){
        i = this;
        jobUI = new List<JobUI>();
        jobUI.Add(monkCount);
        jobUI.Add(writers);
        jobUI.Add(pagers);
        jobUI.Add(bookers);
    }

    public void Step(){
        year.SetText(MonasteryManager.i.year.ToString("F0") + " AD");
    }
    public void UpdateProfession(Job j){
        JobUI job = jobUI[(int)j.jobType];
        job.counter.SetText(j.employees.Count.ToString());
    }

    public void UpdatePageCount(int i){
        pagesTotal.SetText(i.ToString());
    }

    public void UpdateBookCount(int i){
        booksTotal.SetText(i.ToString());
    }

    public void UpdateWordCount(int i){
        wordsTotal.SetText(i.ToString());
    }

    public void UpdatePrayerCount(int i){
        theismAmount.SetText(i.ToString());
    }

}
