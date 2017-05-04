using UnityEngine;
using System.Collections;

public class forceLayerTruss : MonoBehaviour
{

    public void ToggleInputForce()
    {
        GameObject.FindObjectOfType<TRUSSCollector>().toggleInputForce();
    }
    public void ToggleOuterForce()
    {
        GameObject.FindObjectOfType<TRUSSCollector>().toggleOuterForce();
    }
    public void ToggleInnerForce()
    {
        GameObject.FindObjectOfType<TRUSSCollector>().toggleInnerForce();
    }
    public void ToggleStressRatio()
    {
        GameObject.FindObjectOfType<TRUSSCollector>().toggleStress();
    }
}
