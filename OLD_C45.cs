using System;
using System.Data;
using System.Collections;
using System.Windows;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Spartacus
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class C45
	{
		public C45()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public static void TreeBuild(Node nd)
		{
			Attribute dependentVariable = (Attribute) nd.AttributeL[Variable.DependentVariableIndex];
			int dependentVariableCaseLCount = dependentVariable.CaseL.Count;
//			int thresholdIndexActual = -1; int thresholdIndexFinal = -1;
			int i=0;
			double thresholdActual = -1; double thresholdFinal = -1;
			double info = Info(nd); double infoAttributeMin; double minInfo=0;
			bool minInfoSet = false; bool foundAnotherCase; bool splitValid;
			string str="";
			Attribute att;
			Attribute attributeSplit = new Attribute()	;
			VirtualTable dataSetChild;
			Variable testHasBeenSplit;
			Node ndChild;
			Case CaseSplit, cs;

			FrmMain.SFrmMain.StatusBar.Panels[0].Text = "Generating node " + nd.Id;

			nd.Impurity = Info(nd);

			if(nd.Level + 1 == Option.C45MaxNumOfLevels) 
				return;
			if(dependentVariableCaseLCount == 0)
			{
				nd.Text="Couldn't find any class!";
				return;
			}
			if(dependentVariableCaseLCount == 1 || nd.VT.RowCount <= Option.C45MinNumOfCasesPerNode )
			{
				int c = dependentVariableCaseLCount;
				int mostFrequentClassValue=0; int mostFrequentClassIndex=0;
				string mostFrequentClassName="Not set yet!";								
				for(i=0; i < c; ++i)
				{
					cs = (Case) dependentVariable.CaseL[i];
					if(cs.Frequency > mostFrequentClassValue)
					{
						mostFrequentClassIndex=i;
						mostFrequentClassName = cs.Val.ToString();
					}
				}
				nd.Text=mostFrequentClassName;
				return;
			}
			//Tests if all the dependent varibles have the same value
				
			foundAnotherCase = false;
			for(i=0; i < dependentVariable.CaseL.Count; ++i)
			{
				cs = (Case) dependentVariable.CaseL[i];
				if(i==0)
					str = cs.Val.ToString();
				else
					if(str != cs.Val.ToString())
					{
						foundAnotherCase=true;
						break;
					}
			}
			if(!foundAnotherCase)
				return;
			
			for(int attCnt=0; attCnt < nd.AttributeL.Count; ++attCnt)
			{
				att = (Attribute) nd.AttributeL[attCnt];
				
				//Does not do anything with the Dependent Variable
				if(attCnt == Variable.DependentVariableIndex)
				{
					continue;	
				}
				Variable var = (Variable) Tree.VariableL[attCnt];
				if(var.enabled == false)
					continue;
				if(att.Type == Variable.TypeEnum.Categorical )
				{
					//wrong that is a optmisation to execute if any father hasBeenUsed to split, no any node
//					testHasBeenSplit = (Variable) Tree.VariableL[attCnt]; 
//					if(testHasBeenSplit.HasBeenUsedToSplit==true)
//						continue;
					if(att.CaseL.Count > Option.C45MaxNumOfSplitsPerNode)
						continue;
					else
					{
						splitValid=true;
						infoAttributeMin = infoAttribute(nd, att, ref splitValid);
						if(!splitValid)
							continue;
						if(!minInfoSet)
						{
							minInfo = infoAttributeMin;
							attributeSplit=att;
							minInfoSet=true;
						}
						else
						if(infoAttributeMin < minInfo)
						{
							minInfo=infoAttributeMin;
							attributeSplit=att;
						}
					}
				}
				else
				if(att.Type == Variable.TypeEnum.Continuous)
				{
					//continue;
					splitValid=true;
					nd.VT.OrderAscBy(attCnt);
					infoAttributeMin = InfoContinuousAttribute(nd, att, ref thresholdActual, ref splitValid);
					if(!splitValid)
						continue;
					if(!minInfoSet)
					{
						minInfo = infoAttributeMin;
						attributeSplit=att;
						minInfoSet=true;
						thresholdFinal = thresholdActual;
					}
					else
					if(infoAttributeMin < minInfo)
					{
						minInfo=infoAttributeMin;
						attributeSplit=att;
						thresholdFinal = thresholdActual;
					}
				}
			}
			
			//This means that no valid split was found
			if(attributeSplit.VariableIndex==-1)
				return;
			
			nd.SplitVariableName=attributeSplit.Name;
			ArrayList childL;
			if(attributeSplit.Type == Variable.TypeEnum.Categorical)
			{
				childL = new ArrayList();
				for(int s=0; s < attributeSplit.CaseL.Count; ++s)
				{
					CaseSplit = (Case) attributeSplit.CaseL[s];
					dataSetChild = nd.VT.SubSet(attributeSplit.VariableIndex, CaseSplit.Val.ToString()); 
					ndChild = new Node(CaseSplit.Val.ToString(), dataSetChild);
					nd.AddChild(ndChild);
					childL.Add(ndChild);
				}
			}
			else
			{
				childL = new ArrayList(2);
				dataSetChild = nd.VT.SubSetUpTo(attributeSplit.VariableIndex, thresholdFinal); 
				ndChild = new Node("<= " + thresholdFinal, dataSetChild);
				nd.AddChild(ndChild);
				childL.Add(ndChild);

				dataSetChild = nd.VT.SubSetHigher(attributeSplit.VariableIndex, thresholdFinal); 
				ndChild = new Node("> " + thresholdFinal, dataSetChild);
				nd.AddChild(ndChild);
				childL.Add(ndChild);
			}
			foreach(Node nc in childL)
				TreeBuild(nc);

		}
		public static double Info(Node nd)
		{
			Attribute att = nd.DependentVariableAttribute;
			Case cs;
			int attCaseLCount = att.CaseL.Count;
			double info=0;
			for(int y=0; y < attCaseLCount; ++y)
			{
				cs = (Case) att.CaseL[y];
				info += ((double) cs.Frequency / (double) nd.VT.RowCount) * (Math.Log( (double) cs.Frequency / (double) nd.VT.RowCount, 2));
 			}
			return -info;
		}

//		public static double InfoAttributeAndSplitInfo(Node nd, Attribute att)
		public static double infoAttribute(Node nd, Attribute att, ref bool splitValid)
		{
			Case attCs, classCs;
			double infoActual, infoRelp1, infoRelp2, infoAttributeMin;
			int attCaseLCount = att.CaseL.Count;
			int attributeMatchClass=0;
			int y;
			int DependentVariableAttributeCaseLCount = nd.DependentVariableAttribute.CaseL.Count;

			if(attCaseLCount < 2)
			{
				splitValid=false;
				return -1;
			}

			infoAttributeMin=0;
			for(int ac=0; ac < attCaseLCount; ++ac)
			{
				attCs = (Case) att.CaseL[ac];
				infoActual=0;
				for(int cc = 0; cc < DependentVariableAttributeCaseLCount; ++cc)
				{
					classCs = (Case) nd.DependentVariableAttribute.CaseL[cc];
					attributeMatchClass=0;
					for(y = 0; y < nd.VT.RowCount; ++y) // /?????
						if(attCs.Val.ToString().CompareTo(nd.VT[att.VariableIndex, y].ToString() ) == 0  && classCs.Val.ToString().CompareTo(nd.VT[Variable.DependentVariableIndex, y].ToString()) == 0 )
							++attributeMatchClass;
					infoRelp1 = (double) attributeMatchClass / (double) classCs.Frequency;
					infoRelp2 =  -1 * Math.Log( (double) attributeMatchClass / (double) classCs.Frequency, 2);
					//Check above outdated
					if(attributeMatchClass > 0)
						infoActual += infoRelp1 * infoRelp2;
				}
				infoAttributeMin += ((double) attCs.Frequency / (double) nd.VT.RowCount) * infoActual;
			}
			return infoAttributeMin;
		}
	

		public static double InfoContinuousAttribute(Node nd, Attribute att, ref double thresholdMinInfo, ref bool splitValid)
		{
			Case classCs;
			double infoActual, infoActualUptoT, infoActualUptoTp2, infoActualHigherT, infoActualHigherTp2;
			double infoAttributeMin;
			int attributeMatchClass=0;
			int y;
			int DependentVariableAttributeCaseLCount = nd.DependentVariableAttribute.CaseL.Count;
			bool infoAttributeIsSet = false;

			infoAttributeMin=0;

			DynArrInt threshold = new DynArrInt();
			SetThresholdIndexes(threshold, nd, att.VariableIndex);
			if(threshold.Count==0)
			{
				splitValid=false;
				return -1;
			}
			for(int ti=0; ti < threshold.Count; ++ti)
			{
				infoActual=0;
				infoActualUptoT = (double) (threshold[ti] + 1) / nd.VT.RowCount ;
				infoActualUptoTp2 = 0;
				attributeMatchClass = 0;
				infoActualHigherT = (double) (nd.VT.RowCount - (threshold[ti] + 1)) / nd.VT.RowCount ;
				infoActualHigherTp2 = 0;
				attributeMatchClass = 0;

				for (int classIndex = 0; classIndex < DependentVariableAttributeCaseLCount; ++classIndex)
				{
					
					//Calculation of the info up to threshold second part 
					attributeMatchClass = 0;
					classCs = (Case) nd.DependentVariableAttribute.CaseL[classIndex];
					for(y = 0; y <= threshold[ti]; ++y)
					{
						if(classCs.Val.ToString().CompareTo(nd.VT[Variable.DependentVariableIndex, y].ToString()) == 0)
							++attributeMatchClass;
					}
					if(attributeMatchClass > 0)
						infoActualUptoTp2 += -1 * Math.Log( (double) attributeMatchClass / (double) (threshold[ti] + 1), 2);
					
					//Calculation of the info higher threshold second part
					attributeMatchClass = 0;
					for(y = threshold[ti] + 1; y < nd.VT.RowCount; ++y)
					{
							if(classCs.Val.ToString().CompareTo(nd.VT[Variable.DependentVariableIndex, y].ToString()) == 0)
							++attributeMatchClass;
					}
					if(attributeMatchClass > 0)
						infoActualHigherTp2 += -1 * Math.Log( (double) attributeMatchClass / (double) (nd.VT.RowCount - (threshold[ti] + 1)), 2);
				}
				infoActualUptoT = infoActualUptoT * infoActualUptoTp2;
				infoActualHigherT = infoActualHigherT * infoActualHigherTp2;
				infoActual = infoActualUptoT + infoActualHigherT;
				
				if(infoAttributeIsSet == false)
				{
					infoAttributeMin = infoActual;
					infoAttributeIsSet = true;
					//This is the real threshold as a value
					thresholdMinInfo = nd.VT.GetNumber(att.VariableIndex, threshold[ti]);
				}
				else
				if(infoActual < infoAttributeMin)
				{
					infoAttributeMin = infoActual;
					//This is the real threshold as a value
					thresholdMinInfo = nd.VT.GetNumber(att.VariableIndex, threshold[ti]);
				}
			}
			return infoAttributeMin;
		}


			
		private static void	SetThresholdIndexes(DynArrInt threshold, Node nd, int attIndex)
		{
			int uniqueLastIdx=0;
			double uniqueLast = nd.VT.GetNumber(attIndex, uniqueLastIdx);
			int uniqueIdx, thIdx, i, rc = nd.VT.RowCount;
			double unique, th, valMin;
				
			//Ends when there are not enough rows at the end
			for(uniqueIdx = Option.C45MinNumOfCasesPerContinuosSplit -1; uniqueIdx + Option.C45MinNumOfCasesPerContinuosSplit < rc; ++uniqueIdx)
			{
				//outdated can be optimised
				if(nd.VT.GetNumber(attIndex, uniqueIdx) != nd.VT.GetNumber(attIndex, uniqueIdx+1))  
					threshold.Add(uniqueIdx);
			}
		}


	}
}
