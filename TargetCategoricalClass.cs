#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace Spartacus {
    public class TargetCategoricalClass {

        SchemaVariable variable;
        Node node;

        public SortedList<string, int> ClassSd;  //Value, frequency        

        public TargetCategoricalClass(SchemaVariable _variable, Node _node) {
            variable = _variable;
            node = _node;
            ClassSd = new SortedList<string, int>();
            Def.Db.TargetCategoricalClassFill(node, ClassSd);
        }

        public SchemaVariable.DataTypeEnum DataType {
            get { return variable.DataType; }
        }

//        public void AddCase(string _class) {
//
//            if (ClassSd.ContainsKey(_class))
//                ++ClassSd[_class];
//            else
//                ClassSd.Add(_class, 1);
//            return;
//        }


        public int Frequency(string _class) {
            return ClassSd[_class];
        }

        public int ClassCount{
            get {
                 return ClassSd.Count;
            }
        }

        public KeyValuePair<string, int> Mode() {

            KeyValuePair<string, int> mode = new KeyValuePair<string,int>("", -1);
            
            foreach (KeyValuePair<string, int> cls in ClassSd) {
                if (cls.Value > mode.Value)
                    mode = cls;
            }
            if (mode.Key == "") {
                System.Windows.Forms.MessageBox.Show("The mode is blank (TargetCategoricalClass.cs)");
            }
            return mode;

        }


    }
}
