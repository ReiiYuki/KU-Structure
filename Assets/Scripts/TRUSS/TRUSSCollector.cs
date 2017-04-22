using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRUSSCollector : MonoBehaviour {
	
	public GameObject memberPrefab,textPrefab,nodePrefab,pointLoadPrefabX,pointLoadPrefabY,momentumPrefab,uniformLoadPrefab;
    public List<GameObject> nodes,members;
	public GameObject[] supportPrefabs;
    public TrussAnalyzer  trussAnalyzer= new TrussAnalyzer();
	// Use this for initialization
	void Start () {
        this.nodes = new List<GameObject>();
        this.members = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void helpper()
    {
        AddNode(0,0);
        AddNode(12, 0);
        AddNode(24, 0);
        AddNode(12, 16);
        AddMember(0, 3, 0);
        AddMember(1, 3, 0);
        AddMember(2, 3, 0);
        AddSupport(1, 0);
        AddSupport(1, 1);
        AddSupport(1, 2);
        AddPointLoad(3, 150, 300);
        Debug.Log(members.Count);
    }
    public void AddNode(int x,int y)
    {
        Debug.Log("Add Node { x:" + x + " y : " + y + " }");
        // create a new node
		GameObject node = Instantiate(nodePrefab, new Vector3(x, y), Quaternion.identity);

        // init node vriable
        node.GetComponent<TrussNodeProperty>().x = x;
        node.GetComponent<TrussNodeProperty>().y = y;
		node.GetComponent<TrussNodeProperty>().number = nodes.Count;
		node.GetComponentInChildren<TextMesh>().text = nodes.Count + "";

		nodes.Add(node);
    }

    public void AddMember(int node1,int node2,int property)
    {
        // get all the position of the node
		float node1X = nodes [node1].transform.position.x;
		float node1Y = nodes [node1].transform.position.y;
		float node2X = nodes [node2].transform.position.x;
		float node2Y = nodes [node2].transform.position.y;
        Debug.Log("Add Member { node1 : " + node1 + " node2 : " + node2 + " property : " + property + " }");

        // create a new member
		GameObject member = Instantiate(memberPrefab, Vector3.zero, Quaternion.identity);
        // draw line
        LineRenderer line = member.GetComponent<LineRenderer>();
		line.startColor = GetColor(members.Count);
		line.endColor = GetColor(members.Count);
		line.SetPositions(new Vector3[] { new Vector3(node1X, node1Y), new Vector3(node2X, node2Y) });

        // init member variable
		TrussMemberProperty memberProperty = member.GetComponent<TrussMemberProperty>();
		memberProperty.type = property;
		memberProperty.number = members.Count;
        memberProperty.node1 = nodes[node1].GetComponent<TrussNodeProperty>();
        memberProperty.node2 = nodes[node2].GetComponent<TrussNodeProperty>();

        // add member to node
        nodes[node1].GetComponent<TrussNodeProperty>().members.Add(memberProperty);
        nodes[node2].GetComponent<TrussNodeProperty>().members.Add(memberProperty);

        // draw the length of the member
        GameObject lengthText = Instantiate(textPrefab, new Vector3((node1X + node2X) / 2f + .5f, (node1Y + node2Y) / 2f +.5f), Quaternion.identity);
		lengthText.GetComponent<TextMesh>().text = memberProperty.lenght() + " m.";
		lengthText.transform.SetParent(member.transform);

        // draw a number of the member
		GameObject numberText = Instantiate(textPrefab, new Vector3((node1X+node2X) / 2f, (node1Y+node2Y)/2f), Quaternion.identity);
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

        // init support variable
		support.GetComponent<TrussSupportProperty>().node = nodes[node].GetComponent<TrussNodeProperty>();

        // add degree of freedom to node
        nodes[node].GetComponent<TrussNodeProperty>().dx = 1;
        nodes[node].GetComponent<TrussNodeProperty>().dy = 1;

        // add support to node
        nodes[node].GetComponent<TrussNodeProperty>().support = support.GetComponent<TrussSupportProperty>();

        support.transform.SetParent(selectedNode.transform);
	}

    public void AddPointLoad(int node,float loadX,float loadY)
    {
        Debug.Log("Add Point Load { node : " + node + " loadX : " + loadX + " loadY : " + loadY + " }");
		GameObject selectNode = nodes [node];

		// add load X
		if (loadX != 0) {
			GameObject pointLoadX = Instantiate (pointLoadPrefabX,  new Vector3 ((nodes [node].transform.position.x+1.25f), (nodes [node].transform.position.y)), Quaternion.identity);
			pointLoadX.transform.Rotate (new Vector3 (0, 0, -90));
			pointLoadX.GetComponentInChildren<TextMesh> ().text = loadX + " N.";
            pointLoadX.GetComponent<TrussPointLoadProperty>().load = loadX;
            pointLoadX.GetComponent<TrussPointLoadProperty>().axis = 'x';
            pointLoadX.GetComponent<TrussPointLoadProperty> ().node = node;
			selectNode.GetComponent<TrussNodeProperty> ().pointLoadX = pointLoadX.GetComponent<TrussPointLoadProperty> ();

			pointLoadX.GetComponent<TrussPointLoadProperty> ().Inverse ();

			pointLoadX.transform.SetParent (selectNode.transform);
		}
        else
        {
            selectNode.GetComponent<TrussNodeProperty>().pointLoadX = new TrussPointLoadProperty();
        }
		// add Load Y
		if (loadY != 0) {
			GameObject pointLoadY = Instantiate (pointLoadPrefabY, new Vector3 ((nodes [node].transform.position.x), (nodes [node].transform.position.y +1.25f)), Quaternion.identity);

			pointLoadY.GetComponentInChildren<TextMesh> ().text = loadY + " N.";

			pointLoadY.GetComponent<TrussPointLoadProperty> ().load = loadY;
            pointLoadY.GetComponent<TrussPointLoadProperty>().axis = 'y';
            pointLoadY.GetComponent<TrussPointLoadProperty> ().node = node;
			selectNode.GetComponent<TrussNodeProperty> ().pointLoadY = pointLoadY.GetComponent<TrussPointLoadProperty> ();

			pointLoadY.GetComponent<TrussPointLoadProperty> ().Inverse ();

			pointLoadY.transform.SetParent (selectNode.transform);
		}
        else
        {
            selectNode.GetComponent<TrussNodeProperty>().pointLoadY = new TrussPointLoadProperty();
        }
    }

	public Color GetColor(int x)
	{
		if (x % 2 == 0) return new Color(169 / 255f, 169 / 255f, 169 / 255f);
		return new Color(112 / 255f, 128 / 255f, 144 / 255f);
	}
    
}
