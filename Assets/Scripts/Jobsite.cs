using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        return m;
    }
    public Job(Profession p){
        employees = new List<Monk>();
        jobType = p;
        Setup();
    }

    public void AddMonk(Monk m){
        employees.Add(m);
        m.job = jobType;
    }
    public virtual void CalculateOutput(){
        output = productivity * employees.Count * Time.deltaTime;
    }
    
}

public class Jobsite : Unlock
{

    public Profession jobType;
    public Job job;
    public Counter employeeCounter;
    public Resource buildings;

    public void Setup(){
        job = new Job(jobType);
        MonasteryManager.i.jobs.Add(jobType, this);
    }
    public void Step(){
        job.CalculateOutput();
    }

    public void TryAssignWorker(int i){
        if(i > 0){
            if(MonasteryManager.i.prayers.employees.Count > 0){
                Monk m = MonasteryManager.i.prayers.PopMonk();
                MonasteryManager.i.jobs[Profession.prayer].RemoveMonk(m);
                AddMonk(m);
                UIManager.i.AddToFeed(m.name + " became a " + m.job);
            }
        }else{
            if(job.employees.Count > 0){
                
                Monk m = job.PopMonk();
                RemoveMonk(m);
                MonasteryManager.i.jobs[Profession.prayer].AddMonk(m);
            }
        }        
    }

    public void AddMonk(Monk m){
        job.AddMonk(m);
        employeeCounter.SetText(job.employees.Count.ToString());
    }
    public void RemoveMonk(Monk m){
        job.employees.Remove(m);
        employeeCounter.SetText(job.employees.Count.ToString());
    }
}
