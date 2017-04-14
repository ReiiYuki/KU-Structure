using System;

namespace AssemblyCSharp
{
	public class Member : Shape
	{
		private Node node1;
		private Node node2;
		private MemberProperty property;
		public Member (Node node1, Node node2, MemberProperty property)
		{
			this.node1 = node1;
			this.node2 = node2;
			this.property = property;
		}
		public override void draw()
		{
			// TODO
		}
	}
}

