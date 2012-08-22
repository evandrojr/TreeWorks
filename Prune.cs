#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace Spartacus {

    public class Prune {

//        static double K;  // Number of cases in a subtree
//        static double J;  // Number of misclassified cases in a subtree
//        static double JAfterPrune;  // Number of misclassified cases in a subtree after pruning
//        static double E;  // Error for a subtree
//        static double SE; // Standard error for a subtree
//        static double LeafCount;
           
// 
        public Prune() {

        }

        public static void PruneTree() {
            if (Def.Schema.Target.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous)
                return;
            PruneSubTree((NodeTargetCategorical) Def.Tree.Root);
            Def.Tree.CreateLayout();
            Def.PbBase.Invalidate();
            Def.Tree.GrowthState = Tree.GrowthStateEnum.Pruned;
        }

        public static void MisclassifiedCount(NodeTargetCategorical nd) {
            //foreach (NodeTargetCategorical desc in nd.DescendentLst) {
            //    MisclassifiedCount(desc);
            //}
            //if (nd.DescendentLst.Count == 0) {
            //    J += nd.Table.RowCount - nd.TargetClasses.Mode().Value;
            //    ++LeafCount;
            //} else
            //    return;
        }

        private static void PruneSubTree(NodeTargetCategorical subtree) {

//            K = J = E = SE = LeafCount = JAfterPrune = 0;
//
//            if (subtree.DescendentLst.Count == 0)
//                return;
//            JAfterPrune = subtree.Table.RowCount - subtree.TargetClasses.Mode().Value;
//            MisclassifiedCount(subtree);
//            E = J + (LeafCount / 2);
//            K = subtree.Table.Row;
//            SE = Math.Sqrt((E * (K - E)) / K);
//
//            if ((JAfterPrune + 0.5) < (E + SE)) {
//                //System.Windows.Forms.MessageBox.Show("' J e' '" + J + "' LeafCount '" + LeafCount + "' K '" + K + "' JAfterPrune + 0.5 = '" + (JAfterPrune + 0.5).ToString() + "' E + SE '" + (E + SE).ToString() + "'", "Pruning Node '" + subtree.Id);
//                Def.Tree.RemoveDescendents(subtree);
//            } else {
//                foreach (NodeTargetCategorical desc in subtree.DescendentLst) {
//                    PruneSubTree(desc);
//                }
//            }
//
//            //System.Windows.Forms.MessageBox.Show(" A moda e' " + mode.Key + mode.Value.ToString());

        }


    }
}
