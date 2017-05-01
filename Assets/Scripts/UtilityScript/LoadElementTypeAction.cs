using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadElementTypeAction : MonoBehaviour {

    public string type;
    public List<ElementStore.Element> elements;
    public GameObject buttonPrefabs;
	// Use this for initialization
	void Start () {
        LoadElement();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadElement()
    {
        for (int i = 0; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);
        if (type == "H" || type == "I")
        {
            if (type == "H")
            {
                Debug.Log("Assign H");
                elements = ElementStore.H_BEAM;
            }
            else if (type == "I")
            {
                Debug.Log("Assign I");
                Debug.Log(ElementStore.I_BEAM);
                elements = ElementStore.I_BEAM;
            }
            float y = 0;
            int i = 0;
            foreach (ElementStore.Element e in elements)
            {
                GameObject button = Instantiate(buttonPrefabs, transform);
                button.GetComponent<RectTransform>().position = new Vector3(button.GetComponent<RectTransform>().position.x, y + button.GetComponent<RectTransform>().localScale.y);
                y -= button.GetComponent<RectTransform>().localScale.y * 2;
                ColorBlock colorBlock = button.GetComponent<Button>().colors;
                if (i % 2 == 0)
                {
                    colorBlock.normalColor = new Color(233 / 255f, 30 / 255f, 99 / 255f);
                    colorBlock.highlightedColor = new Color(194 / 255f, 24 / 255f, 91 / 255f);
                    colorBlock.pressedColor = new Color(173 / 255f, 20 / 255f, 87 / 255f);
                }
                else
                {
                    colorBlock.normalColor = new Color(156 / 255f, 39 / 255f, 176 / 255f);
                    colorBlock.highlightedColor = new Color(123 / 255f, 31 / 255f, 162 / 255f);
                    colorBlock.pressedColor = new Color(106 / 255f, 27 / 255f, 154 / 255f);
                }
                button.GetComponent<Button>().colors = colorBlock;
                button.GetComponentInChildren<Text>().text = e.name;
                button.GetComponent<SelectElementPropertyAction>().prop = e;
                i++;
            }
        }else if (type == "P")
        {
            float y = 0;
            int i = 0;
            foreach (ElementStore.PElement e in ElementStore.PIPE)
            {
                GameObject button = Instantiate(buttonPrefabs, transform);
                button.GetComponent<RectTransform>().position = new Vector3(button.GetComponent<RectTransform>().position.x, y + button.GetComponent<RectTransform>().localScale.y);
                y -= button.GetComponent<RectTransform>().localScale.y * 2;
                ColorBlock colorBlock = button.GetComponent<Button>().colors;
                if (i % 2 == 0)
                {
                    colorBlock.normalColor = new Color(233 / 255f, 30 / 255f, 99 / 255f);
                    colorBlock.highlightedColor = new Color(194 / 255f, 24 / 255f, 91 / 255f);
                    colorBlock.pressedColor = new Color(173 / 255f, 20 / 255f, 87 / 255f);
                }
                else
                {
                    colorBlock.normalColor = new Color(156 / 255f, 39 / 255f, 176 / 255f);
                    colorBlock.highlightedColor = new Color(123 / 255f, 31 / 255f, 162 / 255f);
                    colorBlock.pressedColor = new Color(106 / 255f, 27 / 255f, 154 / 255f);
                }
                button.GetComponent<Button>().colors = colorBlock;
                button.GetComponentInChildren<Text>().text = e.name;
                //button.GetComponent<SelectElementPropertyAction>().prop = e;
                i++;
            }
        }
        else if (type=="U")
        {
            ElementStore.GenerateU();
            float y = 0;
            int i = 0;
            foreach (ElementStore.UElement e in ElementStore.U_PROP)
            {
                GameObject button = Instantiate(buttonPrefabs, transform);
                button.GetComponent<RectTransform>().position = new Vector3(button.GetComponent<RectTransform>().position.x, y + button.GetComponent<RectTransform>().localScale.y);
                y -= button.GetComponent<RectTransform>().localScale.y*1.5f ;
                ColorBlock colorBlock = button.GetComponent<Button>().colors;
                if (i % 2 == 0)
                {
                    colorBlock.normalColor = new Color(233 / 255f, 30 / 255f, 99 / 255f);
                    colorBlock.highlightedColor = new Color(194 / 255f, 24 / 255f, 91 / 255f);
                    colorBlock.pressedColor = new Color(173 / 255f, 20 / 255f, 87 / 255f);
                }
                else
                {
                    colorBlock.normalColor = new Color(156 / 255f, 39 / 255f, 176 / 255f);
                    colorBlock.highlightedColor = new Color(123 / 255f, 31 / 255f, 162 / 255f);
                    colorBlock.pressedColor = new Color(106 / 255f, 27 / 255f, 154 / 255f);
                }
                button.GetComponent<Button>().colors = colorBlock;
                button.GetComponentInChildren<Text>().text = "E = "+e.E+" I = "+e.I;
                button.GetComponent<SelectElementPropertyAction>().uprop = e;
                i++;
            }
        }
        
    }
}
