using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Spartacus
{
	/// <summary>
	/// Summary description for VirtualDataSet.
	/// </summary>
	public class VirtualTable
	{
		private static PhysicalTable source;
		private DynArrInt row;
		private int orderedAscBy=-1; //Index of the column that is sorted
		private int orderedDescBy=-1;

		public VirtualTable(PhysicalTable _source)
		{
			int i;
			int rc = _source.RowCount;

			source = _source;
			row = new DynArrInt(rc);
			for(i=0; i < rc; ++i)
				row.Add(i);
		}

		public VirtualTable(DynArrInt subRows)
		{
			row = subRows;
		}

		public PhysicalColumn.DataTypeEnum DataType(int col)
		{
			return source.Col[col].DataType;
		}

		public int this[int y] //Returns the source line
		{
			get	{ return row[y]; }
			set { row[y] = value;} 
		}

		public object this[int x, int y]
		{
			get
			{
				
				if(x < 0 || x > ColCount - 1 || y < 0 || y > RowCount - 1)
					throw new IndexOutOfRangeException("The index out of bounds in VT this[int, int] x=" + x + " y=" + y);
				if(source.Col[x].DataType == PhysicalColumn.DataTypeEnum.Number)
				{
					return (object)  source.GetNumber(x, row[y]);
				}
				else
					return (object)  source.GetText(x, row[y]);
			}
		}

		public string GetText(int x, int y)
		{
			if(x < 0 || x > ColCount - 1 || y < 0 || y > RowCount - 1)
			{
				MessageBox.Show("The index out of bounds in GetText x=" + x + " y=" + y + " returning -1", "Debug");
				return "-1";
			}	
			return source.GetText(x, row[y]);
		}

		public double GetNumber(int x, int y)
		{
			if(x < 0 || x > ColCount - 1 || y < 0 || y > RowCount - 1)
			{	
				MessageBox.Show("The index out of bounds in VT GetNumber x=" + x + " y=" + y + " returning -1", "Debug");
				return -1;
			}
			return source.GetNumber(x, row[y]);
		}

		public string ColName(int x)
		{
			return source.Col[x].Name;
		}

		public void OrderAscBy(int col)
		{
			if(orderedAscBy == col) //It is already ordered, return
				return;
			orderedAscBy = col;
			orderedDescBy=-1;
			sort(this, col);
		}

		public int ColIndex(string colS)
		{
			for(int x=0; x < ColCount; ++x)
				if(ColName(x) == colS)
					return x;
			MessageBox.Show("Couldn't find the column", "Error");
			return -1;
		}

		public VirtualTable SubSet(int col, string val)
		{
						
			DynArrInt rowsSelected = new DynArrInt();
			for(int y=0; y <  RowCount; ++y)
			{
				if(this[col, y].ToString().CompareTo(val) == 0)
					rowsSelected.Add(row[y]);	
			}
			VirtualTable subVT = new VirtualTable(rowsSelected);
			return subVT;
		}


		public VirtualTable SubSetUpTo(int col, double threshold)
		{
			DynArrInt rowsSelected = new DynArrInt();
			for(int y=0; y <  RowCount; ++y)
			{
				if(GetNumber(col, y) <= threshold) 
				try
				{
						rowsSelected.Add(row[y]);
				}
				catch(Exception e)
				{
						MessageBox.Show(e.Message);
				}
			}
			VirtualTable subVT = new VirtualTable(rowsSelected);
			return subVT;
		}

		public VirtualTable SubSetHigher(int col, double threshold)
		{
			DynArrInt rowsSelected = new DynArrInt();
			for(int y=0; y <  RowCount; ++y)
			{
				if(GetNumber(col, y) > threshold) 
					try
					{
						rowsSelected.Add(row[y]);
					}
					catch(Exception e)
					{
						MessageBox.Show(e.Message);
					}
			}
			VirtualTable subVT = new VirtualTable(rowsSelected);
			return subVT;
		}


		public int ColCount	{get{return source.ColCount;}}
		public int RowCount	{get{return row.Count;}}

		// QuickSort  implementation for VirtualTable
		//QuickSort (szContents, 0, RowCount - 1);
	

		public static void sort(VirtualTable a, int col)
		{
			QuickSortDouble(a, 0, a.RowCount - 1, col);
		}

		public static void QuickSortDouble(VirtualTable a, int lo0, int hi0, int col)
		{
			int lo = lo0;
			int hi = hi0;
			double mid;
	
			if ( hi0 > lo0)
			{
				/* Arbitrarily establishing partition element as the midpoint of
				* the array.
				*/
				mid = a.GetNumber(col, (int) ( lo0 + hi0 ) / 2);
	
				// loop through the array until indices cross
				while( lo <= hi )
				{
					/* find the first element that is greater than or equal to 
					* the partition element starting from the left Index.
					*/
					while( ( lo < hi0 ) && ( a.GetNumber(col, lo) < mid ) )
						++lo;

					/* find an element that is smaller than or equal to 
					* the partition element starting from the right Index.
					*/
					while( ( hi > lo0 ) && ( a.GetNumber(col, hi) > mid ) )
						--hi;

					// if the indexes have not crossed, swap
					if( lo <= hi ) 
					{
						swap(a, lo, hi);

						++lo;
						--hi;
					}
				}

				/* If the right index has not reached the left side of array
				* must now sort the left partition.
				*/
				if( lo0 < hi )
					QuickSortDouble( a, lo0, hi, col );

				/* If the left index has not reached the right side of array
				* must now sort the right partition.
				*/
				if( lo < hi0 )
					QuickSortDouble( a, lo, hi0, col );
			}
		}

		public static void swap(VirtualTable a, int i, int j)
		{
			int T;
			
			if(i==-1)
				MessageBox.Show("Index = -1", "VirtualTable.cs");
			
			T = a[i]; 
			a[i] = a[j];
			a[j] = T;
		}

	}

}
