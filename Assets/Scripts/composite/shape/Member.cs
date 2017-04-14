using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class Member : Shape, MonoBehaviour
	{
		private Node node1;
		private Node node2;
		private MemberProperty property;
		public GameObject memberPrefab;
		public Member (Node node1, Node node2, MemberProperty property)
		{
			this.node1 = node1;
			this.node2 = node2;
			this.property = property;
		}
		public override void draw()
		{
			GameObject member = Instantiate(memberPrefab, Vector3.zero, Quaternion.identity);

			LineRenderer line = member.GetComponent<LineRenderer>();
			line.SetPositions(new Vector3[] { new Vector3(node1.getX(), node1.getY()), new Vector3(node2.getX(),node2.getY()) });
		}
	}
}

