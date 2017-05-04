using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadElementToTable : MonoBehaviour {

    public GameObject HBeamTable, HBeamPrefab,IBeamTable,IBeamPrefab,PIPETable,PIPEPrefab,BUTable,BUPrefab,TUTable,TUPrefab;

	// Use this for initialization
	void Start () {
        LoadHBeam();
        LoadIBeam();
        LoadPIPE();
        LoadBU();
        LoadTU();
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
        HBeamTable.GetComponent<LayoutElement>().preferredHeight = HBeamTable.transform.childCount * 49;
        HBeamTable.GetComponent<LayoutElement>().preferredWidth = HBeamTable.transform.GetChild(1).childCount * 200;
        Debug.Log("Set up");
    }

    void LoadIBeam()
    {
        foreach (ElementStore.Element e in ElementStore.I_BEAM)
        {
            GameObject content = Instantiate(IBeamPrefab, IBeamTable.transform);
            content.transform.GetChild(0).GetComponentInChildren<Text>().text = e.name;
            content.transform.GetChild(1).GetComponentInChildren<Text>().text = e.w + "";
            content.transform.GetChild(2).GetComponentInChildren<Text>().text = e.h + "";
            content.transform.GetChild(3).GetComponentInChildren<Text>().text = e.b + "";
            content.transform.GetChild(4).GetComponentInChildren<Text>().text = e.t1 + "";
            content.transform.GetChild(5).GetComponentInChildren<Text>().text = e.t2 + "";
            content.transform.GetChild(6).GetComponentInChildren<Text>().text = e.r + "";
            content.transform.GetChild(7).GetComponentInChildren<Text>().text = e.area + "";
            content.transform.GetChild(8).GetComponentInChildren<Text>().text = e.ix + "";
            content.transform.GetChild(9).GetComponentInChildren<Text>().text = e.iy + "";
            content.transform.GetChild(10).GetComponentInChildren<Text>().text = e.rx + "";
            content.transform.GetChild(11).GetComponentInChildren<Text>().text = e.ry + "";
            content.transform.GetChild(12).GetComponentInChildren<Text>().text = e.zx + "";
            content.transform.GetChild(13).GetComponentInChildren<Text>().text = e.zy + "";
            content.transform.GetChild(14).GetComponentInChildren<Text>().text = e.rt + "";
            content.transform.GetChild(15).GetComponentInChildren<Text>().text = e.c + "";
            content.transform.GetChild(16).GetComponentInChildren<Text>().text = e.k + "";
            content.transform.GetChild(17).GetComponentInChildren<Text>().text = e.e + "";
            content.transform.GetChild(18).GetComponentInChildren<Text>().text = e.fb + "";
            content.transform.GetChild(19).GetComponentInChildren<Text>().text = e.cb + "";
            content.transform.GetChild(20).GetComponentInChildren<Text>().text = e.fy + "";
            content.transform.GetChild(21).GetComponentInChildren<Text>().text = e.bf + "";

        }
        HBeamTable.GetComponent<LayoutElement>().preferredHeight = IBeamTable.transform.childCount * 49;
        HBeamTable.GetComponent<LayoutElement>().preferredWidth = IBeamTable.transform.GetChild(1).childCount * 200;
        Debug.Log("Set up");
    }

    void LoadPIPE()
    {
        foreach (ElementStore.PElement e in ElementStore.PIPE)
        {
            GameObject content = Instantiate(PIPEPrefab, PIPETable.transform);
            content.transform.GetChild(0).GetComponentInChildren<Text>().text = e.name;
            content.transform.GetChild(1).GetComponentInChildren<Text>().text = e.outsideDiameter + "";
            content.transform.GetChild(2).GetComponentInChildren<Text>().text = e.thickness + "";
            content.transform.GetChild(3).GetComponentInChildren<Text>().text = e.weight + "";
            content.transform.GetChild(4).GetComponentInChildren<Text>().text = e.area + "";
            content.transform.GetChild(5).GetComponentInChildren<Text>().text = e.i + "";
            content.transform.GetChild(6).GetComponentInChildren<Text>().text = e.r + "";
            content.transform.GetChild(7).GetComponentInChildren<Text>().text = e.z + "";
            content.transform.GetChild(8).GetComponentInChildren<Text>().text = e.fb + "";
            content.transform.GetChild(9).GetComponentInChildren<Text>().text = e.fy + "";
            content.transform.GetChild(10).GetComponentInChildren<Text>().text = e.k + "";

        }
        PIPETable.GetComponent<LayoutElement>().preferredHeight = PIPETable.transform.childCount * 49;
        PIPETable.GetComponent<LayoutElement>().preferredWidth = 4400;
        Debug.Log("Set up");
    }

    void LoadBU()
    {
        foreach (ElementStore.UElement e in ElementStore.U_PROP)
        {
            GameObject content = Instantiate(BUPrefab, BUTable.transform);
            content.transform.GetChild(0).GetComponentInChildren<Text>().text = e.E+"";
            content.transform.GetChild(1).GetComponentInChildren<Text>().text = e.I + "";
        }
        BUTable.GetComponent<LayoutElement>().preferredHeight = BUTable.transform.childCount * 49;
        BUTable.GetComponent<LayoutElement>().preferredWidth = 4400;
        Debug.Log("Set up");
    }
    void LoadTU()
    {
        foreach (ElementStore.AElement e in ElementStore.UT_PROP)
        {
            GameObject content = Instantiate(TUPrefab, TUTable.transform);
            content.transform.GetChild(0).GetComponentInChildren<Text>().text = e.E + "";
            content.transform.GetChild(1).GetComponentInChildren<Text>().text = e.A + "";
        }
        TUTable.GetComponent<LayoutElement>().preferredHeight = TUTable.transform.childCount * 49;
        TUTable.GetComponent<LayoutElement>().preferredWidth = 4400;
        Debug.Log("Set up");
    }

}
