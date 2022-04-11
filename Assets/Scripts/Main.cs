using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameManager gameManager;

    public static GameManager manager;
    public static JobManager jobs;
    public static MonkManager monks;
    public static Resources resources;
    
    
    void Awake(){
        manager = gameManager;
        jobs = new JobManager();
        resources = new Resources();
        monks = new MonkManager();
    }
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.S)){
            Save();
        }

        if(Input.GetKeyDown(KeyCode.R)){
            PlayerPrefs.DeleteAll();
        }
        manager.Step();
    }

    public void OnQuitApplication(){
        Save();
    }

    public void Save(){
        Debug.Log("saved");
        
        resources.Save();
        jobs.Save();
    }
}
