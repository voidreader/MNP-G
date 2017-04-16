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
	public class MNP_NekoSkillRow : IGoogle2uRow
	{
		public int _tid;
		public int _star;
		public string _skill;
		public string _detail;
		public string _grade;
		public string _phrase;
		public string _skill2;
		public string _detail2;
		public string _grade2;
		public string _phrase2;
		public string _skill3;
		public string _detail3;
		public string _grade3;
		public string _phrase3;
		public MNP_NekoSkillRow(string __id, string __tid, string __star, string __skill, string __detail, string __grade, string __phrase, string __skill2, string __detail2, string __grade2, string __phrase2, string __skill3, string __detail3, string __grade3, string __phrase3) 
		{
			{
			int res;
				if(int.TryParse(__tid, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_tid = res;
				else
					Debug.LogError("Failed To Convert _tid string: "+ __tid +" to int");
			}
			{
			int res;
				if(int.TryParse(__star, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_star = res;
				else
					Debug.LogError("Failed To Convert _star string: "+ __star +" to int");
			}
			_skill = __skill.Trim();
			_detail = __detail.Trim();
			_grade = __grade.Trim();
			_phrase = __phrase.Trim();
			_skill2 = __skill2.Trim();
			_detail2 = __detail2.Trim();
			_grade2 = __grade2.Trim();
			_phrase2 = __phrase2.Trim();
			_skill3 = __skill3.Trim();
			_detail3 = __detail3.Trim();
			_grade3 = __grade3.Trim();
			_phrase3 = __phrase3.Trim();
		}

		public int Length { get { return 14; } }

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
					ret = _star.ToString();
					break;
				case 2:
					ret = _skill.ToString();
					break;
				case 3:
					ret = _detail.ToString();
					break;
				case 4:
					ret = _grade.ToString();
					break;
				case 5:
					ret = _phrase.ToString();
					break;
				case 6:
					ret = _skill2.ToString();
					break;
				case 7:
					ret = _detail2.ToString();
					break;
				case 8:
					ret = _grade2.ToString();
					break;
				case 9:
					ret = _phrase2.ToString();
					break;
				case 10:
					ret = _skill3.ToString();
					break;
				case 11:
					ret = _detail3.ToString();
					break;
				case 12:
					ret = _grade3.ToString();
					break;
				case 13:
					ret = _phrase3.ToString();
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
				case "star":
					ret = _star.ToString();
					break;
				case "skill":
					ret = _skill.ToString();
					break;
				case "detail":
					ret = _detail.ToString();
					break;
				case "grade":
					ret = _grade.ToString();
					break;
				case "phrase":
					ret = _phrase.ToString();
					break;
				case "skill2":
					ret = _skill2.ToString();
					break;
				case "detail2":
					ret = _detail2.ToString();
					break;
				case "grade2":
					ret = _grade2.ToString();
					break;
				case "phrase2":
					ret = _phrase2.ToString();
					break;
				case "skill3":
					ret = _skill3.ToString();
					break;
				case "detail3":
					ret = _detail3.ToString();
					break;
				case "grade3":
					ret = _grade3.ToString();
					break;
				case "phrase3":
					ret = _phrase3.ToString();
					break;
			}

			return ret;
		}
		public override string ToString()
		{
			string ret = System.String.Empty;
			ret += "{" + "tid" + " : " + _tid.ToString() + "} ";
			ret += "{" + "star" + " : " + _star.ToString() + "} ";
			ret += "{" + "skill" + " : " + _skill.ToString() + "} ";
			ret += "{" + "detail" + " : " + _detail.ToString() + "} ";
			ret += "{" + "grade" + " : " + _grade.ToString() + "} ";
			ret += "{" + "phrase" + " : " + _phrase.ToString() + "} ";
			ret += "{" + "skill2" + " : " + _skill2.ToString() + "} ";
			ret += "{" + "detail2" + " : " + _detail2.ToString() + "} ";
			ret += "{" + "grade2" + " : " + _grade2.ToString() + "} ";
			ret += "{" + "phrase2" + " : " + _phrase2.ToString() + "} ";
			ret += "{" + "skill3" + " : " + _skill3.ToString() + "} ";
			ret += "{" + "detail3" + " : " + _detail3.ToString() + "} ";
			ret += "{" + "grade3" + " : " + _grade3.ToString() + "} ";
			ret += "{" + "phrase3" + " : " + _phrase3.ToString() + "} ";
			return ret;
		}
	}
	public sealed class MNP_NekoSkill : IGoogle2uDB
	{
		public enum rowIds {
			Skill01, Skill02, Skill03, Skill12, Skill13, Skill14, Skill22, Skill23, Skill24, Skill32, Skill33, Skill34, Skill35, Skill41, Skill42, Skill43, Skill52, Skill53
			, Skill54, Skill63, Skill64, Skill65, Skill82, Skill83, Skill84, Skill93, Skill94, Skill95, Skill111, Skill112, Skill113, Skill142, Skill143, Skill144, Skill133, Skill134, Skill135, Skill151
			, Skill152, Skill153, Skill162, Skill163, Skill164, Skill165, Skill171, Skill172, Skill173, Skill174, Skill175, Skill181, Skill182, Skill183, Skill184, Skill185, Skill201, Skill202, Skill203, Skill213
			, Skill214, Skill215, Skill221, Skill222, Skill223, Skill224, Skill243, Skill244, Skill245, Skill251, Skill252, Skill253, Skill254, Skill255, Skill261, Skill262, Skill263, Skill291, Skill292, Skill293
			, Skill294, Skill295, Skill301, Skill302, Skill303, Skill304, Skill305, Skill311, Skill312, Skill313, Skill314, Skill315, Skill321, Skill322, Skill323, Skill343, Skill344, Skill345, Skill352, Skill353
			, Skill354, Skill361, Skill362, Skill363, Skill373, Skill374, Skill375, Skill381, Skill382, Skill383, Skill384, Skill413, Skill414, Skill415, Skill421, Skill422, Skill423, Skill424, Skill425, Skill442
			, Skill443, Skill444, Skill445, Skill451, Skill452, Skill453, Skill454, Skill455, Skill461, Skill462, Skill463, Skill464, Skill465, Skill471, Skill472, Skill473, Skill474, Skill475, Skill481, Skill482
			, Skill483, Skill484, Skill485, Skill491, Skill492, Skill493, Skill494, Skill495, Skill501, Skill502, Skill503, Skill504, Skill505, Skill511, Skill512, Skill513, Skill514, Skill515, Skill521, Skill522
			, Skill523, Skill524, Skill525, Skill531, Skill532, Skill533, Skill534, Skill543, Skill544, Skill545, Skill583, Skill584, Skill585, Skill593, Skill594, Skill595, Skill601, Skill602, Skill603, Skill613
			, Skill614, Skill615, Skill621, Skill632, Skill633, Skill634, Skill635, Skill641, Skill642, Skill643, Skill644, Skill645, Skill651, Skill652, Skill653, Skill654, Skill663, Skill664, Skill665, Skill681
			, Skill682, Skill683, Skill684, Skill685, Skill691, Skill692, Skill693, Skill701, Skill702, Skill703, Skill712, Skill713, Skill714, Skill733, Skill734, Skill735, Skill741, Skill742, Skill743, Skill744
			, Skill745, Skill763, Skill764, Skill765, Skill771, Skill772, Skill773, Skill774, Skill775, Skill811, Skill812, Skill813, Skill814, Skill815, Skill821, Skill822, Skill823, Skill824, Skill825, Skill831
			, Skill832, Skill833, Skill834, Skill835, Skill841, Skill842, Skill843, Skill844, Skill845, Skill851, Skill852, Skill853, Skill854, Skill855, Skill861, Skill862, Skill863, Skill864, Skill865, Skill883
			, Skill884, Skill885, Skill891, Skill892, Skill893, Skill894, Skill895, Skill901, Skill902, Skill903, Skill904, Skill921, Skill922, Skill923, Skill932, Skill933, Skill934, Skill935, Skill941, Skill942
			, Skill943, Skill944, Skill945, Skill1093, Skill1094, Skill1095, Skill1101, Skill1102, Skill1103, Skill1104, Skill1105, Skill1111, Skill1112, Skill1113, Skill1114, Skill1115, Skill1121, Skill1122, Skill1123, Skill1124
			, Skill1125, Skill1131, Skill1132, Skill1133, Skill1134, Skill1143, Skill1144, Skill1145, Skill1163, Skill1164, Skill1165, Skill1183, Skill1184, Skill1185, Skill1201, Skill1202, Skill1203, Skill1211, Skill1212, Skill1213
			, Skill1214, Skill1215, Skill1221, Skill1222, Skill1223, Skill1224, Skill1225, Skill1231, Skill1232, Skill1233, Skill1234, Skill1235, Skill1241, Skill1242, Skill1243, Skill1244, Skill1245, Skill1251, Skill1252, Skill1253
			, Skill1254, Skill1273, Skill1274, Skill1275, Skill1281, Skill1282, Skill1283, Skill1284, Skill1285, Skill1291, Skill1292, Skill1293, Skill1294, Skill1313, Skill1314, Skill1315, Skill1321, Skill1322, Skill1323, Skill1324
			, Skill1325, Skill1353, Skill1354, Skill1355, Skill1361, Skill1362, Skill1363, Skill1364, Skill1365, Skill1371, Skill1372, Skill1373, Skill1374, Skill1375, Skill1401, Skill1402, Skill1403, Skill1422, Skill1423, Skill1424
			, Skill1425, Skill1431, Skill1432, Skill1433, Skill1434, Skill1435, Skill1441, Skill1442, Skill1443, Skill1444, Skill1445, Skill7011, Skill7012, Skill7013, Skill7014, Skill7015, Skill7025, Skill7035, Skill7041, Skill7042
			, Skill7043, Skill7044, Skill7045, Skill1451, Skill1452, Skill1453, Skill1454, Skill1455, Skill1501, Skill1502, Skill1503, Skill1504, Skill1505, Skill1513, Skill1514, Skill1515, Skill1523, Skill1524, Skill1525, Skill1533
			, Skill1534, Skill1535, Skill1543, Skill1544, Skill1545, Skill1553, Skill1554, Skill1555, Skill1565, Skill1573, Skill1574, Skill1575, Skill1583, Skill1584, Skill1585, Skill1593, Skill1594, Skill1595, Skill1603, Skill1604
			, Skill1605, Skill1613, Skill1614, Skill1615, Skill1623, Skill1624, Skill1625, Skill1633, Skill1634, Skill1635, Skill1463, Skill1464, Skill1465, Skill1473, Skill1474, Skill1475, Skill1481, Skill1482, Skill1483, Skill1484
			, Skill1485, Skill1491, Skill1492, Skill1493, Skill1494, Skill1495, Skill1643, Skill1644, Skill1645, Skill963, Skill964, Skill965, Skill983, Skill984, Skill985, Skill993, Skill994, Skill995, Skill1003, Skill1004
			, Skill1005, Skill1023, Skill1024, Skill1025, Skill1033, Skill1034, Skill1035, Skill1045, Skill1063, Skill1064, Skill1065, Skill1073, Skill1074, Skill1075, Skill1653, Skill1654, Skill1655, Skill1663, Skill1664, Skill1665
			, Skill1673, Skill1674, Skill1675, Skill1683, Skill1684, Skill1685, Skill1693, Skill1694, Skill1695, Skill1703, Skill1704, Skill1705, Skill1713, Skill1714, Skill1715, Skill1723, Skill1724, Skill1725, Skill1733, Skill1734
			, Skill1735, Skill1743, Skill1744, Skill1745, Skill1753, Skill1754, Skill1755, Skill1763, Skill1764, Skill1765, Skill1773, Skill1774, Skill1775, Skill1783, Skill1784, Skill1785, Skill1793, Skill1794, Skill1795, Skill1803
			, Skill1804, Skill1805, Skill1813, Skill1814, Skill1815, Skill1823, Skill1824, Skill1825, Skill1833, Skill1834, Skill1835, Skill1843, Skill1844, Skill1845, Skill1853, Skill1854, Skill1855, Skill1863, Skill1864, Skill1865
			, Skill1873, Skill1874, Skill1875, Skill1883, Skill1884, Skill1885, Skill1893, Skill1894, Skill1895, Skill1903, Skill1904, Skill1905, Skill1913, Skill1914, Skill1915, Skill1923, Skill1924, Skill1925, Skill1935, Skill1943
			, Skill1944, Skill1945, Skill1953, Skill1954, Skill1955, Skill1963, Skill1964, Skill1965, Skill1973, Skill1974, Skill1975, Skill1983, Skill1984, Skill1985, Skill1053, Skill1054, Skill1055, Skill1993, Skill1994, Skill1995
			, Skill2003, Skill2004, Skill2005, Skill2033, Skill2034, Skill2035, Skill2043, Skill2044, Skill2045, Skill2073, Skill2074, Skill2075, Skill2085, Skill2095, Skill2103, Skill2104, Skill2105, Skill2113, Skill2114, Skill2115
			, Skill2123, Skill2124, Skill2125, Skill2133, Skill2134, Skill2135, Skill2143, Skill2144, Skill2145, Skill2153, Skill2154, Skill2155, Skill2163, Skill2164, Skill2165, Skill2173, Skill2174, Skill2175, Skill2185, Skill2193
			, Skill2194, Skill2195, Skill2203, Skill2204, Skill2205, Skill2213, Skill2214, Skill2215, Skill2223, Skill2224, Skill2225
		};
		public string [] rowNames = {
			"Skill01", "Skill02", "Skill03", "Skill12", "Skill13", "Skill14", "Skill22", "Skill23", "Skill24", "Skill32", "Skill33", "Skill34", "Skill35", "Skill41", "Skill42", "Skill43", "Skill52", "Skill53"
			, "Skill54", "Skill63", "Skill64", "Skill65", "Skill82", "Skill83", "Skill84", "Skill93", "Skill94", "Skill95", "Skill111", "Skill112", "Skill113", "Skill142", "Skill143", "Skill144", "Skill133", "Skill134", "Skill135", "Skill151"
			, "Skill152", "Skill153", "Skill162", "Skill163", "Skill164", "Skill165", "Skill171", "Skill172", "Skill173", "Skill174", "Skill175", "Skill181", "Skill182", "Skill183", "Skill184", "Skill185", "Skill201", "Skill202", "Skill203", "Skill213"
			, "Skill214", "Skill215", "Skill221", "Skill222", "Skill223", "Skill224", "Skill243", "Skill244", "Skill245", "Skill251", "Skill252", "Skill253", "Skill254", "Skill255", "Skill261", "Skill262", "Skill263", "Skill291", "Skill292", "Skill293"
			, "Skill294", "Skill295", "Skill301", "Skill302", "Skill303", "Skill304", "Skill305", "Skill311", "Skill312", "Skill313", "Skill314", "Skill315", "Skill321", "Skill322", "Skill323", "Skill343", "Skill344", "Skill345", "Skill352", "Skill353"
			, "Skill354", "Skill361", "Skill362", "Skill363", "Skill373", "Skill374", "Skill375", "Skill381", "Skill382", "Skill383", "Skill384", "Skill413", "Skill414", "Skill415", "Skill421", "Skill422", "Skill423", "Skill424", "Skill425", "Skill442"
			, "Skill443", "Skill444", "Skill445", "Skill451", "Skill452", "Skill453", "Skill454", "Skill455", "Skill461", "Skill462", "Skill463", "Skill464", "Skill465", "Skill471", "Skill472", "Skill473", "Skill474", "Skill475", "Skill481", "Skill482"
			, "Skill483", "Skill484", "Skill485", "Skill491", "Skill492", "Skill493", "Skill494", "Skill495", "Skill501", "Skill502", "Skill503", "Skill504", "Skill505", "Skill511", "Skill512", "Skill513", "Skill514", "Skill515", "Skill521", "Skill522"
			, "Skill523", "Skill524", "Skill525", "Skill531", "Skill532", "Skill533", "Skill534", "Skill543", "Skill544", "Skill545", "Skill583", "Skill584", "Skill585", "Skill593", "Skill594", "Skill595", "Skill601", "Skill602", "Skill603", "Skill613"
			, "Skill614", "Skill615", "Skill621", "Skill632", "Skill633", "Skill634", "Skill635", "Skill641", "Skill642", "Skill643", "Skill644", "Skill645", "Skill651", "Skill652", "Skill653", "Skill654", "Skill663", "Skill664", "Skill665", "Skill681"
			, "Skill682", "Skill683", "Skill684", "Skill685", "Skill691", "Skill692", "Skill693", "Skill701", "Skill702", "Skill703", "Skill712", "Skill713", "Skill714", "Skill733", "Skill734", "Skill735", "Skill741", "Skill742", "Skill743", "Skill744"
			, "Skill745", "Skill763", "Skill764", "Skill765", "Skill771", "Skill772", "Skill773", "Skill774", "Skill775", "Skill811", "Skill812", "Skill813", "Skill814", "Skill815", "Skill821", "Skill822", "Skill823", "Skill824", "Skill825", "Skill831"
			, "Skill832", "Skill833", "Skill834", "Skill835", "Skill841", "Skill842", "Skill843", "Skill844", "Skill845", "Skill851", "Skill852", "Skill853", "Skill854", "Skill855", "Skill861", "Skill862", "Skill863", "Skill864", "Skill865", "Skill883"
			, "Skill884", "Skill885", "Skill891", "Skill892", "Skill893", "Skill894", "Skill895", "Skill901", "Skill902", "Skill903", "Skill904", "Skill921", "Skill922", "Skill923", "Skill932", "Skill933", "Skill934", "Skill935", "Skill941", "Skill942"
			, "Skill943", "Skill944", "Skill945", "Skill1093", "Skill1094", "Skill1095", "Skill1101", "Skill1102", "Skill1103", "Skill1104", "Skill1105", "Skill1111", "Skill1112", "Skill1113", "Skill1114", "Skill1115", "Skill1121", "Skill1122", "Skill1123", "Skill1124"
			, "Skill1125", "Skill1131", "Skill1132", "Skill1133", "Skill1134", "Skill1143", "Skill1144", "Skill1145", "Skill1163", "Skill1164", "Skill1165", "Skill1183", "Skill1184", "Skill1185", "Skill1201", "Skill1202", "Skill1203", "Skill1211", "Skill1212", "Skill1213"
			, "Skill1214", "Skill1215", "Skill1221", "Skill1222", "Skill1223", "Skill1224", "Skill1225", "Skill1231", "Skill1232", "Skill1233", "Skill1234", "Skill1235", "Skill1241", "Skill1242", "Skill1243", "Skill1244", "Skill1245", "Skill1251", "Skill1252", "Skill1253"
			, "Skill1254", "Skill1273", "Skill1274", "Skill1275", "Skill1281", "Skill1282", "Skill1283", "Skill1284", "Skill1285", "Skill1291", "Skill1292", "Skill1293", "Skill1294", "Skill1313", "Skill1314", "Skill1315", "Skill1321", "Skill1322", "Skill1323", "Skill1324"
			, "Skill1325", "Skill1353", "Skill1354", "Skill1355", "Skill1361", "Skill1362", "Skill1363", "Skill1364", "Skill1365", "Skill1371", "Skill1372", "Skill1373", "Skill1374", "Skill1375", "Skill1401", "Skill1402", "Skill1403", "Skill1422", "Skill1423", "Skill1424"
			, "Skill1425", "Skill1431", "Skill1432", "Skill1433", "Skill1434", "Skill1435", "Skill1441", "Skill1442", "Skill1443", "Skill1444", "Skill1445", "Skill7011", "Skill7012", "Skill7013", "Skill7014", "Skill7015", "Skill7025", "Skill7035", "Skill7041", "Skill7042"
			, "Skill7043", "Skill7044", "Skill7045", "Skill1451", "Skill1452", "Skill1453", "Skill1454", "Skill1455", "Skill1501", "Skill1502", "Skill1503", "Skill1504", "Skill1505", "Skill1513", "Skill1514", "Skill1515", "Skill1523", "Skill1524", "Skill1525", "Skill1533"
			, "Skill1534", "Skill1535", "Skill1543", "Skill1544", "Skill1545", "Skill1553", "Skill1554", "Skill1555", "Skill1565", "Skill1573", "Skill1574", "Skill1575", "Skill1583", "Skill1584", "Skill1585", "Skill1593", "Skill1594", "Skill1595", "Skill1603", "Skill1604"
			, "Skill1605", "Skill1613", "Skill1614", "Skill1615", "Skill1623", "Skill1624", "Skill1625", "Skill1633", "Skill1634", "Skill1635", "Skill1463", "Skill1464", "Skill1465", "Skill1473", "Skill1474", "Skill1475", "Skill1481", "Skill1482", "Skill1483", "Skill1484"
			, "Skill1485", "Skill1491", "Skill1492", "Skill1493", "Skill1494", "Skill1495", "Skill1643", "Skill1644", "Skill1645", "Skill963", "Skill964", "Skill965", "Skill983", "Skill984", "Skill985", "Skill993", "Skill994", "Skill995", "Skill1003", "Skill1004"
			, "Skill1005", "Skill1023", "Skill1024", "Skill1025", "Skill1033", "Skill1034", "Skill1035", "Skill1045", "Skill1063", "Skill1064", "Skill1065", "Skill1073", "Skill1074", "Skill1075", "Skill1653", "Skill1654", "Skill1655", "Skill1663", "Skill1664", "Skill1665"
			, "Skill1673", "Skill1674", "Skill1675", "Skill1683", "Skill1684", "Skill1685", "Skill1693", "Skill1694", "Skill1695", "Skill1703", "Skill1704", "Skill1705", "Skill1713", "Skill1714", "Skill1715", "Skill1723", "Skill1724", "Skill1725", "Skill1733", "Skill1734"
			, "Skill1735", "Skill1743", "Skill1744", "Skill1745", "Skill1753", "Skill1754", "Skill1755", "Skill1763", "Skill1764", "Skill1765", "Skill1773", "Skill1774", "Skill1775", "Skill1783", "Skill1784", "Skill1785", "Skill1793", "Skill1794", "Skill1795", "Skill1803"
			, "Skill1804", "Skill1805", "Skill1813", "Skill1814", "Skill1815", "Skill1823", "Skill1824", "Skill1825", "Skill1833", "Skill1834", "Skill1835", "Skill1843", "Skill1844", "Skill1845", "Skill1853", "Skill1854", "Skill1855", "Skill1863", "Skill1864", "Skill1865"
			, "Skill1873", "Skill1874", "Skill1875", "Skill1883", "Skill1884", "Skill1885", "Skill1893", "Skill1894", "Skill1895", "Skill1903", "Skill1904", "Skill1905", "Skill1913", "Skill1914", "Skill1915", "Skill1923", "Skill1924", "Skill1925", "Skill1935", "Skill1943"
			, "Skill1944", "Skill1945", "Skill1953", "Skill1954", "Skill1955", "Skill1963", "Skill1964", "Skill1965", "Skill1973", "Skill1974", "Skill1975", "Skill1983", "Skill1984", "Skill1985", "Skill1053", "Skill1054", "Skill1055", "Skill1993", "Skill1994", "Skill1995"
			, "Skill2003", "Skill2004", "Skill2005", "Skill2033", "Skill2034", "Skill2035", "Skill2043", "Skill2044", "Skill2045", "Skill2073", "Skill2074", "Skill2075", "Skill2085", "Skill2095", "Skill2103", "Skill2104", "Skill2105", "Skill2113", "Skill2114", "Skill2115"
			, "Skill2123", "Skill2124", "Skill2125", "Skill2133", "Skill2134", "Skill2135", "Skill2143", "Skill2144", "Skill2145", "Skill2153", "Skill2154", "Skill2155", "Skill2163", "Skill2164", "Skill2165", "Skill2173", "Skill2174", "Skill2175", "Skill2185", "Skill2193"
			, "Skill2194", "Skill2195", "Skill2203", "Skill2204", "Skill2205", "Skill2213", "Skill2214", "Skill2215", "Skill2223", "Skill2224", "Skill2225"
		};
		public System.Collections.Generic.List<MNP_NekoSkillRow> Rows = new System.Collections.Generic.List<MNP_NekoSkillRow>();

		public static MNP_NekoSkill Instance
		{
			get { return NestedMNP_NekoSkill.instance; }
		}

		private class NestedMNP_NekoSkill
		{
			static NestedMNP_NekoSkill() { }
			internal static readonly MNP_NekoSkill instance = new MNP_NekoSkill();
		}

		private MNP_NekoSkill()
		{
			Rows.Add( new MNP_NekoSkillRow("Skill01", "0", "1", "score_passive", "3950", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill02", "0", "2", "score_passive", "3950", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill03", "0", "3", "score_passive", "3950", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill12", "1", "2", "score_passive", "3950", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill13", "1", "3", "score_passive", "3950", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill14", "1", "4", "score_passive", "3950", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill22", "2", "2", "coin_passive", "3951", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill23", "2", "3", "coin_passive", "3951", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill24", "2", "4", "coin_passive", "3951", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill32", "3", "2", "time_passive", "3952", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill33", "3", "3", "time_passive", "3952", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill34", "3", "4", "time_passive", "3952", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill35", "3", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill41", "4", "1", "score_passive", "3950", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill42", "4", "2", "score_passive", "3950", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill43", "4", "3", "score_passive", "3950", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill52", "5", "2", "nekoskill_appear_passive", "3962", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill53", "5", "3", "nekoskill_appear_passive", "3962", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill54", "5", "4", "nekoskill_appear_passive", "3962", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill63", "6", "3", "blue_bomb_active", "3969", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill64", "6", "4", "blue_bomb_active", "3969", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill65", "6", "5", "blue_bomb_active", "3969", "3", "3974", "yellowblock_appear_passive", "3958", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill82", "8", "2", "coin_passive", "3951", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill83", "8", "3", "coin_passive", "3951", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill84", "8", "4", "coin_passive", "3951", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill93", "9", "3", "yellowblock_score_passive", "3955", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill94", "9", "4", "yellowblock_score_passive", "3955", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill95", "9", "5", "yellowblock_score_passive", "3955", "3", "3974", "coin_passive", "3951", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill111", "11", "1", "score_passive", "3950", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill112", "11", "2", "score_passive", "3950", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill113", "11", "3", "score_passive", "3950", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill142", "14", "2", "yellowblock_appear_passive", "3958", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill143", "14", "3", "yellowblock_appear_passive", "3958", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill144", "14", "4", "yellowblock_appear_passive", "3958", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill133", "13", "3", "power_passive", "3954", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill134", "13", "4", "power_passive", "3954", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill135", "13", "5", "power_passive", "3954", "3", "3974", "red_bomb_active", "3970", "1", "3972", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill151", "15", "1", "score_passive", "3950", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill152", "15", "2", "score_passive", "3950", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill153", "15", "3", "score_passive", "3950", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill162", "16", "2", "nekoskill_appear_passive", "3962", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill163", "16", "3", "nekoskill_appear_passive", "3962", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill164", "16", "4", "nekoskill_appear_passive", "3962", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill165", "16", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill171", "17", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill172", "17", "2", "power_passive", "3954", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill173", "17", "3", "power_passive", "3954", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill174", "17", "4", "power_passive", "3954", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill175", "17", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill181", "18", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill182", "18", "2", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill183", "18", "3", "bomb_appear_passive", "3961", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill184", "18", "4", "bomb_appear_passive", "3961", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill185", "18", "5", "bomb_appear_passive", "3961", "3", "3974", "time_passive", "3952", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill201", "20", "1", "coin_passive", "3951", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill202", "20", "2", "coin_passive", "3951", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill203", "20", "3", "coin_passive", "3951", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill213", "21", "3", "score_passive", "3950", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill214", "21", "4", "score_passive", "3950", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill215", "21", "5", "score_passive", "3950", "3", "3974", "userexp_passive", "3963", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill221", "22", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill222", "22", "2", "fevertime_passive", "3953", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill223", "22", "3", "fevertime_passive", "3953", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill224", "22", "4", "fevertime_passive", "3953", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill243", "24", "3", "random_bomb_active", "3964", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill244", "24", "4", "random_bomb_active", "3964", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill245", "24", "5", "random_bomb_active", "3964", "2", "3973", "fevertime_passive", "3953", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill251", "25", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill252", "25", "2", "bomb_appear_passive", "3961", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill253", "25", "3", "bomb_appear_passive", "3961", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill254", "25", "4", "bomb_appear_passive", "3961", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill255", "25", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill261", "26", "1", "coin_passive", "3951", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill262", "26", "2", "coin_passive", "3951", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill263", "26", "3", "coin_passive", "3951", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill291", "29", "1", "score_passive", "3950", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill292", "29", "2", "score_passive", "3950", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill293", "29", "3", "score_passive", "3950", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill294", "29", "4", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill295", "29", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill301", "30", "1", "coin_passive", "3951", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill302", "30", "2", "coin_passive", "3951", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill303", "30", "3", "coin_passive", "3951", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill304", "30", "4", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill305", "30", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill311", "31", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill312", "31", "2", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill313", "31", "3", "yellowblock_appear_passive", "3958", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill314", "31", "4", "yellowblock_appear_passive", "3958", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill315", "31", "5", "yellowblock_appear_passive", "3958", "3", "3974", "redblock_score_passive", "3957", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill321", "32", "1", "coin_passive", "3951", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill322", "32", "2", "coin_passive", "3951", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill323", "32", "3", "coin_passive", "3951", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill343", "34", "3", "redblock_appear_passive", "3960", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill344", "34", "4", "redblock_appear_passive", "3960", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill345", "34", "5", "redblock_appear_passive", "3960", "3", "3974", "blueblock_score_passive", "3956", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill352", "35", "2", "time_passive", "3952", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill353", "35", "3", "time_passive", "3952", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill354", "35", "4", "time_passive", "3952", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill361", "36", "1", "coin_passive", "3951", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill362", "36", "2", "coin_passive", "3951", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill363", "36", "3", "coin_passive", "3951", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill373", "37", "3", "nekoskill_appear_passive", "3962", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill374", "37", "4", "nekoskill_appear_passive", "3962", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill375", "37", "5", "nekoskill_appear_passive", "3962", "3", "3974", "bomb_appear_passive", "3961", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill381", "38", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill382", "38", "2", "fevertime_passive", "3953", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill383", "38", "3", "fevertime_passive", "3953", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill384", "38", "4", "fevertime_passive", "3953", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill413", "41", "3", "fever_raise_active", "3966", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill414", "41", "4", "fever_raise_active", "3966", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill415", "41", "5", "fever_raise_active", "3966", "3", "3974", "time_passive", "3952", "4", "3975", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill421", "42", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill422", "42", "2", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill423", "42", "3", "power_passive", "3954", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill424", "42", "4", "power_passive", "3954", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill425", "42", "5", "power_passive", "3954", "3", "3974", "blueblock_appear_passive", "3959", "4", "3975", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill442", "44", "2", "score_passive", "3950", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill443", "44", "3", "score_passive", "3950", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill444", "44", "4", "score_passive", "3950", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill445", "44", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill451", "45", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill452", "45", "2", "coin_passive", "3951", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill453", "45", "3", "coin_passive", "3951", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill454", "45", "4", "coin_passive", "3951", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill455", "45", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill461", "46", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill462", "46", "2", "fevertime_passive", "3953", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill463", "46", "3", "fevertime_passive", "3953", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill464", "46", "4", "fevertime_passive", "3953", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill465", "46", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill471", "47", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill472", "47", "2", "time_passive", "3952", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill473", "47", "3", "time_passive", "3952", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill474", "47", "4", "time_passive", "3952", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill475", "47", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill481", "48", "1", "score_passive", "3950", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill482", "48", "2", "score_passive", "3950", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill483", "48", "3", "score_passive", "3950", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill484", "48", "4", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill485", "48", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill491", "49", "1", "coin_passive", "3951", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill492", "49", "2", "coin_passive", "3951", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill493", "49", "3", "coin_passive", "3951", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill494", "49", "4", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill495", "49", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill501", "50", "1", "score_passive", "3950", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill502", "50", "2", "score_passive", "3950", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill503", "50", "3", "score_passive", "3950", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill504", "50", "4", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill505", "50", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill511", "51", "1", "coin_passive", "3951", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill512", "51", "2", "coin_passive", "3951", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill513", "51", "3", "coin_passive", "3951", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill514", "51", "4", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill515", "51", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill521", "52", "1", "score_passive", "3950", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill522", "52", "2", "score_passive", "3950", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill523", "52", "3", "score_passive", "3950", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill524", "52", "4", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill525", "52", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill531", "53", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill532", "53", "2", "bomb_appear_passive", "3961", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill533", "53", "3", "bomb_appear_passive", "3961", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill534", "53", "4", "bomb_appear_passive", "3961", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill543", "54", "3", "power_passive", "3954", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill544", "54", "4", "power_passive", "3954", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill545", "54", "5", "power_passive", "3954", "4", "3975", "score_passive", "3950", "4", "3975", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill583", "58", "3", "time_passive", "3952", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill584", "58", "4", "time_passive", "3952", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill585", "58", "5", "time_passive", "3952", "3", "3974", "coin_passive", "3951", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill593", "59", "3", "blueblock_score_passive", "3956", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill594", "59", "4", "blueblock_score_passive", "3956", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill595", "59", "5", "blueblock_score_passive", "3956", "4", "3975", "yellow_bomb_active", "3968", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill601", "60", "1", "coin_passive", "3951", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill602", "60", "2", "coin_passive", "3951", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill603", "60", "3", "coin_passive", "3951", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill613", "61", "3", "time_passive", "3952", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill614", "61", "4", "time_passive", "3952", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill615", "61", "5", "time_passive", "3952", "4", "3975", "fevertime_passive", "3953", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill621", "62", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill632", "63", "2", "power_passive", "3954", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill633", "63", "3", "power_passive", "3954", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill634", "63", "4", "power_passive", "3954", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill635", "63", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill641", "64", "1", "time_passive", "3952", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill642", "64", "2", "time_passive", "3952", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill643", "64", "3", "time_passive", "3952", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill644", "64", "4", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill645", "64", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill651", "65", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill652", "65", "2", "bomb_appear_passive", "3961", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill653", "65", "3", "bomb_appear_passive", "3961", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill654", "65", "4", "bomb_appear_passive", "3961", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill663", "66", "3", "time_passive", "3952", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill664", "66", "4", "time_passive", "3952", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill665", "66", "5", "time_passive", "3952", "4", "3975", "fevertime_passive", "3953", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill681", "68", "1", "score_passive", "3950", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill682", "68", "2", "score_passive", "3950", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill683", "68", "3", "score_passive", "3950", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill684", "68", "4", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill685", "68", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill691", "69", "1", "coin_passive", "3951", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill692", "69", "2", "coin_passive", "3951", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill693", "69", "3", "coin_passive", "3951", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill701", "70", "1", "coin_passive", "3951", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill702", "70", "2", "coin_passive", "3951", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill703", "70", "3", "coin_passive", "3951", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill712", "71", "2", "power_passive", "3954", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill713", "71", "3", "power_passive", "3954", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill714", "71", "4", "power_passive", "3954", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill733", "73", "3", "combo_maintain_active", "3965", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill734", "73", "4", "combo_maintain_active", "3965", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill735", "73", "5", "combo_maintain_active", "3965", "2", "3973", "redblock_appear_passive", "3960", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill741", "74", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill742", "74", "2", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill743", "74", "3", "black_bomb_active", "3971", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill744", "74", "4", "black_bomb_active", "3971", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill745", "74", "5", "black_bomb_active", "3971", "2", "3973", "fevertime_passive", "3953", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill763", "76", "3", "time_passive", "3952", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill764", "76", "4", "time_passive", "3952", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill765", "76", "5", "time_passive", "3952", "3", "3974", "fevertime_passive", "3953", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill771", "77", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill772", "77", "2", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill773", "77", "3", "redblock_score_passive", "3957", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill774", "77", "4", "redblock_score_passive", "3957", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill775", "77", "5", "redblock_score_passive", "3957", "3", "3974", "blueblock_appear_passive", "3959", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill811", "81", "1", "coin_passive", "3951", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill812", "81", "2", "coin_passive", "3951", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill813", "81", "3", "coin_passive", "3951", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill814", "81", "4", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill815", "81", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill821", "82", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill822", "82", "2", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill823", "82", "3", "fevertime_passive", "3953", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill824", "82", "4", "fevertime_passive", "3953", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill825", "82", "5", "fevertime_passive", "3953", "3", "3974", "score_passive", "3950", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill831", "83", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill832", "83", "2", "score_passive", "3950", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill833", "83", "3", "score_passive", "3950", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill834", "83", "4", "score_passive", "3950", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill835", "83", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill841", "84", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill842", "84", "2", "nekoskill_appear_passive", "3962", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill843", "84", "3", "nekoskill_appear_passive", "3962", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill844", "84", "4", "nekoskill_appear_passive", "3962", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill845", "84", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill851", "85", "1", "coin_passive", "3951", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill852", "85", "2", "coin_passive", "3951", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill853", "85", "3", "coin_passive", "3951", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill854", "85", "4", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill855", "85", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill861", "86", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill862", "86", "2", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill863", "86", "3", "coin_passive", "3951", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill864", "86", "4", "coin_passive", "3951", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill865", "86", "5", "coin_passive", "3951", "4", "3975", "fevertime_passive", "3953", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill883", "88", "3", "bomb_appear_passive", "3961", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill884", "88", "4", "bomb_appear_passive", "3961", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill885", "88", "5", "bomb_appear_passive", "3961", "3", "3974", "red_bomb_active", "3970", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill891", "89", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill892", "89", "2", "time_passive", "3952", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill893", "89", "3", "time_passive", "3952", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill894", "89", "4", "time_passive", "3952", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill895", "89", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill901", "90", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill902", "90", "2", "fevertime_passive", "3953", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill903", "90", "3", "fevertime_passive", "3953", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill904", "90", "4", "fevertime_passive", "3953", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill921", "92", "1", "score_passive", "3950", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill922", "92", "2", "score_passive", "3950", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill923", "92", "3", "score_passive", "3950", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill932", "93", "2", "nekoskill_appear_passive", "3962", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill933", "93", "3", "nekoskill_appear_passive", "3962", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill934", "93", "4", "nekoskill_appear_passive", "3962", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill935", "93", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill941", "94", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill942", "94", "2", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill943", "94", "3", "score_passive", "3950", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill944", "94", "4", "score_passive", "3950", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill945", "94", "5", "score_passive", "3950", "4", "3975", "nekoskill_appear_passive", "3962", "4", "3975", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1093", "109", "3", "time_passive", "3952", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1094", "109", "4", "time_passive", "3952", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1095", "109", "5", "time_passive", "3952", "4", "3975", "userexp_passive", "3963", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1101", "110", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1102", "110", "2", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1103", "110", "3", "blueblock_score_passive", "3956", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1104", "110", "4", "blueblock_score_passive", "3956", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1105", "110", "5", "blueblock_score_passive", "3956", "3", "3974", "redblock_appear_passive", "3960", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1111", "111", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1112", "111", "2", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1113", "111", "3", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1114", "111", "4", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1115", "111", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1121", "112", "1", "score_passive", "3950", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1122", "112", "2", "score_passive", "3950", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1123", "112", "3", "score_passive", "3950", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1124", "112", "4", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1125", "112", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1131", "113", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1132", "113", "2", "redblock_appear_passive", "3960", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1133", "113", "3", "redblock_appear_passive", "3960", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1134", "113", "4", "redblock_appear_passive", "3960", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1143", "114", "3", "score_passive", "3950", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1144", "114", "4", "score_passive", "3950", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1145", "114", "5", "score_passive", "3950", "4", "3975", "coin_passive", "3951", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1163", "116", "3", "score_passive", "3950", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1164", "116", "4", "score_passive", "3950", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1165", "116", "5", "score_passive", "3950", "4", "3975", "fevertime_passive", "3953", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1183", "118", "3", "blue_bomb_active", "3969", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1184", "118", "4", "blue_bomb_active", "3969", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1185", "118", "5", "blue_bomb_active", "3969", "2", "3973", "yellow_bomb_active", "3968", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1201", "120", "1", "score_passive", "3950", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1202", "120", "2", "score_passive", "3950", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1203", "120", "3", "score_passive", "3950", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1211", "121", "1", "coin_passive", "3951", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1212", "121", "2", "coin_passive", "3951", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1213", "121", "3", "coin_passive", "3951", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1214", "121", "4", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1215", "121", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1221", "122", "1", "coin_passive", "3951", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1222", "122", "2", "coin_passive", "3951", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1223", "122", "3", "coin_passive", "3951", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1224", "122", "4", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1225", "122", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1231", "123", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1232", "123", "2", "score_passive", "3950", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1233", "123", "3", "score_passive", "3950", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1234", "123", "4", "score_passive", "3950", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1235", "123", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1241", "124", "1", "score_passive", "3950", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1242", "124", "2", "score_passive", "3950", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1243", "124", "3", "score_passive", "3950", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1244", "124", "4", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1245", "124", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1251", "125", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1252", "125", "2", "blueblock_appear_passive", "3959", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1253", "125", "3", "blueblock_appear_passive", "3959", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1254", "125", "4", "blueblock_appear_passive", "3959", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1273", "127", "3", "blueblock_score_passive", "3956", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1274", "127", "4", "blueblock_score_passive", "3956", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1275", "127", "5", "blueblock_score_passive", "3956", "3", "3974", "power_passive", "3954", "4", "3975", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1281", "128", "1", "coin_passive", "3951", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1282", "128", "2", "coin_passive", "3951", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1283", "128", "3", "coin_passive", "3951", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1284", "128", "4", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1285", "128", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1291", "129", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1292", "129", "2", "coin_passive", "3951", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1293", "129", "3", "coin_passive", "3951", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1294", "129", "4", "coin_passive", "3951", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1313", "131", "3", "yellowblock_score_passive", "3955", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1314", "131", "4", "yellowblock_score_passive", "3955", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1315", "131", "5", "yellowblock_score_passive", "3955", "4", "3975", "combo_maintain_active", "3965", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1321", "132", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1322", "132", "2", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1323", "132", "3", "yellowblock_score_passive", "3955", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1324", "132", "4", "yellowblock_score_passive", "3955", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1325", "132", "5", "yellowblock_score_passive", "3955", "3", "3974", "yellow_bomb_active", "3968", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1353", "135", "3", "blueblock_appear_passive", "3959", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1354", "135", "4", "blueblock_appear_passive", "3959", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1355", "135", "5", "blueblock_appear_passive", "3959", "3", "3974", "yellowblock_appear_passive", "3958", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1361", "136", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1362", "136", "2", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1363", "136", "3", "power_passive", "3954", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1364", "136", "4", "power_passive", "3954", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1365", "136", "5", "power_passive", "3954", "3", "3974", "fevertime_passive", "3953", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1371", "137", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1372", "137", "2", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1373", "137", "3", "blue_bomb_active", "3969", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1374", "137", "4", "blue_bomb_active", "3969", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1375", "137", "5", "blue_bomb_active", "3969", "2", "3973", "time_passive", "3952", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1401", "140", "1", "time_passive", "3952", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1402", "140", "2", "time_passive", "3952", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1403", "140", "3", "time_passive", "3952", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1422", "142", "2", "power_passive", "3954", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1423", "142", "3", "power_passive", "3954", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1424", "142", "4", "power_passive", "3954", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1425", "142", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1431", "143", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1432", "143", "2", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1433", "143", "3", "bomb_appear_passive", "3961", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1434", "143", "4", "bomb_appear_passive", "3961", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1435", "143", "5", "bomb_appear_passive", "3961", "4", "3975", "time_passive", "3952", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1441", "144", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1442", "144", "2", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1443", "144", "3", "coin_passive", "3951", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1444", "144", "4", "coin_passive", "3951", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1445", "144", "5", "coin_passive", "3951", "2", "3973", "black_bomb_active", "3971", "1", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill7011", "701", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill7012", "701", "2", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill7013", "701", "3", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill7014", "701", "4", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill7015", "701", "5", "coin_passive", "3951", "4", "3975", "bomb_appear_passive", "3961", "4", "3975", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill7025", "702", "5", "black_bomb_active", "3971", "3", "3974", "fevertime_passive", "3953", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill7035", "703", "5", "score_passive", "3950", "2", "3973", "power_passive", "3954", "1", "3972", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill7041", "704", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill7042", "704", "2", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill7043", "704", "3", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill7044", "704", "4", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill7045", "704", "5", "fever_raise_active", "3966", "2", "3973", "redblock_score_passive", "3957", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1451", "145", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1452", "145", "2", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1453", "145", "3", "time_passive", "3952", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1454", "145", "4", "time_passive", "3952", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1455", "145", "5", "time_passive", "3952", "2", "3973", "bomb_appear_passive", "3961", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1501", "150", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1502", "150", "2", "random_bomb_active", "3964", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1503", "150", "3", "random_bomb_active", "3964", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1504", "150", "4", "random_bomb_active", "3964", "3", "3674", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1505", "150", "5", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1513", "151", "3", "fevertime_passive", "3953", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1514", "151", "4", "fevertime_passive", "3953", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1515", "151", "5", "fevertime_passive", "3953", "2", "3973", "coin_passive", "3951", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1523", "152", "3", "yellowblock_appear_passive", "3958", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1524", "152", "4", "yellowblock_appear_passive", "3958", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1525", "152", "5", "yellowblock_appear_passive", "3958", "2", "3973", "yellowblock_score_passive", "3955", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1533", "153", "3", "blue_bomb_active", "3969", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1534", "153", "4", "blue_bomb_active", "3969", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1535", "153", "5", "blue_bomb_active", "3969", "1", "3972", "yellow_bomb_active", "3968", "1", "3972", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1543", "154", "3", "nekoskill_appear_passive", "3962", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1544", "154", "4", "nekoskill_appear_passive", "3962", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1545", "154", "5", "nekoskill_appear_passive", "3962", "2", "3973", "power_passive", "3954", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1553", "155", "3", "time_active", "3967", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1554", "155", "4", "time_active", "3967", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1555", "155", "5", "time_active", "3967", "2", "3973", "fevertime_passive", "3953", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1565", "156", "5", "blueblock_score_passive", "3956", "3", "3974", "redblock_score_passive", "3957", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1573", "157", "3", "time_active", "3967", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1574", "157", "4", "time_active", "3967", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1575", "157", "5", "time_active", "3967", "2", "3973", "red_bomb_active", "3970", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1583", "158", "3", "score_passive", "3950", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1584", "158", "4", "score_passive", "3950", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1585", "158", "5", "score_passive", "3950", "2", "3973", "blueblock_score_passive", "3956", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1593", "159", "3", "bomb_appear_passive", "3961", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1594", "159", "4", "bomb_appear_passive", "3961", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1595", "159", "5", "bomb_appear_passive", "3961", "2", "3973", "random_bomb_active", "3964", "1", "3972", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1603", "160", "3", "power_passive", "3954", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1604", "160", "4", "power_passive", "3954", "3", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1605", "160", "5", "power_passive", "3954", "3", "3974", "random_bomb_active", "3964", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1613", "161", "3", "nekoskill_appear_passive", "3962", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1614", "161", "4", "nekoskill_appear_passive", "3962", "3", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1615", "161", "5", "nekoskill_appear_passive", "3962", "3", "3974", "red_bomb_active", "3970", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1623", "162", "3", "time_passive", "3952", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1624", "162", "4", "time_passive", "3952", "3", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1625", "162", "5", "time_passive", "3952", "3", "3974", "coin_passive", "3951", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1633", "163", "3", "black_bomb_active", "3971", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1634", "163", "4", "black_bomb_active", "3971", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1635", "163", "5", "black_bomb_active", "3971", "2", "3973", "redblock_appear_passive", "3960", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1463", "146", "3", "coin_passive", "3951", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1464", "146", "4", "coin_passive", "3951", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1465", "146", "5", "coin_passive", "3951", "4", "3975", "bomb_appear_passive", "3961", "4", "3975", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1473", "147", "3", "black_bomb_active", "3971", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1474", "147", "4", "black_bomb_active", "3971", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1475", "147", "5", "black_bomb_active", "3971", "3", "3974", "fevertime_passive", "3953", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1481", "148", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1482", "148", "2", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1483", "148", "3", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1484", "148", "4", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1485", "148", "5", "score_passive", "3950", "2", "3973", "power_passive", "3954", "1", "3972", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1491", "149", "1", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1492", "149", "2", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1493", "149", "3", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1494", "149", "4", "none", "3052", "", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1495", "149", "5", "fever_raise_active", "3966", "2", "3973", "redblock_score_passive", "3957", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1643", "164", "3", "score_passive", "3950", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1644", "164", "4", "score_passive", "3950", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1645", "164", "5", "score_passive", "3950", "2", "3973", "power_passive", "3954", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill963", "96", "3", "score_passive", "3950", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill964", "96", "4", "score_passive", "3950", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill965", "96", "5", "score_passive", "3950", "2", "3973", "bomb_appear_passive", "3961", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill983", "98", "3", "userexp_passive", "3963", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill984", "98", "4", "userexp_passive", "3963", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill985", "98", "5", "userexp_passive", "3963", "2", "3973", "nekoskill_appear_passive", "3962", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill993", "99", "3", "redblock_appear_passive", "3960", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill994", "99", "4", "redblock_appear_passive", "3960", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill995", "99", "5", "redblock_appear_passive", "3960", "2", "3973", "red_bomb_active", "3970", "1", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1003", "100", "3", "coin_passive", "3951", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1004", "100", "4", "coin_passive", "3951", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1005", "100", "5", "coin_passive", "3951", "2", "3973", "power_passive", "3954", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1023", "102", "3", "time_passive", "3952", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1024", "102", "4", "time_passive", "3952", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1025", "102", "5", "time_passive", "3952", "2", "3973", "fevertime_passive", "3953", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1033", "103", "3", "random_bomb_active", "3964", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1034", "103", "4", "random_bomb_active", "3964", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1035", "103", "5", "random_bomb_active", "3964", "4", "3975", "black_bomb_active", "3971", "4", "3975", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1045", "104", "5", "score_passive", "3950", "4", "3975", "bomb_appear_passive", "3961", "4", "4975", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1063", "106", "3", "time_active", "3967", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1064", "106", "4", "time_active", "3967", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1065", "106", "5", "time_active", "3967", "2", "3973", "yellowblock_appear_passive", "3958", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1073", "107", "3", "blue_bomb_active", "3969", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1074", "107", "4", "blue_bomb_active", "3969", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1075", "107", "5", "blue_bomb_active", "3969", "2", "3973", "fevertime_passive", "3953", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1653", "165", "3", "redblock_appear_passive", "3960", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1654", "165", "4", "redblock_appear_passive", "3960", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1655", "165", "5", "redblock_appear_passive", "3960", "4", "3975", "blue_bomb_active", "3969", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1663", "166", "3", "time_passive", "3952", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1664", "166", "4", "time_passive", "3952", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1665", "166", "5", "time_passive", "3952", "3", "3974", "coin_passive", "3951", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1673", "167", "3", "fevertime_passive", "3953", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1674", "167", "4", "fevertime_passive", "3953", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1675", "167", "5", "fevertime_passive", "3953", "3", "3974", "blueblock_score_passive", "3956", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1683", "168", "3", "time_active", "3967", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1684", "168", "4", "time_active", "3967", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1685", "168", "5", "time_active", "3967", "3", "3974", "combo_maintain_active", "3965", "4", "3975", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1693", "169", "3", "yellow_bomb_active", "3968", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1694", "169", "4", "yellow_bomb_active", "3968", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1695", "169", "5", "yellow_bomb_active", "3968", "2", "3973", "blue_bomb_active", "3969", "1", "3972", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1703", "170", "3", "fever_raise_active", "3966", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1704", "170", "4", "fever_raise_active", "3966", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1705", "170", "5", "fever_raise_active", "3966", "2", "3973", "yellow_bomb_active", "3968", "1", "3972", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1713", "171", "3", "random_bomb_active", "3964", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1714", "171", "4", "random_bomb_active", "3964", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1715", "171", "5", "random_bomb_active", "3964", "2", "3973", "blue_bomb_active", "3969", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1723", "172", "3", "blueblock_appear_passive", "3959", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1724", "172", "4", "blueblock_appear_passive", "3959", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1725", "172", "5", "blueblock_appear_passive", "3959", "4", "3975", "blueblock_score_passive", "3956", "4", "3975", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1733", "173", "3", "bomb_appear_passive", "3961", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1734", "173", "4", "bomb_appear_passive", "3961", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1735", "173", "5", "bomb_appear_passive", "3961", "3", "3974", "yellowblock_appear_passive", "3958", "4", "3975", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1743", "174", "3", "score_passive", "3950", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1744", "174", "4", "score_passive", "3950", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1745", "174", "5", "score_passive", "3950", "4", "3975", "coin_passive", "3951", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1753", "175", "3", "blue_bomb_active", "3969", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1754", "175", "4", "blue_bomb_active", "3969", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1755", "175", "5", "blue_bomb_active", "3969", "3", "3974", "score_passive", "3950", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1763", "176", "3", "userexp_passive", "3963", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1764", "176", "4", "userexp_passive", "3963", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1765", "176", "5", "userexp_passive", "3963", "3", "3974", "bomb_appear_passive", "3961", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1773", "177", "3", "blueblock_score_passive", "3956", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1774", "177", "4", "blueblock_score_passive", "3956", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1775", "177", "5", "blueblock_score_passive", "3956", "4", "3975", "blueblock_appear_passive", "3959", "4", "3975", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1783", "178", "3", "fever_raise_active", "3966", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1784", "178", "4", "fever_raise_active", "3966", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1785", "178", "5", "fever_raise_active", "3966", "4", "3975", "power_passive", "3954", "4", "3975", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1793", "179", "3", "redblock_score_passive", "3957", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1794", "179", "4", "redblock_score_passive", "3957", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1795", "179", "5", "redblock_score_passive", "3957", "4", "3975", "yellow_bomb_active", "3968", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1803", "180", "3", "random_bomb_active", "3964", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1804", "180", "4", "random_bomb_active", "3964", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1805", "180", "5", "random_bomb_active", "3964", "3", "3974", "yellowblock_score_passive", "3955", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1813", "181", "3", "fever_raise_active", "3966", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1814", "181", "4", "fever_raise_active", "3966", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1815", "181", "5", "fever_raise_active", "3966", "3", "3974", "time_active", "3967", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1823", "182", "3", "redblock_score_passive", "3957", "2", "3973", "blueblock_score_passive", "3956", "1", "3972", "yellowblock_score_passive", "3955", "1", "3972"));
			Rows.Add( new MNP_NekoSkillRow("Skill1824", "182", "4", "redblock_score_passive", "3957", "2", "3973", "blueblock_score_passive", "3956", "2", "3973", "yellowblock_score_passive", "3955", "1", "3972"));
			Rows.Add( new MNP_NekoSkillRow("Skill1825", "182", "5", "redblock_score_passive", "3957", "2", "3973", "blueblock_score_passive", "3956", "2", "3973", "yellowblock_score_passive", "3955", "2", "3973"));
			Rows.Add( new MNP_NekoSkillRow("Skill1833", "183", "3", "bomb_appear_passive", "3961", "2", "3973", "nekoskill_appear_passive", "3962", "1", "3972", "power_passive", "3954", "1", "3972"));
			Rows.Add( new MNP_NekoSkillRow("Skill1834", "183", "4", "bomb_appear_passive", "3961", "2", "3973", "nekoskill_appear_passive", "3962", "2", "3973", "power_passive", "3954", "1", "3972"));
			Rows.Add( new MNP_NekoSkillRow("Skill1835", "183", "5", "bomb_appear_passive", "3961", "2", "3973", "nekoskill_appear_passive", "3962", "2", "3973", "power_passive", "3954", "2", "3973"));
			Rows.Add( new MNP_NekoSkillRow("Skill1843", "184", "3", "score_passive", "3950", "2", "3973", "yellowblock_score_passive", "3955", "1", "3972", "blueblock_appear_passive", "3959", "1", "3972"));
			Rows.Add( new MNP_NekoSkillRow("Skill1844", "184", "4", "score_passive", "3950", "2", "3973", "yellowblock_score_passive", "3955", "2", "3973", "blueblock_appear_passive", "3959", "2", "3973"));
			Rows.Add( new MNP_NekoSkillRow("Skill1845", "184", "5", "score_passive", "3950", "2", "3973", "yellowblock_score_passive", "3955", "3", "3974", "blueblock_appear_passive", "3959", "3", "3974"));
			Rows.Add( new MNP_NekoSkillRow("Skill1853", "185", "3", "coin_passive", "3951", "2", "3973", "coin_passive", "3951", "1", "3972", "userexp_passive", "3963", "1", "3972"));
			Rows.Add( new MNP_NekoSkillRow("Skill1854", "185", "4", "coin_passive", "3951", "2", "3973", "coin_passive", "3951", "2", "3973", "userexp_passive", "3963", "1", "3972"));
			Rows.Add( new MNP_NekoSkillRow("Skill1855", "185", "5", "coin_passive", "3951", "2", "3973", "coin_passive", "3951", "2", "3973", "userexp_passive", "3963", "2", "3973"));
			Rows.Add( new MNP_NekoSkillRow("Skill1863", "186", "3", "redblock_appear_passive", "3960", "2", "3973", "redblock_score_passive", "3957", "2", "3973", "red_bomb_active", "3970", "1", "3972"));
			Rows.Add( new MNP_NekoSkillRow("Skill1864", "186", "4", "redblock_appear_passive", "3960", "3", "3974", "redblock_score_passive", "3957", "3", "3974", "red_bomb_active", "3970", "1", "3972"));
			Rows.Add( new MNP_NekoSkillRow("Skill1865", "186", "5", "redblock_appear_passive", "3960", "4", "3975", "redblock_score_passive", "3957", "4", "3975", "red_bomb_active", "3970", "1", "3972"));
			Rows.Add( new MNP_NekoSkillRow("Skill1873", "187", "3", "yellowblock_appear_passive", "3958", "2", "3973", "yellowblock_score_passive", "3955", "2", "3973", "yellow_bomb_active", "3968", "1", "3972"));
			Rows.Add( new MNP_NekoSkillRow("Skill1874", "187", "4", "yellowblock_appear_passive", "3958", "3", "3974", "yellowblock_score_passive", "3955", "3", "3974", "yellow_bomb_active", "3968", "1", "3972"));
			Rows.Add( new MNP_NekoSkillRow("Skill1875", "187", "5", "yellowblock_appear_passive", "3958", "4", "3975", "yellowblock_score_passive", "3955", "4", "3975", "yellow_bomb_active", "3968", "1", "3972"));
			Rows.Add( new MNP_NekoSkillRow("Skill1883", "188", "3", "time_passive", "3952", "1", "3972", "fevertime_passive", "3953", "1", "3972", "combo_maintain_active", "3965", "1", "3972"));
			Rows.Add( new MNP_NekoSkillRow("Skill1884", "188", "4", "time_passive", "3952", "2", "3973", "fevertime_passive", "3953", "2", "3973", "combo_maintain_active", "3965", "2", "3973"));
			Rows.Add( new MNP_NekoSkillRow("Skill1885", "188", "5", "time_passive", "3952", "3", "3974", "fevertime_passive", "3953", "2", "3973", "combo_maintain_active", "3965", "3", "3974"));
			Rows.Add( new MNP_NekoSkillRow("Skill1893", "189", "3", "blueblock_appear_passive", "3959", "2", "3973", "blueblock_score_passive", "3956", "2", "3973", "blue_bomb_active", "3969", "1", "3971"));
			Rows.Add( new MNP_NekoSkillRow("Skill1894", "189", "4", "blueblock_appear_passive", "3959", "3", "3974", "blueblock_score_passive", "3956", "3", "3974", "blue_bomb_active", "3969", "1", "3971"));
			Rows.Add( new MNP_NekoSkillRow("Skill1895", "189", "5", "blueblock_appear_passive", "3959", "4", "3975", "blueblock_score_passive", "3956", "4", "3975", "blue_bomb_active", "3969", "1", "3971"));
			Rows.Add( new MNP_NekoSkillRow("Skill1903", "190", "3", "blue_bomb_active", "3969", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1904", "190", "4", "blue_bomb_active", "3969", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1905", "190", "5", "blue_bomb_active", "3969", "2", "3973", "random_bomb_active", "3964", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1913", "191", "3", "redblock_score_passive", "3957", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1914", "191", "4", "redblock_score_passive", "3957", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1915", "191", "5", "redblock_score_passive", "3957", "4", "3975", "bomb_appear_passive", "3961", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1923", "192", "3", "fevertime_passive", "3953", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1924", "192", "4", "fevertime_passive", "3953", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1925", "192", "5", "fevertime_passive", "3953", "3", "3974", "fever_raise_active", "3966", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1935", "193", "5", "yellowblock_score_passive", "3955", "4", "3975", "redblock_score_passive", "3957", "4", "3975", "score_passive", "3950", "4", "3975"));
			Rows.Add( new MNP_NekoSkillRow("Skill1943", "194", "3", "score_passive", "3950", "3", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1944", "194", "4", "score_passive", "3950", "4", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1945", "194", "5", "score_passive", "3950", "4", "3975", "score_passive", "3950", "4", "3975", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1953", "195", "3", "nekoskill_appear_passive", "3962", "1", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1954", "195", "4", "nekoskill_appear_passive", "3962", "2", "", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1955", "195", "5", "nekoskill_appear_passive", "3962", "2", "3973", "combo_maintain_active", "3965", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1963", "196", "3", "time_passive", "3952", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1964", "196", "4", "time_passive", "3952", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1965", "196", "5", "time_passive", "3952", "4", "3975", "bomb_appear_passive", "3961", "4", "3975", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1973", "197", "3", "random_bomb_active", "3964", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1974", "197", "4", "random_bomb_active", "3964", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1975", "197", "5", "random_bomb_active", "3964", "2", "3973", "fevertime_passive", "3953", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1983", "198", "3", "red_bomb_active", "3970", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1984", "198", "4", "red_bomb_active", "3970", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1985", "198", "5", "red_bomb_active", "3970", "3", "3974", "redblock_appear_passive", "3960", "4", "3975", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1053", "105", "3", "red_bomb_active", "3970", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1054", "105", "4", "red_bomb_active", "3970", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1055", "105", "5", "red_bomb_active", "3970", "2", "3973", "yellow_bomb_active", "3968", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1993", "199", "3", "time_active", "3967", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1994", "199", "4", "time_active", "3967", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill1995", "199", "5", "time_active", "3967", "3", "3974", "time_active", "3967", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2003", "200", "3", "time_passive", "3952", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2004", "200", "4", "time_passive", "3952", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2005", "200", "5", "time_passive", "3952", "4", "3975", "time_passive", "3952", "4", "3975", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2033", "203", "3", "random_bomb_active", "3964", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2034", "203", "4", "random_bomb_active", "3964", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2035", "203", "5", "random_bomb_active", "3964", "4", "3975", "bomb_appear_passive", "3961", "4", "3975", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2043", "204", "3", "blueblock_score_passive", "3956", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2044", "204", "4", "blueblock_score_passive", "3956", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2045", "204", "5", "blueblock_score_passive", "3956", "4", "3975", "blueblock_score_passive", "3956", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2073", "207", "3", "yellow_bomb_active", "3968", "1", "3972", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2074", "207", "4", "yellow_bomb_active", "3968", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2075", "207", "5", "yellow_bomb_active", "3968", "2", "3973", "blue_bomb_active", "3969", "1", "3972", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2085", "208", "5", "redblock_score_passive", "3957", "4", "3975", "yellowblock_score_passive", "3955", "4", "3975", "blueblock_score_passive", "3956", "4", "3975"));
			Rows.Add( new MNP_NekoSkillRow("Skill2095", "209", "5", "coin_passive", "3951", "4", "3975", "score_passive", "3950", "4", "3975", "power_passive", "3954", "4", "3975"));
			Rows.Add( new MNP_NekoSkillRow("Skill2103", "210", "3", "yellowblock_score_passive", "3955", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2104", "210", "4", "yellowblock_score_passive", "3955", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2105", "210", "5", "yellowblock_score_passive", "3955", "3", "3974", "blueblock_appear_passive", "3959", "4", "3975", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2113", "211", "3", "redblock_appear_passive", "3960", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2114", "211", "4", "redblock_appear_passive", "3960", "4", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2115", "211", "5", "redblock_appear_passive", "3960", "4", "3975", "nekoskill_appear_passive", "3962", "4", "3975", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2123", "212", "3", "blue_bomb_active", "3969", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2124", "212", "4", "blue_bomb_active", "3969", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2125", "212", "5", "blue_bomb_active", "3969", "3", "3974", "time_passive", "3952", "4", "3975", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2133", "213", "3", "red_bomb_active", "3970", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2134", "213", "4", "red_bomb_active", "3970", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2135", "213", "5", "red_bomb_active", "3970", "3", "3974", "redblock_appear_passive", "3960", "4", "3975", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2143", "214", "3", "yellowblock_score_passive", "3955", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2144", "214", "4", "yellowblock_score_passive", "3955", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2145", "214", "5", "yellowblock_score_passive", "3955", "3", "3974", "fevertime_passive", "3953", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2153", "215", "3", "time_active", "3967", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2154", "215", "4", "time_active", "3967", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2155", "215", "5", "time_active", "3967", "3", "3974", "blueblock_appear_passive", "3959", "4", "3975", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2163", "216", "3", "time_passive", "3952", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2164", "216", "4", "time_passive", "3952", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2165", "216", "5", "time_passive", "3952", "3", "3974", "score_passive", "3950", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2173", "217", "3", "power_passive", "3954", "2", "3973", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2174", "217", "4", "power_passive", "3954", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2175", "217", "5", "power_passive", "3954", "3", "3974", "redblock_score_passive", "3957", "3", "3974", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2185", "218", "5", "combo_maintain_active", "3965", "4", "3975", "fever_raise_active", "3966", "4", "3975", "time_active", "3967", "4", "3975"));
			Rows.Add( new MNP_NekoSkillRow("Skill2193", "219", "3", "redblock_score_passive", "3957", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2194", "219", "4", "redblock_score_passive", "3957", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2195", "219", "5", "redblock_score_passive", "3957", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2203", "220", "3", "combo_maintain_active", "3965", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2204", "220", "4", "combo_maintain_active", "3965", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2205", "220", "5", "combo_maintain_active", "3965", "4", "3975", "combo_maintain_active", "3965", "2", "3973", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2213", "221", "3", "fever_raise_active", "3966", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2214", "221", "4", "fever_raise_active", "3966", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2215", "221", "5", "fever_raise_active", "3966", "4", "3975", "nekoskill_appear_passive", "3962", "4", "3975", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2223", "222", "3", "redblock_score_passive", "3957", "3", "3974", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2224", "222", "4", "redblock_score_passive", "3957", "4", "3975", "", "", "", "", "", "", "", ""));
			Rows.Add( new MNP_NekoSkillRow("Skill2225", "222", "5", "redblock_score_passive", "3957", "4", "3975", "redblock_score_passive", "3957", "4", "3975", "", "", "", ""));
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
		public MNP_NekoSkillRow GetRow(rowIds in_RowID)
		{
			MNP_NekoSkillRow ret = null;
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
		public MNP_NekoSkillRow GetRow(string in_RowString)
		{
			MNP_NekoSkillRow ret = null;
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
