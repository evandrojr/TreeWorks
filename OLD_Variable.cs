using System;
using System.Collections;
using System.Windows.Forms;

namespace Spartacus
{
	/// <summary>
	/// Summary description for Variable.
	/// </summary>
	public class Variable
	{

		public string Name;
		public TypeEnum Type;
		private static string dependentVariable="";
		private static int dependentVariableIndex=-1;
		private PhysicalColumn.DataTypeEnum dataType;
		public bool enabled = true;

		public Variable(string name, TypeEnum type, PhysicalColumn.DataTypeEnum _dataType)
		{
			Name = name;
			Type = type;
			dataType = _dataType;
			Tree.VariableL.Add(this);
		}

		public enum TypeEnum : byte
		{
			Continuous,
            Categorical,
			Ordered,
		}

		public PhysicalColumn.DataTypeEnum DataType
		{
			get{return dataType;}
		}

		public static int DependentVariableIndex
		{
			get
			{
				if(Variable.dependentVariableIndex == -1)
				{
					MessageBox.Show("Error dependentVariableIndex is not set yet", "Variable.cs");
					
				}
				return Variable.dependentVariableIndex;
			}
		}

		private static void setDependentVariableIndex()
		{
				Variable var;
				for(int x=0; x < Tree.VariableL.Count; ++x)
				{
					var = (Variable) Tree.VariableL[x];
					if(var.Name.CompareTo(DependentVariable) == 0)
					{
						dependentVariableIndex = x;
						return;
					}
				}
				MessageBox.Show("Error could not find the DependentVariableIndex ", "Node.cs");
								
		}


		public static string DependentVariable
		{
			set
			{
				if(value.Length > 0)
				{
					dependentVariable = value;
					setDependentVariableIndex();
				}
				else
					MessageBox.Show("Empty DependentVariable in Variable.cs!");
			}
			get
			{
				return dependentVariable;
			}
		}

	}
}
