using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoTruss : MonoBehaviour {

	public void Undo()
    {
        GameObject.FindObjectOfType<TRUSSCollector>().Undo();
    }
}
