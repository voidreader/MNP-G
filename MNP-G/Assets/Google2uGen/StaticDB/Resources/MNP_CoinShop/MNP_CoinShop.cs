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
	public class MNP_CoinShopRow : IGoogle2uRow
	{
		public int _gold;
		public int _gem;
		public MNP_CoinShopRow(string __id, string __gold, string __gem) 
		{
			{
			int res;
				if(int.TryParse(__gold, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_gold = res;
				else
					Debug.LogError("Failed To Convert _gold string: "+ __gold +" to int");
			}
			{
			int res;
				if(int.TryParse(__gem, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_gem = res;
				else
					Debug.LogError("Failed To Convert _gem string: "+ __gem +" to int");
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
					ret = _gold.ToString();
					break;
				case 1:
					ret = _gem.ToString();
					break;
			}

			return ret;
		}

		public string GetStringData( string colID )
		{
			var ret = System.String.Empty;
			switch( colID )
			{
				case "gold":
					ret = _gold.ToString();
					break;
				case "gem":
					ret = _gem.ToString();
					break;
			}

			return ret;
		}
		public override string ToString()
		{
			string ret = System.String.Empty;
			ret += "{" + "gold" + " : " + _gold.ToString() + "} ";
			ret += "{" + "gem" + " : " + _gem.ToString() + "} ";
			return ret;
		}
	}
	public sealed class MNP_CoinShop : IGoogle2uDB
	{
		public enum rowIds {
			Product0, Product1, Product2, Product3, Product4
		};
		public string [] rowNames = {
			"Product0", "Product1", "Product2", "Product3", "Product4"
		};
		public System.Collections.Generic.List<MNP_CoinShopRow> Rows = new System.Collections.Generic.List<MNP_CoinShopRow>();

		public static MNP_CoinShop Instance
		{
			get { return NestedMNP_CoinShop.instance; }
		}

		private class NestedMNP_CoinShop
		{
			static NestedMNP_CoinShop() { }
			internal static readonly MNP_CoinShop instance = new MNP_CoinShop();
		}

		private MNP_CoinShop()
		{
			Rows.Add( new MNP_CoinShopRow("Product0", "30000", "300"));
			Rows.Add( new MNP_CoinShopRow("Product1", "53000", "500"));
			Rows.Add( new MNP_CoinShopRow("Product2", "110000", "1000"));
			Rows.Add( new MNP_CoinShopRow("Product3", "340000", "3000"));
			Rows.Add( new MNP_CoinShopRow("Product4", "580000", "5000"));
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
		public MNP_CoinShopRow GetRow(rowIds in_RowID)
		{
			MNP_CoinShopRow ret = null;
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
		public MNP_CoinShopRow GetRow(string in_RowString)
		{
			MNP_CoinShopRow ret = null;
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
