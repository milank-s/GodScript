using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScriptWriter : MonoBehaviour
{   
    [SerializeField] TextMeshProUGUI text;
    public void WriteWord(){
        text.text += "word ";
    }
}
