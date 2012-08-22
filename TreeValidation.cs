using System;
using System.Collections.Generic;
using System.Text;

namespace Spartacus {
    public class TreeValidation {

        public TreeValidation() {
            NodeFill(Def.Tree.Root);
        }

        public void NodeFill(Node node) {
            if (node.DescendentLst.Count == 0)
                return;
            Node nl = node.DescendentLst[0], nr = node.DescendentLst[1];
            string sql;
            int nextNodeId;

            Def.Db.TableDropIfExists(Def.DbTsTb + nl.Id);
            Def.Db.TableDropIfExists(Def.DbTsTb + nr.Id);
            Def.Db.ValidationTableIndexDropIfExists(nl.Id);
            Def.Db.ValidationTableIndexDropIfExists(nr.Id);

            if (node.SplitVariable.Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Continuous) {
                sql =
                @"CREATE TABLE " +
                    Def.DbTsTb + nl.Id +
                    " AS " +
                    "SELECT " + Def.DbTsTb + node.Id + "." + Def.DbTableIdName + " FROM " +
                        Def.DbTsTb + node.Id + ", " + Def.DbBsTb +
                    " WHERE " +
                    Def.DbTsTb + node.Id + "." + Def.DbTableIdName + "=" + Def.DbBsTb + "." + Def.DbTableIdName +
                    " AND " + node.SplitVariable.Variable.Name + " <= " + node.SplitValue;
                Def.Db.ExecuteNonQuery(sql);
                Def.Db.ValidationTableIndexCreate(nl.Id);

                sql =
                @"CREATE TABLE " +
                    Def.DbTsTb + nr.Id +
                    " AS " +
                    "SELECT " + Def.DbTsTb + node.Id + "." + Def.DbTableIdName + " FROM " +
                        Def.DbTsTb + node.Id + ", " + Def.DbBsTb +
                    " WHERE " +
                    Def.DbTsTb + node.Id + "." + Def.DbTableIdName + "=" + Def.DbBsTb + "." + Def.DbTableIdName +
                    " AND " + node.SplitVariable.Variable.Name + " > " + node.SplitValue;
                Def.Db.ExecuteNonQuery(sql);
                Def.Db.ValidationTableIndexCreate(nr.Id);

            } else { //IF(nd.SplitVariable.Variable.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Categorical)
                string vals = "";
                nextNodeId = nl.Id;
                //LEFT NODE

                List<string> caseLst = node.SplitVariable.ChildrenGroups.ValueGroupLst[0];
                for (int i = 0; i < caseLst.Count; ++i) {
                    vals += node.SplitVariable.Variable.Name + "='" + caseLst[i] + "' ";
                    if (i < (caseLst.Count - 1)) {
                        vals += " or ";
                    } else
                        vals += ")";
                }
                sql =
                @"CREATE TABLE " +
                    Def.DbTsTb + nextNodeId +
                    " AS " +
                    "SELECT " + Def.DbTsTb + node.Id + "." + Def.DbTableIdName + " FROM " +
                        Def.DbTsTb + node.Id + ", " + Def.DbBsTb +
                    " WHERE (" +
                    Def.DbTsTb + node.Id + "." + Def.DbTableIdName + "=" + Def.DbBsTb + "." + Def.DbTableIdName +
                    ") AND (" + vals;
                Def.Db.ExecuteNonQuery(sql);
                Def.Db.ValidationTableIndexCreate(nextNodeId);

                //RIGHT NODE
                vals = "";
                nextNodeId = nr.Id;
                caseLst = node.SplitVariable.ChildrenGroups.ValueGroupLst[1];
                for (int i = 0; i < caseLst.Count; ++i) {
                    vals += node.SplitVariable.Variable.Name + "='" + caseLst[i] + "' ";
                    if (i < (caseLst.Count - 1)) {
                        vals += " or ";
                    } else
                        vals += ")";
                }
                sql =
                @"CREATE TABLE " +
                    Def.DbTsTb + nextNodeId +
                    " AS " +
                    "SELECT " + Def.DbTsTb + node.Id + "." + Def.DbTableIdName + " FROM " +
                        Def.DbTsTb + node.Id + ", " + Def.DbBsTb +
                    " WHERE (" +
                    Def.DbTsTb + node.Id + "." + Def.DbTableIdName + "=" + Def.DbBsTb + "." + Def.DbTableIdName +
                    ") AND (" + vals;
                Def.Db.ExecuteNonQuery(sql);
                Def.Db.ValidationTableIndexCreate(nextNodeId);
            }
            NodeFill(nl);
            NodeFill(nr);
        }
    }
}
