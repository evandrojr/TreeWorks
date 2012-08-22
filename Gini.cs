#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc;
using System.Data;
using System.Windows.Forms;

#endregion

namespace Spartacus {
    public class Gini {

        FrmGraph fg=null;

        public static double NodeImp(NodeTargetCategorical node) {

            List<double> nLst = new List<double>();
            double info = 0;

            string sql =
            @"SELECT " +
                      "count(" + Def.Schema.Target.Name + ") " +
             "FROM " +
                Def.DbBsTb + ", " + Def.DbTrTb + node.Id + " " +
             "WHERE " +
                Def.DbBsTb + "." + Def.DbTableIdName + "=" +
                Def.DbTrTb + node.Id + "." + Def.DbTableIdName + " " +
             "GROUP BY " +
                    Def.Schema.Target.Name;
            nLst = Def.Db.GetNumberLst(sql);
            for (int i = 0; i < nLst.Count; ++i) {
                info += Math.Pow(nLst[i] / node.Table.RowCount,2);
            }
            return 1 - info;
        }

        public static double ImpCat(SortedList<string, int> classText, int itemCount) {
            double sumP2 = 0;

            foreach (int J in classText.Values) {
                sumP2 += Math.Pow((double)J / itemCount, 2);
            }
            return 1 - sumP2;

        }

        public static double ImpCont(int i1, int i2, List<N3T> AvcLst, out int rowCount) {

            double s1, s2, s3;
            int i;
            rowCount = 0;
            SortedDictionary<string, double> classFreq = new SortedDictionary<string, double>();
            //AvcLst[i].N0 = Value of the dependent varible
            //AvcLst[i].N1 = Frequency of y
            //AvcLst[i].N2 = Total of distinct registries until that row
            //AvcLst[i].T = y

            s1 = s2 = s3 = 0;
            if (i2 - i1 + 1 == 1) {
                rowCount = (int)AvcLst[0].N2;
                return 0;
            }
            for (i = i1; i <= i2; ++i) {
                if (classFreq.ContainsKey(AvcLst[i].T)) {
                    classFreq[AvcLst[i].T] += AvcLst[i].N1;
                } else {
                    classFreq.Add(AvcLst[i].T, AvcLst[i].N1);
                }
                //               s1 += AvsLst[i].N0 * AvsLst[i].N2;
                //               s2 += AvsLst[i].N0 * AvsLst[i].N0 * AvsLst[i].N2;
            }
            if (i1 == 0)
                s3 = AvcLst[i2].N2;
            else
                s3 = AvcLst[i2].N2 - AvcLst[i1 - 1].N2;
            rowCount = (int)s3;

            foreach (KeyValuePair<string, double> pair in classFreq) {
                s1 += Math.Pow(pair.Value / rowCount, 2);
            }
            if (rowCount <= 1) {
                MessageBox.Show("Row Count should never be <=1");
                return 0;
            }
            return 1 - s1;
        }

        //Sets the best p.SplitValue p.Gain
        public static double MinImpCont(NodeTargetCategorical n, Predictor p) {

            int i, leftRowCount = 0, rightRowCount = 0;
            //            int dfd; //delete
            double minImp, imp, lImp, rImp, instanceCount = n.Table.RowCount;
            lImp = rImp = minImp = imp = double.NaN;
            List<N3T> AvcLst;
            List<int> thresholdIndexLst;
            //Tries each partition:

            //AvcLst[i].N0 = Value of the dependent varible
            //AvcLst[i].N1 = Frequency of y
            //AvcLst[i].N2 = Total of distinct registries until that row
            //AvcLst[i].T  = y

            string sql =
            @"SELECT ALL " +
                Def.DbBsTb + "." + p.Variable.Name + ", " +
                " count(" + Def.DbBsTb + "." + p.Variable.Name + "),0 , " +
                Def.DbBsTb + "." + Def.Schema.Target.Name + " " +
            "FROM "
                + Def.DbBsTb + "," + Def.DbTrTb + n.Id + " " +
            "WHERE "
                + Def.DbBsTb + "." + Def.DbTableIdName + " = " +
                Def.DbTrTb + n.Id + "." + Def.DbTableIdName + " " +
                " AND " + Def.DbBsTb + "." + p.Variable.Name + " IS NOT NULL " +
            "GROUP BY " +
                Def.DbBsTb + "." + Def.Schema.Target.Name + ", " +
                Def.DbBsTb + "." + p.Variable.Name + " " +
            "ORDER BY " +
                Def.DbBsTb + "." + p.Variable.Name;
            AvcLst = Def.Db.GetN3TLst(sql);

            //N2 is the number of registries until a given row
            if (AvcLst.Count > 0) {
                AvcLst[0].N2 = AvcLst[0].N1;
                for (i = 1; i < AvcLst.Count; ++i)
                    AvcLst[i].N2 = AvcLst[i - 1].N2 + AvcLst[i].N1;
            }

            thresholdIndexLst = Fcn.SetPossibleThresholdIndexLst(AvcLst);
            if (thresholdIndexLst.Count == 0) {
                p.SplitStatus = Predictor.SplitStatusEnum.NotEnoughCases;
                p.Gain = 0;
                return 0;
            }

            n.DescendentImpPreCalculated = new List<double>(2);
            n.DescendentImpPreCalculated.Add(0);
            n.DescendentImpPreCalculated.Add(0);

            p.SplitStatus = Predictor.SplitStatusEnum.CanBeUsed;
            if (AvcLst.Count == 0) {
                p.SplitStatus = Predictor.SplitStatusEnum.NotEnoughCases;
                p.Gain = 0;
                return 0;
            }
            for (i = 0; i < thresholdIndexLst.Count; ++i) {
                lImp = ImpCont(0, thresholdIndexLst[i], AvcLst, out leftRowCount);
                rImp = ImpCont(thresholdIndexLst[i] + 1, AvcLst.Count - 1, AvcLst, out rightRowCount);
                if (Double.IsNaN(minImp) && leftRowCount >= Def.TreeMinNumberOfCasesPerNode && rightRowCount >= Def.TreeMinNumberOfCasesPerNode) {
                    imp = (leftRowCount * lImp + rightRowCount * rImp) / (leftRowCount + rightRowCount);
                    if (Double.IsNaN(imp) == false) {
                        minImp = imp;
                        p.SplitValue = AvcLst[thresholdIndexLst[i]].N0;
                        n.DescendentImpPreCalculated[0] = lImp;
                        n.DescendentImpPreCalculated[1] = rImp;
                    }
                } else {
                    imp = (leftRowCount * lImp + rightRowCount * rImp) / (leftRowCount + rightRowCount);
                    if (imp < minImp && !Double.IsNaN(imp) && leftRowCount >= Def.TreeMinNumberOfCasesPerNode && rightRowCount >= Def.TreeMinNumberOfCasesPerNode) {
                        minImp = imp;
                        p.SplitValue = AvcLst[thresholdIndexLst[i]].N0;
                        n.DescendentImpPreCalculated[0] = lImp;
                        n.DescendentImpPreCalculated[1] = rImp;
                    }
                }
            }
            if(Double.IsNaN(minImp)){
                p.SplitStatus = Predictor.SplitStatusEnum.NotEnoughCases;
                p.Gain = 0;
                return 0;
            }

            List<double> nLst;
            sql = @"
            SELECT 
                DISTINCT " + p.Variable.Name + " " +
            "FROM "
                + Def.DbBsTb + " , " + Def.DbTrTb + n.Id + " " +
            "WHERE "
                + Def.DbBsTb + "." + Def.DbTableIdName + "=" +
                Def.DbTrTb + n.Id + "." + Def.DbTableIdName + " AND " +
                p.Variable.Name + " IS NOT NULL " +
            "ORDER BY "
                + p.Variable.Name + " ASC ";
            nLst = Def.Db.GetNumberLst(sql);

            //Finds the angle bisector of the slit
            if (p.SplitValue != nLst[0] && p.SplitValue != nLst[nLst.Count - 2]) {
                for (i = 1; i < nLst.Count - 2; ++i) {
                    if (p.SplitValue == nLst[i]) {
                        p.SplitValue = (nLst[i - 1] + nLst[i + 1]) / 2;
                        break;
                    }
                }
            }

            p.ImpUniMin = minImp;
            p.Gain = (n.Imp - minImp) * 100 / n.Imp;
            p.Gain *= (double)(n.Table.RowCount - p.NullCount) / n.Table.RowCount;
            return p.Gain;
        }


