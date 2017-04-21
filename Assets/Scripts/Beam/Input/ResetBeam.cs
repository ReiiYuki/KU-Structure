using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBeam : MonoBehaviour {

	public void ResetCollector()
    {
        GameObject.FindObjectOfType<BeamCollector>().ResetCollector();
    }
}
