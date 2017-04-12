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
        List<GameObject> memebers = GameObject.FindObjectOfType<BeamCollector>().members;
        Dropdown memberDropdown = GetComponent<Dropdown>();
        foreach (GameObject member in memebers)
        {
            memberDropdown.options.Add(new Dropdown.OptionData() { text = member.GetComponent<MemberProperty>().number+"" });
        }
        memberDropdown.RefreshShownValue();
    }

}
