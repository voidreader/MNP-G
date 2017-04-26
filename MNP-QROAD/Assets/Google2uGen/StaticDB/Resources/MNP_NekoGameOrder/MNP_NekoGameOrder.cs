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
	public class MNP_NekoGameOrderRow : IGoogle2uRow
	{
		public int _neko_id;
		public int _boss_id;
		public int _HP;
		public int _boss_HP;
		public int _coinMin;
		public int _coinMax;
		public int _killScore;
		public MNP_NekoGameOrderRow(string __id, string __neko_id, string __boss_id, string __HP, string __boss_HP, string __coinMin, string __coinMax, string __killScore) 
		{
			{
			int res;
				if(int.TryParse(__neko_id, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_neko_id = res;
				else
					Debug.LogError("Failed To Convert _neko_id string: "+ __neko_id +" to int");
			}
			{
			int res;
				if(int.TryParse(__boss_id, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_boss_id = res;
				else
					Debug.LogError("Failed To Convert _boss_id string: "+ __boss_id +" to int");
			}
			{
			int res;
				if(int.TryParse(__HP, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_HP = res;
				else
					Debug.LogError("Failed To Convert _HP string: "+ __HP +" to int");
			}
			{
			int res;
				if(int.TryParse(__boss_HP, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_boss_HP = res;
				else
					Debug.LogError("Failed To Convert _boss_HP string: "+ __boss_HP +" to int");
			}
			{
			int res;
				if(int.TryParse(__coinMin, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_coinMin = res;
				else
					Debug.LogError("Failed To Convert _coinMin string: "+ __coinMin +" to int");
			}
			{
			int res;
				if(int.TryParse(__coinMax, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_coinMax = res;
				else
					Debug.LogError("Failed To Convert _coinMax string: "+ __coinMax +" to int");
			}
			{
			int res;
				if(int.TryParse(__killScore, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_killScore = res;
				else
					Debug.LogError("Failed To Convert _killScore string: "+ __killScore +" to int");
			}
		}

		public int Length { get { return 7; } }

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
					ret = _neko_id.ToString();
					break;
				case 1:
					ret = _boss_id.ToString();
					break;
				case 2:
					ret = _HP.ToString();
					break;
				case 3:
					ret = _boss_HP.ToString();
					break;
				case 4:
					ret = _coinMin.ToString();
					break;
				case 5:
					ret = _coinMax.ToString();
					break;
				case 6:
					ret = _killScore.ToString();
					break;
			}

			return ret;
		}

		public string GetStringData( string colID )
		{
			var ret = System.String.Empty;
			switch( colID )
			{
				case "neko_id":
					ret = _neko_id.ToString();
					break;
				case "boss_id":
					ret = _boss_id.ToString();
					break;
				case "HP":
					ret = _HP.ToString();
					break;
				case "boss_HP":
					ret = _boss_HP.ToString();
					break;
				case "coinMin":
					ret = _coinMin.ToString();
					break;
				case "coinMax":
					ret = _coinMax.ToString();
					break;
				case "killScore":
					ret = _killScore.ToString();
					break;
			}

			return ret;
		}
		public override string ToString()
		{
			string ret = System.String.Empty;
			ret += "{" + "neko_id" + " : " + _neko_id.ToString() + "} ";
			ret += "{" + "boss_id" + " : " + _boss_id.ToString() + "} ";
			ret += "{" + "HP" + " : " + _HP.ToString() + "} ";
			ret += "{" + "boss_HP" + " : " + _boss_HP.ToString() + "} ";
			ret += "{" + "coinMin" + " : " + _coinMin.ToString() + "} ";
			ret += "{" + "coinMax" + " : " + _coinMax.ToString() + "} ";
			ret += "{" + "killScore" + " : " + _killScore.ToString() + "} ";
			return ret;
		}
	}
	public sealed class MNP_NekoGameOrder : IGoogle2uDB
	{
		public enum rowIds {
			Stage1, Stage2, Stage3, Stage4, Stage5, Stage6, Stage7, Stage8, Stage9, Stage10, Stage11, Stage12, Stage13, Stage14, Stage15, Stage16, Stage17, Stage18
			, Stage19, Stage20, Stage21, Stage22, Stage23, Stage24, Stage25, Stage26, Stage27, Stage28, Stage29, Stage30, Stage31, Stage32, Stage33, Stage34, Stage35, Stage36, Stage37, Stage38
			, Stage39, Stage40, Stage41, Stage42, Stage43, Stage44, Stage45, Stage46, Stage47, Stage48, Stage49, Stage50, Stage51, Stage52, Stage53, Stage54, Stage55, Stage56, Stage57, Stage58
			, Stage59, Stage60, Stage61, Stage62, Stage63, Stage64, Stage65, Stage66, Stage67, Stage68, Stage69, Stage70, Stage71, Stage72, Stage73, Stage74, Stage75, Stage76, Stage77, Stage78
			
		};
		public string [] rowNames = {
			"Stage1", "Stage2", "Stage3", "Stage4", "Stage5", "Stage6", "Stage7", "Stage8", "Stage9", "Stage10", "Stage11", "Stage12", "Stage13", "Stage14", "Stage15", "Stage16", "Stage17", "Stage18"
			, "Stage19", "Stage20", "Stage21", "Stage22", "Stage23", "Stage24", "Stage25", "Stage26", "Stage27", "Stage28", "Stage29", "Stage30", "Stage31", "Stage32", "Stage33", "Stage34", "Stage35", "Stage36", "Stage37", "Stage38"
			, "Stage39", "Stage40", "Stage41", "Stage42", "Stage43", "Stage44", "Stage45", "Stage46", "Stage47", "Stage48", "Stage49", "Stage50", "Stage51", "Stage52", "Stage53", "Stage54", "Stage55", "Stage56", "Stage57", "Stage58"
			, "Stage59", "Stage60", "Stage61", "Stage62", "Stage63", "Stage64", "Stage65", "Stage66", "Stage67", "Stage68", "Stage69", "Stage70", "Stage71", "Stage72", "Stage73", "Stage74", "Stage75", "Stage76", "Stage77", "Stage78"
			
		};
		public System.Collections.Generic.List<MNP_NekoGameOrderRow> Rows = new System.Collections.Generic.List<MNP_NekoGameOrderRow>();

		public static MNP_NekoGameOrder Instance
		{
			get { return NestedMNP_NekoGameOrder.instance; }
		}

		private class NestedMNP_NekoGameOrder
		{
			static NestedMNP_NekoGameOrder() { }
			internal static readonly MNP_NekoGameOrder instance = new MNP_NekoGameOrder();
		}

		private MNP_NekoGameOrder()
		{
			Rows.Add( new MNP_NekoGameOrderRow("Stage1", "4", "20", "1500", "2500", "4", "8", "20000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage2", "4", "20", "1500", "2500", "4", "8", "21000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage3", "4", "20", "800", "2500", "4", "8", "22000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage4", "51", "20", "2000", "2500", "8", "12", "23000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage5", "51", "20", "2000", "2500", "8", "12", "24000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage6", "51", "20", "900", "2500", "8", "12", "25000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage7", "10", "20", "2000", "2500", "12", "16", "26000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage8", "10", "20", "2000", "2500", "12", "16", "27000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage9", "10", "20", "1000", "2500", "12", "16", "28000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage10", "12", "20", "2000", "2500", "12", "16", "29000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage11", "12", "20", "2000", "2500", "12", "16", "30000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage12", "12", "20", "1100", "2500", "12", "16", "31000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage13", "20", "20", "1300", "1300", "12", "16", "32000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage14", "44", "21", "2000", "2500", "16", "20", "33000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage15", "44", "21", "2000", "2500", "16", "20", "34000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage16", "44", "21", "1500", "2500", "16", "20", "35000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage17", "45", "21", "2000", "2500", "16", "20", "36000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage18", "45", "21", "2000", "2500", "16", "20", "37000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage19", "45", "21", "1600", "2500", "16", "20", "38000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage20", "46", "21", "2000", "2500", "16", "20", "39000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage21", "46", "21", "2000", "2500", "16", "20", "40000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage22", "46", "21", "1800", "2500", "16", "20", "41000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage23", "47", "21", "2000", "2500", "16", "20", "42000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage24", "47", "21", "2000", "2500", "16", "20", "43000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage25", "47", "21", "2100", "2500", "16", "20", "44000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage26", "21", "21", "2400", "2400", "16", "20", "45000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage27", "28", "22", "2400", "2800", "16", "20", "46000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage28", "28", "22", "2400", "2800", "16", "20", "47000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage29", "28", "22", "2600", "2800", "16", "20", "48000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage30", "34", "22", "2400", "2800", "16", "20", "49000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage31", "34", "22", "2400", "2800", "16", "20", "50000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage32", "34", "22", "2700", "2800", "16", "20", "51000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage33", "79", "22", "2400", "2800", "16", "20", "52000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage34", "79", "22", "2400", "2800", "16", "20", "53000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage35", "79", "22", "2800", "2800", "16", "20", "54000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage36", "75", "22", "2400", "2800", "16", "20", "55000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage37", "75", "22", "2400", "2800", "16", "20", "56000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage38", "75", "22", "3000", "2800", "16", "20", "57000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage39", "22", "22", "3300", "3300", "16", "20", "58000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage40", "93", "23", "2800", "3200", "16", "20", "59000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage41", "93", "23", "2800", "3200", "16", "20", "60000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage42", "93", "23", "3600", "3200", "16", "20", "61000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage43", "108", "23", "2800", "3200", "16", "20", "62000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage44", "108", "23", "2800", "3200", "16", "20", "63000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage45", "108", "23", "3700", "3200", "16", "20", "64000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage46", "115", "23", "2800", "3200", "16", "20", "65000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage47", "115", "23", "2800", "3200", "16", "20", "66000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage48", "115", "23", "4000", "3200", "16", "20", "67000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage49", "124", "23", "2800", "3200", "16", "20", "68000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage50", "124", "23", "2800", "3200", "16", "20", "69000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage51", "124", "23", "4400", "3200", "16", "20", "70000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage52", "23", "23", "4700", "4700", "16", "20", "71000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage53", "127", "68", "2800", "4500", "16", "20", "71000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage54", "127", "68", "2800", "4500", "16", "20", "71000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage55", "127", "68", "5000", "5000", "16", "20", "71000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage56", "132", "68", "2800", "4500", "16", "20", "71000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage57", "132", "68", "2800", "4500", "16", "20", "71000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage58", "132", "68", "5200", "5200", "16", "20", "71000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage59", "135", "68", "2800", "4500", "16", "20", "71000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage60", "135", "68", "2800", "4500", "16", "20", "71000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage61", "135", "68", "5400", "5400", "16", "20", "71000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage62", "134", "68", "2800", "4500", "16", "20", "71000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage63", "134", "68", "2800", "4500", "16", "20", "71000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage64", "134", "68", "5600", "5600", "16", "20", "71000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage65", "68", "68", "6000", "6000", "16", "20", "71000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage66", "57", "69", "2800", "4500", "16", "20", "71000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage67", "57", "69", "2800", "4500", "16", "20", "71000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage68", "57", "69", "6200", "6200", "16", "20", "71000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage69", "59", "69", "2800", "4500", "16", "20", "71000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage70", "59", "69", "2800", "4500", "16", "20", "71000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage71", "59", "69", "6400", "6400", "16", "20", "71000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage72", "58", "69", "2800", "4500", "16", "20", "71000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage73", "58", "69", "2800", "4500", "16", "20", "71000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage74", "58", "69", "6600", "6600", "16", "20", "71000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage75", "56", "69", "2800", "4500", "16", "20", "71000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage76", "56", "69", "2800", "4500", "16", "20", "71000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage77", "56", "69", "7000", "4360", "16", "20", "71000"));
			Rows.Add( new MNP_NekoGameOrderRow("Stage78", "69", "69", "8400", "8400", "16", "20", "71000"));
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
		public MNP_NekoGameOrderRow GetRow(rowIds in_RowID)
		{
			MNP_NekoGameOrderRow ret = null;
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
		public MNP_NekoGameOrderRow GetRow(string in_RowString)
		{
			MNP_NekoGameOrderRow ret = null;
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
