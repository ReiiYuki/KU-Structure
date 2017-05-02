using UnityEngine;
using System.Collections;

public class forceLayerTruss : MonoBehaviour
{

    public void ToggleInputForce()
    {
        GameObject.FindObjectOfType<TRUSSCollector>().toggleInputForce();
        Debug.Log("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
    }
    public void ToggleOuterForce()
    {
        GameObject.FindObjectOfType<TRUSSCollector>().toggleOuterForce();
        Debug.Log("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
    }
    public void ToggleInnerForce()
    {
        GameObject.FindObjectOfType<TRUSSCollector>().toggleInnerForce();
        Debug.Log("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
    }
}