        /// Sets the best p.SplitHyperplane and p.Gain
        /// <summary>
        /// Look for the local minimum by fixing all the coeficients except one and then vary it
        /// </summary>
//        public static double MinImpMvStartingFromBestNumericalNoOtherOrder(NodeTargetCategorical n, Predictor p) {

//            bool infoNotYetCalculated = true;
//            List<double> bestCoefLst = new List<double>();
//            List<double> testCoefLst = new List<double>(); // VariableToBeTestedCoodinates
//            SortedList<string, int> leftNode = new SortedList<string, int>();
//            SortedList<string, int> rightNode = new SortedList<string, int>();
//            List<int> optimisedVariableLst = new List<int>(); //Stores the index of predictors that have been used
//            List<int> NonOptimisedVariableLst = new List<int>(); //Stores the index of predictors that have been used
//            // starts with the best split index
//            double leftImp = 0, rightImp = 0;
//            List<double> azLst = new List<double>();
//            double leftCount = 0, rightCount = 0;// Left and right item count
//            double c=0; // (= best split coordinate) Constant of the equation A1X1 + A2X2 + C = V [0] normalised [1] original value
//            double az;          // New linear coefficient
//            double aSplit = 0;    // Linear coefficient candidate for the split
//            double xjm = 0;      // Normalised coordinate
//            double xts = 0;     // Normalised coordinate of the variable being tested
//            double lowestSplitImp = n.ImpBestUniSplit, splitImp = 0;
//            double OptVarSum;
//            double aSplitB = 0, xtsB = 0, xjmB = 0;
//            double v=0;   // see On growing better decision trees from data page 49
//            double bestMvGain;
//            int j; // NonOptimisedVariableLst index
//            List<double> orderedNumericGain = new List<double>(); // value, index or PredictorLst
//            int bestNumericSplitIdx = -1;

//            Def.LogMessage += "Starting MinImpContMvStartingFromBestNumericalNoOtherOrder" + Environment.NewLine;

//            for (int i = 0; i < n.PredictorLst.Count; ++i) {
//                if (n.PredictorLst[i].Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
//                    orderedNumericGain.Add(n.PredictorLst[i].Gain);                                    
//                }
//            }

//            orderedNumericGain.Sort();

//            if (orderedNumericGain.Count == 0) {
//                Def.LogMessage += "No suitable numeric split to be used as startpoint" + Environment.NewLine;
//                return -1;
//            }
//            bestNumericSplitIdx = orderedNumericGain.Count - 1;



//            foreach (PredictorMv pmv in n.PredMvLst) {
//                pmv.Coef = 0;
//                if (pmv.Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Categorical) {
//                    pmv.Optimised = true;
//                    optimisedVariableLst.Add(pmv.PredMvLstIdx);
//                    pmv.Coef = 0;
//                    //caseLst = n.PredictorLst[pmv.PredictorLstIdx].ChildrenGroups.ValueGroupLst[0];
//                    //for (int i = 0; i < caseLst.Count; ++i) {
//                    //    n.PredMvLst[pmv.PredMvLstIdx + caseLst[i]].BestCoef = 1;
//                    //}
//                }
//            }
//            n.MvTb.DataFill();

//            c = n.PredictorLst[bestNumericSplitIdx].SplitValue;
//            n.PredMvLst[n.PredictorLst[bestNumericSplitIdx].PredMvBase].Coef = 1; // variable used to split has coef 1
//            n.PredMvLst[n.PredictorLst[bestNumericSplitIdx].PredMvBase].Optimised = true;
//            optimisedVariableLst.Add(n.PredictorLst[bestNumericSplitIdx].PredMvBase);


//            //if (n.BestSplit.Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
//            //    c=n.SplitValue;
//            //    n.PredMvLst[n.BestSplit.PredMvBase].BestCoef = 1; // variable used to split has coef 1
//            //    n.PredMvLst[n.BestSplit.PredMvBase].Optimised = true;
//            //    optimisedVariableLst.Add(n.BestSplit.PredMvBase);
//            //}
            
//            //else {
//            //    c = 0;
//            //    caseLst = n.BestSplit.ChildrenGroups.ValueGroupLst[0];
//            //    for (int i = 0; i < caseLst.Count; ++i) {
//            //          n.PredMvLst[n.BestSplit.PredMvBase + caseLst[i]].BestCoef=0;
//            //    }
//            //}

//            //for (int pidx = 0; pidx < n.PredictorLst.Count; ++pidx) {
//            //    caseLst = n.PredictorLst[pidx].ChildrenGroups.ValueGroupLst[0];
//            //    for (int i = 0; i < caseLst.Count; ++i) {
//            //        n.PredMvLst[n.PredictorLst[pidx].PredMvBase + caseLst[i]].BestCoef = 0;
//            //    }
//            //}

//            for (int i = 0; i < n.PredMvLst.Count; ++i) {
//                if (n.PredMvLst[i].Optimised == false)
//                    NonOptimisedVariableLst.Add(i);
//            }

//            for (j = 0; j < NonOptimisedVariableLst.Count; ++j) {
//                azLst.Clear();
//                for (int y = 0; y < n.Table.RowCount; ++y) {
//                    OptVarSum = 0;
//                    for (int i = 0; i < optimisedVariableLst.Count; ++i) {
//                        OptVarSum += n.PredMvLst[optimisedVariableLst[i]].Coef * n.PredMvLst[optimisedVariableLst[i]].X(y);
//                    }
//                    v = OptVarSum + c;
//                    az = ((n.PredMvLst[NonOptimisedVariableLst[j]].Coef * n.PredMvLst[NonOptimisedVariableLst[j]].X(y)) -v ) / n.PredMvLst[NonOptimisedVariableLst[j]].X(y);
//                    azLst.Add(az);
//                }
//                azLst.Sort();
//                for (int azIdx = 0; azIdx < azLst.Count - 1; ++azIdx) {
//                    //Finally we got a new linear coefficient for a variable that
//                    //had linear coefficient = 0 
//                    //The new hyperplane is:
//                    // A1X1 + (1)X2 + c = 0 (c= - bestSplitNorm)
//                    // (new midpoint coef) * testVar + (coordinate of the bestSplit variable) - bestSplitNorm; 
//                    // aSplit * Fcn.NormP1(testLst[y], PredictorLst[i].LowerNumber, PredictorLst[i].HigherNumber);
//                    // +
//                    aSplit = (azLst[azIdx] + azLst[azIdx + 1]) / 2;
//                    for (int y = 0; y < n.Table.RowCount; ++y) {
//                        xts = n.PredMvLst[NonOptimisedVariableLst[j]].X(y);
////                      //  xjm = Fcn.NormP1(nnt.N0, n.BestSplit.LowerNumber, n.BestSplit.HigherNumber);
//                        //am = Fcn.NormP1(n.BestSplit.SplitValue, n.PredictorLst[i].LowerNumber, n.PredictorLst[i].HigherNumber);
//                        OptVarSum = 0;
//                        for (int i = 0; i < optimisedVariableLst.Count; ++i) {
//                            OptVarSum += n.PredMvLst[optimisedVariableLst[i]].Coef * n.PredMvLst[optimisedVariableLst[i]].X(y);
//                        }
//                        if ((aSplit * xts + OptVarSum + c) > 0) {
//                            ++leftCount;
//                            if (!leftNode.ContainsKey(n.MvTb.Data.TC[y]))
//                                leftNode.Add(n.MvTb.Data.TC[y], 1);
//                            else
//                                ++leftNode[n.MvTb.Data.TC[y]];
//                        } else {
//                            ++rightCount;
//                            if (!rightNode.ContainsKey(n.MvTb.Data.TC[y]))
//                                rightNode.Add(n.MvTb.Data.TC[y], 1);
//                            else
//                                ++rightNode[n.MvTb.Data.TC[y]];
//                        }
//                    }
//                    leftImp = Gini.ImpCat(leftNode, (int)leftCount);
//                    rightImp = Gini.ImpCat(rightNode, (int)rightCount);
//                    if (infoNotYetCalculated) {
//                        lowestSplitImp = (leftCount * leftImp + rightCount * rightImp) / n.Table.RowCount;
//                        aSplitB = aSplit; xtsB = xts; xjmB = xjm;
//                        infoNotYetCalculated = false;
//                        n.PredMvLst[NonOptimisedVariableLst[j]].Coef = aSplit;
//                    } else {
//                        splitImp = (leftCount * leftImp + rightCount * rightImp) / n.Table.RowCount;
//                        if (splitImp < lowestSplitImp) {
//                            lowestSplitImp = splitImp;
//                            n.PredMvLst[NonOptimisedVariableLst[j]].Coef = aSplit;
//                            aSplitB = aSplit; xtsB = xts; xjmB = xjm;
//                        }
//                    }
//                    leftNode.Clear();
//                    rightNode.Clear();
//                    leftCount = 0;
//                    rightCount = 0;

//                }
//            }
//            double bestUniGain = p.Gain;
//            bestMvGain = (n.Imp - lowestSplitImp) * 100 / n.Imp;
//            bestMvGain *= (double)(n.Table.RowCount - p.NullCount) / n.Table.RowCount;
//            string coefStr="";
//            for (int i = 0; i < n.PredMvLst.Count; ++i){
//                if(n.PredMvLst[i].Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous)
//                    coefStr += n.PredMvLst[i].Coef + "(" + n.PredMvLst[i].Variable.Name + ") ";  
//                else
//                    coefStr += n.PredMvLst[i].Coef + "(" + n.PredMvLst[i].Variable.Name + n.PredMvLst[i].Offset + ") ";  
//            }
//            coefStr +="c= " + c;
//            //MessageBox.Show("Gain= " + p.Gain + " bestUniGain= " + bestUniGain + Environment.NewLine + coefStr);
//            Def.LogMessage += "Gain= " + bestMvGain + " bestUniGain= " + bestUniGain + Environment.NewLine + coefStr + Environment.NewLine + Environment.NewLine;
//            n.MvTb.DataEmpty();
//            return p.Gain;
//        }


        /// Sets the best p.SplitHyperplane and p.Gain
        /// <summary>
        /// Starts from the best numerical split and then try each possible combination of splits for
        /// nominal and numerical variables.
        /// 
        /// Each nominal is tested for 0 and 1 and numeric variables are set to include each coordinate
        /// </summary>
        //public static double MinImpMvStartingFromBestNumericalOrderedByBestUnivariateNumericalSplits(NodeTargetCategorical n, Predictor p) {

        //    bool infoNotYetCalculated = true;
        //    List<double> bestCoefLst = new List<double>();
        //    List<double> testCoefLst = new List<double>(); // VariableToBeTestedCoodinates
        //    SortedList<string, int> leftNode = new SortedList<string, int>();
        //    SortedList<string, int> rightNode = new SortedList<string, int>();
        //    List<int> optimisedVariableLst = new List<int>(); //Stores the index of predictors that have been used
        //    List<int> NonOptimisedVariableLst = new List<int>(); //Stores the index of predictors that have been used
        //    // starts with the best split index
        //    double leftImp = 0, rightImp = 0;
        //    List<double> azLst = new List<double>();
        //    double leftCount = 0, rightCount = 0;// Left and right item count
        //    double c = 0; // (= best split coordinate) Constant of the equation A1X1 + A2X2 + C = V [0] normalised [1] original value
        //    double az;          // New linear coefficient
        //    double aSplit = 0;    // Linear coefficient candidate for the split
        //    double xjm = 0;      // Normalised coordinate
        //    double xts = 0;     // Normalised coordinate of the variable being tested
        //    double lowestSplitImp = n.Imp, splitImp = 0;
        //    double OptVarSum;
        //    double aSplitB = 0, xtsB = 0, xjmB = 0;
        //    double v = 0;   // see On growing better decision trees from data page 49
        //    double bestMvGain;
        //    int j; // NonOptimisedVariableLst index
        //    List<int> orderedNumericGainIdx = new List<int>(); // index or PredNumLst
        //    int bestNumericSplitIdx = -1;
            
        //    Def.LogMessage += "Starting MinImpContMvStartingFromBestNumericalOrderedByBestUnivariateNumericalSplits" + Environment.NewLine;
        //    int orderedNumericGainIdxCount;
            
        //    if (n.PredNumLst.Count > 0) {
        //        orderedNumericGainIdx.Add(0);
                
        //        for (int pn = 1; pn < n.PredNumLst.Count; ++pn) {
        //            orderedNumericGainIdxCount = orderedNumericGainIdx.Count;
        //            for (int i = 0; i < orderedNumericGainIdxCount; ++i) {
        //                if (n.PredNumLst[pn].Gain < n.PredNumLst[orderedNumericGainIdx[i]].Gain) {
        //                        orderedNumericGainIdx.Insert(i, pn);
        //                        continue;
        //                    }
        //                    orderedNumericGainIdx.Add(pn);
        //                }
        //        }
        //    }


        //    if (orderedNumericGainIdx.Count == 0) {
        //        Def.LogMessage += "No suitable numeric split to be used as startpoint" + Environment.NewLine;
        //        return -1;
        //    }
        //    bestNumericSplitIdx = n.PredNumLst[orderedNumericGainIdx[0]].PredictorLstIdx;


        //    foreach (PredictorMv pmv in n.PredMvLst) {
        //        pmv.Coef = 0;
        //        if (pmv.Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Categorical) {
        //            pmv.Optimised = true;
        //            optimisedVariableLst.Add(pmv.PredMvLstIdx);
        //            //caseLst = n.PredictorLst[pmv.PredictorLstIdx].ChildrenGroups.ValueGroupLst[0];
        //            //for (int i = 0; i < caseLst.Count; ++i) {
        //            //    n.PredMvLst[pmv.PredMvLstIdx + caseLst[i]].BestCoef = 1;
        //            //}
        //        }
        //    }
        //    n.MvTb.DataFill();
        //    c = n.PredictorLst[bestNumericSplitIdx].SplitValue;
        //    n.PredMvLst[n.PredictorLst[bestNumericSplitIdx].PredMvBase].Coef = 1; // variable used to split has coef 1
        //    n.PredMvLst[n.PredictorLst[bestNumericSplitIdx].PredMvBase].Optimised = true;
        //    optimisedVariableLst.Add(n.PredictorLst[bestNumericSplitIdx].PredMvBase);

        //    //if (n.BestSplit.Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
        //    //    c=n.SplitValue;
        //    //    n.PredMvLst[n.BestSplit.PredMvBase].BestCoef = 1; // variable used to split has coef 1
        //    //    n.PredMvLst[n.BestSplit.PredMvBase].Optimised = true;
        //    //    optimisedVariableLst.Add(n.BestSplit.PredMvBase);
        //    //}

        //    //else {
        //    //    c = 0;
        //    //    caseLst = n.BestSplit.ChildrenGroups.ValueGroupLst[0];
        //    //    for (int i = 0; i < caseLst.Count; ++i) {
        //    //          n.PredMvLst[n.BestSplit.PredMvBase + caseLst[i]].BestCoef=0;
        //    //    }
        //    //}

        //    //for (int pidx = 0; pidx < n.PredictorLst.Count; ++pidx) {
        //    //    caseLst = n.PredictorLst[pidx].ChildrenGroups.ValueGroupLst[0];
        //    //    for (int i = 0; i < caseLst.Count; ++i) {
        //    //        n.PredMvLst[n.PredictorLst[pidx].PredMvBase + caseLst[i]].BestCoef = 0;
        //    //    }
        //    //}

        //    //Set the order for nonOptimisedVariables to start by the univariate gain
        //    //orderedNumericGain.Count -1 is the best and was used already
        //    for (int i = 0; i < orderedNumericGainIdx.Count; ++i) {
        //        NonOptimisedVariableLst.Add(n.PredNumLst[orderedNumericGainIdx[i]].PredMvBase);
        //    }

        //    for (int i = 0; i < n.PredMvLst.Count; ++i) {
        //        if (n.PredMvLst[i].Optimised == false) {
        //            if(!NonOptimisedVariableLst.Contains(i))
        //                NonOptimisedVariableLst.Add(i);
        //        }
        //    }

