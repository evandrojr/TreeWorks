#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


#endregion

namespace Spartacus {

    public class Level {

        public int Number;
        public static int Last=-1;
        public static int BorderHorizontal = 32;
        public static int BorderVertical = 70;
        public int Width, Height;
        public List<Node> NodeLst = new List<Node>();
        public Level() {
            Number = ++Last;
        }
    }

    public class Tree {

        private Node root;
        public Schema Schema;
        public AlgorithmEnum Algorithm;
        public OrientationEnum Orientation;
        public int TopBorder = 30;
        public int BorderHorizontal = 50;
        public int BorderVertical = 50;
        public int LabelTopDistanceToNode = 10;
        public int LabelBottomDistanceToNode = 10;
        public List<SchemaVariable> VariableLst = new List<SchemaVariable>(); //Includes de dependent variable and all others
        public List<Level> LevelLst = new List<Level>();
        public List<Node> NodeLst = new List<Node>(30); 
        public const int NodeDistForBrothers = 20;
        public const int NodeDistForLevels = 100;
        public int Width = 0;
        public int Height = 0;
        public int NodeWidthMax, NodeHeightMax;
        private GrowthStateEnum growthState;
        public double ReductionInImp = 0;
        public ModelTypeEnum ModelType;

        public Tree() {
            GrowthState = GrowthStateEnum.None;
            Orientation = OrientationEnum.Vertical;
            Level.Last = -1;
            Schema = Def.Schema;
        }

        public enum ModelTypeEnum : byte {
            Classification,
            Regression,
        }

        public enum GrowthStateEnum : byte {
            None,
            Root,
            Intermediate,
            FullGrow,
            Pruned,
        }

        public enum AlgorithmEnum : byte {
            Gini,
            Entropy,
            Chaid,
            NetRiV,
            GrossRiV,
            MaxDif,
            Hybrid,
        }

        public enum OrientationEnum : byte {
            Vertical,
            Horizontal,
        }

        public GrowthStateEnum GrowthState {
            get { return GrowthState; }
            set {
                growthState = value;
                if (value == GrowthStateEnum.None) {
                    if (!Def.ExperimentRunning) {
                        for (int i = 0; i < Def.ToolBar.Items.Count; ++i)
                            Def.ToolBar.Items[i].Enabled = false;
                        Def.ToolBar.Items["btImportData"].Enabled = true;
                        Def.ToolBar.Items["btOptions"].Enabled = true;
                        Def.ToolBar.Items["btAbout"].Enabled = true;
         
                    }
                    return;
                }
                if (value == GrowthStateEnum.Root) {
                    if (!Def.ExperimentRunning) {
                        for (int i = 0; i < Def.ToolBar.Items.Count; ++i)
                            Def.ToolBar.Items[i].Enabled = false;
                        Def.ToolBar.Items["btNew"].Enabled = true;
                        Def.ToolBar.Items["btImportData"].Enabled = true;
                        Def.ToolBar.Items["btOptions"].Enabled = true;
                        Def.ToolBar.Items["btAbout"].Enabled = true;

                        Def.ToolBar.Items["btFullGrow"].Enabled = true;
                        Def.ToolBar.Items["btGrowAndPrune"].Enabled = true;
                    }
                    return;
                }
                if (value == GrowthStateEnum.Intermediate) {
                    if (Def.Tree.NodeLst.Count == 1) {
                        GrowthState = GrowthStateEnum.Root;
                        return;
                    }
                    if (!Def.ExperimentRunning) {
                        for (int i = 0; i < Def.ToolBar.Items.Count; ++i)
                            Def.ToolBar.Items[i].Enabled = false;
                        Def.ToolBar.Items["btNew"].Enabled = true;
                        Def.ToolBar.Items["btImportData"].Enabled = true;
                        Def.ToolBar.Items["btOptions"].Enabled = true;
                        Def.ToolBar.Items["btAbout"].Enabled = true;

                        Def.ToolBar.Items["btFullGrow"].Enabled = true;
                        Def.ToolBar.Items["btGrowAndPrune"].Enabled = true;
                        Def.ToolBar.Items["btPrune"].Enabled = true;

                        Def.ToolBar.Items["btMoveData"].Enabled = true;
                        Def.ToolBar.Items["btValidate"].Enabled = true;
             

                    }
                    return;
                }
                if (value == GrowthStateEnum.FullGrow) {
                    if (Def.Tree.NodeLst.Count == 1) {
                        GrowthState = GrowthStateEnum.Root;
                        return;
                    }
                    if (!Def.ExperimentRunning) {
                        for (int i = 0; i < Def.ToolBar.Items.Count; ++i)
                            Def.ToolBar.Items[i].Enabled = false;
                        Def.ToolBar.Items["btNew"].Enabled = true;
                        Def.ToolBar.Items["btImportData"].Enabled = true;
                        Def.ToolBar.Items["btOptions"].Enabled = true;
                        Def.ToolBar.Items["btAbout"].Enabled = true;

                        Def.ToolBar.Items["btPrune"].Enabled = true;

                        Def.ToolBar.Items["btMoveData"].Enabled = true;
                        Def.ToolBar.Items["btValidate"].Enabled = true;
                        

                    }

                }
                if (value == GrowthStateEnum.Pruned) {
                    if (Def.Tree.NodeLst.Count == 1) {
                        GrowthState = GrowthStateEnum.Root;
                        return;
                    }
                    if (!Def.ExperimentRunning) {
                        for (int i = 0; i < Def.ToolBar.Items.Count; ++i)
                            Def.ToolBar.Items[i].Enabled = false;
                        Def.ToolBar.Items["btNew"].Enabled = true;
                        Def.ToolBar.Items["btImportData"].Enabled = true;
                        Def.ToolBar.Items["btOptions"].Enabled = true;
                        Def.ToolBar.Items["btAbout"].Enabled = true;
                        Def.ToolBar.Items["btFullGrow"].Enabled = true;


                    }
                }
            }
        }

