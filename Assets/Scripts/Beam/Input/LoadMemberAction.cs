using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoadMemberAction : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
        LoadMember();
	}
	
	void LoadMember()
    {
        Debug.Log("Load Member is working");
        List<GameObject> members = GameObject.FindObjectOfType<BeamCollector>().members;
        string[] data = new string[members.Count];
        for (int i = 0;i< members.Count;i++)
        {
            data[i] = members[i].GetComponent<MemberProperty>().number+"";
        }
        GetComponent<LoadInputValue>().UpdateData(data);
    }

}
