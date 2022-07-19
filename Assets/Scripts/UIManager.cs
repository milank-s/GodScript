using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public static UIManager i;

    [Header ("Feeds")]
    public TextScroll feed;
    

    [Header ("Resources")]  
    public Counter theismAmount;    
    public Counter names;    
    public Counter emptyPages;     
    public Counter currentPage;    

    
    [Header ("Progress")]
    public TextObject year;    
    [SerializeField] Counter wordsTotal;    
    [SerializeField] Counter pagesTotal;    
    public Counter booksTotal;    


    public void AddToFeed(string t){
        feed.AddText(t);
    }

    public void InsertToFeed(string t){
        
    }

    public void Awake(){
        i = this;
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
