using System;
using System.Collections;
using System.Data;


namespace Spartacus
{
	/// <summary>
	/// Summary description for Cluster.
	/// </summary>
	public class Cluster_
	{

		public Node NodeParent;

		public Cluster_(VirtualTable data, Node nodeParent)
		{
			//this.DS=data;
			
			
			NodeParent=nodeParent;

			//The root parameters and cases will be set during the Fill DataSet
			if(Tree.Root==null) //It is OK! (workaround)
				return;

			//Set the Attributes

			//outdated!!! blocks changing the type of the variable
//			DataRow row = FrmMain.SFrmMain.DS.Tables[0].Rows[0];
//			double val;
//			foreach(DataColumn dc in FrmMain.SFrmMain.DS.Tables[0].Columns)
//			{
//				try
//				{
//					val=Double.Parse(row[dc, DataRowVersion.Original].ToString());	
//					att = new Attribute(dc.ColumnName.ToString(), Attribute.TypeEnum.Numerical);
//					AttributeL.Add(att);
//				}
//				catch
//				{
//					att = new Attribute(dc.ColumnName.ToString(), Attribute.TypeEnum.Categorical);
//					AttributeL.Add(att);
//				}
//			}
//
//		
//			//Set the Cases
//			for(int x=0; x < AttributeQty; ++x)
//			{
//				att = (Attribute) AttributeL[x];
//				for(int y=0; y < RowQty; ++y)
//				{
//					att.AddCase(DS[x,y]);
//				}
//			}	

		}



		

	}
}
