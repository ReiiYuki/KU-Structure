using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoBeam : MonoBehaviour {

	public void Undo()
    {
        GameObject.FindObjectOfType<BeamCollector>().Undo();
    }
}