        public void UpdateLevelsInformation() {

            int levelLast = -1;
            Level.Last = -1;
            LevelLst = new List<Level>();
            Level lv = new Level();
            lv.NodeLst.Add(root);
            LevelLst.Add(lv);

            foreach (Node ndSearch in Def.Tree.NodeLst)
                if (ndSearch.Level > levelLast)
                    levelLast = ndSearch.Level;
            for (int i = 1; i <= levelLast; ++i) {
                lv = new Level();
                LevelLst.Add(lv);
            }
            foreach (Node nd in Def.Tree.NodeLst) {
                foreach (Level existingLevel in LevelLst) {
                    if (existingLevel.Number == nd.Level) {
                        existingLevel.NodeLst.Add(nd);
                        continue;
                    }
                }
            }
        }

        public void ManuallySplit(Node node) {

            int nextNodeId=-1;
            string sql="";

            NodeTargetCategorical lcat, rcat;
            NodeTargetContinuous lcon, rcon;

            if (node.SplitVariable.Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
                nextNodeId = Node.CountId+1;
                Def.Db.TableDropIfExists(Def.DbTrTb + nextNodeId);
                Def.Db.ReferenceTableIndexDropIfExists(nextNodeId);
                sql = 
                @"CREATE TABLE " + 
                    Def.DbTrTb + nextNodeId + 
                    " AS " +
                    "SELECT " + Def.DbTrTb + node.Id + "." + Def.DbTableIdName + " FROM " +
                        Def.DbTrTb + node.Id + ", " + Def.DbBsTb +
                    " WHERE " + 
                    Def.DbTrTb + node.Id + "." + Def.DbTableIdName + "=" + Def.DbBsTb + "." + Def.DbTableIdName +
                    " AND " + node.SplitVariable.Variable.Name + " <= " + node.SplitValue;
                Def.Db.ExecuteNonQuery(sql);
                Def.Db.ReferenceTableIndexCreate(nextNodeId);

                if (Def.Schema.Target.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
                    lcon = new NodeTargetContinuous(node);
                    lcon.LabelTopText = " <= " + Math.Round(node.SplitValue, 2);
                } else {
                    lcat = new NodeTargetCategorical(node);
                    lcat.LabelTopText = " <= " + Math.Round(node.SplitValue, 2);
                }

                //Right
                nextNodeId = Node.CountId + 1;
                Def.Db.TableDropIfExists(Def.DbTrTb + nextNodeId);
                Def.Db.ReferenceTableIndexDropIfExists(nextNodeId);
                sql =
                @"CREATE TABLE " +
                    Def.DbTrTb + nextNodeId +
                    " AS " +
                    "SELECT " + Def.DbTrTb + node.Id + "." + Def.DbTableIdName + " FROM " +
                        Def.DbTrTb + node.Id + ", " + Def.DbBsTb +
                    " WHERE " +
                    Def.DbTrTb + node.Id + "." + Def.DbTableIdName + "=" + Def.DbBsTb + "." + Def.DbTableIdName +
                    " AND " + node.SplitVariable.Variable.Name + " > " + node.SplitValue;
                Def.Db.ExecuteNonQuery(sql);
                Def.Db.ReferenceTableIndexCreate(nextNodeId);
                if (Def.Schema.Target.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
                    rcon = new NodeTargetContinuous(node);
                    rcon.LabelTopText = " > " + Math.Round(node.SplitValue, 2);
                } else {
                    rcat = new NodeTargetCategorical(node);
                    rcat.LabelTopText = " > " + Math.Round(node.SplitValue, 2);
                }
            } else //SchemaVariable.VariableTypeEnum.Continuous
                if (node.SplitVariable.Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Categorical) {

                    string vals = "";
                    nextNodeId = Node.CountId + 1;
                    //LEFT NODE

                    List<string> caseLst = node.SplitVariable.ChildrenGroups.ValueGroupLst[0];
                    for (int i = 0; i < caseLst.Count; ++i) {
                        vals += node.SplitVariable.Variable.Name + "='" + caseLst[i] + "' ";
                        if (i < (caseLst.Count - 1)) {
                            vals += " or ";
                        } else
                            vals += ")";
                    }
                    Def.Db.TableDropIfExists(Def.DbTrTb + nextNodeId);
                    Def.Db.ReferenceTableIndexDropIfExists(nextNodeId);
                    sql =
                    @"CREATE TABLE " +
                        Def.DbTrTb + nextNodeId +
                        " AS " +
                        "SELECT " + Def.DbTrTb + node.Id + "." + Def.DbTableIdName + " FROM " +
                            Def.DbTrTb + node.Id + ", " + Def.DbBsTb +
                        " WHERE (" +
                        Def.DbTrTb + node.Id + "." + Def.DbTableIdName + "=" + Def.DbBsTb + "." + Def.DbTableIdName +
                        ") AND (" + vals;
                    Def.Db.ExecuteNonQuery(sql);
                    Def.Db.ReferenceTableIndexCreate(nextNodeId);
                    if (Def.Schema.Target.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
                        lcon = new NodeTargetContinuous(node);
                    } else {
                        lcat = new NodeTargetCategorical(node);
                    }

                    //RIGHT NODE
                    vals = "";
                    nextNodeId = Node.CountId + 1;
                    caseLst = node.SplitVariable.ChildrenGroups.ValueGroupLst[1];
                    for (int i = 0; i < caseLst.Count; ++i) {
                        vals += node.SplitVariable.Variable.Name + "='" + caseLst[i] + "' ";
                        if (i < (caseLst.Count - 1)) {
                            vals += " or ";
                        } else
                            vals += ")";
                    }
                    Def.Db.TableDropIfExists(Def.DbTrTb + nextNodeId);
                    Def.Db.ReferenceTableIndexDropIfExists(nextNodeId);
                    sql =
                    @"CREATE TABLE " +
                        Def.DbTrTb + nextNodeId +
                        " AS " +
                        "SELECT " + Def.DbTrTb + node.Id + "." + Def.DbTableIdName + " FROM " +
                            Def.DbTrTb + node.Id + ", " + Def.DbBsTb +
                        " WHERE (" +
                        Def.DbTrTb + node.Id + "." + Def.DbTableIdName + "=" + Def.DbBsTb + "." + Def.DbTableIdName +
                        ") AND (" + vals;
                    Def.Db.ExecuteNonQuery(sql);
                    Def.Db.ReferenceTableIndexCreate(nextNodeId);
                    if (Def.Schema.Target.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
                        rcon = new NodeTargetContinuous(node);
                    } else {
                        rcat = new NodeTargetCategorical(node);
                    }
                }
            afterSplit(node);
        }


