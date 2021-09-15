using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    void Update()
    {
        MonkManager.i.Step();
        MonasteryManager.i.Step();
        UIManager.i.Step();
    }
}
