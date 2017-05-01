using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBeam : MonoBehaviour {

	public void ResetCollector()
    {
        GameObject.FindObjectOfType<BeamAnalyzer>().ResetAnalyzer();
        GameObject.FindObjectOfType<BeamCollector>().ResetCollector();
    }
}
