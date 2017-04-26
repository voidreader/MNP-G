//----------------------------------------------
//    Google2u: Google Doc Unity integration
//         Copyright © 2015 Litteratus
//
//        This file has been auto-generated
//              Do not manually edit
//----------------------------------------------

using UnityEngine;
using System.Globalization;

namespace Google2u
{
	[System.Serializable]
	public class MNP_NekoUpgradeCostRow : IGoogle2uRow
	{
		public int _cost;
		public MNP_NekoUpgradeCostRow(string __id, string __cost) 
		{
			{
			int res;
				if(int.TryParse(__cost, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_cost = res;
				else
					Debug.LogError("Failed To Convert _cost string: "+ __cost +" to int");
			}
		}

		public int Length { get { return 1; } }

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
					ret = _cost.ToString();
					break;
			}

			return ret;
		}

		public string GetStringData( string colID )
		{
			var ret = System.String.Empty;
			switch( colID )
			{
				case "cost":
					ret = _cost.ToString();
					break;
			}

			return ret;
		}
		public override string ToString()
		{
			string ret = System.String.Empty;
			ret += "{" + "cost" + " : " + _cost.ToString() + "} ";
			return ret;
		}
	}
	public sealed class MNP_NekoUpgradeCost : IGoogle2uDB
	{
		public enum rowIds {
			Level1, Level2, Level3, Level4, Level5, Level6, Level7, Level8, Level9, Level10, Level11, Level12, Level13, Level14, Level15, Level16, Level17, Level18
			, Level19, Level20, Level21, Level22, Level23, Level24, Level25, Level26, Level27, Level28, Level29, Level30, Level31, Level32, Level33, Level34, Level35, Level36, Level37, Level38
			, Level39, Level40, Level41, Level42, Level43, Level44, Level45, Level46, Level47, Level48, Level49, Level50
		};
		public string [] rowNames = {
			"Level1", "Level2", "Level3", "Level4", "Level5", "Level6", "Level7", "Level8", "Level9", "Level10", "Level11", "Level12", "Level13", "Level14", "Level15", "Level16", "Level17", "Level18"
			, "Level19", "Level20", "Level21", "Level22", "Level23", "Level24", "Level25", "Level26", "Level27", "Level28", "Level29", "Level30", "Level31", "Level32", "Level33", "Level34", "Level35", "Level36", "Level37", "Level38"
			, "Level39", "Level40", "Level41", "Level42", "Level43", "Level44", "Level45", "Level46", "Level47", "Level48", "Level49", "Level50"
		};
		public System.Collections.Generic.List<MNP_NekoUpgradeCostRow> Rows = new System.Collections.Generic.List<MNP_NekoUpgradeCostRow>();

		public static MNP_NekoUpgradeCost Instance
		{
			get { return NestedMNP_NekoUpgradeCost.instance; }
		}

		private class NestedMNP_NekoUpgradeCost
		{
			static NestedMNP_NekoUpgradeCost() { }
			internal static readonly MNP_NekoUpgradeCost instance = new MNP_NekoUpgradeCost();
		}

		private MNP_NekoUpgradeCost()
		{
			Rows.Add( new MNP_NekoUpgradeCostRow("Level1", "500"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level2", "500"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level3", "500"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level4", "500"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level5", "1000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level6", "1000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level7", "1000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level8", "1000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level9", "1000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level10", "2500"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level11", "2500"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level12", "2500"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level13", "2500"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level14", "2500"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level15", "5000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level16", "5000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level17", "5000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level18", "5000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level19", "5000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level20", "10000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level21", "10000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level22", "10000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level23", "10000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level24", "10000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level25", "15000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level26", "15000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level27", "15000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level28", "15000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level29", "15000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level30", "20000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level31", "20000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level32", "20000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level33", "20000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level34", "20000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level35", "30000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level36", "30000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level37", "30000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level38", "30000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level39", "30000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level40", "50000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level41", "50000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level42", "50000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level43", "50000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level44", "50000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level45", "75000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level46", "75000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level47", "75000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level48", "75000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level49", "75000"));
			Rows.Add( new MNP_NekoUpgradeCostRow("Level50", "80000"));
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
		public MNP_NekoUpgradeCostRow GetRow(rowIds in_RowID)
		{
			MNP_NekoUpgradeCostRow ret = null;
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
		public MNP_NekoUpgradeCostRow GetRow(string in_RowString)
		{
			MNP_NekoUpgradeCostRow ret = null;
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
