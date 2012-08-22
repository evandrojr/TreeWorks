#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.Common;
using System.Data.SQLite;

#endregion

namespace SpartacusSQL {
    partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            try{

            SQLiteConnection cnn = new SQLiteConnection();
            cnn.ConnectionString = "Data Source=test2.db";
            cnn.Open();

            SQLiteCommand cmd = new SQLiteCommand(cnn);

            cmd.CommandText = "drop TABLE customers";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "CREATE TABLE customers(ID integer primary key autoincrement, name varchar(50))";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "drop TABLE TestCase";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "CREATE TABLE TestCase (ID integer primary key autoincrement, Field1 Integer, Field2 Float, Field3 VARCHAR(50), Field4 CHAR(10), Field5 DateTime, Field6 Image)";
            cmd.ExecuteNonQuery();

            //cmd.CommandText = "INSERT INTO TestCase(Field1, Field2, Field3, Field4, Field5) VALUES(?,?,?,?,?)";
            //DbParameter Field1 = cmd.CreateParameter();
            //DbParameter Field2 = cmd.CreateParameter();
            //DbParameter Field3 = cmd.CreateParameter();
            //DbParameter Field4 = cmd.CreateParameter();
            //DbParameter Field5 = cmd.CreateParameter();

            //Field1.DbType = System.Data.DbType.Int32;

            double start = DateTime.Now.Ticks;
            System.Console.WriteLine("start");
            double end;


            DbTransaction dbTrans = cnn.BeginTransaction();

            for (int i = 0; i < 1000000; ++i) {
                cmd.CommandText = "insert into customers (name) values ('Ze mane')";
                cmd.ExecuteNonQuery();
            }
            dbTrans.Commit();

            end = DateTime.Now.Ticks - start;
            System.Console.WriteLine("Finished " + (end / 10000000));
            start = DateTime.Now.Ticks;

            dbTrans = cnn.BeginTransaction();
            SQLiteParameter p = new SQLiteParameter();
            cmd.CommandText = "insert into customers (name) values (?)";
            cmd.Parameters.Add(p);
            for (int i = 0; i < 1000000; ++i) {
                p.Value = "Ze Mane2";
                cmd.ExecuteNonQuery();
            }
            dbTrans.Commit();

            end = DateTime.Now.Ticks - start;
            System.Console.WriteLine("Finished " + (end / 10000000));
        }
    }
}