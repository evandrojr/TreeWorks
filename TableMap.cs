#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace Spartacus {
    public class TableMap {

        private Node node;
        private Schema schema;
        private int rowCount;
        public double Minimun, Maximun; //used for the data normalisation funtion

        public TableMap(Node _node) {
            node = _node;
            schema = Def.Schema;
            rowCount = (int) Def.Db.GetNumber("Select count(*) from " + Def.DbTrTb + node.Id);
        }

        public int VariableCount { get { return schema.VariableLst.Count; } }
        public int RowCount { get { return rowCount; } }

    }
}
