using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadInputStat : MonoBehaviour {

	public GameObject textPrefab;
	public GameObject canvas;
	// Use this for initialization
	void OnEnable () {
		TRUSSCollector collector = GameObject.FindObjectOfType<TRUSSCollector>();


		for(int i =0;i<collector.members.Count;i++) {
			//TrussMemberProperty m = mm.GetComponent<TrussMemberProperty> ();
			GameObject m = collector.members[i];
			GameObject text = Instantiate (textPrefab, new Vector3 (0, i), Quaternion.identity);
			Debug.Log ("in");
			text.GetComponentInChildren<TextMesh>().text = ""+(m.GetComponent<TrussMemberProperty>().number);
			text.GetComponentInChildren<TextMesh>().text +=" "+ m.GetComponent<TrussMemberProperty>().node1.number;
			text.GetComponentInChildren<TextMesh>().text +=" "+ m.GetComponent<TrussMemberProperty>().node2.number;
			text.GetComponentInChildren<TextMesh>().text +=" "+(m.GetComponent<TrussMemberProperty>().lenght());
			text.GetComponentInChildren<TextMesh>().text +=" "+ m.GetComponent<TrussMemberProperty>().prop;
			text.transform.SetParent (canvas.transform);
		}

	}

}
