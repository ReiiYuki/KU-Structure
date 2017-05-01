using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTruss : MonoBehaviour {

	public void ResetCollector()
    {
        //GameObject.FindObjectOfType<BeamAnalyzer>().ResetAnalyzer();
        GameObject.FindObjectOfType<TRUSSCollector>().ResetAll();
    }
}
