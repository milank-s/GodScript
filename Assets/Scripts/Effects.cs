using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Effects : MonoBehaviour
{
    public static IEnumerator Type(float time, TextMeshProUGUI t, string text){

        t.text = "";
        
        for(int i = 0; i < text.Length; i++){
            t.text += text[i];
            yield return new WaitForSeconds(Random.Range(0.1f, 0.15f) * time);
        }
    }

    public static IEnumerator FadeIn(float time, SpriteRenderer s){
        float t = 0;
        while(t < 1){
            Color c = s.color;
            c.a = t;
            s.color = c;
            t += Time.deltaTime / time;
            yield return null;
        }

        s.color = Color.white;
    }

    
}
