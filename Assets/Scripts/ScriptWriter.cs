using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Script {

    public string[] words;
    public int letterIndex = 0;
    public int wordIndex = 0;

    public string curWord => words[wordIndex % words.Length];

    public Script(string text){
        words = text.Split (new char[] { ' ' });
    }
}

public class ScriptWriter : MonoBehaviour
{
    public static ScriptWriter i;
    public TextAsset sourceText;

    Script script;
    int lineIndex;

    public float autowriteSpeed = 0.1f;
    bool writingInsert;
    Queue<string> wordQueue;
    //string curWord => wordQueue.Peek();

    TextMeshProUGUI curLine => lines[lineIndex];

    // [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI dropCap;
    [SerializeField] TextObject title;
    [SerializeField] List<TextMeshProUGUI> lines;

    public void Awake(){
        i = this;    
        script = new Script(sourceText.text);
        wordQueue = new Queue<string>();
    }

    public void WriteInsert(string t){
        StartCoroutine(WriteText(t));
    }
    public IEnumerator WriteText(string t){
        
        Script s = new Script(t);
        writingInsert = true;

        if(lineIndex != 0){
            LineBreak();
        }

        while(ScriptWriter.i.WriteLetter(ref s, false)){
           yield return new WaitForSeconds(autowriteSpeed);
        }
        
        //add a two line breaks; only if the first one doesnt create a new page

        LineBreak();
        
        writingInsert = false;
    }

    void LineBreak(){
        if(!Return()){
            Return();
        }
    }

    public void WriteScriptLetter(){

        if(writingInsert) return;

        Resources.Decrement(ResourceType.writers);
        WriteLetter(ref script);

    }

    public bool WriteLetter(ref Script s, bool consumeResources = true){
        
        if(s.wordIndex >= s.words.Length){
            return false;
        }
        
        string word = s.curWord;
        char letter = word[s.letterIndex];


        if(lineIndex == 0 && curLine.text == "" && s.letterIndex == 0){
            dropCap.text = letter.ToString();
        }else{
            curLine.text += letter;
        }

        s.letterIndex ++;

        if(s.letterIndex >= word.Length){

            s.wordIndex ++;

            if(consumeResources){    
                ScriptManager.i.FinishWord();
            }

            //trying to get rid of spaces?
            while(s.curWord.Length <= 1 && s.curWord == " "){
                s.wordIndex ++;
            }

            s.letterIndex = 0;
            curLine.text += " ";

            //check if next word fits on the line
            
            curLine.text += s.curWord + " ";
            curLine.ForceMeshUpdate();

            bool isOverflow = curLine.isTextOverflowing;

             //now remove that shit
            curLine.text = curLine.text.Remove(curLine.text.Length - (s.curWord.Length + 1));

            if(isOverflow){
                //delete the last word and start it on the next line
                Return();
            }
        }
        
        return true;
    }

    public bool Return(){
        lineIndex++;

        if(lineIndex >= lines.Count){
            //finished page
            //clear all lines
            ScriptManager.i.FinishPage();
            ClearPage();
            lineIndex = 0;
            return true;
        }

        return false;
    }

    public void ClearPage(){
       foreach(TextMeshProUGUI t in lines){
           t.text = "";
       }
       dropCap.text = "";
    }
}
