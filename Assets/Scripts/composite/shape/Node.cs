using System;

namespace AssemblyCSharp
{
	public class Node : Shape
	{
		private String name;
		public Node (float x,float y, string name)
		{
			this.name = name;
			this.x = x;
			this.y = y;
		}
		public override void draw(){
			// TODO
		}
	}
}

