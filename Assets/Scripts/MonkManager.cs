using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Monk{
    public string name;
    public float lifeSpan;
    public static string[] names = {"Joseph", "Thomas", "Quine", "Nathanial", "Isaac", "Frank", "Isaiah", "Samuel"};
    public Profession job;
    public Monk(){
        lifeSpan = Random.Range(250, 350);
        job = Profession.prayer;
        name = GetMonkName();
    }

    public static string GetMonkName(){
        return names[Random.Range(0, names.Length)];
    }

    public void Tick(){
        lifeSpan -= Time.deltaTime;
    }
}

public class MonkManager : MonoBehaviour
{
    public static MonkManager i;
    
    public UnityEvent OnAddMonk;
    public UnityEvent OnRemoveMonk;

    public List<Monk> monks;
    
    public void CreateMonk(){
        AddMonk();
    }
    public Monk AddMonk(){
        Monk m = new Monk();
        monks.Add(m);

        if(OnAddMonk != null){
            OnAddMonk.Invoke();
        }
        
        UIManager.i.AddToFeed(m.name + " has joined the monastery");
        MonasteryManager.i.AddMonk(m);

        return m;
    }

    public void RemoveMonk(Monk m){
        monks.Remove(m);
        MonasteryManager.i.jobs[m.job].RemoveMonk(m);
    }

    void Awake(){
        i = this;
        monks = new List<Monk>();
    }

    void Start(){
        Monk m = AddMonk();
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
