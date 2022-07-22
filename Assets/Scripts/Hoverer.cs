using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class Hoverer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public RectTransform rect;
    public AudioClip hoverSFX;

    public UnityEvent OnHover;
    public UnityEvent OnExit;
    public UnityEvent OnChange;

    public float jitter = 1;

    void Awake(){
        rect = GetComponent<RectTransform>();
    }
    public void OnPointerEnter(PointerEventData p){
        AnimateHover(true);

        if(OnHover != null){
            OnHover.Invoke();
        }
    }

    public void OnPointerExit(PointerEventData p){
        AnimateHover(false);

        if(OnExit != null){
            OnExit.Invoke();
        }
    }

    public void AnimateHover(bool b){
        if(b){
            rect.anchoredPosition += Vector2.one * jitter;
            // if(Globals.audio != null){
            //     Globals.audio.PlaySound(hoverSFX, Vector3.zero);
            // }
        }else{
            
             rect.anchoredPosition -= Vector2.one * jitter;
        }
    }
}
