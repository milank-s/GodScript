using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Profession {prayer, writer, papermaker, bookbinder, layclergy}

public class Job
    {

    public delegate void Event();
    public Event OnHireMonk;
    public Resource resource;
    public float productivity = 1;
    public Profession jobType;
    public List<Monk> employees;

    public float output;

    public void Setup(){
        
    }

    public Monk PopMonk(){
        Monk m = employees[employees.Count -1];
        RemoveMonk(m);

        return m;
    }

    public Job(Profession p, ResourceType r){
        employees = new List<Monk>();
        jobType = p;
        resource = Resources.GetResource(r);
        Setup();
    }

    public void RemoveMonk(Monk m){
        employees.Remove(m);

        if(OnHireMonk != null){
            OnHireMonk.Invoke();
        }
    }
    public void AddMonk(Monk m){
        employees.Add(m);
        m.job = jobType;

        if(OnHireMonk != null){
            OnHireMonk.Invoke();
        }
    }
    public virtual void CalculateOutput(){
        output = productivity * employees.Count * Time.deltaTime;
    }

    public void Step(){
        if(resource.resourceType < 0) return;

        CalculateOutput();
        resource.AddOutput(output);
    }
    
}

public class Jobsite : UIObject
{

    public Profession jobType;
    public ResourceType resourceProduced;
    public Job job;
    public Counter employeeCounter;

    public ImageObject upArrow;
    public ImageObject downArrow;


    public void Awake(){
        job = new Job(jobType, resourceProduced);
        JobManager.i.jobs.Add(jobType, this);
        job.OnHireMonk += ChangeEmployeeCount;
    }

    public override IEnumerator Reveal(float time = 1){
        
        yield return StartCoroutine(base.Reveal(1));

        yield return StartCoroutine(employeeCounter.Reveal(1));
        
        if(upArrow == null) yield break;

        yield return StartCoroutine(upArrow.Reveal(1));
        yield return StartCoroutine(downArrow.Reveal(1));
    }

    public void Update(){
        job.Step();
    }

    public void TryAssignWorker(int i){
        
        if(i > 0){
            if(JobManager.i.prayers.employees.Count > 0 || JobManager.i.layclergy.employees.Count > 0){
                Monk m;
                
                if(JobManager.i.layclergy.employees.Count > 0){
                    m = JobManager.i.layclergy.PopMonk();
                }else{
                    m = JobManager.i.prayers.PopMonk();
                }

                job.AddMonk(m);
                UIManager.i.AddToFeed(m.name + " became a " + m.job);
            }
        }else{
            if(job.employees.Count > 0){
                
                Monk m = job.PopMonk();
                if(m.rank > 0){
                    JobManager.i.jobs[Profession.prayer].job.AddMonk(m);
                }else{
                    JobManager.i.jobs[Profession.layclergy].job.AddMonk(m);
                }
            }
        }    
    }

    public void ChangeEmployeeCount(){
        employeeCounter.SetAmount(job.employees.Count);
    }
}
