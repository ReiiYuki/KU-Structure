using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisposePanelAction : MonoBehaviour {

	public void Dispose()
    {
        transform.parent.gameObject.SetActive(false);
    }

}