        public bool AutoSplit(Node node) {

            int nextNodeId=-1;
            string sql="";

            NodeTargetCategorical lcat, rcat;
            NodeTargetContinuous lcon, rcon;

            if (node.SplitVariable.SplitStatus != Predictor.SplitStatusEnum.CanBeUsed)
                return false;

            if (node.Level >= Def.TreeLevelsMax)
                return false;
//
            if (node.SplitVariable.Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
       
                nextNodeId = Node.CountId + 1;
                Def.Db.TableDropIfExists(Def.DbTrTb + nextNodeId);
                Def.Db.ReferenceTableIndexDropIfExists(nextNodeId);
                sql =
                @"CREATE TABLE " +
                    Def.DbTrTb + nextNodeId +
                    " AS " +
                    "SELECT " + Def.DbTrTb + node.Id + "." + Def.DbTableIdName + " FROM " +
                        Def.DbTrTb + node.Id + ", " + Def.DbBsTb +
                    " WHERE " +
                    Def.DbTrTb + node.Id + "." + Def.DbTableIdName + "=" + Def.DbBsTb + "." + Def.DbTableIdName +
                    " AND " + node.SplitVariable.Variable.Name + " <= " + node.SplitValue;
                Def.Db.ExecuteNonQuery(sql);
                Def.Db.ReferenceTableIndexCreate(nextNodeId);

                if (Def.Schema.Target.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
                    lcon = new NodeTargetContinuous(node);
                    lcon.LabelTopText = " <= " + Math.Round(node.SplitValue, 2);
                } else {
                    lcat = new NodeTargetCategorical(node);
                    lcat.LabelTopText = " <= " + Math.Round(node.SplitValue, 2);
                }

                //Right
                nextNodeId = Node.CountId + 1;
                Def.Db.TableDropIfExists(Def.DbTrTb + nextNodeId);
                Def.Db.ReferenceTableIndexDropIfExists(nextNodeId);
                sql =
                @"CREATE TABLE " +
                    Def.DbTrTb + nextNodeId +
                    " AS " +
                    "SELECT " + Def.DbTrTb + node.Id + "." + Def.DbTableIdName + " FROM " +
                        Def.DbTrTb + node.Id + ", " + Def.DbBsTb +
                    " WHERE " +
                    Def.DbTrTb + node.Id + "." + Def.DbTableIdName + "=" + Def.DbBsTb + "." + Def.DbTableIdName +
                    " AND " + node.SplitVariable.Variable.Name + " > " + node.SplitValue;
                Def.Db.ExecuteNonQuery(sql);
                Def.Db.ReferenceTableIndexCreate(nextNodeId);
                if (Def.Schema.Target.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
                    rcon = new NodeTargetContinuous(node);
                    rcon.LabelTopText = " > " + Math.Round(node.SplitValue, 2);
                } else {
                    rcat = new NodeTargetCategorical(node);
                    rcat.LabelTopText = " > " + Math.Round(node.SplitValue, 2);
                }
            } else
            {//SchemaVariable.VariableTypeEnum.Continuous
                try {
                if (node.SplitVariable.ChildrenGroups.ValueGroupLst[0].Count == 0 || node.SplitVariable.ChildrenGroups.ValueGroupLst[1].Count == 0)
                        return false;
                } catch { return false; }

                    string vals = "";
                    nextNodeId = Node.CountId + 1;

                    
                    //LEFT NODE

                    List<string> caseLst = node.SplitVariable.ChildrenGroups.ValueGroupLst[0];
                    for (int i = 0; i < caseLst.Count; ++i) {
                        vals += node.SplitVariable.Variable.Name + "='" + caseLst[i] + "' ";
                        if (i < (caseLst.Count - 1)) {
                            vals += " or ";
                        } else
                            vals += ")";
                    }
                    Def.Db.TableDropIfExists(Def.DbTrTb + nextNodeId);
                    Def.Db.ReferenceTableIndexDropIfExists(nextNodeId);
                    sql =
                    @"CREATE TABLE " +
                        Def.DbTrTb + nextNodeId +
                        " AS " +
                        "SELECT " + Def.DbTrTb + node.Id + "." + Def.DbTableIdName + " FROM " +
                            Def.DbTrTb + node.Id + ", " + Def.DbBsTb +
                        " WHERE (" +
                        Def.DbTrTb + node.Id + "." + Def.DbTableIdName + "=" + Def.DbBsTb + "." + Def.DbTableIdName +
                        ") AND (" + vals;
                    Def.Db.ExecuteNonQuery(sql);
                    Def.Db.ReferenceTableIndexCreate(nextNodeId);
                    if (Def.Schema.Target.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
                        lcon = new NodeTargetContinuous(node);
                    } else {
                        lcat = new NodeTargetCategorical(node);
                    }
                    
                    //RIGHT NODE
                    vals = "";
                    nextNodeId = Node.CountId + 1;
                    caseLst = node.SplitVariable.ChildrenGroups.ValueGroupLst[1];
                    for (int i = 0; i < caseLst.Count; ++i) {
                        vals += node.SplitVariable.Variable.Name + "='" + caseLst[i] + "' ";
                        if (i < (caseLst.Count - 1)) {
                            vals += " or ";
                        } else
                            vals += ")";
                    }
                    Def.Db.TableDropIfExists(Def.DbTrTb + nextNodeId);
                    Def.Db.ReferenceTableIndexDropIfExists(nextNodeId);
                    sql =
                    @"CREATE TABLE " +
                        Def.DbTrTb + nextNodeId +
                        " AS " +
                        "SELECT " + Def.DbTrTb + node.Id + "." + Def.DbTableIdName + " FROM " +
                            Def.DbTrTb + node.Id + ", " + Def.DbBsTb +
                        " WHERE (" +
                        Def.DbTrTb + node.Id + "." + Def.DbTableIdName + "=" + Def.DbBsTb + "." + Def.DbTableIdName +
                        ") AND (" + vals;
                    Def.Db.ExecuteNonQuery(sql);
                    Def.Db.ReferenceTableIndexCreate(nextNodeId);
                    if (Def.Schema.Target.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
                        rcon = new NodeTargetContinuous(node);
                    } else {
                        rcat = new NodeTargetCategorical(node);
                    }
            }
            afterSplit(node);
            return true;
        }

