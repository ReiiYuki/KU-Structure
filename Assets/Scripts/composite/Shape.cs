using System;

namespace AssemblyCSharp
{
	public abstract class Shape
	{
		protected int x;
		protected int y;
		public int getX()
		{
			return x;
		}
		public int getY()
		{
			return y;
		}
		public abstract void draw();
	}
}

