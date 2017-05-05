using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour {
    ColorBlock c1, c2;
    bool hold = false;
	// Use this for initialization
	void Awake () {
        c1 = GetComponent<Button>().colors;
        c2 = new ColorBlock();
        c2.normalColor = new Color(255/255f, 205/255f, 210/255f);
        c2.highlightedColor = new Color(239/255f, 154/255f, 154/255f);
        c2.pressedColor = new Color(239/255f, 83/255f, 80/255f);
        c2.colorMultiplier = 1;
    }
	
	public void ChangeColor()
    {
        Debug.Log(c1.highlightedColor);
        Debug.Log(c2.highlightedColor);
        if (!hold)
        {
            GetComponent<Button>().colors = c2;
        }
        else
            GetComponent<Button>().colors = c1;
        Debug.Log(GetComponent<Button>().colors);
        hold = !hold;
    } 
}