        public bool AutoSplitMv(Node node) {

            int nextNodeId = -1;
            string sql = "";
            double varSum=0;

            NodeTargetCategorical lcat, rcat;
            NodeTargetContinuous lcon, rcon;

            //if (node.SplitVariable.SplitStatus != Predictor.SplitStatusEnum.CanBeUsed)
            //    return false;

            if (node.Level >= Def.TreeLevelsMax)
                return false;

            nextNodeId = Node.CountId + 1;
            Def.Db.TableDropIfExists(Def.DbTrTb + nextNodeId);
            Def.Db.ReferenceTableIndexDropIfExists(nextNodeId);
            //Left node
            sql =
            @"CREATE TABLE " +
                Def.DbTrTb + nextNodeId + "(" + Def.DbTableIdName + " integer NOT NULL)";
            Def.Db.ExecuteNonQuery(sql);

            //Right node
            nextNodeId = nextNodeId + 1;
            Def.Db.TableDropIfExists(Def.DbTrTb + nextNodeId);
            Def.Db.ReferenceTableIndexDropIfExists(nextNodeId);
            sql =
            @"CREATE TABLE " +
                Def.DbTrTb + nextNodeId + "(" + Def.DbTableIdName + " integer NOT NULL)";
            Def.Db.ExecuteNonQuery(sql);

            List<string> insert = new List<string>();
            for (int y = 0; y < node.Table.RowCount; ++y) {
                varSum = 0;
                for (int i = 0; i < node.PredMvLst.Count; ++i) {
                    varSum += node.PredMvLst[i].Coef * node.PredMvLst[i].X(y);
                }
                if ((varSum + node.C) <= 0) {
                    insert.Add(@"insert into " + Def.DbTrTb + (nextNodeId - 1) + " values (" + node.MvTb.Data.ID[y] + ")");
                } else {
                    insert.Add(@"insert into " + Def.DbTrTb + (nextNodeId) + " values (" + node.MvTb.Data.ID[y] + ")");
                }
            }
            Def.Db.NonQueryTransaction(insert);
            Def.Db.ReferenceTableIndexCreate(nextNodeId-1);
            Def.Db.ReferenceTableIndexCreate(nextNodeId);

            if (Def.Schema.Target.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
                rcon = new NodeTargetContinuous(node);
                lcon = new NodeTargetContinuous(node);
                // node equation goes here
            } else {
                rcat = new NodeTargetCategorical(node);
                lcat = new NodeTargetCategorical(node);
                // node equation goes here
            }

            afterSplit(node);
            return true;
        }


