using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Spartacus
{
	/// <summary>
	/// Summary description for PhysicalTable.
	/// </summary>
	public class PhysicalTable
	{
		public PhysicalColumn[] Col;
		private int colCount, rowCount;

		public PhysicalTable(int _colCount, int _rowCount)
		{
			colCount = _colCount; rowCount = _rowCount;
			Col = new PhysicalColumn[colCount];
		}

		public void AddCol(int colIndex, string colName, int rowCount, PhysicalColumn.DataTypeEnum dataType)
		{
			Col[colIndex] = new PhysicalColumn(colName, rowCount, dataType);
		}
			

		public int ColCount{get{ return colCount;}}
		public int RowCount{get{ return rowCount;}}

		public string GetText(int x, int y)
		{
			return Col[x].Row.GetText(y);
		}

		public double GetNumber(int x, int y)
		{
			return Col[x].Row.GetNumber(y);
		}

		public object this[int x, int y]
		{
			set
			{
				bool b;
				char c;
				DateTime dt;

				if(value is double) 
					Col[x].Row.SetNumber((double) value, y);
				else
				if(value is int) 
					Col[x].Row.SetNumber((double) (int) value, y);
				else
				if(value is string) 
					Col[x].Row.SetText((string) value, y);
				else
				if(value is bool)
				{
					b = (bool) value;
					Col[x].Row.SetText(b.ToString(), y);
				}
				else
				if(value is char)
				{
					c = (char) value;
					Col[x].Row.SetText(c.ToString(), y);
				}
				else
				if(value is char)
				{
					c = (char) value;
					Col[x].Row.SetText(c.ToString(), y);
				}
				else
				if(value is DateTime)
				{
					dt = (DateTime) value;
					Col[x].Row.SetText(dt.ToString(), y);
				}
//				else
//				if(value is DBNull)
//				{
//					Col[x].Row.SetNumber(-999999, y);
//				}
				else
				MessageBox.Show("This value '" + value + "' (" + x + ", " + y + ") has type: " + value.GetType(), "PhysicalTable.cs");

			}
		}
	}

	public class PhysicalColumn
	{
		public string Name;
		public DataTypeEnum DataType;
		public PhysicalRow Row;

		public PhysicalColumn(string colName, int rowCount, PhysicalColumn.DataTypeEnum dataType)
		{
			Name = colName;
			DataType = dataType;
			Row = new PhysicalRow(rowCount, DataType); 			
 		}

		public enum DataTypeEnum : byte
		{
			Number,
			Text,
		}
	}

	public class PhysicalRow
	{
		public string[] TextData;
		public double[] NumericData;
		
		public PhysicalRow(int rowCount, PhysicalColumn.DataTypeEnum dataType)
		{
			if(dataType == PhysicalColumn.DataTypeEnum.Number)
				NumericData = new double[rowCount];
			else
				TextData = new string[rowCount];
		}
		public string GetText(int y) {return TextData[y];}
		public double GetNumber(int y) {return NumericData[y];}
		
		public void SetText(string val, int y) {TextData[y]=val;}
		public void SetNumber(double val, int y) {NumericData[y]=val;}


	}



}
