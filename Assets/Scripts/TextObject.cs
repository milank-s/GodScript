using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextObject : MonoBehaviour
{
    
    [SerializeField] protected TextMeshProUGUI text;

    public virtual void SetText(string t){
        text.text = t;
    }
}
