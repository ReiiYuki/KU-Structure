using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIndexButton : MonoBehaviour {

    public int direction;
    public GameObject valueText;

	public void Move()
    {
        valueText.GetComponent<LoadInputValue>().Change(direction);
    }
}
