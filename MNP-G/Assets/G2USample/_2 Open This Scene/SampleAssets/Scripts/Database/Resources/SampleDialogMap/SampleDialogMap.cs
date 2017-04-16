//----------------------------------------------
//    Google2u: Google Doc Unity integration
//         Copyright © 2015 Litteratus
//
//        This file has been auto-generated
//              Do not manually edit
//----------------------------------------------

using UnityEngine;

namespace Google2u
{
	[System.Serializable]
	public class SampleDialogMapRow : IGoogle2uRow
	{
		public System.Collections.Generic.List<string> _OnDamaged = new System.Collections.Generic.List<string>();
		public System.Collections.Generic.List<string> _OnDestroyed = new System.Collections.Generic.List<string>();
		public SampleDialogMapRow(string __GOOGLEFU_ID, string __OnDamaged, string __OnDestroyed) 
		{
			{
				string []result = __OnDamaged.Split("|".ToCharArray(),System.StringSplitOptions.RemoveEmptyEntries);
				for(int i = 0; i < result.Length; i++)
				{
					_OnDamaged.Add( result[i].Trim() );
				}
			}
			{
				string []result = __OnDestroyed.Split("|".ToCharArray(),System.StringSplitOptions.RemoveEmptyEntries);
				for(int i = 0; i < result.Length; i++)
				{
					_OnDestroyed.Add( result[i].Trim() );
				}
			}
		}

		public int Length { get { return 2; } }

		public string this[int i]
		{
		    get
		    {
		        return GetStringDataByIndex(i);
		    }
		}

		public string GetStringDataByIndex( int index )
		{
			string ret = System.String.Empty;
			switch( index )
			{
				case 0:
					ret = _OnDamaged.ToString();
					break;
				case 1:
					ret = _OnDestroyed.ToString();
					break;
			}

			return ret;
		}

		public string GetStringData( string colID )
		{
			var ret = System.String.Empty;
			switch( colID.ToLower() )
			{
				case "OnDamaged":
					ret = _OnDamaged.ToString();
					break;
				case "OnDestroyed":
					ret = _OnDestroyed.ToString();
					break;
			}

			return ret;
		}
		public override string ToString()
		{
			string ret = System.String.Empty;
			ret += "{" + "OnDamaged" + " : " + _OnDamaged.ToString() + "} ";
			ret += "{" + "OnDestroyed" + " : " + _OnDestroyed.ToString() + "} ";
			return ret;
		}
	}
	public class SampleDialogMap :  Google2uComponentBase, IGoogle2uDB
	{
		public enum rowIds {
			DLG_MINEBOT, DLG_BUZZERBOT
		};
		public string [] rowNames = {
			"DLG_MINEBOT", "DLG_BUZZERBOT"
		};
		public System.Collections.Generic.List<SampleDialogMapRow> Rows = new System.Collections.Generic.List<SampleDialogMapRow>();
		public override void AddRowGeneric (System.Collections.Generic.List<string> input)
		{
			Rows.Add(new SampleDialogMapRow(input[0],input[1],input[2]));
		}
		public override void Clear ()
		{
			Rows.Clear();
		}
		public IGoogle2uRow GetGenRow(string in_RowString)
		{
			IGoogle2uRow ret = null;
			try
			{
				ret = Rows[(int)System.Enum.Parse(typeof(rowIds), in_RowString)];
			}
			catch(System.ArgumentException) {
				Debug.LogError( in_RowString + " is not a member of the rowIds enumeration.");
			}
			return ret;
		}
		public IGoogle2uRow GetGenRow(rowIds in_RowID)
		{
			IGoogle2uRow ret = null;
			try
			{
				ret = Rows[(int)in_RowID];
			}
			catch( System.Collections.Generic.KeyNotFoundException ex )
			{
				Debug.LogError( in_RowID + " not found: " + ex.Message );
			}
			return ret;
		}
		public SampleDialogMapRow GetRow(rowIds in_RowID)
		{
			SampleDialogMapRow ret = null;
			try
			{
				ret = Rows[(int)in_RowID];
			}
			catch( System.Collections.Generic.KeyNotFoundException ex )
			{
				Debug.LogError( in_RowID + " not found: " + ex.Message );
			}
			return ret;
		}
		public SampleDialogMapRow GetRow(string in_RowString)
		{
			SampleDialogMapRow ret = null;
			try
			{
				ret = Rows[(int)System.Enum.Parse(typeof(rowIds), in_RowString)];
			}
			catch(System.ArgumentException) {
				Debug.LogError( in_RowString + " is not a member of the rowIds enumeration.");
			}
			return ret;
		}

	}

}
