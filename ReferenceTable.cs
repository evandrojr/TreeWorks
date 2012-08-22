//#region Using directives

//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Windows.Forms;

//#endregion

//namespace Spartacus {
//    public class ReferenceTable {

//        public List<int> Row;
//        private VariableCct orderedAscBy = null; //Column that sorts the table Asc
//        private VariableCct orderedDescBy = null;//Column that sorts the table Desc

//        public ReferenceTable(TableCct _source) {
//            int i;
//            int rc = _source.InstanceCount;

//            source = _source;
//            Row = new List<int>(rc);
//            for(i=0; i < rc; ++i)
//                Row.Add(i);
//        }


//        //outdated must take a random sample
//        public ReferenceTable(TableCct _source, int LearningSamplePercSize)
//        {
//            int i;
//            int rc = (int) (_source.InstanceCount * LearningSamplePercSize) / 100;

//            source = _source;
//            Row = new List<int>(rc);
//            for (i = 0; i < rc; ++i)
//                Row.Add(i);
//        }

//        public ReferenceTable(TableCct _source, bool blank) {
//            source = _source;
//            Row = new List<int>();
//        }


//        public ReferenceTable(List<int> subRows) {
			
//            Row = subRows;
//        }

//        public int this[int y] //Returns the source line
//        {
//            get	{ return Row[y]; }
//            set { Row[y] = value;} 
//        }

//        public int VariableCount{get{return source.VariableCount;}}
//        public int InstanceCount{get{return Row.Count;}}

//        public string GetText(int x, int y) {
//            if (x < 0 || x > VariableCount - 1 || y < 0 || y > InstanceCount - 1) {
//                throw new IndexOutOfRangeException("Index out of bounds in GetText x=" + x + " y=" + y + " ");
//            }
//            return source.GetText(x, Row[y]);
//        }

//        public string GetText(VariableCct var, int y){
//            if (y < 0 || y > InstanceCount - 1)
//                throw new IndexOutOfRangeException("Index out of bounds in GetText y=" + y + " ");
//            return var.Instance.GetText(Row[y]);
//        }

//        public double GetNumber(int x, int y) {
//            if (x < 0 || x > VariableCount - 1 || y < 0 || y > InstanceCount - 1) {
//                throw new IndexOutOfRangeException("Index out of bounds in GetText y=" + y + " "); 
//            }
//            return source.GetNumber(x, Row[y]);
//        }

//        public double GetNumber(VariableCct var, int y) {
//            if (y < 0 || y > InstanceCount - 1)
//                throw new IndexOutOfRangeException("Index out of bounds in GetNumber y=" + y + " ");
//            return var.Instance.GetNumber(Row[y]);
//        }

//        //Using the sorted dictionary will be faster
//        //outdated
//        // <=
//        public ReferenceTable SubsetUpTo(VariableCct col, double threshold) {
//            List<int> rowsSelected = new List<int>();
//            for (int y = 0; y < InstanceCount; ++y) {
//                if (col.Instance.GetNumber(Row[y]) <= threshold)
//                        rowsSelected.Add(Row[y]);
//            }
//            ReferenceTable subRT = new ReferenceTable(rowsSelected);
//            return subRT;
//        }

//        //Using the sorted dictionary will be faster
//        //outdated
//        // <=
//        public ReferenceTable SubsetUpTo(VariableCct col, string threshold) {
//            threshold = threshold.ToLowerInvariant();
//            List<int> rowsSelected = new List<int>();
//            for (int y = 0; y < InstanceCount; ++y) {
//                if (col.Instance.GetText(Row[y]).CompareTo(threshold) <= 0)
//                    rowsSelected.Add(Row[y]);
//            }
//            ReferenceTable subRT = new ReferenceTable(rowsSelected);
//            return subRT;
//        }


//        //Using the sorted dictionary will be faster
//        //outdated
//        // <=
//        public ReferenceTable SubsetLessThan(VariableCct col, double threshold) {
//            List<int> rowsSelected = new List<int>();
//            for (int y = 0; y < InstanceCount; ++y) {
//                if (col.Instance.GetNumber(Row[y]) < threshold)
//                    rowsSelected.Add(Row[y]);
//            }
//            ReferenceTable subRT = new ReferenceTable(rowsSelected);
//            return subRT;
//        }