        //    for (j = 0; j < NonOptimisedVariableLst.Count; ++j) {
        //        azLst.Clear();
        //        for (int y = 0; y < n.Table.RowCount; ++y) {
        //            OptVarSum = 0;
        //            for (int i = 0; i < optimisedVariableLst.Count; ++i) {
        //                OptVarSum += n.PredMvLst[optimisedVariableLst[i]].Coef * n.PredMvLst[optimisedVariableLst[i]].X(y);
        //            }
        //            v = OptVarSum + c;
        //            az = ((n.PredMvLst[NonOptimisedVariableLst[j]].Coef * n.PredMvLst[NonOptimisedVariableLst[j]].X(y)) - v) / n.PredMvLst[NonOptimisedVariableLst[j]].X(y);
        //            azLst.Add(az);
        //        }
        //        azLst.Sort();
        //        for (int azIdx = 0; azIdx < azLst.Count - 1; ++azIdx) {
        //            //Finally we got a new linear coefficient for a variable that
        //            //had linear coefficient = 0 
        //            //The new hyperplane is:
        //            // A1X1 + (1)X2 + c = 0 (c= - bestSplitNorm)
        //            // (new midpoint coef) * testVar + (coordinate of the bestSplit variable) - bestSplitNorm; 
        //            // aSplit * Fcn.NormP1(testLst[y], PredictorLst[i].LowerNumber, PredictorLst[i].HigherNumber);
        //            // +
        //            aSplit = (azLst[azIdx] + azLst[azIdx + 1]) / 2;
        //            for (int y = 0; y < n.Table.RowCount; ++y) {
        //                xts = n.PredMvLst[NonOptimisedVariableLst[j]].X(y);
        //                //                      //  xjm = Fcn.NormP1(nnt.N0, n.BestSplit.LowerNumber, n.BestSplit.HigherNumber);
        //                //am = Fcn.NormP1(n.BestSplit.SplitValue, n.PredictorLst[i].LowerNumber, n.PredictorLst[i].HigherNumber);
        //                OptVarSum = 0;
        //                for (int i = 0; i < optimisedVariableLst.Count; ++i) {
        //                    OptVarSum += n.PredMvLst[optimisedVariableLst[i]].Coef * n.PredMvLst[optimisedVariableLst[i]].X(y);
        //                }
        //                if ((aSplit * xts + OptVarSum + c) > 0) {
        //                    ++leftCount;
        //                    if (!leftNode.ContainsKey(n.MvTb.Data.TC[y]))
        //                        leftNode.Add(n.MvTb.Data.TC[y], 1);
        //                    else
        //                        ++leftNode[n.MvTb.Data.TC[y]];
        //                } else {
        //                    ++rightCount;
        //                    if (!rightNode.ContainsKey(n.MvTb.Data.TC[y]))
        //                        rightNode.Add(n.MvTb.Data.TC[y], 1);
        //                    else
        //                        ++rightNode[n.MvTb.Data.TC[y]];
        //                }
        //            }
        //            leftImp = Gini.ImpCat(leftNode, (int)leftCount);
        //            rightImp = Gini.ImpCat(rightNode, (int)rightCount);
        //            if (infoNotYetCalculated) {
        //                lowestSplitImp = (leftCount * leftImp + rightCount * rightImp) / n.Table.RowCount;
        //                aSplitB = aSplit; xtsB = xts; xjmB = xjm;
        //                infoNotYetCalculated = false;
        //                n.PredMvLst[NonOptimisedVariableLst[j]].Coef = aSplit;
        //            } else {
        //                splitImp = (leftCount * leftImp + rightCount * rightImp) / n.Table.RowCount;
        //                if (splitImp < lowestSplitImp) {
        //                    lowestSplitImp = splitImp;
        //                    n.PredMvLst[NonOptimisedVariableLst[j]].Coef = aSplit;
        //                    aSplitB = aSplit; xtsB = xts; xjmB = xjm;
        //                }
        //            }
        //            leftNode.Clear();
        //            rightNode.Clear();
        //            leftCount = 0;
        //            rightCount = 0;
        //        }
        //    }
        //    double bestUniGain = p.Gain;
        //    bestMvGain = (n.Imp - lowestSplitImp) * 100 / n.Imp;
        //    bestMvGain *= (double)(n.Table.RowCount - p.NullCount) / n.Table.RowCount;
        //    string coefStr = "";
        //    for (int i = 0; i < n.PredMvLst.Count; ++i) {
        //        if (n.PredMvLst[i].Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous)
        //            coefStr += n.PredMvLst[i].Coef + "(" + n.PredMvLst[i].Variable.Name + ") ";
        //        else
        //            coefStr += n.PredMvLst[i].Coef + "(" + n.PredMvLst[i].Variable.Name + n.PredMvLst[i].Offset + ") ";
        //    }
        //    coefStr += "c= " + c;
        //    //MessageBox.Show("Gain= " + p.Gain + " bestUniGain= " + bestUniGain + Environment.NewLine + coefStr);
        //    Def.LogMessage += "Gain= " + bestMvGain + " bestUniGain= " + bestUniGain + Environment.NewLine + coefStr + Environment.NewLine + Environment.NewLine;
        //    n.MvTb.DataEmpty();
        //    return p.Gain;
        //}


        /// Sets the best p.SplitHyperplane and p.Gain
        /// <summary>
        /// Look for the local minimum by fixing all the coeficients except one and then vary it
        /// </summary>
        //public static double MinImpMvStartingFromBestNominalNoOtherOrder(NodeTargetCategorical n, Predictor p) {

        //    bool infoNotYetCalculated = true;
        //    List<double> bestCoefLst = new List<double>();
        //    List<double> testCoefLst = new List<double>(); // VariableToBeTestedCoodinates
        //    SortedList<string, int> leftNode = new SortedList<string, int>();
        //    SortedList<string, int> rightNode = new SortedList<string, int>();
        //    List<int> optimisedVariableLst = new List<int>(); //Stores the index of predictors that have been used
        //    List<int> NonOptimisedVariableLst = new List<int>(); //Stores the index of predictors that have been used
        //    // starts with the best split index
        //    double leftImp = 0, rightImp = 0;
        //    List<double> azLst = new List<double>();
        //    double leftCount = 0, rightCount = 0;// Left and right item count
        //    double c = 0; // (= best split coordinate) Constant of the equation A1X1 + A2X2 + C = V [0] normalised [1] original value
        //    double az;          // New linear coefficient
        //    double aSplit = 0;    // Linear coefficient candidate for the split
        //    double xjm = 0;      // Normalised coordinate
        //    double xts = 0;     // Normalised coordinate of the variable being tested
        //    double lowestSplitImp = n.Imp, splitImp = 0;
        //    double OptVarSum;
        //    double aSplitB = 0, xtsB = 0, xjmB = 0;
        //    double v = 0;   // see On growing better decision trees from data page 49
        //    double bestMvGain;
        //    int j; // NonOptimisedVariableLst index
        //    SortedList<double, int> orderedNominalGain = new SortedList<double, int>(); // value, index or PredictorLst
        //    int bestNominalSplitIdx = -1;
        //    SortedList<double, int> orderedNumericGain = new SortedList<double, int>(); // value, index or PredictorLst

        //    Def.LogMessage += "Starting MinImpContMvStartingFromBestNominalNoOtherOrder" + Environment.NewLine;

        //    for (int i = 0; i < n.PredictorLst.Count; ++i) {
        //        if (n.PredictorLst[i].Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Categorical) {
        //            orderedNominalGain.Add(n.PredictorLst[i].Gain, i);
        //        }
        //    }

        //    if (orderedNominalGain.Count == 0) {
        //        Def.LogMessage += "No suitable nominal split to be used as startpoint" + Environment.NewLine;
        //        return -1;
        //    }
        //    bestNominalSplitIdx = orderedNominalGain.Values[orderedNominalGain.Count - 1];


        //    List<int> caseLst;
        //    foreach (PredictorMv pmv in n.PredMvLst) {
        //        pmv.Coef = 0;
        //        if (pmv.Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Categorical) {
        //            pmv.Optimised = true;
        //            optimisedVariableLst.Add(pmv.PredMvLstIdx);
        //            pmv.Coef = 0;
        //        }
        //    }
        //    n.MvTb.DataFill();

        //    c = 0;
        //    caseLst = n.PredictorLst[bestNominalSplitIdx].ChildrenGroups.ValueGroupLst[0];
        //    for (int i = 0; i < caseLst.Count; ++i) {
        //        n.PredMvLst[n.PredictorLst[bestNominalSplitIdx].PredMvBase + caseLst[i]].Coef = 1;
        //    }


        //    //if (n.BestSplit.Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
        //    //    c=n.SplitValue;
        //    //    n.PredMvLst[n.BestSplit.PredMvBase].BestCoef = 1; // variable used to split has coef 1
        //    //    n.PredMvLst[n.BestSplit.PredMvBase].Optimised = true;
        //    //    optimisedVariableLst.Add(n.BestSplit.PredMvBase);
        //    //}

        //    //else {
        //    //    c = 0;
        //    //    caseLst = n.BestSplit.ChildrenGroups.ValueGroupLst[0];
        //    //    for (int i = 0; i < caseLst.Count; ++i) {
        //    //          n.PredMvLst[n.BestSplit.PredMvBase + caseLst[i]].BestCoef=0;
        //    //    }
        //    //}

        //    //for (int pidx = 0; pidx < n.PredictorLst.Count; ++pidx) {
        //    //    caseLst = n.PredictorLst[pidx].ChildrenGroups.ValueGroupLst[0];
        //    //    for (int i = 0; i < caseLst.Count; ++i) {
        //    //        n.PredMvLst[n.PredictorLst[pidx].PredMvBase + caseLst[i]].BestCoef = 0;
        //    //    }
        //    //}

        //    for (int i = 0; i < n.PredMvLst.Count; ++i) {
        //        if (n.PredMvLst[i].Optimised == false)
        //            NonOptimisedVariableLst.Add(i);
        //    }

        //    for (j = 0; j < NonOptimisedVariableLst.Count; ++j) {
        //        azLst.Clear();
        //        for (int y = 0; y < n.Table.RowCount; ++y) {
        //            OptVarSum = 0;
        //            for (int i = 0; i < optimisedVariableLst.Count; ++i) {
        //                OptVarSum += n.PredMvLst[optimisedVariableLst[i]].Coef * n.PredMvLst[optimisedVariableLst[i]].X(y);
        //            }
        //            v = OptVarSum + c;
        //            az = ((n.PredMvLst[NonOptimisedVariableLst[j]].Coef * n.PredMvLst[NonOptimisedVariableLst[j]].X(y)) - v) / n.PredMvLst[NonOptimisedVariableLst[j]].X(y);
        //            azLst.Add(az);
        //        }
        //        azLst.Sort();
        //        for (int azIdx = 0; azIdx < azLst.Count - 1; ++azIdx) {
        //            //Finally we got a new linear coefficient for a variable that
        //            //had linear coefficient = 0 
        //            //The new hyperplane is:
        //            // A1X1 + (1)X2 + c = 0 (c= - bestSplitNorm)
        //            // (new midpoint coef) * testVar + (coordinate of the bestSplit variable) - bestSplitNorm; 
        //            // aSplit * Fcn.NormP1(testLst[y], PredictorLst[i].LowerNumber, PredictorLst[i].HigherNumber);
        //            // +
        //            aSplit = (azLst[azIdx] + azLst[azIdx + 1]) / 2;
        //            for (int y = 0; y < n.Table.RowCount; ++y) {
        //                xts = n.PredMvLst[NonOptimisedVariableLst[j]].X(y);
        //                //                      //  xjm = Fcn.NormP1(nnt.N0, n.BestSplit.LowerNumber, n.BestSplit.HigherNumber);
        //                //am = Fcn.NormP1(n.BestSplit.SplitValue, n.PredictorLst[i].LowerNumber, n.PredictorLst[i].HigherNumber);
        //                OptVarSum = 0;
        //                for (int i = 0; i < optimisedVariableLst.Count; ++i) {
        //                    OptVarSum += n.PredMvLst[optimisedVariableLst[i]].Coef * n.PredMvLst[optimisedVariableLst[i]].X(y);
        //                }
        //                if ((aSplit * xts + OptVarSum + c) > 0) {
        //                    ++leftCount;
        //                    if (!leftNode.ContainsKey(n.MvTb.Data.TC[y]))
        //                        leftNode.Add(n.MvTb.Data.TC[y], 1);
        //                    else
        //                        ++leftNode[n.MvTb.Data.TC[y]];
        //                } else {
        //                    ++rightCount;
        //                    if (!rightNode.ContainsKey(n.MvTb.Data.TC[y]))
        //                        rightNode.Add(n.MvTb.Data.TC[y], 1);
        //                    else
        //                        ++rightNode[n.MvTb.Data.TC[y]];
        //                }
        //            }
        //            leftImp = Gini.ImpCat(leftNode, (int)leftCount);
        //            rightImp = Gini.ImpCat(rightNode, (int)rightCount);
        //            if (infoNotYetCalculated) {
        //                lowestSplitImp = (leftCount * leftImp + rightCount * rightImp) / n.Table.RowCount;
        //                aSplitB = aSplit; xtsB = xts; xjmB = xjm;
        //                infoNotYetCalculated = false;
        //                n.PredMvLst[NonOptimisedVariableLst[j]].Coef = aSplit;
        //            } else {
        //                splitImp = (leftCount * leftImp + rightCount * rightImp) / n.Table.RowCount;
        //                if (splitImp < lowestSplitImp) {
        //                    lowestSplitImp = splitImp;
        //                    n.PredMvLst[NonOptimisedVariableLst[j]].Coef = aSplit;
        //                    aSplitB = aSplit; xtsB = xts; xjmB = xjm;
        //                }
        //            }
        //            leftNode.Clear();
        //            rightNode.Clear();
        //            leftCount = 0;
        //            rightCount = 0;