        private void afterSplit(Node node) { // Things to do after any kind of split
            node.LabelBottomText = node.SplitVariable.Variable.Name;
            node.SplitDone = true;
            Def.Tree.GrowthState = Tree.GrowthStateEnum.Intermediate;
            CreateLayout();
        }


        public void ReductionInImpCalc() {

            //Update

            if (Def.Schema.Target.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
                NodeTargetContinuous root = (NodeTargetContinuous) Def.Tree.Root;
                
                ReductionInImp = root.Variance;
                foreach (NodeTargetContinuous n  in  Def.Tree.NodeLst) {
                    if (n.DescendentLst.Count == 0)
                        ReductionInImp -= ((double) n.Table.RowCount / Def.TrainingSetRowCount) * n.Variance;
                }
                ReductionInImp = (ReductionInImp / root.Variance) * 100;
            } else {
                ReductionInImp = Def.Tree.Root.Imp;
                foreach (Node n in Def.Tree.NodeLst) {
                    if (n.DescendentLst.Count == 0)
                        ReductionInImp -= ((double)n.Table.RowCount / Def.TrainingSetRowCount) * n.Imp;
                }
                ReductionInImp = (ReductionInImp / Def.Tree.Root.Imp) * 100;
            }
        }

        public void NodeAdd(Node n) {
                NodeLst.Add(n);
                return;
        }


