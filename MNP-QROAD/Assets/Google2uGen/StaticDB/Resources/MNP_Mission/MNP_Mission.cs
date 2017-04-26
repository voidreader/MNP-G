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
	public class MNP_MissionRow : IGoogle2uRow
	{
		public string _title;
		public string _content;
		public string _remark;
		public MNP_MissionRow(string __id, string __title, string __content, string __remark) 
		{
			_title = __title.Trim();
			_content = __content.Trim();
			_remark = __remark.Trim();
		}

		public int Length { get { return 3; } }

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
					ret = _title.ToString();
					break;
				case 1:
					ret = _content.ToString();
					break;
				case 2:
					ret = _remark.ToString();
					break;
			}

			return ret;
		}

		public string GetStringData( string colID )
		{
			var ret = System.String.Empty;
			switch( colID )
			{
				case "title":
					ret = _title.ToString();
					break;
				case "content":
					ret = _content.ToString();
					break;
				case "remark":
					ret = _remark.ToString();
					break;
			}

			return ret;
		}
		public override string ToString()
		{
			string ret = System.String.Empty;
			ret += "{" + "title" + " : " + _title.ToString() + "} ";
			ret += "{" + "content" + " : " + _content.ToString() + "} ";
			ret += "{" + "remark" + " : " + _remark.ToString() + "} ";
			return ret;
		}
	}
	public sealed class MNP_Mission : IGoogle2uDB
	{
		public enum rowIds {
			day1, day2, day3, day4, day5, day6, day7, day8, day9, day10, day11, day12, day13, week1, week2, week3, week4, week5
			, week6, week7, week8, week9, week10, week11, week12, week13
		};
		public string [] rowNames = {
			"day1", "day2", "day3", "day4", "day5", "day6", "day7", "day8", "day9", "day10", "day11", "day12", "day13", "week1", "week2", "week3", "week4", "week5"
			, "week6", "week7", "week8", "week9", "week10", "week11", "week12", "week13"
		};
		public System.Collections.Generic.List<MNP_MissionRow> Rows = new System.Collections.Generic.List<MNP_MissionRow>();

		public static MNP_Mission Instance
		{
			get { return NestedMNP_Mission.instance; }
		}

		private class NestedMNP_Mission
		{
			static NestedMNP_Mission() { }
			internal static readonly MNP_Mission instance = new MNP_Mission();
		}

		private MNP_Mission()
		{
			Rows.Add( new MNP_MissionRow("day1", "5000", "5050", ""));
			Rows.Add( new MNP_MissionRow("day2", "5001", "5051", ""));
			Rows.Add( new MNP_MissionRow("day3", "5002", "5052", ""));
			Rows.Add( new MNP_MissionRow("day4", "5003", "5053", ""));
			Rows.Add( new MNP_MissionRow("day5", "5004", "5054", ""));
			Rows.Add( new MNP_MissionRow("day6", "5005", "5055", ""));
			Rows.Add( new MNP_MissionRow("day7", "5006", "5056", ""));
			Rows.Add( new MNP_MissionRow("day8", "5007", "5057", ""));
			Rows.Add( new MNP_MissionRow("day9", "5008", "5058", ""));
			Rows.Add( new MNP_MissionRow("day10", "5009", "5059", ""));
			Rows.Add( new MNP_MissionRow("day11", "5010", "5060", "모든 일일 미션 클리어"));
			Rows.Add( new MNP_MissionRow("day12", "5011", "5061", ""));
			Rows.Add( new MNP_MissionRow("day13", "5012", "5062", "Free Crane n회"));
			Rows.Add( new MNP_MissionRow("week1", "5100", "5150", ""));
			Rows.Add( new MNP_MissionRow("week2", "5101", "5151", ""));
			Rows.Add( new MNP_MissionRow("week3", "5102", "5152", ""));
			Rows.Add( new MNP_MissionRow("week4", "5103", "5153", ""));
			Rows.Add( new MNP_MissionRow("week5", "5104", "5154", ""));
			Rows.Add( new MNP_MissionRow("week6", "5105", "5155", ""));
			Rows.Add( new MNP_MissionRow("week7", "5106", "5156", "네코 보너스"));
			Rows.Add( new MNP_MissionRow("week8", "5107", "5157", ""));
			Rows.Add( new MNP_MissionRow("week9", "5108", "5158", ""));
			Rows.Add( new MNP_MissionRow("week10", "5109", "5159", ""));
			Rows.Add( new MNP_MissionRow("week11", "5110", "5160", "모든 주간미션 클리어"));
			Rows.Add( new MNP_MissionRow("week12", "5111", "5161", ""));
			Rows.Add( new MNP_MissionRow("week13", "5112", "5162", "Free Crane n회"));
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
		public MNP_MissionRow GetRow(rowIds in_RowID)
		{
			MNP_MissionRow ret = null;
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
		public MNP_MissionRow GetRow(string in_RowString)
		{
			MNP_MissionRow ret = null;
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
