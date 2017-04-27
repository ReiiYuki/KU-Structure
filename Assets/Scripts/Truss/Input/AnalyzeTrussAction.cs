using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyzeTrussAction : MonoBehaviour {

	public void Analyze()
    {
        GameObject.FindObjectOfType<TrussAnalyzer>().analyze();
    }
}
 