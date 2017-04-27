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
	public class MNP_NekoSkillValueRow : IGoogle2uRow
	{
		public string _local;
		public float _value1;
		public float _value2;
		public float _value3;
		public float _value4;
		public string _index;
		public MNP_NekoSkillValueRow(string __id, string __local, string __value1, string __value2, string __value3, string __value4, string __index) 
		{
			_local = __local.Trim();
			{
			float res;
				if(float.TryParse(__value1, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_value1 = res;
				else
					Debug.LogError("Failed To Convert _value1 string: "+ __value1 +" to float");
			}
			{
			float res;
				if(float.TryParse(__value2, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_value2 = res;
				else
					Debug.LogError("Failed To Convert _value2 string: "+ __value2 +" to float");
			}
			{
			float res;
				if(float.TryParse(__value3, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_value3 = res;
				else
					Debug.LogError("Failed To Convert _value3 string: "+ __value3 +" to float");
			}
			{
			float res;
				if(float.TryParse(__value4, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_value4 = res;
				else
					Debug.LogError("Failed To Convert _value4 string: "+ __value4 +" to float");
			}
			_index = __index.Trim();
		}

		public int Length { get { return 6; } }

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
					ret = _local.ToString();
					break;
				case 1:
					ret = _value1.ToString();
					break;
				case 2:
					ret = _value2.ToString();
					break;
				case 3:
					ret = _value3.ToString();
					break;
				case 4:
					ret = _value4.ToString();
					break;
				case 5:
					ret = _index.ToString();
					break;
			}

			return ret;
		}

		public string GetStringData( string colID )
		{
			var ret = System.String.Empty;
			switch( colID )
			{
				case "local":
					ret = _local.ToString();
					break;
				case "value1":
					ret = _value1.ToString();
					break;
				case "value2":
					ret = _value2.ToString();
					break;
				case "value3":
					ret = _value3.ToString();
					break;
				case "value4":
					ret = _value4.ToString();
					break;
				case "index":
					ret = _index.ToString();
					break;
			}

			return ret;
		}
		public override string ToString()
		{
			string ret = System.String.Empty;
			ret += "{" + "local" + " : " + _local.ToString() + "} ";
			ret += "{" + "value1" + " : " + _value1.ToString() + "} ";
			ret += "{" + "value2" + " : " + _value2.ToString() + "} ";
			ret += "{" + "value3" + " : " + _value3.ToString() + "} ";
			ret += "{" + "value4" + " : " + _value4.ToString() + "} ";
			ret += "{" + "index" + " : " + _index.ToString() + "} ";
			return ret;
		}
	}
	public sealed class MNP_NekoSkillValue : IGoogle2uDB
	{
		public enum rowIds {
			score_passive, coin_passive, time_passive, fevertime_passive, power_passive, yellowblock_score_passive, blueblock_score_passive, redblock_score_passive, yellowblock_appear_passive, blueblock_appear_passive, redblock_appear_passive, bomb_appear_passive, nekoskill_appear_passive, userexp_passive, random_bomb_active, combo_maintain_active, fever_raise_active, time_active
			, yellow_bomb_active, blue_bomb_active, red_bomb_active, black_bomb_active
		};
		public string [] rowNames = {
			"score_passive", "coin_passive", "time_passive", "fevertime_passive", "power_passive", "yellowblock_score_passive", "blueblock_score_passive", "redblock_score_passive", "yellowblock_appear_passive", "blueblock_appear_passive", "redblock_appear_passive", "bomb_appear_passive", "nekoskill_appear_passive", "userexp_passive", "random_bomb_active", "combo_maintain_active", "fever_raise_active", "time_active"
			, "yellow_bomb_active", "blue_bomb_active", "red_bomb_active", "black_bomb_active"
		};
		public System.Collections.Generic.List<MNP_NekoSkillValueRow> Rows = new System.Collections.Generic.List<MNP_NekoSkillValueRow>();

		public static MNP_NekoSkillValue Instance
		{
			get { return NestedMNP_NekoSkillValue.instance; }
		}

		private class NestedMNP_NekoSkillValue
		{
			static NestedMNP_NekoSkillValue() { }
			internal static readonly MNP_NekoSkillValue instance = new MNP_NekoSkillValue();
		}

		private MNP_NekoSkillValue()
		{
			Rows.Add( new MNP_NekoSkillValueRow("score_passive", "3950", "3", "5", "8", "12", "1"));
			Rows.Add( new MNP_NekoSkillValueRow("coin_passive", "3951", "5", "10", "15", "20", "2"));
			Rows.Add( new MNP_NekoSkillValueRow("time_passive", "3952", "1", "2", "3", "4", "3"));
			Rows.Add( new MNP_NekoSkillValueRow("fevertime_passive", "3953", "0.5", "1", "1.5", "2", "4"));
			Rows.Add( new MNP_NekoSkillValueRow("power_passive", "3954", "5", "10", "15", "25", "5"));
			Rows.Add( new MNP_NekoSkillValueRow("yellowblock_score_passive", "3955", "20", "40", "60", "100", "6"));
			Rows.Add( new MNP_NekoSkillValueRow("blueblock_score_passive", "3956", "20", "40", "60", "100", "7"));
			Rows.Add( new MNP_NekoSkillValueRow("redblock_score_passive", "3957", "20", "40", "60", "100", "8"));
			Rows.Add( new MNP_NekoSkillValueRow("yellowblock_appear_passive", "3958", "10", "15", "25", "35", "9"));
			Rows.Add( new MNP_NekoSkillValueRow("blueblock_appear_passive", "3959", "10", "15", "25", "35", "10"));
			Rows.Add( new MNP_NekoSkillValueRow("redblock_appear_passive", "3960", "10", "15", "25", "35", "11"));
			Rows.Add( new MNP_NekoSkillValueRow("bomb_appear_passive", "3961", "57", "55", "52", "49", "12"));
			Rows.Add( new MNP_NekoSkillValueRow("nekoskill_appear_passive", "3962", "3.2", "3.4", "3.6", "4", "13"));
			Rows.Add( new MNP_NekoSkillValueRow("userexp_passive", "3963", "5", "10", "15", "25", "14"));
			Rows.Add( new MNP_NekoSkillValueRow("random_bomb_active", "3964", "1", "2", "3", "4", "15"));
			Rows.Add( new MNP_NekoSkillValueRow("combo_maintain_active", "3965", "2", "3", "4", "5", "16"));
			Rows.Add( new MNP_NekoSkillValueRow("fever_raise_active", "3966", "1", "2", "3", "4", "17"));
			Rows.Add( new MNP_NekoSkillValueRow("time_active", "3967", "1", "1.5", "2", "3", "18"));
			Rows.Add( new MNP_NekoSkillValueRow("yellow_bomb_active", "3968", "1", "2", "3", "4", "19"));
			Rows.Add( new MNP_NekoSkillValueRow("blue_bomb_active", "3969", "1", "2", "3", "4", "20"));
			Rows.Add( new MNP_NekoSkillValueRow("red_bomb_active", "3970", "1", "2", "3", "4", "21"));
			Rows.Add( new MNP_NekoSkillValueRow("black_bomb_active", "3971", "1", "2", "3", "4", "22"));
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
		public MNP_NekoSkillValueRow GetRow(rowIds in_RowID)
		{
			MNP_NekoSkillValueRow ret = null;
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
		public MNP_NekoSkillValueRow GetRow(string in_RowString)
		{
			MNP_NekoSkillValueRow ret = null;
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
