using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRUSSCollector : MonoBehaviour {
	
	public GameObject memberPrefab,textPrefab,nodePrefab,pointLoadPrefabX,pointLoadPrefabY,momentumPrefab,uniformLoadPrefab;
    public List<GameObject> nodes,members;
	public GameObject[] supportPrefabs;
	// Use this for initialization
	void Start () {
        this.nodes = new List<GameObject>();
        this.members = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddNode(int x,int y)
    {
        Debug.Log("Add Node { x:" + x + " y : " + y + " }");

		GameObject node = Instantiate(nodePrefab, new Vector3(x, y), Quaternion.identity);

		node.GetComponent<NodeProperty>().number = nodes.Count;
		node.GetComponentInChildren<TextMesh>().text = nodes.Count + "";

		nodes.Add(node);
    }

    public void AddMember(int node1,int node2,int property)
    {
		float node1X = nodes [node1].transform.position.x;
		float node1Y = nodes [node1].transform.position.y;
		float node2X = nodes [node2].transform.position.x;
		float node2Y = nodes [node2].transform.position.y;
        Debug.Log("Add Member { node1 : " + node1 + " node2 : " + node2 + " property : " + property + " }");

		GameObject member = Instantiate(memberPrefab, Vector3.zero, Quaternion.identity);

		LineRenderer line = member.GetComponent<LineRenderer>();
		line.startColor = GetColor(members.Count);
		line.endColor = GetColor(members.Count);
		line.SetPositions(new Vector3[] { new Vector3(node1X, node1Y), new Vector3(node2X, node2Y) });

		MemberProperty memberProperty = member.GetComponent<MemberProperty>();
		memberProperty.type = property;
		memberProperty.length = node2X-node1X;
		memberProperty.number = members.Count;
		memberProperty.origin = node1X;

		GameObject lengthText = Instantiate(textPrefab, new Vector3((node1X+node2X) / 2f, -0.5f), Quaternion.identity);
		lengthText.GetComponent<TextMesh>().text = (node2X-node1X) + " m.";
		lengthText.transform.SetParent(member.transform);

		GameObject numberText = Instantiate(textPrefab, new Vector3(node2X / 2f, 0,-1f), Quaternion.identity);
		numberText.GetComponent<TextMesh>().text = members.Count + "";
		numberText.GetComponent<TextMesh>().color = Color.white;
		numberText.transform.SetParent(member.transform);



		member.transform.SetParent(transform); 
		members.Add(member);
    }

    public void AddSupport(int type,int node)
    {
        Debug.Log("Add Support { type : " + type + " node : " + node + " }");

		GameObject selectedNode = nodes[node];
		GameObject support;
		if (type == 0)
		{
			support = Instantiate(supportPrefabs[type], selectedNode.transform.position - new Vector3(-0.25f, 0.75f), Quaternion.identity);
			support.transform.Rotate(new Vector3(0, 0,-90f));
		}
		else if (type == 3)
		{
			support = Instantiate(supportPrefabs[type], selectedNode.transform.position - new Vector3(0, 0.4f), Quaternion.identity);
		}
		else
		{
			support = Instantiate(supportPrefabs[type], selectedNode.transform.position - new Vector3(0, 1.35f), Quaternion.identity);
		}

		selectedNode.GetComponent<NodeProperty>().dy = support.GetComponent<SupportProperty>().dy;
		selectedNode.GetComponent<NodeProperty>().m = support.GetComponent<SupportProperty>().m;
		support.GetComponent<SupportProperty>().node = node;

		support.transform.SetParent(selectedNode.transform);
	}

    public void AddPointLoad(int node,float loadX,float loadY)
    {
        Debug.Log("Add Point Load { node : " + node + " loadX : " + loadX + " loadY : " + loadY + " }");
		GameObject selectNode = nodes [node];

		// add load X
		if (loadX != 0) {
			GameObject pointLoadX = Instantiate (pointLoadPrefabX, selectNode.transform.position + new Vector3 ((nodes [node].transform.position.x), (nodes [node].transform.position.y)), Quaternion.identity);
			pointLoadX.transform.Rotate (new Vector3 (0, 0, 90));
			pointLoadX.GetComponentInChildren<TextMesh> ().text = loadX + " N.";
			pointLoadX.GetComponent<TrussPointLoadProperty> ().loadX = loadX;
			pointLoadX.GetComponent<TrussPointLoadProperty> ().loadY = 0;
			pointLoadX.GetComponent<TrussPointLoadProperty> ().node = node;
			selectNode.GetComponent<NodeProperty> ().pointLoad = pointLoadX.GetComponent<PointLoadProperty> ();

			//pointLoadX.GetComponent<PointLoadProperty> ().Inverse ();

			pointLoadX.transform.SetParent (selectNode.transform);
		}
		// add Load Y
		if (loadY != 0) {
			GameObject pointLoadY = Instantiate (pointLoadPrefabY, selectNode.transform.position + new Vector3 ((nodes [node].transform.position.x + 1), (nodes [node].transform.position.y)), Quaternion.identity);

			pointLoadY.GetComponentInChildren<TextMesh> ().text = loadY + " N.";
			pointLoadY.GetComponent<TrussPointLoadProperty> ().loadX = 0;
			pointLoadY.GetComponent<TrussPointLoadProperty> ().loadY = loadY;
			pointLoadY.GetComponent<TrussPointLoadProperty> ().node = node;
			selectNode.GetComponent<NodeProperty> ().pointLoad = pointLoadY.GetComponent<PointLoadProperty> ();

			//pointLoadY.GetComponent<PointLoadProperty> ().Inverse ();

			pointLoadY.transform.SetParent (selectNode.transform);
			Debug.Log ("in");
		}
    }

	public Color GetColor(int x)
	{
		if (x % 2 == 0) return new Color(169 / 255f, 169 / 255f, 169 / 255f);
		return new Color(112 / 255f, 128 / 255f, 144 / 255f);
	}
}
