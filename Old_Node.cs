using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Imaging;

namespace Spartacus
{
	/// <summary>
	/// Summary description for Node.
	/// </summary>
	public class Node : TextBox
	{
		private Node father;
		private ArrayList child = new ArrayList();
		public TypeEnum type;
		private static int cntId=-1; //Used to count all the Nodes never decrement
		private int id=-1; //Never decrement, used to refer objects
		private int level=-1;
		public Label LabelTop, LabelBotton;
		private Point centerCoord = new Point(0,0);
		public VirtualTable VT;
		public ArrayList AttributeL = new ArrayList();
		public string SplitVariableName="";
		public string CaseSplit;
		public double Impurity = -1;
		public bool DataGridOpen = false;


		public Node()
		{
			++Node.cntId;
			id = Node.cntId;
			Tree.NodeL.Add(this);
			LabelTop = new Label();
			LabelBotton = new Label();
			this.BackColor = System.Drawing.Color.GhostWhite;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "Node ID " + id;
			this.Size = new System.Drawing.Size(100, 100);
//			this.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Multiline = true;
			this.WordWrap = false;
			this.Click +=new EventHandler(Node_Click);
			this.MouseDown+=new MouseEventHandler(Node_MouseDown);
			this.MouseUp+=new MouseEventHandler(Node_MouseUp);
			this.MouseMove+=new MouseEventHandler(Node_MouseMove);
			

			this.Visible = false;
			this.ForeColor = Color.Black;
			this.ReadOnly = true;
			this.Cursor = System.Windows.Forms.Cursors.Arrow;

			
			LabelTop.AutoSize = true;
			LabelTop.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			LabelTop.ForeColor = Color.DarkViolet;
			LabelBotton.AutoSize = true;
			LabelBotton.Font = new System.Drawing.Font("Tahoma", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			LabelBotton.ForeColor = Color.DarkBlue;
			FrmMain.SFrmMain.PbBase.Controls.Add(this);
			FrmMain.SFrmMain.PbBase.Controls.Add(this.LabelTop);
			FrmMain.SFrmMain.PbBase.Controls.Add(this.LabelBotton);
			this.type=Node.TypeEnum.Leaf;

		}

		public Node(string title) : this()
		{
			CaseSplit=title;
		}


		public Node(string title, VirtualTable vt) : this(title)
		{
			Attribute att=new Attribute();

			VT = vt;
			//Fills the cases, maybe is not suitable for C&RT
			for(int x=0; x < Tree.VariableL.Count; ++x)
			{
				att = new Attribute(x);
				AttributeL.Add(att);
				for(int y=0; y < VT.RowCount; ++y)
				{
					att.AddCase(VT[x, y]);					
				}
			}
	
		}

		protected override void OnClick(System.EventArgs e)
		{
			return;
		}

		protected void Node_Click(object o, System.EventArgs e)
		{
			Select(0,0);

	
		}

		public void FillGrid()
		{

			if(this.DataGridOpen==true)
				return;
			
			int rc = this.VT.RowCount;
			int cc = this.VT.ColCount;

			FrmNodeGrid fg = new FrmNodeGrid();
			fg.SetNode(this);
			fg.Show();
			fg.Text = "Node " + this.id + " data";
			

			fg.Grid.Location = new System.Drawing.Point(0, 0);
			
			fg.Grid.Rows.Count = rc + 1;
			fg.Grid.Cols.Count = cc + 1;

			for(int x=0; x < cc; ++x)
				fg.Grid[0, x+1] = VT.ColName(x);	
			for(int y=0; y < rc; ++y)
			{
				fg.Grid[y+1, 0] = y;
				for(int x=0; x < cc; ++x)
				 fg.Grid[y+1, x+1] = VT[x, y];	
			}
			fg.Height = fg.Grid.Height + 4;
		}

		protected void Node_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Select(0,0);
			FillGrid();
		}
		protected void Node_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Select(0,0);
		}
		protected void Node_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Select(0,0);
		}


		public enum TypeEnum : byte
		{
			Root,
			Leaf,
		}

		//Check!
		public int AttributeIndex(string sAtt)
		{
			int ac=AttributeL.Count;
			Attribute att;
			for(int i=0; i < ac; ++i)
			{
				att = (Attribute) AttributeL[i];
				if(att.Name==sAtt)
					return i;
			}
			MessageBox.Show("Couldn't find the AttributeIndex", "Node.cd");
			return -1;
		}


		//Check!
		public Attribute DependentVariableAttribute
		{
			get
			{
				foreach(Attribute att in AttributeL)
				{
					if(att.Name.CompareTo(Variable.DependentVariable) == 0)
						return att;
				}
				MessageBox.Show("Error could not find the attribute: " + Variable.DependentVariable, "Node.cs");				
				return null;
			}
	    }

		public static int CntId
		{
			set{cntId=value;}
		}


		public void AddChild(Node n)
		{
			this.child.Add(n);
			n.father=this;
			ArrayList bL;
			if(n.type != Node.TypeEnum.Root)
			{
				n.level=n.father.level+1;
				if(Tree.LevelLast < n.level)
				{
					Tree.LevelLast=n.level;
					bL = new ArrayList();
					bL.Add(n);
					Tree.LevelBrotherL.Add(bL);
				}
				else
				{
					bL = (ArrayList) Tree.LevelBrotherL[n.Level];
					bL.Add(n);
					Tree.LevelBrotherL[n.Level]= bL;
				}

			}
		}

		public Node Father
		{
			set{father=value;}
			get{return father;}
		}

		public ArrayList Child
		{
			set{child=value;}
			get{return child;}
		}


		public TypeEnum Type
		{
			get {return type;}
		}
		public int Id
		{
			get{return id;}
		}
		
		public int Level
		{
			get{return level;}

			set
			{
				if(value  >= 0)
					level=value;
			}
		}

		public Point CenterCoord
		{
			set
			{
				Point pt = (Point) value;
				centerCoord.X = pt.X;
				centerCoord.Y = pt.Y;
				this.Left=centerCoord.X-(this.Width/2);
				this.Top=centerCoord.Y-(this.Height/2);
				LabelTop.Left =  centerCoord.X - (LabelTop.Width/2);
				LabelTop.Top =  Top  - (int) (Tree.LabelTopDistanceToNode + Tree.NodeCharHeight);
				LabelBotton.Left =  centerCoord.X - (int) (LabelBotton.Width/2);
				LabelBotton.Top =  Top  + Height + (Tree.LabelBottonDistanceToNode);
			}
			get
			{
				return centerCoord;
			}
		}
			

	}
}