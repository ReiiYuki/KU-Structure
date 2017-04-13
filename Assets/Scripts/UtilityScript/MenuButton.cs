using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Text theText;
    public Color enterColor,exitColor;

    public void OnPointerEnter(PointerEventData eventData)
    {
        theText.color = enterColor; //Or however you do your color
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        theText.color = exitColor; //Or however you do your color
    }
}