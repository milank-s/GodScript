using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScroll : TextObject
{
    public Queue<string> writing;

    public void Awake(){
        writing = new Queue<string>();
    }
    public void AddText(string t){
        writing.Enqueue(t);
        if(writing.Count > 5){
            writing.Dequeue();
        }

        text.text = "";
        foreach(string s in writing){
            text.text += s + '\n';
        }

    }
}
