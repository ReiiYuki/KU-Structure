using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamCollector : Collector {

    public GameObject memberPrefab,textPrefab,nodePrefab,pointLoadPrefab,momentumPrefab,uniformLoadPrefab;
    public GameObject[] supportPrefabs;

    public List<GameObject> members, nodes;
    public List<PointLoadProperty> pointLoads;
    public List<UniformLoadProperty> uniformLoads;
    public List<GameObject> history;

    float currentPoint = 0;

	// Use this for initialization
	void Start () {
        members = new List<GameObject>();
        nodes = new List<GameObject>();
        pointLoads = new List<PointLoadProperty>();
        uniformLoads = new List<UniformLoadProperty>();
        history = new List<GameObject>();
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
        property.node1 = nodes[nodes.Count - 1].GetComponent<NodeProperty>();
        property.node1.GetComponent<NodeProperty>().rightMember = property;
        CreateNode(member.transform, currentPoint + span);
        property.node2 = nodes[nodes.Count - 1].GetComponent<NodeProperty>();
        property.node2.GetComponent<NodeProperty>().leftMember = property;

        currentPoint += span;

        member.transform.SetParent(transform); 
        members.Add(member);

        history.Add(member);
    }

    public void AddSupport(int type,int node)
    {
        Debug.Log("Add Support { " + "type : " + type + " Node : " + node + " }");

        GameObject selectedNode = nodes[node];
        GameObject support;
        if (type == 0)
        {
            support = Instantiate(supportPrefabs[type], selectedNode.transform.position - new Vector3(-0.25f, 0.75f), Quaternion.identity);
            support.transform.Rotate(new Vector3(0, 0,-90f));
        }
        else if (type == 3 || type == 4)
        {
            support = Instantiate(supportPrefabs[type], selectedNode.transform.position - new Vector3(0, 0.4f), Quaternion.identity);
        }
        else
        {
            support = Instantiate(supportPrefabs[type], selectedNode.transform.position - new Vector3(0, 1.35f), Quaternion.identity);
        }

        selectedNode.GetComponent<NodeProperty>().dy = support.GetComponent<SupportProperty>().dy;
        selectedNode.GetComponent<NodeProperty>().m = support.GetComponent<SupportProperty>().m;
        selectedNode.GetComponent<NodeProperty>().support = support.GetComponent<SupportProperty>();
        support.GetComponent<SupportProperty>().node = node;

        support.transform.SetParent(selectedNode.transform);

        history.Add(support);

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

        pointLoad.GetComponent<PointLoadProperty>().Inverse();

        pointLoad.transform.SetParent(selectNode.transform);
        pointLoads.Add(pointLoad.GetComponent<PointLoadProperty>());

        history.Add(pointLoad);

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

        uniformLoad.GetComponent<UniformLoadProperty>().Inverse();

        uniformLoad.transform.SetParent(selectedElement.transform);

        uniformLoads.Add(uniformLoad.GetComponent<UniformLoadProperty>());

        history.Add(uniformLoad);

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

        history.Add(momentumObj);

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

    override
    public void Undo()
    {
        Debug.Log(history.Count);
        if (history.Count == 0) return;
        GameObject obj = history[history.Count - 1];
        if (members.IndexOf(obj) >= 0)
        {
            nodes.RemoveAt(obj.GetComponent<MemberProperty>().node2.number);
            if (members.Count == 1)
                nodes.RemoveAt(obj.GetComponent<MemberProperty>().node1.number);    
            members.Remove(obj);
        }
        else if (obj.GetComponent<PointLoadProperty>())
            pointLoads.Remove(obj.GetComponent<PointLoadProperty>());
        else if (obj.GetComponent<UniformLoadProperty>())
            uniformLoads.Remove(obj.GetComponent<UniformLoadProperty>());
        history.Remove(obj);
        DestroyObject(obj);
        Debug.Log("History : " + history.Count);
        Debug.Log("Member : " + members.Count);
        Debug.Log("Node : " + nodes.Count);
        Debug.Log("Uniform Load : " + uniformLoads.Count);
        Debug.Log("Point Load : " + pointLoads.Count);
    }

    public void ResetCollector()
    {
        while (history.Count > 0)
            Undo();
    }
}
