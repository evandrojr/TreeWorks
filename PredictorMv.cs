#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace Spartacus {
    public class PredictorMv {

        public SchemaVariable Variable;
        public Node N;
        public MvTb Table;
        public double Gain;
        public double Coef;
        public SplitStatusEnum SplitStatus;
        public bool CustomisedSplit = false;
        public bool Optimised = false; // Tells if the best coeficient has been set
        public double LowerNumber;  //Used for continous variables
        public double HigherNumber; //Used for continous variables
        public int NullCount = -1;
        public int PredMvLstIdx;
        public int PredictorLstIdx;
        public int FieldSpan; // How many variables are required always 1 for numeric
        public int NativeIdx; // The index for the PC OR PN
        public int Offset; //
        //The variation of the index for this predictor ex:
        //Sex: female, male offset 1 = male


        public PredictorMv(SchemaVariable var, Node _node, int idx, int fieldSpan, int nativeIdx, int offSet, int predictorLstIdx, MvTb MvTb) {
            Variable = var;
            N = _node;
            PredMvLstIdx = idx;
            Table = MvTb;
            FieldSpan = fieldSpan;
            NativeIdx = nativeIdx;
            Offset = offSet;
            PredictorLstIdx = predictorLstIdx;
            if (Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
                LowerNumber = N.PredictorLst[PredictorLstIdx].LowerNumber;
                HigherNumber = N.PredictorLst[PredictorLstIdx].HigherNumber;
            }
        }

        public SchemaVariable.DataTypeEnum DataType {
            get { return Variable.DataType; }
        }

        /// <summary>
        /// Returns the X of a given line Y
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        public double X(int y) {

           
            //try {
                if (this.Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
                    return Table.Data.PN[NativeIdx, y];
                } else {
                    if (this.Table.Data.PC[NativeIdx, y] == this.N.PredCatLst[NativeIdx].ValueSd.Keys[Offset])
                        return 1;
                    else
                        return 0;
                }
            //} catch (Exception e) {
            //    FE.Show(e);
            //    //FE.Show("Trying to access a empty multivariate table", "Error in PredictorMV.cs public double X(int y)");
            //    Def.FrmMain.Close();
            //    return -1;
            //}
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