        //        }
        //    }
        //    double bestUniGain = p.Gain;
        //    bestMvGain = (n.Imp - lowestSplitImp) * 100 / n.Imp;
        //    bestMvGain *= (double)(n.Table.RowCount - p.NullCount) / n.Table.RowCount;
        //    string coefStr = "";
        //    for (int i = 0; i < n.PredMvLst.Count; ++i) {
        //        if (n.PredMvLst[i].Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous)
        //            coefStr += n.PredMvLst[i].Coef + "(" + n.PredMvLst[i].Variable.Name + ") ";
        //        else
        //            coefStr += n.PredMvLst[i].Coef + "(" + n.PredMvLst[i].Variable.Name + n.PredMvLst[i].Offset + ") ";
        //    }
        //    coefStr += "c= " + c;
        //    //MessageBox.Show("Gain= " + p.Gain + " bestUniGain= " + bestUniGain + Environment.NewLine + coefStr);
        //    Def.LogMessage += "Gain= " + bestMvGain + " bestUniGain= " + bestUniGain + Environment.NewLine + coefStr + Environment.NewLine + Environment.NewLine;
        //    n.MvTb.DataEmpty();
        //    return p.Gain;
        //}


        /// Sets the best p.SplitHyperplane and p.Gain
        /// <summary>
        /// Look for the local minimum by fixing all the coeficients except one and then vary it
        /// </summary>
        //public static double MinImpMvStartingFromBestNominalOrderedByBestUnivariateNumericalSplit(NodeTargetCategorical n, Predictor p) {

        //    bool infoNotYetCalculated = true;
        //    List<double> bestCoefLst = new List<double>();
        //    List<double> testCoefLst = new List<double>(); // VariableToBeTestedCoodinates
        //    SortedList<string, int> leftNode = new SortedList<string, int>();
        //    SortedList<string, int> rightNode = new SortedList<string, int>();
        //    List<int> optimisedVariableLst = new List<int>(); //Stores the index of predictors that have been used
        //    List<int> NonOptimisedVariableLst = new List<int>(); //Stores the index of predictors that have been used
        //    // starts with the best split index
        //    double leftImp = 0, rightImp = 0;
        //    List<double> azLst = new List<double>();
        //    double leftCount = 0, rightCount = 0;// Left and right item count
        //    double c = 0; // (= best split coordinate) Constant of the equation A1X1 + A2X2 + C = V [0] normalised [1] original value
        //    double az;          // New linear coefficient
        //    double aSplit = 0;    // Linear coefficient candidate for the split
        //    double xjm = 0;      // Normalised coordinate
        //    double xts = 0;     // Normalised coordinate of the variable being tested
        //    double lowestSplitImp = n.Imp, splitImp = 0;
        //    double OptVarSum;
        //    double aSplitB = 0, xtsB = 0, xjmB = 0;
        //    double v = 0;   // see On growing better decision trees from data page 49
        //    double bestMvGain;
        //    int j; // NonOptimisedVariableLst index
        //    SortedList<double, int> orderedNominalGain = new SortedList<double, int>(); // value, index or PredictorLst
        //    SortedList<double, int> orderedNumericGain = new SortedList<double, int>(); // value, index or PredictorLst
        //    int bestNominalSplitIdx = -1;

        //    Def.LogMessage += "Starting MinImpContMvStartingFromBestNominalOrderedByBestUnivariateNumericalSplit" + Environment.NewLine;

        //    for (int i = 0; i < n.PredictorLst.Count; ++i) {
        //        if (n.PredictorLst[i].Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Categorical) {
        //            orderedNominalGain.Add(n.PredictorLst[i].Gain, i);
        //        }
        //    }

        //    if (orderedNominalGain.Count == 0) {
        //        Def.LogMessage += "No suitable nominal split to be used as startpoint" + Environment.NewLine;
        //        return -1;
        //    }
        //    bestNominalSplitIdx = orderedNominalGain.Values[orderedNominalGain.Count - 1];

        //    for (int i = 0; i < n.PredictorLst.Count; ++i) {
        //        if (n.PredictorLst[i].Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
        //            orderedNumericGain.Add(n.PredictorLst[i].Gain, i);
        //        }
        //    }

        //    List<int> caseLst;
        //    foreach (PredictorMv pmv in n.PredMvLst) {
        //        pmv.Coef = 0;
        //        if (pmv.Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Categorical) {
        //            pmv.Optimised = true;
        //            optimisedVariableLst.Add(pmv.PredMvLstIdx);
        //            pmv.Coef = 0;
        //            //caseLst = n.PredictorLst[pmv.PredictorLstIdx].ChildrenGroups.ValueGroupLst[0];
        //            //for (int i = 0; i < caseLst.Count; ++i) {
        //            //    n.PredMvLst[pmv.PredMvLstIdx + caseLst[i]].BestCoef = 1;
        //            //}
        //        }
        //    }
        //    n.MvTb.DataFill();

        //    c = 0;
        //    caseLst = n.PredictorLst[bestNominalSplitIdx].ChildrenGroups.ValueGroupLst[0];
        //    for (int i = 0; i < caseLst.Count; ++i) {
        //        n.PredMvLst[n.PredictorLst[bestNominalSplitIdx].PredMvBase + caseLst[i]].Coef = 1;
        //    }

        //    //if (n.BestSplit.Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
        //    //    c=n.SplitValue;
        //    //    n.PredMvLst[n.BestSplit.PredMvBase].BestCoef = 1; // variable used to split has coef 1
        //    //    n.PredMvLst[n.BestSplit.PredMvBase].Optimised = true;
        //    //    optimisedVariableLst.Add(n.BestSplit.PredMvBase);
        //    //}

        //    //else {
        //    //    c = 0;
        //    //    caseLst = n.BestSplit.ChildrenGroups.ValueGroupLst[0];
        //    //    for (int i = 0; i < caseLst.Count; ++i) {
        //    //          n.PredMvLst[n.BestSplit.PredMvBase + caseLst[i]].BestCoef=0;
        //    //    }
        //    //}

        //    //for (int pidx = 0; pidx < n.PredictorLst.Count; ++pidx) {
        //    //    caseLst = n.PredictorLst[pidx].ChildrenGroups.ValueGroupLst[0];
        //    //    for (int i = 0; i < caseLst.Count; ++i) {
        //    //        n.PredMvLst[n.PredictorLst[pidx].PredMvBase + caseLst[i]].BestCoef = 0;
        //    //    }
        //    //}

        //    //Set the order for nonOptimisedVariables to start by the univariate gain
        //    //orderedNumericGain.Count -1 is the best and was used already
        //    for (int i = orderedNumericGain.Count - 2; i >= 0; --i) {
        //        NonOptimisedVariableLst.Add(n.PredictorLst[orderedNumericGain.Values[i]].PredMvBase);
        //    }

        //    for (int i = 0; i < n.PredMvLst.Count; ++i) {
        //        if (n.PredMvLst[i].Optimised == false) {
        //            if (!NonOptimisedVariableLst.Contains(i))
        //                NonOptimisedVariableLst.Add(i);
        //        }
        //    }

        //    for (j = 0; j < NonOptimisedVariableLst.Count; ++j) {
        //        azLst.Clear();
        //        for (int y = 0; y < n.Table.RowCount; ++y) {
        //            OptVarSum = 0;
        //            for (int i = 0; i < optimisedVariableLst.Count; ++i) {
        //                OptVarSum += n.PredMvLst[optimisedVariableLst[i]].Coef * n.PredMvLst[optimisedVariableLst[i]].X(y);
        //            }
        //            v = OptVarSum + c;
        //            az = ((n.PredMvLst[NonOptimisedVariableLst[j]].Coef * n.PredMvLst[NonOptimisedVariableLst[j]].X(y)) - v) / n.PredMvLst[NonOptimisedVariableLst[j]].X(y);
        //            azLst.Add(az);
        //        }
        //        azLst.Sort();
        //        for (int azIdx = 0; azIdx < azLst.Count - 1; ++azIdx) {
        //            //Finally we got a new linear coefficient for a variable that
        //            //had linear coefficient = 0 
        //            //The new hyperplane is:
        //            // A1X1 + (1)X2 + c = 0 (c= - bestSplitNorm)
        //            // (new midpoint coef) * testVar + (coordinate of the bestSplit variable) - bestSplitNorm; 
        //            // aSplit * Fcn.NormP1(testLst[y], PredictorLst[i].LowerNumber, PredictorLst[i].HigherNumber);
        //            // +
        //            aSplit = (azLst[azIdx] + azLst[azIdx + 1]) / 2;
        //            for (int y = 0; y < n.Table.RowCount; ++y) {
        //                xts = n.PredMvLst[NonOptimisedVariableLst[j]].X(y);
        //                //                      //  xjm = Fcn.NormP1(nnt.N0, n.BestSplit.LowerNumber, n.BestSplit.HigherNumber);
        //                //am = Fcn.NormP1(n.BestSplit.SplitValue, n.PredictorLst[i].LowerNumber, n.PredictorLst[i].HigherNumber);
        //                OptVarSum = 0;
        //                for (int i = 0; i < optimisedVariableLst.Count; ++i) {
        //                    OptVarSum += n.PredMvLst[optimisedVariableLst[i]].Coef * n.PredMvLst[optimisedVariableLst[i]].X(y);
        //                }
        //                if ((aSplit * xts + OptVarSum + c) > 0) {
        //                    ++leftCount;
        //                    if (!leftNode.ContainsKey(n.MvTb.Data.TC[y]))
        //                        leftNode.Add(n.MvTb.Data.TC[y], 1);
        //                    else
        //                        ++leftNode[n.MvTb.Data.TC[y]];
        //                } else {
        //                    ++rightCount;
        //                    if (!rightNode.ContainsKey(n.MvTb.Data.TC[y]))
        //                        rightNode.Add(n.MvTb.Data.TC[y], 1);
        //                    else
        //                        ++rightNode[n.MvTb.Data.TC[y]];
        //                }
        //            }
        //            leftImp = Gini.ImpCat(leftNode, (int)leftCount);
        //            rightImp = Gini.ImpCat(rightNode, (int)rightCount);
        //            if (infoNotYetCalculated) {
        //                lowestSplitImp = (leftCount * leftImp + rightCount * rightImp) / n.Table.RowCount;
        //                aSplitB = aSplit; xtsB = xts; xjmB = xjm;
        //                infoNotYetCalculated = false;
        //                n.PredMvLst[NonOptimisedVariableLst[j]].Coef = aSplit;
        //            } else {
        //                splitImp = (leftCount * leftImp + rightCount * rightImp) / n.Table.RowCount;
        //                if (splitImp < lowestSplitImp) {
        //                    lowestSplitImp = splitImp;
        //                    n.PredMvLst[NonOptimisedVariableLst[j]].Coef = aSplit;
        //                    aSplitB = aSplit; xtsB = xts; xjmB = xjm;
        //                }
        //            }
        //            leftNode.Clear();
        //            rightNode.Clear();
        //            leftCount = 0;
        //            rightCount = 0;
        //        }
        //    }
        //    double bestUniGain = p.Gain;
        //    bestMvGain = (n.Imp - lowestSplitImp) * 100 / n.Imp;
        //    bestMvGain *= (double)(n.Table.RowCount - p.NullCount) / n.Table.RowCount;
        //    string coefStr = "";
        //    for (int i = 0; i < n.PredMvLst.Count; ++i) {
        //        if (n.PredMvLst[i].Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous)
        //            coefStr += n.PredMvLst[i].Coef + "(" + n.PredMvLst[i].Variable.Name + ") ";
        //        else
        //            coefStr += n.PredMvLst[i].Coef + "(" + n.PredMvLst[i].Variable.Name + n.PredMvLst[i].Offset + ") ";
        //    }
        //    coefStr += "c= " + c;
        //    //MessageBox.Show("Gain= " + p.Gain + " bestUniGain= " + bestUniGain + Environment.NewLine + coefStr);
        //    Def.LogMessage += "Gain= " + bestMvGain + " bestUniGain= " + bestUniGain + Environment.NewLine + coefStr + Environment.NewLine + Environment.NewLine;
        //    n.MvTb.DataEmpty();
        //    return p.Gain;
        //}

