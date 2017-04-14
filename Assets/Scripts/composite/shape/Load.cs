using System;

namespace AssemblyCSharp
{
	public class Load : Shape
	{
		private Node node;
		private double loadX;
		private double loadY;
		public Load (Node node,double loadX, double loadY)
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

