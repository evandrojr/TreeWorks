using System;
using System.Collections;

namespace Spartacus
{
	/// <summary>
	/// Summary description for Case.
	/// </summary>
	public class Case : IComparable
	{
		
		public object Val;
		public int Frequency;

		public Case(object val)
		{
			Val=val;
			Frequency=1;
		}

		int IComparable.CompareTo(object a)
		{
				Case c=(Case)a;
				
				return Val.ToString().CompareTo(c.Val.ToString());
		}
		
	}
}
