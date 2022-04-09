using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobManager
{
    
    public Job prayers => jobs[Profession.prayer].job;
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
        jobs[p].AddMonk(m);
    }

}
