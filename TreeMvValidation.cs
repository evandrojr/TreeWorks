using System;
using System.Collections.Generic;
using System.Text;

namespace Spartacus {
    class TreeMvValidation {

        public TreeMvValidation() {
            NodeFill(Def.Tree.Root);
        }

        public void NodeFill(Node node) {
            if (node.DescendentLst.Count == 0)
                return;
            Node nl = node.DescendentLst[0], nr = node.DescendentLst[1];
            string sql;
            double varSum;
 

            Def.Db.ValidationTableIndexDropIfExists(nl.Id);
            Def.Db.TableDropIfExists(Def.DbTsTb + nl.Id);
            Def.Db.ValidationTableIndexDropIfExists(nr.Id);
            Def.Db.TableDropIfExists(Def.DbTsTb + nr.Id);

            //Left            
            sql =
            @"CREATE TABLE " +
                Def.DbTsTb + nl.Id + "(" + Def.DbTableIdName + " integer NOT NULL)";
            Def.Db.ExecuteNonQuery(sql);

            //Right node
            sql =
            @"CREATE TABLE " +
                Def.DbTsTb + nr.Id + "(" + Def.DbTableIdName + " integer NOT NULL)";
            Def.Db.ExecuteNonQuery(sql);

            
            List<string> insert = new List<string>();
            node.MvTsTb.RowCount = (int)Def.Db.GetNumber("Select count(*) from " + Def.DbTsTb + node.Id);
            node.MvTsTb.DataFill();
            for (int y = 0; y < node.MvTsTb.RowCount; ++y) {
                varSum = 0;
                for (int i = 0; i < node.PredMvTsLst.Count; ++i) {
                    varSum += node.PredMvLst[i].Coef * node.PredMvTsLst[i].X(y);
                }
                if ((varSum + node.C) <= 0) {
                    insert.Add(@"insert into " + Def.DbTsTb + (nl.Id) + " values (" + node.MvTsTb.Data.ID[y] + ")");
                } else {
                    insert.Add(@"insert into " + Def.DbTsTb + (nr.Id) + " values (" + node.MvTsTb.Data.ID[y] + ")");
                }
            }
            node.MvTb.DataEmpty();
            Def.Db.NonQueryTransaction(insert);
            Def.Db.ValidationTableIndexCreate(nr.Id);
            Def.Db.ValidationTableIndexCreate(nl.Id);
            NodeFill(nl);
            NodeFill(nr);
        }
    }
}
