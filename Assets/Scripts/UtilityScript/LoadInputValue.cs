using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadInputValue : MonoBehaviour {

    public string[] list;
    public int index = 0;

	// Use this for initialization
	void Start () {
        if (list.Length == 0) GetComponent<Text>().text = "None";
        else
        {
            int num;
            string show = list[index];
            if (int.TryParse(show,out num))
                show = (num + 1)+"";
            GetComponent<Text>().text = show;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Change(int direction)
    {
        if (list.Length > 0)
        {
            if (direction == 1)
            {
                if (index + 1 == list.Length) index = 0;
                else index++;
            }
            else
            {
                if (index - 1 < 0) index = list.Length - 1;
                else index--;
            }
            int num;
            string show = list[index];
            if (int.TryParse(show,out num))
                show = (num + 1) + "";
            GetComponent<Text>().text = show;
        }
    }

    public void UpdateData(string[] data)
    {
        list = data;
        index = 0;
        if (list.Length == 0) GetComponent<Text>().text = "None";
        else
        {
            int num;
            string show = list[index];
            if (int.TryParse(show,out num))
                show = (num + 1) + "";
            GetComponent<Text>().text = show;
        }
    }
}
