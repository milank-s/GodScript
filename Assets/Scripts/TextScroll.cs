using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScroll : TextObject
{
    Queue<string> writing;
    Queue<string> written;

    public TextObject currentLine;

    bool typing;
    int index;
    public float speed = 0.25f;
    public void Awake(){
        writing = new Queue<string>();
        written = new Queue<string>();
    }
    public void AddText(string t){

        writing.Enqueue(t);
        if(!typing && writing.Count == 1){
            StartCoroutine(TypeLine());
        }
    }

    void Update(){
        if(!typing && writing.Count > 1){
            StartCoroutine(TypeLine());
        }
    }

    IEnumerator TypeLine(){

        typing = true;

        if(writing.Count > 1){
            string t = writing.Dequeue();
            written.Enqueue(t);
            
            if(written.Count > 5){
                written.Dequeue();
            }

            text.text = "";
            
            string[] feedReadout =  written.ToArray();

            for(int i = feedReadout.Length-1; i>=0; i--){
                text.text += feedReadout[i] + '\n';
            }
        }

        string t2 = writing.Peek();

        yield return StartCoroutine(Effects.Type(speed, currentLine.text, t2));
        typing = false;
    }
}
