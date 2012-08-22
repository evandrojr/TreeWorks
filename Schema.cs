#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;

#endregion

namespace Spartacus {
    public class Schema {

        public List<SchemaVariable> VariableLst = new List<SchemaVariable>(); //All the variables
        public SchemaVariable Target;// Only the dependent variable
        public List<SchemaVariable> PredictorLst = new List<SchemaVariable>(); //Only the Predictors variable
        public Tree Tree;

        public Schema() {

        }

        public void Load(){
            VariableLst.Clear();
            PredictorLst.Clear();
            Def.Db.SchemaFill(this);
        }

    }

        public class SchemaVariable {

            public string Name;
            public DataTypeEnum DataType;
            public VariableTypeEnum VariableTypeDetected;
            public VariableTypeEnum VariableTypeUserSet;
            public VariableRoleEnum VariableRole;

            public SchemaVariable() {
                VariableRole = VariableRoleEnum.NotUsed;
            }


            public enum DataTypeEnum : byte {
                Number = 0,
                Text,
            }

            public enum VariableTypeEnum : byte {
                Continuous = 0,
                Categorical,
            }

            public enum VariableRoleEnum : byte {
                Target = 0,
                Predictor,
                NotUsed,
            }

        }

    }

