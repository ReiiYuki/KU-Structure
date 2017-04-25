using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyzeAction : MonoBehaviour {

	public void Analyze()
    {
        GameObject.FindObjectOfType<BeamAnalyzer>().ResetAnalyzer();
        GameObject.FindObjectOfType<BeamAnalyzer>().Analyze();
    }
}
