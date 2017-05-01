using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamDisposePanel : MonoBehaviour {

    public void Dispose()
    {
        transform.parent.parent.gameObject.SetActive(false);
    }
}
