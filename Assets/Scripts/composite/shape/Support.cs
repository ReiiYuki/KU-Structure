using System;

namespace AssemblyCSharp
{
	public class Support : Shape
	{	
		private Node node;
		private SupportProperty property;
		public Support (Node node, SupportProperty property)
		{
			this.node = node;
			this.x = node.getX ();
			this.y = node.getY ();
			this.property = property;
		}

		public override void draw ()
		{
			// TODO 
		}
	}
}

