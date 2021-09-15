using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScriptManager : MonoBehaviour
{
    public static ScriptManager i;
    public TextAsset sourceText;
    string[] words;
    int index;
     [SerializeField] TextMeshProUGUI text;

    public void WriteWord(){
        text.text += words[index % words.Length] + " ";
        index ++;
    }

    public void ClearPage(){
        text.text = "";
    }
    public void Awake(){
        i = this;
        words = sourceText.text.Split (new char[] { ' ' });
    }
}
