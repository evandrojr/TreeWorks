#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace Spartacus
{
    public partial class FrmMain : Form
    {
        private FrmWizardNewProject fwnp = new FrmWizardNewProject();
        private FrmWizardImportDataChooseDimensionality fwidcd = null;
        public static SplitCategoryView SplitCategoryViewActive = null;
        private bool treeCanBeDisplayed = false;
        
        public FrmMain()
        {
            Visible = false;
            InitializeComponent();
            Def.FrmMain = this; //Implicitly initialises the Def class
            Def.PbBase = this.PbBase;
            Def.PanelMain = this.PanelMain;
            Def.ToolBar = this.ToolBar;
            Def.Schema = new Schema();
            Def.Tree = new Tree();
            //for (int i = 0; i < ToolBar.Items.Count; ++i)
            //    ToolBar.Items[i].Enabled= true;
            Def.Schema.Tree = Def.Tree;
            Def.Tree.Schema = Def.Schema;
            Database db = new Database(Database.DriverEnum.PostgreSQL);
            Def.Db = db;
            Def.Db.ConStr = "DRIVER={" + Def.DbDriver + "};UID=" + Def.DbUser + ";SERVER=" + Def.DbServer + ";Port=" + Def.DbPort + ";Database=" + Def.DbDatabase + ";Password=" + Def.DbPassword;
            Def.Db.Connect();
            Def.TreeCanBeDisplayed = false;

        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            Visible = true;
            FillPanelMainWithPbBase();
            fwnp.Show(this);
        }


        public void FillPanelMainWithPbBase(){
            if (PbBase.Width < PanelMain.Width)
                PbBase.Width = PanelMain.Width - Def.LtSizeDifferencePanelMainAndPbBase;
            if (PbBase.Height < PanelMain.Height)
                PbBase.Height = PanelMain.Height - Def.LtSizeDifferencePanelMainAndPbBase;
        }

        public void TreeBuild() {
            Def.Tree.CreateLayout();
            PbBase.Invalidate();
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            //if(DialogResult.Yes != MessageBox.Show("Are you sure you want to close this project? The tree will be lost", "Close project", MessageBoxButtons.YesNo))
            //    return;
            lbInfo.Text = "";
            Def.TreeCanBeDisplayed = false;
            Node nd;      
//          Removes the last Table and table
            for (int i = Def.Tree.NodeLst.Count - 1; i >= 0; --i) {
                nd = Def.Tree.NodeLst[i];
                nd.Dispose();
            }
            Def.Schema = new Schema();
            Def.Tree = new Tree();
            Def.Schema.Tree = Def.Tree;
            Def.Tree.Schema = Def.Schema;
            Node.NodeIDCounterReset();
            Def.PbBase.Invalidate();
//          Opens FrmWizardNewProject if it is closed
            ToolBar.Items["btNew"].Enabled = false;
            if (fwnp.IsDisposed == true) {
                fwnp = new FrmWizardNewProject();
                fwnp.Show(this);
            }
        }

        private void showNodeDesingToolStripButton_Click(object sender, EventArgs e)
        {
            FrmNodeDesign f = new FrmNodeDesign();
            f.Show(this);
        }

        private void PbBase_Paint(object sender, PaintEventArgs e)
        {
           // double scrollBarY=PanelMain.AutoScrollPosition.Y;
            Graphics g = e.Graphics;
            Def.Tree.Draw(g);
            if (Def.Tree.Root != null) {
                if (Def.Schema.Target.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Categorical)
                    lbInfo.Text = "RiE: ";
                else
                    lbInfo.Text = "RiV: ";
                lbInfo.Text += Math.Round(Def.Tree.ReductionInImp, 0) + "%";
            }
            //PanelMain.AutoScrollPosition.Y = scrollBarY;
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            FillPanelMainWithPbBase();
        }

        private void optionsToolStripButton_Click(object sender, EventArgs e)
        {
            FrmOption fo = new FrmOption();
            fo.ShowDialog();
        }

        private void movedataToolStripButton_Click(object sender, EventArgs e)
        {
            FrmDataTransport fdt = new FrmDataTransport();
            fdt.ShowDialog();
            Def.Tree.ReductionInImpCalc();
            Def.PbBase.Invalidate();
        }

        private void pruneToolStripButton_Click(object sender, EventArgs e)
        {
            if (Def.Tree.Root == null)
                return;
            if (Def.Schema.Target.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
                MessageBox.Show("Prune not available for continuous target variables yet", "Warning");
                return;
            }
            Prune.PruneTree();
            Def.Tree.ReductionInImpCalc();
            Def.PbBase.Invalidate();
        }

        private void growandPruneToolStripButton_Click(object sender, EventArgs e)
        {
            if (Def.Tree.Root == null)
                return;
            if (Def.Schema.Target.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
                MessageBox.Show("Prune not available for continuous target variables yet", "Warning");
                return;
            }
            Def.Tree.RemoveDescendents(Def.Tree.Root);
            Node.NodeIDCounterSetTo(0);
            Def.Tree.FullAutogrow(Def.Tree.Root);
            Prune.PruneTree();
            Def.Tree.ReductionInImpCalc();
            PbBase.Invalidate();
        }

        private void fullgrowToolStripButton_Click(object sender, EventArgs e)
        {
            Def.Tree.RemoveDescendents(Def.Tree.Root);
            Node.NodeIDCounterSetTo(0);
            Def.Tree.FullAutogrow(Def.Tree.Root);
            Def.Tree.ReductionInImpCalc();
            PbBase.Invalidate();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Png files|*.png";
            saveFileDialog.ShowDialog();
            //PbBase.LayoutEngine.Layout(
           
        }

        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
   //         bmp.Save(saveFileDialog.FileName);
        }

        private void label1_Click(object sender, EventArgs e)
        {
        
        }

        private void pnlInfo_Paint(object sender, PaintEventArgs e)
        {
        
        }

        private void PbBase_Click(object sender, EventArgs e)
        {
        
        }

        private void ImportDataToolStripButton_Click(object sender, EventArgs e) {

            bool canOpen = false;

            if (fwidcd == null)
                canOpen = true;
            else
                if (fwidcd.IsDisposed == true)
                    canOpen = true;
            if (canOpen == false)
                return;
            FrmWizardImportDataSelectFile fwidsf = new FrmWizardImportDataSelectFile();
            fwidsf.Show(this);
        }

        public bool TreeCanBeDisplayed {
            get { return treeCanBeDisplayed; }
            set {
                if (value==false) {
                    treeCanBeDisplayed = false;                        
                    this.PbBase.Paint -= new System.Windows.Forms.PaintEventHandler(this.PbBase_Paint);
                 } else {
                    treeCanBeDisplayed = true;
                    this.PbBase.Paint += new System.Windows.Forms.PaintEventHandler(this.PbBase_Paint);
                    this.PbBase.Invalidate();
                }
            }
        }

        private void toolStripButtonAbout_Click(object sender, EventArgs e) {
            FrmAbout f = new FrmAbout();
            f.ShowDialog();
        }

        private void toolStripButtonValidate_Click(object sender, EventArgs e) {
            if (Def.Tree.ModelType == Tree.ModelTypeEnum.Classification) {
                //FrmPredict frp = new FrmPredict();
                //frp.Show();
                if (Def.Multivariate) {
                    TreeMvValidation tmv = new TreeMvValidation();
                    FrmClsValidation fcv = new FrmClsValidation();
                    fcv.Show();
                } else {
                    Def.TreeValidation = new TreeValidation();
                    FrmClsValidation fcv = new FrmClsValidation();
                    fcv.Show();
                }
                return;
            } else {
                Def.TreeValidation = new TreeValidation();
                FrmVltValidation frv = new FrmVltValidation();
                frv.Show();
                return;
            }
        }



 
    }
}