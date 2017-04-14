using System;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class CompositeShape
	{	
		private List<Shape> shapes = new List<Shape>();
		public CompositeShape ()
		{
			
		}
		public void add(Shape shape){
			shapes.Add (shape);
		}
		public void remove(Shape shape){
			shapes.Remove (shape);
		}
	}
}

