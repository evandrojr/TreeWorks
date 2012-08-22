using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc;
using System.Data;
using System.Data.Common;

namespace Spartacus {
    /// <summary>
    /// Multivariate Table
    /// Abstraction layer that returns normalised and sparse data from the database
    /// </summary>
    
    public class MvTb {

        public Node N;

        private AimEnum aim;
        public int PredNumCount, PredCatCount = 0;
        public MvTbData Data=null;
        private bool DataLoaded = false; // DataFill() has been called but not DataEmpty() yet 
        public int ZeroTrainOneValidate=-1;  //Possible values for the kind of MvTb
        public int RowCount = -1;

        public MvTb(Node n, int zeroTrainOneValidate) {
            N = n;
            ZeroTrainOneValidate = zeroTrainOneValidate;
            if (ZeroTrainOneValidate == 0)
                RowCount = n.Table.RowCount;
            else {
                RowCount = -1; // Will update the rowCont only during the validation
            }
            if (n is NodeTargetCategorical) {
                aim = AimEnum.Classification;
                //TC = new string[N.Table.RowCount];
            } else {
                aim = AimEnum.Valuation;
                //tn = new double[N.Table.RowCount];
            }
        }


        public AimEnum Aim {
            get { return aim; }
        }

        public void DataFill() {
            if (DataLoaded)
                return;
            DataLoaded = true;
            Data = new MvTbData(this);
        }
        public void DataEmpty() {
            if (DataLoaded) {
                Data.Dispose();
                Data = null;
            }
            DataLoaded = false;
        }



        /// <summary>
        /// Tells which is the correnpondent numeric 
        /// Univariate predictor index given a multivariate numeric predictor index
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public int PredSrcNum(int idx) { 
            int c = 0;
            for (int i = 0; i < N.PredictorLst.Count; ++i) {
                if (N.PredictorLst[i].Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
                    if (c == idx)
                        return i;
                    ++c;
                }                
            }
            FE.Show("Error finding int PredSrcNum(int idx)", "Error");
            return -1;
        }

        public int PredSrcCat(int idx) {
            int c = 0;
            for (int i = 0; i < N.PredictorLst.Count; ++i) {
                if (N.PredictorLst[i].Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Categorical) {
                    if ((c + N.PredictorLst[i].DistinctValuesCount -1) >= idx)
                        return i;
                    c+=N.PredictorLst[i].DistinctValuesCount;
                }
            }
            FE.Show("Error finding int PredSrcCat(int idx)", "Error");
            return -1;
        }

        /// <summary>
        /// Returns a list with all the multivariate predictor indexes related to a univariate index
        /// </summary>
        /// <param name="uni"></param>
        /// <returns></returns>
        public static List<int> Uni2MultIdx(int uni, Node node) {

            List<int> idxLst;
            if (node.PredictorLst[uni].Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
                idxLst = new List<int>(1);
                idxLst.Add(node.PredictorLst[uni].PredMvBase);                
            } else {
                idxLst = new List<int>(node.PredictorLst[uni].DistinctValuesCount);
                for (int i = 0; i < node.PredictorLst[uni].DistinctValuesCount; ++i)
                    idxLst.Add(node.PredictorLst[uni].PredMvBase + i);
            }
            return idxLst;
        }




        /// <summary>
        /// Low level function
        /// SerchDataStructureAndIndex
        /// given a predictor index returns 
        /// 0 PC
        /// 1 PN
        /// Plus the index inside PC or PN
        /// </summary>
        /// <param name="idx"></param>
        /// <returns>[0/1, index]
        /// /// given a predictor index returns 
        /// 0 PC
        /// 1 PN </returns>
        public int[] DtStrAndIdx(int idx) {
            int dataStructure=-1, mvIdx=0;
            if (N.PredCatLst[idx].Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Categorical){
                dataStructure = 0;
                for (int i = 0; i < idx; ++i) {
                    if(N.PredCatLst[idx].Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Categorical){
                        ++mvIdx;
                    }
                }
            }
            else
                dataStructure = 1;
            for (int i = 0; i < idx; ++i) {
                if (N.PredCatLst[idx].Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Categorical) {
                    ++mvIdx;
                }
            }
            int[] dataAndIndex = new int[2] {dataStructure, mvIdx};
            return dataAndIndex;
        }
        
        public enum AimEnum : byte {
            Valuation,
            Classification
        }

