using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Profession {prayer, writer, papermaker, bookbinder}

public class Job
    {

    public ResourceType resourceProduced;
    public Resource resource;
    public float productivity = 1;
    public Profession jobType;
    public List<Monk> employees;
    public float output;

    public void Setup(){
        
    }

    public Monk PopMonk(){
        Monk m = employees[employees.Count -1];
        employees.Remove(m);
        return m;
    }

    public Job(Profession p){
        employees = new List<Monk>();
        jobType = p;
        resource = Resources.GetResource(resourceProduced);
        Setup();
    }

    public void AddMonk(Monk m){
        employees.Add(m);
        m.job = jobType;
    }
    public virtual void CalculateOutput(){
        output = productivity * employees.Count * Time.deltaTime;
    }

    public void Tick(){
        CalculateOutput();
        resource.AddOutput(output);
    }
    
}

public class Jobsite : MonoBehaviour
{

    public Profession jobType;
    public Job job;
    public Counter employeeCounter;

    public void Awake(){
        job = new Job(jobType);
        JobManager.i.jobs.Add(jobType, this);
    }

    public void Update(){
        job.Tick();
    }

    public void TryAssignWorker(int i){
        
        if(i > 0){

            if(JobManager.i.prayers.employees.Count > 0){
                Monk m = JobManager.i.prayers.PopMonk();
                AddMonk(m);
                UIManager.i.AddToFeed(m.name + " became a " + m.job);
            }
        }else{
            if(job.employees.Count > 0){
                
                Monk m = job.PopMonk();
                JobManager.i.jobs[Profession.prayer].AddMonk(m);
                RemoveMonk(m);
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
