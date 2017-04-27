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
	public class MNP_PowerUpgradeCostRow : IGoogle2uRow
	{
		public int _cost;
		public MNP_PowerUpgradeCostRow(string __id, string __cost) 
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
	public sealed class MNP_PowerUpgradeCost : IGoogle2uDB
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
		public System.Collections.Generic.List<MNP_PowerUpgradeCostRow> Rows = new System.Collections.Generic.List<MNP_PowerUpgradeCostRow>();

		public static MNP_PowerUpgradeCost Instance
		{
			get { return NestedMNP_PowerUpgradeCost.instance; }
		}

		private class NestedMNP_PowerUpgradeCost
		{
			static NestedMNP_PowerUpgradeCost() { }
			internal static readonly MNP_PowerUpgradeCost instance = new MNP_PowerUpgradeCost();
		}

		private MNP_PowerUpgradeCost()
		{
			Rows.Add( new MNP_PowerUpgradeCostRow("Level1", "3000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level2", "5000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level3", "10000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level4", "15000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level5", "20000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level6", "25000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level7", "30000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level8", "40000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level9", "50000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level10", "60000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level11", "14500"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level12", "16500"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level13", "18500"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level14", "20500"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level15", "22500"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level16", "5000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level17", "5000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level18", "5000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level19", "5000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level20", "10000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level21", "10000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level22", "10000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level23", "10000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level24", "10000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level25", "15000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level26", "15000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level27", "15000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level28", "15000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level29", "15000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level30", "20000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level31", "20000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level32", "20000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level33", "20000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level34", "20000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level35", "30000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level36", "30000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level37", "30000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level38", "30000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level39", "30000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level40", "50000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level41", "50000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level42", "50000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level43", "50000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level44", "50000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level45", "75000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level46", "75000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level47", "75000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level48", "75000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level49", "75000"));
			Rows.Add( new MNP_PowerUpgradeCostRow("Level50", "80000"));
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
		public MNP_PowerUpgradeCostRow GetRow(rowIds in_RowID)
		{
			MNP_PowerUpgradeCostRow ret = null;
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
		public MNP_PowerUpgradeCostRow GetRow(string in_RowString)
		{
			MNP_PowerUpgradeCostRow ret = null;
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
