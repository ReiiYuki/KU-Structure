using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOrHideToolbox : MonoBehaviour {

	public void HideOrShow()
    {
        transform.parent.GetComponent<Animator>().SetTrigger("Action");
    }
}
