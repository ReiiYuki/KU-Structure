using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollControl : MonoBehaviour {

	// Use this for initialization
	/*void Start () {
        GetComponent<ScrollRect>().onValueChanged = delegate { Scroll()};
	}*/
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Scroll()
    {
        Debug.Log(GetComponent<ScrollRect>().verticalNormalizedPosition);
        Debug.Log(GetComponent<RectTransform>().position);
        if (GetComponent<ScrollRect>().verticalNormalizedPosition > 0) {
            GetComponent<ScrollRect>().verticalNormalizedPosition = 0;
        }
       /* Debug.Log(GetComponent<ScrollRect>().verticalNormalizedPosition / transform.GetChild(0).childCount);
        Debug.Log(transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().rect.height);
        Debug.Log(transform.GetChild(0).childCount);
        Debug.Log(transform.GetChild(0).GetChild(transform.GetChild(0).childCount-1).GetComponent<RectTransform>().localPosition);
         if (GetComponent<ScrollRect>().verticalNormalizedPosition < -1*transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().rect.height*transform.GetChild(0).childCount)
             {
                 GetComponent<ScrollRect>().verticalNormalizedPosition = -1 * transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().rect.height * transform.GetChild(0).childCount;
             }*/

    }
}