        /// <summary>
        /// Receives one PredMvIdx to be tested, and the constant and return the:
        /// 
        /// Return[0] the best value for the coefficient 0 or 1
        /// Return[1] the lowestSplitImp for this coefficient
        /// </summary>
        /// <param name="n"></param>
        /// <param name="bestCoefLst"></param>
        /// <param name="c"></param>
        /// <param name="testPredMvIdx"></param>
        /// <returns></returns>
        public static double[] BestGainMvCategorical(NodeTargetCategorical n, double c, int testPredMvIdx, List<int> optimisedVariableLst) {

            double OptVarSum, bestAts=0;
            bool infoNotYetCalculated=true;
            double leftImp = 0, rightImp = 0;
            double leftCount = 0, rightCount = 0;// Left and right item count
            SortedList<string, int> leftNode = new SortedList<string, int>();
            SortedList<string, int> rightNode = new SortedList<string, int>();
            double lowestSplitImp =0, splitImp = 0;
            double minCoefCat = Def.AbsentCoefficientValue, maxCoefCat = Def.PresentCoefficientValue;


            //Instead of having a large list of coefficients,
            //there are only two possible linear coefficients, it will test the values for minCoefCat and maxCoefCat
            // ats = coeficient being tested
            for (double ats = minCoefCat; ats <= maxCoefCat; ats += maxCoefCat - minCoefCat) {
                leftNode.Clear();
                rightNode.Clear();
                leftCount = 0;
                rightCount = 0;
                for (int y = 0; y < n.Table.RowCount; ++y) {
                            //Very important detail
                            OptVarSum = ats * n.PredMvLst[testPredMvIdx].X(y);
//                            for (int i = 0; i < optimisedVariableLst.Count; ++i) {
//                                OptVarSum += n.PredMvLst[optimisedVariableLst[i]].Coef * n.PredMvLst[optimisedVariableLst[i]].X(y);
                            for (int i = 0; i < n.PredMvLst.Count; ++i) {
                                  OptVarSum += n.PredMvLst[i].Coef * n.PredMvLst[i].X(y);
                            }
                            if ( ( OptVarSum + c ) <= 0) {
                                ++leftCount;
                                if (!leftNode.ContainsKey(n.MvTb.Data.TC[y]))
                                    leftNode.Add(n.MvTb.Data.TC[y], 1);
                                else
                                    ++leftNode[n.MvTb.Data.TC[y]];
                            } else {
                                ++rightCount;
                                if (!rightNode.ContainsKey(n.MvTb.Data.TC[y]))
                                    rightNode.Add(n.MvTb.Data.TC[y], 1);
                                else
                                    ++rightNode[n.MvTb.Data.TC[y]];
                            }
                }
                leftImp = Gini.ImpCat(leftNode, (int) leftCount);
                rightImp = Gini.ImpCat(rightNode, (int) rightCount);
                if (rightCount < Def.TreeMinNumberOfCasesPerNode || leftCount < Def.TreeMinNumberOfCasesPerNode)
                    continue;
                if (infoNotYetCalculated) {
                    lowestSplitImp = (leftCount * leftImp + rightCount * rightImp) / n.Table.RowCount;
                    infoNotYetCalculated = false;
                    bestAts = minCoefCat;
                } else {
                    splitImp = (leftCount * leftImp + rightCount * rightImp) / n.Table.RowCount;
                    if (splitImp < lowestSplitImp) {
                        lowestSplitImp = splitImp;
                        bestAts = maxCoefCat;
                    }
                }
            }
                
            double[] r = new double[2];
            r[0] = bestAts;
            r[1] = lowestSplitImp;
            if (infoNotYetCalculated) {
                r[0] = Double.NaN;
                r[1] = Double.NaN;
            }

            return r;
        }

        /// <summary>
        /// Receives one PredMvIdx to be tested, and the constant and returns:
        /// 
        /// Return[0] the best value for the coefficient 
        /// Return[1] the lowestSplitImp for this coefficient
        /// </summary>
        /// <param name="n"></param>
        /// <param name="bestCoefLst"></param>
        /// <param name="c"></param>
        /// <param name="testPredMvIdx"></param>
        /// <returns></returns>
        public static double[] BestGainMvNumeric(NodeTargetCategorical n, double c, int testPredMvIdx, List<int> optimisedVariableLst, FrmGraph fg) {

            double OptVarSum;
            bool infoNotYetCalculated = true;
            double leftImp = 0, rightImp = 0;
            double v, az, aSplit, xts,aSplitB=-9999999999;
            SortedList<double, int> azLst = new SortedList<double, int>();
            double leftCount = 0, rightCount = 0;// Left and right item count
            SortedList<string, int> leftNode = new SortedList<string, int>();
            SortedList<string, int> rightNode = new SortedList<string, int>();
            double lowestSplitImp = 9999999, splitImp = 9999999;

            if (!Def.ExperimentRunning) {
                fg.ALst.Clear();
                fg.AbsLst.Clear();
            }

            azLst.Clear();
            for (int y = 0; y < n.Table.RowCount; ++y) {
                OptVarSum = 0;
                //for (int i = 0; i < optimisedVariableLst.Count; ++i) {
                //    OptVarSum += n.PredMvLst[optimisedVariableLst[i]].Coef * n.PredMvLst[optimisedVariableLst[i]].X(y);
                for (int i = 0; i < n.PredMvLst.Count; ++i) {
                    OptVarSum += n.PredMvLst[i].Coef * n.PredMvLst[i].X(y);
                }
                v = OptVarSum + c;
                if (n.PredMvLst[testPredMvIdx].X(y) != 0) {
                    az = ((n.PredMvLst[testPredMvIdx].Coef * n.PredMvLst[testPredMvIdx].X(y)) - v) / n.PredMvLst[testPredMvIdx].X(y);
                    if (!azLst.ContainsKey(az) ) {
                        azLst.Add(az, 1);
                        if (!Def.ExperimentRunning) 
                            fg.ALst.Add((float)az);
                    }
                }
            }
            for (int azIdx = 0; azIdx < azLst.Count - 1; ++azIdx) {
                //Finally we got a new linear coefficient for a variable that
                //had linear coefficient = 0 
                //The new hyperplane is:
                // A1X1 + (1)X2 + c = 0 (c= - bestSplitNorm)
                // (new midpoint coef) * testVar + (coordinate of the bestSplit variable) - bestSplitNorm; 
                // aSplit * Fcn.NormP1(testLst[y], PredictorLst[i].LowerNumber, PredictorLst[i].HigherNumber);
                aSplit = (azLst.Keys[azIdx] + azLst.Keys[azIdx + 1]) / 2;
                leftNode.Clear();
                rightNode.Clear();
                leftCount = 0;
                rightCount = 0;                
                if (!Def.ExperimentRunning) 
                    fg.AbsLst.Add((float)aSplit);
                for (int y = 0; y < n.Table.RowCount; ++y) {

                    xts = n.PredMvLst[testPredMvIdx].X(y);
                    OptVarSum = 0;
                    for (int i = 0; i < optimisedVariableLst.Count; ++i) {
                        OptVarSum += n.PredMvLst[optimisedVariableLst[i]].Coef * n.PredMvLst[optimisedVariableLst[i]].X(y);
                    }
                    if ((aSplit * xts + OptVarSum + c) <= 0) {
                        ++leftCount;
                        if (!leftNode.ContainsKey(n.MvTb.Data.TC[y]))
                            leftNode.Add(n.MvTb.Data.TC[y], 1);
                        else
                            ++leftNode[n.MvTb.Data.TC[y]];
                    } else {
                        ++rightCount;
                        if (!rightNode.ContainsKey(n.MvTb.Data.TC[y]))
                            rightNode.Add(n.MvTb.Data.TC[y], 1);
                        else
                            ++rightNode[n.MvTb.Data.TC[y]];
                    }
                }
                if (rightCount < Def.TreeMinNumberOfCasesPerNode || leftCount < Def.TreeMinNumberOfCasesPerNode)
                    continue;
                leftImp = Gini.ImpCat(leftNode, (int)leftCount);
                rightImp = Gini.ImpCat(rightNode, (int)rightCount);
                if (infoNotYetCalculated) {
                    infoNotYetCalculated = false;
                    lowestSplitImp = (leftCount * leftImp + rightCount * rightImp) / n.Table.RowCount;
                    if (n.PredMvLst[testPredMvIdx].Variable.VariableTypeDetected == SchemaVariable.VariableTypeEnum.Categorical) {
                        MessageBox.Show("Error n.PredMvLst[testPredMvIdx].Variable is categorical ");
                    }
                    aSplitB = aSplit;
                } else {
                    splitImp = (leftCount * leftImp + rightCount * rightImp) / n.Table.RowCount;
                    if (splitImp < lowestSplitImp) {
                        lowestSplitImp = splitImp;
                        if (n.PredMvLst[testPredMvIdx].Variable.VariableTypeDetected == SchemaVariable.VariableTypeEnum.Categorical) {
                            MessageBox.Show("Error n.PredMvLst[testPredMvIdx].Variable is categorical ");
                        }
                        aSplitB = aSplit;
                    }
                }
            }

            //Creates bissections for the last and the 'x0 axis'
            if (azLst.Count > 1) { // if only 1 is the zero
                aSplit = azLst.Keys[azLst.Count-1] * 2;
                if (!Def.ExperimentRunning) 
                    fg.AbsLst.Add((float)aSplit);
                leftNode.Clear();
                rightNode.Clear();
                leftCount = 0;
                rightCount = 0;
                for (int y = 0; y < n.Table.RowCount; ++y) {
                    xts = n.PredMvLst[testPredMvIdx].X(y);
                    OptVarSum = 0;
                    for (int i = 0; i < optimisedVariableLst.Count; ++i) {
                        OptVarSum += n.PredMvLst[optimisedVariableLst[i]].Coef * n.PredMvLst[optimisedVariableLst[i]].X(y);
                    }
                    if ((aSplit * xts + OptVarSum + c) <= 0) {
                        ++leftCount;
                        if (!leftNode.ContainsKey(n.MvTb.Data.TC[y]))
                            leftNode.Add(n.MvTb.Data.TC[y], 1);
                        else
                            ++leftNode[n.MvTb.Data.TC[y]];
                    } else {
                        ++rightCount;
                        if (!rightNode.ContainsKey(n.MvTb.Data.TC[y]))
                            rightNode.Add(n.MvTb.Data.TC[y], 1);
                        else
                            ++rightNode[n.MvTb.Data.TC[y]];
                    }
                }
                if (rightCount >= Def.TreeMinNumberOfCasesPerNode && leftCount >= Def.TreeMinNumberOfCasesPerNode) {
                    leftImp = Gini.ImpCat(leftNode, (int)leftCount);
                    rightImp = Gini.ImpCat(rightNode, (int)rightCount);
                    if (infoNotYetCalculated) {
                        infoNotYetCalculated = false;
                        lowestSplitImp = (leftCount * leftImp + rightCount * rightImp) / n.Table.RowCount;
                        if (n.PredMvLst[testPredMvIdx].Variable.VariableTypeDetected == SchemaVariable.VariableTypeEnum.Categorical) {
                            MessageBox.Show("Error n.PredMvLst[testPredMvIdx].Variable is categorical ");
                        }
                        aSplitB = aSplit;
                    } else {
                        splitImp = (leftCount * leftImp + rightCount * rightImp) / n.Table.RowCount;
                        if (splitImp < lowestSplitImp) {
                            lowestSplitImp = splitImp;
                            if (n.PredMvLst[testPredMvIdx].Variable.VariableTypeDetected == SchemaVariable.VariableTypeEnum.Categorical) {
                                MessageBox.Show("Error n.PredMvLst[testPredMvIdx].Variable is categorical ");
                            }
                            aSplitB = aSplit;
                        }
                    }
                }//end if (rightCount >= Def.TreeMinNumberOfCasesPerNode && leftCount >= Def.TreeMinNumberOfCasesPerNode) {
            }
            double[] r = new double[2];
            r[0] = aSplitB;
            r[1] = lowestSplitImp;
            return r;
        }

