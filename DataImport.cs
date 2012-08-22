using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms; // Only because of the progress bar
using System.Data.Common;
using System.Text.RegularExpressions;

namespace Spartacus {
    public class DataImport {

        public string FilenameOriginal;
        public string FilenameNormalised;
        public int RegistryCount;
        public string TableName;
        public bool Multivariate = false;
        public int DependentVariableIdx = -1;

        public List<DataImportVariable> VariableLst = null; // Has variable that are imported or not
        public List<DataImportVariable> VariableMvLst = null;//Has only the imported variable

        public DataImport() {
        }

        public string Filename {
            set {
                FilenameOriginal = value;
                FilenameNormalised = value;
                FilenameNormalised = FilenameNormalised.Replace("-", "_");
                FilenameNormalised = FilenameNormalised.Replace("'", "_");
                FilenameNormalised = FilenameNormalised.Replace("`", "_");
                FilenameNormalised = FilenameNormalised.Replace("&", "_");
                FilenameNormalised = FilenameNormalised.Replace("%", "perc");
                FilenameNormalised = FilenameNormalised.Replace(" ", "_");
            }
        }

        public bool Autodetect(){

            string path = FilenameOriginal;
            string pattern = ",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))";
            string doubleQuotes = "\"";
            Regex r = new Regex(pattern);
            int x, lineCounter = 0;
            string[] strArr;
            string fieldValue;
            string line;
            double fieldValueDouble;          

            using (StreamReader sr = new StreamReader(path)) {

                while (sr.Peek() >= 0) {
                    ++lineCounter;
                    line = sr.ReadLine();
                    strArr = r.Split(line);
                    if (lineCounter == 1) {
                        VariableLst = new List<DataImportVariable>(strArr.Length);
                        for (x = 0; x < strArr.Length; ++x) {
                            VariableLst.Add(new DataImportVariable());
                        }
                        for (x = 0; x < strArr.Length; ++x) {
                            fieldValue = strArr[x].Replace(doubleQuotes, "");
                            fieldValue = fieldValue.Replace(" ", "_");
                            fieldValue = fieldValue.Replace("'", "");
                            fieldValue = fieldValue.Replace("?", "");
                            fieldValue = fieldValue.Replace("-", "_");
                            fieldValue = fieldValue.Replace("%", "perc");
                            if (fieldValue == "") {
                                MessageBox.Show("Corrupt CSV file", "Warning");
                                return false;
                            }
                            VariableLst[x].Name = fieldValue.ToLowerInvariant();
                        }
                        continue;
                    }
                    for (x = 0; x < strArr.Length; ++x) {
                        fieldValue = strArr[x].Replace(doubleQuotes, "").ToLowerInvariant();
                        if (fieldValue == "")
                            ++VariableLst[x].Blanks;
                        else{
                            if (VariableLst[x].DataType == Database.DataTypeEnum.Number) {
                                if (!Double.TryParse(fieldValue, out fieldValueDouble)) {
                                    VariableLst[x].DataType = Database.DataTypeEnum.String;
                                    VariableLst[x].SumD = double.NaN;
                                    if (VariableLst[x].ValuesSet == true) {
                                        VariableLst[x].minimumS = VariableLst[x].MinimumD.ToString();
                                        VariableLst[x].maximumS = VariableLst[x].MaximumD.ToString();
                                    }
                                } else {
                                    VariableLst[x].SumD += fieldValueDouble;
                                    if (VariableLst[x].ValuesSet == false) {
                                        VariableLst[x].MinimumD = fieldValueDouble;
                                        VariableLst[x].MaximumD = fieldValueDouble;
                                        VariableLst[x].ValuesSet = true;
                                    } else {
                                        if (fieldValueDouble < VariableLst[x].MinimumD)
                                            VariableLst[x].MinimumD = fieldValueDouble;
                                        else {
                                            if (fieldValueDouble > VariableLst[x].MaximumD)
                                                VariableLst[x].MaximumD = fieldValueDouble;
                                        }
                                    }
                                }
                            }
                            if (VariableLst[x].DataType == Database.DataTypeEnum.String) {
   
                                if(Multivariate){
                                    if(!VariableLst[x].ValueGroupLst.ContainsKey(fieldValue))
                                        VariableLst[x].ValueGroupLst.Add(fieldValue, 0);
                                    else
                                        ++VariableLst[x].ValueGroupLst[fieldValue];
                                }                                
                                if (VariableLst[x].ValuesSet == false) {
                                    VariableLst[x].minimumS = fieldValue;
                                    VariableLst[x].maximumS = fieldValue;
                                    VariableLst[x].ValuesSet = true;
                                } else {
                                    if (fieldValue.CompareTo(VariableLst[x].minimumS) < 0)
                                        VariableLst[x].minimumS = fieldValue;
                                    else {
                                        if (fieldValue.CompareTo(VariableLst[x].maximumS) > 0)
                                            VariableLst[x].maximumS = fieldValue;
                                    }
                                }
                            }
                        }

                    }
                }
            }
            for (x = 0; x < VariableLst.Count; ++x) {
                if (VariableLst[x].DataType == Database.DataTypeEnum.Number) {
                    if (lineCounter > 1)
                        VariableLst[x].MeanD = VariableLst[x].SumD / lineCounter - 1; 
                }
            }
            RegistryCount = lineCounter - 1;
            //if (Multivariate) {
            //    for (x = 0; x < Def.DataImportDefs.VariableLst.Count; ++x) {
            //        if (Def.DataImportDefs.VariableLst[x].Import == false || Def.DataImportDefs.VariableLst[x].DataType == Database.DataTypeEnum.Number)
            //            continue;
            //        Def.DataImportDefs.VariableLst[x].ValueGroupLst.Count = Def.DataImportDefs.VariableLst[x].ValueGroupLst.Count;
            //    }

            //}

            return true;
        }
    }

    public class DataImportVariable {

        public double MeanD, MaximumD, MinimumD, SumD;
        public string maximumS, minimumS;
        public string Name;
        public Database.DataTypeEnum DataType;
        public bool ValuesSet;
        public bool Import;
        public int Blanks;
//        public int valuesCount; //Number of distinct values
        public SortedList<string, int> ValueGroupLst = new SortedList<string, int>(); //Value and frequency, filled only if the dataset is multidimensional
        
        public DataImportVariable() {
            ValuesSet = false;
            Import = true;
            Name = "No_Name";
            MeanD = Double.NaN; MaximumD = Double.NaN; MinimumD = Double.NaN; Blanks = 0; SumD = 0;
            maximumS = "__NOT_SET"; minimumS = "__NOT_SET";
            DataType = Database.DataTypeEnum.Number;
        }

        public string Minimum {
            get{
                if (DataType == Database.DataTypeEnum.Number)
                    return Convert.ToString(Math.Round(MinimumD,4));
                else
                    return minimumS;
            }
        }


        public string Maximum {
            get {
                if (DataType == Database.DataTypeEnum.Number)
                    return Convert.ToString(Math.Round(MaximumD,4));
                else
                    return maximumS;
            }
        }

        public string Mean {
            get {
                if (DataType == Database.DataTypeEnum.Number)
                    return Convert.ToString(Math.Round(MeanD,4));
                else
                    return "  -  ";
            }
        }

       
    }

}
