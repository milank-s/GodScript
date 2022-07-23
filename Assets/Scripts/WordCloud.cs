using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordCloud : MonoBehaviour
{
    public TextAsset text;
    public Text textPrefab;
    Script words;
    void Start()
    {
        words = new Script(text.text);
        Resources.names.OnWholeNumberDelta += SpawnWords;
    }

    public void SpawnWords(int i){
        for(int j = 0; j < i; j++){
            SpawnWord();
        }
    }
    public void SpawnWord(){
        Vector3 pos = Random.insideUnitSphere;
        pos.z = 0;
        Text newText = Instantiate(textPrefab, transform.position + pos * 3f, Quaternion.identity);
        newText.SetText(words.GetNextWord().ToUpper());
    }
}
