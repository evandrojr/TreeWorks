#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace Spartacus {
    //OUTDATED Can be a list of string now
    public class ValueGroup {

        //Values that will be used for the children
        private int groupCount;
        public List<List<string>> ValueGroupLst; // Store indexes Keys of depedent variable values 
        private Predictor pred;

        public ValueGroup(Predictor _pred, int _groupCount) {
            pred=_pred;
            List<string> ValueLst;
            groupCount = _groupCount;
            ValueGroupLst = new List<List<string>>(groupCount);
            for (int i = 0; i < groupCount; ++i) {
                ValueLst = new List<string>();
                ValueGroupLst.Add(ValueLst);
            }
        }

        public void AddValueFromIndex(int valueIdx, int childrenIdx){
            ValueGroupLst[childrenIdx].Add(pred.ValueSd.Keys[valueIdx]);
        }
   }
}
