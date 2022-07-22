using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : TextObject
{
    public TextObject title;
    public Image upArrow;
    public Image downArrow;

    
    public override IEnumerator Reveal(float time = 1){
        
        yield return StartCoroutine(title.Show());
        yield return new WaitForSeconds(0.25f);
        yield return StartCoroutine(base.Reveal(0.2f));
    }

    public override void Awake()
    {
        base.Awake();  

        if(upArrow == null) return;
        upArrow.gameObject.SetActive(false);
        downArrow.gameObject.SetActive(false);
    }

    public void ShowButtons(bool b){
        if(!visible) return;
        upArrow.gameObject.SetActive(b);
        downArrow.gameObject.SetActive(b);
    }

}