        public Node Root {
            set {
                root = value;
                root.PositionType = Node.PositionTypeEnum.Root;
                Level lv = new Level();
                lv.NodeLst.Add(value);
                LevelLst.Add(lv);
                root.Level = 0;
                if(!Def.ExperimentRunning)
                    root.LabelTopText = "Targeting " + Def.Schema.Target.Name;
            }
            get {
                return root;
            }
        }


        public int LevelLast {
            get {return Level.Last; }
        }


        public void UpdateLevelsDimension(){

            bool firstTime = true;
                        
            foreach (Level lv in LevelLst) {
                lv.Width = lv.Height = 0;
                firstTime = true;
                foreach (Node nd in lv.NodeLst) {
                    lv.Width += nd.Width + Level.BorderHorizontal;
                    if (firstTime) {
                        firstTime = false;
                        lv.Height = nd.Height + Level.BorderVertical;
                    } else
                        if ((nd.Height + Level.BorderVertical) > lv.Height)
                            lv.Height = nd.Height + Level.BorderVertical;
                }
            }
        }

        public void UpdateTreeDimension() {

            bool firstTime = true;
            Height = 0;
            foreach (Level lv in LevelLst) {
                if (firstTime) {
                    firstTime = false;
                    Width = lv.Width;
                } else {
                    if (lv.Width > Width)
                        Width = lv.Width;
                }
                Height += lv.Height;
            }
            Height+= BorderVertical;
            Width += BorderHorizontal;
        }

