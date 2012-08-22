using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Spartacus {
    class Experiment {

        private FrmConsole f;
        private DateTime startTime;
        private string dataset;
        private string target;
        private int trainingSampleSize;
        public static int SampleSeed = 0; // if = 0  will be used a random or '1' if Def.SampleUsingTheSameSeed = true;
        private static List<string> datasetAvLst = new List<string>();
            
        public Experiment(FrmConsole f) {
            this.f = f;
            startTime = DateTime.Now;       
            f.ad("Started at: " + startTime);
            try {
                init();
            } catch (Exception e) {
                FE.Show(e);
            }

            SampleSeed = 1;
            Def.TreeMinNumberOfCasesPerNode = 30;
            trainingSampleSize = 70;
            Def.Multivariate = true;
            Def.Tree.Algorithm = Tree.AlgorithmEnum.Gini;
            Def.SampleUsingTheSameSeed = true;
//            dataset = "materntur";
           //target = "spontdel";

            //dataset = "paul_icu";
            //target = "outcome";

            //dataset = "diabetes";
            //target = "retinopa";

            //dataset = "wpbc";
            //target = "outcome_";

            dataset = "icu";
            target = "outcome";
            


            Def.PresentCoefficientValue = 1;

            //try {
                execute();
//            } catch (Exception e) {
//                FE.Show(e);
//            }
        }

        private void init() {
            Def.ExperimentRunning = true;
            Def.Schema = new Schema();
            Def.Tree = new Tree();
            Def.Schema.Tree = Def.Tree;
            Def.Tree.Schema = Def.Schema;
            Database db = new Database(Database.DriverEnum.PostgreSQL);
            Def.Db = db;
            Def.Db.ConStr = "DRIVER={" + Def.DbDriver + "};UID=" + Def.DbUser + ";SERVER=" + Def.DbServer + ";Port=" + Def.DbPort + ";Database=" + Def.DbDatabase + ";Password=" + Def.DbPassword;
            Def.Db.Connect();
        }

        private bool dataSetSet(string t) {
            
            List<string> tablesLst = Def.Db.BaseTableList();
            foreach (string table in tablesLst) {
                if (t == table.Substring(0, table.IndexOf(Def.DbBaseTableSufix))) {
                    Def.DbTableInUse = t;
                    return true;
                }                    
            }
            return false;
        }


        private bool targetSet(string t) {
            bool ret = false;
            for (int y = 0; y < Def.Schema.VariableLst.Count; ++y) {
                if (t == Def.Schema.VariableLst[y].Name) {
                    Def.Schema.Target = Def.Schema.VariableLst[y];
                    Def.Schema.VariableLst[y].VariableRole = SchemaVariable.VariableRoleEnum.Target;
                    ret=true;
                } else {
                    Def.Schema.VariableLst[y].VariableRole = SchemaVariable.VariableRoleEnum.Predictor;
                    Def.Schema.PredictorLst.Add(Def.Schema.VariableLst[y]);
                }
            }
            return ret;
        }

        //private bool predictorAllSet() {

        //    bool r = false;
            
        //    for (int y = 0; y < Def.Schema.VariableLst.Count; ++y) {
        //        if (Def.Schema.VariableLst[y].VariableRole != SchemaVariable.VariableRoleEnum.Target) {
        //            Def.Schema.VariableLst[y].VariableRole = SchemaVariable.VariableRoleEnum.Predictor;
        //            r = true;
        //        }
        //    }
        //    if (r == false) {
        //        f.ad("You must define a target variable before using predictorAllSet()");
        //    }
        //    return r;
        //}


        private void execute() {

            string dimensionality = "_UniVar";
            string SampleSeedStr = "";
            if(Def.Multivariate)
                dimensionality = "_MultVar";

            if (SampleSeed!=0)
                SampleSeedStr = "_SampleSeed(" + SampleSeed + ")";

            string reportFilename = dataset + "_tg(" + target + ")" + dimensionality + "_trSS(" + trainingSampleSize + ")_presCoef(" + Def.PresentCoefficientValue + ")_alg(" + Def.Tree.Algorithm + ")" + SampleSeedStr + "_MinCasePerNode(" + Def.TreeMinNumberOfCasesPerNode + ")" + startTime.Day + "-" + startTime.Month + "-" + startTime.Year + " " + startTime.Hour + " " + startTime.Minute + " " + startTime.Second + " " + startTime.Millisecond;

            if (!dataSetSet(dataset)) {
                f.ad("Couldn't find data set " + dataset);
                f.ad("Available datasets are:");
                f.ad("");
                List<string> tablesLst = Def.Db.BaseTableList();
                foreach (string tb in tablesLst) {
                    f.ad(tb.Substring(0, tb.IndexOf(Def.DbBaseTableSufix)));
                }
                return;
            } else {
                f.ad("Using data set: " + dataset);
            }
      
            Def.Schema.PredictorLst.Clear();
            try {
                Def.Schema.Load();
            } catch (Exception e) {
                FE.Show(e);
                return;
            }
            if (!targetSet(target)) {
                f.ad("Couldn't find target " + target);
                f.ad("Available variables are:");
                f.ad("");
                for (int y = 0; y < Def.Schema.VariableLst.Count; ++y) {
                    f.ad(Def.Schema.VariableLst[y].Name);                    
                }
                return;
            } else {
                f.ad("Using target: " + target);

            }
            Application.DoEvents();           
            try {
                sample(trainingSampleSize);
            } catch (Exception e) {
                FE.Show(e);
                return;
            }
            f.ad("Training set created using " + trainingSampleSize + "% of data," + " validation set created using " + (100-trainingSampleSize) + "% of data");
            modelTypeSet();
            //try {
                treeInitialise();
            //} catch (Exception e) {
            //    FE.Show(e);
            //    return;
            //}
            Def.Tree.FullAutogrow(Def.Tree.Root);
            Def.Tree.ReductionInImpCalc();
            validate(reportFilename);
        }

        private void sample(int sampleSize) {

            string sqlDeleteNulls;

            Def.DatasetWithNullRowCount = (int)Def.Db.GetNumber("select count(*) from " + Def.DbBsTb);
            Def.DatasetNotNullRowCount = (int)Def.Db.GetNumber("select count(*) from " + Def.DbBsTb + " WHERE " + Def.Schema.Target.Name + " IS NOT NULL");
            Def.TrainingSetPercent = sampleSize;
            Def.TestingSetPercent = 100 - Def.TrainingSetPercent;
            Def.TrainingSetRowCount = Def.TrainingSetPercent * Def.DatasetNotNullRowCount / 100;
            Def.TestingSetRowCount = Def.DatasetNotNullRowCount - Def.TrainingSetRowCount;
            Def.Db.RefereceTableCreate(0);
            Def.Db.ExecuteNonQuery("DROP table " + Def.DbTsTb + "0 ", true);
            Def.Db.ExecuteNonQuery("Create table " + Def.DbTsTb + "0 (" + Def.DbTableIdName + " integer PRIMARY KEY)");
            Def.Db.Sample();
            sqlDeleteNulls =
            @"DELETE FROM " +
                Def.DbTrTb + "0 " +
             "WHERE " +
                Def.DbTableIdName + " in " +
                "(select " + Def.DbTableIdName + " " +
                    "FROM " + Def.DbBsTb + " WHERE " + Def.Schema.Target.Name + " IS NULL)";
            Def.Db.ExecuteNonQuery(sqlDeleteNulls);
        }

        private void modelTypeSet() {
            if (Def.Schema.Target.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Categorical) {
                Def.Tree.ModelType = Tree.ModelTypeEnum.Classification;
            } else {
                Def.Tree.ModelType = Tree.ModelTypeEnum.Regression;
            }
            f.ad("Model type: " + Def.Tree.ModelType.ToString());
        }

        private void treeInitialise() {
            if (Def.Schema.Target.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Categorical) {
                NodeTargetCategorical root = new NodeTargetCategorical();
                Def.Tree.Root = root;
            } else {
                NodeTargetContinuous root = new NodeTargetContinuous();
                Def.Tree.Root = root;
            }
            Def.Tree.GrowthState = Tree.GrowthStateEnum.Root;
            Def.TreeCanBeDisplayed = false;
            //Def.FrmMain.TreeBuild();
        }

        private void validate(string reportFilename) {
            if (Def.Multivariate) {
                TreeMvValidation t = new TreeMvValidation();
                t.NodeFill(Def.Tree.Root);
            } else {
                TreeValidation t = new TreeValidation();
                t.NodeFill(Def.Tree.Root);
            }
            if (Def.Tree.ModelType == Tree.ModelTypeEnum.Classification) {
                TreeClsValReport r = new TreeClsValReport(reportFilename);
                r.Dump();
            }
        }
    }
}
