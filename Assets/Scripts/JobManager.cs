using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobManager
{
    
    public Job prayers => jobs[Profession.prayer].job;
    public Job layclergy => jobs[Profession.layclergy].job;
    public Dictionary<Profession, Jobsite> jobs;

    public static JobManager i;

    public JobManager(){
        Setup();
    }

    void Setup(){
        i = this;
        jobs = new Dictionary<Profession, Jobsite>();
    }

    public void AssignJob(Monk m, Profession p){
        jobs[p].job.AddMonk(m);
    }

    public void Save(){
        foreach(int i in System.Enum.GetValues(typeof(Profession))){
            
            Jobsite j;

            if(jobs.TryGetValue((Profession) i, out j)){
                PlayerPrefs.SetFloat(j.ToString() + "s", j.job.employees.Count);
            
            }
        }
    }
}