        public void CreateLayout(){

            //First calculate the space used for the tree

            if (Def.ExperimentRunning)
                return;

            int x, y, L, ndi;
            Point p;
            Level lv;
            Node nd;

//            FrmMain frmMain = Def.FrmMain;
//
              UpdateLevelsDimension();
              UpdateTreeDimension();
              if (Width > Def.PanelMain.Width)
                  Def.PbBase.Width = Width;
              else
                  Def.PbBase.Width = Def.PanelMain.Width - Def.LtSizeDifferencePanelMainAndPbBase;
              p = new Point((int)Def.PbBase.Width / 2, (int)(root.Height / 2) + TopBorder);
              root.CenterCoord = p;
              sortNodesInsideLevelLstByAncestor();

              if (Height + (root.CenterCoord.Y - (root.Height / 2) ) > Def.PanelMain.Height)
                  Def.PbBase.Height = Height + (root.CenterCoord.Y - (root.Height / 2));
              else
                  Def.PbBase.Height = Def.PanelMain.Height;

              for (L = 1; L <= LevelLast ; ++L) {
                  lv = LevelLst[L];
                  for (ndi = 0; ndi < lv.NodeLst.Count; ++ndi) {
                      nd = lv.NodeLst[ndi];
                      if( (LevelLst[L].NodeLst.Count % 2) == 1)
                          x = (Def.PbBase.Width / 2) + ((ndi - (LevelLst[L].NodeLst.Count / 2)) * (Level.BorderHorizontal + nd.Width));
                          //                          x = (Def.PbBase.Width / 2) + ((ndi - (LevelLst[L].NodeLst.Count / 2)) * (Level.BorderHorizontal + HztSpacer));
                      //   x = Tree.Width/2 + ( (i - (brotherCount/2) ) *  (NodeDistForBrothers + NodeWidthMax) ) ;
                      else
                          x = (Def.PbBase.Width / 2) + ((ndi - (LevelLst[L].NodeLst.Count / 2)) * (Level.BorderHorizontal + nd.Width) + (nd.Ancestor.Width / 2 + Level.BorderHorizontal / 3));
                      //                          x = (Def.PbBase.Width / 2) + ((ndi - (LevelLst[L].NodeLst.Count / 2)) * (Level.BorderHorizontal + HztSpacer) + HztSpacer);                      //                          x = (Def.PbBase.Width / 2) + ((ndi - (LevelLst[L].NodeLst.Count / 2)) * (Level.BorderHorizontal + HztSpacer) + HztSpacer);
                          //x = Tree.Width / 2 + ((i - (brotherCount / 2)) * (NodeDistForBrothers + NodeWidthMax)) + DistOffsetForEvenBrothers;
                      y = nd.Ancestor.CenterCoord.Y + LevelLst[L - 1].Height;
                      p = new Point(x, y) ;
                      nd.CenterCoord = p;
                  }
            }

        }

        private void sortNodesInsideLevelLstByAncestor() {
            //The 1st Level that may need to be sorted is the level 2  
            int li, anci, desi, descPos, posi;
            Node anc, des, temp;
            Level ancLv, desLv;


            for (li = 1; li < LevelLast ; ++li) {
                  ancLv = LevelLst[li];
                  desLv = LevelLst[li+1];
                  posi = 0;
                  for (anci = 0; anci < ancLv.NodeLst.Count; ++anci) {
                      anc = ancLv.NodeLst[anci];
                      for (desi = 0; desi < anc.DescendentLst.Count; ++desi) {
                          des = anc.DescendentLst[desi]; 
                          descPos = desLv.NodeLst.IndexOf(des);
                          temp = desLv.NodeLst[posi];
                          desLv.NodeLst[posi] = des;
                          desLv.NodeLst[descPos] = temp;
                          ++posi;
                      }
                  }
              }
            
        }

