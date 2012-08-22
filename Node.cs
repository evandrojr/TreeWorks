#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using System.Drawing;
using System.Drawing.Imaging;

#endregion

namespace Spartacus {
    public abstract class Node : IComparable {

        public Tree Tree;
        public Node Ancestor=null;
        public List<Node> DescendentLst = new List<Node>();
//Durring the search for best split or if manual spliting a node the DescendentImp are pre calculated
//This value will be set as the node impurity after the child node is created
        public List<double> DescendentImpPreCalculated;
        public TableMap Table;
        private Predictor splitVariable;
        private int splitVariableIdx; // Tree.Schema.PredictorLst[splitVariableIdx] = splitVariable
        private Predictor bestSplit; // Set by bestSplitIdx
        private int bestSplitIdx; //   
        public static int CountId = -1; // Used to count all the Nodes never decrement
        private int id = -1; // Never decrement, used to refer objects
        public PositionTypeEnum PositionType;
        private int level = -1;
        public List<Predictor> PredictorLst = new List<Predictor>();
        public List<Predictor> PredCatLst = new List<Predictor>(); // List of Categorical Predictor
        public List<Predictor> PredNumLst = new List<Predictor>(); // List of Numerical Predictor
        public List<PredictorMv> PredMvLst = new List<PredictorMv>(); // List of Multivariate Predictors
        public List<PredictorMv> PredMvTsLst = new List<PredictorMv>(); // List of Multivariate Predictors for test set
        protected Point centerCoord = new Point(0, 0);
        protected bool splitDone = false;
        public List<Node> DescToBeVisitedLst = new List<Node>(); // (For pruning) List of the descendents of this node that wasn't been visited yet.
        public double Imp;
        public double ImpBestUniSplit;
        public double ImpBestMvSplit;
        public MvTb MvTb;
        public MvTb MvTsTb; // Multivariate table for the test data set
        public double C; //the constant for multivate nodes

//      public Mode

        //Not fully implemented
        public enum PositionTypeEnum : byte {
            Root,
            Intermediate,
            Leaf,
        }

        public Node() {
            Tree = Def.Tree;
            ++Node.CountId;
            id = Node.CountId;
            PositionType = PositionTypeEnum.Root;
            Table = new TableMap(this);
            Def.Db.PredictorsFill(this);
            MvProcedures();
            Def.Tree.NodeAdd(this);
        }

        public Node(Node ancestor) {
            Tree = Def.Tree;
            ++Node.CountId;
            id = Node.CountId;
            PositionType = PositionTypeEnum.Intermediate;
            Table = new TableMap(this);
            Def.Db.PredictorsFill(this);
            MvProcedures();
            Def.Tree.NodeAdd(this);
            Ancestor = ancestor;
            Ancestor.DescendentLst.Add(this);
        }

        public void MvProcedures() {
            int nativeIdxN = 0;
            int nativeIdxC = 0;

            if (Def.Multivariate) { // Creates the multivariate abstraction layer
                MvTb = new MvTb(this,0);
                MvTsTb = new MvTb(this, 1);
                for (int i = 0; i < PredictorLst.Count; ++i) {
                    if (PredictorLst[i].Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
                        PredictorLst[i].PredMvBase = PredMvLst.Count;
                        PredictorMv pmv = new PredictorMv(PredictorLst[i].Variable, this, PredMvLst.Count, 1, nativeIdxN, 0, i, MvTb);
                        PredMvLst.Add(pmv);
                        PredictorMv pmvTs = new PredictorMv(PredictorLst[i].Variable, this, PredMvLst.Count, 1, nativeIdxN, 0, i, MvTsTb);
                        PredMvTsLst.Add(pmvTs);
                        ++nativeIdxN;
                    } else {
                        PredictorLst[i].PredMvBase = PredMvLst.Count;
                        for (int dx = 0; dx < PredictorLst[i].DistinctValuesCount; ++dx) {
                            PredictorMv pmv = new PredictorMv(PredictorLst[i].Variable, this, PredMvLst.Count, PredictorLst[i].DistinctValuesCount, nativeIdxC, dx, i, MvTb );
                            PredMvLst.Add(pmv);
                            PredictorMv pmvTs = new PredictorMv(PredictorLst[i].Variable, this, PredMvLst.Count, PredictorLst[i].DistinctValuesCount, nativeIdxC, dx, i, MvTsTb);
                            PredMvTsLst.Add(pmvTs);
                        }
                        ++nativeIdxC;
                    }
                }
            }
        }
        
    
        //Should be executed only for the Root, the other nodes will calculate the impurity internaly
        // and store it in DescendentImpuritiesPreCalculated
        public abstract void ImpCalc();
        public abstract void LocalMinimumSearchMv();