        /// Sets the best p.SplitHyperplane and p.Gain
        /// <summary>
        /// Look for the local minimum by fixing all the coeficients except one and then vary it
        /// </summary>
        public static double MinImpMvGreed(NodeTargetCategorical n, Predictor p) {

            bool hadOverallImprovement=false;
            //List<double> bestCoefLst = new List<double>();
            //List<double> testCoefLst = new List<double>(); // VariableToBeTestedCoodinates
            SortedList<string, int> leftNode = new SortedList<string, int>();
            SortedList<string, int> rightNode = new SortedList<string, int>();
            List<int> nonOptimisedVariableLst = new List<int>(); //Stores the index of MV predictors that have been used
            List<int> optimisedVariableLst = new List<int>(); //Stores the index of MV predictors that have been used
            // starts with the best split index
            List<double> azLst = new List<double>();
            n.C = 0; // (= best split coordinate) Constant of the equation A1X1 + A2X2 + C = V [0] normalised [1] original value
            double lowestImp = n.ImpBestUniSplit;
            double bestCoef=-1;            
            double bestMvGain;
            double bestUniGain;
            int bestSplitIdx=-1;
            SortedList<double, int> orderedNominalGain = new SortedList<double, int>(); // value, index or PredictorLst
            SortedList<double, int> orderedNumericGain = new SortedList<double, int>(); // value, index or PredictorLst
           
            FrmGraph fg = new FrmGraph();
            

            bestUniGain = (n.Imp - n.ImpBestUniSplit) * 100 / n.Imp;
            bestUniGain *= (double)(n.Table.RowCount - p.NullCount) / n.Table.RowCount;

            Def.LogMessage += "Starting MinImpMvGreed, smallest univariate imp= " + lowestImp + " bestUniSplit gain " + n.BestSplit.Gain + " should be equal bestUniGain " + bestUniGain + Environment.NewLine;

            for(int i=0; i < n.PredMvLst.Count; ++i){
                n.PredMvLst[i].Optimised = false;
                n.PredMvLst[i].Coef = Def.AbsentCoefficientValue;
                nonOptimisedVariableLst.Add(i);
            }
            if (n.BestSplit.Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
                //Sets the properties of the best univariate split
                #region
                nonOptimisedVariableLst.Remove(n.BestSplit.PredMvBase);
                optimisedVariableLst.Add(n.BestSplit.PredMvBase);
                n.PredMvLst[n.BestSplit.PredMvBase].Coef = 1; 
                n.PredMvLst[n.BestSplit.PredMvBase].Optimised = true;
                n.C = -Fcn.NormP1(n.BestSplit.SplitValue, n.BestSplit.LowerNumber, n.BestSplit.HigherNumber);
               // c = 0;                
#endregion
            }else{//Nominal
                List<string> caseLst;
                n.C = 0;
                //Sets the coefficients of the values on the left with the Def.PresentCoefficientValue
                caseLst = n.PredictorLst[n.BestSplit.PredictorLstIdx].ChildrenGroups.ValueGroupLst[0];
                for (int i = 0; i < caseLst.Count; ++i) {
                    //Not sure if it is right MessageBox.Show("didn't undestand bellow");
                    n.PredMvLst[n.BestSplit.PredMvBase + n.PredictorLst[n.BestSplit.PredictorLstIdx].ValueSd.IndexOfKey(caseLst[i])].Coef = Def.PresentCoefficientValue;
                }
                //If the best split is nominal, then all the column there describe it are already optmised
                for (int i = 0; i < n.PredictorLst[n.BestSplit.PredictorLstIdx].DistinctValuesCount; ++i) {
                    nonOptimisedVariableLst.Remove(n.BestSplit.PredMvBase + i);
                    optimisedVariableLst.Add(n.BestSplit.PredMvBase + i);
                    n.PredMvLst[n.BestSplit.PredMvBase + i].Optimised = true;
                 }
            }

            //if (n.BestSplit.Gain == 100) {
            //    Def.LogMessage += "Nothing to be done, univariate gain equals 100%" + Environment.NewLine;
            //    n.MvTb.DataFill();
            //    DataValidate(n, c);
            //    n.MvTb.DataEmpty();
            //    return p.Gain;
            //}

            double[] r= new double[2];

            while(nonOptimisedVariableLst.Count > 0){
         //while(optimisedVariableLst.Count < 11){
                hadOverallImprovement=false;
                for (int nopVarIdx = 0; nopVarIdx < nonOptimisedVariableLst.Count; ++nopVarIdx) {
                    ////Gets lowestCatSplitImp, catSplitImp;
                    if (n.PredMvLst[nonOptimisedVariableLst[nopVarIdx]].Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Categorical) {
                        r = BestGainMvCategorical(n, n.C, nonOptimisedVariableLst[nopVarIdx], optimisedVariableLst);
                        if (r[1] < lowestImp) {
                            bestCoef = r[0];
                            bestSplitIdx = nonOptimisedVariableLst[nopVarIdx];
                            lowestImp = r[1];
                            hadOverallImprovement=true;
                        }
                    }
                    //Gets lowestNumSplitImp, numSplitImp;
                    else {
                        r = BestGainMvNumeric(n, n.C, nonOptimisedVariableLst[nopVarIdx], optimisedVariableLst, fg);
                        if (r[1] < lowestImp) {
                            bestCoef = r[0];
                            bestSplitIdx = nonOptimisedVariableLst[nopVarIdx];
                            lowestImp = r[1];
                            hadOverallImprovement=true;
                        }
                    }
                    if (n.PredMvLst[nonOptimisedVariableLst[nopVarIdx]].FieldSpan > 0)
                        Def.LogMessage += "     nopVarIdx " + nonOptimisedVariableLst[nopVarIdx] + " Name " + n.PredMvLst[nonOptimisedVariableLst[nopVarIdx]].Variable.Name + "(" + n.PredMvLst[nonOptimisedVariableLst[nopVarIdx]].Offset + ")";
                    else
                        Def.LogMessage += "     nopVarIdx " + nonOptimisedVariableLst[nopVarIdx] + " Name " + n.PredMvLst[nonOptimisedVariableLst[nopVarIdx]].Variable.Name;
                    Def.LogMessage += "     Coefficient " + r[0] + " SplitImp " + r[1] + Environment.NewLine;
                }//End for
                if (hadOverallImprovement) {
                    if (!nonOptimisedVariableLst.Contains(bestSplitIdx)) {
                        Def.LogMessage += "Error couldn't find bestSplitIdx " + bestSplitIdx + " inside nonOptimisedVariableLst " + Environment.NewLine;
                        MessageBox.Show("Error couldn't find bestSplitIdx " + bestSplitIdx + " inside nonOptimisedVariableLst ");
                        return -1;
                    }
                    n.PredMvLst[bestSplitIdx].Coef = bestCoef;
                    n.PredMvLst[bestSplitIdx].Optimised = true;
                    nonOptimisedVariableLst.Remove(bestSplitIdx);
                    optimisedVariableLst.Add(bestSplitIdx);
                    Def.LogMessage += "Best variable " + n.PredMvLst[bestSplitIdx].Variable.Name + n.PredMvLst[bestSplitIdx].Offset + " Coef " + bestCoef + " lowestCatSplitImp " + lowestImp + "++++++++++++++++++++" + Environment.NewLine;
                } else { //No improvement
                    Def.LogMessage += "No more improvement " + (optimisedVariableLst.Count) + " variables of " + n.PredMvLst.Count + " are being combined on the purity function -------------------" + Environment.NewLine;
                    foreach (int predMvIdx in nonOptimisedVariableLst) {
                        optimisedVariableLst.Add(predMvIdx);
                        n.PredMvLst[predMvIdx].Optimised = true;
                    }
                    nonOptimisedVariableLst.Clear();
                }
                if (lowestImp==0) {
                    Def.LogMessage += "Node is now 100% pure stopping greed seach " + Environment.NewLine;
                    break;
                }
            }//End while  
            n.ImpBestMvSplit = lowestImp;

            p.Gain = n.BestSplit.Gain;

            bestMvGain = (n.Imp - n.ImpBestMvSplit) * 100 / n.Imp;
            bestMvGain *= (double)(n.Table.RowCount - p.NullCount) / n.Table.RowCount;

            bestUniGain = (n.Imp - n.ImpBestUniSplit) * 100 / n.Imp;
            bestUniGain *= (double)(n.Table.RowCount - p.NullCount) / n.Table.RowCount;

            string coefStr = "";
            for (int i = 0; i < n.PredMvLst.Count; ++i) {
                if (n.PredMvLst[i].Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous)
                    coefStr += n.PredMvLst[i].Coef + "(" + n.PredMvLst[i].Variable.Name + ") ";
                else
                    coefStr += n.PredMvLst[i].Coef + "(" + n.PredMvLst[i].Variable.Name + n.PredMvLst[i].Offset + ") ";
            }
            coefStr += "c = " + n.C;
            //Why UniGain is sometimes different to p.Gain
            Def.LogMessage += "MvGain: " + bestMvGain + " UniGain: " + bestUniGain + " P.Gain: " + p.Gain + " MvImp: " + lowestImp + " UniImp: " + n.ImpBestUniSplit + " NdImp: " + n.Imp + Environment.NewLine + coefStr + Environment.NewLine;
             
      
            if (!Def.ExperimentRunning) {
                for (int y = 0; y < n.Table.RowCount; ++y) {
                              fg.x0Lst.Add((float) n.PredMvLst[0].X(y));
                        fg.x1Lst.Add((float) n.PredMvLst[1].X(y));
                    if (n.MvTb.Data.TC[y].ToLower() == "n") {
                        
                        fg.n.Add(y);
                    }
                }
                fg.a0=(float) n.PredMvLst[0].Coef;
                fg.a1 = (float) n.PredMvLst[1].Coef;
                fg.c = (float) n.C;
                fg.N = n;
                fg.ABest = (float)bestCoef;
                fg.Show();
                fg.Invalidate();

                DataValidate(n, n.C);
            }
            //if auto n.Imp = 
            return p.Gain;
        }

        public static void DataValidate(NodeTargetCategorical n, double c) {
            FrmGrid fg = new FrmGrid();

            fg.Grid.ColumnCount = n.PredMvLst.Count + 5;
            fg.Grid.RowCount = n.Table.RowCount;
            double v;
            string offset="";
            int i;
            for (i = 0; i < n.PredMvLst.Count; ++i) {
                offset = "";
                if (n.PredMvLst[i].FieldSpan > 1)
                    offset = n.PredMvLst[i].Offset.ToString();
                fg.Grid.Columns[i].HeaderText = n.PredictorLst[n.PredMvLst[i].PredictorLstIdx].Variable.Name + offset;
            }
            fg.Grid.Columns[i].HeaderText = Def.Schema.Target.Name;
            fg.Grid.Columns[i+1].HeaderText = "V";

            for (int y = 0; y < n.Table.RowCount; ++y) {

                v = 0;
                fg.Grid[n.PredMvLst.Count, y].Value = n.MvTb.Data.TC[y];
                for (int x = 0; x < n.PredMvLst.Count; ++x) {
                    fg.Grid[x, y].Value = n.PredMvLst[x].X(y);
                    v += n.PredMvLst[x].X(y) * n.PredMvLst[x].Coef;
                }
                v += c;
                fg.Grid[n.PredMvLst.Count + 1, y].Value = v;
            }
            fg.Show();
        }

