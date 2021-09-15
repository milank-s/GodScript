using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TextIO : MonoBehaviour {

	public TextAsset sourceText;
	
	string[] formattedText;

	string[][] canto;
	string[] line;
	string[] insertedText;

	int lineCount;
	float letterSpacing = 1;
	
	/*
	 * box of 600x400 fits 1254 chara at 14pt 2 in Space Mono
	 * 66 chara per line, 19 lines
	 * 
	 */

	// Use this for initialization
	void Start () {
		formattedText = sourceText.text.Split("\n"[0]);
		//insertedText = injectedText.text.Split (new char[] { ' ' });
		canto = new string[formattedText.Length][];
		

		bool scanning = true;
		int j = 0;
		int k = 0;

		while (scanning) {
			line = formattedText [j].Split(new char[] { ' ' });
			line [Random.Range (0, line.Length)] += " " + insertedText [j % insertedText.Length];

			canto[j] = line;

			j++;

			if (j >= formattedText.Length) {
				scanning = false;
			}
		}
	}
}