        /// <summary>
        ///        
        ////*  X      X0|X1|X2|
        ////*  a       1| 0| 0|
        ////*  b       0| 1| 0|
        ////*  c       0| 0| 1|
        ////* 
        ////* */
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double[] TN(int x) {
            int predSrc = PredSrcNum(x);
            double[] col = new double[RowCount];
            OdbcDataReader reader = null;
            OdbcCommand cmd = null;
            string qry ="";
            int y=0;

            if (Def.Db.Con.State != ConnectionState.Open)
                Def.Db.Con.Open();
            if (Def.Db.Con.State != ConnectionState.Open) {
                FE.Show("Could not open the connection", "Error");
                return null;
            }
            if (ZeroTrainOneValidate == 0) {
                qry =
                @"SELECT " +
                        N.PredictorLst[predSrc].Variable.Name + " " +
                 "FROM " +
                    Def.DbBsTb + ", " + Def.DbTrTb + N.Id + " " +
                 "WHERE " +
                    Def.DbBsTb + "." + Def.DbTableIdName + "=" +
                    Def.DbTrTb + N.Id + "." + Def.DbTableIdName + " " +
                 "ORDER BY " +
                        Def.DbTrTb + N.Id + "." + Def.DbTableIdName;
            } else {
                qry =
                @"SELECT " +
                        N.PredictorLst[predSrc].Variable.Name + " " +
                 "FROM " +
                    Def.DbBsTb + ", " + Def.DbTsTb + N.Id + " " +
                 "WHERE " +
                    Def.DbBsTb + "." + Def.DbTableIdName + "=" +
                    Def.DbTsTb + N.Id + "." + Def.DbTableIdName + " " +
                 "ORDER BY " +
                        Def.DbTsTb + N.Id + "." + Def.DbTableIdName;
            }
            try {
                cmd = new OdbcCommand(qry, Def.Db.Con);
                reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    col[y] = Fcn.NormP1((double)reader[0], N.PredictorLst[predSrc].LowerNumber, N.PredictorLst[predSrc].HigherNumber);
                    ++y;
                }
            } catch (Exception ex) {
                FE.Show("Could not execute double[] TN(int x) " + ex.Message + " SQL = " + qry, "Error", ex.StackTrace);
            } finally {
                reader.Close();
            }
            return col;           
        }
    }

    /// <summary>
    /// Temporary structure!!! Should be disposed after use!!
    /// </summary>
    public class MvTbData {

        public Node N;
        public MvTb MvTb;
        public double[,] PN;   // numeric predictor
        public string[,] PC;  // categorical predictor
        public double[] TN;  // numeric target 
        public string[] TC; // categorical target 
        public int[] ID; // Id row

        public MvTbData(MvTb mvTb){
            MvTb = mvTb;
            N = MvTb.N;
            DataFill();
        }

        /// <summary>
        /// Rescale but doesnt spread the categorical data
        /// 
        /// </summary>
        /// <returns></returns>
        public void DataFill() {
            PN = new double[N.PredNumLst.Count, MvTb.RowCount];
            PC = new string[N.PredCatLst.Count, MvTb.RowCount];
            if(MvTb.Aim == MvTb.AimEnum.Valuation)
                TN = new double[MvTb.RowCount];
            else
                TC = new string[MvTb.RowCount];
            ID = new int[MvTb.RowCount];
    
            OdbcCommand cmd = null;
            OdbcDataReader reader = null;
            int x, y = 0, cc=0, cn=0;

            string sqlPredictors = "";
            string qry="";

            for (int i = 0; i < N.PredictorLst.Count; ++i) {
                sqlPredictors += N.PredictorLst[i].Variable.Name + ", ";
            }

            if (this.MvTb.ZeroTrainOneValidate == 0) {
                qry =
                @"SELECT " +
                           sqlPredictors + Def.Schema.Target.Name + ", " + Def.DbBsTb + "." + Def.DbTableIdName + " " +
                 "FROM " +
                    Def.DbBsTb + ", " + Def.DbTrTb + N.Id + " " +
                 "WHERE " +
                    Def.DbBsTb + "." + Def.DbTableIdName + "=" +
                    Def.DbTrTb + N.Id + "." + Def.DbTableIdName + " " +
                 "ORDER BY " +
                        Def.DbTrTb + N.Id + "." + Def.DbTableIdName;
            } else {
                qry =
                @"SELECT " +
                           sqlPredictors + Def.Schema.Target.Name + ", " + Def.DbBsTb + "." + Def.DbTableIdName + " " +
                 "FROM " +
                    Def.DbBsTb + ", " + Def.DbTsTb + N.Id + " " +
                 "WHERE " +
                    Def.DbBsTb + "." + Def.DbTableIdName + "=" +
                    Def.DbTsTb + N.Id + "." + Def.DbTableIdName + " " +
                 "ORDER BY " +
                        Def.DbTsTb + N.Id + "." + Def.DbTableIdName;
            }

            if (Def.Db.Con.State != ConnectionState.Open)
                Def.Db.Con.Open();
            if (Def.Db.Con.State != ConnectionState.Open) {
                FE.Show("Could not open the connection", "Error");
                return;
            }
            //try {
                cmd = new OdbcCommand(qry, Def.Db.Con);
                reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    cn = cc = 0;
                    for (x = 0; x < N.PredictorLst.Count; ++x) {
                        if (N.PredictorLst[x].Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
                            PN[cn, y] = Fcn.NormP1((float)reader[x], N.PredictorLst[x].LowerNumber, N.PredictorLst[x].HigherNumber);
                            ++cn;
                        } else {
                            PC[cc, y] = (string)reader[x];
                            ++cc;
                        }
                    }
                    if (MvTb.Aim == MvTb.AimEnum.Valuation) {
                        TN[y] = (double)reader[x];
                    } else {
                        TC[y] = (string)reader[x];
                    }
                    ++x;
                    ID[y] = (int)reader[x];
                    ++y;
                }
            //} catch (Exception ex) {
            //    FE.Show("Could not execute TableMvFill " + ex.Message + " SQL = " + qry, "Error", ex.StackTrace);
            //} finally {
            //    reader.Close();
            //}
            return;
        }




        public void Dispose() {
            PN = null;
            PC = null;
            TN = null;
            TC = null;
        }



    }

}