        public abstract bool SplitDone {
            get;
            set;
        }

        public abstract void Dispose();

        public abstract void DisplayUpdate();


        public int Level {
            get {
                if (level < 0)
                    MessageBox.Show("Error negative level node id " + id);
                return level; }

            set {
                if (value >= 0)
                    level = value;
            }
        }

        public Predictor SplitVariable {
            get { return splitVariable; }
        }

        public double SplitValue {
            get {
                return PredictorLst[splitVariableIdx].SplitValue;
            }
            set {
                PredictorLst[splitVariableIdx].SplitValue = value;
            }

        }

        public int SplitVariableIdx {
            set {
                splitVariableIdx = value;
                splitVariable = PredictorLst[splitVariableIdx];
            }
            get { return splitVariableIdx; }
        }

        public int BestSplitIdx {
            set {
                bestSplitIdx = value;
                bestSplit = PredictorLst[bestSplitIdx];
            }
            get { return bestSplitIdx; }
        }

        public Predictor BestSplit {
            get { return bestSplit; }
        }

        public int CompareTo(object o) {
            Node nd = (Node)o;
            int a = (Ancestor.id * 10) + id;
            return  a.CompareTo((nd.Ancestor.id * 10) + id);
        }

        public bool SearchBestSplit(){

            bool firstTime = true;
            double bestGain=0;
            bool foundOne = false;
            Predictor p;

            for (int i = 0; i < PredictorLst.Count; ++i) {
                p = PredictorLst[i];
                if (firstTime) {
                    if (p.SplitStatus == Predictor.SplitStatusEnum.CanBeUsed) {
                        firstTime = false;
                        bestGain = p.Gain;
                        ImpBestUniSplit = p.ImpUniMin;
                        BestSplitIdx = i;
                        foundOne = true;
                    }
                    else
                        firstTime = true;
                } else {
                    if (p.SplitStatus == Predictor.SplitStatusEnum.CanBeUsed)
                        if (p.Gain > bestGain) {
                            bestGain = p.Gain;
                            ImpBestUniSplit = p.ImpUniMin;
                            BestSplitIdx = i;
                            foundOne = true;
                        }
                }
            }
            return foundOne;
        }

        public abstract void AllGainsCalc();
        
        //public abstract void AllGainsCalcMv();

        public void UpdateLevelLst(Node n) {

            Level lv;
            if (n.PositionType != Node.PositionTypeEnum.Root) {
                n.level = n.Ancestor.level + 1;
                if (Tree.LevelLast < n.level) {
                    lv = new Level();
                    lv.NodeLst.Add(n);
                    Tree.LevelLst.Add(lv);
                } else {
                    Tree.LevelLst[n.Level].NodeLst.Add(n);
                }
            }
           // LabelBottomText = splitVariable.Variable.Name;
        }

        public abstract Point CenterCoord {
            get;
            set;
        }


        public abstract int Width {
            get;
        }

        public abstract int Height {
            get;
        }


        public int Id {
            get { return id;}
        }

        public abstract string LabelTopText{
            get;
            set;
        }

        public abstract void LabelBottomShow();

        public abstract void LabelBottomHide();

        public abstract string LabelBottomText{
            get;
            set;
        }

        public static void NodeIDCounterReset() {
            CountId = -1;
        }

        public static void NodeIDCounterSetTo(int setTo) {
            //Use 9 if you want to keep the root node
            CountId = setTo;
        }

        public void PredictorsFill() {
            Def.Db.PredictorsFill(this);
        }

    }
}
