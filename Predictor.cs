#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace Spartacus {
    public class Predictor {

        public SchemaVariable Variable;
        Node node;
        public SortedList<string, int> ValueSd; //Value, frequency (used only for categorical variables)
        public double Gain;
        public double ImpUniMin;
        public double ImpMvMin;
        public double SplitValue; //For continuos information when possible it is the bisector angle of the value before and the next value
//        public double SplitValueOriginal; //The real value before finding the bisector angle
        public ValueGroup ChildrenGroups = null; //Contain values that will split the node (only if the variable is categorical)
        public SplitStatusEnum SplitStatus;
        public bool CustomisedSplit = false;
        private double lowerNumer;  //Used for continous variables
        private double higherNumer; //Used for continous variables
        private int distinctValuesCount = -1;
        private int nullCount = -1;
        public int PredictorLstIdx; // The index for the Node.PredictorLst
        public int PredMvBase; //The first multivariate predictor it is related, if it is numeric will be exactly that one

        public int DistinctValuesCount {
            get {
                if (Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Categorical)
                    return ValueSd.Count;
                else
                    return distinctValuesCount;           
            }
            set { // Used only for continuos variables
                distinctValuesCount = value;
            }
        }

        public int NullCount {
            get { return nullCount;    }
            set { nullCount = value;   }
        }


        public double LowerNumber {
            get {return lowerNumer; }
        }

        public double HigherNumber {
            get {return higherNumer; }
        }

        public void SetLowerAndHigher(double l, double h){
            lowerNumer = l;
            higherNumer = h;
        }

        public Predictor(SchemaVariable var, Node _node, int predictorLstIdx) {
            ChildrenGroups = new ValueGroup(this, 2);
            Variable = var;
            node = _node;
            PredictorLstIdx = predictorLstIdx;
            ValueSd = new SortedList<string,int>();
        }

        public SchemaVariable.DataTypeEnum DataType {
            get { return Variable.DataType; }
        }

        public int Frequency(string _case) {
            return ValueSd[_case];
        }

        public enum SplitStatusEnum : byte {
            CanBeUsed,
            NotEnoughCases,
            TooManyValuesToSearch,
            OnlyOneValueAvailable,
            OneClassNode,
        }

    }
}
