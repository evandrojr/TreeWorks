using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Spartacus {
    public partial class FrmDataTransport : Form {

        bool UiCanUpdate = false;

        public FrmDataTransport() {
            InitializeComponent();
            if (Def.Schema.VariableLst.Count == 0) {
                MessageBox.Show("Tree not built yet", "Warning");
                Close();
                return;
            }
        }

        private void UiUpdate() {
            if (!UiCanUpdate)
                return;

            string clbValueSql;

            //CATEGORICAL
            if (Def.Schema.VariableLst[cbVariable.SelectedIndex].VariableTypeDetected == SchemaVariable.VariableTypeEnum.Categorical) {
                cbOperator.Items.Clear();
                cbOperator.Items.Add((string)" = ");
                cbOperator.Items.Add((string)" <> ");
                tbValue.Visible = false;
                clbValue.Visible = true;
                cbOperator.SelectedIndex = 0;
                clbValue.Items.Clear();
                clbValueSql =
                @"SELECT DISTINCT "
                    + cbVariable.SelectedItem +
                 " FROM " + Def.DbTrTb + cBSourceNode.SelectedItem + "," + Def.DbBsTb +
                 " WHERE " + Def.DbTrTb + cBSourceNode.SelectedItem + "." + Def.DbTableIdName + "=" +
                     Def.DbBsTb + "." + Def.DbTableIdName + " AND " +
                     cbVariable.SelectedItem + " IS NOT NULL " +    
                " ORDER BY " + cbVariable.SelectedItem;
                List<string> vLst;
                vLst = Def.Db.GetTextLst(clbValueSql);
                foreach(string s in vLst){
                    clbValue.Items.Add((string) s);
                }                
            } else { //CONTINUOUS  
                cbOperator.Items.Clear();
                cbOperator.Items.Add((string)" = ");
                cbOperator.Items.Add((string)" < ");
                cbOperator.Items.Add((string)" <= ");
                cbOperator.Items.Add((string)" > ");
                cbOperator.Items.Add((string)" >= ");
                cbOperator.Items.Add((string)" <> ");
                cbOperator.SelectedIndex = 0;
                tbValue.Visible = true;
                clbValue.Visible = false;
            }
        }

        private void cBSourceNode_SelectedIndexChanged(object sender, EventArgs e) {
            UiUpdate();
        }

        private void cBDestinationNode_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void FrmDataTransport_Load(object sender, EventArgs e) {
            foreach (Node nd in Def.Tree.NodeLst) {
                cBSourceNode.Items.Add((int)nd.Id);
                cBDestinationNode.Items.Add((int)nd.Id);
            }

            foreach (SchemaVariable sv in Def.Schema.VariableLst) {
                cbVariable.Items.Add((string) sv.Name);
            }
            cBSourceNode.SelectedIndex = 0;
            cBDestinationNode.SelectedIndex = 1;
            cbVariable.SelectedIndex=0;
            UiCanUpdate = true;
            UiUpdate();
        }

        private void cbVariable_SelectedIndexChanged(object sender, EventArgs e) {
            UiUpdate();
        }

        private void btInsert_Click(object sender, EventArgs e) {

            string with;
            if (rbAnd.Checked)
                with = " AND ";
            else
                if (rbOr.Checked)
                    with = " OR ";
                else
                    with = " ";

            //CATEGORICAL
            if (Def.Schema.VariableLst[cbVariable.SelectedIndex].VariableTypeDetected == SchemaVariable.VariableTypeEnum.Categorical) {
                string s ="( " ;
                //((Variable = cbVariable.SelectedItem) OR (Variable = cbVariable.SelectedItem))
                if ((string)cbOperator.SelectedItem == " = ") {
                    for(int i=0; i < clbValue.CheckedItems.Count; ++i){
                        s += "(" + cbVariable.SelectedItem + " = '" + clbValue.CheckedItems[i] + "') ";
                        if (i < clbValue.CheckedItems.Count - 1)
                            s+= "OR "; 
                    }
                } else {
                    //((Variable <> cbVariable.SelectedItem) AND (Variable <> cbVariable.SelectedItem))
                    for (int i = 0; i < clbValue.CheckedItems.Count; ++i) {
                        s += "(" + cbVariable.SelectedItem + " <> '" + clbValue.CheckedItems[i] + "') ";
                        if (i < clbValue.CheckedItems.Count - 1)
                            s += "AND ";
                    }
                }
                s+= ")" + with;
                tbWhere.Text += s;
            } else { //CONTINUOUS  
                tbWhere.Text += " " + cbVariable.SelectedItem + cbOperator.SelectedItem + tbValue.Text + with;
            }
        }

        private bool createTmpTable() {
            double rowsAffected=-1;
            if (tbWhere.Text.Length == 0) {
                MessageBox.Show("You've forgot to define the condition", "Warning");
                return false;
            }
            if (cBSourceNode.Text == cBDestinationNode.Text) {
                MessageBox.Show("Please, select a destination different of the source", "Warning");
                return false;
            }
            Def.Db.ExecuteNonQuery("DROP TABLE TMP", true);
            string lastAnd="";
            if (tbWhere.Text != "")
                lastAnd = " AND (" + tbWhere.Text + ")";
            string sql =
                @"CREATE TABLE " + 
                    "TMP AS " +
                        "SELECT " + Def.DbBsTb + "." + Def.DbTableIdName + " " +
                        "FROM " + 
                                Def.DbTrTb + cBSourceNode.SelectedItem + "," +
                                Def.DbBsTb + 
                        " WHERE " +
                    Def.DbTrTb + cBSourceNode.SelectedItem + "." + Def.DbTableIdName + "=" +
                    Def.DbBsTb + "." + Def.DbTableIdName + lastAnd;
            try {
                Def.Db.ExecuteNonQuery(sql);
            } catch (Exception ex) {
                FE.Show("SQL Error, please check the condition " + ex.Message, "Warning", ex.StackTrace);
                return false;

            }
            rowsAffected = Def.Db.GetNumber("SELECT COUNT(*) FROM TMP");
            if (rowsAffected == 0)
                return false;
            else
                return true;
        }

        private void btPreview_Click(object sender, EventArgs e) {

            bool ok = false;
            try {
                ok=createTmpTable();
            } catch (Exception ex) {
               FE.Show("Error executing query " + ex.Message + " please check whether the query is correct", "Warning", ex.StackTrace);
                return;
            }
            if (ok) {
                FrmReferenceTableData f = new FrmReferenceTableData("TMP");
                f.ShowDialog();
            } else {
                MessageBox.Show("No items will be moved", "Warning");
                return;
            }
        }

        private void btTransfer_Click(object sender, EventArgs e) {
            double rowsAffected = -1;
            Node source = null, destination = null;
            bool ok=false;

            foreach(Node nd in Def.Tree.NodeLst){
                if (nd.Id.ToString() == cBSourceNode.SelectedItem.ToString())
                    source = nd;
                if (nd.Id.ToString() == cBDestinationNode.SelectedItem.ToString())
                    destination = nd;
            }
            
            try{
                ok=createTmpTable();
            }catch(Exception ex){
                FE.Show("Error executing query " + ex.Message + " please check whether the query is correct", "Warning", ex.StackTrace);
                return;
            }

            if (ok) {
                Def.Db.ExecuteNonQuery("DROP TABLE TMP2", true);
                string sql =
                @"CREATE TABLE TMP2 AS " +
                 "SELECT " + Def.DbTableIdName + " FROM TMP " +
                 "WHERE NOT EXISTS("+
                 "SELECT 1 FROM " + Def.DbTrTb + cBDestinationNode.SelectedItem + " where " + Def.DbTableIdName + " = TMP." + Def.DbTableIdName + ") " +
                 "UNION " +
                    "SELECT * FROM " + Def.DbTrTb + cBDestinationNode.SelectedItem;
                try {
                    Def.Db.ExecuteNonQuery(sql);
                } catch (Exception ex) {
                    FE.Show(ex);
                    return;
                }
                rowsAffected = Def.Db.GetNumber("SELECT COUNT(*) FROM TMP2");
                Def.Db.ExecuteNonQuery("DROP TABLE " + Def.DbTrTb + cBDestinationNode.SelectedItem);
                Def.Db.ExecuteNonQuery("ALTER TABLE TMP2 RENAME TO " + Def.DbTrTb + cBDestinationNode.SelectedItem);
                sql = "DELETE FROM " + Def.DbTrTb + cBSourceNode.SelectedItem +
                    " WHERE EXISTS(SELECT 1 FROM TMP WHERE " + Def.DbTableIdName + " = " + Def.DbTrTb + cBSourceNode.SelectedItem + "." + Def.DbTableIdName + ")";
                Def.Db.ExecuteNonQuery(sql);
                Def.Db.ExecuteNonQuery("DROP TABLE TMP", true);
                if(rowsAffected==0)
                    MessageBox.Show("No items will be moved", "Warning");
                else
                    MessageBox.Show("Data transported succesfully", "OK");
                source.PredictorLst.Clear();
                destination.PredictorLst.Clear();
                Def.Db.PredictorsFill(source);
                Def.Db.PredictorsFill(destination);
                source.Table = new TableMap(source);
                destination.Table = new TableMap(destination);
                if (Def.Schema.Target.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Categorical) {
                    NodeTargetCategorical nt = (NodeTargetCategorical)source;
                    nt.TargetClasses = new TargetCategoricalClass(Def.Schema.Target, nt);
                    nt = (NodeTargetCategorical) destination;
                    nt.TargetClasses = new TargetCategoricalClass(Def.Schema.Target, nt);
                }
                source.DisplayUpdate();
                destination.DisplayUpdate();

            } else {
                MessageBox.Show("No items will be moved", "Warning");
                return;
            }
        }

        private void label3_Click(object sender, EventArgs e) {

        }
    }
}