        //It the number of variables is too large it can't compute
        //public static double KILLED_MinImpCatRandom(NodeTargetCategorical n, Predictor p) {

        //    //For some partitions gets the min Impiance

        //    int valueCount = p.DistinctValuesCount;

        //    int pos, instanceI;
        //    double partitionCount;
        //    int c, i;
        //    double bestPartition = 0, minImp = Double.NaN, imp = Double.NaN, lImp = Double.NaN, rImp = Double.NaN;
        //    int nodeLfItemCount, nodeRtItemCount;
        //    List<NTT> nttLst;
        //    string binStr = "";

        //    p.SplitStatus = Predictor.SplitStatusEnum.CanBeUsed;
        //    n.DescendentImpPreCalculated = new List<double>(2);
        //    n.DescendentImpPreCalculated.Add(0);
        //    n.DescendentImpPreCalculated.Add(0);

        //    List<string> lComb = new List<string>(valueCount);
        //    List<string> rComb = new List<string>(valueCount);
        //    SortedList<string, int> lPredVal = new SortedList<string, int>();
        //    SortedList<string, int> rPredVal = new SortedList<string, int>();

        //    string sql =
        //    @"SELECT ALL " +
        //        " count(*), " +
        //        Def.DbBsTb + "." + p.Variable.Name + ",  " +
        //        Def.DbBsTb + "." + Def.Schema.Target.Name + " " +
        //    "FROM "
        //        + Def.DbBsTb + "," + Def.DbTrTb + n.Id + " " +
        //    "WHERE "
        //        + Def.DbBsTb + "." + Def.DbTableIdName + " = " +
        //        Def.DbTrTb + n.Id + "." + Def.DbTableIdName + " AND " +
        //        Def.DbBsTb + "." + p.Variable.Name + " IS NOT NULL " +
        //    "GROUP BY " +
        //        Def.DbBsTb + "." + Def.Schema.Target.Name + ", " +
        //        Def.DbBsTb + "." + p.Variable.Name + " ";
        //    nttLst = Def.Db.GetNTTLst(sql);

        //    int instanceCount = n.Table.RowCount;
        //    partitionCount = Math.Pow(2, valueCount - 1) - 1;
        //    double partitionCountMax = 0;
        //    if (partitionCount > 4095) { // 4095
        //        partitionCountMax = 4095;
        //    } else
        //        partitionCountMax = partitionCount;
        //    List<double> partLst = new List<double>((int)partitionCountMax);
        //    i = 1;
        //    //CHECK
        //    for (int t = 0; t < partitionCountMax; ++t) {
        //        partLst.Add((int)RNG.GetUniform(i, partitionCount));
        //        ++i;
        //    }
        //    for (i = 0; i < partitionCountMax; ++i) {
        //        pos = 0;
        //        binStr = Fcn.Decimal2BinaryStr(partLst[(int)i]);
        //        lComb.Clear(); rComb.Clear();
        //        lPredVal.Clear(); rPredVal.Clear();
        //        nodeLfItemCount = nodeRtItemCount = 0;
        //        for (c = 0; c < p.ValueSd.Count; ++c) {
        //            if (binStr[binStr.Length - 1 - c] == '1')
        //                lComb.Add(p.ValueSd.Keys[pos]); //if the 'case' is in, put it on the left
        //            else
        //                rComb.Add(p.ValueSd.Keys[pos]);//else, in the right side
        //            ++pos;
        //        }
        //        for (instanceI = 0; instanceI < nttLst.Count; ++instanceI) {
        //            foreach (string ls in lComb) {
        //                if (nttLst[instanceI].T0 == ls) {
        //                    if (!lPredVal.ContainsKey(nttLst[instanceI].T1))
        //                        lPredVal.Add(nttLst[instanceI].T1, (int)nttLst[instanceI].N);
        //                    else
        //                        lPredVal[nttLst[instanceI].T1] += (int)nttLst[instanceI].N;
        //                    nodeLfItemCount += (int)nttLst[instanceI].N;
        //                    break;
        //                }
        //            }
        //            foreach (string rs in rComb) {
        //                if (nttLst[instanceI].T0 == rs) {
        //                    if (!rPredVal.ContainsKey(nttLst[instanceI].T1))
        //                        rPredVal.Add(nttLst[instanceI].T1, (int)nttLst[instanceI].N);
        //                    else
        //                        rPredVal[nttLst[instanceI].T1] += (int)nttLst[instanceI].N;
        //                    nodeRtItemCount += (int)nttLst[instanceI].N;
        //                    break;
        //                }
        //            }
        //        }
        //        if (nodeLfItemCount >= Def.TreeMinNumberOfCasesPerNode && nodeRtItemCount >= Def.TreeMinNumberOfCasesPerNode) {
        //            lImp = ImpCat(lPredVal, nodeLfItemCount);
        //            rImp = ImpCat(rPredVal, nodeRtItemCount);
        //            if (Double.IsNaN(minImp)) {
        //                bestPartition = partLst[(int)i];
        //                minImp = (double)(nodeLfItemCount) / instanceCount * lImp + (double)nodeRtItemCount / instanceCount * rImp;
        //                n.DescendentImpPreCalculated[0] = lImp;
        //                n.DescendentImpPreCalculated[1] = rImp;
        //            } else {
        //                imp = (double)(nodeLfItemCount) / instanceCount * lImp + (double)nodeRtItemCount / instanceCount * rImp;
        //                if (imp < minImp) {
        //                    minImp = imp;
        //                    bestPartition = partLst[(int)i];
        //                    n.DescendentImpPreCalculated[0] = lImp;
        //                    n.DescendentImpPreCalculated[1] = rImp;
        //                }
        //            }
        //        }
        //    }
        //    if (bestPartition == 0) {
        //        p.SplitStatus = Predictor.SplitStatusEnum.NotEnoughCases;
        //        p.Gain = 0;
        //        return 0;
        //    }

        //    //Set the possible children
        //    ValueGroup valueGroup = new ValueGroup(2);
        //    p.ChildrenGroups = valueGroup;
        //    pos = 0;
        //    binStr = Fcn.Decimal2BinaryStr((double)bestPartition);
        //    for (c = 0; c < p.ValueSd.Count; ++c) {
        //        //                if ((bestPartition & c) == c)
        //        if (binStr[binStr.Length - 1 - c] == '1')
        //            valueGroup.ValueGroupLst[0].Add(pos); //if the 'case' is in put it on the left
        //        else
        //            valueGroup.ValueGroupLst[1].Add(pos);//else, in the right side
        //        ++pos;
        //    }
        //    n.ImpBestUniSplit = minImp;
        //    p.Gain = (n.Imp - minImp) * 100 / n.Imp;
        //    p.Gain *= (double)(n.Table.RowCount - p.NullCount) / n.Table.RowCount;
        //    return p.Gain;
        //}


        //Sets the best p.Gain, valueGroup.ValueGroupLst[0] (left child) and valueGroup.ValueGroupLst[1] (right child)
        public static double MinImpCatFullSearch(NodeTargetCategorical n, Predictor p) {

            //For each possible partition gets the min Imp

            int valueCount = p.DistinctValuesCount;
            if (valueCount > Def.ClfMaxNumberOfValuesForFullSearch) {
                p.SplitStatus = Predictor.SplitStatusEnum.TooManyValuesToSearch;
                p.Gain = 0;
                return 0;
            }

            int partitionCount, pos, instanceI;
            uint c, i, bestPartition = 0;
            double minImp = Double.NaN, imp = Double.NaN, lImp = Double.NaN, rImp = Double.NaN;
            int nodeLfItemCount, nodeRtItemCount;
            List<NTT> nttLst;

            p.SplitStatus = Predictor.SplitStatusEnum.CanBeUsed;
            n.DescendentImpPreCalculated = new List<double>(2);
            n.DescendentImpPreCalculated.Add(0);
            n.DescendentImpPreCalculated.Add(0);

            List<string> lComb = new List<string>(valueCount);
            List<string> rComb = new List<string>(valueCount);
            SortedList<string, int> lPredVal = new SortedList<string, int>();
            SortedList<string, int> rPredVal = new SortedList<string, int>();

            string sql =
            @"SELECT ALL " +
                " count(*), " +
                Def.DbBsTb + "." + p.Variable.Name + ",  " +
                Def.DbBsTb + "." + Def.Schema.Target.Name + " " +
            "FROM "
                + Def.DbBsTb + "," + Def.DbTrTb + n.Id + " " +
            "WHERE "
                + Def.DbBsTb + "." + Def.DbTableIdName + " = " +
                Def.DbTrTb + n.Id + "." + Def.DbTableIdName + " AND " +
                Def.DbBsTb + "." + p.Variable.Name + " IS NOT NULL " +
            "GROUP BY " +
                Def.DbBsTb + "." + Def.Schema.Target.Name + ", " +
                Def.DbBsTb + "." + p.Variable.Name + " ";
            nttLst = Def.Db.GetNTTLst(sql);

            int instanceCount = n.Table.RowCount;

            partitionCount = (int)(Math.Pow(2, valueCount - 1) - 1);

            for (i = 1; i <= partitionCount; ++i) { //Enumerates all the possible partition but the empty
                pos = 0;
                lComb.Clear(); rComb.Clear();
                lPredVal.Clear(); rPredVal.Clear();
                nodeLfItemCount = nodeRtItemCount = 0;
                for (c = 1; c <= partitionCount + 1; c *= 2) {
                    if ((i & c) == c) // i & c == c
                        lComb.Add(p.ValueSd.Keys[pos]); //if the 'case' is in, put it on the left
                    else
                        rComb.Add(p.ValueSd.Keys[pos]);//else, in the right side
                    ++pos;
                }
                for (instanceI = 0; instanceI < nttLst.Count; ++instanceI) {
                    foreach (string ls in lComb) {
                        if (nttLst[instanceI].T0 == ls) {
                            if (!lPredVal.ContainsKey(nttLst[instanceI].T1))
                                lPredVal.Add(nttLst[instanceI].T1, (int)nttLst[instanceI].N);
                            else
                                lPredVal[nttLst[instanceI].T1] += (int)nttLst[instanceI].N;
                            nodeLfItemCount += (int)nttLst[instanceI].N;
                            break;
                        }
                    }
                    foreach (string rs in rComb) {
                        if (nttLst[instanceI].T0 == rs) {
                            if (!rPredVal.ContainsKey(nttLst[instanceI].T1))
                                rPredVal.Add(nttLst[instanceI].T1, (int)nttLst[instanceI].N);
                            else
                                rPredVal[nttLst[instanceI].T1] += (int)nttLst[instanceI].N;
                            nodeRtItemCount += (int)nttLst[instanceI].N;
                            break;
                        }
                    }
                }
                if (nodeLfItemCount >= Def.TreeMinNumberOfCasesPerNode && nodeRtItemCount >= Def.TreeMinNumberOfCasesPerNode) {
                    lImp = ImpCat(lPredVal, nodeLfItemCount);
                    rImp = ImpCat(rPredVal, nodeRtItemCount);
                    if (Double.IsNaN(minImp)) {
                        bestPartition = i;
                        minImp = (double)(nodeLfItemCount) / instanceCount * lImp + (double)nodeRtItemCount / instanceCount * rImp;
                        n.DescendentImpPreCalculated[0] = lImp;
                        n.DescendentImpPreCalculated[1] = rImp;
                    } else {
                        imp = (double)(nodeLfItemCount) / instanceCount * lImp + (double)nodeRtItemCount / instanceCount * rImp;
                        if (imp < minImp) {
                            minImp = imp;
                            bestPartition = i;
                            n.DescendentImpPreCalculated[0] = lImp;
                            n.DescendentImpPreCalculated[1] = rImp;
                        }
                    }
                }
            }
            if (bestPartition == 0) {
                p.SplitStatus = Predictor.SplitStatusEnum.NotEnoughCases;
                p.Gain = 0;
                return 0;
            }

            //Set the possible children
            ValueGroup valueGroup = new ValueGroup(p, 2);
            p.ChildrenGroups = valueGroup;

            pos = 0;
            for (c = 1; c <= partitionCount + 1; c *= 2) {
                if ((bestPartition & c) == c)
                    valueGroup.AddValueFromIndex(pos, 0); //if the 'case' is in put it on the left
                else
                    valueGroup.AddValueFromIndex(pos, 1);//else, in the right side
                ++pos;
            }
            p.ImpUniMin = minImp;
            p.Gain = (n.Imp - minImp) * 100 / n.Imp;
            p.Gain *= (double)(n.Table.RowCount - p.NullCount) / n.Table.RowCount;
            return p.Gain;
        }


