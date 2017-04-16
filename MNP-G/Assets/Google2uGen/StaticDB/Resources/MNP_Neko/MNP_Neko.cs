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
	public class MNP_NekoRow : IGoogle2uRow
	{
		public int _collection_index;
		public string _main_sprite;
		public string _mini_head;
		public string _NameLocalID;
		public string _InfoLocalID;
		public string _base_grade;
		public string _max_grade;
		public string _five_grade_change;
		public string _five_main_sprite;
		public string _five_main_id;
		public string _five_mini_head;
		public string _five_NameLocalID;
		public string _five_InfoLocalID;
		public MNP_NekoRow(string __id, string __collection_index, string __main_sprite, string __mini_head, string __NameLocalID, string __InfoLocalID, string __base_grade, string __max_grade, string __five_grade_change, string __five_main_sprite, string __five_main_id, string __five_mini_head, string __five_NameLocalID, string __five_InfoLocalID) 
		{
			{
			int res;
				if(int.TryParse(__collection_index, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_collection_index = res;
				else
					Debug.LogError("Failed To Convert _collection_index string: "+ __collection_index +" to int");
			}
			_main_sprite = __main_sprite.Trim();
			_mini_head = __mini_head.Trim();
			_NameLocalID = __NameLocalID.Trim();
			_InfoLocalID = __InfoLocalID.Trim();
			_base_grade = __base_grade.Trim();
			_max_grade = __max_grade.Trim();
			_five_grade_change = __five_grade_change.Trim();
			_five_main_sprite = __five_main_sprite.Trim();
			_five_main_id = __five_main_id.Trim();
			_five_mini_head = __five_mini_head.Trim();
			_five_NameLocalID = __five_NameLocalID.Trim();
			_five_InfoLocalID = __five_InfoLocalID.Trim();
		}

		public int Length { get { return 13; } }

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
					ret = _collection_index.ToString();
					break;
				case 1:
					ret = _main_sprite.ToString();
					break;
				case 2:
					ret = _mini_head.ToString();
					break;
				case 3:
					ret = _NameLocalID.ToString();
					break;
				case 4:
					ret = _InfoLocalID.ToString();
					break;
				case 5:
					ret = _base_grade.ToString();
					break;
				case 6:
					ret = _max_grade.ToString();
					break;
				case 7:
					ret = _five_grade_change.ToString();
					break;
				case 8:
					ret = _five_main_sprite.ToString();
					break;
				case 9:
					ret = _five_main_id.ToString();
					break;
				case 10:
					ret = _five_mini_head.ToString();
					break;
				case 11:
					ret = _five_NameLocalID.ToString();
					break;
				case 12:
					ret = _five_InfoLocalID.ToString();
					break;
			}

			return ret;
		}

		public string GetStringData( string colID )
		{
			var ret = System.String.Empty;
			switch( colID )
			{
				case "collection_index":
					ret = _collection_index.ToString();
					break;
				case "main_sprite":
					ret = _main_sprite.ToString();
					break;
				case "mini_head":
					ret = _mini_head.ToString();
					break;
				case "NameLocalID":
					ret = _NameLocalID.ToString();
					break;
				case "InfoLocalID":
					ret = _InfoLocalID.ToString();
					break;
				case "base_grade":
					ret = _base_grade.ToString();
					break;
				case "max_grade":
					ret = _max_grade.ToString();
					break;
				case "five_grade_change":
					ret = _five_grade_change.ToString();
					break;
				case "five_main_sprite":
					ret = _five_main_sprite.ToString();
					break;
				case "five_main_id":
					ret = _five_main_id.ToString();
					break;
				case "five_mini_head":
					ret = _five_mini_head.ToString();
					break;
				case "five_NameLocalID":
					ret = _five_NameLocalID.ToString();
					break;
				case "five_InfoLocalID":
					ret = _five_InfoLocalID.ToString();
					break;
			}

			return ret;
		}
		public override string ToString()
		{
			string ret = System.String.Empty;
			ret += "{" + "collection_index" + " : " + _collection_index.ToString() + "} ";
			ret += "{" + "main_sprite" + " : " + _main_sprite.ToString() + "} ";
			ret += "{" + "mini_head" + " : " + _mini_head.ToString() + "} ";
			ret += "{" + "NameLocalID" + " : " + _NameLocalID.ToString() + "} ";
			ret += "{" + "InfoLocalID" + " : " + _InfoLocalID.ToString() + "} ";
			ret += "{" + "base_grade" + " : " + _base_grade.ToString() + "} ";
			ret += "{" + "max_grade" + " : " + _max_grade.ToString() + "} ";
			ret += "{" + "five_grade_change" + " : " + _five_grade_change.ToString() + "} ";
			ret += "{" + "five_main_sprite" + " : " + _five_main_sprite.ToString() + "} ";
			ret += "{" + "five_main_id" + " : " + _five_main_id.ToString() + "} ";
			ret += "{" + "five_mini_head" + " : " + _five_mini_head.ToString() + "} ";
			ret += "{" + "five_NameLocalID" + " : " + _five_NameLocalID.ToString() + "} ";
			ret += "{" + "five_InfoLocalID" + " : " + _five_InfoLocalID.ToString() + "} ";
			return ret;
		}
	}
	public sealed class MNP_Neko : IGoogle2uDB
	{
		public enum rowIds {
			Neko0, Neko1, Neko2, Neko3, Neko4, Neko5, Neko6, Neko7, Neko8, Neko9, Neko10, Neko11, Neko12, Neko13, Neko14, Neko15, Neko16, Neko17
			, Neko18, Neko19, Neko20, Neko21, Neko22, Neko23, Neko24, Neko25, Neko26, Neko27, Neko28, Neko29, Neko30, Neko31, Neko32, Neko33, Neko34, Neko35, Neko36, Neko37
			, Neko38, Neko39, Neko40, Neko41, Neko42, Neko43, Neko44, Neko45, Neko46, Neko47, Neko48, Neko49, Neko50, Neko51, Neko52, Neko53, Neko54, Neko55, Neko56, Neko57
			, Neko58, Neko59, Neko60, Neko61, Neko62, Neko63, Neko64, Neko65, Neko66, Neko67, Neko68, Neko69, Neko70, Neko71, Neko72, Neko73, Neko74, Neko75, Neko76, Neko77
			, Neko78, Neko79, Neko80, Neko81, Neko82, Neko83, Neko84, Neko85, Neko86, Neko87, Neko88, Neko89, Neko90, Neko91, Neko92, Neko93, Neko94, Neko95, Neko96, Neko97
			, Neko98, Neko99, Neko100, Neko101, Neko102, Neko103, Neko104, Neko105, Neko106, Neko107, Neko108, Neko109, Neko110, Neko111, Neko112, Neko113, Neko114, Neko115, Neko116, Neko117
			, Neko118, Neko119, Neko120, Neko121, Neko122, Neko123, Neko124, Neko125, Neko126, Neko127, Neko128, Neko129, Neko130, Neko131, Neko132, Neko133, Neko134, Neko135, Neko136, Neko137
			, Neko138, Neko139, Neko140, Neko141, Neko142, Neko143, Neko144, Neko145, Neko146, Neko147, Neko148, Neko149, Neko150, Neko151, Neko152, Neko153, Neko154, Neko155, Neko156, Neko157
			, Neko158, Neko159, Neko160, Neko161, Neko162, Neko163, Neko164, Neko165, Neko166, Neko167, Neko168, Neko169, Neko170, Neko171, Neko172, Neko173, Neko174, Neko175, Neko176, Neko177
			, Neko178, Neko179, Neko180, Neko181, Neko182, Neko183, Neko184, Neko185, Neko186, Neko187, Neko188, Neko189, Neko190, Neko191, Neko192, Neko193, Neko194, Neko195, Neko196, Neko197
			, Neko198, Neko199, Neko200, Neko201, Neko202, Neko203, Neko204, Neko205, Neko206, Neko207, Neko208, Neko209, Neko210, Neko211, Neko212, Neko213, Neko214, Neko215, Neko216, Neko217
			, Neko218, Neko219, Neko220, Neko221, Neko222
		};
		public string [] rowNames = {
			"Neko0", "Neko1", "Neko2", "Neko3", "Neko4", "Neko5", "Neko6", "Neko7", "Neko8", "Neko9", "Neko10", "Neko11", "Neko12", "Neko13", "Neko14", "Neko15", "Neko16", "Neko17"
			, "Neko18", "Neko19", "Neko20", "Neko21", "Neko22", "Neko23", "Neko24", "Neko25", "Neko26", "Neko27", "Neko28", "Neko29", "Neko30", "Neko31", "Neko32", "Neko33", "Neko34", "Neko35", "Neko36", "Neko37"
			, "Neko38", "Neko39", "Neko40", "Neko41", "Neko42", "Neko43", "Neko44", "Neko45", "Neko46", "Neko47", "Neko48", "Neko49", "Neko50", "Neko51", "Neko52", "Neko53", "Neko54", "Neko55", "Neko56", "Neko57"
			, "Neko58", "Neko59", "Neko60", "Neko61", "Neko62", "Neko63", "Neko64", "Neko65", "Neko66", "Neko67", "Neko68", "Neko69", "Neko70", "Neko71", "Neko72", "Neko73", "Neko74", "Neko75", "Neko76", "Neko77"
			, "Neko78", "Neko79", "Neko80", "Neko81", "Neko82", "Neko83", "Neko84", "Neko85", "Neko86", "Neko87", "Neko88", "Neko89", "Neko90", "Neko91", "Neko92", "Neko93", "Neko94", "Neko95", "Neko96", "Neko97"
			, "Neko98", "Neko99", "Neko100", "Neko101", "Neko102", "Neko103", "Neko104", "Neko105", "Neko106", "Neko107", "Neko108", "Neko109", "Neko110", "Neko111", "Neko112", "Neko113", "Neko114", "Neko115", "Neko116", "Neko117"
			, "Neko118", "Neko119", "Neko120", "Neko121", "Neko122", "Neko123", "Neko124", "Neko125", "Neko126", "Neko127", "Neko128", "Neko129", "Neko130", "Neko131", "Neko132", "Neko133", "Neko134", "Neko135", "Neko136", "Neko137"
			, "Neko138", "Neko139", "Neko140", "Neko141", "Neko142", "Neko143", "Neko144", "Neko145", "Neko146", "Neko147", "Neko148", "Neko149", "Neko150", "Neko151", "Neko152", "Neko153", "Neko154", "Neko155", "Neko156", "Neko157"
			, "Neko158", "Neko159", "Neko160", "Neko161", "Neko162", "Neko163", "Neko164", "Neko165", "Neko166", "Neko167", "Neko168", "Neko169", "Neko170", "Neko171", "Neko172", "Neko173", "Neko174", "Neko175", "Neko176", "Neko177"
			, "Neko178", "Neko179", "Neko180", "Neko181", "Neko182", "Neko183", "Neko184", "Neko185", "Neko186", "Neko187", "Neko188", "Neko189", "Neko190", "Neko191", "Neko192", "Neko193", "Neko194", "Neko195", "Neko196", "Neko197"
			, "Neko198", "Neko199", "Neko200", "Neko201", "Neko202", "Neko203", "Neko204", "Neko205", "Neko206", "Neko207", "Neko208", "Neko209", "Neko210", "Neko211", "Neko212", "Neko213", "Neko214", "Neko215", "Neko216", "Neko217"
			, "Neko218", "Neko219", "Neko220", "Neko221", "Neko222"
		};
		public System.Collections.Generic.List<MNP_NekoRow> Rows = new System.Collections.Generic.List<MNP_NekoRow>();

		public static MNP_Neko Instance
		{
			get { return NestedMNP_Neko.instance; }
		}

		private class NestedMNP_Neko
		{
			static NestedMNP_Neko() { }
			internal static readonly MNP_Neko instance = new MNP_Neko();
		}

		private MNP_Neko()
		{
			Rows.Add( new MNP_NekoRow("Neko0", "0", "neko1-1", "neko-m-1", "1000", "2000", "1", "3", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko1", "0", "neko2-1", "neko-m-2", "1001", "2001", "2", "4", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko2", "0", "neko3-1", "neko-m-3", "1002", "2002", "2", "4", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko3", "0", "neko4-1", "neko-m-4", "1003", "2003", "2", "4", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko4", "0", "neko5-1", "neko-m-5", "1004", "2004", "1", "3", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko5", "0", "neko6-1", "neko-m-6", "1005", "2005", "2", "4", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko6", "0", "neko7-1", "neko-m-7", "1006", "2006", "3", "5", "Y", "neko8-1", "7", "neko-m-8", "1007", "2007"));
			Rows.Add( new MNP_NekoRow("Neko7", "0", "neko8-1", "neko-m-8", "1007", "2007", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko8", "0", "neko9-1", "neko-m-9", "1008", "2008", "2", "4", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko9", "0", "neko10-1", "neko-m-10", "1009", "2009", "3", "5", "Y", "neko11-1", "10", "neko-m-11", "1010", "2010"));
			Rows.Add( new MNP_NekoRow("Neko10", "0", "neko11-1", "neko-m-11", "1010", "2010", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko11", "0", "neko12-1", "neko-m-12", "1011", "2011", "1", "3", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko12", "0", "neko13-1", "neko-m-13", "1012", "2012", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko13", "0", "neko15-1", "neko-m-15", "1013", "2013", "3", "5", "Y", "neko13-1", "12", "neko-m-13", "1012", "2012"));
			Rows.Add( new MNP_NekoRow("Neko14", "0", "neko14-1", "neko-m-14", "1014", "2014", "2", "4", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko15", "0", "neko16-1", "neko-m-16", "1015", "2015", "1", "3", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko16", "0", "neko17-1", "neko-m-17", "1016", "2016", "2", "4", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko17", "0", "neko18-1", "neko-m-18", "1017", "2017", "2", "4", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko18", "0", "neko19-1", "neko-m-19", "1018", "2018", "3", "5", "Y", "neko20-1", "19", "neko-m-20", "1019", "2019"));
			Rows.Add( new MNP_NekoRow("Neko19", "0", "neko20-1", "neko-m-20", "1019", "2019", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko20", "0", "neko21-1", "neko-m-21", "1020", "2020", "1", "3", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko21", "0", "neko22-1", "neko-m-22", "1021", "2021", "3", "5", "Y", "neko24-1", "23", "neko-m-24", "1023", "2023"));
			Rows.Add( new MNP_NekoRow("Neko22", "0", "neko23-1", "neko-m-23", "1022", "2022", "2", "4", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko23", "0", "neko24-1", "neko-m-24", "1023", "2023", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko24", "0", "neko25-1", "neko-m-25", "1024", "2024", "3", "5", "Y", "neko28-1", "27", "neko-m-28", "1027", "2027"));
			Rows.Add( new MNP_NekoRow("Neko25", "0", "neko26-1", "neko-m-26", "1025", "2025", "2", "4", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko26", "0", "neko27-1", "neko-m-27", "1026", "2026", "1", "3", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko27", "0", "neko28-1", "neko-m-28", "1027", "2027", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko28", "0", "neko29-1", "neko-m-29", "1028", "2028", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko29", "0", "neko30-1", "neko-m-30", "1029", "2029", "1", "3", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko30", "0", "neko31-1", "neko-m-31", "1030", "2030", "1", "3", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko31", "0", "neko32-1", "neko-m-32", "1031", "2031", "3", "5", "Y", "neko29-1", "28", "neko-m-29", "1028", "2028"));
			Rows.Add( new MNP_NekoRow("Neko32", "0", "neko33-1", "neko-m-33", "1032", "2032", "1", "3", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko33", "0", "neko34-1", "neko-m-34", "1033", "2033", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko34", "0", "neko35-1", "neko-m-35", "1034", "2034", "3", "5", "Y", "neko34-1", "33", "neko-m-34", "1033", "2033"));
			Rows.Add( new MNP_NekoRow("Neko35", "0", "neko36-1", "neko-m-36", "1035", "2035", "2", "4", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko36", "0", "neko37-1", "neko-m-37", "1036", "2036", "1", "3", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko37", "0", "neko38-1", "neko-m-38", "1037", "2037", "3", "5", "Y", "neko40-1", "39", "neko-m-40", "1039", "2039"));
			Rows.Add( new MNP_NekoRow("Neko38", "0", "neko39-1", "neko-m-39", "1038", "2038", "2", "4", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko39", "0", "neko40-1", "neko-m-40", "1039", "2039", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko40", "0", "neko41-1", "neko-m-41", "1040", "2040", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko41", "0", "neko42-1", "neko-m-42", "1041", "2041", "3", "5", "Y", "neko41-1", "40", "neko-m-41", "1040", "2040"));
			Rows.Add( new MNP_NekoRow("Neko42", "0", "neko43-1", "neko-m-43", "1042", "2042", "3", "5", "Y", "neko44-1", "43", "neko-m-44", "1043", "2043"));
			Rows.Add( new MNP_NekoRow("Neko43", "0", "neko44-1", "neko-m-44", "1043", "2043", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko44", "0", "neko45-1", "neko-m-45", "1044", "2044", "2", "4", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko45", "0", "neko46-1", "neko-m-46", "1045", "2045", "2", "4", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko46", "0", "neko47-1", "neko-m-47", "1046", "2046", "2", "4", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko47", "0", "neko48-1", "neko-m-48", "1047", "2047", "2", "4", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko48", "0", "neko49-1", "neko-m-49", "1048", "2048", "1", "3", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko49", "0", "neko50-1", "neko-m-50", "1049", "2049", "1", "3", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko50", "0", "neko51-1", "neko-m-51", "1050", "2050", "1", "3", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko51", "0", "neko52-1", "neko-m-52", "1051", "2051", "1", "3", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko52", "0", "neko53-1", "neko-m-53", "1052", "2052", "1", "3", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko53", "0", "neko54-1", "neko-m-54", "1053", "2053", "2", "4", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko54", "0", "neko55-1", "neko-m-55", "1054", "2054", "3", "5", "Y", "neko56-1", "55", "neko-m-56", "1055", "2055"));
			Rows.Add( new MNP_NekoRow("Neko55", "0", "neko56-1", "neko-m-56", "1055", "2055", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko56", "0", "neko57-1", "neko-m-57", "1056", "2056", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko57", "0", "neko58-1", "neko-m-58", "1057", "2057", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko58", "0", "neko59-1", "neko-m-59", "1058", "2058", "3", "5", "Y", "neko57-1", "56", "neko-m-57", "1056", "2056"));
			Rows.Add( new MNP_NekoRow("Neko59", "0", "neko60-1", "neko-m-60", "1059", "2059", "3", "5", "Y", "neko58-1", "57", "neko-m-58", "1057", "2057"));
			Rows.Add( new MNP_NekoRow("Neko60", "0", "neko61-1", "neko-m-61", "1060", "2060", "1", "3", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko61", "0", "neko62-1", "neko-m-62", "1061", "2061", "3", "5", "Y", "neko63-1", "62", "neko-m-63", "1062", "2062"));
			Rows.Add( new MNP_NekoRow("Neko62", "0", "neko63-1", "neko-m-63", "1062", "2062", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko63", "0", "neko64-1", "neko-m-64", "1063", "2063", "2", "4", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko64", "0", "neko65-1", "neko-m-65", "1064", "2064", "1", "3", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko65", "0", "neko66-1", "neko-m-66", "1065", "2065", "2", "4", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko66", "0", "neko67-1", "neko-m-67", "1066", "2066", "3", "5", "Y", "neko68-1", "67", "neko-m-68", "1067", "2067"));
			Rows.Add( new MNP_NekoRow("Neko67", "0", "neko68-1", "neko-m-68", "1067", "2067", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko68", "0", "neko69-1", "neko-m-69", "1068", "2068", "1", "3", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko69", "0", "neko70-1", "neko-m-70", "1069", "2069", "1", "3", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko70", "0", "neko71-1", "neko-m-71", "1070", "2070", "1", "3", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko71", "0", "neko72-1", "neko-m-72", "1071", "2071", "2", "4", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko72", "0", "neko73-1", "neko-m-73", "1072", "2072", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko73", "0", "neko74-1", "neko-m-74", "1073", "2073", "3", "5", "Y", "neko73-1", "72", "neko-m-73", "1072", "2072"));
			Rows.Add( new MNP_NekoRow("Neko74", "0", "neko75-1", "neko-m-75", "1074", "2074", "3", "5", "Y", "neko76-1", "75", "neko-m-76", "1075", "2075"));
			Rows.Add( new MNP_NekoRow("Neko75", "0", "neko76-1", "neko-m-76", "1075", "2075", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko76", "0", "neko77-1", "neko-m-77", "1076", "2076", "3", "5", "Y", "neko80-1", "79", "neko-m-80", "1079", "2079"));
			Rows.Add( new MNP_NekoRow("Neko77", "0", "neko78-1", "neko-m-78", "1077", "2077", "3", "5", "Y", "neko79-1", "78", "neko-m-79", "1078", "2078"));
			Rows.Add( new MNP_NekoRow("Neko78", "0", "neko79-1", "neko-m-79", "1078", "2078", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko79", "0", "neko80-1", "neko-m-80", "1079", "2079", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko80", "0", "neko81-1", "neko-m-81", "1080", "2080", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko81", "0", "neko82-1", "neko-m-82", "1081", "2081", "1", "3", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko82", "0", "neko83-1", "neko-m-83", "1082", "2082", "3", "5", "Y", "neko81-1", "80", "neko-m-81", "1080", "2080"));
			Rows.Add( new MNP_NekoRow("Neko83", "0", "neko84-1", "neko-m-84", "1083", "2083", "2", "4", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko84", "0", "neko85-1", "neko-m-85", "1084", "2084", "2", "4", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko85", "0", "neko86-1", "neko-m-86", "1085", "2085", "1", "3", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko86", "0", "neko87-1", "neko-m-87", "1086", "2086", "3", "5", "Y", "neko88-1", "87", "neko-m-88", "1087", "2087"));
			Rows.Add( new MNP_NekoRow("Neko87", "0", "neko88-1", "neko-m-88", "1087", "2087", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko88", "0", "neko89-1", "neko-m-89", "1088", "2088", "3", "5", "Y", "neko92-1", "91", "neko-m-92", "1091", "2091"));
			Rows.Add( new MNP_NekoRow("Neko89", "0", "neko90-1", "neko-m-90", "1089", "2089", "2", "4", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko90", "0", "neko91-1", "neko-m-91", "1090", "2090", "2", "4", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko91", "0", "neko92-1", "neko-m-92", "1091", "2091", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko92", "0", "neko93-1", "neko-m-93", "1092", "2092", "1", "3", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko93", "0", "neko94-1", "neko-m-94", "1093", "2093", "2", "4", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko94", "0", "neko95-1", "neko-m-95", "1094", "2094", "3", "5", "Y", "neko96-1", "95", "neko-m-96", "1095", "2095"));
			Rows.Add( new MNP_NekoRow("Neko95", "0", "neko96-1", "neko-m-96", "1095", "2095", "", "", "", "neko96-1", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko96", "0", "neko97-1", "neko-m-97", "1096", "2096", "3", "5", "Y", "neko97-1", "96", "neko-m-97", "1096", "2096"));
			Rows.Add( new MNP_NekoRow("Neko97", "0", "neko98-1", "neko-m-98", "1097", "2097", "", "", "", "neko98-1", "97", "neko-m-98", "1097", "2097"));
			Rows.Add( new MNP_NekoRow("Neko98", "0", "neko99-1", "neko-m-99", "1098", "2098", "3", "5", "Y", "neko99-1", "98", "neko-m-99", "1098", "2098"));
			Rows.Add( new MNP_NekoRow("Neko99", "0", "neko100-1", "neko-m-100", "1099", "2099", "3", "5", "Y", "neko100-1", "99", "neko-m-100", "1099", "2099"));
			Rows.Add( new MNP_NekoRow("Neko100", "0", "neko101-1", "neko-m-101", "1100", "2100", "3", "5", "Y", "neko101-1", "100", "neko-m-101", "1100", "2100"));
			Rows.Add( new MNP_NekoRow("Neko101", "0", "neko102-1", "neko-m-102", "1101", "2101", "", "", "", "neko102-1", "101", "neko-m-102", "1101", "2101"));
			Rows.Add( new MNP_NekoRow("Neko102", "0", "neko103-1", "neko-m-103", "1102", "2102", "3", "5", "Y", "neko103-1", "102", "neko-m-103", "1102", "2102"));
			Rows.Add( new MNP_NekoRow("Neko103", "0", "neko104-1", "neko-m-104", "1103", "2103", "3", "5", "Y", "neko104-1", "103", "neko-m-104", "1103", "2103"));
			Rows.Add( new MNP_NekoRow("Neko104", "0", "neko105-1", "neko-m-105", "1104", "2104", "3", "5", "Y", "neko105-1", "104", "neko-m-105", "1104", "2104"));
			Rows.Add( new MNP_NekoRow("Neko105", "0", "neko106-1", "neko-m-106", "1105", "2105", "3", "5", "Y", "neko106-1", "105", "neko-m-106", "1105", "2105"));
			Rows.Add( new MNP_NekoRow("Neko106", "0", "neko107-1", "neko-m-107", "1106", "2106", "3", "5", "Y", "neko107-1", "106", "neko-m-107", "1106", "2106"));
			Rows.Add( new MNP_NekoRow("Neko107", "0", "neko108-1", "neko-m-108", "1107", "2107", "3", "5", "Y", "neko108-1", "107", "neko-m-108", "1107", "2107"));
			Rows.Add( new MNP_NekoRow("Neko108", "0", "neko109-1", "neko-m-109", "1108", "2108", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko109", "0", "neko110-1", "neko-m-110", "1109", "2109", "3", "5", "Y", "neko109-1", "108", "neko-m-109", "1108", "2108"));
			Rows.Add( new MNP_NekoRow("Neko110", "0", "neko111-1", "neko-m-111", "1110", "2110", "3", "5", "Y", "neko112-1", "111", "neko-m-112", "1111", "2111"));
			Rows.Add( new MNP_NekoRow("Neko111", "0", "neko112-1", "neko-m-112", "1111", "2111", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko112", "0", "neko113-1", "neko-m-113", "1112", "2112", "1", "3", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko113", "0", "neko114-1", "neko-m-114", "1113", "2113", "2", "4", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko114", "0", "neko115-1", "neko-m-115", "1114", "2114", "3", "5", "Y", "neko116-1", "115", "neko-m-116", "1115", "2115"));
			Rows.Add( new MNP_NekoRow("Neko115", "0", "neko116-1", "neko-m-116", "1115", "2115", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko116", "0", "neko117-1", "neko-m-117", "1116", "2116", "3", "5", "Y", "neko120-1", "119", "neko-m-120", "1119", "2119"));
			Rows.Add( new MNP_NekoRow("Neko117", "0", "neko118-1", "neko-m-118", "1117", "2117", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko118", "0", "neko119-1", "neko-m-119", "1118", "2118", "3", "5", "Y", "neko118-1", "117", "neko-m-118", "1117", "2117"));
			Rows.Add( new MNP_NekoRow("Neko119", "0", "neko120-1", "neko-m-120", "1119", "2119", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko120", "0", "neko121-1", "neko-m-121", "1120", "2120", "1", "3", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko121", "0", "neko122-3", "neko-m-122", "1121", "2121", "1", "3", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko122", "0", "neko123-1", "neko-m-123", "1122", "2122", "1", "3", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko123", "0", "neko124-1", "neko-m-124", "1123", "2123", "2", "4", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko124", "0", "neko125-1", "neko-m-125", "1124", "2124", "1", "3", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko125", "0", "neko126-1", "neko-m-126", "1125", "2125", "2", "4", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko126", "0", "neko127-1", "neko-m-127", "1126", "2126", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko127", "0", "neko128-1", "neko-m-128", "1127", "2127", "3", "5", "Y", "neko127-1", "126", "neko-m-127", "1126", "2126"));
			Rows.Add( new MNP_NekoRow("Neko128", "0", "neko129-1", "neko-m-129", "1128", "2128", "1", "3", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko129", "0", "neko130-1", "neko-m-130", "1129", "2129", "2", "4", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko130", "0", "neko131-1", "neko-m-131", "1130", "2130", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko131", "0", "neko132-1", "neko-m-132", "1131", "2131", "3", "5", "Y", "neko131-1", "130", "neko-m-131", "1130", "2130"));
			Rows.Add( new MNP_NekoRow("Neko132", "0", "neko133-1", "neko-m-133", "1132", "2132", "3", "5", "Y", "neko134-1", "133", "neko-m-134", "1133", "2133"));
			Rows.Add( new MNP_NekoRow("Neko133", "0", "neko134-1", "neko-m-134", "1133", "2133", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko134", "0", "neko135-1", "neko-m-135", "1134", "2134", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko135", "0", "neko136-1", "neko-m-136", "1135", "2135", "3", "5", "Y", "neko135-1", "134", "neko-m-135", "1134", "2134"));
			Rows.Add( new MNP_NekoRow("Neko136", "0", "neko137-1", "neko-m-137", "1136", "2136", "3", "5", "Y", "neko140-1", "139", "neko-m-140", "1139", "2139"));
			Rows.Add( new MNP_NekoRow("Neko137", "0", "neko138-1", "neko-m-138", "1137", "2137", "3", "5", "Y", "neko139-1", "138", "neko-m-139", "1138", "2138"));
			Rows.Add( new MNP_NekoRow("Neko138", "0", "neko139-1", "neko-m-139", "1138", "2138", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko139", "0", "neko140-1", "neko-m-140", "1139", "2139", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko140", "0", "neko141-1", "neko-m-141", "1140", "2140", "1", "3", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko141", "0", "neko142-1", "neko-m-142", "1141", "2141", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko142", "0", "neko143-1", "neko-m-143", "1142", "2142", "2", "4", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko143", "0", "neko144-1", "neko-m-144", "1143", "2143", "3", "5", "Y", "neko142-1", "141", "neko-m-142", "1141", "2141"));
			Rows.Add( new MNP_NekoRow("Neko144", "0", "neko145-1", "neko-m-145", "1144", "2144", "5", "5", "Y", "neko145-1", "144", "neko-m-145", "1144", "2144"));
			Rows.Add( new MNP_NekoRow("Neko145", "0", "neko146-1", "neko-m-146", "1145", "2145", "5", "5", "Y", "neko146-1", "145", "neko-m-146", "1145", "2145"));
			Rows.Add( new MNP_NekoRow("Neko146", "0", "neko147-1", "neko-m-147", "1146", "2146", "5", "5", "Y", "neko147-1", "146", "neko-m-147", "1146", "2146"));
			Rows.Add( new MNP_NekoRow("Neko147", "0", "neko148-1", "neko-m-148", "1147", "2147", "5", "5", "Y", "neko148-1", "147", "neko-m-148", "1147", "2147"));
			Rows.Add( new MNP_NekoRow("Neko148", "0", "neko149-1", "neko-m-149", "1148", "2148", "5", "5", "Y", "neko149-1", "148", "neko-m-149", "1148", "2148"));
			Rows.Add( new MNP_NekoRow("Neko149", "0", "neko150-1", "neko-m-150", "1149", "2149", "5", "5", "Y", "neko150-1", "149", "neko-m-150", "1149", "2149"));
			Rows.Add( new MNP_NekoRow("Neko150", "0", "neko151-1", "neko-m-151", "1150", "2150", "2", "4", "N", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko151", "0", "neko152-1", "neko-m-152", "1151", "2151", "3", "5", "Y", "neko152-1", "151", "neko-m-152", "1151", "2151"));
			Rows.Add( new MNP_NekoRow("Neko152", "0", "neko153-1", "neko-m-153", "1152", "2152", "3", "5", "Y", "neko153-1", "152", "neko-m-153", "1152", "2152"));
			Rows.Add( new MNP_NekoRow("Neko153", "0", "neko154-1", "neko-m-154", "1153", "2153", "3", "5", "Y", "neko154-1", "153", "neko-m-154", "1153", "2153"));
			Rows.Add( new MNP_NekoRow("Neko154", "0", "neko155-1", "neko-m-155", "1154", "2154", "3", "5", "Y", "neko155-1", "154", "neko-m-155", "1154", "2154"));
			Rows.Add( new MNP_NekoRow("Neko155", "0", "neko156-1", "neko-m-156", "1155", "2155", "3", "5", "Y", "neko156-1", "155", "neko-m-156", "1155", "2155"));
			Rows.Add( new MNP_NekoRow("Neko156", "0", "neko157-1", "neko-m-157", "1156", "2156", "5", "5", "Y", "neko157-1", "156", "neko-m-157", "1156", "2156"));
			Rows.Add( new MNP_NekoRow("Neko157", "0", "neko158-1", "neko-m-158", "1157", "2157", "5", "5", "Y", "neko158-1", "157", "neko-m-158", "1157", "2157"));
			Rows.Add( new MNP_NekoRow("Neko158", "0", "neko159-1", "neko-m-159", "1158", "2158", "3", "5", "Y", "neko159-1", "158", "neko-m-159", "1158", "2158"));
			Rows.Add( new MNP_NekoRow("Neko159", "0", "neko160-1", "neko-m-160", "1159", "2159", "3", "5", "Y", "neko160-1", "159", "neko-m-160", "1159", "2159"));
			Rows.Add( new MNP_NekoRow("Neko160", "0", "neko161-1", "neko-m-161", "1160", "2160", "3", "5", "Y", "neko161-1", "160", "neko-m-161", "1160", "2160"));
			Rows.Add( new MNP_NekoRow("Neko161", "0", "neko162-1", "neko-m-162", "1161", "2161", "3", "5", "Y", "neko162-1", "161", "neko-m-162", "1161", "2161"));
			Rows.Add( new MNP_NekoRow("Neko162", "0", "neko163-1", "neko-m-163", "1162", "2162", "3", "5", "Y", "neko163-1", "162", "neko-m-163", "1162", "2162"));
			Rows.Add( new MNP_NekoRow("Neko163", "0", "neko164-1", "neko-m-164", "1163", "2163", "3", "5", "Y", "neko164-1", "163", "neko-m-164", "1163", "2163"));
			Rows.Add( new MNP_NekoRow("Neko164", "0", "neko165-1", "neko-m-165", "1164", "2164", "3", "5", "Y", "neko165-1", "164", "neko-m-165", "1164", "2164"));
			Rows.Add( new MNP_NekoRow("Neko165", "0", "neko166-1", "neko-m-166", "1165", "2165", "3", "5", "Y", "neko166-1", "165", "neko-m-166", "1165", "2165"));
			Rows.Add( new MNP_NekoRow("Neko166", "0", "neko167-1", "neko-m-167", "1166", "2166", "3", "5", "Y", "neko167-1", "166", "neko-m-167", "1166", "2166"));
			Rows.Add( new MNP_NekoRow("Neko167", "0", "neko168-1", "neko-m-168", "1167", "2167", "3", "5", "Y", "neko168-1", "167", "neko-m-168", "1167", "2167"));
			Rows.Add( new MNP_NekoRow("Neko168", "0", "neko169-1", "neko-m-169", "1168", "2168", "3", "5", "Y", "neko169-1", "168", "neko-m-169", "1168", "2168"));
			Rows.Add( new MNP_NekoRow("Neko169", "0", "neko170-1", "neko-m-170", "1169", "2169", "3", "5", "Y", "neko170-1", "169", "neko-m-170", "1169", "2169"));
			Rows.Add( new MNP_NekoRow("Neko170", "0", "neko171-1", "neko-m-171", "1170", "2170", "3", "5", "Y", "neko171-1", "170", "neko-m-171", "1170", "2170"));
			Rows.Add( new MNP_NekoRow("Neko171", "0", "neko172-1", "neko-m-172", "1171", "2171", "3", "5", "Y", "neko172-1", "171", "neko-m-172", "1171", "2171"));
			Rows.Add( new MNP_NekoRow("Neko172", "0", "neko173-1", "neko-m-173", "1172", "2172", "3", "5", "Y", "neko173-1", "172", "neko-m-173", "1172", "2172"));
			Rows.Add( new MNP_NekoRow("Neko173", "0", "neko174-1", "neko-m-174", "1173", "2173", "3", "5", "Y", "neko174-1", "173", "neko-m-174", "1173", "2173"));
			Rows.Add( new MNP_NekoRow("Neko174", "0", "neko175-1", "neko-m-175", "1174", "2174", "3", "5", "Y", "neko175-1", "174", "neko-m-175", "1174", "2174"));
			Rows.Add( new MNP_NekoRow("Neko175", "0", "neko176-1", "neko-m-176", "1175", "2175", "3", "5", "Y", "neko176-1", "175", "neko-m-176", "1175", "2175"));
			Rows.Add( new MNP_NekoRow("Neko176", "0", "neko177-1", "neko-m-177", "1176", "2176", "3", "5", "Y", "neko177-1", "176", "neko-m-177", "1176", "2176"));
			Rows.Add( new MNP_NekoRow("Neko177", "0", "neko178-1", "neko-m-178", "1177", "2177", "3", "5", "Y", "neko178-1", "177", "neko-m-178", "1177", "2177"));
			Rows.Add( new MNP_NekoRow("Neko178", "0", "neko179-1", "neko-m-179", "1178", "2178", "3", "5", "Y", "neko179-1", "178", "neko-m-179", "1178", "2178"));
			Rows.Add( new MNP_NekoRow("Neko179", "0", "neko180-1", "neko-m-180", "1179", "2179", "3", "5", "Y", "neko180-1", "179", "neko-m-180", "1179", "2179"));
			Rows.Add( new MNP_NekoRow("Neko180", "0", "neko181-1", "neko-m-181", "1180", "2180", "3", "5", "Y", "neko181-1", "180", "neko-m-181", "1180", "2180"));
			Rows.Add( new MNP_NekoRow("Neko181", "0", "neko182-1", "neko-m-182", "1181", "2181", "3", "5", "Y", "neko182-1", "181", "neko-m-182", "1181", "2181"));
			Rows.Add( new MNP_NekoRow("Neko182", "0", "neko183-1", "neko-m-183", "1182", "2182", "3", "5", "Y", "neko183-1", "182", "neko-m-183", "1182", "2182"));
			Rows.Add( new MNP_NekoRow("Neko183", "0", "neko184-1", "neko-m-184", "1183", "2183", "3", "5", "Y", "neko184-1", "183", "neko-m-184", "1183", "2183"));
			Rows.Add( new MNP_NekoRow("Neko184", "0", "neko185-1", "neko-m-185", "1184", "2184", "3", "5", "Y", "neko185-1", "184", "neko-m-185", "1184", "2184"));
			Rows.Add( new MNP_NekoRow("Neko185", "0", "neko186-1", "neko-m-186", "1185", "2185", "3", "5", "Y", "neko186-1", "185", "neko-m-186", "1185", "2185"));
			Rows.Add( new MNP_NekoRow("Neko186", "0", "neko187-1", "neko-m-187", "1186", "2186", "3", "5", "Y", "neko187-1", "186", "neko-m-187", "1186", "2186"));
			Rows.Add( new MNP_NekoRow("Neko187", "0", "neko188-1", "neko-m-188", "1187", "2187", "3", "5", "Y", "neko188-1", "187", "neko-m-188", "1187", "2187"));
			Rows.Add( new MNP_NekoRow("Neko188", "0", "neko189-1", "neko-m-189", "1188", "2188", "3", "5", "Y", "neko189-1", "188", "neko-m-189", "1188", "2188"));
			Rows.Add( new MNP_NekoRow("Neko189", "0", "neko190-1", "neko-m-190", "1189", "2189", "3", "5", "Y", "neko190-1", "189", "neko-m-190", "1189", "2189"));
			Rows.Add( new MNP_NekoRow("Neko190", "0", "neko191-1", "neko-m-191", "1190", "2190", "3", "5", "Y", "neko191-1", "190", "neko-m-191", "1190", "2190"));
			Rows.Add( new MNP_NekoRow("Neko191", "0", "neko192-1", "neko-m-192", "1191", "2191", "3", "5", "Y", "neko192-1", "191", "neko-m-192", "1191", "2191"));
			Rows.Add( new MNP_NekoRow("Neko192", "0", "neko193-1", "neko-m-193", "1192", "2192", "3", "5", "Y", "neko193-1", "192", "neko-m-193", "1192", "2192"));
			Rows.Add( new MNP_NekoRow("Neko193", "0", "neko194-1", "neko-m-194", "1193", "2193", "3", "5", "Y", "neko194-1", "193", "neko-m-194", "1193", "2193"));
			Rows.Add( new MNP_NekoRow("Neko194", "0", "neko195-1", "neko-m-195", "1194", "2194", "3", "5", "Y", "neko195-1", "194", "neko-m-195", "1194", "2194"));
			Rows.Add( new MNP_NekoRow("Neko195", "0", "neko196-1", "neko-m-196", "1195", "2195", "3", "5", "Y", "neko196-1", "195", "neko-m-196", "1195", "2195"));
			Rows.Add( new MNP_NekoRow("Neko196", "0", "neko197-1", "neko-m-197", "1196", "2196", "3", "5", "Y", "neko197-1", "196", "neko-m-197", "1196", "2196"));
			Rows.Add( new MNP_NekoRow("Neko197", "0", "neko198-1", "neko-m-198", "1197", "2197", "3", "5", "Y", "neko198-1", "197", "neko-m-198", "1197", "2197"));
			Rows.Add( new MNP_NekoRow("Neko198", "0", "neko199-1", "neko-m-199", "1198", "2198", "3", "5", "Y", "neko199-1", "198", "neko-m-199", "1198", "2198"));
			Rows.Add( new MNP_NekoRow("Neko199", "0", "neko200-1", "neko-m-200", "1199", "2199", "3", "5", "Y", "neko202-1", "201", "neko-m-202", "1201", "2201"));
			Rows.Add( new MNP_NekoRow("Neko200", "0", "neko201-1", "neko-m-201", "1200", "2200", "3", "5", "Y", "neko203-1", "202", "neko-m-202", "1202", "2202"));
			Rows.Add( new MNP_NekoRow("Neko201", "0", "neko202-1", "neko-m-202", "1201", "2201", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko202", "0", "neko203-1", "neko-m-203", "1202", "2202", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko203", "0", "neko204-1", "neko-m-204", "1203", "2203", "3", "5", "Y", "neko206-1", "205", "neko-m-206", "1205", "2205"));
			Rows.Add( new MNP_NekoRow("Neko204", "0", "neko205-1", "neko-m-205", "1204", "2204", "3", "5", "Y", "neko207-1", "206", "neko-m-207", "1206", "2206"));
			Rows.Add( new MNP_NekoRow("Neko205", "0", "neko206-1", "neko-m-206", "1205", "2205", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko206", "0", "neko207-1", "neko-m-207", "1206", "2206", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoRow("Neko207", "0", "neko208-1", "neko-m-208", "1207", "2207", "3", "5", "Y", "neko208-1", "207", "neko-m-208", "1207", "2207"));
			Rows.Add( new MNP_NekoRow("Neko208", "0", "neko209-1", "neko-m-209", "1208", "2208", "5", "5", "Y", "neko209-1", "208", "neko-m-209", "1208", "2208"));
			Rows.Add( new MNP_NekoRow("Neko209", "0", "neko214-1", "neko-m-214", "1213", "2213", "5", "5", "Y", "neko214-1", "209", "neko-m-214", "1213", "2213"));
			Rows.Add( new MNP_NekoRow("Neko210", "0", "neko211-1", "neko-m-211", "1210", "2210", "3", "5", "Y", "neko211-1", "210", "neko-m-211", "1210", "2210"));
			Rows.Add( new MNP_NekoRow("Neko211", "0", "neko212-1", "neko-m-212", "1211", "2211", "3", "5", "Y", "neko212-1", "211", "neko-m-212", "1211", "2211"));
			Rows.Add( new MNP_NekoRow("Neko212", "0", "neko213-1", "neko-m-213", "1212", "2212", "3", "5", "Y", "neko213-1", "212", "neko-m-213", "1212", "2212"));
			Rows.Add( new MNP_NekoRow("Neko213", "0", "neko210-1", "neko-m-210", "1209", "2209", "3", "5", "Y", "neko210-1", "213", "neko-m-210", "1209", "2209"));
			Rows.Add( new MNP_NekoRow("Neko214", "0", "neko215-1", "neko-m-215", "1214", "2214", "3", "5", "Y", "neko215-1", "214", "neko-m-215", "1214", "2214"));
			Rows.Add( new MNP_NekoRow("Neko215", "0", "neko216-1", "neko-m-216", "1215", "2215", "3", "5", "Y", "neko216-1", "215", "neko-m-216", "1215", "2215"));
			Rows.Add( new MNP_NekoRow("Neko216", "0", "neko217-1", "neko-m-217", "1216", "2216", "3", "5", "Y", "neko217-1", "216", "neko-m-217", "1216", "2216"));
			Rows.Add( new MNP_NekoRow("Neko217", "0", "neko218-1", "neko-m-218", "1217", "2217", "3", "5", "Y", "neko218-1", "217", "neko-m-218", "1217", "2217"));
			Rows.Add( new MNP_NekoRow("Neko218", "0", "neko219-1", "neko-m-219", "1218", "2218", "5", "5", "Y", "neko219-1", "218", "neko-m-219", "1218", "2218"));
			Rows.Add( new MNP_NekoRow("Neko219", "0", "neko220-1", "neko-m-220", "1219", "2219", "3", "5", "Y", "neko220-1", "219", "neko-m-220", "1219", "2219"));
			Rows.Add( new MNP_NekoRow("Neko220", "0", "neko221-1", "neko-m-221", "1220", "2220", "3", "5", "Y", "neko221-1", "220", "neko-m-221", "1220", "2220"));
			Rows.Add( new MNP_NekoRow("Neko221", "0", "neko222-1", "neko-m-222", "1221", "2221", "3", "5", "Y", "neko222-1", "221", "neko-m-222", "1221", "2221"));
			Rows.Add( new MNP_NekoRow("Neko222", "0", "neko223-1", "neko-m-223", "1222", "2222", "3", "5", "Y", "neko223-1", "222", "neko-m-223", "1222", "2222"));
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
		public MNP_NekoRow GetRow(rowIds in_RowID)
		{
			MNP_NekoRow ret = null;
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
		public MNP_NekoRow GetRow(string in_RowString)
		{
			MNP_NekoRow ret = null;
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