        public void Draw(Graphics g) {

            if (Def.ExperimentRunning)
                return;


            Pen linkPen = new Pen(Color.Black);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (Def.Schema.Target.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Categorical) {
                NodeTargetCategorical nd, ndDescendent;
                for (int j = 0; j < Def.Tree.NodeLst.Count; ++j) {
                    //Not efficient, change later | outdated
                    nd = (NodeTargetCategorical) Def.Tree.NodeLst[j];
                    for (int c = 0; c < nd.DescendentLst.Count; ++c) {
                        ndDescendent = (NodeTargetCategorical)nd.DescendentLst[c];
                        g.DrawLine(linkPen, nd.CenterCoord.X, nd.UI.Top + nd.UI.Height,
                            ndDescendent.CenterCoord.X, ndDescendent.UI.Top);
                    }
                }
            }
            else{
                NodeTargetContinuous nd, ndDescendent;
               for (int j = 0; j < Def.Tree.NodeLst.Count; ++j) {
                    //Not efficient, change later | outdated
                    nd = (NodeTargetContinuous) Def.Tree.NodeLst[j];
                    for (int c = 0; c < nd.DescendentLst.Count; ++c) {
                        ndDescendent = (NodeTargetContinuous) nd.DescendentLst[c];
                        g.DrawLine(linkPen, nd.CenterCoord.X, nd.UI.Top + nd.UI.Height,
                           ndDescendent.CenterCoord.X, ndDescendent.UI.Top);
                    }
                }
            }
        }


        public void RemoveDescendents(Node subtree) {

            List<Node> NodeRemoveLst = new List<Node>();
            subtree.LabelBottomHide();
            SelectNodesToRemove(subtree, NodeRemoveLst);
            foreach (Node node in NodeRemoveLst) {
                Def.Tree.NodeLst.Remove(node);
                node.Dispose();
            }
            Def.Tree.UpdateLevelsInformation();
            subtree.SplitDone = false;
            if (subtree is NodeTargetCategorical) {
                NodeTargetCategorical nd = (NodeTargetCategorical)subtree;
                nd.UI.LabelBottom.Text = "";
            } else {
                NodeTargetContinuous nd = (NodeTargetContinuous)subtree;
                nd.UI.LabelBottom.Text = "";
            }
            Def.Tree.growthState=GrowthStateEnum.Intermediate;

        }

        //Called only by public void RemoveDescendents(Node subtree) 
        private void SelectNodesToRemove(Node nd, List<Node> toRemove) {

            foreach (Node ndDescendent in nd.DescendentLst) {
                toRemove.Add(ndDescendent);
                SelectNodesToRemove(ndDescendent, toRemove);
            }
            nd.DescendentLst.Clear();
            return;
        }


        public void FullAutogrow(Node node) {
            
            FrmMessage fmsg = null;

            if (!Def.ExperimentRunning) {
                Def.FrmMain.WindowState = FormWindowState.Minimized;
                fmsg = new FrmMessage("Please, wait", "Calculating...");
                fmsg.Show(Def.FrmMain);
                Application.DoEvents();
            }
            FullAutoGrowSubtree(node);
            if (!Def.ExperimentRunning) {
                Def.FrmMain.WindowState = FormWindowState.Maximized;
                fmsg.Close();
                Def.FrmMain.TopMost = true;
                Application.DoEvents();
                Def.FrmMain.TopMost = false;
                Def.FrmMain.Focus();
            }
            Def.Tree.GrowthState = Tree.GrowthStateEnum.FullGrow;

        }


        private void FullAutoGrowSubtree(Node nd) {

            bool couldSplit = false;
            if (nd.Level == Def.TreeLevelsMax)
                return;

            if (!Def.Multivariate) {
                nd.AllGainsCalc();
                nd.SearchBestSplit();
                nd.SplitVariableIdx = nd.BestSplitIdx;
                couldSplit = nd.Tree.AutoSplit(nd);
            } else {
                nd.AllGainsCalc();  
                if (nd.SearchBestSplit()) {
                    nd.SplitVariableIdx = nd.BestSplitIdx;
                    nd.MvTb.DataFill();
                    nd.LocalMinimumSearchMv();
                    couldSplit = nd.Tree.AutoSplitMv(nd);
                    nd.MvTb.DataEmpty();
                }
            }
            if (couldSplit)
                foreach (Node ndDescendent in nd.DescendentLst)
                    FullAutoGrowSubtree(ndDescendent);

        }

    }
}
