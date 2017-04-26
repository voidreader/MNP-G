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
	public class MNP_WantedRow : IGoogle2uRow
	{
		public int _tid;
		public string _wanted_type;
		public MNP_WantedRow(string __id, string __tid, string __wanted_type) 
		{
			{
			int res;
				if(int.TryParse(__tid, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_tid = res;
				else
					Debug.LogError("Failed To Convert _tid string: "+ __tid +" to int");
			}
			_wanted_type = __wanted_type.Trim();
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
					ret = _tid.ToString();
					break;
				case 1:
					ret = _wanted_type.ToString();
					break;
			}

			return ret;
		}

		public string GetStringData( string colID )
		{
			var ret = System.String.Empty;
			switch( colID )
			{
				case "tid":
					ret = _tid.ToString();
					break;
				case "wanted_type":
					ret = _wanted_type.ToString();
					break;
			}

			return ret;
		}
		public override string ToString()
		{
			string ret = System.String.Empty;
			ret += "{" + "tid" + " : " + _tid.ToString() + "} ";
			ret += "{" + "wanted_type" + " : " + _wanted_type.ToString() + "} ";
			return ret;
		}
	}
	public sealed class MNP_Wanted : IGoogle2uDB
	{
		public enum rowIds {
			wanted0, wanted1, wanted2, wanted3, wanted4, wanted5, wanted6, wanted7, wanted8, wanted9, wanted10, wanted11, wanted12, wanted13, wanted14, wanted15, wanted16, wanted17
			, wanted18, wanted19, wanted20, wanted21, wanted22, wanted23, wanted24, wanted25, wanted26, wanted27, wanted28, wanted29, wanted30, wanted31, wanted32, wanted33, wanted34, wanted35, wanted36, wanted37
			, wanted38, wanted39, wanted40, wanted41, wanted42, wanted43, wanted44, wanted45, wanted46, wanted47, wanted48, wanted49, wanted50, wanted51, wanted52, wanted53, wanted54, wanted55, wanted56, wanted57
			, wanted58, wanted59, wanted60, wanted61, wanted62, wanted63, wanted64, wanted65, wanted66, wanted67, wanted68, wanted69, wanted70, wanted71, wanted72, wanted73, wanted74, wanted75, wanted76, wanted77
			, wanted78, wanted79, wanted80, wanted81, wanted82, wanted83, wanted84, wanted85, wanted86, wanted87, wanted88, wanted89, wanted90, wanted91, wanted92, wanted93, wanted94, wanted95, wanted96, wanted97
			, wanted98, wanted99, wanted100, wanted101, wanted102, wanted103, wanted104, wanted105, wanted106, wanted107, wanted108, wanted109, wanted110, wanted111, wanted112, wanted113, wanted114, wanted115, wanted116, wanted117
			, wanted118, wanted119, wanted120, wanted121, wanted122, wanted123, wanted124, wanted125, wanted126, wanted127, wanted128, wanted129, wanted130, wanted131, wanted132, wanted133, wanted134, wanted135, wanted136, wanted137
			, wanted138, wanted139, wanted140, wanted141, wanted142, wanted143, wanted144, wanted145, wanted146, wanted147, wanted148, wanted149, wanted150, wanted151, wanted152, wanted153, wanted154, wanted155, wanted156, wanted157
			, wanted158, wanted159, wanted160, wanted161, wanted162, wanted163, wanted164, wanted165, wanted166, wanted167, wanted168, wanted169, wanted170, wanted171, wanted172, wanted173, wanted174, wanted175, wanted176, wanted177
			, wanted178, wanted179, wanted180, wanted181, wanted182, wanted183, wanted184, wanted185, wanted186, wanted187, wanted188, wanted189, wanted190, wanted191, wanted192, wanted193, wanted194, wanted195, wanted196, wanted197
			, wanted198, wanted199, wanted200, wanted201
		};
		public string [] rowNames = {
			"wanted0", "wanted1", "wanted2", "wanted3", "wanted4", "wanted5", "wanted6", "wanted7", "wanted8", "wanted9", "wanted10", "wanted11", "wanted12", "wanted13", "wanted14", "wanted15", "wanted16", "wanted17"
			, "wanted18", "wanted19", "wanted20", "wanted21", "wanted22", "wanted23", "wanted24", "wanted25", "wanted26", "wanted27", "wanted28", "wanted29", "wanted30", "wanted31", "wanted32", "wanted33", "wanted34", "wanted35", "wanted36", "wanted37"
			, "wanted38", "wanted39", "wanted40", "wanted41", "wanted42", "wanted43", "wanted44", "wanted45", "wanted46", "wanted47", "wanted48", "wanted49", "wanted50", "wanted51", "wanted52", "wanted53", "wanted54", "wanted55", "wanted56", "wanted57"
			, "wanted58", "wanted59", "wanted60", "wanted61", "wanted62", "wanted63", "wanted64", "wanted65", "wanted66", "wanted67", "wanted68", "wanted69", "wanted70", "wanted71", "wanted72", "wanted73", "wanted74", "wanted75", "wanted76", "wanted77"
			, "wanted78", "wanted79", "wanted80", "wanted81", "wanted82", "wanted83", "wanted84", "wanted85", "wanted86", "wanted87", "wanted88", "wanted89", "wanted90", "wanted91", "wanted92", "wanted93", "wanted94", "wanted95", "wanted96", "wanted97"
			, "wanted98", "wanted99", "wanted100", "wanted101", "wanted102", "wanted103", "wanted104", "wanted105", "wanted106", "wanted107", "wanted108", "wanted109", "wanted110", "wanted111", "wanted112", "wanted113", "wanted114", "wanted115", "wanted116", "wanted117"
			, "wanted118", "wanted119", "wanted120", "wanted121", "wanted122", "wanted123", "wanted124", "wanted125", "wanted126", "wanted127", "wanted128", "wanted129", "wanted130", "wanted131", "wanted132", "wanted133", "wanted134", "wanted135", "wanted136", "wanted137"
			, "wanted138", "wanted139", "wanted140", "wanted141", "wanted142", "wanted143", "wanted144", "wanted145", "wanted146", "wanted147", "wanted148", "wanted149", "wanted150", "wanted151", "wanted152", "wanted153", "wanted154", "wanted155", "wanted156", "wanted157"
			, "wanted158", "wanted159", "wanted160", "wanted161", "wanted162", "wanted163", "wanted164", "wanted165", "wanted166", "wanted167", "wanted168", "wanted169", "wanted170", "wanted171", "wanted172", "wanted173", "wanted174", "wanted175", "wanted176", "wanted177"
			, "wanted178", "wanted179", "wanted180", "wanted181", "wanted182", "wanted183", "wanted184", "wanted185", "wanted186", "wanted187", "wanted188", "wanted189", "wanted190", "wanted191", "wanted192", "wanted193", "wanted194", "wanted195", "wanted196", "wanted197"
			, "wanted198", "wanted199", "wanted200", "wanted201"
		};
		public System.Collections.Generic.List<MNP_WantedRow> Rows = new System.Collections.Generic.List<MNP_WantedRow>();

		public static MNP_Wanted Instance
		{
			get { return NestedMNP_Wanted.instance; }
		}

		private class NestedMNP_Wanted
		{
			static NestedMNP_Wanted() { }
			internal static readonly MNP_Wanted instance = new MNP_Wanted();
		}

		private MNP_Wanted()
		{
			Rows.Add( new MNP_WantedRow("wanted0", "0", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted1", "1", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted2", "2", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted3", "3", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted4", "4", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted5", "5", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted6", "6", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted7", "7", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted8", "8", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted9", "9", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted10", "10", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted11", "11", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted12", "12", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted13", "13", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted14", "14", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted15", "15", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted16", "16", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted17", "17", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted18", "18", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted19", "19", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted20", "20", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted21", "21", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted22", "22", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted23", "23", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted24", "24", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted25", "25", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted26", "26", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted27", "27", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted28", "28", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted29", "29", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted30", "30", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted31", "31", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted32", "32", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted33", "33", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted34", "34", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted35", "35", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted36", "36", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted37", "37", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted38", "38", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted39", "39", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted40", "40", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted41", "41", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted42", "42", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted43", "43", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted44", "44", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted45", "45", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted46", "46", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted47", "47", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted48", "48", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted49", "49", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted50", "50", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted51", "51", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted52", "52", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted53", "53", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted54", "54", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted55", "55", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted56", "56", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted57", "57", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted58", "58", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted59", "59", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted60", "60", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted61", "61", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted62", "62", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted63", "63", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted64", "64", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted65", "65", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted66", "66", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted67", "67", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted68", "68", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted69", "69", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted70", "70", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted71", "71", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted72", "72", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted73", "73", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted74", "74", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted75", "75", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted76", "76", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted77", "77", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted78", "78", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted79", "79", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted80", "80", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted81", "81", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted82", "82", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted83", "83", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted84", "84", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted85", "85", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted86", "86", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted87", "87", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted88", "88", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted89", "89", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted90", "90", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted91", "91", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted92", "92", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted93", "93", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted94", "94", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted95", "95", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted96", "108", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted97", "109", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted98", "110", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted99", "111", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted100", "112", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted101", "113", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted102", "114", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted103", "115", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted104", "116", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted105", "117", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted106", "118", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted107", "119", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted108", "120", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted109", "121", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted110", "122", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted111", "123", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted112", "124", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted113", "125", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted114", "126", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted115", "127", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted116", "128", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted117", "129", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted118", "130", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted119", "131", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted120", "132", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted121", "133", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted122", "134", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted123", "135", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted124", "136", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted125", "137", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted126", "138", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted127", "139", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted128", "140", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted129", "141", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted130", "142", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted131", "143", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted132", "98", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted133", "168", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted134", "169", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted135", "170", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted136", "99", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted137", "171", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted138", "172", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted139", "173", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted140", "100", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted141", "177", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted142", "178", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted143", "179", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted144", "102", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted145", "174", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted146", "175", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted147", "176", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted148", "199", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted149", "200", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted150", "201", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted151", "202", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted152", "203", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted153", "204", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted154", "205", "fr-bronze"));
			Rows.Add( new MNP_WantedRow("wanted155", "206", "fr-silver"));
			Rows.Add( new MNP_WantedRow("wanted156", "146", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted157", "147", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted158", "148", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted159", "149", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted160", "156", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted161", "157", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted162", "158", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted163", "159", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted164", "160", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted165", "161", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted166", "162", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted167", "163", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted168", "106", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted169", "107", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted170", "180", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted171", "181", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted172", "190", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted173", "191", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted174", "192", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted175", "193", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted176", "195", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted177", "196", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted178", "197", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted179", "198", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted180", "207", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted181", "208", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted182", "209", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted183", "210", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted184", "211", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted185", "212", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted186", "213", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted187", "219", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted188", "220", "fr-gold"));
			Rows.Add( new MNP_WantedRow("wanted189", "96", "fr-rainbow"));
			Rows.Add( new MNP_WantedRow("wanted190", "165", "fr-rainbow"));
			Rows.Add( new MNP_WantedRow("wanted191", "166", "fr-rainbow"));
			Rows.Add( new MNP_WantedRow("wanted192", "167", "fr-rainbow"));
			Rows.Add( new MNP_WantedRow("wanted193", "182", "fr-rainbow"));
			Rows.Add( new MNP_WantedRow("wanted194", "183", "fr-rainbow"));
			Rows.Add( new MNP_WantedRow("wanted195", "184", "fr-rainbow"));
			Rows.Add( new MNP_WantedRow("wanted196", "185", "fr-rainbow"));
			Rows.Add( new MNP_WantedRow("wanted197", "186", "fr-rainbow"));
			Rows.Add( new MNP_WantedRow("wanted198", "187", "fr-rainbow"));
			Rows.Add( new MNP_WantedRow("wanted199", "188", "fr-rainbow"));
			Rows.Add( new MNP_WantedRow("wanted200", "189", "fr-rainbow"));
			Rows.Add( new MNP_WantedRow("wanted201", "194", "fr-rainbow"));
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
		public MNP_WantedRow GetRow(rowIds in_RowID)
		{
			MNP_WantedRow ret = null;
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
		public MNP_WantedRow GetRow(string in_RowString)
		{
			MNP_WantedRow ret = null;
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
