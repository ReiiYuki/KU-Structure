using System;

namespace AssemblyCSharp
{
	public class Load : Shape
	{
		private Node node;
		private float loadX;
		private float loadY;
		public Load (Node node,float loadX, float loadY)
		{
			this.node = node;
			this.x = node.getX ();
			this.y = node.getY ();
			this.loadX = loadX;
			this.loadY = loadY;
		}

		public override void draw ()
		{
			// TODO 
		}
	}
}