//        //Using the sorted dictionary will be faster
//        //outdated
//        // <=
//        public ReferenceTable SubsetLessThan(VariableCct col, string threshold) {
//            threshold = threshold.ToLowerInvariant();
//            List<int> rowsSelected = new List<int>();
//            for (int y = 0; y < InstanceCount; ++y) {
//                if (col.Instance.GetText(Row[y]).CompareTo(threshold) < 0)
//                    rowsSelected.Add(Row[y]);
//            }
//            ReferenceTable subRT = new ReferenceTable(rowsSelected);
//            return subRT;
//        }


//        // =
//        public ReferenceTable SubsetEquals(VariableCct col, double val) {
//            List<int> rowsSelected = new List<int>();


//            for (int y = 0; y < InstanceCount; ++y) {
//                if (col.Instance.GetNumber(Row[y]) == val)
//                    rowsSelected.Add(Row[y]);
//            }
//            ReferenceTable subRT = new ReferenceTable(rowsSelected);
//            return subRT;
//        }

//        public ReferenceTable SubsetEquals(VariableCct col, string val) {

//            val = val.ToLowerInvariant();
//            List<int> rowsSelected = new List<int>();
//            for (int y = 0; y < InstanceCount; ++y) {
//                if (col.Instance.GetText(Row[y]).CompareTo(val) == 0)
//                    rowsSelected.Add(Row[y]);
//            }
//            ReferenceTable subRT = new ReferenceTable(rowsSelected);
//            return subRT;
//        }


//        // <>
//        public ReferenceTable SubsetDifferent(VariableCct col, double val) {
//            List<int> rowsSelected = new List<int>();
//            for (int y = 0; y < InstanceCount; ++y) {
//                if (col.Instance.GetNumber(Row[y]) != val)
//                    rowsSelected.Add(Row[y]);
//            }
//            ReferenceTable subRT = new ReferenceTable(rowsSelected);
//            return subRT;
//        }

//        // <>
//        public ReferenceTable SubsetDifferent(VariableCct col, string val) {

//            val = val.ToLowerInvariant();
//            List<int> rowsSelected = new List<int>();
//            for (int y = 0; y < InstanceCount; ++y) {
//                if (col.Instance.GetText(Row[y]).CompareTo(val) != 0)
//                    rowsSelected.Add(Row[y]);
//            }
//            ReferenceTable subRT = new ReferenceTable(rowsSelected);
//            return subRT;
//        }


//        //Using the sorted dictionary will be faster
//        //outdated
//        // >
//        public ReferenceTable SubsetHigherThan(VariableCct col, double threshold) {
//            List<int> rowsSelected = new List<int>();
//            for (int y = 0; y < InstanceCount; ++y) {
//                if (col.Instance.GetNumber(Row[y]) > threshold)
//                    rowsSelected.Add(Row[y]);
//            }
//            ReferenceTable subRT = new ReferenceTable(rowsSelected);
//            return subRT;
//        }

//        //Using the sorted dictionary will be faster
//        //outdated
//        // >
//        public ReferenceTable SubsetHigherThan(VariableCct col, string threshold) {
//            threshold = threshold.ToLowerInvariant();
//            List<int> rowsSelected = new List<int>();
//            for (int y = 0; y < InstanceCount; ++y) {
//                if ( col.Instance.GetText(Row[y]).CompareTo(threshold) > 0)
//                    rowsSelected.Add(Row[y]);
//            }
//            ReferenceTable subRT = new ReferenceTable(rowsSelected);
//            return subRT;
//        }



//        //Using the sorted dictionary will be faster
//        //outdated
//        // >=
//        public ReferenceTable SubsetAtLeast(VariableCct col, double threshold) {
//            List<int> rowsSelected = new List<int>();
//            for (int y = 0; y < InstanceCount; ++y) {
//                if (col.Instance.GetNumber(Row[y]) >= threshold)
//                    rowsSelected.Add(Row[y]);
//            }
//            ReferenceTable subRT = new ReferenceTable(rowsSelected);
//            return subRT;
//        }

//        //Using the sorted dictionary will be faster
//        //outdated
//        // >=
//        public ReferenceTable SubsetAtLeast(VariableCct col, string threshold) {
//            threshold = threshold.ToLowerInvariant();
//            List<int> rowsSelected = new List<int>();
//            for (int y = 0; y < InstanceCount; ++y) {
//                if (col.Instance.GetText(Row[y]).CompareTo(threshold) >= 0)
//                    rowsSelected.Add(Row[y]);
//            }
//            ReferenceTable subRT = new ReferenceTable(rowsSelected);
//            return subRT;
//        }


