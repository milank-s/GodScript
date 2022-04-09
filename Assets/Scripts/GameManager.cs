using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager i;
    public float year = 1000;
    public enum Stage{intro}

    public bool running = false;

    public List<Room> rooms;
    public Stage stage;
    public void Awake(){
        i = this;
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

    public void OpenRoom(Rooms roomType){

    }
    public void SetupUnlock(Room u){

        if(PlayerPrefs.HasKey(u.roomType.ToString())){
            u.Lock(PlayerPrefs.GetInt(u.roomType.ToString()) == 0);
        }else{
            PlayerPrefs.SetInt(u.roomType.ToString(), 1);
        }

        rooms.Add(u);
    }

    public IEnumerator GameLoop(){
        
        Stage curStage = stage;

        foreach(Sequence s in sequences){
            yield return StartCoroutine(s.SequenceBody());
        }

        running = true;
    }
    

}
