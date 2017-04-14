using System;

namespace AssemblyCSharp
{
	public abstract class Shape
	{
		protected float x;
		protected float y;
		public float getX()
		{
			return x;
		}
		public float getY()
		{
			return y;
		}
		public abstract void draw();
	}
}

