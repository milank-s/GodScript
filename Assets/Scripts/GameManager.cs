using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager i;
    public float year = 1000;
    public enum Stage{intro, study, writing, bookmaking, funding, resources, building}

    public bool running = false;

    public float startingPages = 3;
    
    public Stage stage;
    
    Dictionary<Rooms, Room> rooms;

    [Header("Sequences")]
    public Sequence[] sequences;

    public void Awake(){
        i = this;
        rooms = new Dictionary<Rooms, Room>();
        
        Initialize();
    }

    void Start(){
        UIManager.i.year.SetText(year.ToString("F0") + " AD");
        StartCoroutine(GameLoop());
    }

    public void Initialize(){
        
        if(!PlayerPrefs.HasKey("startedGame")){
            Debug.Log("Starting clean game");
            PlayerPrefs.SetInt("startedGame", 1);
            Resources.pages.amount = 3;
        }


        if(PlayerPrefs.HasKey("stage")){
            
            stage = (Stage)PlayerPrefs.GetInt("stage");
        }else{
            stage = Stage.intro;
            PlayerPrefs.SetInt("stage", (int)Stage.intro);
        }

            Debug.Log("beginning from " + stage);

        if(PlayerPrefs.HasKey("year")){
            year = PlayerPrefs.GetInt("year");
        }else{
            PlayerPrefs.SetInt("year", (int)year);
        }
    }

    public void Step(){
        if(running){
            UIManager.i.year.SetText(year.ToString("F0") + " AD");
            Main.monks.Step();
            ScriptManager.i.Step();
            Main.resources.Step();
        }
    }

    public void Pray(){
        Resources.names.Increment();
    }

    public void AddMonk(){
        Main.monks.MonkArrival();
    }

    public void UnlockRoom(Rooms roomType){
        Room r = null;
        if(rooms.TryGetValue(roomType, out r)){
            r.Unlock();
        }
    }
    public void SetupUnlock(Room u){

        if(PlayerPrefs.HasKey(u.roomType.ToString())){
            u.Lock(PlayerPrefs.GetInt(u.roomType.ToString()) == 0);
        }else{
            PlayerPrefs.SetInt(u.roomType.ToString(), 1);
        }
        
        rooms.Add(u.roomType, u);
    }

    void OnApplicationQuit(){
        PlayerPrefs.Save();
        Debug.Log("saving game");
    }

    public IEnumerator GameLoop(){
        
        Stage curStage = stage;

        foreach(Sequence s in sequences){
            yield return StartCoroutine(s.PlaySequence());
            yield return null;
        }

        running = true;
    }
}
