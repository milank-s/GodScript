using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager i;
    public float year = 1000;
    public enum Stage{intro}

    public bool running = false;
    public Stage stage;
    
    Dictionary<Rooms, Room> rooms;
    
    public void Awake(){
        i = this;
        rooms = new Dictionary<Rooms, Room>();
        
        if(PlayerPrefs.HasKey("stage")){
            stage = (Stage)PlayerPrefs.GetInt("stage");
        }else{
            stage = Stage.intro;
            PlayerPrefs.SetInt("stage", (int)Stage.intro);
        }
    }

    [Header("Sequences")]
    public Sequence[] sequences;

    void Start(){
        
        UIManager.i.year.SetText(year.ToString("F0") + " AD");
        StartCoroutine(GameLoop());
    }

    public void Step(){
        if(running){
            UIManager.i.year.SetText(year.ToString("F0") + " AD");
            Main.monks.Step();
            ScriptManager.i.Step();
        }
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

    public IEnumerator GameLoop(){
        
        Stage curStage = stage;

        foreach(Sequence s in sequences){
            yield return StartCoroutine(s.SequenceBody());
        }

        running = true;
    }
    

}
