using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonkManager 
{   
    public UnityEvent OnAddMonk;
    public UnityEvent OnRemoveMonk;
    public List<Monk> monks;
    
    public MonkManager(){
        monks = new List<Monk>();
    }


    public void MonkArrival(){
        Monk m = CreateMonk();
        UIManager.i.AddToFeed(m.name + " has joined the monastery");
    }
    
    public Monk CreateMonk(){
        Monk m = new Monk(Monk.Hierarchy.clergy);
        monks.Add(m);

        if(OnAddMonk != null){
            OnAddMonk.Invoke();
        }
        
        JobManager.i.AssignJob(m, Profession.prayer);

        return m;
    }

    public void RemoveMonk(Monk m){
        monks.Remove(m);
        JobManager.i.jobs[m.job].job.RemoveMonk(m);
    }

    public void Step()
    {
        List<Monk> deadMonks = new List<Monk>();
        foreach(Monk m in monks){
            m.Tick();

            if(m.lifeSpan < 0){
                deadMonks.Add(m);
            }
        }

        foreach(Monk m in deadMonks){
            UIManager.i.AddToFeed(m.name + " has gone to heaven");
            RemoveMonk(m);
        }
    }
}

public class Monk{
    public string name;
    public float lifeSpan;
    float lifeLeft;
    public static string[] names = {"Joseph", "Thomas", "Quine", "Nathanial", "Isaac", "Frank", "Isaiah", "Samuel"};

    public enum Hierarchy{laity, clergy}
    public Profession job;
    public Hierarchy rank;
    public Monk(Hierarchy h){
        lifeSpan = Random.Range(250, 350);
        lifeLeft = lifeSpan;
        rank = h;

        if(h == Hierarchy.clergy){
            job = Profession.prayer;
        }else{
            job = Profession.layclergy;
        }

        name = GetMonkName();
    }

    public static string GetMonkName(){
        return names[Random.Range(0, names.Length)];
    }

    public void Tick(){
        lifeLeft -= Time.deltaTime;
    }
}
