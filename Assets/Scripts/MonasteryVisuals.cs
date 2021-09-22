using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonasteryVisuals : MonoBehaviour
{
    
    public static MonasteryVisuals i;
    public List<SpriteRenderer> sprites;

    public List<Transform> spriteContainers;

    int index;

    void Awake(){
        i = this;
    }

    public void TryAddBuilding(){
        if(MonkManager.i.monks.Count > index * 10){
            ShowNewSprite();
        }
    }

    void Start(){
        foreach(Transform t in spriteContainers){
            foreach (SpriteRenderer r in t.GetComponentsInChildren<SpriteRenderer>()){
                sprites.Add(r);
                r.enabled = false;
            }
        }

        sprites.Reverse();
    }

    public void ShowNewSprite(){
        sprites[index%sprites.Count].enabled = true;
        index++;
    }
}
