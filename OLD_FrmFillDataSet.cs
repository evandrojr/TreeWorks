using System;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Spartacus
{
	/// <summary>
	/// Summary description for FrmDataSet.
	/// </summary>
	public class FrmDataSet : System.Windows.Forms.Form
	{
		private System.Windows.Forms.RadioButton rbDBase;
		private System.Windows.Forms.RadioButton rbExcell;
		private System.Windows.Forms.RadioButton rbCSV;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.GroupBox groupBoxDatabaseDriver;
		private System.Windows.Forms.Button openDatabase;
		private C1.Win.C1FlexGrid.C1FlexGrid grid;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrmDataSet()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBoxDatabaseDriver = new System.Windows.Forms.GroupBox();
			this.rbCSV = new System.Windows.Forms.RadioButton();
			this.rbDBase = new System.Windows.Forms.RadioButton();
			this.rbExcell = new System.Windows.Forms.RadioButton();
			this.openDatabase = new System.Windows.Forms.Button();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.grid = new C1.Win.C1FlexGrid.C1FlexGrid();
			this.groupBoxDatabaseDriver.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBoxDatabaseDriver
			// 
			this.groupBoxDatabaseDriver.Controls.Add(this.rbCSV);
			this.groupBoxDatabaseDriver.Controls.Add(this.rbDBase);
			this.groupBoxDatabaseDriver.Controls.Add(this.rbExcell);
			this.groupBoxDatabaseDriver.Controls.Add(this.openDatabase);
			this.groupBoxDatabaseDriver.Location = new System.Drawing.Point(5, 0);
			this.groupBoxDatabaseDriver.Name = "groupBoxDatabaseDriver";
			this.groupBoxDatabaseDriver.Size = new System.Drawing.Size(427, 56);
			this.groupBoxDatabaseDriver.TabIndex = 0;
			this.groupBoxDatabaseDriver.TabStop = false;
			this.groupBoxDatabaseDriver.Text = "Database Driver";
			// 
			// rbCSV
			// 
			this.rbCSV.Location = new System.Drawing.Point(16, 24);
			this.rbCSV.Name = "rbCSV";
			this.rbCSV.Size = new System.Drawing.Size(48, 24);
			this.rbCSV.TabIndex = 2;
			this.rbCSV.Text = "CSV";
			// 
			// rbDBase
			// 
			this.rbDBase.Location = new System.Drawing.Point(120, 24);
			this.rbDBase.Name = "rbDBase";
			this.rbDBase.Size = new System.Drawing.Size(56, 24);
			this.rbDBase.TabIndex = 0;
			this.rbDBase.Text = "dBase";
			// 
			// rbExcell
			// 
			this.rbExcell.Location = new System.Drawing.Point(224, 24);
			this.rbExcell.Name = "rbExcell";
			this.rbExcell.Size = new System.Drawing.Size(72, 24);
			this.rbExcell.TabIndex = 1;
			this.rbExcell.Text = "MS Excel";
			this.rbExcell.CheckedChanged += new System.EventHandler(this.rbExcell_CheckedChanged);
			// 
			// openDatabase
			// 
			this.openDatabase.Location = new System.Drawing.Point(344, 24);
			this.openDatabase.Name = "openDatabase";
			this.openDatabase.Size = new System.Drawing.Size(72, 24);
			this.openDatabase.TabIndex = 2;
			this.openDatabase.Text = "Open file";
			this.openDatabase.Click += new System.EventHandler(this.OpenDatabase_Click);
			// 
			// openFileDialog
			// 
			this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
			// 
			// grid
			// 
			this.grid.AllowEditing = false;
			this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grid.BackColor = System.Drawing.Color.Silver;
			this.grid.ColumnInfo = "10,1,0,0,0,85,Columns:0{Width:28;}\t";
			this.grid.ExtendLastCol = true;
			this.grid.Location = new System.Drawing.Point(8, 64);
			this.grid.Name = "grid";
			this.grid.Size = new System.Drawing.Size(424, 280);
			this.grid.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(@"Normal{BackColor:GhostWhite;ForeColor:MidnightBlue;Border:Flat,1,RoyalBlue,Both;}	Fixed{BackColor:MidnightBlue;ForeColor:Lavender;Border:None,1,Black,Both;}	Highlight{BackColor:Teal;ForeColor:PaleGreen;}	Search{BackColor:Highlight;ForeColor:HighlightText;}	Frozen{BackColor:Beige;}	EmptyArea{BackColor:Lavender;Border:Flat,1,ControlDarkDark,Both;}	GrandTotal{BackColor:Black;ForeColor:White;}	Subtotal0{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal1{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal2{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal3{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal4{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal5{BackColor:ControlDarkDark;ForeColor:White;}	");
			this.grid.TabIndex = 5;
			this.grid.Visible = false;
			// 
			// FrmDataSet
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(440, 350);
			this.Controls.Add(this.grid);
			this.Controls.Add(this.groupBoxDatabaseDriver);
			this.Name = "FrmDataSet";
			this.Text = "Training data";
			this.Load += new System.EventHandler(this.FrmDataSet_Load);
			this.Closed += new System.EventHandler(this.FrmDataSet_Closed);
			this.groupBoxDatabaseDriver.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void FrmDataSet_Load(object sender, System.EventArgs e)
		{

									
		}

		private void OpenDatabase_Click(object sender, System.EventArgs e)
		{
			Db.DriverEnum dbDriver=databaseDriver();
			switch (dbDriver)
			{
				case Db.DriverEnum.Undefined:
					MessageBox.Show("Please, select a database driver first", "Warning");
					return;
				case Db.DriverEnum.Access:
					this.openFileDialog.Filter="MS Access database (*.mdb)|*.mdb";
					break;
				case Db.DriverEnum.CSV:
					this.openFileDialog.Filter="Comma separated values (*.csv)|*.csv";
					break;
				case Db.DriverEnum.DBase:
					this.openFileDialog.Filter="dBase table files (*.dbf)|*.dbf";
					break;
				case Db.DriverEnum.Excel:
					this.openFileDialog.Filter="MS Excel table files (*.xls)|*.xls";
					break;
				default:
					MessageBox.Show("Internal Error", "Warning");
					break;
			}
			this.openFileDialog.ShowDialog();
		}

		private void openFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if( openFileDialog.FileName.ToString() != "")
				startDataReader();
		}
		private Db.DriverEnum databaseDriver()
		{
			if(this.rbDBase.Checked==true)
				return Db.DriverEnum.DBase;
			else
			if(this.rbExcell.Checked==true)
				return Db.DriverEnum.Excel;
			else
			if(this.rbCSV.Checked==true)
				return Db.DriverEnum.CSV;
			return Db.DriverEnum.Undefined;
		}

		private void startDataReader()
		{
			Db.DriverEnum dbDriver=databaseDriver();
			Db db=new Db(dbDriver);
			string qryReader, qryRowCount, fileName, fileBaseName, filePath, fileNameWithPath;
			OdbcCommand cmd = new OdbcCommand();
			int rowCount, colCount;
			OdbcDataReader reader; 

			rowCount=colCount=0;
			qryReader=qryRowCount=fileName=fileBaseName=filePath=fileNameWithPath="";

			fileNameWithPath = this.openFileDialog.FileName;
			fileName = Fcn.FileName(fileNameWithPath);
			fileBaseName = Fcn.FileBaseName(fileName);
			filePath = Fcn.FilePath(fileNameWithPath);
			switch (dbDriver)
			{
				case Db.DriverEnum.DBase:
					 db.Connect("MaxBufferSize=2048;DSN=dBASE Files;PageTimeout=5;DefaultDir=" + filePath +
					     ";DBQ=" + filePath + ";DriverId=533");
      			     qryRowCount="Select count(*) from " + fileBaseName;
					 qryReader="Select * from " + fileBaseName;
				break;
				case Db.DriverEnum.CSV:
					 db.Connect("MaxBufferSize=2048;FIL=text;DSN=CSV;PageTimeout=5;DefaultDir=" + filePath + 
						";DBQ=" + filePath + ";DriverId=27");
					 qryRowCount="Select count(*) from " + fileName;
					 qryReader="Select * from " + fileName;
				break;
				case Db.DriverEnum.Excel:
					db.Connect("MaxBufferSize=2048;DSN=Excel Files;PageTimeout=5;DefaultDir=" + filePath + 
					";DBQ=" + fileNameWithPath + ";DriverId=790");
					qryRowCount="Select count(*) from [Sheet1$]";
					qryReader="Select * from [Sheet1$]";
					break;
			}
			cmd = new OdbcCommand(qryRowCount, db.Con); 
			Def= (int) cmd.ExecuteScalar();
			cmd = new OdbcCommand(qryReader, db.Con); 
			reader = cmd.ExecuteReader();
			colCount = reader.FieldCount;
			FillPhysicalTable(reader, colCount, rowCount);	
			db.Close();
		}


		private void FillPhysicalTable(OdbcDataReader reader, int colCount, int rowCount)
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

			FrmMain.SFrmMain.PT = new PhysicalTable(colCount , rowCount);
			int x, y;
			bool firstTime = true;
			Variable var;
			string colName="";
			
			grid.Rows.Count = rowCount+1;
			grid.Cols.Count = colCount+1;
			y=0;

			while(reader.Read())
			{
				grid[y+1, 0]=y;
				for(x=0; x < reader.FieldCount; ++x)
				{
					if(firstTime)
					{
						colName = reader.GetName(x).ToLower();
						grid[y, x+1] = colName;
						if(reader[x] is double || reader[x] is int)
						{
							FrmMain.SFrmMain.PT.AddCol(x, colName, rowCount, PhysicalColumn.DataTypeEnum.Number);
							var = new Variable(colName, Variable.TypeEnum.Continuous, PhysicalColumn.DataTypeEnum.Number);
						}
						else
						{
							FrmMain.SFrmMain.PT.AddCol(x, colName, rowCount, PhysicalColumn.DataTypeEnum.Text);
							var = new Variable(colName, Variable.TypeEnum.Categorical, PhysicalColumn.DataTypeEnum.Text);
						}	
					}
					FrmMain.SFrmMain.PT[x, y]=reader[x];
					grid[y+1, x+1] = reader[x];
				}
				firstTime=false;
				++y;
			}
			grid.Visible=true;
//			grid.Cols //Set a code to autofit the columns width

			VirtualTable virtualTable = new VirtualTable(FrmMain.SFrmMain.PT);
			Node root = new Node("All data", virtualTable);
			Tree.Root = root;
		}

		private void FrmDataSet_Closed(object sender, System.EventArgs e)
		{
			if(Tree.Root == null)
				return; 
			this.Visible=false;
			FrmDependentVariable fdv=new FrmDependentVariable();
				fdv.ShowDialog();
		}

		private void rbExcell_CheckedChanged(object sender, System.EventArgs e)
		{
			if(rbExcell.Checked==true)
				MessageBox.Show("The information has to be in the sheet named 'Sheet1' inside of the MS Excel file that you are going to open. If it is not, then open the file in the MS Excel, change the name of the sheet to 'Sheet1' and CLOSE the MS Excel. After that you can use the MS Excel file with this program.", "Warning!");
		}
	
	}
}
