using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamCollector : MonoBehaviour {

    public GameObject memberPrefab,textPrefab,nodePrefab,pointLoadPrefab,momentumPrefab,uniformLoadPrefab;

    public List<GameObject> members, nodes;

    float currentPoint = 0;

	// Use this for initialization
	void Start () {
        members = new List<GameObject>();
        nodes = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddMember(float span,int type)
    {
        Debug.Log("Add Member { "+"Span : "+span+" Type : "+type+" }");

        GameObject member = Instantiate(memberPrefab, Vector3.zero, Quaternion.identity);

        LineRenderer line = member.GetComponent<LineRenderer>();
        line.startColor = GetColor(members.Count);
        line.endColor = GetColor(members.Count);
        line.SetPositions(new Vector3[] { new Vector3(currentPoint, 0), new Vector3(currentPoint + span, 0) });

        MemberProperty property = member.GetComponent<MemberProperty>();
        property.type = type;
        property.length = span;
        property.number = members.Count;
        property.origin = currentPoint;

        GameObject lengthText = Instantiate(textPrefab, new Vector3(currentPoint + span / 2f, -0.5f), Quaternion.identity);
        lengthText.GetComponent<TextMesh>().text = span + " m.";
        lengthText.transform.SetParent(member.transform);

        GameObject numberText = Instantiate(textPrefab, new Vector3(currentPoint + span / 2f, 0,-1f), Quaternion.identity);
        numberText.GetComponent<TextMesh>().text = members.Count + "";
        numberText.GetComponent<TextMesh>().color = Color.white;
        numberText.transform.SetParent(member.transform);

        if (members.Count == 0) CreateNode(member.transform, currentPoint);
        CreateNode(member.transform, currentPoint + span);

        currentPoint += span;

        member.transform.SetParent(transform); 
        members.Add(member);

    }

    public void AddSupport(int type,int node)
    {
        Debug.Log("Add Support { " + "type : " + type + " Node : " + node + " }");
    }

    public void AddPointLoad(int node, float load)
    {
        Debug.Log("Add Point Load { " + "node : " + node + " load : " + load + " }");
        GameObject selectNode = nodes[node];
        GameObject pointLoad = Instantiate(pointLoadPrefab, selectNode.transform.position + new Vector3(0, 1), Quaternion.identity);

        pointLoad.GetComponentInChildren<TextMesh>().text = load + " N.";
        pointLoad.GetComponent<PointLoadProperty>().load = load;
        pointLoad.GetComponent<PointLoadProperty>().node = node;
        selectNode.GetComponent<NodeProperty>().pointLoad = pointLoad.GetComponent<PointLoadProperty>();

        pointLoad.transform.SetParent(selectNode.transform);
    }

    public void AddUniformLoad(int element,float load)
    {
        Debug.Log("Add Uniform Load { " + "load : " + load + " element : " + element + " }");

        GameObject selectedElement = members[element];
        GameObject uniformLoad = Instantiate(uniformLoadPrefab, new Vector3(selectedElement.GetComponent<MemberProperty>().origin+ selectedElement.GetComponent<MemberProperty>().length/2,1f), Quaternion.identity);

        uniformLoad.GetComponent<SpriteRenderer>().size = new Vector3(uniformLoad.GetComponent<SpriteRenderer>().size.x*selectedElement.GetComponent<MemberProperty>().length, uniformLoad.GetComponent<SpriteRenderer>().size.y);

        uniformLoad.GetComponent<UniformLoadProperty>().load = load;
        uniformLoad.GetComponent<UniformLoadProperty>().element = element;
        uniformLoad.GetComponentInChildren<TextMesh>().text = load + " N/m.";
        selectedElement.GetComponent<MemberProperty>().uniformLoad = uniformLoad.GetComponent< UniformLoadProperty>();

        uniformLoad.transform.SetParent(selectedElement.transform);
    }

    public void AddMomentum(int node,float momentum)
    {
        Debug.Log("Add Momentum { " + "Momentum : " + momentum + " node : " + node + "} ");

        GameObject selectNode = nodes[node];
        GameObject momentumObj = Instantiate(momentumPrefab, selectNode.transform.position-new Vector3(0,0.75f,0f), Quaternion.identity);

        momentumObj.GetComponentInChildren<TextMesh>().text = momentum + " N.m";
        momentumObj.GetComponent<MomentumProperty>().node = node;
        momentumObj.GetComponent<MomentumProperty>().momentum = momentum;
        momentumObj.GetComponent<MomentumProperty>().UpdateDirection();
        selectNode.GetComponent<NodeProperty>().momentum = momentumObj.GetComponent<MomentumProperty>();

        momentumObj.transform.SetParent(selectNode.transform);
    }

    public Color GetColor(int x)
    {
        if (x % 2 == 0) return new Color(169 / 255f, 169 / 255f, 169 / 255f);
        return new Color(112 / 255f, 128 / 255f, 144 / 255f);
    }

    public void CreateNode(Transform parent,float position)
    {
        GameObject node = Instantiate(nodePrefab, new Vector3(position, 0.75f), Quaternion.identity);
        node.GetComponent<NodeProperty>().number = nodes.Count;
        node.GetComponentInChildren<TextMesh>().text = nodes.Count + "";
        node.transform.SetParent(parent);
        nodes.Add(node);
    }
}
