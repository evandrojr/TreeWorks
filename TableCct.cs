using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Spartacus
{
	/// <summary>
	/// Summary description for TableCct.
	/// </summary>
    /// 
/*
	public class TableCct // Concrete table
	{
        public List<VariableCct> VariableLst = new List<VariableCct>(); //All the variables
        private VariableCct target;// Only the depenmdent variable
        public List<VariableCct> PredictorLst = new List<VariableCct>(); //Only the Predictors variable
        public Tree Tree; 
        private int targetIndex; //The index of the target variable in the VariableLst

        private int variableCount, instanceCount;

        public TableCct() {
        }

        public void SetDimensions(int _variableCount, int _instanceCount) {
            variableCount = _variableCount; instanceCount = _instanceCount;
        }

		public void AddVariable(string VariableName, VariableCct.DataTypeEnum dataType)
		{
            VariableCct v = new VariableCct(VariableName, instanceCount, dataType);
            VariableLst.Add(v);
        }

        public VariableCct Target {
            get { return target; }
            set {
                target = value;
                if (target.VariableTypeUserSet == VariableCct.VariableTypeEnum.Categorical)
                    Tree.NodeTargetType = Tree.NodeTargetTypeEnum.TargetCategorical;
                else
                    Tree.NodeTargetType = Tree.NodeTargetTypeEnum.TargetContinuous;
                targetIndex = VariableLst.IndexOf(target);
            }
        }

        public int TargetIndex {
            get { return targetIndex; }
        }

        public void AddPredictor(VariableCct Predictor)
        {
            PredictorLst.Add(Predictor);
        }

        public int VariableCount{get{ return variableCount;}}
		public int InstanceCount{get{ return instanceCount;}}

		public string GetText(int x, int y)
		{
			return VariableLst[x].Instance.GetText(y);
		}

		public double GetNumber(int x, int y)
		{
			return VariableLst[x].Instance.GetNumber(y);
		}

		public object this[int x, int y]
		{
            set
            {
                if (this.VariableLst[x].DataType == VariableCct.DataTypeEnum.Number)
                {
                    if (value is DBNull)
                        VariableLst[x].Instance.SetNumber(Def.NullNumber, y);
                    else
                        VariableLst[x].Instance.SetNumber(Convert.ToDouble(value), y);
                }
                else
                if (this.VariableLst[x].DataType == VariableCct.DataTypeEnum.Text)
                {
                    if (value is DBNull)
                        VariableLst[x].Instance.SetText(Def.NullText, y);
                    else{
                        string v;
                        v = Convert.ToString(value);
                        if(v == "")
                            VariableLst[x].Instance.SetText(Def.NullText, y);
                        else
                            VariableLst[x].Instance.SetText(v, y);
                    }
                }
                else
                    throw new InvalidOperationException("This value '" + value + "' (" + x + ", " + y + ") has no DataType defined");
            }
        }
	}

	public class VariableCct
	{
		public string Name;
		public DataTypeEnum DataType;
        public VariableTypeEnum VariableTypeDetected;
        public VariableTypeEnum VariableTypeUserSet;
        public VariableRoleEnum VariableRole;
        public InstanceCct Instance;

        public VariableCct(string VariableName, int instanceCount, VariableCct.DataTypeEnum dataType)
        {
			Name = VariableName;
			DataType = dataType;
            if (DataType == DataTypeEnum.Number)
            {
                VariableTypeDetected = VariableTypeEnum.Continuous;
                VariableTypeUserSet = VariableTypeEnum.Continuous;
            }
            else
            {
                VariableTypeDetected = VariableTypeEnum.Categorical;
                VariableTypeUserSet = VariableTypeEnum.Categorical;
            }
            Instance = new InstanceCct(instanceCount, DataType); 			
 		}

		public enum DataTypeEnum : byte
		{
			Number = 0,
			Text,
		}

        public enum VariableTypeEnum : byte
        {
            Continuous = 0,
            Categorical,
        }

        public enum VariableRoleEnum : byte {
            Target = 0,
            Predictor,
            NotUsed, 
        }

    }

	public class InstanceCct
	{
        //In this case array seems to be more appropriated
        public string[] TextData;
		public double[] NumericData;
		
		public InstanceCct(int instanceCount, VariableCct.DataTypeEnum dataType)
		{
			if(dataType == VariableCct.DataTypeEnum.Number)
				NumericData = new double[instanceCount];
			else
				TextData = new string[instanceCount];
		}
		public string GetText(int y) {return TextData[y];}
		public double GetNumber(int y) {return NumericData[y];}
		
		public void SetText(string val, int y) {TextData[y]=val;}
		public void SetNumber(double val, int y) {NumericData[y]=val;}
	}

*/

}
