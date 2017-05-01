using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadElementToTable : MonoBehaviour {

    public GameObject HBeamTable, HBeamPrefab;

	// Use this for initialization
	void Start () {
        LoadHBeam();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void LoadHBeam()
    {
        foreach (ElementStore.Element e in ElementStore.H_BEAM)
        {
            GameObject content = Instantiate(HBeamPrefab, HBeamTable.transform);
            content.transform.GetChild(0).GetComponentInChildren<Text>().text = e.name;
            content.transform.GetChild(1).GetComponentInChildren<Text>().text = e.w+"";
            content.transform.GetChild(2).GetComponentInChildren<Text>().text = e.h+"";
            content.transform.GetChild(3).GetComponentInChildren<Text>().text = e.b+"";
            content.transform.GetChild(4).GetComponentInChildren<Text>().text = e.t1+"";
            content.transform.GetChild(5).GetComponentInChildren<Text>().text = e.t2+"";
            content.transform.GetChild(6).GetComponentInChildren<Text>().text = e.r+"";
            content.transform.GetChild(7).GetComponentInChildren<Text>().text = e.area+"";
            content.transform.GetChild(8).GetComponentInChildren<Text>().text = e.ix+"";
            content.transform.GetChild(9).GetComponentInChildren<Text>().text = e.iy+"";
            content.transform.GetChild(10).GetComponentInChildren<Text>().text = e.rx+"";
            content.transform.GetChild(11).GetComponentInChildren<Text>().text = e.ry+"";
            content.transform.GetChild(12).GetComponentInChildren<Text>().text = e.zx+"";
            content.transform.GetChild(13).GetComponentInChildren<Text>().text = e.zy+"";
            content.transform.GetChild(14).GetComponentInChildren<Text>().text = e.rt+"";
            content.transform.GetChild(15).GetComponentInChildren<Text>().text = e.c+"";
            content.transform.GetChild(16).GetComponentInChildren<Text>().text = e.k+"";
            content.transform.GetChild(17).GetComponentInChildren<Text>().text = e.e+"";
            content.transform.GetChild(18).GetComponentInChildren<Text>().text = e.fb+"";
            content.transform.GetChild(19).GetComponentInChildren<Text>().text = e.cb+"";
            content.transform.GetChild(20).GetComponentInChildren<Text>().text = e.fy+"";
            content.transform.GetChild(21).GetComponentInChildren<Text>().text = e.bf+"";

        }
    }
}
