using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Spartacus
{
	/// <summary>
	/// Summary description for Tree.
	/// </summary>
	public class Tree 
	{
		private static Node root;
		public static OrientationEnum Orientation;
		public const int TopBorder = 10;
		public const string NodeClassLabel = "Class";
		public const string NodeClassQtyLabel = "n";
		public const string NodeClassPercentLabel = "%";
		public const int LabelTopDistanceToNode = 10;
		public const int LabelBottonDistanceToNode = 10;		
		public const int NodeDisplayClassNameTextSizeMax = 10;
		public const int NodeDisplayClassPercentageTextSizeMax = 2;		
		
		public const int ClassNameDisplayCharMax = 10;
		public static ArrayList VariableL = new ArrayList(); //Includes de dependent variable and all others
		public static int LevelLast=0;
		public static ArrayList NodeL = new ArrayList();
		public static ArrayList LevelBrotherL = new ArrayList(); //Countain the a list of brothers(node) for each level
// 6.9x11.2 pixes Char size for Lucida Console, 8.25pt
		public const double NodeCharWidth = 7.5;
		public const double NodeCharHeight = 12;
		public const int classDisplayQtyMax = 4 ;
		public const int NodeCharRowQtyMax = 20 ;
		public const int NodeWidthMax = (int) ( (double) NodeCharRowQtyMax * NodeCharWidth); 
		public const int NodeHeightMax = (int) ( (double) (classDisplayQtyMax + 5) * NodeCharHeight); 
		public const int NodeDistForBrothers = 20;
		public const int NodeDistForLevels = 100;
		public const int DistOffsetForEvenBrothers = (int) (double) NodeWidthMax / 2; 
		public static int Width = 0;
		public static int Height = 0;
	
		// 
		//

		static Tree()
		{
			ArrayList bL = new ArrayList();
			LevelBrotherL.Add(bL);
			Orientation=OrientationEnum.Vertical;
		}

		public static Node Root
		{
			set{
				root=value;
				value.Level=0;
				value.type=Node.TypeEnum.Root;
		
				ArrayList bL = new ArrayList();
				bL.Add(value);
				LevelBrotherL[0]=bL; //In the level zero only root exists
				root.CenterCoord= new Point(FrmMain.SFrmMain.PbBase.Width/2, NodeDistForLevels + (TopBorder * 2));
			}
			get
			{
				return root;
			}
		}


		public static void Reset()
		{
			//Resets the objects
			foreach(Node nd in Tree.NodeL)
			{
				nd.AttributeL.Clear();
				nd.Dispose();
				nd.LabelTop.Dispose();
				nd.LabelBotton.Dispose();
			}
			Tree.NodeL.Clear();
			Tree.VariableL.Clear();
			Tree.LevelBrotherL.Clear();
			Tree.LevelLast=0;
			Tree.Width = 0;
			Tree.Height = 0;
			Node.CntId=-1;
			//End resets
			//Code of the static tree constructor that must be called
			ArrayList bL = new ArrayList();
			Tree.LevelBrotherL.Add(bL);
			//end Code of the static tree constructor that must be called
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

		public static void ResetKeepingRoot()
		{
			//Resets the objects
			int c = Tree.NodeL.Count;
			Node nd;
			Tree.root.Child.Clear();
			for(int i = c-1; i >= 0; --i)
			{
				nd = (Node) Tree.NodeL[i];
				if(nd!=Tree.Root)
				{
					nd.AttributeL.Clear();
					nd.LabelTop.Dispose();
					nd.LabelBotton.Dispose();
					Tree.NodeL.Remove(nd);
					nd.Dispose();
				}
			}
//			foreach(Variable val in Tree.VariableL)
//				val.HasBeenUsedToSplit=false;
//			Tree.LevelBrotherL.Clear();
			Tree.LevelLast=0;
			Node.CntId=0; // Root is already the 0 it is incremented before setting
			//End resets
			//Code of the static tree constructor that must be called
			ArrayList bL;
			c = Tree.LevelBrotherL.Count;
			for(int i = c-1; i > 0; --i)
			{
				bL = (ArrayList) Tree.LevelBrotherL[i];
				Tree.LevelBrotherL.Remove(bL);
			}


			
			//end Code of the static tree constructor that must be called
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

			


		public enum OrientationEnum : byte
		{
			Horizontal,
			Vertical,
		}

		public static void GenerateLayout()
		{
			Attribute classes = (Attribute) Tree.Root.AttributeL[Variable.DependentVariableIndex];
			int classesCount = classes.CaseL.Count;	
			Case cs, rootCs;
			int ClassNameSize=0;
			int ClassNameSizeMax=NodeClassLabel.Length;
			int ClassCaseQtyMax=0;
			int rowCharWidth = 0;
			string  str;

			foreach(Node nd in Tree.NodeL)
			{
				nd.Text="";
				ClassNameSize=0;
				ClassCaseQtyMax=nd.VT.RowCount.ToString().Length;
				nd.Visible=true;
				nd.DependentVariableAttribute.CaseSort();
				//Gets the max size
				for(int c=0; c < nd.DependentVariableAttribute.CaseL.Count; ++c)
				{
					cs = (Case) nd.DependentVariableAttribute.CaseL[c];	
					ClassNameSize=cs.Val.ToString().Length;
					if(ClassNameSize > ClassNameSizeMax)
						ClassNameSizeMax=ClassNameSize;
					if(ClassNameSizeMax > Tree.NodeDisplayClassNameTextSizeMax)
						ClassNameSizeMax = Tree.NodeDisplayClassNameTextSizeMax;
//					if(cs.Frequency.ToString().Length > ClassCaseQtyMax)
//						ClassCaseQtyMax = cs.Frequency.ToString().Length;
					
				}
				rowCharWidth = ClassNameSizeMax + ClassCaseQtyMax + 9;
				nd.Height = (int) ( (double) (nd.DependentVariableAttribute.CaseL.Count + 5) * NodeCharHeight); 
				if(nd.Height > NodeHeightMax)
					nd.Height = NodeHeightMax;
				nd.Width =  (int) ( (double) (ClassCaseQtyMax + 9 + ClassNameSizeMax) * NodeCharWidth);
 
				for(int i=0; i <= ( 7 + classesCount); ++i)
				{
					if(i==0) // Dependent Variable name (Root only)
					{
						if(nd == Tree.Root)
							nd.LabelTop.Text = "Dependent var, " + Variable.DependentVariable + " (" + Math.Round(nd.Impurity, 3) + " i)";
						else
							nd.LabelTop.Text = nd.CaseSplit + " (" + Math.Round(nd.Impurity, 3) +" i)";
					}
					else
					if(i==1) // Node Id
						nd.Text += Fcn.LineFill("Node " + nd.Id.ToString(), " " , rowCharWidth) + "\r\n";						
					else
					if(i==2) // ClassLabel    %  n
					{
						str = " ";
						if(ClassNameSizeMax > NodeClassLabel.Length)
							str = Fcn.LineFill(str, " ",  ClassNameSizeMax + 1 -  NodeClassLabel.Length);					
						nd.Text += Fcn.LineFill(NodeClassLabel + str + "%a  %r  n" , " " , rowCharWidth) + "\r\n";
					}
					else
					if(i==3)// ————————————————
						nd.Text +=Fcn.LineFill("—" , "—" , rowCharWidth) + "\r\n";
					else
					if( i > 3 && i <= ( 3 + classesCount) )
					{
						if( nd.DependentVariableAttribute.CaseL.Count >= ( i-4 + 1 ))
						{
							cs = (Case) nd.DependentVariableAttribute.CaseL[i-4];
							double percent =   ((double) cs.Frequency ) / ((double) nd.VT.RowCount);
							string percentS;
							percent = Math.Round( percent * 100);
							percentS = percent.ToString();

							//Just to initialise
							rootCs = (Case) Tree.Root.DependentVariableAttribute.CaseL[0];
							//Search for the same variable
							for(int caseCC=0; caseCC < Tree.Root.DependentVariableAttribute.CaseL.Count; ++ caseCC)
							{
								rootCs = (Case) Tree.Root.DependentVariableAttribute.CaseL[caseCC];
								if(rootCs.Val.ToString().CompareTo(cs.Val.ToString())==0)
									break;
							}							
							double percentAbs =   ((double) cs.Frequency ) / ((double) rootCs.Frequency);
							string percentAbsS;
							percentAbs = Math.Round( percentAbs * 100);
							percentAbsS = percentAbs.ToString();
							
							
							
							//Only writes ClassNameSizeMax chars for the class name if it is larger than the allowed
							if(cs.Val.ToString().Length > ClassNameSizeMax)
							{
								str=cs.Val.ToString().Substring(0, ClassNameSizeMax)  + " "; 
								nd.Text += str + Fcn.LineFill(percentS, " ", 3) + " " + Fcn.LineFill(percentAbsS, " ", 3) + " " + Fcn.LineFill(cs.Frequency.ToString(), " ", ClassCaseQtyMax);
							}
							else
							{
								str = " ";
								if(ClassNameSizeMax > cs.Val.ToString().Length )
									str = Fcn.LineFill(str, " ",  ClassNameSizeMax + 1 - cs.Val.ToString().Length);								
								str = cs.Val  + str + Fcn.LineFill(percentS, " ", 3) + " " + Fcn.LineFill(percentAbsS, " ", 3) + " " + Fcn.LineFill(cs.Frequency.ToString(), " ", ClassCaseQtyMax);
								nd.Text += Fcn.LineFill(str, " ", rowCharWidth) + "\r\n";
								
							}
						}
					}
					else
					if(i==(5 + classesCount)) // ———————————————————————
						nd.Text += Fcn.LineFill("—" , "—" , rowCharWidth) + "\r\n";
					if(i==(6 + classesCount)) // Total:     100   (total)
					{
						str = Fcn.LineFill("Total", " ", ClassNameSizeMax);
						str += " 100     " + nd.VT.RowCount ; 
						nd.Text+= str + "\r\n";
					}
					else
						if(i==(7 + classesCount))
					{
						if(nd.SplitVariableName.Length > 0)
							nd.LabelBotton.Text = nd.SplitVariableName;
					}
				}
			}
			SetCoordinates();
		}
			




		public static void SetCoordinates()
		{
			
			switch (Option.Algorithm)
			{
				case Option.AlgorithmEnum.C45:
					C45SetCoordinates();
					break;
				case Option.AlgorithmEnum.CART:
					CartSetCoordinates();
					break;
				default:
					MessageBox.Show("Select the algorithm in 'Options'", "Sorry");
					break;
			}
		}


		public static void C45SetCoordinates()
		{
			int brotherCount, brotherCountMax, x, y;
			Node n;
			ArrayList bL;
			brotherCountMax=0;
			for(int level=Tree.LevelLast; level>=0; --level)
			{
				bL = (ArrayList) LevelBrotherL[level];
				if(bL.Count > brotherCountMax)
					brotherCountMax = bL.Count;
			}
			Tree.Height = (Tree.LevelLast + 1) * (NodeDistForLevels + NodeHeightMax);
			Tree.Width = brotherCountMax * (NodeDistForBrothers + NodeWidthMax) + DistOffsetForEvenBrothers;
			FrmMain.SFrmMain.PbBase.Height = Tree.Height;
			FrmMain.SFrmMain.PbBase.Width  = Tree.Width;
			for(int level=Tree.LevelLast; level>=0; --level)
			{
				bL = (ArrayList) LevelBrotherL[level];
				brotherCount = bL.Count;
				for(int i=0; i < brotherCount; ++i)
				{
					n = (Node) bL[i];
					y = (Tree.Height / (Tree.LevelLast+1) ) * level + TopBorder * 8;
					if(brotherCount % 2 == 1)
						x = Tree.Width/2 + ( (i - (brotherCount/2) ) *  (NodeDistForBrothers + NodeWidthMax) ) ;
					else
						x = Tree.Width/2 + ( (i - (brotherCount/2) ) *  (NodeDistForBrothers + NodeWidthMax) ) + DistOffsetForEvenBrothers ;
					n.CenterCoord= new Point(x,y);
				}
			}
		}


		public static void CartSetCoordinates()
		{
//			int brotherCount, x, y;
//			Node n;
//			ArrayList bL;
//			for(int level=Tree.LevelLast; level>=0; --level)
//			{
//				bL = (ArrayList) LevelBrotherL[level];
//				brotherCount = bL.Count;
//				for(int i=0; i < brotherCount; ++i)
//				{
//					n = (Node) bL[i];
//					y = (FrmMain.SFrmMain.PbBase.Height / (Tree.LevelLast+1) ) * level + 20;
//					if(brotherCount % 2 == 1)
//						x = FrmMain.SFrmMain.PbBase.Width/2 + ( (i - (brotherCount/2) ) *  MinDistBetwLevels) ;
//					else
//						x = FrmMain.SFrmMain.PbBase.Width/2 + ( (i - (brotherCount/2) ) *  ) + 25 ;
//					n.CenterCoord= new Point(x,y);			
//				}
//			}
		}


		public static void Draw(Graphics g)
		{
			Pen linkPen = new Pen(Color.Black);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			Attribute classes;
			int classesCount = -1;
		
			if(Tree.NodeL.Count < 2)
				return;
			else
			{
				classes = (Attribute) Tree.Root.AttributeL[Variable.DependentVariableIndex];
				classesCount = classes.CaseL.Count;	
			}
			Node nd, nchild;
			for(int j=0; j < Tree.NodeL.Count; ++j)
			{
				//Not efficient, change later | outdated
				nd= (Node) Tree.NodeL[j];
				for(int c=0; c < nd.Child.Count; ++c)
				{
					nchild= (Node) nd.Child[c];
					g.DrawLine(linkPen, nd.CenterCoord.X , nd.Top + nd.Height, 
						 nchild.CenterCoord.X, nchild.Top);
				}
			}
			FrmMain.SFrmMain.StatusBar.Panels[0].Text = "Finished";
		}





	}
}
