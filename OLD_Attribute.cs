using System;
using System.Collections;

namespace Spartacus
{
	/// <summary>
	/// Summary description for Attribute.
	/// </summary>
	public class Attribute
	{
		public ArrayList CaseL = new ArrayList();
		private int variableIndex;

		public Attribute()
		{
			variableIndex=-1;
		}

		public Attribute(int _variableIndex) : this()
		{
			variableIndex = _variableIndex; 
		}
		
		public void CaseSort()
		{
			CaseL.Sort();			
		}

		public string Name
		{
			get{
				Variable v = (Variable) Tree.VariableL[variableIndex];
				return v.Name;
				}
		}
		public Variable.TypeEnum Type
		{
			get
			{
				Variable v = (Variable) Tree.VariableL[variableIndex];
				return v.Type;
			}
		}

		public int VariableIndex{get{ return variableIndex;}}

		public void AddCase(object Case)
		{
			Case cs;
			for(int i=0; i < CaseL.Count; ++i)
			{
				cs = (Case) CaseL[i];
				if(cs.Val.ToString() == Case.ToString())
				{
					++cs.Frequency;
					return;
				}
			}
			cs = new Case(Case);
			CaseL.Add(cs);
			return;
		}


	}
}
