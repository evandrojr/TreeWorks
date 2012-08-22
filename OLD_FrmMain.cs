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
	/// Summary description.
	/// </summary>
	public class FrmMain : System.Windows.Forms.Form
	{
		public static FrmMain SFrmMain;
		
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.ToolBar toolBar;
		private System.Windows.Forms.ImageList imageListToolBar;
		public System.Windows.Forms.PictureBox PbBase;
		private System.Windows.Forms.ToolBarButton tbBtAddLeaf;
		private System.Windows.Forms.ToolBarButton tbBtShowDataset;
		private System.Windows.Forms.ToolBarButton tbBtDependentVariable;
		private System.Windows.Forms.ToolBarButton tbBtOption;
		private System.ComponentModel.IContainer components;
		public System.Windows.Forms.StatusBar StatusBar;
		private System.Windows.Forms.StatusBarPanel statusBarPanel1;
		private System.Windows.Forms.Panel panel1;

		public PhysicalTable PT;

		public FrmMain()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			SFrmMain=this;
			this.PbBase.Top=43;
			this.PbBase.Left=0;
			this.PbBase.Width = this.Width;
			this.PbBase.Height = this.Height - 43;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FrmMain));
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.toolBar = new System.Windows.Forms.ToolBar();
			this.tbBtAddLeaf = new System.Windows.Forms.ToolBarButton();
			this.tbBtShowDataset = new System.Windows.Forms.ToolBarButton();
			this.tbBtDependentVariable = new System.Windows.Forms.ToolBarButton();
			this.tbBtOption = new System.Windows.Forms.ToolBarButton();
			this.imageListToolBar = new System.Windows.Forms.ImageList(this.components);
			this.PbBase = new System.Windows.Forms.PictureBox();
			this.StatusBar = new System.Windows.Forms.StatusBar();
			this.statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2,
																					  this.menuItem3,
																					  this.menuItem4});
			this.menuItem1.Text = "File";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "Open";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "Save";
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 2;
			this.menuItem4.Text = "Save as";
			// 
			// toolBar
			// 
			this.toolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																					   this.tbBtAddLeaf,
																					   this.tbBtShowDataset,
																					   this.tbBtDependentVariable,
																					   this.tbBtOption});
			this.toolBar.ButtonSize = new System.Drawing.Size(30, 30);
			this.toolBar.DropDownArrows = true;
			this.toolBar.ImageList = this.imageListToolBar;
			this.toolBar.Location = new System.Drawing.Point(0, 0);
			this.toolBar.Name = "toolBar";
			this.toolBar.ShowToolTips = true;
			this.toolBar.Size = new System.Drawing.Size(736, 42);
			this.toolBar.TabIndex = 0;
			this.toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar_ButtonClick);
			// 
			// tbBtAddLeaf
			// 
			this.tbBtAddLeaf.ImageIndex = 0;
			this.tbBtAddLeaf.ToolTipText = "Create Tree";
			// 
			// tbBtShowDataset
			// 
			this.tbBtShowDataset.ImageIndex = 1;
			this.tbBtShowDataset.ToolTipText = "Choose training data";
			// 
			// tbBtDependentVariable
			// 
			this.tbBtDependentVariable.ImageIndex = 2;
			this.tbBtDependentVariable.ToolTipText = "Variables";
			// 
			// tbBtOption
			// 
			this.tbBtOption.ImageIndex = 3;
			this.tbBtOption.Text = "Options";
			this.tbBtOption.ToolTipText = "Options";
			// 
			// imageListToolBar
			// 
			this.imageListToolBar.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.imageListToolBar.ImageSize = new System.Drawing.Size(30, 30);
			this.imageListToolBar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListToolBar.ImageStream")));
			this.imageListToolBar.TransparentColor = System.Drawing.Color.Black;
			// 
			// PbBase
			// 
			this.PbBase.BackColor = System.Drawing.Color.WhiteSmoke;
			this.PbBase.Location = new System.Drawing.Point(8, 8);
			this.PbBase.Name = "PbBase";
			this.PbBase.Size = new System.Drawing.Size(80, 56);
			this.PbBase.TabIndex = 4;
			this.PbBase.TabStop = false;
			this.PbBase.Paint += new System.Windows.Forms.PaintEventHandler(this.PbBase_Paint);
			// 
			// StatusBar
			// 
			this.StatusBar.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.StatusBar.Location = new System.Drawing.Point(0, 347);
			this.StatusBar.Name = "StatusBar";
			this.StatusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						 this.statusBarPanel1});
			this.StatusBar.ShowPanels = true;
			this.StatusBar.Size = new System.Drawing.Size(736, 22);
			this.StatusBar.TabIndex = 5;
			// 
			// statusBarPanel1
			// 
			this.statusBarPanel1.Width = 200;
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.AutoScroll = true;
			this.panel1.Controls.Add(this.PbBase);
			this.panel1.Location = new System.Drawing.Point(0, 40);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(736, 304);
			this.panel1.TabIndex = 6;
			// 
			// FrmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 11);
			this.AutoScroll = true;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(736, 369);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.StatusBar);
			this.Controls.Add(this.toolBar);
			this.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Menu = this.mainMenu;
			this.Name = "FrmMain";
			this.Text = "Spartacus";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Option.AlgorithmEnum Algorithm = Option.Algorithm; //Only to initialise the Option values by calling the static constructor
			string APPLICATION_DIR = Constant.APPLICATION_DIR; //Only to initialise the Constant.APPLICATION_DIR value by calling the static constructor
			Application.Run(new FrmMain());			
		}


		private void TreeBuild()
		{
			if(Tree.Root == null)
			{
				MessageBox.Show("You must open the training data set first!", "Sorry");
				return;
			}
			Tree.ResetKeepingRoot();
			switch (Option.Algorithm)
			{
				case Option.AlgorithmEnum.C45:
					C45.TreeBuild(Tree.Root);
					break;
				case Option.AlgorithmEnum.CART:
			//		CART.TreeBuild();
			//		int t=0;
					break;
				default:
					MessageBox.Show("Select the algorithm in 'Options'", "Sorry");
					break;
			}
			Tree.GenerateLayout();
			PbBase.Invalidate();
		}
	
		private void ShowDataSet()
		{
			FrmDataSet fds=new FrmDataSet();
			fds.ShowDialog();
		}

		private void ChooseDependentVariable()
		{
			FrmDependentVariable fdv=new FrmDependentVariable();
			fdv.ShowDialog();
		}

		private void ShowOption()
		{
			FrmOption fo=new FrmOption();
			fo.ShowDialog();
		}

		private void toolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			switch(toolBar.Buttons.IndexOf(e.Button))
			{
				case 0:
					this.TreeBuild();
					break; 
				case 1:
					ShowDataSet();					
					break; 
				case 2:
					ChooseDependentVariable();					
					break;
				case 3:
					ShowOption();					
					break; 
			}			
		}

		private void PbBase_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			Tree.Draw(g);
		}


		private void LabelGetCharSize_Click(object sender, System.EventArgs e)
		{
		
		}

		private void textBox1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			//textBox1.Select(0,0);
			//textBox1.
		}

		private void textBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
		
		}

		private void FrmMain_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			this.toolBar.Top = -1 * this.AutoScrollPosition.Y;
			this.toolBar.Left = this.AutoScrollPosition.X;

		}


	}
}
