#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

#endregion

namespace Spartacus
{
	/// <summary>
	/// Summary description for Fcn.
	/// </summary>
	public class Fcn
	{
        public static bool Test(object o) {
            bool r=false;

            try
            {
                if (o != null)
                {
                    if (o is bool) {
                        if ((bool)o == true)
                            r = true;
                    } else
                        if (o is int) {
                            if ((int)o == 1)
                                r = true;
                        }
                        else
                        if ((string)o == "true")
                            r = true;
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Fcn.cs Test");
            }
            return r;
        }

        public static string FileName(string pathAndFileName)
		{
			int idx=pathAndFileName.IndexOf(@"\");
			while(idx!=-1)
			{
				pathAndFileName=pathAndFileName.Substring(idx+1, pathAndFileName.Length - (idx+1) );
				idx=pathAndFileName.IndexOf(@"\");	
			}
			return pathAndFileName;
		}

		public static string FileBaseName(string FileName)
		{
			int idx=FileName.IndexOf(@".");
			return FileName.Substring(0, idx);
		}

		public static string FilePath(string pathAndFileName)
		{
			string file = FileName(pathAndFileName);
			return pathAndFileName.Remove(pathAndFileName.Length - file.Length -1, file.Length + 1);
		}

		public static Array Resize(Array array, int newSize)
		{
			Type type = array.GetType();
			Array newArray = Array.CreateInstance(type.GetElementType(), newSize);
			Array.Copy(array, 0, newArray, 0, Math.Min(newArray.Length, newSize));
			return newArray;
		}

        public static string LineFill(string toBeFilled, string filler, int sizeFinal)
		{
			int toBeFilledLength = toBeFilled.Length;
//			filler = " ";
			if(sizeFinal < toBeFilledLength)
				return "Final size is too small";
			for(int i=0; i < sizeFinal - toBeFilledLength; ++i)
				toBeFilled+=filler;
			return toBeFilled;
		}

		    // QuickSort  implementation for strings
			//QuickSort (szContents, 0, szContents.GetLength(0) - 1);
			static void QuickSort (string[] szArray, int nLower, int nUpper)
			{
				// Check for non-base case
				if (nLower < nUpper)
				{
					// Split and sort partitions
					int nSplit = Partition (szArray, nLower, nUpper);
					QuickSort (szArray, nLower, nSplit - 1);
					QuickSort (szArray, nSplit + 1, nUpper);
				}
			}
			// QuickSort partition implementation
			static int Partition (string[] szArray, int nLower, int nUpper)
			{
				// Pivot with first element
				int nLeft = nLower + 1;
				string szPivot = (string) szArray[nLower];
				int nRight = nUpper;
				// Partition array elements
				string szSwap;
				while (nLeft <= nRight)
				{
					// Find item out of place
					while (nLeft <= nRight)
					{
						if (((string) szArray[nLeft]).CompareTo (szPivot) > 0)
							break;
						nLeft = nLeft + 1;
					}
					while (nLeft <= nRight)
					{
						if (((string) szArray[nRight]).CompareTo (szPivot) <= 0)
							break;
						nRight = nRight - 1;
					}
					// Swap values if necessary
					if (nLeft < nRight)
					{
						szSwap = (string) szArray[nLeft];
						szArray[nLeft] = szArray[nRight];
						szArray[nRight] = szSwap;
						nLeft = nLeft + 1;
						nRight = nRight - 1;
					}
				}
				// Move pivot element
				szSwap = (string) szArray[nLower];
				szArray[nLower] = szArray[nRight];
				szArray[nRight] = szSwap;
				return nRight;
			}

// QuickSort  implementation for Doubles
		//QuickSort (szContents, 0, szContents.GetLength(0) - 1);
		static void QuickSort (double[] szArray, int nLower, int nUpper)
		{
			// Check for non-base case
			if (nLower < nUpper)
			{
				// Split and sort partitions
				int nSplit = Partition (szArray, nLower, nUpper);
				QuickSort (szArray, nLower, nSplit - 1);
				QuickSort (szArray, nSplit + 1, nUpper);
			}
		}
		// QuickSort partition implementation
		static int Partition (double[] szArray, int nLower, int nUpper)
		{
			// Pivot with first element
			int nLeft = nLower + 1;
			double szPivot = szArray[nLower];
			int nRight = nUpper;
			// Partition array elements
			double szSwap;
			while (nLeft <= nRight)
			{
				// Find item out of place
				while (nLeft <= nRight)
				{
					if (szArray[nLeft] > szPivot)
						break;
					nLeft = nLeft + 1;
				}
				while (nLeft <= nRight)
				{
					if (szArray[nRight] <= szPivot)
						break;
					nRight = nRight - 1;
				}
				// Swap values if necessary
				if (nLeft < nRight)
				{
					szSwap = szArray[nLeft];
					szArray[nLeft] = szArray[nRight];
					szArray[nRight] = szSwap;
					nLeft = nLeft + 1;
					nRight = nRight - 1;
				}
			}
			// Move pivot element
			szSwap = szArray[nLower];
			szArray[nLower] = szArray[nRight];
			szArray[nRight] = szSwap;
			return nRight;
		}



        public static double Mean(List<double> itLst) {
            double tot = 0;
            int i, count = itLst.Count;

            for (i = 0; i < count ; ++i) {
                tot += itLst[i];
            }
            return tot / count;
        }


        public static double Variance(List<NN> nnLst) {

            int i, n=0;
            double sumY, sumY2;
            sumY = sumY2 = 0;

            if (nnLst.Count <= 1)
                return 0;

            for (i = 0; i < nnLst.Count; ++i) {
                sumY+= nnLst[i].N0 * nnLst[i].N1;
                sumY2+= nnLst[i].N0 * nnLst[i].N0 * nnLst[i].N1;
                n += (int) nnLst[i].N1;
            }
            if (n <= 1)
                return 0;
            return ((sumY2 * n) - (sumY * sumY)) / (n * (n - 1));
        }

        public static double VarianceTimesCount(List<string> vals, Node node, Predictor pred) {

            string sql = "", valSql="";
            List<double> NLst = null;

            for (int i = 0; i < vals.Count; ++i) {
                valSql += pred.Variable.Name + "='" + vals[i] + "' ";
                if (i < (vals.Count - 1)) {
                    valSql += " or ";
                } else
                    valSql += ")";
            }

            sql =
            @"SELECT " +
                "COALESCE(variance(" + Def.Tree.Schema.Target.Name + "), 0), count(*) " +
            "FROM "
               + Def.DbBsTb + " , " + Def.DbTrTb + node.Id + " " +
            "WHERE ("
                + Def.DbBsTb + "." + Def.DbTableIdName + "=" +
                Def.DbTrTb + node.Id + "." + Def.DbTableIdName + ") and (" +
                valSql;
            NLst = Def.Db.GetNumberRowLst(sql);
            return NLst[0] * NLst[1];
        }
        

        public static string Decimal2BinaryStr(double x){

            string ret = "";
            double x2=x;
            double i, power;
            double l2, log2=0;
            while(x2>=2){
	            x2=x2/2;
	            log2=log2+1;
            }

            char[] answer = new char[(int)log2 + 1];

            for(l2=log2; l2>=0; l2--){
	            power=Math.Pow(2,l2);
	            if (x>=power) {
		            answer[(int) l2]='1';
		            x=x-power;
	            }		
	            else answer[(int) l2]='0';
            }
            for (i=log2; i>=0; i--){
	            ret+=answer[(int) i];	

            }
            return ret.PadLeft(Def.MaxDifferentCatValues, '0');
         }

        /// <summary>
        /// Normalise plus 1
        /// </summary>
        /// <param name="val">Value to be normalises</param>
        /// <param name="min">smaller of the values</param>
        /// <param name="max">bigger value</param>
        /// <returns>Normalised number between 1 and 2</returns>
        public static double NormP1(double val, double min, double max){

            // There will be a Division by Zero if min=max


            //if (val == min)
            //    return 0.0001;
            //else
           //     return val / max;
            
            
           //  return val;
           if (max == min)
                return 0;
           return ((val - min) / (max - min) );
            //return val;
        }

        /// <summary>
        /// Inverse of normalise plus 1
        /// </summary>
        /// <param name="val">Value to be normalises</param>
        /// <param name="min">smaller of the values</param>
        /// <param name="max">bigger value</param>
        /// <returns>Normalised number between 1 and 2</returns>
        public static double InvNormP1(double val, double min, double max) {

            if (val == 0.00001)
                return min;
            else
                return ((max - min) * (val)) + min;
            
            //return val;
            //return max * val;
        }


        #region Common functions for decision tree

        public static List<int> SetPossibleThresholdIndexLst(List<N4> N4Lst) {
            List<int> thresholdIdxLst = new List<int>();
            int uniqueIdx, rc = N4Lst.Count;

            //Ends when there are not enough rows at the end
            for (uniqueIdx = Def.TreeMinNumberOfCasesPerNode - 1; uniqueIdx + Def.TreeMinNumberOfCasesPerNode < rc; ++uniqueIdx) {
                if (N4Lst[uniqueIdx].N1 != N4Lst[uniqueIdx + 1].N1)
                    thresholdIdxLst.Add(uniqueIdx);
            }
            return thresholdIdxLst;
        }

        public static List<int> SetPossibleThresholdIndexLst(List<N3T> N3TLst) {
            List<int> thresholdIdxLst = new List<int>();
            int uniqueIdx, rc = N3TLst.Count;

            //Ends when there are not enough rows at the end
            for (uniqueIdx = Def.TreeMinNumberOfCasesPerNode - 1; uniqueIdx + Def.TreeMinNumberOfCasesPerNode < rc; ++uniqueIdx) {
                if (N3TLst[uniqueIdx].N0 != N3TLst[uniqueIdx + 1].N0)
                    thresholdIdxLst.Add(uniqueIdx);
            }
            return thresholdIdxLst;
        }


        #endregion

    }
}
