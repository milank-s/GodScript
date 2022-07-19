using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    public float framerate = 12;

    public Sprite[] frames;
    [SerializeField] private SpriteRenderer r;
    private int index;

    private float timer;

    // Update is called once per frame
    void Start()
    {
        if(framerate == 0){
            framerate = 0.0000001f;
        }

        index = Random.Range(0, frames.Length);
        r.sprite = frames[index];
    }
    void Update()
    {
        
        if (timer > 1f/framerate)
        {
            index++;
            if (index >= frames.Length)
            {
                index = 0;
            }
            
            r.sprite = frames[index];
            timer = 0;
        }

        timer += Time.deltaTime;
    }
}