        public static double MinImpCatProgressive(NodeTargetCategorical n, Predictor p) {

            //For some partitions gets the min Impiance
            int valueCount = p.DistinctValuesCount;
            double impBest= double.NaN;
            int i;
            int instanceCount = n.Table.RowCount;
            double imp = Double.NaN;
            double impIfValueGoesLeft = Double.NaN, impIfValueGoesRight = Double.NaN;
            List<NTT> nttLst;
            double impBeforePhase2;
            int improvementCode = 0;

            p.SplitStatus = Predictor.SplitStatusEnum.CanBeUsed;
            
            if (valueCount < 2) {
                p.SplitStatus = Predictor.SplitStatusEnum.OnlyOneValueAvailable;
                p.Gain = 0;
                return 0;
            }

            List<string> lComb = new List<string>(valueCount);
            List<string> rComb = new List<string>(valueCount);
            SortedList<string, int> lPredVal = new SortedList<string, int>();
            SortedList<string, int> rPredVal = new SortedList<string, int>();

            string sql =
            @"SELECT ALL " +
                " count(*), " +
                Def.DbBsTb + "." + p.Variable.Name + ",  " +
                Def.DbBsTb + "." + Def.Schema.Target.Name + " " +
            "FROM "
                + Def.DbBsTb + "," + Def.DbTrTb + n.Id + " " +
            "WHERE "
                + Def.DbBsTb + "." + Def.DbTableIdName + " = " +
                Def.DbTrTb + n.Id + "." + Def.DbTableIdName + " AND " +
                Def.DbBsTb + "." + p.Variable.Name + " IS NOT NULL " +
            "GROUP BY " +
                Def.DbBsTb + "." + Def.Schema.Target.Name + ", " +
                Def.DbBsTb + "." + p.Variable.Name + " ";
            nttLst = Def.Db.GetNTTLst(sql);
       
            lComb.Clear(); rComb.Clear();
            lPredVal.Clear(); rPredVal.Clear();
            lComb.Add(p.ValueSd.Keys[0]);  // Adds the 1st value to the combination of the left node
            rComb.Add(p.ValueSd.Keys[1]);  // Adds the 2nd value to the combination of the right node
            //Done only if the number of values is 2
            if (valueCount == 2) {
                imp = fillPredVals(lPredVal, rPredVal, lComb, rComb, nttLst, n);
                p.ChildrenGroups.ValueGroupLst[0].Clear();
                p.ChildrenGroups.ValueGroupLst[1].Clear();
                if (Double.IsNaN(imp)) {
                    p.SplitStatus = Predictor.SplitStatusEnum.NotEnoughCases;
                    p.ImpUniMin = Double.NaN;
                    p.Gain = Double.NaN;
                    p.ChildrenGroups.ValueGroupLst[0].Clear();
                    p.ChildrenGroups.ValueGroupLst[1].Clear();
                    return 0;
                }
                foreach(string s in lComb){
                    p.ChildrenGroups.ValueGroupLst[0].Add(s);
                }
                foreach(string s in rComb){
                    p.ChildrenGroups.ValueGroupLst[1].Add(s);
                }
                n.ImpBestUniSplit = imp;
                p.Gain = (n.Imp - imp) * 100 / n.Imp;
                p.Gain *= (double)(n.Table.RowCount - p.NullCount) / n.Table.RowCount;
                return p.Gain;
            }
            //IF valueCount > 2
            for (i = 2; i < valueCount; ++i) {
                //try to adding to the left
                lComb.Add(p.ValueSd.Keys[i]);
                impIfValueGoesLeft = fillPredVals(lPredVal, rPredVal, lComb, rComb, nttLst, n);
                //Changes the side
                rComb.Add(p.ValueSd.Keys[i]);
                lComb.RemoveAt(lComb.Count - 1);
                impIfValueGoesRight = fillPredVals(lPredVal, rPredVal, lComb, rComb, nttLst, n);
                if (!Double.IsNaN(impIfValueGoesLeft) && !Double.IsNaN(impIfValueGoesRight)) {
                    if (impIfValueGoesLeft < impIfValueGoesRight) {
                        imp = impIfValueGoesLeft;
                        lComb.Add(p.ValueSd.Keys[i]);
                        rComb.RemoveAt(rComb.Count - 1);
                    } else {
                        imp = impIfValueGoesRight;
                    }
                } else {
                    if (Double.IsNaN(impIfValueGoesLeft)) {
                        imp = impIfValueGoesRight;
                    }
                    if (Double.IsNaN(impIfValueGoesRight)) {
                        imp = impIfValueGoesLeft;
                        lComb.Add(p.ValueSd.Keys[i]);
                        rComb.RemoveAt(rComb.Count - 1);
                    }
                }
            }  
                
                impBest = imp;
                if (Def.ClfOptimisationLevelForCatSearch >= 1) {

                    #region enhanced progressive phase 1


                    // 0 = no improvement
                    // 1 = 1
                    // 2 = 2
                    // 3 = 3

                    //if (valueCount > 2) {

                    // Final combinations for the two 1st values 
                    // 1- Removes 1st val from left and put in the right
                    // 2- Removes 2nd val from right and send to left
                    // 3- Puts back the 1st val to left

                    // 0) Initial status 
                    // left  0xxxxxx
                    // right 1xxxxxx

                    // 1) 
                    // left  xxxxxx
                    // right 1xxxxxx0

                    // 2)  
                    // left  xxxxxx1
                    // right xxxxxx0

                    // 3)  
                    // left  xxxxxx10
                    // right xxxxxx

                    // 1  
                    lComb.RemoveAt(0);
                    rComb.Add(p.ValueSd.Keys[0]);
                    imp = fillPredVals(lPredVal, rPredVal, lComb, rComb, nttLst, n);
                    if (imp < impBest) {
                        improvementCode = 1;
                        impBest = imp;
                    }

                    // 2  
                    rComb.RemoveAt(0);
                    lComb.Add(p.ValueSd.Keys[1]);
                    imp = fillPredVals(lPredVal, rPredVal, lComb, rComb, nttLst, n);
                    if (imp < impBest) {
                        improvementCode = 2;
                        impBest = imp;
                    }

                    // 3  
                    rComb.RemoveAt(rComb.Count - 1);
                    lComb.Add(p.ValueSd.Keys[0]);
                    imp = fillPredVals(lPredVal, rPredVal, lComb, rComb, nttLst, n);
                    if (imp < impBest) {
                        improvementCode = 3;
                        impBest = imp;
                    }

                    if (improvementCode == 0) {
                        rComb.Add(lComb[lComb.Count - 2]);
                        lComb.RemoveAt(lComb.Count - 2);
                    }
                    if (improvementCode == 1) {
                        lComb.RemoveAt(lComb.Count - 1);
                        lComb.RemoveAt(lComb.Count - 1);
                        rComb.Add(p.ValueSd.Keys[0]);
                        rComb.Add(p.ValueSd.Keys[1]);
                    }
                    if (improvementCode == 2) {
                        lComb.RemoveAt(lComb.Count - 1);
                        rComb.RemoveAt(rComb.Count - 1);
                        lComb.Add(p.ValueSd.Keys[1]);
                        rComb.Add(p.ValueSd.Keys[0]);                        
                    }
                    
                    #endregion  enhanced progressive phase 1

                    #region enhanced progressive phase 2

                    if (Def.ClfOptimisationLevelForCatSearch >= 2) {
                        impBeforePhase2 = imp;
                        int lCombCount = lComb.Count;
                        for (int lidx = 0; lidx < lCombCount; ++lidx) {
                            rComb.Add(lComb[0]);
                            lComb.RemoveAt(0);

                            imp = fillPredVals(lPredVal, rPredVal, lComb, rComb, nttLst, n);
                            if (imp < impBest) {
                                impBest = imp;
                            } else {
                                lComb.Add(rComb[rComb.Count - 1]);
                                rComb.RemoveAt(rComb.Count - 1);
                            }
                        }

                        int rCombCount = rComb.Count;
                        for (int ridx = 0; ridx < rCombCount; ++ridx) {
                            lComb.Add(rComb[0]);
                            rComb.RemoveAt(0);

                            imp = fillPredVals(lPredVal, rPredVal, lComb, rComb, nttLst, n);
                            if (imp < impBest) {
                                impBest = imp;
                            } else {
                                rComb.Add(lComb[lComb.Count - 1]);
                                lComb.RemoveAt(lComb.Count - 1);
                            }
                        }
                    }//If optimisation >=2
                    #endregion phase 2
                }//If optimisation >=1

            if (Double.IsNaN(impBest)) {
                p.SplitStatus = Predictor.SplitStatusEnum.NotEnoughCases;
                p.ImpUniMin = Double.NaN;
                p.Gain = Double.NaN;
                p.ChildrenGroups.ValueGroupLst[0].Clear();
                p.ChildrenGroups.ValueGroupLst[1].Clear();
                return 0;
            }

            p.ChildrenGroups.ValueGroupLst[0].Clear();
            p.ChildrenGroups.ValueGroupLst[1].Clear();
            foreach(string s in lComb){
                p.ChildrenGroups.ValueGroupLst[0].Add(s);
            }
            foreach(string s in rComb){
                p.ChildrenGroups.ValueGroupLst[1].Add(s);
            }
            imp = impBest;
            p.ImpUniMin = imp;
            p.Gain = (n.Imp - imp) * 100 / n.Imp;
            p.Gain *= (double)(n.Table.RowCount - p.NullCount) / n.Table.RowCount;
            return p.Gain;
        }

        private static double fillPredVals(SortedList<string, int> lPredVal, SortedList<string, int> rPredVal, List<string> lComb, List<string> rComb, List<NTT> nttLst, NodeTargetCategorical n) {

            int nodeLfItemCount, nodeRtItemCount;
            int instanceI;
            double imp = Double.NaN, lImp = Double.NaN, rImp = Double.NaN;
           
            nodeLfItemCount = nodeRtItemCount = 0;
            lPredVal.Clear(); rPredVal.Clear();
            for (instanceI = 0; instanceI < nttLst.Count; ++instanceI) {
                foreach (string ls in lComb) {
                    if (nttLst[instanceI].T0 == ls) {
                        if (!lPredVal.ContainsKey(nttLst[instanceI].T1))
                            lPredVal.Add(nttLst[instanceI].T1, (int)nttLst[instanceI].N);
                        else
                            lPredVal[nttLst[instanceI].T1] += (int)nttLst[instanceI].N;
                        nodeLfItemCount += (int)nttLst[instanceI].N;
                        break;
                    }
                }
                foreach (string rs in rComb) {
                    if (nttLst[instanceI].T0 == rs) {
                        if (!rPredVal.ContainsKey(nttLst[instanceI].T1))
                            rPredVal.Add(nttLst[instanceI].T1, (int)nttLst[instanceI].N);
                        else
                            rPredVal[nttLst[instanceI].T1] += (int)nttLst[instanceI].N;
                        nodeRtItemCount += (int)nttLst[instanceI].N;
                        break;
                    }
                }
            }
            if (nodeLfItemCount >= Def.TreeMinNumberOfCasesPerNode && nodeRtItemCount >= Def.TreeMinNumberOfCasesPerNode) {
                lImp = ImpCat(lPredVal, nodeLfItemCount);
                rImp = ImpCat(rPredVal, nodeRtItemCount);
                imp = (double)(nodeLfItemCount) / n.Table.RowCount * lImp + (double)nodeRtItemCount / n.Table.RowCount * rImp;
            }
            return imp;
        }
    }
}