//        public ReferenceTable SubsetCategoricalCases(Predictor p, ref List<int> caseLst) {
////            List<int> rowsSelected = new List<int>();
////            SchemaVariable col = p.Variable;    
////        
////            if(p.DataType == SchemaVariable.DataTypeEnum.Number)
////                for (int y = 0; y < InstanceCount; ++y) {
////                    foreach (int idx in caseLst) {
////                        if (col.Instance.GetNumber(Row[y]) == p.ValueSd.Keys[idx])
////                            rowsSelected.Add(Row[y]);
////                    }
////                }
////            else
////                for (int y = 0; y < InstanceCount; ++y) {
////                    foreach (int idx in caseLst) {
////                        if (col.Instance.GetText(Row[y]) == p.CaseTextSd.Keys[idx])
////                            rowsSelected.Add(Row[y]);
////                    }
////                }
////
////            ReferenceTable subRT = new ReferenceTable(rowsSelected);
////            return subRT;
//            return null;
//        }

//        public void OrderAscBy(VariableCct v) {
//            if (orderedAscBy == v) //It is already ordered, return
//                return;
//            orderedAscBy = v;
//            orderedDescBy = null;
//            sort(this, v);
//        }


//        public static void sort(ReferenceTable a, VariableCct col) {
            
//            if(col.DataType == VariableCct.DataTypeEnum.Number)
//                QuickSortNumber(a, 0, a.InstanceCount - 1, col);
//            else
//                QuickSortText(a, 0, a.InstanceCount - 1, col);

//        }

//        public static void QuickSortNumber(ReferenceTable a, int lo0, int hi0, VariableCct col) {
//            int lo = lo0;
//            int hi = hi0;
//            double mid;

//            if (hi0 > lo0) {
//                /* Arbitrarily establishing partition element as the midpoint of
//                * the array.
//                */
                
//                mid = a.GetNumber(col, (int)(lo0 + hi0) / 2);

//                // loop through the array until indices cross
//                while (lo <= hi) {
//                    /* find the first element that is greater than or equal to 
//                    * the partition element starting from the left Index.
//                    */
//                    while ((lo < hi0) && (a.GetNumber(col, lo) < mid))
//                        ++lo;

//                    /* find an element that is smaller than or equal to 
//                    * the partition element starting from the right Index.
//                    */
//                    while ((hi > lo0) && (a.GetNumber(col, hi) > mid))
//                        --hi;

//                    // if the indexes have not crossed, swap
//                    if (lo <= hi) {
//                        swap(a, lo, hi);

//                        ++lo;
//                        --hi;
//                    }
//                }

//                /* If the right index has not reached the left side of array
//                * must now sort the left partition.
//                */
//                if (lo0 < hi)
//                    QuickSortNumber(a, lo0, hi, col);

//                /* If the left index has not reached the right side of array
//                * must now sort the right partition.
//                */
//                if (lo < hi0)
//                    QuickSortNumber(a, lo, hi0, col);
//            }
//        }

//        public static void swap(ReferenceTable a, int i, int j) {
//            int T;

//            if (i == -1)
//                throw new IndexOutOfRangeException("Index = -1");

//            T = a[i];
//            a[i] = a[j];
//            a[j] = T;
//        }

//        public static void QuickSortText(ReferenceTable a, int lo0, int hi0, VariableCct col) {
//            int lo = lo0;
//            int hi = hi0;
//            string mid;

//            if (hi0 > lo0) {
//                /* Arbitrarily establishing partition element as the midpoint of
//                * the array.
//                */

//                mid = a.GetText(col, (int)(lo0 + hi0) / 2);

//                // loop through the array until indices cross
//                while (lo <= hi) {
//                    /* find the first element that is greater than or equal to 
//                    * the partition element starting from the left Index.
//                    */
//                    while ((lo < hi0) && (String.Compare(a.GetText(col, lo), mid) < 0 ))
//                        ++lo;

//                    /* find an element that is smaller than or equal to 
//                    * the partition element starting from the right Index.
//                    */
//                    while ((hi > lo0) && (String.Compare(a.GetText(col, hi), mid) > 0))
//                        --hi;

//                    // if the indexes have not crossed, swap
//                    if (lo <= hi) {
//                        swap(a, lo, hi);

//                        ++lo;
//                        --hi;
//                    }
//                }
//                /* If the right index has not reached the left side of array
//                * must now sort the left partition.
//                */
//                if (lo0 < hi)
//                    QuickSortText(a, lo0, hi, col);

//                /* If the left index has not reached the right side of array
//                * must now sort the right partition.
//                */
//                if (lo < hi0)
//                    QuickSortText(a, lo, hi0, col);
//            }
//        }



//    }
//}
