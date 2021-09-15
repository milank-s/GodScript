﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScriptManager : MonoBehaviour
{
    public static ScriptManager i;
    public TextAsset sourceText;
    string[] words;
    int wordIndex;
    int letterIndex;
    int lineIndex;

    string curWord => words[wordIndex%words.Length];
    TextMeshProUGUI curLine => lines[lineIndex];

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI dropCap;

    [SerializeField] List<TextMeshProUGUI> lines;

    public void WriteLetter(){

        char letter = curWord[letterIndex];
        
        if(lineIndex == 0 && curLine.text == "" && letterIndex == 0){
            dropCap.text = letter.ToString();
        }else{
            curLine.text += letter;
        }

        letterIndex ++;

        if(letterIndex >= curWord.Length){

            MonasteryManager.i.FinishWord();
            
            //finished word
            wordIndex ++;

            //trying to get rid of spaces?
            while(curWord.Length <= 1 && curWord == " "){
                wordIndex ++;
            }

            letterIndex = 0;
            curLine.text += " ";

            //check if next word fits on the line
            
            curLine.text += curWord + " ";
            curLine.ForceMeshUpdate();

            bool isOverflow = curLine.isTextOverflowing;

            Debug.Log(isOverflow);

             //now remove that shit
            curLine.text = curLine.text.Remove(curLine.text.Length - (curWord.Length + 1));

            if(isOverflow){
                //delete the last word and start it on the next line
                lineIndex++;

                if(lineIndex >= lines.Count){
                    //finished page
                    //clear all lines
                    MonasteryManager.i.FinishPage();
                    ClearPage();
                    lineIndex = 0;
                }
            }
        }
    }

    public void ClearPage(){
       foreach(TextMeshProUGUI t in lines){
           t.text = "";
       }
       dropCap.text = "";
    }

    public void Awake(){
        i = this;
        
        words = sourceText.text.Split (new char[] { ' ' });
    }
}
