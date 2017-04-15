using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyzeAction : MonoBehaviour {

	public void Analyze()
    {
        GameObject.FindObjectOfType<BeamAnalyzer>().Analyze();
    }
}
