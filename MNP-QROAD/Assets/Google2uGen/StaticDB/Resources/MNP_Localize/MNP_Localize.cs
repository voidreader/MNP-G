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
	public class MNP_LocalizeRow : IGoogle2uRow
	{
		public int _local;
		public string _Korean;
		public string _English;
		public string _memo;
		public MNP_LocalizeRow(string __id, string __local, string __Korean, string __English, string __memo) 
		{
			{
			int res;
				if(int.TryParse(__local, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_local = res;
				else
					Debug.LogError("Failed To Convert _local string: "+ __local +" to int");
			}
			_Korean = __Korean.Trim();
			_English = __English.Trim();
			_memo = __memo.Trim();
		}

		public int Length { get { return 4; } }

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
					ret = _Korean.ToString();
					break;
				case 2:
					ret = _English.ToString();
					break;
				case 3:
					ret = _memo.ToString();
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
				case "Korean":
					ret = _Korean.ToString();
					break;
				case "English":
					ret = _English.ToString();
					break;
				case "memo":
					ret = _memo.ToString();
					break;
			}

			return ret;
		}
		public override string ToString()
		{
			string ret = System.String.Empty;
			ret += "{" + "local" + " : " + _local.ToString() + "} ";
			ret += "{" + "Korean" + " : " + _Korean.ToString() + "} ";
			ret += "{" + "English" + " : " + _English.ToString() + "} ";
			ret += "{" + "memo" + " : " + _memo.ToString() + "} ";
			return ret;
		}
	}
	public sealed class MNP_Localize : IGoogle2uDB
	{
		public enum rowIds {
			L1000, L1001, L1002, L1003, L1004, L1005, L1006, L1007, L1008, L1009, L1010, L1011, L1012, L1013, L1014, L1015, L1016, L1017
			, L1018, L1019, L1020, L1021, L1022, L1023, L1024, L1025, L1026, L1027, L1028, L1029, L1030, L1031, L1032, L1033, L1034, L1035, L1036, L1037
			, L1038, L1039, L1040, L1041, L1042, L1043, L1044, L1045, L1046, L1047, L1048, L1049, L1050, L1051, L1052, L1053, L1054, L1055, L1056, L1057
			, L1058, L1059, L1060, L1061, L1062, L1063, L1064, L1065, L1066, L1067, L1068, L1069, L1070, L1071, L1072, L1073, L1074, L1075, L1076, L1077
			, L1078, L1079, L1080, L1081, L1082, L1083, L1084, L1085, L1086, L1087, L1088, L1089, L1090, L1091, L1092, L1093, L1094, L1095, L1096, L1097
			, L1098, L1099, L1100, L1101, L1102, L1103, L1104, L1105, L1106, L1107, L1108, L1109, L1110, L1111, L1112, L1113, L1114, L1115, L1116, L1117
			, L1118, L1119, L1120, L1121, L1122, L1123, L1124, L1125, L1126, L1127, L1128, L1129, L1130, L1131, L1132, L1133, L1134, L1135, L1136, L1137
			, L1138, L1139, L1140, L1141, L1142, L1143, L1144, L1145, L1146, L1147, L1148, L1149, L1150, L1151, L1152, L1153, L1154, L1155, L1156, L1157
			, L1158, L1159, L1160, L1161, L1162, L1163, L1164, L1165, L1166, L1167, L1168, L1169, L1170, L1171, L1172, L1173, L1174, L1175, L1176, L1177
			, L1178, L1179, L1180, L1181, L1182, L1183, L1184, L1185, L1186, L1187, L1188, L1189, L1190, L1191, L1192, L1193, L1194, L1195, L1196, L1197
			, L1198, L1199, L1200, L1201, L1202, L1203, L1204, L1205, L1206, L1207, L1208, L1209, L1210, L1211, L1212, L1213, L1214, L1215, L1216, L1217
			, L1218, L1219, L1220, L1221, L1222, L1223, L1224, L1225, L1226, L1227, L2000, L2001, L2002, L2003, L2004, L2005, L2006, L2007, L2008, L2009
			, L2010, L2011, L2012, L2013, L2014, L2015, L2016, L2017, L2018, L2019, L2020, L2021, L2022, L2023, L2024, L2025, L2026, L2027, L2028, L2029
			, L2030, L2031, L2032, L2033, L2034, L2035, L2036, L2037, L2038, L2039, L2040, L2041, L2042, L2043, L2044, L2045, L2046, L2047, L2048, L2049
			, L2050, L2051, L2052, L2053, L2054, L2055, L2056, L2057, L2058, L2059, L2060, L2061, L2062, L2063, L2064, L2065, L2066, L2067, L2068, L2069
			, L2070, L2071, L2072, L2073, L2074, L2075, L2076, L2077, L2078, L2079, L2080, L2081, L2082, L2083, L2084, L2085, L2086, L2087, L2088, L2089
			, L2090, L2091, L2092, L2093, L2094, L2095, L2096, L2097, L2098, L2099, L2100, L2101, L2102, L2103, L2104, L2105, L2106, L2107, L2108, L2109
			, L2110, L2111, L2112, L2113, L2114, L2115, L2116, L2117, L2118, L2119, L2120, L2121, L2122, L2123, L2124, L2125, L2126, L2127, L2128, L2129
			, L2130, L2131, L2132, L2133, L2134, L2135, L2136, L2137, L2138, L2139, L2140, L2141, L2142, L2143, L2144, L2145, L2146, L2147, L2148, L2149
			, L2150, L2151, L2152, L2153, L2154, L2155, L2156, L2157, L2158, L2159, L2160, L2161, L2162, L2163, L2164, L2165, L2166, L2167, L2168, L2169
			, L2170, L2171, L2172, L2173, L2174, L2175, L2176, L2177, L2178, L2179, L2180, L2181, L2182, L2183, L2184, L2185, L2186, L2187, L2188, L2189
			, L2190, L2191, L2192, L2193, L2194, L2195, L2196, L2197, L2198, L2199, L2200, L2201, L2202, L2203, L2204, L2205, L2206, L2207, L2208, L2209
			, L2210, L2211, L2212, L2213, L2214, L2215, L2216, L2217, L2218, L2219, L2220, L2221, L2222, L2500, L2501, L2502, L2503, L2504, L2505, L2506
			, L2507, L2508, L2509, L2512, L2513, L2514, L2515, L2516, L2517, L2518, L2519, L2520, L2521, L2522, L2523, L2524, L2700, L2701, L2702, L2703
			, L2704, L2705, L2706, L2707, L2708, L2709, L2710, L2711, L2712, L2713, L2714, L2715, L2716, L2717, L2718, L2719, L2720, L2721, L2722, L2723
			, L2724, L3000, L3001, L3002, L3003, L3004, L3005, L3006, L3007, L3008, L3009, L3010, L3011, L3012, L3013, L3014, L3015, L3016, L3017, L3018
			, L3019, L3020, L3021, L3022, L3023, L3024, L3025, L3026, L3027, L3028, L3029, L3030, L3031, L3032, L3033, L3034, L3035, L3036, L3037, L3038
			, L3039, L3040, L3041, L3042, L3043, L3044, L3045, L3046, L3047, L3048, L3049, L3050, L3051, L3052, L3053, L3054, L3055, L3056, L3057, L3058
			, L3059, L3060, L3061, L3062, L3063, L3064, L3065, L3066, L3067, L3068, L3069, L3070, L3071, L3072, L3073, L3074, L3076, L3077, L3078, L3079
			, L3080, L3081, L3082, L3083, L3084, L3085, L3086, L3087, L3088, L3089, L3090, L3091, L3092, L3093, L3094, L3095, L3096, L3097, L3098, L3099
			, L3100, L3101, L3102, L3103, L3104, L3105, L3106, L3107, L3108, L3109, L3110, L3111, L3112, L3113, L3114, L3115, L3116, L3117, L3118, L3119
			, L3120, L3121, L3122, L3123, L3124, L3125, L3126, L3127, L3128, L3129, L3130, L3131, L3132, L3137, L3138, L3139, L3140, L3141, L3142, L3143
			, L3144, L3145, L3146, L3147, L3148, L3149, L3150, L3151, L3152, L3153, L3170, L3171, L3180, L3181, L3182, L3183, L3184, L3185, L3186, L3200
			, L3201, L3202, L3203, L3204, L3205, L3206, L3207, L3208, L3209, L3210, L3211, L3212, L3213, L3214, L3215, L3216, L3217, L3218, L3219, L3220
			, L3221, L3222, L3223, L3224, L3225, L3226, L3227, L3228, L3229, L3230, L3231, L3232, L3233, L3234, L3235, L3236, L3237, L3238, L3239, L3240
			, L3241, L3242, L3243, L3244, L3245, L3246, L3247, L3248, L3249, L3250, L3251, L3252, L3253, L3254, L3255, L3288, L3289, L3290, L3292, L3295
			, L3298, L3299, L3300, L3301, L3306, L3307, L3400, L3401, L3402, L3403, L3404, L3405, L3406, L3407, L3408, L3409, L3410, L3411, L3412, L3418
			, L3419, L3420, L3421, L3422, L3423, L3424, L3425, L3426, L3427, L3428, L3429, L3430, L3431, L3432, L3433, L3434, L3435, L3436, L3437, L3438
			, L3439, L3440, L3441, L3442, L3443, L3444, L3445, L3446, L3447, L3448, L3449, L3450, L3451, L3452, L3453, L3454, L3455, L3456, L3457, L3458
			, L3459, L3460, L3461, L3462, L3463, L3464, L3465, L3466, L3467, L3468, L3469, L3470, L3471, L3472, L3473, L3474, L3475, L3476, L3477, L3478
			, L3479, L3480, L3481, L3482, L3483, L3484, L3485, L3486, L3487, L3488, L3489, L3490, L3491, L3492, L3493, L3494, L3495, L3496, L3497, L3498
			, L3500, L3501, L3502, L3503, L3504, L3505, L3506, L3600, L3601, L3602, L3603, L3604, L3605, L3606, L3607, L3608, L3609, L3610, L3611, L3822
			, L3900, L3901, L3902, L3950, L3951, L3952, L3953, L3954, L3955, L3956, L3957, L3958, L3959, L3960, L3961, L3962, L3963, L3964, L3965, L3966
			, L3967, L3968, L3969, L3970, L3971, L3972, L3973, L3974, L3975, L3976, L3977, L3978, L3979, L3980, L3981, L3982, L3983, L3984, L3985, L3986
			, L3987, L3988, L3989, L4100, L4101, L4102, L4103, L4104, L4105, L4106, L4107, L4108, L4109, L4110, L4111, L4112, L4113, L4114, L4115, L4116
			, L4117, L4118, L4119, L4120, L4121, L4122, L4123, L4124, L4125, L4126, L4127, L4128, L4129, L4130, L4131, L4132, L4133, L4134, L4135, L4136
			, L4137, L4138, L4139, L4140, L4141, L4142, L4143, L4144, L4145, L4146, L4147, L4148, L4149, L4150, L4151, L4152, L4153, L4154, L4155, L4156
			, L4157, L4158, L4159, L4160, L4161, L4162, L4163, L4164, L4165, L4166, L4167, L4168, L4169, L4170, L4171, L4172, L4173, L4174, L4200, L4201
			, L4202, L4203, L4204, L4205, L4206, L4207, L4208, L4209, L4210, L4211, L4212, L4213, L4214, L4215, L4216, L4217, L4218, L4219, L4220, L4221
			, L4222, L4223, L4224, L4225, L4226, L4227, L4228, L4229, L4230, L4231, L4232, L4233, L4234, L4235, L4236, L4237, L4238, L4239, L4240, L4241
			, L4242, L4243, L4244, L4245, L4246, L4247, L4248, L4249, L4250, L4251, L4252, L4253, L4254, L4255, L4256, L4257, L4258, L4259, L4260, L4261
			, L4262, L4297, L4298, L4299, L4300, L4301, L4302, L4303, L4304, L4305, L4306, L4307, L4308, L4309, L4310, L4311, L4312, L4313, L4314, L4315
			, L4316, L4317, L4318, L4319, L4320, L4321, L4322, L4323, L4324, L4325, L4326, L4327, L4328, L4329, L4330, L4331, L4332, L4333, L4334, L4335
			, L4336, L4337, L4338, L4339, L4340, L4341, L4342, L4343, L4344, L4345, L4346, L4347, L4348, L4349, L4350, L4351, L4352, L4353, L4354, L4355
			, L4356, L4357, L4358, L4359, L4360, L4361, L4362, L4363, L4364, L5050, L5051, L5052, L5053, L5054, L5055, L5056, L5057, L5058, L5059, L5060
			, L5061, L5062, L5150, L5151, L5152, L5153, L5154, L5155, L5156, L5157, L5158, L5159, L5160, L5161, L5162, L5300, L5301, L5302, L5303, L5304
			, L5305, L5306, L5307, L5308, L5309, L5310, L5311, L5312, L5313, L5314, L5315, L5316, L5317, L5318, L5319, L5320, L5321, L5322, L5323, L5324
			, L5325, L5326, L5327, L5328, L5329, L5330, L5331, L5332, L5333, L5334, L5335, L5336, L5337, L5338, L5339, L5340, L5341, L5342, L5343, L5344
			, L5345, L5346, L5347, L5348, L5349, L5350, L5351, L5352, L5353, L5354, L5355, L5356, L5357, L5358, L5359, L5360, L5361, L5362, L5363, L5364
			, L5365, L5366, L6000, L6001, L6002, L6003, L6004, L6005, L6006, L6007, L6008, L6009, L6010, L6011, L6012, L6013, L6014, L6015, L6016, L6017
			, L6018, L6019, L6020, L6021, L6022, L6023, L6024, L6025, L6026, L6027, L6028, L6029, L6030, L6031, L6032, L6033, L6034, L6035, L6036, L6037
			, L6038, L6039, L6040, L6500, L6501, L6502, L6503, L6504, L6505, L6506, L6507, L6508, L6509, L6510, L6511, L6512, L6513, L6514, L6515, L6516
			, L6517, L6518, L6519, L6520, L6600, L6601, L6602, L6603, L6604, L6605, L6606, L6607, L6608, L6609, L6610, L6611
		};
		public string [] rowNames = {
			"L1000", "L1001", "L1002", "L1003", "L1004", "L1005", "L1006", "L1007", "L1008", "L1009", "L1010", "L1011", "L1012", "L1013", "L1014", "L1015", "L1016", "L1017"
			, "L1018", "L1019", "L1020", "L1021", "L1022", "L1023", "L1024", "L1025", "L1026", "L1027", "L1028", "L1029", "L1030", "L1031", "L1032", "L1033", "L1034", "L1035", "L1036", "L1037"
			, "L1038", "L1039", "L1040", "L1041", "L1042", "L1043", "L1044", "L1045", "L1046", "L1047", "L1048", "L1049", "L1050", "L1051", "L1052", "L1053", "L1054", "L1055", "L1056", "L1057"
			, "L1058", "L1059", "L1060", "L1061", "L1062", "L1063", "L1064", "L1065", "L1066", "L1067", "L1068", "L1069", "L1070", "L1071", "L1072", "L1073", "L1074", "L1075", "L1076", "L1077"
			, "L1078", "L1079", "L1080", "L1081", "L1082", "L1083", "L1084", "L1085", "L1086", "L1087", "L1088", "L1089", "L1090", "L1091", "L1092", "L1093", "L1094", "L1095", "L1096", "L1097"
			, "L1098", "L1099", "L1100", "L1101", "L1102", "L1103", "L1104", "L1105", "L1106", "L1107", "L1108", "L1109", "L1110", "L1111", "L1112", "L1113", "L1114", "L1115", "L1116", "L1117"
			, "L1118", "L1119", "L1120", "L1121", "L1122", "L1123", "L1124", "L1125", "L1126", "L1127", "L1128", "L1129", "L1130", "L1131", "L1132", "L1133", "L1134", "L1135", "L1136", "L1137"
			, "L1138", "L1139", "L1140", "L1141", "L1142", "L1143", "L1144", "L1145", "L1146", "L1147", "L1148", "L1149", "L1150", "L1151", "L1152", "L1153", "L1154", "L1155", "L1156", "L1157"
			, "L1158", "L1159", "L1160", "L1161", "L1162", "L1163", "L1164", "L1165", "L1166", "L1167", "L1168", "L1169", "L1170", "L1171", "L1172", "L1173", "L1174", "L1175", "L1176", "L1177"
			, "L1178", "L1179", "L1180", "L1181", "L1182", "L1183", "L1184", "L1185", "L1186", "L1187", "L1188", "L1189", "L1190", "L1191", "L1192", "L1193", "L1194", "L1195", "L1196", "L1197"
			, "L1198", "L1199", "L1200", "L1201", "L1202", "L1203", "L1204", "L1205", "L1206", "L1207", "L1208", "L1209", "L1210", "L1211", "L1212", "L1213", "L1214", "L1215", "L1216", "L1217"
			, "L1218", "L1219", "L1220", "L1221", "L1222", "L1223", "L1224", "L1225", "L1226", "L1227", "L2000", "L2001", "L2002", "L2003", "L2004", "L2005", "L2006", "L2007", "L2008", "L2009"
			, "L2010", "L2011", "L2012", "L2013", "L2014", "L2015", "L2016", "L2017", "L2018", "L2019", "L2020", "L2021", "L2022", "L2023", "L2024", "L2025", "L2026", "L2027", "L2028", "L2029"
			, "L2030", "L2031", "L2032", "L2033", "L2034", "L2035", "L2036", "L2037", "L2038", "L2039", "L2040", "L2041", "L2042", "L2043", "L2044", "L2045", "L2046", "L2047", "L2048", "L2049"
			, "L2050", "L2051", "L2052", "L2053", "L2054", "L2055", "L2056", "L2057", "L2058", "L2059", "L2060", "L2061", "L2062", "L2063", "L2064", "L2065", "L2066", "L2067", "L2068", "L2069"
			, "L2070", "L2071", "L2072", "L2073", "L2074", "L2075", "L2076", "L2077", "L2078", "L2079", "L2080", "L2081", "L2082", "L2083", "L2084", "L2085", "L2086", "L2087", "L2088", "L2089"
			, "L2090", "L2091", "L2092", "L2093", "L2094", "L2095", "L2096", "L2097", "L2098", "L2099", "L2100", "L2101", "L2102", "L2103", "L2104", "L2105", "L2106", "L2107", "L2108", "L2109"
			, "L2110", "L2111", "L2112", "L2113", "L2114", "L2115", "L2116", "L2117", "L2118", "L2119", "L2120", "L2121", "L2122", "L2123", "L2124", "L2125", "L2126", "L2127", "L2128", "L2129"
			, "L2130", "L2131", "L2132", "L2133", "L2134", "L2135", "L2136", "L2137", "L2138", "L2139", "L2140", "L2141", "L2142", "L2143", "L2144", "L2145", "L2146", "L2147", "L2148", "L2149"
			, "L2150", "L2151", "L2152", "L2153", "L2154", "L2155", "L2156", "L2157", "L2158", "L2159", "L2160", "L2161", "L2162", "L2163", "L2164", "L2165", "L2166", "L2167", "L2168", "L2169"
			, "L2170", "L2171", "L2172", "L2173", "L2174", "L2175", "L2176", "L2177", "L2178", "L2179", "L2180", "L2181", "L2182", "L2183", "L2184", "L2185", "L2186", "L2187", "L2188", "L2189"
			, "L2190", "L2191", "L2192", "L2193", "L2194", "L2195", "L2196", "L2197", "L2198", "L2199", "L2200", "L2201", "L2202", "L2203", "L2204", "L2205", "L2206", "L2207", "L2208", "L2209"
			, "L2210", "L2211", "L2212", "L2213", "L2214", "L2215", "L2216", "L2217", "L2218", "L2219", "L2220", "L2221", "L2222", "L2500", "L2501", "L2502", "L2503", "L2504", "L2505", "L2506"
			, "L2507", "L2508", "L2509", "L2512", "L2513", "L2514", "L2515", "L2516", "L2517", "L2518", "L2519", "L2520", "L2521", "L2522", "L2523", "L2524", "L2700", "L2701", "L2702", "L2703"
			, "L2704", "L2705", "L2706", "L2707", "L2708", "L2709", "L2710", "L2711", "L2712", "L2713", "L2714", "L2715", "L2716", "L2717", "L2718", "L2719", "L2720", "L2721", "L2722", "L2723"
			, "L2724", "L3000", "L3001", "L3002", "L3003", "L3004", "L3005", "L3006", "L3007", "L3008", "L3009", "L3010", "L3011", "L3012", "L3013", "L3014", "L3015", "L3016", "L3017", "L3018"
			, "L3019", "L3020", "L3021", "L3022", "L3023", "L3024", "L3025", "L3026", "L3027", "L3028", "L3029", "L3030", "L3031", "L3032", "L3033", "L3034", "L3035", "L3036", "L3037", "L3038"
			, "L3039", "L3040", "L3041", "L3042", "L3043", "L3044", "L3045", "L3046", "L3047", "L3048", "L3049", "L3050", "L3051", "L3052", "L3053", "L3054", "L3055", "L3056", "L3057", "L3058"
			, "L3059", "L3060", "L3061", "L3062", "L3063", "L3064", "L3065", "L3066", "L3067", "L3068", "L3069", "L3070", "L3071", "L3072", "L3073", "L3074", "L3076", "L3077", "L3078", "L3079"
			, "L3080", "L3081", "L3082", "L3083", "L3084", "L3085", "L3086", "L3087", "L3088", "L3089", "L3090", "L3091", "L3092", "L3093", "L3094", "L3095", "L3096", "L3097", "L3098", "L3099"
			, "L3100", "L3101", "L3102", "L3103", "L3104", "L3105", "L3106", "L3107", "L3108", "L3109", "L3110", "L3111", "L3112", "L3113", "L3114", "L3115", "L3116", "L3117", "L3118", "L3119"
			, "L3120", "L3121", "L3122", "L3123", "L3124", "L3125", "L3126", "L3127", "L3128", "L3129", "L3130", "L3131", "L3132", "L3137", "L3138", "L3139", "L3140", "L3141", "L3142", "L3143"
			, "L3144", "L3145", "L3146", "L3147", "L3148", "L3149", "L3150", "L3151", "L3152", "L3153", "L3170", "L3171", "L3180", "L3181", "L3182", "L3183", "L3184", "L3185", "L3186", "L3200"
			, "L3201", "L3202", "L3203", "L3204", "L3205", "L3206", "L3207", "L3208", "L3209", "L3210", "L3211", "L3212", "L3213", "L3214", "L3215", "L3216", "L3217", "L3218", "L3219", "L3220"
			, "L3221", "L3222", "L3223", "L3224", "L3225", "L3226", "L3227", "L3228", "L3229", "L3230", "L3231", "L3232", "L3233", "L3234", "L3235", "L3236", "L3237", "L3238", "L3239", "L3240"
			, "L3241", "L3242", "L3243", "L3244", "L3245", "L3246", "L3247", "L3248", "L3249", "L3250", "L3251", "L3252", "L3253", "L3254", "L3255", "L3288", "L3289", "L3290", "L3292", "L3295"
			, "L3298", "L3299", "L3300", "L3301", "L3306", "L3307", "L3400", "L3401", "L3402", "L3403", "L3404", "L3405", "L3406", "L3407", "L3408", "L3409", "L3410", "L3411", "L3412", "L3418"
			, "L3419", "L3420", "L3421", "L3422", "L3423", "L3424", "L3425", "L3426", "L3427", "L3428", "L3429", "L3430", "L3431", "L3432", "L3433", "L3434", "L3435", "L3436", "L3437", "L3438"
			, "L3439", "L3440", "L3441", "L3442", "L3443", "L3444", "L3445", "L3446", "L3447", "L3448", "L3449", "L3450", "L3451", "L3452", "L3453", "L3454", "L3455", "L3456", "L3457", "L3458"
			, "L3459", "L3460", "L3461", "L3462", "L3463", "L3464", "L3465", "L3466", "L3467", "L3468", "L3469", "L3470", "L3471", "L3472", "L3473", "L3474", "L3475", "L3476", "L3477", "L3478"
			, "L3479", "L3480", "L3481", "L3482", "L3483", "L3484", "L3485", "L3486", "L3487", "L3488", "L3489", "L3490", "L3491", "L3492", "L3493", "L3494", "L3495", "L3496", "L3497", "L3498"
			, "L3500", "L3501", "L3502", "L3503", "L3504", "L3505", "L3506", "L3600", "L3601", "L3602", "L3603", "L3604", "L3605", "L3606", "L3607", "L3608", "L3609", "L3610", "L3611", "L3822"
			, "L3900", "L3901", "L3902", "L3950", "L3951", "L3952", "L3953", "L3954", "L3955", "L3956", "L3957", "L3958", "L3959", "L3960", "L3961", "L3962", "L3963", "L3964", "L3965", "L3966"
			, "L3967", "L3968", "L3969", "L3970", "L3971", "L3972", "L3973", "L3974", "L3975", "L3976", "L3977", "L3978", "L3979", "L3980", "L3981", "L3982", "L3983", "L3984", "L3985", "L3986"
			, "L3987", "L3988", "L3989", "L4100", "L4101", "L4102", "L4103", "L4104", "L4105", "L4106", "L4107", "L4108", "L4109", "L4110", "L4111", "L4112", "L4113", "L4114", "L4115", "L4116"
			, "L4117", "L4118", "L4119", "L4120", "L4121", "L4122", "L4123", "L4124", "L4125", "L4126", "L4127", "L4128", "L4129", "L4130", "L4131", "L4132", "L4133", "L4134", "L4135", "L4136"
			, "L4137", "L4138", "L4139", "L4140", "L4141", "L4142", "L4143", "L4144", "L4145", "L4146", "L4147", "L4148", "L4149", "L4150", "L4151", "L4152", "L4153", "L4154", "L4155", "L4156"
			, "L4157", "L4158", "L4159", "L4160", "L4161", "L4162", "L4163", "L4164", "L4165", "L4166", "L4167", "L4168", "L4169", "L4170", "L4171", "L4172", "L4173", "L4174", "L4200", "L4201"
			, "L4202", "L4203", "L4204", "L4205", "L4206", "L4207", "L4208", "L4209", "L4210", "L4211", "L4212", "L4213", "L4214", "L4215", "L4216", "L4217", "L4218", "L4219", "L4220", "L4221"
			, "L4222", "L4223", "L4224", "L4225", "L4226", "L4227", "L4228", "L4229", "L4230", "L4231", "L4232", "L4233", "L4234", "L4235", "L4236", "L4237", "L4238", "L4239", "L4240", "L4241"
			, "L4242", "L4243", "L4244", "L4245", "L4246", "L4247", "L4248", "L4249", "L4250", "L4251", "L4252", "L4253", "L4254", "L4255", "L4256", "L4257", "L4258", "L4259", "L4260", "L4261"
			, "L4262", "L4297", "L4298", "L4299", "L4300", "L4301", "L4302", "L4303", "L4304", "L4305", "L4306", "L4307", "L4308", "L4309", "L4310", "L4311", "L4312", "L4313", "L4314", "L4315"
			, "L4316", "L4317", "L4318", "L4319", "L4320", "L4321", "L4322", "L4323", "L4324", "L4325", "L4326", "L4327", "L4328", "L4329", "L4330", "L4331", "L4332", "L4333", "L4334", "L4335"
			, "L4336", "L4337", "L4338", "L4339", "L4340", "L4341", "L4342", "L4343", "L4344", "L4345", "L4346", "L4347", "L4348", "L4349", "L4350", "L4351", "L4352", "L4353", "L4354", "L4355"
			, "L4356", "L4357", "L4358", "L4359", "L4360", "L4361", "L4362", "L4363", "L4364", "L5050", "L5051", "L5052", "L5053", "L5054", "L5055", "L5056", "L5057", "L5058", "L5059", "L5060"
			, "L5061", "L5062", "L5150", "L5151", "L5152", "L5153", "L5154", "L5155", "L5156", "L5157", "L5158", "L5159", "L5160", "L5161", "L5162", "L5300", "L5301", "L5302", "L5303", "L5304"
			, "L5305", "L5306", "L5307", "L5308", "L5309", "L5310", "L5311", "L5312", "L5313", "L5314", "L5315", "L5316", "L5317", "L5318", "L5319", "L5320", "L5321", "L5322", "L5323", "L5324"
			, "L5325", "L5326", "L5327", "L5328", "L5329", "L5330", "L5331", "L5332", "L5333", "L5334", "L5335", "L5336", "L5337", "L5338", "L5339", "L5340", "L5341", "L5342", "L5343", "L5344"
			, "L5345", "L5346", "L5347", "L5348", "L5349", "L5350", "L5351", "L5352", "L5353", "L5354", "L5355", "L5356", "L5357", "L5358", "L5359", "L5360", "L5361", "L5362", "L5363", "L5364"
			, "L5365", "L5366", "L6000", "L6001", "L6002", "L6003", "L6004", "L6005", "L6006", "L6007", "L6008", "L6009", "L6010", "L6011", "L6012", "L6013", "L6014", "L6015", "L6016", "L6017"
			, "L6018", "L6019", "L6020", "L6021", "L6022", "L6023", "L6024", "L6025", "L6026", "L6027", "L6028", "L6029", "L6030", "L6031", "L6032", "L6033", "L6034", "L6035", "L6036", "L6037"
			, "L6038", "L6039", "L6040", "L6500", "L6501", "L6502", "L6503", "L6504", "L6505", "L6506", "L6507", "L6508", "L6509", "L6510", "L6511", "L6512", "L6513", "L6514", "L6515", "L6516"
			, "L6517", "L6518", "L6519", "L6520", "L6600", "L6601", "L6602", "L6603", "L6604", "L6605", "L6606", "L6607", "L6608", "L6609", "L6610", "L6611"
		};
		public System.Collections.Generic.List<MNP_LocalizeRow> Rows = new System.Collections.Generic.List<MNP_LocalizeRow>();

		public static MNP_Localize Instance
		{
			get { return NestedMNP_Localize.instance; }
		}

		private class NestedMNP_Localize
		{
			static NestedMNP_Localize() { }
			internal static readonly MNP_Localize instance = new MNP_Localize();
		}

		private MNP_Localize()
		{
			Rows.Add( new MNP_LocalizeRow("L1000", "1000", "삼색냥", "Calico-mew", "캐릭터 이름 시작"));
			Rows.Add( new MNP_LocalizeRow("L1001", "1001", "옐로냥", "Yellow-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1002", "1002", "핑크냥", "Pink-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1003", "1003", "콧수염냥", "Mastache-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1004", "1004", "타이거냥", "Tiger-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1005", "1005", "그린냥", "Green-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1006", "1006", "퍼플냥", "Purple-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1007", "1007", "닭벼슬냥", "Cockscomb-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1008", "1008", "귤냥", "Hassaku-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1009", "1009", "알알이냥", "Tsubutsubu-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1010", "1010", "꼭지없다냥", "Hetanash-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1011", "1011", "신맛난다냥", "Sour-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1012", "1012", "거품난다냥", "Fizzy-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1013", "1013", "사이다냥", "Cider-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1014", "1014", "라무네냥", "Ramune-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1015", "1015", "탄산빠진냥", "Fizzless-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1016", "1016", "마지고로냥", "Maji-Goro mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1017", "1017", "실패냥", "Mistake-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1018", "1018", "스몰트릭냥", "Small-Trick-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1019", "1019", "빅트릭냥", "Big-Trick-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1020", "1020", "데빌냥", "Devil-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1021", "1021", "소악마냥", "Little-Fiend-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1022", "1022", "가고이냥", "Gaagoi-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1023", "1023", "마왕냥", "Archfiend-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1024", "1024", "패스너냥", "Fasten-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1025", "1025", "스타냥", "Star-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1026", "1026", "블랙홀냥", "Black-Hole-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1027", "1027", "메인바디냥", "Main-Body-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1028", "1028", "레드냥", "Red-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1029", "1029", "핸섬블루냥", "Handsome-Bule-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1030", "1030", "발명가그린냥", "Inventor-Green-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1031", "1031", "헝그리옐로냥", "Hungry-Yellow-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1032", "1032", "봄바냥", "Bomba-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1033", "1033", "대참사냥", "Disaster-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1034", "1034", "타버린냥", "Burnt-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1035", "1035", "아프로x1.5냥", "Afro x 1.5-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1036", "1036", "어글리냥", "Ugly-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1037", "1037", "토이박스냥", "Toy-Box-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1038", "1038", "오렌지박스냥", "Orange-Carton-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1039", "1039", "꼇다냥", "Loaded-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1040", "1040", "검사냥", "Kenshi-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1041", "1041", "대나무소드냥", "Bamboo-Sword-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1042", "1042", "라이트세이버냥", "Light Saber-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1043", "1043", "롤리팝냥", "Lollipop-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1044", "1044", "우유냥", "Milk-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1045", "1045", "커피우유냥", "Coffee-Flavored-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1046", "1046", "딸기우유냥", "Strawberry-Flavored-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1047", "1047", "후르츠우유냥", "Fruit-Flavored-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1048", "1048", "불만가득냥", "Grumpy-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1049", "1049", "불안해냥", "Anxious-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1050", "1050", "장난꾸러기냥", "Mischief-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1051", "1051", "음흉하다냥", "Boor-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1052", "1052", "뮤톤냥", "Mewton-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1053", "1053", "프로페서냥", "Professor-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1054", "1054", "닥터냥", "Doctor-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1055", "1055", "매드냥", "Mad-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1056", "1056", "하나짱냥", "Hana-chan-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1057", "1057", "단델리온냥", "Dandelion-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1058", "1058", "튤립냥", "Tulip-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1059", "1059", "팬지냥", "Pansy-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1060", "1060", "커피냥", "Coffee-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1061", "1061", "아메리카노냥", "American-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1062", "1062", "에스프레소냥", "Espresso-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1063", "1063", "라떼냥", "Latte-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1064", "1064", "츄비냥", "Chubby-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1065", "1065", "에어로빅냥", "Aerobics-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1066", "1066", "헬스냥", "Muscle-Training-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1067", "1067", "이모탈냥", "Immortal-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1068", "1068", "스키니냥", "Skinny-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1069", "1069", "본냥", "Bone-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1070", "1070", "스킨냥", "Skin-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1071", "1071", "그레이냥", "Grey-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1072", "1072", "기업전사냥", "Corporate-Warrior-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1073", "1073", "신입냥", "Freshman-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1074", "1074", "2년차냥", "Junior-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1075", "1075", "회식부장냥", "Party-Animal-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1076", "1076", "미궁에빠진탐정냥", "Lost-Detective-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1077", "1077", "미궁에빠진수사냥", "Lost Investigation-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1078", "1078", "미궁에빠진추리냥", "Lost-Reasoning-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1079", "1079", "미궁에빠진발견냥", "Lost-Discovery-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1080", "1080", "폭주족냥", "Delinquent-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1081", "1081", "잘부탁해냥", "Thanks-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1082", "1082", "킬러냥", "Killer-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1083", "1083", "싸움의신냥", "Mighty-Fighter-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1084", "1084", "무사도냥", "Samurai-Spirit-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1085", "1085", "방랑무사냥", "Outlaw-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1086", "1086", "잠입수사냥", "Magistrate-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1087", "1087", "별난영주냥", "Eccentric-Lord-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1088", "1088", "시노비냥", "Ninja-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1089", "1089", "하급닌자냥", "Lower-Man-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1090", "1090", "상급닌자냥", "Upper-Man-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1091", "1091", "매력냥", "Seductive-Female-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1092", "1092", "백묘의왕냥", "King-of-Cats-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1093", "1093", "야생의방심냥", "Wild-Inattentiveness-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1094", "1094", "야생의본능냥", "Wild-Instinct-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1095", "1095", "야생의정체냥", "Wild-Inside-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1096", "1096", "리더냥", "Leader-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1097", "1097", "드럼냥", "Drum-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1098", "1098", "우쿨렐레냥", "Ukulele-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1099", "1099", "리코더냥", "Recorder-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1100", "1100", "실로폰냥", "Xylophone-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1101", "1101", "탬버린냥", "Tambourine-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1102", "1102", "트라이앵글냥", "Triangle-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1103", "1103", "아코디언냥", "Accordion-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1104", "1104", "튜바냥", "Tuba-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1105", "1105", "심벌즈냥", "Cymbal-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1106", "1106", "베이스드럼냥", "Bass-Drum-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1107", "1107", "하프냥", "Harp-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1108", "1108", "허니냥", "HoneyBee-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1109", "1109", "메이플냥", "MapleBee-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1110", "1110", "밀크냥", "MilkBee-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1111", "1111", "간장냥", "SoysauceBee-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1112", "1112", "젠틀맨냥", "Gentleman-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1113", "1113", "멋쟁이신사냥", "Fashionable-Gent-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1114", "1114", "콧수염신사냥", "Bearded-Gentleman-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1115", "1115", "백마탄신사냥", "Gentelman-on-White-Horse-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1116", "1116", "선장냥", "Captain-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1117", "1117", "대박냥", "Big-Hit-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1118", "1118", "꽝냥", "No-Luck-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1119", "1119", "유령냥", "Ghost-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1120", "1120", "고로케냥", "Croquette-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1121", "1121", "크림냥", "Cream-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1122", "1122", "단호박냥", "Pumpkin-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1123", "1123", "새우프라이냥", "Fired-Prawn-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1124", "1124", "무쵸냥", "Mucho-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1125", "1125", "마라카스냥", "Maracas-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1126", "1126", "타코스냥", "Tacos-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1127", "1127", "선인장냥", "Cactus-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1128", "1128", "외톨이냥", "Lonesome-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1129", "1129", "파자마냥", "Pajamas-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1130", "1130", "삐에로냥", "Pierrot-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1131", "1131", "굿나잇냥", "Good-Night-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1132", "1132", "히하냥", "Heehaw-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1133", "1133", "퀵슈터냥", "Quick-Shooter-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1134", "1134", "로데오냥", "Rodeo-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1135", "1135", "라쏘냥", "Lasso-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1136", "1136", "메카밋치리냥", "Robo-Mitchiri-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1137", "1137", "육상타입냥", "Overland-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1138", "1138", "수중타입냥", "Underwater-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1139", "1139", "비행타입냥", "Airplane-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1140", "1140", "냥바냥", "Nyanva-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1141", "1141", "버그냥", "Bugged-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1142", "1142", "화성인냥", "Martian-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1143", "1143", "UFO냥", "UFO-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1144", "1144", "리본냥", "Ribbon-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1145", "1145", "컬러풀냥", "Colorful-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1146", "1146", "수박냥", "Watermelon-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1147", "1147", "선탠냥", "Sun-Kissed-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1148", "1148", "비틀냥", "Beetle-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1149", "1149", "불꽃놀이냥", "Fireworks-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1150", "1150", "하우스캣냥", "House-Cat-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1151", "1151", "허니냥(특별판)", "HoneyBee-nya (good-looking version)", ""));
			Rows.Add( new MNP_LocalizeRow("L1152", "1152", "미궁에빠진탐정냥(특별판)", "Lost-Detective-nyan (good-looking version)", ""));
			Rows.Add( new MNP_LocalizeRow("L1153", "1153", "고로케(특별판)", "Croquette-mew (good-looking version)", ""));
			Rows.Add( new MNP_LocalizeRow("L1154", "1154", "컬러풀(특별판)", "Colorful-mew (good-looking verison)", ""));
			Rows.Add( new MNP_LocalizeRow("L1155", "1155", "검사냥(특별판)", "Kenshi-nya (good-looking version)", ""));
			Rows.Add( new MNP_LocalizeRow("L1156", "1156", "섬머메모리냥", "Summer-Memories nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1157", "1157", "봉오도리냥", "Bon-Festival Dance-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1158", "1158", "바다괴물냥", "Sea-Monster-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1159", "1159", "삼단아이스냥", "Triple-Scoop Cone-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1160", "1160", "달구경냥", "Moon-Viewing-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1161", "1161", "코스모스냥", "Cosmos-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1162", "1162", "버니냥", "Bunny-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1163", "1163", "구운밤냥", "Roast-Chestnut-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1164", "1164", "잘했어냥", "Well-Done-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1165", "1165", "반짝반짝냥", "Glittering-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1166", "1166", "러블리냥", "Lovely-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1167", "1167", "빙글빙글냥", "Spinning-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1168", "1168", "뭔가이상해냥", "Something-Wrong-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1169", "1169", "청소당번냥", "Cleaning-Duty-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1170", "1170", "테니스냥", "Tenis-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1171", "1171", "더블냥", "Double-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1172", "1172", "트리플냥", "Triple-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1173", "1173", "파티혼냥", "Party Horn-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1174", "1174", "튜닝냥", "Turning-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1175", "1175", "모바일냥", "Mobiler-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1176", "1176", "세탁냥", "Laundry-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1177", "1177", "해체쇼냥", "Filleting-Show-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1178", "1178", "판매원냥", "Vender-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1179", "1179", "칸사이스타일냥", "Kansai-Style-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1180", "1180", "점박이냥", "Spotted-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1181", "1181", "보스냥", "Boss-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1182", "1182", "간호사냥", "Nurse-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1183", "1183", "트윈테일냥", "Twin-Tail-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1184", "1184", "카우벨냥", "Cowbell-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1185", "1185", "마네키네코냥", "Manekineko-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1186", "1186", "나이트냥", "Knight-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1187", "1187", "카레라이스냥", "Curry-Rice-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1188", "1188", "시쿠와사냥", "Shikuwasa-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1189", "1189", "스파이냥", "Spy-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1190", "1190", "마죠링냥", "Majorin-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1191", "1191", "냥코의지팡이냥", "Cat-Magic-Wand-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1192", "1192", "장화냥", "Long-boots-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1193", "1193", "단풍냥", "Maple-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1194", "1194", "더부룩냥", "Moppy-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1195", "1195", "티포트냥", "Tea-Pot-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1196", "1196", "진실냥", "Truth-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1197", "1197", "뮤지냥", "Musi-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1198", "1198", "베토냥", "Beethor-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1199", "1199", "포테이토냥", "Potato-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1200", "1200", "캐롯냥", "Carrot-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1201", "1201", "만드라고라냥", "Mandragora-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1202", "1202", "래디쉬냥", "Radish-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1203", "1203", "마스터냥", "Master-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1204", "1204", "소믈리에냥", "Sommelier-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1205", "1205", "지배인냥", "Manager-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1206", "1206", "나쁜척하는냥", "Bit-Bad-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1207", "1207", "모냐냥", "Mo-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1208", "1208", "코타츠냥", "Okota-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1209", "1209", "빨간코냥", "Red-Nosed-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1210", "1210", "두꺼워냥", "Bandled-Up-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1211", "1211", "장갑냥", "Gloves-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1212", "1212", "모자냥", "Beanie-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1213", "1213", "산타냥", "Santa-nya", ""));
			Rows.Add( new MNP_LocalizeRow("L1214", "1214", "다테마키냥", "Datemaki-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1215", "1215", "카마보코냥", "Kamaboko-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1216", "1216", "롤냥", "Roll-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1217", "1217", "키리탄포냥", "Kiritampo-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1218", "1218", "메데타이냥", "Medetai-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1219", "1219", "와일드냥", "Wild-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1220", "1220", "프렌치냥", "French-nyan", ""));
			Rows.Add( new MNP_LocalizeRow("L1221", "1221", "마로냥", "Maro-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1222", "1222", "히메냥", "Hi-mew", ""));
			Rows.Add( new MNP_LocalizeRow("L1223", "1223", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L1224", "1224", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L1225", "1225", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L1226", "1226", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L1227", "1227", "", "", "여기까지가 캐릭터 이름"));
			Rows.Add( new MNP_LocalizeRow("L2000", "2000", "언제나 양지에서 볕쬐기만 하고 있는 삼색털 고양이.\n\"밥\"이라는 소리만 들으면 병에서 튀어 나온다.", "Calico-mew is a fluffy cat that always basks in the sun.\nIt rushes out of the jar when someone says \"food.\"", ""));
			Rows.Add( new MNP_LocalizeRow("L2001", "2001", "삼색털의 노란색 고양이. \n밥을 먹고는 데굴데굴거리면서 행복해 한다. \n배로 통통 튀는 것에 빠져있는 것 같다.", "Three colored Yellow-nyan. The cat rolls around happily after eating meal. It seems Yellow-nyan is fond of bouncing with its stomach.", ""));
			Rows.Add( new MNP_LocalizeRow("L2002", "2002", "삼색털의 핑크색 고양이.\n\"고양이\"와 같은 날씬한 몸매를 목표로 \n하고 있으나 이 체형도 나쁘지는 \n않아라며  현실에서 눈을 돌리고 있다.", "Three colored Pink-nyan. Pink-nyan made a goal to be a slender body like \"Cat\", but Pink-nyan's ignoring the reality. The cat head in the sand making excuses like \"I'm not that bad right now.\"", ""));
			Rows.Add( new MNP_LocalizeRow("L2003", "2003", "코밑수염을 기른 댄디한 고양이.\n다른 고양이보다 겉모습도 마음도 어른스럽다.\n중년남성의 뱃살을 보고 동질감을 느끼고 있다.", "Mastache-nya who has a mustache looks more mature than other cats both inside and outside. This cat identified seeing middle-aged man's belly fat.", ""));
			Rows.Add( new MNP_LocalizeRow("L2004", "2004", "호랑이 모양의 작은 고양이.\n매끈매끈한 털을 자랑스러워 하는 것 같다.\n호랑이 모양은 자기가 사인펜으로 그린 것이다.", "Tiger-nya is a small tabby cat that is proud of its smooth fur coat. Tiger-nya actually hand-painted all the stripes on its fur!", ""));
			Rows.Add( new MNP_LocalizeRow("L2005", "2005", "호랑이 모양의 녹색 고양이.\n야채만 먹기 때문에 녹색이 되어 버렸다고 한다.\n자신을 지금 유행중인 초식계라고 생각하고 있다.", "A Green-mew with tiger pattern. Eating only vegetables  made its color green.\nGreen-mew consider itself cool herbivore cat in fashion these days.", ""));
			Rows.Add( new MNP_LocalizeRow("L2006", "2006", "호랑이 모양의 보라색 고양이.\n보라색 야채 쥬스를 좋아하는 건강 오타쿠.\n생야채를 주면 \n싫어하는 얼굴을 하고 먹지 않는다.", "A Purple-mew with tiger pattern. A health freak likes purple vegetable juice.\nBut Purple-mew makes a face fresh vegetables are offered and never eat them.", ""));
			Rows.Add( new MNP_LocalizeRow("L2007", "2007", "이유는 알 수 없지만 호랑이 모양이면서 \n닭 벼슬이 자라나 있는 고양이. \n깜짝 놀라면 몸이 굳어 버린다.\n다른 누구보다도 빨리 일어나 아침을 알리는 것이 일과이다.", "Nobody knows why Cockscomb-mew has a comb. The cat stiffens by getting surprised.\nGetting up early than anybody to announce the morning.", ""));
			Rows.Add( new MNP_LocalizeRow("L2008", "2008", "귤과 똑같이 생긴 오렌지색 고양이.\n마음이 깨끗해지는 향기가 난다.\n머리에 붙어 있는 꼭지는 탈부착이 가능한 것 같다.", "Hassaku-nya is colored just like the Japanese orange fruit of the same name and emits a lovely scent. Apparently, its head is detachable from its body!", ""));
			Rows.Add( new MNP_LocalizeRow("L2009", "2009", "뺨에 알맹이가 있는 고양이.\n감귤계의 진향 향기가 난다.\n몰래 알맹이 춤을 널리 퍼지게 하려 하고 있다.", "A cat with pulps on cheeks omitting nice citrus scent. Tsubutsubu-nya is secretly trying to spread \"tsubu-tsubu dance\" around the world.", ""));
			Rows.Add( new MNP_LocalizeRow("L2010", "2010", "자랑스럽게 여기던 꼭지가 떨어져버린 고양이.\n꼭지 대신에 귤을 붙이고 있다.\n예비 귤이 22개 있는것 같다.", "As Hetanash-nya lost its proud stem so the cat glued a tangerine instead of it.\nIt is said there are twenty two extra tangerines.", ""));
			Rows.Add( new MNP_LocalizeRow("L2011", "2011", "신맛나는 것 같은 입을 하고 있는 고양이.\n몸에서 신 냄새가 난다.\n열심히하면 달달한 냄새를 낼 수 있다고 생각하고 있다.", "A cat who has a mouth formed like it just ate something sour. And it smells sour.\nSour-nya thinks it could turn to sweet odor if it puts more effort in it.", ""));
			Rows.Add( new MNP_LocalizeRow("L2012", "2012", "빨갛고 탄산소리가 나는 발포계 고양이.\n가까이 가면 탄산 소리가 들린다.\n실제로는 탄산을 잘 마시지 못한다.", "Fizzy-nyan is a red and fizzy cat that emits bubbly sounds, just like a famous drink of the same name. Sadly though, Fizzy-nyan is allergic to carbonated drinks.", ""));
			Rows.Add( new MNP_LocalizeRow("L2013", "2013", "탄산 소리가 나는 녹색 발포계 고양이.\n가까이 가면 상쾌한 기분이 난다.\n세상의 모든 것 중에서 탄산을 제일 좋아한다.", "Cider-nyan is a green and fizzy cat that emits bubbly sounds. Just being around Cider, you feel refreshed.\nCider-nyan loves soda more than anything in the world.", ""));
			Rows.Add( new MNP_LocalizeRow("L2014", "2014", "탄산 소리가 나는 파란색 발포계 고양이.\n입으로 탄산소리를 내는 것에 빠져있다.\n과일을 엄청 좋아하는 것 같다.", "Ramune-nyan is a blue and fizzy cat that emits bubbly sounds. Ramune-nyan is into making bubbly sounds with its mouth. \nThe cat loves fruits.", ""));
			Rows.Add( new MNP_LocalizeRow("L2015", "2015", "전 탄산계 고양이.\n너무 까불다가 탄산이 빠져버렸다.\n탄산이 빠진 뒤로는 뒹굴거리는 시간이 늘어난 것 같다.", "Ex-carbonated cat. Being over excited, it has gone flat.\nFizzless-nyan is just loafing around all day after losing the bubbles.", ""));
			Rows.Add( new MNP_LocalizeRow("L2016", "2016", "마술을 보여주는 매지션 고양이.\n그러나 아무래도 말솜씨 쪽이 특기인 듯 하다. \n큰 기술을 보여달라니 귀찮으니까 설날까지 \n기다리라는 말을 들었다.", "Maji-Goro mew is a magician cat whose talking skills are far better than its magic skills! Also, Maji-Goro mew is too lazy to perform big-scale magic tricks.", ""));
			Rows.Add( new MNP_LocalizeRow("L2017", "2017", "마술을 좋아하지만 잘하지는 못하는 견습 고양이.\n눈동냥으로 따라 하고는 있으나 잘되지는 않는다.\n마술을 마법이라고 믿고 있는 것 같다.", "A magic-lover cat but not good at doing tricks. Although learning some tricks by imitation, it never is successful.\nMistake-mew believes magic tricks happen spontaneously.", ""));
			Rows.Add( new MNP_LocalizeRow("L2018", "2018", "누구라도 할 수 있는 간단한 마술만 가능한 고양이.\n할 수 있는 마술이 늘어나면 기뻐하게 되는 것 같으며\n마술을 선 보인 후에는 반드시 트릭을 공개해버린다.", "Small-Trick-mew can do only simple tricks anybody could do.\nAs it get too excited after mastering new tricks, the cat always explain the trick after the show.", ""));
			Rows.Add( new MNP_LocalizeRow("L2019", "2019", "아무것도 없는 곳에서 고양이를 나타나게 하는 \n고양이. 트릭도 장치도 없는 것 같으며 절대로 \n가르쳐주지 않는다. 제자들이 많이 있어서 상당히 \n존경받고 있는 것 같다.", "Big-Trick-mew makes a cat appear out of nowhere. You cannot see the tricks or equipment and the cat will never explain how to do it.\nBig-Trick-mew is respected by a lot of disciples.", ""));
			Rows.Add( new MNP_LocalizeRow("L2020", "2020", "소악마 의상을 입은 고양이.\n뿔과 날개는 테이프로 붙여 놓은 것 같다.\n도화지와 가위만 있으면 언제든지 대신할 \n것을 만들 수 있다.", "Devil-nya is a cat that walks around in a devil costume with horns and wings taped to it. Devil-nya can easily be duplicated with paper and scissors.", ""));
			Rows.Add( new MNP_LocalizeRow("L2021", "2021", "소악마에 몰입되어 있는 고양이.\n마음에 드는 포크를 몸에서 떼지 않고 \n늘 지니고 있다. \n포크는 먹을 것으로 만들어진 것 같다.", "A cat who is into playing little devil. Little-Fiend-nya always carries the favorite fork that seems to be made of some food.", ""));
			Rows.Add( new MNP_LocalizeRow("L2022", "2022", "악마 의상을 입은 고양이.\n날개가 남았기 때문에 많이 붙여 놓은 것 같다.\n실은 유령이나 무서운 것에 약하다.", "The cat wearing a devil costume. It seems Gaagoi-nya glued more than one pair of wings only because there were some leftovers.  \nIn fact, Gaagoi-nya is scared of ghosts and anything frightening.", ""));
			Rows.Add( new MNP_LocalizeRow("L2023", "2023", "마왕에 몰입되어 있는 고양이.\n세계를 지배하고 있는 기분에 빠지는 것이 취미.\n미간의 주름은 펜으로 그려놓은 것이다.", "Archfiend-nya believes it is the Archfiend. The cat enjoys acting like ruling the world. The wrinkles between eyes were drawn with a pen.", ""));
			Rows.Add( new MNP_LocalizeRow("L2024", "2024", "수수께끼에 싸인 정체불명의 고양이.\n지퍼가 달려있는 탈인형을 입고 있다.\n그 안에는 뭔가 고양이와는 다른 기운이 느껴진다.", "Fasten-mew is a mysterious cat in a costume with a zipper. There seems to be something not like a cat inside.", ""));
			Rows.Add( new MNP_LocalizeRow("L2025", "2025", "신비한 빛이 새어나오고 있는 고양이.\n안에서 나오고 있는 빛은 강렬하여\n숨바꼭질을 하면 바로 발견되어 버릴 것 같다.", "Star-mew has some mysterious light. The light is too strong to play hide-and-seek.", ""));
			Rows.Add( new MNP_LocalizeRow("L2026", "2026", "흡인력이 발군인 수수께끼의 고양이.\n지퍼 안에는\n무한의 공간이 펼쳐져 있어\n빨아들이면 다시 꺼낼 수가 없다.", "This mysterious cat has strong absorptive power.\nIn the zipper, there is an infinity space. Nobody can pull things off again once absorbed.", ""));
			Rows.Add( new MNP_LocalizeRow("L2027", "2027", "고양이 탈인형에서 나온 고양이.\n탈인형 안에는 쾌적한 공간이 펼쳐져 있는 것 같다.\n나올 때 자주 다리가 걸려버리고 만다.", "Main-Body-mew is a cat come out of the cat suit. It seems there's a cozy place in it. When the cat try to come out, it often stumbles.", ""));
			Rows.Add( new MNP_LocalizeRow("L2028", "2028", "악당을 물리칠 사명에 불타오르는 고양이.\n컬러풀한 동료가 상당히 많이 있는 듯 하다.\n텐션이 매우 높고 답답할 정도로 정의롭다.", "Red-nyan's mission as a cat is to eliminate all the baddies in the world! Red-nyan has many colorful friends. Red-nyan is pretty highly-strung, and to be truthful, can be suffocating to be around at times.", ""));
			Rows.Add( new MNP_LocalizeRow("L2029", "2029", "멋진 꽃미남 히어로인 척 하는 고양이.\n악당을 쓰러트리는 것에는 그다지 흥미가 없다.\n인기를 끌기 위해서 히어로를 하고 있는 것 같다", "Handsome-Bule-nyan acts like a hot superhero.\nNot interested in nocking down bad fellows but rather want to be popular.", ""));
			Rows.Add( new MNP_LocalizeRow("L2030", "2030", "의지가 되는 천재 발명가 고양이.\n동료에게 인정받고 있는 흔들림이 없는 스타일의 고양이.\n번뜩이는 힘은 있으나 신체 능력은 그저 그렇다.", "You can trust this genius Inventor-Green-nyan. The cat is respected by companions and has steady spirit.\nThe inventor got some brilliant powers but It physical capabilities aren't that good. ", ""));
			Rows.Add( new MNP_LocalizeRow("L2031", "2031", "먹는 것에 대한 사명감이 강한 고양이.\n최근 망토가 꽉 끼게 되어서\n친구들에게는 말하지 않고 벗어버릴까 고민하고 있는 것 같다.", "Hungry-Yellow-nyan's mission is all about eating.\nAs the cape is getting tighter recently, so the cat is thinking about taking it off without telling friends.", ""));
			Rows.Add( new MNP_LocalizeRow("L2032", "2032", "쪼글쪼글 구워진 파마를 한 고양이.\n어떻게 하다가 그렇게 되었는지 물어봤는데 \n아무말없이 저녁해를 바라보고 있었다.", "Bomba-nya a head has frazzled, burnt-like hair. Ask what caused the hair to be like this, and Bomb a head will turn away and simply not answer you.", ""));
			Rows.Add( new MNP_LocalizeRow("L2033", "2033", "머리에 불이 붙어버린 고양이. \n항상 불이 붙어있어서 밝기 때문에\n자주 약속장소에서 사용되는 것 같다.", "Disaster-nya is on fire accidentally. And it made the cat always shining. As it is very prominent, people use it as a meeting point.", ""));
			Rows.Add( new MNP_LocalizeRow("L2034", "2034", "얼굴이 타버린 고양이.\n수염과 머리도 난리가 났는데\n얼굴이 타버린 것과는 관계가 없는 것 같다.", "Burnt-nya is a cat with the face burned. The cat's mustache and hair are all messed up but maybe it has nothing to do with the burning on its face.", ""));
			Rows.Add( new MNP_LocalizeRow("L2035", "2035", "뽀글뽀글한 머리카락이 비대해진 고양이.\n본인의 의사와는 관계없이 커졌기 때문에\n해외에서 살기로 결정했다.", "Afro x 1.5-nya is a cat with larger frizzled hair. As its hair is getting bigger and bigger against its will, the cat decided to live abroad.", ""));
			Rows.Add( new MNP_LocalizeRow("L2036", "2036", "뭔 이유에서인지는 모르겠지만 머리를 상자에 \n밀어넣고 있는 고양이. 발견했을 때는 \n이미 이런 상태였다. 이미 이 이상은 어떻게 \n할 수가 없는 것 같다. ", "Ugly-mew goes around with its head stuck in a box.\nIn fact when it was found, it was already like this, and there is no use trying to fix it.", ""));
			Rows.Add( new MNP_LocalizeRow("L2037", "2037", "자신의 장난감 상자에 머리가 낀 고양이.\n장난감을 가지고 놀고 싶었을 뿐인데라고 \n후회하고 있는가 관찰해보니 어떤 장난감을 \n가지고 노는 것 보다 재미있어 보인다.", "Toy-Box-mew is a cat accidentally stuck with its toy box. You might feel sorry for the cat, thinking \"Aw, he just wanted to play with a toy.\" But observe. It seems to be enjoying the situation and doesn't need any other toys.", ""));
			Rows.Add( new MNP_LocalizeRow("L2038", "2038", "자주 상자에 머리가 끼는 고양이.\n버림받은 고양이 놀이를 하고 있던 결과이다.\n그런데 상자가 마음에 들었을 뿐으로 실은 \n쉽게 빠져나올 수 있다.", "Orange-Carton-mew often stuck with a box. This is the end for playing \"abandoned cat\". In fact, the cat just likes playing with the box and can get out easily anytime.", ""));
			Rows.Add( new MNP_LocalizeRow("L2039", "2039", "아무리 발버둥쳐도 끼어서 나올 수 없는 고양이.\n이 이상 어찌할 도리가 없기 때문에\n상자와 함께 살아가는 것을 결정한 것 같다.", "A cat who got stuck and couldn't get out. Loaded-mew decided live with the box because this cat cannot do anything else.", ""));
			Rows.Add( new MNP_LocalizeRow("L2040", "2040", "던전에 가고 싶어하는 고양이.\n검과 방패와 같은 굿즈를 가지고 휘두르고 있다.\n가끔 관에 들어가 있다.", "Kenshi-nya likes exploring dungeons, swinging items that look like a sword and shield. For some unexplained reason, Kenshi-nya can also be found resting in coffins.", ""));
			Rows.Add( new MNP_LocalizeRow("L2041", "2041", "최강을 꿈꾸는 노력파 고양이.\n스스로 만든 대나무 칼과 골판지 방패를 장비하고 있다.\n아직 누구와도 싸워본 적이 없다.", "Bamboo-Sword-nya is hard-working cat trying to be the mightiest. The cat is equipped with a hand-made a bamboo sword and cardboard shield.\nBamboo-Sword-nya hasn't had a fight with anybody yet.", ""));
			Rows.Add( new MNP_LocalizeRow("L2042", "2042", "눈부시게 빛나는 검을 든 고양이.\n그 검은 어떠한 물건도 두동강 낼 수 있다.\n방패로는 사물을 움직이는 \"기\"를 날린다.", "Light Saber-nya is holding a saber shining brightly. It could cut anything with that sword. The cat move things with \"qi\" through its shield.", ""));
			Rows.Add( new MNP_LocalizeRow("L2043", "2043", "사탕의 나라에서 온 전사 고양이.\n사탕을 사용한 공격이 특기로 장비는 사탕뿐이다.\n상대는 반드시 끈적끈적하게 된다.", "Lollipop-nya is a warrior cat who came from Candyland. Being armed only with candies, very good at attacking with candies. The opponent will get sticky when attacked by the cat.", ""));
			Rows.Add( new MNP_LocalizeRow("L2044", "2044", "병에 들어가 있는 수수께끼 투성이 고양이.  \n의미를 모르겠다. 포동포동해서 맛있어 보인다. \n보고 있으면 침이 나온다. \n병에 꽉 끼어 있는 것이 매우 행복해 보인다.", "Milk-nya is in a milk bottle and nobody knows why. Milk-nya looks plumpy-tasty, it is tempting to eat. It looks very content as perfectly fit in the bottle.", ""));
			Rows.Add( new MNP_LocalizeRow("L2045", "2045", "우유가 듬뿍 들어간 커피 우유 고양이.\n실은 장난꾸러기 갈색 고양이가 병에 \n들어가 있는 것뿐이다. 병의 라벨은 \n직접 낙서한 것으로 보인다.", "A cat of a coffee flavored milk cat with a plenty of milk. In fact, its just a naughty cat in a bottle. The label on the bottle was just its doodles.", ""));
			Rows.Add( new MNP_LocalizeRow("L2046", "2046", "엄청 달고 맛있는 딸기 우유 고양이.\n빈병처럼 보이려고 머리까지 꼭꼭 들어가 있는 것에 \n빠져있다. 병을 버리기 전에 고양이가 들어가 있는지\n확인이 필요하다.", "Strawberry-Flavored-nya is a sweet tasty strawberry flavored milk cat.\nIt enjoys hiding in a bottle to pretend it's empty. You should make sure the cat isn't in it before throw out the bottle.", ""));
			Rows.Add( new MNP_LocalizeRow("L2047", "2047", "과일의 단맛이 나는  후르츠우유냥. \n매우 좋은 냄새가 난다. 병에서 나오려고 자\n주 바둥바둥거리는데 역시 병 안이 마음이 \n편한 것 같다.", "A cat of  sweet fruit flavored milk with a very nice smell. Fruit-Flavored-nya sometimes shakes and tries to come out of the bottle, but ending up in the bottle as it is much comfortable in there.", ""));
			Rows.Add( new MNP_LocalizeRow("L2048", "2048", "뭐가 불만인지 항상 불만가득한 얼굴을 하고 있다.\n항상 이 얼굴이기 때문에 기분이 좋은 상태라도 알 길이 없다.", "Grumpy-nya is a lazy cat that sits around all the time. Furthermore it always has a glum look, and no-one knows why!", ""));
			Rows.Add( new MNP_LocalizeRow("L2049", "2049", "낯가림으로 항상 불안해하는 고양이.\n긴장하면 왠지 미간에 주름이 생긴다.\n화난 것인가 하고 자주 오해를 받는 것 같다.", "Anxious-nya is a shy cat. When the cat got nervous, there will be wrinkles between her eyebrows. The cat's shyness is often get mistaken for anger.", ""));
			Rows.Add( new MNP_LocalizeRow("L2050", "2050", "장난칠 것을 생각하면서 히죽히죽 웃는 고양이\n\"반드시 저녀석의 참치를 뺏어버릴거다냥!\"과 같이\n생각은 하는데 막상 실행에 옮길 용기는 없다.", "This cat is smirking by imagining some mischievous attempts. Just by thinking  \"I will take that guy's tuna!\" it does not dare to move in to action.", ""));
			Rows.Add( new MNP_LocalizeRow("L2051", "2051", "나쁜 일을 꾸미고 있는 듯이 보이는 고양이.\n그렇게 보이기는 하지만 실제로는 아무것도 \n생각하고 있지 않다. 하품을 하면 이 얼굴이 \n되어 버리는 것 같다.", "Boor-nya looks like scheming something bad. But it has nothing in its mind. It's just a yawning cat.", ""));
			Rows.Add( new MNP_LocalizeRow("L2052", "2052", "실험에 열중한 나머지 다른 일에는 손을 대지 않는 고양이.\n무슨 말인지 이해가 되지 않는 서양말만 써서 이야기 한다.\n언제나 뭔가를 중얼거리고 있다.", "Mewton-nya is absent-minded cat that only thinks about experiments. It speaks using a lot of hard-to-understand scientific jargons. Mewton-nya is always mumbling something.", ""));
			Rows.Add( new MNP_LocalizeRow("L2053", "2053", "어려운 것도 정성껏 설명해 주는 고양이.\n아무도 듣고 있지 않아도 기죽지 않는\n강한 정신력을 가지고 있다.\n개성이 강하다.", "Professor-nya explains nicely whatever difficult questions you ask. The cat does not care even if nobody listens.\nQuite a character with mental strength.", ""));
			Rows.Add( new MNP_LocalizeRow("L2054", "2054", "다방면에 정통한 박식한 고양이.\n언제나 본인이 좋아하는 \"지적인 모자\"를 쓰고 있다.\n가끔 모자 장식을 쫓아 다니기도 한다.", "A cat very familiar with various knowledge. By wearing its favorite \"intellectual hat,\" it sometimes cannot help chasing the tassel of the hat.", ""));
			Rows.Add( new MNP_LocalizeRow("L2055", "2055", "독특한 가치관을 가진 천재 고양이.\n주변에 폐를 끼치는 것에 대해서 꺼림칙 없이\n연구에 몰두한다.\n현재는 고양이형 잠수함을 개발하고 있다.", "Mad-nya is a genius cat with unique values. The cat is totally absorbed in studies without thinking bothering someone around. Mad-nya is currently developing a cat-type submarine.", ""));
			Rows.Add( new MNP_LocalizeRow("L2056", "2056", "실제로는 다른 고양이들과 뭉치는 것을 좋아하지\n않는다. 사건에 자주 휘말리는 비극의 히로인 \n타입. 세상에서 자기가 제일 귀엽다고 생각하고 \n있는 것 같다. ", "Hana-chan-mew is not really fond of hanging around with other felines. Being a bit of a drama queen, it always gets mixed up with problem situations. Hana-chan-mew thinks itself the prettiest cat in the world.", ""));
			Rows.Add( new MNP_LocalizeRow("L2057", "2057", "노란색 민들레를 가지고 있는 고양이.\n뭉쳐있기 보다는 양지에서 햇볕을 쬐는 것을 \n좋아하는 것 같다. 틈만 나면 솜털을 날려 \n사랑점을 보고 있다.", "Dandelion-nyan is holding a yellow dandelion. It prefers to bask in the sun rather than bunch up with other cats.\nThe cat's doing love fortune telling with the flower whenever it can.", ""));
			Rows.Add( new MNP_LocalizeRow("L2058", "2058", "빨간 튤립을 가지고 있는 고양이.\n색은 기분에 따라 바뀌는 것 같으며 빨강은 \n기분이  좋다는 의미이다. 여자로써의 \n능력에 대단한 자신이 있는 것 같다.", "Tulip-nyan is holding a red tulip. The tulip color changes according to its mood. Red means \"feeling good.\" Tulip-nyan has great confidence in its girl's power.", ""));
			Rows.Add( new MNP_LocalizeRow("L2059", "2059", "보라색 팬지꽃를 가지고 있는 고양이.\n다른 고양이들도 모두 꽃을 들고 있기를 바란다.\n보고 있으면 행복한 기분이 드는 치유계 고양이.", "Pansy-nyan is holding a purple pansy. The cat wants other cats to hold the flower too. The cat is a comfort making everyone happy just by seeing it.", ""));
			Rows.Add( new MNP_LocalizeRow("L2060", "2060", "구수한 어른의 향기가 나는 고양이.\n배에 두른 천에 그려진 자수는 직접 만든 것 같다.\n이름을 한자로 쓰면 싫어한다.", "Coffee-nya is a cool cat with a \"roasting savor\" who wears a hand-knitted belly warmer. Coffee-nya takes offence when it sees its name shown without Chinese characters.", ""));
			Rows.Add( new MNP_LocalizeRow("L2061", "2061", "옅게 볶은 연한 고양이.\n쓴맛보다도 산미가 강하고 산뜻한 것이 특징.\n설탕이나 우유와는 사이가 좋지 못하다.", "A mild cat with light roasted beans. It has more acidity than bitterness. It doesn't get along with sugar or milk.", ""));
			Rows.Add( new MNP_LocalizeRow("L2062", "2062", "진하고 깊은 맛이 특징인 고양이.\n의외로 설탕과 상성이 좋으나\n별로 알려져 있지 않다.\n에스프레소라는 이름의 어원은 \"당신에게만\".", "A rich cat with dark roasted beans. It goes well with sugar, but only few knows. \"Espresso\" means \"Just for you\".", ""));
			Rows.Add( new MNP_LocalizeRow("L2063", "2063", "마일드함 속에 살포시 숨어 있는 쓴맛을 \n가지고 있는 고양이. 화려한 그림을 그리는 \n것이 특기이다. 카페오레와 혼동하면 삐진다.", "Latte-nya is a mild cat yet with a bit bitter taste. The cat is good at fancy drawing.\nLatte-nya gets upset if is  confused with café au lait.", ""));
			Rows.Add( new MNP_LocalizeRow("L2064", "2064", "밥을 엄청 좋아하는 영양 만점 고양이.\n형용할 수 없을 정도로 행복한 모습으로 \n밥을 먹는다. 아무도 뭐라 하지 않았는데 \n\"내일부터\"라고 하는 것이 입버릇.", "Chubby-mew is a nutritious cat that loves foods. It looks heavenly when eating. Without even starting a conversation, Chubby-mew always says, “I’ll begin tomorrow.\"", ""));
			Rows.Add( new MNP_LocalizeRow("L2065", "2065", "드디어 옷을 입지 않게된 고양이.\n다이어트를 위해서 에어로빅을 시작했다.\n그런데 간식이 더욱 늘어난 것 같다.", "A cat that finally grew out of any clothes.\nAerobics-mew started taking an aerobic class to lose some weight. But apparently, Aerobics-mew is eating more often than before.", ""));
			Rows.Add( new MNP_LocalizeRow("L2066", "2066", "울퉁불퉁한 근육을 동경하는 고양이.\n최고지속기간은 4일간.\n우선은 바닥에 놓인 아령을 들어올리는 것이 목표.", "Muscle-Training-mew training yearns for a masculine body. But the training lasted only for four days and it is the longest. Right now the goal is lifting a dumbbell from the floor.", ""));
			Rows.Add( new MNP_LocalizeRow("L2067", "2067", "더 이상 살빼기를 포기한 고양이.\n기를 연마하여 체형을 유지하고 있으며\n항상 무언가를 먹고 있다.", "Immortal-mew gave up trying to lose weight. \nUsing \"qi\", it keeps the body shape . \nThe cat is always eating something.", ""));
			Rows.Add( new MNP_LocalizeRow("L2068", "2068", "다이어트에 성공한 고양이.\n좀 오버한 것이 아닌가 하고 생각하고 \n있는 것 같다.요요현상을 신경쓰고 있지만\n당분간은 걱정 없다고 생각한다.", "Skinny-mew successfully has lot a lot of weight, in fact it probably lost too much weight. Although it worries about putting some weight back but it will not happen for some time.", ""));
			Rows.Add( new MNP_LocalizeRow("L2069", "2069", "과도한 다이어트로 인해 홀쭉해져버린 고양이.\n쓸데없는 근육이 사라졌기 때문에 뼈의 느낌이 \n대단하다.본인의 이야기로는 통뼈라고 한다.", "A cat that became skin and borne after being on a strict diet. Bone-nya lost all muscles so you can see the bones. But Bone claims they are rigid bones.", ""));
			Rows.Add( new MNP_LocalizeRow("L2070", "2070", "2D와 같은 얇음을 몸으로 보여주는 고양이.\n꾸준한 노력으로 경이적인 경량화에 성공하였다.\n강한 바람에 약하다.", "A cat of 2D skinny.\nSkin put effort endlessly to lose weight and the cat made it. Skin-nya is vulnerable to strong wind.", ""));
			Rows.Add( new MNP_LocalizeRow("L2071", "2071", "가늘고 회색의 피부를 한 수수께끼의 고양이.\n고양이와 같이 귀는 있지만 \n과연 진짜 고양이가 맞는지…\n소문으로는 \"하늘\"에서 왔다고 한다.", "A mysterious cat having grey skin and thin body. Grey-nya have cat's ears but..Is it a cat really? Some people said it's from \"sky\".", ""));
			Rows.Add( new MNP_LocalizeRow("L2072", "2072", "항상 자신을 낮추고 접대를 하려고 하는 고양이.\n분위기 띄우는 일과 \"일단 맥주 주세요\"라는 멘트는 항상 그의 몫.\n연말 회식 준비는 항상 열심히한다.", "Corporate-Warrior-mew is a very humble \"business\" cat that likes to entertain clients. Corporate-Warrior-mew basically likes to say yes to anyone and to start an evening's drink session with a beer. It always works hard preparing stunts for year-end parties.", ""));
			Rows.Add( new MNP_LocalizeRow("L2073", "2073", "입사한지 얼마 되지 않은 신입 고양이.\n회식 중에는 상사의 잔을 항상 체크하고 있다.\n손에 들고 있는 맥주는 무알콜이다.", "Freshman-mew is a newbie who just got a job.\nThe cat is always checking boss's glass out during the party at work. The beer holding is a non-alcoholic.", ""));
			Rows.Add( new MNP_LocalizeRow("L2074", "2074", "조금 여유가 생긴 선배 고양이.\n상사 앞에서 딸랑거리는 것은 기본.\n자신보다 아래라고 생각되는 상대에게는 강한 태도를 보인다.", "Junior-mew is now taking things in stride at work.  Always being brown-nosing bosses, the cat looks down on those that it thinks less important.", ""));
			Rows.Add( new MNP_LocalizeRow("L2075", "2075", "자신이 어찌되든 회식 분위기를 띄우는 데에\n전력을 다하는 고양이.\n머리에는 넥타이 양손에는 맥주.\n이것이야말로  회식부장의 기본스타일.", "Party-Animal-mew does best to lighten up the mood whatever it takes. Wearing a tie around its head and holding beers in both hands, that is its signature style.", ""));
			Rows.Add( new MNP_LocalizeRow("L2076", "2076", "무언가를 조사하거나 뒤쫓고 있는 고양이.\n언제나 다른 누군가를 미행하고 있다. \n발자국이나 지문에 대단히 흥미를 가지고 있는 것 같다.", "Lost-Detective-nyan likes to be a private detective, always investigating and following suspects. Cats are curious, and so is Lost Detective-nyan whose curiosity focuses on footprints and fingerprints.", ""));
			Rows.Add( new MNP_LocalizeRow("L2077", "2077", "진실을 밝혀낼 탐정 고양이.\n어떤 의뢰라도 반드시 비밀을 들추어 낸다.\n자주 미아가 되는 것은 애교.", "Lost Investigation-nyan is a detective seeking for truths.\nLost Investigation-nyan always finds out the secrets whatever the request is.\nThe cat easily gets lost, which is a part of its charm.", ""));
			Rows.Add( new MNP_LocalizeRow("L2078", "2078", "모든 일에 추측을 하는 것을 좋아하는 고양이.\n자신도 모르게 혼자말을 많이 하게 되었다.\n자신이 없기 때문에\n\"아마도~\", \"~일지도\"등이 말버릇.", "This cat likes to speculate about various events. It often thinks out loud without noticing.\nDue its lack of self-confidence, Lost-Reasoning-nyan keeps saying \"maybe\", \"perhaps.\" quite often.", ""));
			Rows.Add( new MNP_LocalizeRow("L2079", "2079", "뜻밖의 것을 발견해 버리는 고양이.\n정원에서 공룡 화석을 발굴하거나\n모자 안에 병아리가 있는 것을 알아차리거나.", "A cat that find unexpected things.\nIt finding includes a dinosaur fossil in the garden and a chick in the hat.", ""));
			Rows.Add( new MNP_LocalizeRow("L2080", "2080", "언제나 싸울 준비가 되어 있는 청춘의 절정에 있는 고양이.\n오토바이 소리를 흉내내는 것을 잘한다.\n집회가 있다고 하면서 밤에는 집에 잘 없지만\n아침에는 반드시 돌아온다.", "Delinquent-mew is a strong fighting cat the peak of the youthful strength. Very good at mimicking motorcycle sounds.\nStays out most nights but always returns home in the morning.", ""));
			Rows.Add( new MNP_LocalizeRow("L2081", "2081", "한자의 매력을 알아가기 시작한 고양이. \n등에 한자로 \"잘 부탁해\"라고 쓰여있는 것은 뒷사람에게 인사하기 위해서이다.", "A cat getting to know the fun of learning kanji.\n \"Yoroshiku\" on its back is for greeting people behind.", ""));
			Rows.Add( new MNP_LocalizeRow("L2082", "2082", "\"대승\"이라는 슬로건을 짊어지고 있는 고양이. \n공부나 스포츠를 어느 누구에게도 지지 않으려고 한다. \n마스크를 끼고 있는 것은 감기에 걸리지 않기 위해서.", "A Cat whose slogan is \"Butchigiri\" or \"second to none by far\".\nA cat who takes on \"clean sweep\".\nIt want to be the best in any field: sport or study. Wearing a mask is in order not to catch a cold.", ""));
			Rows.Add( new MNP_LocalizeRow("L2083", "2083", "누군가가 싸움을 걸어오면 언제라도 상대하는 고양이. 옷에 팔을 넣지 않고 어깨에 걸치는 스타일을 좋아한다. 리젠트 스타일의 헤어는 떼어내어 씻는다.", "A cat that never refuses a fight picked by someone else.  Likes the style to wear a jacket without putting arms through.\nThe pompadour on Regent Style hair is detachable to wash.", ""));
			Rows.Add( new MNP_LocalizeRow("L2084", "2084", "\"수상한 놈이다\"라고 하면서 바로 쫓아나가는 고양이.\n모두들 자기가 한 지역의 영주라고 생각하고 있는 것 같은데\n그런 영주가 많이 있으면 큰일일 것이다.", "Samurai-Spirit-nyan likes chasing those are suspicious. Although every Samurai-Spirit-nyan regards itself as the head of their feudal domains, but if there are so many leaders, it will only lead to troubles.", ""));
			Rows.Add( new MNP_LocalizeRow("L2085", "2085", "바로 \"그만둬\"라고 하면서 도망치는 고양이.\n싸움에 져서 도망의 나날을 보내고 있으며\n일본 상투 모양의 가발을 살까 고민하는 중.", "An escaping cat that says 'cut it out'.\nOn evasional days due to losing fights, it is thinking to buy a topknot wig or not.", ""));
			Rows.Add( new MNP_LocalizeRow("L2086", "2086", "옷 안쪽에 자수로 되어 있는 벚꽃이 눈보라 처럼 지는 모양을 자주 보여주는 고양이.\n악당을 잡는 일을 하고 있다. \n칼은 가지고 있으나 \n기본적으로 맨손으로 상대를 기절시킨다.", "Magistrate-nyan frequently shows off fallen-cherry-blossom pedals embroideries of the lining. Its job is to arrest baddies. Although it has a knife, basically it knocks enemies out with bare hands.", ""));
			Rows.Add( new MNP_LocalizeRow("L2087", "2087", "얼굴에 흰색으로 화장을 한 고양이.\n말 끝에 항상 \"오쟈루\"가 붙는다. \n챠밍 포인트는 위로 올라가 있는 눈썹.", "A cat with white make up on.\nAlways close words with 'Ojjaru'.\nThe cat is proud of the dainty eyebrows.", ""));
			Rows.Add( new MNP_LocalizeRow("L2088", "2088", "검은 의복을 몸에 두른 특명 고양이.\n장기인 인술을 보여주려고 자주 숨는데\n마무리가 허술해서 바로 들킨다.", "Ninja-nya is a cat on a special mission in a black costume.\nIt often tries to hide through ninja art but easily spotted due to the lack of diligence.", ""));
			Rows.Add( new MNP_LocalizeRow("L2089", "2089", "보라색 의복을 몸에 두른 특명 고양이.\n물속에서 은신하는 인술이 장기인데 \n왠지 그것밖에 못하는 것 같다.", "Lower-Man-nya is a cat on a special mission in a purple costume. Good at hiding under the water which is only trick available.", ""));
			Rows.Add( new MNP_LocalizeRow("L2090", "2090", "오렌지색 의복을 몸에 두른 특명 고양이.\n모든 인술을 습득하였으나\n가끔 수리검을 까먹고 집에 두고 온다.", "Upper-Man-nya is a cat on a special mission in an orange costume.\nAlthough it is through various training, it sometime forget to bring 'shrikes' or Japanese concealed weapon.", ""));
			Rows.Add( new MNP_LocalizeRow("L2091", "2091", "고운 의복을 몸에 두른 특명 고양이.\n혹독한 단련 끝에 인술의 끝에 이르렀다.\n마지막으로 다다른 곳은 성적 매력의 인술인 것 같다.", "An Seductive-Female-nya is a cat on a special mission in a beautiful costume.\nAfter a harsh training, the ultimate trick it reached at is the art of seducing.", ""));
			Rows.Add( new MNP_LocalizeRow("L2092", "2092", "덥수럭하게 자라 있는 갈기를 가진 고양이과의 생명체.\n항상 뭔가를 와작와적 먹고 있다. 먹보녀석.\n다른 고양이와 함께 있으면 어째서인지 고양이의 숫자가 줄어든다.", "King-of-Cats-mew is a crested-feline that is always eating. In fact it is a true glutton. When it is with other cats, for some unexplained reason, their numbers dwindle.", ""));
			Rows.Add( new MNP_LocalizeRow("L2093", "2093", "무방비 상태로 코풍선을 불고 있는 고양이.\n제왕의 여유인지 장소를 가리지 않고 잠이 든다.\n자고 일어났을 때의 기분이 개운치 않다고 하는데\n그렇게 자고 있는 모습에서는 상상이 되지 않는다", "A cat is sleeping tight with a defenselessly.\nIt falls asleep wherever perhaps because it is a off-the-guard king.\nIt does not wakes up easily despite of the sound sleep while it is asleep. ", ""));
			Rows.Add( new MNP_LocalizeRow("L2094", "2094", "뭔가를 입에 우물거리고 있는 고양이.\n야채만 먹어왔던 야생의 제왕이 \n진정한 \"맛\"을 알게 되고 말았다.", "A cat mumbling something.\nThis ex-vegetarian king of cat has discovered 'real taste' finally.", ""));
			Rows.Add( new MNP_LocalizeRow("L2095", "2095", "아무도 보고 있지 않다고 생각해서 방심해 버린 고양이.\n야생의 제왕의 정체는\n실은 먹보 고양이였다.\n시급이 높기 때문에 인기가 높은 아르바이트인 것 같다.", "Wild-Inside-mew became off the guard thinking nobody is watching. Actually the identity of the king of the wild is big eater. Because hourly wage is high, this part time job is popular.", ""));
			Rows.Add( new MNP_LocalizeRow("L2096", "2096", "앞에 서서 행진을 지휘하는 음악대의 리더.\n음악대가 가는 곳은 모두 그에게 맡겨져 있다.\n그렇지만 실은 방향치인 것 같다.", "A Leader-nyan that heads the marching band. The band counts on Leader for the direction the band, however the Leader-nyan is actually bad at direction.", ""));
			Rows.Add( new MNP_LocalizeRow("L2097", "2097", "경쾌한 스틱 기술을 보여주는 고양이.\n리듬감에 상당히 자신이 있는 것 같다.\n근처에 있는 물건은 무엇이든 두드리기 때문에 상당히 시끄럽다.", "A cat that loves to show off the skillful drumming techniques. It is very proud of its rhythmic sense and making a big noise by beating various things around it.", ""));
			Rows.Add( new MNP_LocalizeRow("L2098", "2098", "마이페이스로 우쿨렐레를 연주하는 고양이.\n악기를 자주 헷갈려서 다양한 것을 가지고 온다.\n모양이 비슷하면 뭐든 괜찮은 것일까?", "A slow-tempo cat that thrums a ukulele at its own pace. It carries different things around by confusing with a ukulele. It seems it does not care whatever it is as long as it looks similar to a ukulele.", ""));
			Rows.Add( new MNP_LocalizeRow("L2099", "2099", "어렸을 때를 생각나게 하는 음색을 연주하는 고양이.\n다른 고양이와 비교해도 상당한 연습량을 엿볼 수 있다.\n낮은음 \"도\"의 소리는 둔탁하지 않고 부드러운 \"도\" 소리가 난다.", "A cat that plays nostalgic tunes which remind everyone of their good old days. \nWe can tell how hard it practices unlike the way other cats practice. For the cat, the low C tone should be pronounced as \"toe\" softly, instead of \"do.\"", ""));
			Rows.Add( new MNP_LocalizeRow("L2100", "2100", "실로폰을 목에 걸고 즐겁게 연주하는 고양이.\n고속스핀주법이라는 울트라C를 체득했다.\n목에 뭔가 걸고 하는 아르바이트가 특기이다.", "Hanging a xylophone from its neck, this cat plays it merrily. It came up with an original playing technique of high-speed spinning. It excel at a job with hanging something from the neck and always has such a kind of part-time job.", ""));
			Rows.Add( new MNP_LocalizeRow("L2101", "2101", "겉모습도 움직임도 활발하고 경쾌한 고양이.\n언제 어디서나 리듬을 타면서 춤추고 있다.\n행진때 발을 들어올리는 각도에 매우 신경쓴다.", "A guy and flashy cat with good spirits. It grooves and dances anytime anywhere. Very particular about the angle of legs during the march.", ""));
			Rows.Add( new MNP_LocalizeRow("L2102", "2102", "정확한 타이밍으로 치유의 고음을 내보내는 고양이.\n싫증이 난 것인지 연주를 땡떙이 치는 일이 많다.\n삼각형 모양의 물건을 좋아한다.", "A cat makes healing high-tone sounds at a perfect timing.\nBeing a lack of endurance, it stops playing from time to time during the performance. It loves anything in a triangular shape.", ""));
			Rows.Add( new MNP_LocalizeRow("L2103", "2103", "커다란 아코디언을 안고 있는 고양이.\n팔이 짧기 때문에 연주는 힘들 것 같다…\n어울리지 않는 것은 아닌지?\n지금 가장 원하는 것은 긴 팔과 5개의 손가락.", "A cat carrying a large accordion.\nWith shorter arms, it looks really hard to play. Maybe it is not meant to play a large accordion in the first place? \nThe thins it wants most are longer arms with five fingers.", ""));
			Rows.Add( new MNP_LocalizeRow("L2104", "2104", "큰 나팔을 부는 숨은 공로자 고양이.\n폐활량이라면 누구에게도 지지 않을 자신이 있다.\n낮음 음도 잘 들어줬으면 하는 마음에 열심히 하고 있는 것 같다.", "A cat of the modest worth that blows a large Tuba. This cat is second to none in the lung capacity. It works very hard for everyone to listen to low-pitched notes well too.", ""));
			Rows.Add( new MNP_LocalizeRow("L2105", "2105", "곡의 절정을 고조시켜주는 고양이.\n동료들 사이에서는 알람시계 대용으로 대활약하고 있는 것 같다. \n누구보다도 큰 소리를 내는 것이 삶의 보람인 것 같다.", "A cat that boosts the climax of songs very much. Other cats rely it on as an alarm clock. It is all about making louder sounds than anybody else.", ""));
			Rows.Add( new MNP_LocalizeRow("L2106", "2106", "모든 중요한 사항을 다루는 큰 덩치의 신뢰할 수 있는 고양이.\n커다란 진동을 몰고오는 연주는 행진이 중단될 정도.\n식욕이 왕성하기 때문에 식욕을 돋우는 디자인의 악기를 들고 있을 때도 있다.", "A large and reliable cat that covers all the important points. As the sound it makes is sometime so large and shaky that it stops the marching band. Having a big appetite, it carries an instrument with appetizing designs.", ""));
			Rows.Add( new MNP_LocalizeRow("L2107", "2107", "마음이 정화되는 아름대운 음색으로 주위를 온화하게 만드는 고양이.\n항상 중요한 타이밍에 등장한다. 혹시 좋은 것을 옆에서 가로채는 타입?", "A cat that clears that atmosphere by playing moving tunes. It always shows up when it comes to the crunch.  It is a cat that always takes the lion's share.", ""));
			Rows.Add( new MNP_LocalizeRow("L2108", "2108", "꿀벌을 닮은 하늘을 날아다니는 밋치리네코.\n8자 댄스에 싫증나면 문워크를 춘다.\n그런 모습을 하고 있으면서 \n실제로는 술꾼인 것 같다.", "HoneyBee-nya is a bee-like flying Mitchiri Neko. Honey switched to dancing the Moonwalk after tiring of doing a bumblebee's waggle dance. While HoneyBee-nya might look cute and sweet, it doesn't like sweet things.", ""));
			Rows.Add( new MNP_LocalizeRow("L2109", "2109", "메이플 시럽을 대단히 좋아하는 하늘을 날아다니는 밋치리네코.\n시럽을 너무 많이 먹어서 몸 색깔이 바뀌었다.\n하늘을 나는 것이 대단히 힘들 정도로 체중이 늘어난 것 같다.", "A cat that loves maple syrup and flies in the sky.\nThe color of body has been changed because of eating too much syrup. MapleBee-nya gets so much weight that flying is hard to it.", ""));
			Rows.Add( new MNP_LocalizeRow("L2110", "2110", "우유병을 가지고 하늘을 날아다니는 밋치리네코.\n내용물이 들어간 병은 상당히 무거운 것 같다.\n병 안에 고양이가 들어있던 적도 있다는 소리도 있는데…", "A cat flies in the air with milk bottle.\nThe bottle containing something looks so heavy.\nThe bottle probably contained a cat once.. ", ""));
			Rows.Add( new MNP_LocalizeRow("L2111", "2111", "찡그린 얼굴로 하늘을 날아다니는 밋치리네코.\n단 것을 좋아하는 고양이임에도 불구하고 \n달지 않을 것을 가지고 있다.\n\"세상을 그렇게 만만하지 않아\"라는 것이 입버릇이다.", "A cat flies in the air with a grimace.\nSoysauceBee-nya even likes sweets but it has something that is not sweet.\nSoysauceBee-nya likes to say \"Life is not that easy.\" ", ""));
			Rows.Add( new MNP_LocalizeRow("L2112", "2112", "조금 멋을 낸 신사인체 하고 있지만 조금 잘못되어 있는 고양이. 용무가 있으면 거세게 헛기침을 한다. 뭐든 서양말로 표현하려고 한다. 짜증나….", "Gentleman-nya is an odd and funny cat who tries to be fashionable.\nThe cat gets people's attention by clearing The cat throat before speaking. The cat tries to express everything in foreign words which is annoying.", ""));
			Rows.Add( new MNP_LocalizeRow("L2113", "2113", "멋쟁이인척 하는 있지만 별로 있어보이지 않은 고양이.\n다른 사람의 옷을 보면 어드바이스 못해서 안달이다.\n조심하지 않으면 \"코디\"되어 버린다.", "A clunky cat pretend to be stylish.\nWhen it looks others fashion style, it is eager to give some advice to them. If someone isn't careful, It will coordinate them.", ""));
			Rows.Add( new MNP_LocalizeRow("L2114", "2114", "멋을 내기는 하는데 뭔가 헛돌고 있는 착각중인 고양이.\n자칭 멋진 코와 멋진 수염을 장착하고 있는데\n진심인지 개그인지　\n뭐라 말해줄 수 없는 상태가 되었다.", "A cat decorates itself but something goes wrong.\nThe cat says it has a nice nose and great whiskers.\nBut I cannot find out whether it is a joke or not.", ""));
			Rows.Add( new MNP_LocalizeRow("L2115", "2115", "백마에 올라탄(?) 고양이.\n아랫배 부분에 백마의 머리와 꼬리를 꿰메어 \n붙여 놓았다. 백마탄 신사 고양이가 당신을 \n데리러 올지도.", "A cat rides a white horse.\nThe head and tail of white horse were stitched its lower stomach.\nGentelman-on-White-Horse-nya might pick you up with a white horse.", ""));
			Rows.Add( new MNP_LocalizeRow("L2116", "2116", "아직 보지 못한 보물을 꿈꾸는 몽상가 고양이.\n금속음과 빛나는 물건에\n사족을 못쓰는 로맨티스트.\n상자를 보면 열지 않고는 못 배기지만 꽝도 많다.", "Captain-mew daydreams about lost treasures and has a fondness for metallic sounds and bright, shiny things. Captain-mew can't resist opening boxes, but only finds most of them to be empty.", ""));
			Rows.Add( new MNP_LocalizeRow("L2117", "2117", "모든 예감이 적중하는 럭키 고양이.\n세계 각지에서 금은보화를 긁어 모았는데\n귀찮아서 어딘가의 섬에 두고 온 것으로 보인다.", "A cat who can predict everything.\nBig-Hit-mew collected a lot of gold and jewels from all over the world. \nHaving so many things, the cat left them in a small island somewhere.", ""));
			Rows.Add( new MNP_LocalizeRow("L2118", "2118", "보물을 꿈꾸고 있는 몽상가 스타일의 고양이.\n상자를 열때 마다 항상 미간에 주름이 잡힌다.\n최근에 자신의 여행 자체에\n의문을 품기 시작하였다.", "A cat like a dreamer wants to find treasures.\nNo-Luck-mew always wrinkles up when it opens the box.\nNo-Luck-mew recently has started to think about his trip seriously.", ""));
			Rows.Add( new MNP_LocalizeRow("L2119", "2119", "보물을 각별히 사랑하는 고양이.\n자신의 보물을 지키기 위해서\n보물 상자에 저주를 걸었다.\n저주를 받은 자는 심각한 냉증에 걸리게 된다.", "A cat who loves treasures a lot.\nGhost-mew made a spell to protect his treasure.\nSomeone who got cursed might be frozen.", ""));
			Rows.Add( new MNP_LocalizeRow("L2120", "2120", "바삭바삭한 튀김옷 침낭 안에 들어가 있는 고양이.\n바삭하게 튀기기 위해서는 온도와 소리가 중요한 것 같다.\n좋아하는 색은 \"알맞게 구워진 아름다운 연한 갈색\"", "Croquette-mew is a cat who likes to wrap itself up snuggly in a \"crispy\" type of sleeping bag. It likes frying potato chips making sure they're cooked at the right temperature. Croquette then makes a loud and clear shout at the top of his voice. You can probably guess that this feline's favorite color is a \"beautiful golden brown.\"", ""));
			Rows.Add( new MNP_LocalizeRow("L2121", "2121", "바삭바삭 튀김옷 안에 크림이 가득차 있는 고양이.\n부드럽고 마일드한 성격.\n마음 속에는 뜨거운 것을 감추고 있다.", "A cat who has a crispy fried skin with fresh cream inside.\nCream-nya is gentle and mild.\nBut there's a flame inside its heart.", ""));
			Rows.Add( new MNP_LocalizeRow("L2122", "2122", "바삭바삭한 튀김옷 안에 달콤한 단호박이 가득차 있는 고양이.\n건강에 신경을 많이 써서 비타민A를 부지런히 모으고 있다.\n무법자인 체 하고 있다.", "A cat who has a crispy fried skin with a sweet pumpkin inside.\nPumpkin-nya worries about its own health a lot, so it collects vitamin A.\nPumpkin-nya pretends to outlaw.", ""));
			Rows.Add( new MNP_LocalizeRow("L2123", "2123", "바삭바삭한 튀김옷 침낭을 뒤집어쓰고 있는 고양이.\n바싹 튀기기 위해서는 온도와 구호가 중요한 것 같다.\n좋아하는 색은 \"알맞게 구워진 아름다운 옅은 갈색\".", "A cat who has a crispy fried skin.\nCalling-out and the temperature are important to fry it crispy.\nFired-Prawn-nya likes \"Well fried beautiful light brown\" the most.", ""));
			Rows.Add( new MNP_LocalizeRow("L2124", "2124", "언제나 마라카스를 흔들면서 리듬을 새기는 \n쾌활한 고양이. 어떤 음식이라도 살사소스를 \n뿌린다. 입버릇처럼 \"아미고!\"라고 말하지만 \n의미는 모르는 것 같다.", "Mucho-nya is a cheery-natured kitty who likes to make its presence known by shaking a pair of maracas. Mucho-nya puts salsa sauce on anything it eats. Its favorite word is \"Amigo\" but unfortunately, this feline doesn't seem to know that it means \"friend.\"", ""));
			Rows.Add( new MNP_LocalizeRow("L2125", "2125", "마라카스를 너무 좋아해서 항상 손에서 놓을 수 없는 고양이.\n식사 때에는 왼손에 포크,\n오른손에 마라카스를 쥔다.\n카라오케에서 장단 맞추는 것은 프로급.", "A cat always hold a maracas in its hand that this cat really likes it.\nMaracas-nya holds a fork in its left hand and a maracas in its right hand when it eats something.\nMaracas-nya can groove with it in the karaoke.", ""));
			Rows.Add( new MNP_LocalizeRow("L2126", "2126", "타코스를 너무나도 사랑하는 멕시칸 고양이.\n미지의 타코스를 찾아 세계를 여행하고 있다.\n달콤하면서 뜨거운 타코스를 고안하는 중.", "A Mexican cat who really loves tacos.\nTacos-nya travels all over the world to find new tacos.\nTacos-nya thinks about sweet and hot tacos.", ""));
			Rows.Add( new MNP_LocalizeRow("L2127", "2127", "항상 옆구리에 선인장을 안고 있는 고양이.\n집에는 선인장으로 가득해서 위험하다.\n가끔 볼로 비비다가 상처를 입기도 한다.", "A cat who always hold a cactus in arm.\nA lot of cactus in Cactus-nya house seems to be dangerous.\nSometimes Cactus-nya gets scars on his cheek.", ""));
			Rows.Add( new MNP_LocalizeRow("L2128", "2128", "\"외롭지 않은 걸\"이라는 말버릇을 가진 고집쟁이 고양이.\n뭉치지 않고 혼자서 조용하게 사는 것이 꿈이지만\n외로워서 무의식중에 눈물이 흐르는 자기 자신과 싸우고 있다.", "Lonesome-nya is a headstrong cat who always says \"I'm not lonely.\"\nLonesome-nya's dreams of living alone without hanging around with other Mitchiri cats. However Lonesome-nya is always battling with its inner-self and ends up in tears of loneliness.", ""));
			Rows.Add( new MNP_LocalizeRow("L2129", "2129", "언제나 밤샘하는 심야형 고양이.\n정신이 들면 아침이 되어 있는 경우가 많다.\n뭉처져 있지 않으면 외로워서 잠들지 못하는 듯 하다.", "A cat always does not sleep at all in the night. \nPajamas-nya usually recovers its senses in the morning.\nPajamas-nya seems to be lonely without gathering.", ""));
			Rows.Add( new MNP_LocalizeRow("L2130", "2130", "쓸데없이 화려하게 튀고 싶어하는 고양이.\n특기인 공기놀이로 \n주위를 즐겁게 해주는 것을 좋아한다.\n삐에로이지만 실패해서 웃음거리가 되면\n안보이는데서 운다…", "A cat who likes to stand out.\nPierrot-nya likes to make people happy with ball play that it is really good at.\nPierrot-nya is a clown, but it cries alone when it failed.", ""));
			Rows.Add( new MNP_LocalizeRow("L2131", "2131", "어디서나 잘 수 있도록\n이불과 베개를 가지고 다니는 고양이.\n외로울 때에는 바로 이불을 둘르고 자버린다.\n일어나면 자기 전의 일은 대체로 잊어버린다.", "A cat always brings a pillow and blanket to sleep anywhere it wants.\nGood-Night-nya sleeps in the blanket when it feels lonely.\nAfter woke up, Good-Night-nya forgets almost everything.", ""));
			Rows.Add( new MNP_LocalizeRow("L2132", "2132", "서부 개척시대의 정취를 풍기는 카우보이 고양이.\n메마른 사막과 가시 없는 선인장을 무엇보다도 \n사랑한다. 휘파람을 불지 못하는 것이 \n컴플렉스인 것 같다.", "Heehaw-mew is a cowboy cat who harks back to the pioneering days of the Wild West, and who just loves the dry sand of the desert as well as cactus trees (as long as they are without thorns!). Heehaw-mew has a bit of a complex about not being able to whistle like real cowboys can.", ""));
			Rows.Add( new MNP_LocalizeRow("L2133", "2133", "서부에서 으뜸가는 속사가 자랑인\n카우보이 고양이.\n목표를 정하자마자 \n적은 이미 쓰려저 있다.\n그러나 물총밖에 못쓰는 것이 결점.", "A cowboy cat who is a quick shooter.\nWhen Quick-Shooter-mew decided a counterpart, the enemy already fell down.\nThe weakest part is that it can only use a water gun.", ""));
			Rows.Add( new MNP_LocalizeRow("L2134", "2134", "카우보이를 동경하는 응석쟁이 고양이.\n엄마아빠가 로데오는 위험하다고 해서\n앞뒤로 흔들거리는 목마에서 연습중.", "A spoiled cat who wants to be a cowboy.\nThe cat practices on the rocking wooden horse. \nBecause the cat was told that rodeo is too dangerous by parents.", ""));
			Rows.Add( new MNP_LocalizeRow("L2135", "2135", "올가미를 솜씨 좋게 다루는 고양이.\n카우보이 모자를 눌러 쓰고 재를 올리는 날에 \n나타나서는 고리던지기 경품을 전부 낚아채 \n가져가 버렸다.", "A cat who can handle a trap nicely.\nLasso-mew came to the festival with a cowboy hat.\nLasso-mew took all the prizes from hoopla.", ""));
			Rows.Add( new MNP_LocalizeRow("L2136", "2136", "전신이 양철로 된 파괴력 발군의 고양이.\n기름 칠하는 것을 잘 잊어버려서 생각보다 \n움직임이 둔하다. 안에 조종자가 \n타고 있다는 소문이 있다.", "Robo-Mitchiri-nya is a cat made entirely out of tin, and is somewhat of a destructive type. This kitty forgets to oil itself and is therefore very slow-moving. Rumor has it there is someone inside Robo-Mitchiri-nya operating this robot-like cat!", ""));
			Rows.Add( new MNP_LocalizeRow("L2137", "2137", "육상 탐색이 특기인 주행형 고양이.\n어떤 거친 길도 가볍게 주행 가능하다.\n재채기를 해서 캐터필러가 빠져 버린 일이 있다.", "A cat who is good at overland exploration.\nOverland-nya can run on the every tough road.\nBut once lost a caterpillar", ""));
			Rows.Add( new MNP_LocalizeRow("L2138", "2138", "수중 탐색이 특기인 잠수형 고양이.\n숨을 오래 참지 못하기 때문에 그렇게 \n깊게 잠수할 수 없다. 친하게 되면 태워주는 것 같다.", "A cat who is good at underwater exploration.\nThe cat can not dive into the deep water that cannot breathe well.\nUnderwater let close friends ride on it.", ""));
			Rows.Add( new MNP_LocalizeRow("L2139", "2139", "몸을 경량화한 비행형 고양이.\n자랑스러워 하는 제트 추진을 통해 \n자유자재로 이동할 수 있다.\n등에 달린 날개는 단순한 장식.", "A light cat who can fly.\nAirplane-nya can go anywhere by using the jet propulsion that it is very proud of.\nAirplane-nya actually does not use the wings on its back.", ""));
			Rows.Add( new MNP_LocalizeRow("L2140", "2140", "2D의 별에서 온 미지의 고양이.\n먹은 물고기의 뼈를 던지는 등 원시적인 \n공격법을 가지고 있다. 점령을 허용하면 \n온 세상이 생선가게로 가득차게 된다고 한다.", "All we know about Nyanva-nya, whose origin remains a mystery, is that this tom hails from a two-dimensional planet. Throwing fish bones at its opponents is just one of its primitive-style fighting methods. This cat's invasion plans call for populating the world with fish stores.", ""));
			Rows.Add( new MNP_LocalizeRow("L2141", "2141", "아주 먼 미래에서 온 고양이.\n최근 타임 슬립을 너무 한 탓에\n온몸에 버그가 발생하였다.", "A cat from very far future.\nBugged-nya recently had a lot of time travelling so it got bugged seriously.", ""));
			Rows.Add( new MNP_LocalizeRow("L2142", "2142", "화성에서 온 문어...가 아닌 고양이.\n입에서 빔을 발사하는 것이 가능한 것 같다.\n이따금 지구에 관광하러 온다.", "A cat (not an octopus) from Mars.\nMartian-nya probably can shoot a beam with its mouth.\nMartian-nya sometimes travels the Earth.", ""));
			Rows.Add( new MNP_LocalizeRow("L2143", "2143", "UFO를 타고 있는 미지의 고양이.\n엄청 빠른 스피드로 하늘을 날아다닌다.\n좋은 멀미약이 없으면 탈 수 없다.", "A cat on the UFO.\nUFO flies very fast in the air.\nYou cannot ride UFO without motion sickness pills.", ""));
			Rows.Add( new MNP_LocalizeRow("L2144", "2144", "최근에 아무래도 신경쓰이는 상대가 있는 듯 한\n사랑에 빠진 아가씨 고양이.\n하지만 수줍음이 많기 때문에 좀처럼 말을 걸지 못해서\n거대 리본으로 여성의 능력을 올려서 어필하고 있다.", "A loving Ribbon-mew who is in love with someone recently.\nBut the cat is too to talk, so Ribbon-mew try to attract the one by wearing a giant ribbon .", ""));
			Rows.Add( new MNP_LocalizeRow("L2145", "2145", "톡특하고 화려한 컬러가 개성적인 고양이.\n그때 그때의 기분에 따라 색이 변하는 것 같으며\n기분 좋을 때에는 매우 밝은 색이 된다.\n감정이 색으로 나타나기 때문에 카드 게임은 아주 못한다.", "A cat who has very unique and fancy colorful fur.\nThe colors change by the mood and it becomes very bright when Colorful feels happy,", ""));
			Rows.Add( new MNP_LocalizeRow("L2146", "2146", "여름에는 수박만 먹는 고양이.\n그 때문인지 수박 모양이 되어 버린 것 같다.\n타이거냥이랑 헷갈리면 화를 낸다. ", "A cat who only eats watermelons during summer,\nSo Watermelon-mew got watermelon shaped body.\nThe cat gets upset when someone confused Watermelon with Tiger-nya.", ""));
			Rows.Add( new MNP_LocalizeRow("L2147", "2147", "일년 내내 선탠을 하고 있는 고양이.\n바다에서 태운 다갈색 피부를 자랑스러워 한다.\n실은 선탠 가게에 다니고 있다는 소문도…", "A cat who is tanning his body everyday for a year.\nSun-Kissed-mew is proud of brown body that made in the ocean.\nBut someone told that a cat goes to the tanning salon.", ""));
			Rows.Add( new MNP_LocalizeRow("L2148", "2148", "장수풍뎅이를 동경하고 있는 고양이.\n아무리 더워도 뿔과 망토를 착용하고 있다.\n스스로 올라갈 수 없기 때문에 나무에 오를 때는 \n다른 고양이에게 도움을 받는다. 손이 많이 가는 타입.", "A cat who wants to be a beetle wearing horns and a cape even in hot weather.\nHigh-maintenance cat cannot help but climbing a tree with someone else's support although it cannot climb by itself.", ""));
			Rows.Add( new MNP_LocalizeRow("L2149", "2149", "여름의 주역이 되고 싶은 고양이.\n화려하게 살고 있다.\n언제나 컬러풀하게 빛을 내고 있으며 눈부시다.", "A cat who wants to be a summer hero.\nFireworks-mew's life is fancy.\nFireworks-mew is always dazzling, colorful, and shiny.", ""));
			Rows.Add( new MNP_LocalizeRow("L2150", "2150", "귀여운 목걸이를 하고 있는 흔한 느낌의 고양이.\n다른 고양이들과 떨어지지 않고 항상 단체 행동을 하는 것이 매일매일의 모토.\n\"빨리 집에 가서 간식을 먹고 싶다\"가 입버릇.", "A typical cat who wears a cute collar always be in a group.  \"I want to go home and grab some snacks.\" is its favorite phrase.", ""));
			Rows.Add( new MNP_LocalizeRow("L2151", "2151", "블록만 지울 것이 아니라 나를 보란 말이다.", "Don't do block play, look at me.", ""));
			Rows.Add( new MNP_LocalizeRow("L2152", "2152", "너의 마음은 조사 완료. 더 이상 내게서 도망갈 수 없어…", "I know your mind. You can't run away from me anymore.", ""));
			Rows.Add( new MNP_LocalizeRow("L2153", "2153", "나의 튀김옷, 뜨겁지? 막 튀겨졌단 말이다…?", "Isn't my body hot? It is just fried.", ""));
			Rows.Add( new MNP_LocalizeRow("L2154", "2154", "말린 멸치가 딱딱해졌잖아! 책임을 지란 말이다!", "Niboshi or dried sardine is cracking hard! It's all your fault!", ""));
			Rows.Add( new MNP_LocalizeRow("L2155", "2155", "소중히 간직해둔 참치를 너와 먹고 싶은데♪ 여기, 아~앙♪", "I want to eat special tuna with you. Open your mouth.", ""));
			Rows.Add( new MNP_LocalizeRow("L2156", "2156", "여름에 모든 것을 걸고 있는 고양이.\n일년 내내 여름 생각만 하며 지내고 있다.\n튜브가 빠지지 않게 된 것도 여름의 좋은 추억.", "A cat who lives only for summer season.\nSummer-Memories nyan always thinks about summer.\nWhen it could not be out of swimming tube, it is a part of sweat summer memories.", ""));
			Rows.Add( new MNP_LocalizeRow("L2157", "2157", "봉오도리의 음악에 맞추어 어디선가 나타난 고양이. \n몸 뒤에 떠다니는 불덩이와 같은 것은 \n종이로 만든 소품으로 손에 쥐고 있는 \n부채로 조작이 가능한 것 같다.", "A cat who appeared out of nowhere dancing to Bon Festival music.\nPaper-made fireballs behind can be controlled with the fan in its hand.", ""));
			Rows.Add( new MNP_LocalizeRow("L2158", "2158", "고양이의 아이덴티티를 잃어가고 있는 고양이.\n일년내내 물장구 연습만 하지만\n발이 너무 짧아서 전혀 앞으로 나아가지 못하는 것 같다.", "A cat who is losing the cat´s identity,\nAlthough always working on kicking, it cannot move forward at all due to its short legs.", ""));
			Rows.Add( new MNP_LocalizeRow("L2159", "2159", "항상 3단 아이스크림을 먹고 있는 고양이.\n\"아이스크림은 당연히 3단이다냥\"이 입버릇.\n아이스크림이 2단으로 되어 있는 것은 좀처럼 볼 수 없는…", "A cat who always eats triple scoop cone.\nKeeps saying \"triple-scoop-cone is the best.\" \nIt is very rare to see this cat with double-scoop-cone.", ""));
			Rows.Add( new MNP_LocalizeRow("L2160", "2160", "경단을 매우 좋아하는 고양이.\n계절과 상관없이 경단만 먹는다.\n장래에는 경단이 되고 싶다고 생각하고 있으며 \n마음 속으로 츄비냥을 동경하고 있다.", "A dumpling lover cat eating only rice dumplings during any season.\nLonging to become rice dumpling in the future and admiring Chubby.", ""));
			Rows.Add( new MNP_LocalizeRow("L2161", "2161", "가을의 패션 리더. \n매년 가을이 되면\n코스모스를 몸에 붙이는 방법을 가르쳐 준다.\n모두들 칭찬해주지만 실제로 흉내내는 고양이는 없다.", "Autumn fashion leader cat.\nIn every autumn, trying to teach how to wear cosmos.\nAlthough many people flatter the way it wear cosmos but nobody actually follows it.", ""));
			Rows.Add( new MNP_LocalizeRow("L2162", "2162", "토끼 머리 장식을 한 고양이.\n귀 부분이 꼿꼿하지 않기 때문에 매일 공기를 넣고 있다.\n토끼 점프 연습을 하고 있지만 3일도 가지 않는 것 같다.", "A cat wearing rabbit ears, that need to be pumped in as ears cannot stand straight otherwise.\nBunny-nya practices rabbit jumping but it quits in three days.", ""));
			Rows.Add( new MNP_LocalizeRow("L2163", "2163", "머리에 밤송이가 꼽혀 있는 고양이.\n아무것도 하지 않아도 밤이 모여들어 박히는 체질이기 때문에\n먹을 것에 걱정한 적이 없다.", "A cat with chestnut bur on the head.\nChestnut burs always come to Roast-Chestnut-nya's head even it did nothing, so never worries about food.", ""));
			Rows.Add( new MNP_LocalizeRow("L2164", "2164", "경사스러운 일에 나타나는 고양이.\n\"장하다\"라고 하면서 손에 든 부채로 바람을 보내 준다.\n여름에는 상당히 요긴하게 여겨지는 것 같다.", "A cat that appears in a congratulating place.\nSaying 'Well done\" and the wind with the fan.\nIt seems to be very useful in the summer.", ""));
			Rows.Add( new MNP_LocalizeRow("L2165", "2165", "반짝반짝하는 것을 대단히 좋아하는 고양이.\n지휘봉 끝에 별을 붙였는데 반짝이기만 하면 뭐든 괜찮은 것 같다.\n다음에는 구슬을 붙이고 싶다고 생각하고 있다.", "A cat who loves something glittering. \nPutting star on the tip of the stick, but actually as long as it sparkles, it does not matter for Glittering so thinking to put marbles for the next time.", ""));
			Rows.Add( new MNP_LocalizeRow("L2166", "2166", "그 이름에서 큐피트적인 존재라는 소문을 가지고 있는 고양이.\n소문을 듣고 연애상담을 잘 해준다.\n자신은 누구에게 상담받으면 좋을지 모르는 것이 최근의 고민.", "A cupid-like cat just like its name.\nThrough word of mouth, many come to seek for love advice. Nobody can its consultant is its recent worry.", ""));
			Rows.Add( new MNP_LocalizeRow("L2167", "2167", "돌아가고 있으면 날아갈 것이라고 믿고 있는 고양이.\n머리에 프로펠라를 붙이고 싶다 생각하고 있지만\n3회전 정도로 어지러워하는 것 같다.", "A cat that is convinced that it can fly if it is keep turning.\nAlthough it would like to put a propeller to its head, it  get dizzy at about three spins.", ""));
			Rows.Add( new MNP_LocalizeRow("L2168", "2168", "뭔가 다른 고양이.\n주위에서도 \"뭔가 이상해\"라는 이야기를 계속 듣고 있으며\n결국 이름까지 \"뭔가이상해냥\"가 되어 버렸다.", "A cat with something wrong.\nMany keep saying \"something wrong\" then it goes by \"something wrong\".", ""));
			Rows.Add( new MNP_LocalizeRow("L2169", "2169", "일년 내내 청소를 하고 있는 고양이.\n눈깜짝할 사이에 주변 일대를 깨끗하게 청소해 버린다.\n빗자루질밖에 할 줄 모르기 때문에 어딘가에 쓰레기를 모은 후에는 어떻게 할 수가 없다.", "A cat that is cleaning all the year.\nIt cleans all around very quickly.\nIt does not know what to do after sweeping and gathering the dust.", ""));
			Rows.Add( new MNP_LocalizeRow("L2170", "2170", "\"테니스\"라는 경기를 동경하고 있는 고양이.\n라켓과 볼을 구입하였지만 룰은 모르는 것 같다.\n모든 일을 흉내내는 것 부터 시작하는 타입.", "A cat that is longing for a tennis match.\nAlthough Tenis-nyan is fully-equipped with a racket and a ball, it doesn't seem to know the rules. It is a cat that start from the style.", ""));
			Rows.Add( new MNP_LocalizeRow("L2171", "2171", "두명분의 노력을 하는 고양이.\n실전에서 누군가가 쉬게 되더라도 자신이 두명분의 연주를 할 수 있다면 좋을 것이라 생각하고 있다.\n지금까지 한명분의 실력도 발휘하지 못하고 있다.", "A cat making an efforts \nEven though someone is on absent Double-nyan thinks it can play for two.\nCurrently, does not prove that ability.", ""));
			Rows.Add( new MNP_LocalizeRow("L2172", "2172", "피리를 입에 3개 물고 있는 고양이.\n3개 동시에 불면 화려한 음색을 낼 수 있을 것이라 생각하지만 한번도 성공한 적이 없다.\n이제는 노력인지 고집인지 알수가 없다.", "A cat that has three whistles in one mouth.\nTriple-nyan thought it could play a beautiful tone as it blew three in one times, but it did not succeed.\nTriple-nyan does not know whether it is effort or stubborn.", ""));
			Rows.Add( new MNP_LocalizeRow("L2173", "2173", "일정한 리듬으로 삐하는 소리를 내고 있는 고양이.\n들이마시고 내뿜는 것을 반복하고 있는 와중에 엄청난 폐활량을 얻게 되었다.\n튜바를 권유받고 있지만 아직 삐하는 소리를 내고 싶어하는 것 같다.", "A cat that makes sounds  from a patty horn in a constant rhythm. It got a lot of lung capacity through repeating it. Party Horn-nyan was offered to Tuba but It seems that this cat still wants to sound ppia.", ""));
			Rows.Add( new MNP_LocalizeRow("L2174", "2174", "악기의 유지보수를 하는 고양이.\n다양한 악기를 잘 알고 있으며 음악대 모두에게 의지가 되는 존재이다.\n가장 좋아하는 악기는 \"기로\".", "A cat that carries out the maintenance of the instrument.\nThe cat knows a lot about musical instruments, and everyone in the band is rely on Turning.\nThe favorite instrument is \"Guiro\"", ""));
			Rows.Add( new MNP_LocalizeRow("L2175", "2175", "전화가 울리면 받아버리는 고양이.\n다른 사람의 전화도 받아버리기 때문에 여러 사람들과 통화를 하여 깜짝 놀라게 하고 있다. \n이야기를 잘하는 비결은 적당한 맞장구 같다.", "A cat who take a phone when it ringing.\nMobiler-nyan makes other people surprise for talking with others because it gets anyone's phone.\nThe secret to a good talking is make a quick response.", ""));
			Rows.Add( new MNP_LocalizeRow("L2176", "2176", "세탁을 좋아하는 고양이.\n비오는 날에도 개의치 않고 세탁을 해 버린다.\n세탁물이 없으면 적당히 더럽혀서 다시 세탁을 시작하기 때문에 세탁소에서 짤렸다.", "A cat who loves washing.\nDoesn't care about a rainy day, and have it cleaned up.\nLaundry-nyan was fired at a laundry because If there is no laundry, Laundry-nyan makes some laundry dirty and starts to do laundry again.", ""));
			Rows.Add( new MNP_LocalizeRow("L2177", "2177", "생선을 해체하면서 돌아다니는 고양이.\n물고기는 실물로써 배가 고프면 먹어버리는 것 같다.\n칼은 플라스틱제.", "A cat walking while dismantling a fish.\nFish is genuine, it seems to eat if he gets hungry.\nKitchen knife is made of plastic.", ""));
			Rows.Add( new MNP_LocalizeRow("L2178", "2178", "공연장에서 돌아다니며 음료와 음식을 판매하는 고양이.\n손님에게 꽤 호평을 얻고 있어서 상당한 매상을 올리고 있다.\n잘 팔리는 것은 \"개다래나무\"", "A cat who sells food and drinks at the concert hall.\nIt is quite popular with customers and gives considerable sales.\nGood item is a \"Date\".", ""));
			Rows.Add( new MNP_LocalizeRow("L2179", "2179", "좋은 냄새를 풍기고 있는 고양이.\n지글거리는 소리와 고소한 향기가 무기.\n철판의 열기로 피부도 빨갛게 되었다.", "A cat that has a good smell.\nThe sound of sizzle and a fragrant scent are its weapons.\nThe skin got red due to the heat of the iron plate.", ""));
			Rows.Add( new MNP_LocalizeRow("L2180", "2180", "검은고양이단의 일원인 점박이냥.\n검은고양이단의 동료를 찾아서 음악대에 잠입했다.\n그런데 예상 외로 연주를 잘해서음악대에 스카웃된 탓에 곤란해하고 있다. ", "Spotted-nya, a member of the Black Cat.\nSpotted-nya infiltrated into the music group for finding a fellow . \nRather, because surprisingly good at playing up onto the scout in the music.", ""));
			Rows.Add( new MNP_LocalizeRow("L2181", "2181", "타도 밋치리네코를 목표로 하고 있는 \"검은고양이단\"의 보스.\n하프를 연주하여 다른 고양이들을 잠들게 하는 작전을 펴지만 자기가 먼저 잠들기 때문에 잠을 버티는 특훈중.", "The boss of Black cat group who has a target to overthrow the \"Mitchiri the Neko\".\nIt's a strategy to play harps and invite other cats to sleep.\nBecause Boss-mew fall asleep first, it is under special training because it can not lose to sleep.", ""));
			Rows.Add( new MNP_LocalizeRow("L2182", "2182", "상냥하고 모성 넘치는 고양이. \n콜이 울리면 어디든 훨훨 내려 앉는다.\n치료내용은 적당히 끝내는 것 같은데 어쩐지 낫는다.", "A cat that gently and maternally overflows.\nWhen the call rings, the cat flies down everywhere.\nThe treatment content is hilarious, but somehow it cures.", ""));
			Rows.Add( new MNP_LocalizeRow("L2183", "2183", "트윈테일을 하고 있는 고양이.\n와인을 한손으로 쥐고 있지만 실제로는 마실 수 없다.\n머리카락이 보이면 쫓아가게 되는 자신의 본능을 용서할 수 없는 것 같다.", "A cat with twin tails.\nWith a glass of wine in one hand, actually Twin-Tail-nyan can not drink at all.\nIt cannot stand the instinct of chasing its own tails without noticing.", ""));
			Rows.Add( new MNP_LocalizeRow("L2184", "2184", "순간 보면 소처럼 보이는 고양이.\n본인은 고양이라고 우기지만 주변에서는 소라고 생각하고 있다. \n뭐가 되었든지 다들 상냥하게 대해주기 때문에 뭐든 상관 없다고 생각하기 시작했다. ", "The moment, cat that look like a cow.\nThe cat claims itself as a cat, but everyone thinks it to be a cow. It actually does not matter if cat or cow as everyone is nice to Cowbell-nya.", ""));
			Rows.Add( new MNP_LocalizeRow("L2185", "2185", "항상 행복을 가져다주고 싶어하는 신령스런 고양이.\n항상 뭔가를 부르면서 손짓한다.\n올해의 운세가 나쁘게 나온 것은 비밀로 하고 있다.", "A superstitious cat that always wants to bring happiness.\nIt always beckoning to call something.\nNever told anybody that a Omikuji or the paper oracle it drew this year was Daikyo or great misfortune, the worst one.", ""));
			Rows.Add( new MNP_LocalizeRow("L2186", "2186", "어렸을 때 가지고 놀던 게임에 꿈을 품은채 자란 고양이.\n칼로 찌르는 소리와 같은 효과음을 매우 좋아한다.\n갑옷을 입고 있지는 않지만 어딘가의 마을에서 팔고 있겟지라고 생각하면서 여행을 다니고 있다.", "A cat who grew up with a dream in a game played since it was little.\nSpit! Or Shakien! are favorite sounds. \nTravelling without an armor by thinking there is an armor shop somewhere.", ""));
			Rows.Add( new MNP_LocalizeRow("L2187", "2187", "카레라이스를 매우 좋아하는 고양이.\n질리지도 않고 매일 카레라이스만 먹고 있다.\n본인 이야기에 의하면 매일 재료는 바꾸고 있는 것 같다.", "Curry and rice is its favorite.\nEating just curry and rice everyday without getting bored. According to Curry-Rice-nyan, ingredients change everyday.", ""));
			Rows.Add( new MNP_LocalizeRow("L2188", "2188", "사자상을 옆에 끼고 있는 고양이.\n좋아하는 것은 나무, 싫어하는 것은 믹서라고 하는데\n실은 시쿠와사가 고양이로 된 모습이라는 소문이 있다.", "A cat with Shisa or a traditional Ryukyuan decoration resembling a cross between a lion and a dog, by its side. \nAs it likes trees and hates mixer, a rumor has it is a cat-shikuwasa, or citrus depressa.", ""));
			Rows.Add( new MNP_LocalizeRow("L2189", "2189", "몰래 극비 미션을 수행하고 있는 고양이.\n고양이의 민첩함을 살려서 어디든지 잠입하지만\n덫에 생선을 설치해 두면 쉽게 잡힌다.", "A cat who performs a secret mission unfamiliar.\nThe cat will infiltrate anywhere with the agility of cats\nIt is easy to captured if put a fish in a trap.", ""));
			Rows.Add( new MNP_LocalizeRow("L2190", "2190", "마법을 쓸수 있게 되고 싶은 고양이.\n나무 위에서 빗자루로 날고자 하고 있기 때문에 상처가 끊임없이 생긴다.\n이제 올빼미와 대화가 가능하게 되었다는 소문이 하나둘씩.", "A wanna-be witch cat.\nAs trying to fly on a broom from the top of the tree, so the scars are constantly marked.\nThere are some rumors that Majorin-mew can talk with the owl.", ""));
			Rows.Add( new MNP_LocalizeRow("L2191", "2191", "고양이 모양의 지팡이를 가진 고양이.\n붕붕 휘두르면 뭔가 나올 것 같지만 아무것도 나오지 않는다.\n너무 휘둘러서 지팡이 끝 부분을 잘 잃어버린다.", "A cat with a cat-shaped stick.\nAlthough it tried spinning around to put out something, with no luck.\nAs swings the wand too much,  it misses the tip quite often.", ""));
			Rows.Add( new MNP_LocalizeRow("L2192", "2192", "신발로 멋내는 것에 신경쓰고 있는 고양이.\n벗었다가 신었다가 바라보다가 하면서 만족하는 것 같다.\n뭐가 그렇게 재미있는 것일까?", "A cat who is careful about the fashion of his feet.\nYes, it seems satisfying with taking off or looking at it.\nWhat is fun so far?", ""));
			Rows.Add( new MNP_LocalizeRow("L2193", "2193", "낙엽이 많아질 쯤에 나타나는 고양이.\n매일 낙엽을 주워서 몸에 장식한다.\n마음에 들지 않는 낙엽이 있으면 바로 버려 버린다.", "A cat that appears when the fallen leaves have increased.\nEvery day The cat dresses up with some fallen leaves.\nThe cat throws away as soon as there are fallen leaves that The cat does not like.", ""));
			Rows.Add( new MNP_LocalizeRow("L2194", "2194", "털이 나 있는 모양이 상당히 볼륨감 있어서 폭신폭신한 고양이. 붙이는 털이기 때문에 떼고 붙이는 것이 자유라고.\n빗질하는 것이 귀찮기 때문에 항상 얽혀 있다.", "A cat of puffed whiffed fur\nits extra so it's easy to remove or take.\nThe cat doesn't like brushing so it seems to be trouble some all the time.", ""));
			Rows.Add( new MNP_LocalizeRow("L2195", "2195", "티포트를 옆에 끼고 있는 고양이.\n평소에는 인간들에게 차를 팔아 생활하고 있는 것 같다.\n좋아하는 음료는 생맥주.", "A cat who has a tea pot aside.\nIt seems that this cat usually sells tea to people for a living.\nTea-Pot-mew's favorite drink is draft beer.", ""));
			Rows.Add( new MNP_LocalizeRow("L2196", "2196", "모든 진실을 알 예정인 고양이.\n항상 누군가가 자신을 노리고 있는 느낌이 들어 못견뎌 한다.\n뒤돌아보고 아무도 없는 것을 확인한 후 안심하는 매일을 보내고 있다.", "A cat who is planning on knowing all truth.\nThe cat was disturbing from someone is being targeted.\nTurning around behind, The cat is sending everyday to relieve that no one exists.", ""));
			Rows.Add( new MNP_LocalizeRow("L2197", "2197", "고전 음악을 사랑하는 재능이 넘치는 고양이.\n통조림을 열때의 소리에서 영감을 얻는 등\n고양이 특유의 감성으로 작곡 활동을 하고 있다.", "Talented cat who loves classical music.\nMusi-nyan gets inspiration from the sound that opens up a can.\nMusi-nyan is  composing music with the sensibility of the cats.", ""));
			Rows.Add( new MNP_LocalizeRow("L2198", "2198", "악보를 가지고 있는 고양이.\n항상 오케스트라를 흥얼거리며 능력있는 고양이인 척 연출하고 있다.\n참고로 악보는 읽지 못한다.", "A cat holding a score.\nBeethor-nyan always produce an awkwardness to deceive an orchestra.\nBy the way, can not read the score.", ""));
			Rows.Add( new MNP_LocalizeRow("L2199", "2199", "흙 투성이의 고양이.\n오랫동안 흙에 묻여 있던 채로 파내어지지 않았기 때문에 몸도 갈색이 되어 버렸다.\n파내어지기 전까지는 푹 잠자고 있었던 것 같다.", "Muddy the cat. \nBecause it was buried in soil for a long time, the body became a brown as well.\nIt seems that it was in a sound asleep until dug up.", ""));
			Rows.Add( new MNP_LocalizeRow("L2200", "2200", "당근 같은 색깔의 고양이.\n스스로 영양 만점이라고 주변에 말을 퍼뜨리고 있어서 자주 갉아먹힘을 당한다.\n가장 무서워 하는 것은 말에게 갉아 먹히는 것.", "A cat with a color like a carrot.\nAs the cat advertise itself as nutritious, it is got bit quite often.\nWhat Carrot-mew was most afraid of was bitten by a horse.", ""));
			Rows.Add( new MNP_LocalizeRow("L2201", "2201", "땅속에서 새로 발견된 고양이.\n이 고양이의 소리를 들으면 모두들 3번 돌고나서 \"야옹\"이라고 말하고 싶어 진다.\n단팥죽에 넣는 것을 추천 (본인경험담).", "A newly-discovered cat duck from the soil.\nBy listening to it is mewing, everyone will be impulsive to turn around three times and then say \"meow\". \n(It itself says that) it goes well with Oshiruko or a  sweet red-bean soup", ""));
			Rows.Add( new MNP_LocalizeRow("L2202", "2202", "새하얀 고양이.\n내버려두면 예쁜 색의 꽃을 피울 수 있다.\n잘 보면 희미하게 비쳐져 보이는 부분이 있다고 한다.", "A snow white cat.\nIt will have a beautiful flower after leaving the cat alone.\nIt seems that there seems to be a portion that is totally transparent as it is often seen.", ""));
			Rows.Add( new MNP_LocalizeRow("L2203", "2203", "격식 차린 복장의 댄디한 고양이.\n손님으로부터 \"항상 먹던 것\"을 주문받는 것을 기다리고 있는데 메뉴는 별로 외우고 있지 않은 것 같다.", "Formal attire dandy cat.\nWhile waiting for customers to order \"usual menu\"\nIt seems that he does not remember much of the menu.", ""));
			Rows.Add( new MNP_LocalizeRow("L2204", "2204", "딱하고 나비 넥타이를 착용한 고양이.\n한손에는 잔, 한손에는 와인을 항상 들고 있다.\n손님으로부터 뭔가 요청이 있어도 자신이 와인을 먹는 것을 최우선으로 한다.", "A cat with a bow tie.\nGlass in one hand and a bottle of wine with one hand as usual.\nRegardless of what customers are asked, Sommelier-nya gives top priority to drinking wine.", ""));
			Rows.Add( new MNP_LocalizeRow("L2205", "2205", "조금 훌륭하게 된 고양이.\n훌륭하게 된 탓인지 여러 고양이들을 거만하게 부린다.\n최근 너무 뚱뚱해져서 이중턱이 된 것을 신경쓰고 있다.", "A cat who got a little famous.\nBecause Manager-nya is getting better, it wants to point various cats with Manager-nya's  jaws.\nManager-nya is concerned about becoming a double jaw because it is too plump recently.", ""));
			Rows.Add( new MNP_LocalizeRow("L2206", "2206", "조금 나쁘게 보이는 고양이.\n양복도 선글라스도 어두운 색으로 맞추어 분위기를 내는 것을 중요시하고 있다.", "A cat who looks a little bad.\nBoth the suit and the sunglasses set in a dark color to take care of the atmosphere.", ""));
			Rows.Add( new MNP_LocalizeRow("L2207", "2207", "매우 그림에 능숙한 고양이.\n옷은 더럽혀져 있는 편이 예술가답다고 믿고 있다.\n캔버스 앞에 서는 시간보다 옷을 세탁하는 시간이 길다.", "Very skillful painting cat.\nMo-nya is convinced that clothes are more dirty than artists.\nThe time washing the clothes is longer than going to the canvas.", ""));
			Rows.Add( new MNP_LocalizeRow("L2208", "2208", "코타츠를 보면 들어가지 않고는 못배기는 고양이.\n무엇이든 코타츠 안에서 해결하려고 한다.\n너무 귀찮아라고 생각하면서 자신의 몸을 뒤돌아 보았다.", "A cat who can not stand entering when The cat  looking at the kotatsu.\nThe cat wants doing everything in the kotatsu.\nThe cat thought how lazy The cat looked back to it.", ""));
			Rows.Add( new MNP_LocalizeRow("L2209", "2209", "새빨간 코의 고양이.\n크리스마스가 되면 썰매를 타고 온다.\n타고 온 설매는 어딘가에 숨기놓은 것 같은데 돌아갈 때에 숨긴 장소를 잊어버리고 마는 애들 같은 고양이이다.", "Cat of red your nose.\nWhen it comes to Christmas, Red-Nosed-mew gets on a sled and come.\nSorry seems to be hiding somewhere, but like a kids who forgets the place it hid when it returned.", ""));
			Rows.Add( new MNP_LocalizeRow("L2210", "2210", "일년 내내 두꺼운 옷을 입고 있는 숨막힐 듯한 고양이.\n뜨개질이 취미지만 시간이 많이 걸리기 때문에\n1년걸쳐 뜨개질 한 것을 다음해에 입는 것 같다.", "A difficult breath cat  wear some  thick clothes all the year round.\nKnitting is her hobby, but it takes lots of time\nIt seems to knit for wearing a knitted clothes for a next year.", ""));
			Rows.Add( new MNP_LocalizeRow("L2211", "2211", "일년 내내 장갑을 끼고 있는 고양이.\n손이 따끈따끈해서 마음도 따뜻한 것 같다.\n터치가 잘 되지 않을 때 이외에는 불편함을 느끼지 않는 것 같다.", "A cat who is wearing gloves all the year.\nIt seems that its hands are dull and the heart is warm.\nIt seems that it does not feel inconvenient except that the touch panel can not operate well.", ""));
			Rows.Add( new MNP_LocalizeRow("L2212", "2212", "모자를 매우 좋아하는 고양이.\n모자를 보게될 때마다 써버리기 때문에 머리 위에는 모자가 많이 쌓여있는 경우도 있다.", "A cat who loves a hat.\nSome times many hats are piled up on top of its head.", ""));
			Rows.Add( new MNP_LocalizeRow("L2213", "2213", "크리스마스가 가까워지면 나타나는 고양이.\n멋진 수염을 기르고 있으며 선물을 가지고 온다.\n선물 내용은 거진 개다래나무이다.", "A cat that appears when Christmas is near.\nSanta-nya keeps a fine beard and brings a present.\nThe content of the gift is roughly date.", ""));
			Rows.Add( new MNP_LocalizeRow("L2214", "2214", "설날을 좋아하는 고양이.\n좋은 향기에 이끌려 얼굴을 들이밀었는데 다테마키에서 빠져나갈 수 없게 되었다. \n맛있기 때문에 본인은 아무런 신경을 쓰지 않고 있다.", "A cat who loves New Year 's Day.\nThe cat was invited by a good smell and thrust his face to a new year's day pan cake and it could not get out of it. As it is tasty, the cat does not care.", ""));
			Rows.Add( new MNP_LocalizeRow("L2215", "2215", "으깬 어육과 함께 잘 반죽한 고양이.\n태어날 때 부터 카마보코 안에 있었기 때문에 카마보코의 맛 밖에 모른다.\n가장 흥미를 가지고 있는 것은 개다래나무인 것 같다.", "A cat that was kneaded up with fish cake.\nBecause The cat is in kamaboko since The cat was born The cat only know the taste of kamaboko.\nThe cat seems most interesting is a date.", ""));
			Rows.Add( new MNP_LocalizeRow("L2216", "2216", "뭔가에 둘러쌓여있지 않으면 안정이 되지 않는 냉증 고양이.\n둘러쌀 수 있는 것이라면 뭐든 좋은듯 하다.\n이동할 때에는 그대로 굴러서 간다.", "A coldness cat not calm unless you are surrounded by something.\nRoll-mew seems anything is possible as long as it is wrapped.\nWhen Roll-mew moving, this cat rolls as it is.", ""));
			Rows.Add( new MNP_LocalizeRow("L2217", "2217", "키리탄포 안에 끼어 있는 고양이.\n그 안은 코타츠보다 따뜻하고 있기에 편한 것 같다.\n치쿠와랑 자주  헷갈린다.", "A cat caught in a kiritanpo.\nThe inside seems to be warmer and cooler than the kotatsu.\nIt is mistaken as a fish cake often.", ""));
			Rows.Add( new MNP_LocalizeRow("L2218", "2218", "경사때 나타나는 소문의 고양이.\n도미 탈인형을 입고 깡총깡총 뛰는 모습이 목격되고 있다.\n한바탕 뛰고 나면 어디론가 가버리는 것 같다.", "A cat of rumor when it comes at congratulated.\nSome witnessed the appearance of bouncing with wearing a mask of bream.\nIt seems that if The cat bounced one time and disappear again to somewhere.", ""));
			Rows.Add( new MNP_LocalizeRow("L2219", "2219", "태고에 서식하고 있던 원시 고양이.\n팔힘이 세고 뭐든지 기합과 체력으로 극복한다.\n매일 사냥에 나가지만 수확이 있던 것을 본 적이 없다.", "A primitive cat who inhabited ancient times.\nThe cat is strong in arms and rides with anything with fight and physical strength.\nThe cat was never seen with any harvests although it go hunting everyday.", ""));
			Rows.Add( new MNP_LocalizeRow("L2220", "2220", "외국에서 수업을 받고 돌아온 고양이.\n요리 실력은 한사람 몫을 하지만 요리를 만들면 언제나 두사람 몫이 된다.\n함께 먹을 상대는 없는 것으로 보인다.", "A cat who trained abroad to be a French cook.\nFrench is very well-trained but it always cook for two people. French does not have anybody to eat with.", ""));
			Rows.Add( new MNP_LocalizeRow("L2221", "2221", "우아한 분위기가 감도는 고귀한 고양이.\n고급요리밖에 먹지 않기 때문에 ", "A noble cat with a delicious atmosphere.\nMaro-mew's bad attitude is cause from it eats only fine cuisine.\nIt seems to be trendy to write poetry with fish as a theme.", ""));
			Rows.Add( new MNP_LocalizeRow("L2222", "2222", "둥그런 눈썹이 매력인 고양이.\n유행품이라면 사족을 못써서 다양한 취미를 가지고 있는 것으로 보인다.\n물고기를 주제로 옛시를 읊는 것은 구식이라고 생각하고 있다.", "A cat who has a pair of round eyebrows are charming.\nIt seems that Hi-mew loves trendy items and The cat has lots of hobbies.\nThe cat thinks that it is an old style if makes a poetry with fish as a theme.", "캐릭터 설명 마지막"));
			Rows.Add( new MNP_LocalizeRow("L2500", "2500", "클리어한 미션이 생겼다냥!", "Myay! You’ve got cleared mission(s)!", "미션 잠금해제"));
			Rows.Add( new MNP_LocalizeRow("L2501", "2501", "미션'을 눌러서 확인해보자~", "Tap on ‘MISSION’ to check it out!", "미션 잠금해제"));
			Rows.Add( new MNP_LocalizeRow("L2502", "2502", "여러가지 미션이 있다냥\n클리어 하면 보상을 받을 수 있으니 잊지 말고 도전하자냥~", "There are a variety of missions you can clear. Don't forget to clear them all and get your rewards! Nyan!", "미션 잠금해제"));
			Rows.Add( new MNP_LocalizeRow("L2503", "2503", "지금의 설명은 '옵션'에서 언제든지 다시 볼 수 있다냥", "You can always see this explanation again from ‘OPTIONS’ menu.", "미션 잠금해제"));
			Rows.Add( new MNP_LocalizeRow("L2504", "2504", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L2505", "2505", "즐겁게 게임을 즐기고 있었냥?\n\n새 능력을 사용할 수 있게 되었다냥!", "Meow! Are you enjoying the game?\n\nYou can now use a new skill!", "패시브 잠금해제"));
			Rows.Add( new MNP_LocalizeRow("L2506", "2506", "레벨을 올리면 영구적인 효과를 얻을 수 있다냥!", "Level it up to get permanent effect! Nyan!", "패시브 잠금해제"));
			Rows.Add( new MNP_LocalizeRow("L2507", "2507", "지금의 설명은 '옵션'에서 언제든지 다시 볼 수 있다냥", "You can always see this explanation again from ‘OPTIONS’ menu.", "패시브 잠금해제"));
			Rows.Add( new MNP_LocalizeRow("L2508", "2508", "굉장히 잘해왔다냥!", "You’ve done a meawrvelous job so far!", "랭킹 잠금해제"));
			Rows.Add( new MNP_LocalizeRow("L2509", "2509", "그런데 랭킹은 알고 있을까냥?", "But I wonder if you're familiar with your Ranking?", "랭킹 잠금해제"));
			Rows.Add( new MNP_LocalizeRow("L2512", "2512", "이제 퍼즐의 조작에 익숙해졌냥?", "Have you become familiar with the puzzles?", "퍼즐아이템 잠금해제"));
			Rows.Add( new MNP_LocalizeRow("L2513", "2513", "지금부터 퍼즐에서 사용할 수 있는 유용한 아이템을 소개한다냥!", "Listen up nyow! I’ll introduce you to some useful items that can be used in puzzles!", "퍼즐아이템 잠금해제"));
			Rows.Add( new MNP_LocalizeRow("L2514", "2514", "아이템을 사용하면 어려운 스테이지를 좀 더 수월하게\n클리어 할 수 있다냥~~~", "You can clear difficult stages faster if you use an item, meow!", "퍼즐아이템 잠금해제"));
			Rows.Add( new MNP_LocalizeRow("L2515", "2515", "아이템을 터치하면 설명이 나온다냥!", "Tap the item to see the descriptions!", "퍼즐아이템 잠금해제"));
			Rows.Add( new MNP_LocalizeRow("L2516", "2516", "이제 '구출 히스토리'를 볼 수 있게 되었다냥\n어서 눌러서 확인해 보자냥~", "You can see ‘RESCUE HISTORY’ nyow! Let's check it out!", "도감 잠금해제"));
			Rows.Add( new MNP_LocalizeRow("L2517", "2517", "이곳은 스테이지를 진행하면서 달성하게 되는\n구출과 보스 퇴치의 결과를 보여준다 냥!", "You can see the results of the rescue and boss defeat mission progressions here!", "도감 잠금해제"));
			Rows.Add( new MNP_LocalizeRow("L2518", "2518", "모든 테마에서는 4마리의 밋치리네코를 구출하고\n1마리의 나쁜 보스를 물리쳐야한다 냥!", "Nyan~! In every episode, you have to rescue 4 Mitchiri Neko and defeat 1 bad boss!", "도감 잠금해제"));
			Rows.Add( new MNP_LocalizeRow("L2519", "2519", "테마의 구출과 보스 임무를 모두 달성하게 되면 \n보상을 받을 수 있으니 열심히 해보자 냥!", "Do your best to rescue all the kittens and defeat the boss! You’ll get rewards for every episode!", "도감 잠금해제"));
			Rows.Add( new MNP_LocalizeRow("L2520", "2520", "이제까지 굉장히 잘 해왔다냥!", "Meow~ So far so good!", "랭킹 잠금해제"));
			Rows.Add( new MNP_LocalizeRow("L2521", "2521", "그런데 순위는 알고 있을까~냥?", "But I wonder if you're familiar with ranking~?", "랭킹 잠금해제"));
			Rows.Add( new MNP_LocalizeRow("L2522", "2522", "여기에서는 모든 플레이어와 스코어를 겨룰 수 있다냥!", "You can compete your scores with all players here!", "랭킹 잠금해제"));
			Rows.Add( new MNP_LocalizeRow("L2523", "2523", "랭킹 순위가 높을수록 화려한 보상을 받을 수 있다냥~~", "You will get meawsome rewards if you rank high enough~!", "랭킹 잠금해제"));
			Rows.Add( new MNP_LocalizeRow("L2524", "2524", "보상으로 생선을 받으면 좀 나누어 달라냥!! 앞으로도 응원하겠다냐~~", "By the way, if you get some fish as a reward, don't forget to share with meow!! I’ll keep rooting for nya~~", "랭킹 잠금해제"));
			Rows.Add( new MNP_LocalizeRow("L2700", "2700", "게임에 도움이 되는 부스트 아이템이다냥\n폭탄이 더 자주 등장하게 하고,\n스페셜 어택이 더 많이 발동하게 하고,\n게임 시간을 늘려주니 게임이 어려울때 사용해보자 냥!", "This is a booster item that will help you in the game.\nIt will make bombs appear more often,\nmake special attacks happen more frequently,\nand extend time, so make sure to take advantage of them!", "부스트 아이템 팁"));
			Rows.Add( new MNP_LocalizeRow("L2701", "2701", "블록을 없애면, 폭탄 블록이 생긴다!\n색깔 폭탄은 같은 색의 블록을 함께 터뜨린다냥~", "When you eliminate a block, a bomb block will appear!\nColor bomb will pop with other blocks of the same color~", "폭탄 팁"));
			Rows.Add( new MNP_LocalizeRow("L2702", "2702", "별 폭탄은 주변 블록을 모두 터뜨려준다!!\n폭탄의 등장은 무작위로 등장하니 신경쓰지 말라냥!", "Star bomb will pop with all of the surrounding blocks!!\nKeep in mind that the bombs will appear randomly!", "폭탄 팁"));
			Rows.Add( new MNP_LocalizeRow("L2703", "2703", "퍼즐에 함께 입장한 고양이 위치에 맞는 색의 블록을\n터뜨리다 보면, 『스페셜 어택』이 발동한다 냥!\n\n밋치리네코를 구출하고 나쁜 보스를 물리치기 \n위해서는『스페셜 어택』이 매우 중요하다 냥!", "Keep popping blocks of the cat's position, until “Special Attack” activates!\n\n“Special Attack” is very important to rescue Mitchiri Neko and defeat the bad boss!", "스페셜 어택 팁"));
			Rows.Add( new MNP_LocalizeRow("L2704", "2704", "이번에는 맛있는 쿠키를 주워먹자냥!\n블록을 깨뜨리면 아래 깔린 쿠키도 함께 \n부서지니 신나게 블록을 깨자! 냥!", "This time, let's eat some delicious cookies!\nThe cookies will break when you pop block on top, so have at it! Nyan!", "쿠키 스테이지 팁"));
			Rows.Add( new MNP_LocalizeRow("L2705", "2705", "냥이들은 차갑고 단단한 돌맹이들이 \n싫다! 블록을 깨뜨려서 돌맹이들을 \n다 치워달라냥~~~~~", "Cats don't like hard rocks! Get rid of all the rocks by popping blocks~~~~~", "바위 스테이지 팁"));
			Rows.Add( new MNP_LocalizeRow("L2706", "2706", "맛있는 생선!! 생선이 먹고싶다냥!\n어서 블록을 깨뜨려서 튀겨달라냥!!", "Yummy fish!! Meow wants yummy fish!\nHurry, pop the blocks and fry it for me!!", "생선굽기 스테이지 팁"));
			Rows.Add( new MNP_LocalizeRow("L2707", "2707", "칼라풀냥들을 집에 보내주자!\n길을 막고 있는 장애물을 치워보자냥!", "Let's send these Colorful-mews home!\nLet's get rid of the obstacles that are blocking the way!", "이동미션 스테이지 팁"));
			Rows.Add( new MNP_LocalizeRow("L2708", "2708", "장애물을 모두 치워주면 \n칼라풀냥이 안심하고 이동한다냥!", "Clear all the obstacles away, so Colorful-mew can go home safely!", "이동미션 스테이지 팁"));
			Rows.Add( new MNP_LocalizeRow("L2709", "2709", "같은 색상의 블록이 만나는 공간을 탭하자!", "Tap the space where the same colored blocks meet!", "팁 퍼즐플레이1"));
			Rows.Add( new MNP_LocalizeRow("L2710", "2710", "같은 색상의 블록이 만나는 공간을 탭하자!\n익숙해지면 3개를 동시에 깨뜨리기에도 도전 해 보자!", "Tap the space where the same colored blocks meet!\nLet's try breaking 3 at once, once you become familiar!", "팁 퍼즐플레이2"));
			Rows.Add( new MNP_LocalizeRow("L2711", "2711", "콤보를 이어가면 피버에 돌입!\n피버 동안에는 획득할 수 있는 점수가 증가하고 콤보가 이어질수록 스코어도 증가!", "Don't break Combo to get Fever mode!\nDuring Fever, you will be able to get more points and higher score if you don't break Combo!", "팁 퍼즐플레이3"));
			Rows.Add( new MNP_LocalizeRow("L2712", "2712", "1일 14회 고급 아이템을 획득할 수 있는 이상한 상자!\n잊지 말고 받아보자!", "Get advanced item 14 times a day from the “Strange Box”!\nDon't forget to get your item!", "팁 네코 서비스"));
			Rows.Add( new MNP_LocalizeRow("L2713", "2713", "클리어하면 아이템을 받을 수 있는 \"미션\"이 있습니다.\n홈 화면에서 확인 할 수 있습니다.", "There is a “mission” that you can get item from once you clear it.\nCheck it out on the Home screen.", "미션 팁 1"));
			Rows.Add( new MNP_LocalizeRow("L2714", "2714", "미션을 클리어 하면 아이템을 받을 수 있어요\n잊지 말고 받아주세요!", "You can get item from clearing missions. Don't forget to get your item!", "미션 팁 2"));
			Rows.Add( new MNP_LocalizeRow("L2715", "2715", "고양이의 레벨을 올려주세요.\n고양이의 레벨을 올리면 파워가 올라 점수 획득이 쉬워집니다.", "Please level up your cat.\nOnce you level up your cat, its power will be increased and it will become easier for you to earn points.", "네코 레벨업 팁"));
			Rows.Add( new MNP_LocalizeRow("L2716", "2716", "[FFF55A]빙고에 도전하여 화려한 보상을 획득하자![-]\n\n[ff8ab1]처음 도전하는 사람은 \"Easy\" 모드로 시작하는 것을 추천![-]", "[FFF55A] Let’s do the Bingo challenge to get meawsome rewards! [-]\n\n[ff8ab1] Start with the \"Easy\" mode if you’re a rookie![-]", "빙고 팁1"));
			Rows.Add( new MNP_LocalizeRow("L2717", "2717", "[FFF55A]미션을 클리어 하면 보상을 받습니다!\n가로 세로 대각선이 연결되면 더 많은\n보상을 받을 수 있어요！[-]\n\n[ff8ab1]모든 미션을 클리어하면,\n특별한 보상을 받을 수 있어요![-]", "[FFF55A] You will get rewards for clearing missions!\nYou will get even more rewards if you connect the horizontal, vertical, and diagonal lines![-]\n\n[ff8ab1] You will get special reward if you clear all missions![-]", "빙고 팁2"));
			Rows.Add( new MNP_LocalizeRow("L2718", "2718", "[FFF55A]빙고를 중단해도\n도중부터 재개 할 수 있어요.[-]\n\n[ff8ab1]난이도를 확인하고 자신에게 맞는\n빙고에 도전하자！[-]", "[FFF55A] You can resume again even if the Bingo was interrupted halfway. [-]\n\n[ff8ab1] Check the level of difficulty and challenge the bingo of your level! [-]", "빙고 팁3"));
			Rows.Add( new MNP_LocalizeRow("L2719", "2719", "퍼즐 게임방법을 알려주겠다냥!\n똑같은 색깔의 블록 사이 공간을 터치해 보자냥!\n일직선 상의 블록끼리는 아무 빈공간을 터치해도 된당!", "Meow will teach you how to play the puzzle game!\nTouch the space in between the same colored blocks!\nMeow can touch any empty space if the blocks are aligned in a single line!", "스테이지 1팁"));
			Rows.Add( new MNP_LocalizeRow("L2720", "2720", "그림처럼 일직선이 아닌 블록을 깨뜨리려면\n블록들의 교차점을 터치해 줘야된다 냥!\n터치한 곳에서 상하좌우로 뻗어나간다고 \n생각해라냥!", "Meow have to touch the intersection point of the blocks if you want to break blocks that are not in one line!\nBasically, it will break in vertical and horizontal directions from where you touch.", "스테이지 1팁"));
			Rows.Add( new MNP_LocalizeRow("L2721", "2721", "4개까지 한번에 깨뜨릴 수 있다냥!\n당연히 한번에 많은 블록을 깨뜨릴때\n높은 점수를 얻을 수 있다냥~", "Meow can break up to 4 blocks at once!\nObviously, meow will get higher score if you break many blocks at once!", "스테이지 1팁"));
			Rows.Add( new MNP_LocalizeRow("L2722", "2722", "4개를 한번에 깨뜨렸을 때는 \n보너스로 뒤의 블록까지 깨뜨려준다냥\n\n이제 실전으로 돌입해보자냥!", "If you break 4 blocks at once, they’ll break the block in the back as a bonus!\n\nNyow, let's go for the real thing!", "스테이지 1팁"));
			Rows.Add( new MNP_LocalizeRow("L2723", "2723", "이 퍼즐이 끝나면\n리본의 가이드를 졸업한다 냥! \n자신의 능력을 믿어라 냥!", "Meow~ You’ll be graduating from Ribbon's guide once this puzzle is over! \nTrust your own abilities, nya~", "레벨 10 퍼즐팁2"));
			Rows.Add( new MNP_LocalizeRow("L2724", "2724", "가이드 없이 어렵다고 생각되면 \n\"옵션\"에서 가이드를 다시 켤 수 있다 냥!", "If you find anything difficult without the guide, you can always open the guide again from “OPTIONS” meow~", "레벨10 퍼즐팁3"));
			Rows.Add( new MNP_LocalizeRow("L3000", "3000", "보석샵", "Gem Shop", ""));
			Rows.Add( new MNP_LocalizeRow("L3001", "3001", "코인 샵", "Coin Shop", ""));
			Rows.Add( new MNP_LocalizeRow("L3002", "3002", "하트 충전", "Charge Heart", ""));
			Rows.Add( new MNP_LocalizeRow("L3003", "3003", "보석", "Gem", ""));
			Rows.Add( new MNP_LocalizeRow("L3004", "3004", "코인 샵", "Coin Shop", ""));
			Rows.Add( new MNP_LocalizeRow("L3005", "3005", "하트 충전", "Charge Heart", ""));
			Rows.Add( new MNP_LocalizeRow("L3006", "3006", "스킬", "Skill", ""));
			Rows.Add( new MNP_LocalizeRow("L3007", "3007", "예", "Yes", ""));
			Rows.Add( new MNP_LocalizeRow("L3008", "3008", "아니오", "No", ""));
			Rows.Add( new MNP_LocalizeRow("L3009", "3009", "보석가 부족합니다\n구매 하시겠습니까?", "Not enough Gem.\nWould you like to make purchase?", "재화 부족 안내 팝업"));
			Rows.Add( new MNP_LocalizeRow("L3010", "3010", "코인이 부족합니다\n구매 하시겠습니까?", "Not enough Coin.\nWould you like to make purchase?", "재화 부족 안내 팝업"));
			Rows.Add( new MNP_LocalizeRow("L3011", "3011", "하트가 부족합니다.\n충전 하시겠습니까?", "Not enough Heart.\nWould you like to charge?", "재화 부족 안내 팝업"));
			Rows.Add( new MNP_LocalizeRow("L3012", "3012", "마리를 얻었다.", "fish earned.", "생선 획득 메세지(일반)"));
			Rows.Add( new MNP_LocalizeRow("L3013", "3013", "마리를 입수했습니다!", "fish earned!", "생선 획득 메세지(랭킹)"));
			Rows.Add( new MNP_LocalizeRow("L3014", "3014", "알림", "Notification", "옵션 창"));
			Rows.Add( new MNP_LocalizeRow("L3015", "3015", "옵션", "Options", "옵션 창"));
			Rows.Add( new MNP_LocalizeRow("L3016", "3016", "효과음", "SFX", "옵션 창"));
			Rows.Add( new MNP_LocalizeRow("L3017", "3017", "BGM", "BGM", "옵션 창"));
			Rows.Add( new MNP_LocalizeRow("L3018", "3018", "공지사항", "Notice", "옵션 창"));
			Rows.Add( new MNP_LocalizeRow("L3019", "3019", "문의하기", "Contact Us", "옵션 창"));
			Rows.Add( new MNP_LocalizeRow("L3020", "3020", "FAQ", "FAQ", "옵션 창"));
			Rows.Add( new MNP_LocalizeRow("L3021", "3021", "인계 코드 등록", "Register Transfer Code", "옵션 창"));
			Rows.Add( new MNP_LocalizeRow("L3022", "3022", "인계 코드를 등록하십시오.", "Please register a Transfer Code.", "옵션 창"));
			Rows.Add( new MNP_LocalizeRow("L3023", "3023", "인계 코드 입력에 성공했습니다.", "Successfully entered Transfer Code.", "옵션 창"));
			Rows.Add( new MNP_LocalizeRow("L3024", "3024", "인계 코드 입력에 실패 했습니다.\n다시 확인해 주세요.", "Faied to enter Transfer Code.\nPlease try again.", "옵션 창"));
			Rows.Add( new MNP_LocalizeRow("L3025", "3025", "인계 코드 발행", "Issue Transfer Code", "옵션 창"));
			Rows.Add( new MNP_LocalizeRow("L3026", "3026", "인계 코드 보기", "View Transfer Code", "옵션 창"));
			Rows.Add( new MNP_LocalizeRow("L3027", "3027", "메일함", "Mailbox", ""));
			Rows.Add( new MNP_LocalizeRow("L3028", "3028", "확인", "OK", ""));
			Rows.Add( new MNP_LocalizeRow("L3029", "3029", "랭킹", "Ranking", ""));
			Rows.Add( new MNP_LocalizeRow("L3030", "3030", "캐릭터", "Character", ""));
			Rows.Add( new MNP_LocalizeRow("L3031", "3031", "크레인", "Crane", ""));
			Rows.Add( new MNP_LocalizeRow("L3032", "3032", "게임을 진행하고 사용할 수 있습니다.", "You can use it after game progression.", ""));
			Rows.Add( new MNP_LocalizeRow("L3033", "3033", "보석 [n]개를 사용하고\n'스페셜 크레인'에 도전 하시겠습니까?", "Would you like to use [n] Gem(s) to play ‘Special Crane’?", ""));
			Rows.Add( new MNP_LocalizeRow("L3034", "3034", "코인 [n1]을 사용해서 낚시를 [n2]회 하시겠습니까?", "Would you like to use [n1] Coin(s) to fish [n2] time(s)?", "낚시 안내"));
			Rows.Add( new MNP_LocalizeRow("L3035", "3035", "보석 [n]을 사용해서 10회 '스페셜 크레인'에 도전 하시겠습니까?", "Would you like to use [n] Gem(s) to play ‘Special Crane’ 10 times?", "10회 뽑기 안내"));
			Rows.Add( new MNP_LocalizeRow("L3036", "3036", "레벨이 오르면 \n업그레이드 할 수 있습니다.", "Level up to upgrade this Neko.", "네코 레벨업 불가 안내"));
			Rows.Add( new MNP_LocalizeRow("L3037", "3037", "게임에 도움이 되는 아이템 입니다.", "This item is helpful during gameplay.", "확인 필요"));
			Rows.Add( new MNP_LocalizeRow("L3038", "3038", "고양이의 파워를 높일 수 있습니다.", "You can increase the cat's power.", "확인 필요"));
			Rows.Add( new MNP_LocalizeRow("L3039", "3039", "Facebook 연결이 끊어진 친구입니다.", "This Facebook friend is disconnected.", "페이스북 링크가 끊어진 친구에 대한 안내"));
			Rows.Add( new MNP_LocalizeRow("L3040", "3040", "밋치리네코 세계에 접속 중이에요", "Welcome to the world of Mitchiri Neko!", ""));
			Rows.Add( new MNP_LocalizeRow("L3041", "3041", "친구목록", "Friends List", "옵션의 친구목록 버튼"));
			Rows.Add( new MNP_LocalizeRow("L3042", "3042", "고양이의 이름과 모양이 바뀝니다.\n하시겠습니까?", "The name and appearance of the cat will change.\nWould you like to proceed?", ""));
			Rows.Add( new MNP_LocalizeRow("L3043", "3043", "주간랭킹 보상", "Weekly Ranking Reward", ""));
			Rows.Add( new MNP_LocalizeRow("L3044", "3044", "무지개 교환권 리스트", "Rainbow Ticket List", ""));
			Rows.Add( new MNP_LocalizeRow("L3045", "3045", "10초 연장", "Extend 10 seconds", ""));
			Rows.Add( new MNP_LocalizeRow("L3046", "3046", "시간을 10초 연장합니다.", "10 seconds extended.", ""));
			Rows.Add( new MNP_LocalizeRow("L3047", "3047", "거절", "Decline", ""));
			Rows.Add( new MNP_LocalizeRow("L3048", "3048", "광고가 충전 중입니다.", "Ad is being charged at the moment.", "동영상 광고 충전 중 안내 멘트"));
			Rows.Add( new MNP_LocalizeRow("L3049", "3049", "이미 구입했습니다.", "You have already made this purchase.", "이미 구입했습니다. (패키지 구매)"));
			Rows.Add( new MNP_LocalizeRow("L3050", "3050", "게임을 종료합니다.", "Quit game?", "게임 종료 확인 멘트"));
			Rows.Add( new MNP_LocalizeRow("L3051", "3051", "Android / iOS에서 지원하는 기능입니다.", "This function is supported only on Android / iOS.", "웹에서 지원하지 않는 기능에 대한 멘트"));
			Rows.Add( new MNP_LocalizeRow("L3052", "3052", "더 이상 광고를 볼 수 없습니다.\n07:00 (22:00 GMT)에 \n횟수가 5회 충전 됩니다.", "We’ve run out of ads to show you at the moment.\nYou will be able to watch 5 more ads at 07:00 (22:00 GMT).", "광고 충전 안내"));
			Rows.Add( new MNP_LocalizeRow("L3053", "3053", "07:00 (22:00 GMT)에 횟수가 재 설정 됩니다.", "Number of ads you can watch will be reset at 07:00 (22:00 GMT).", "프리 크레인 광고 충전 안내"));
			Rows.Add( new MNP_LocalizeRow("L3054", "3054", "서버를 다시 시작 혹은 업데이트에 의해 연결이 끊어졌습니다.\n타이틀 화면으로 돌아갑니다.", "Connection lost due to server reboot or update.\nReturning to title page.", "재접속 요청"));
			Rows.Add( new MNP_LocalizeRow("L3055", "3055", "광고를 보고 Free Crane을 1회 하시겠습니까?", "Would you like to watch an ad and play Free Crane 1 time(s)?", "프리 크레인 광고 시청 확인 멘트"));
			Rows.Add( new MNP_LocalizeRow("L3056", "3056", "동영상 광고를 보고 '일반 크레인'에 \n도전 하시겠습니까?\n★ [n]회 더 도전 할 수 있습니다.", "Would you like to watch a video ad and play ‘Regular Crane’?\n★ You have [n] more tries.", "프리 크레인 광고 시청 확인 멘트2"));
			Rows.Add( new MNP_LocalizeRow("L3057", "3057", "공유하기가 완료되었습니다.", "Share successful!", "SNS 업로드 성공"));
			Rows.Add( new MNP_LocalizeRow("L3058", "3058", "공유하기가 취소되었습니다.", "Share canceled.", "SNS 업로드 실패"));
			Rows.Add( new MNP_LocalizeRow("L3059", "3059", "친구를 초대했습니다. \n(초대 보너스 하트는 1일 5개까지 받을 수 있습니다.)", "Invitation sent! \n(You can receive up to 5 Heart(s) each day for friend invitation.)", "SNS 초대 완료"));
			Rows.Add( new MNP_LocalizeRow("L3060", "3060", "이미 코드를 발급 받았습니다.", "Transfer Code has already been issued.", "인계 코드 이미 발급되었음"));
			Rows.Add( new MNP_LocalizeRow("L3061", "3061", "인계 코드가 잘못 되었습니다.", "Invalid Transfer Code", "잘못된 인계코드 입력"));
			Rows.Add( new MNP_LocalizeRow("L3062", "3062", "이미 사용된 인계 코드 입니다.", "This Transfer Code has already been used.", "이미 사용된 인계코드 입력"));
			Rows.Add( new MNP_LocalizeRow("L3063", "3063", "인계코드가 만료되었습니다.", "The Transfer Code has expired.", "만료된 인계코드 입력"));
			Rows.Add( new MNP_LocalizeRow("L3064", "3064", "같은 단말기에서 이전 할 수 없습니다.", "You may not transfer from the same device.", "같은 디바이스에서 발행된 인계코드 입력"));
			Rows.Add( new MNP_LocalizeRow("L3065", "3065", "인계가 완료되었습니다. \r\n게임을 다시 시작해주세요.", "Transfer complete! \nPlease restart the game.", "데이터 인계 완료"));
			Rows.Add( new MNP_LocalizeRow("L3066", "3066", "※ 서버와의 연결이 끊겼습니다\n통신 환경이 좋은 곳에서\n다시 연결을 시도해보십시오 ※ ", "※ Disconnected from the Server\nPlease check your network connection and try again later ※ ", "서버와의 연결이 끊어졌을 때 멘트"));
			Rows.Add( new MNP_LocalizeRow("L3067", "3067", "로그인", "Login", "확인 필요"));
			Rows.Add( new MNP_LocalizeRow("L3068", "3068", "완료", "Complete", ""));
			Rows.Add( new MNP_LocalizeRow("L3069", "3069", "캐릭터", "Character", "확인 필요"));
			Rows.Add( new MNP_LocalizeRow("L3070", "3070", "튜토리얼", "Tutorial", "확인 필요"));
			Rows.Add( new MNP_LocalizeRow("L3071", "3071", "플레이 시간을 5초 늘려줍니다.", "Extends play time for 5 more seconds.", "부스트 아이템 설명"));
			Rows.Add( new MNP_LocalizeRow("L3072", "3072", "폭탄이 더 자주 생성됩니다.", "Generates bombs more often.", "부스트 아이템 설명"));
			Rows.Add( new MNP_LocalizeRow("L3073", "3073", "고양이 스페셜 어택이 더 자주 발생합니다.", "Cat Special Attacks takes place more frequently.", "부스트 아이템 설명"));
			Rows.Add( new MNP_LocalizeRow("L3074", "3074", "시작하자마자 피버가 발생합니다.", "FEVER starts at the very beginning.", "부스트 아이템 설명"));
			Rows.Add( new MNP_LocalizeRow("L3076", "3076", "푸시 ON", "Push ON", "패시브 업그레이드 안내"));
			Rows.Add( new MNP_LocalizeRow("L3077", "3077", "푸시 OFF", "Push OFF", "패시브 업그레이드 안내"));
			Rows.Add( new MNP_LocalizeRow("L3078", "3078", "전송", "Send", "패시브 업그레이드 안내"));
			Rows.Add( new MNP_LocalizeRow("L3079", "3079", "일괄로 읽을 수 없는 메일이 있습니다.", "There are mails that can not be read in batches.", "일괄로 읽을 수 없는 메일이 있을때"));
			Rows.Add( new MNP_LocalizeRow("L3080", "3080", "게임을 진행하고 사용할 수 있습니다.", "You can use it after game progression.", "잠금처리된 컨텐츠 터치"));
			Rows.Add( new MNP_LocalizeRow("L3081", "3081", "티켓을 사용하여, 고양이를 받을까요?", "Would you like to use the ticket to get a cat?", "티켓 사용 확인"));
			Rows.Add( new MNP_LocalizeRow("L3082", "3082", "지금은 아직 획득 할 수 없습니다. \n업데이트를 기다려 주세요.", "Cannot be obtained at the moment. \nPlease wait for the next update!", "아직 구현되지 않은 컨텐츠"));
			Rows.Add( new MNP_LocalizeRow("L3083", "3083", "레벨을 10까지 올리면 특수 고양이 선물!", "You’ll get a special cat as a gift once you reach lv.10!", "레벨 10 달성  보상"));
			Rows.Add( new MNP_LocalizeRow("L3084", "3084", "페이스북을 연동하면 받을 수 있어요!", "Link your Facebook account to get this reward!", "페이스북 연결 보상"));
			Rows.Add( new MNP_LocalizeRow("L3085", "3085", "", "", "티켓 미구현 안내"));
			Rows.Add( new MNP_LocalizeRow("L3086", "3086", "LINE 초대 선물입니다!", "Thanks for sending out LINE invites!", "라인 초대 보상입니다!"));
			Rows.Add( new MNP_LocalizeRow("L3087", "3087", "밋치리 빙고 Easy를 클리어 하면 드립니다!", "Clear Mitchiri Bingo ‘Easy’ to get this reward!", "빙고 시작 안내"));
			Rows.Add( new MNP_LocalizeRow("L3088", "3088", "MAX", "MAX", ""));
			Rows.Add( new MNP_LocalizeRow("L3089", "3089", "순위는 1시간에 1번 업데이트 됩니다.", "Rankings are updated once every hour.", ""));
			Rows.Add( new MNP_LocalizeRow("L3090", "3090", "하트를 충전합니다.", "Charging Hearts.", ""));
			Rows.Add( new MNP_LocalizeRow("L3091", "3091", "[FFF55A] ※ 인계 방법 [-]", "[FFF55A] ※ How to transfer your account [-]", ""));
			Rows.Add( new MNP_LocalizeRow("L3092", "3092", "[46F0FF] 인계 코드 발행 [-]\n\n· 하단의 \"코드 발행\"버튼을 통해 인계 코드를 발행합니다.\n코드의 유효 기간은 발급일로부터 13 일입니다.\n· 새로운 인계 코드가 실행되면 이전 발행 된 인계 코드는 유효 기간 내라도 무효가됩니다.\n\n[46F0FF]인계 코드 입력 [-]\n\n· 인계 대상 단말기에서 게임 실행 후 \"옵션\"→ \"인계 코드 입력\"화면에서 발행한 인계 코드를 입력하십시오. \"", "[46F0FF] Issue Transfer Code [-]\n\n· Tap the “Issue Code” button at the bottom to issue your Transfer Code.\nThe code will expire in 13 days from the date of issue.\n· If a new transfer code is issued, any code that was issued before that will become invalid, even if 13 days have not passed since the date of issue.\n\n[46F0FF] Enter Transfer Code  [-]\n\n· From the target device, launch the game. Go to “OPTIONS”→”Enter Transfer Code” and enter the Transfer Code that was issued. “", ""));
			Rows.Add( new MNP_LocalizeRow("L3093", "3093", "[FFF55A] ※주의 사항 [-]", "[FFF55A] ※Warning [-]", ""));
			Rows.Add( new MNP_LocalizeRow("L3094", "3094", "· 인계 코드를 잘 관리하십시오.\n· Android에서 iOS로 변경 한 경우 또는 그 반대의 경우 업적 및 리더 보드 정보는 유지되지 않습니다.\n미션의 진행 상황은 유지되지 않습니다. 데이터 인계 후 초기화 되므로 주의하십시오.\n· 인계 전에 데이터에서 미션의 보상을 획득하는 경우, 인계 후 데이터에서 같은 임무 보상을 받을 수 없으므로 주의하시기 바랍니다.\n· 인계 코드의 유효 기간이 지난 것에 관해서는 사용할 수 없게 되므로주의하시기 바랍니다. ", "Please keep track of your transfer code.\nIf you wish to transfer across OS (i.e. From Android to iOS or vice versa), keep in mind that your achievements and leaderboard information will not be retained.\nPlease note that progress of the mission will not be retained. Please note that the data after transfer will be reset.\n· If you have already received a reward for a certain mission from the previous device, you will not be able to receive the same reward for that mission after the transfer.\n· Please note that expired Transfer Codes can not be used. ", ""));
			Rows.Add( new MNP_LocalizeRow("L3095", "3095", "코드 발행", "Issue Code", ""));
			Rows.Add( new MNP_LocalizeRow("L3096", "3096", "이미 인계코드가 발행되었습니다.", "Your Transfer Code has already been issued.", ""));
			Rows.Add( new MNP_LocalizeRow("L3097", "3097", "(유효기간 : [n])", "(Expires in : [n])", ""));
			Rows.Add( new MNP_LocalizeRow("L3098", "3098", "인계 코드를 입력하십시오", "Please enter your Transfer Code.", ""));
			Rows.Add( new MNP_LocalizeRow("L3099", "3099", "코드 입력", "Enter Code", ""));
			Rows.Add( new MNP_LocalizeRow("L3100", "3100", "코드사용", "Use Code", ""));
			Rows.Add( new MNP_LocalizeRow("L3101", "3101", "종료시간", "End Time", ""));
			Rows.Add( new MNP_LocalizeRow("L3102", "3102", "남은시간 {0:D2}시간 {1:D2}분", "Remaining time {0: D2} hour(s) {1: D2} min(s)", "친구 하트보내기쪽"));
			Rows.Add( new MNP_LocalizeRow("L3103", "3103", "다른 계정으로 로그인", "Sign in with another account", ""));
			Rows.Add( new MNP_LocalizeRow("L3104", "3104", "데이터 연동", "Sync Data", ""));
			Rows.Add( new MNP_LocalizeRow("L3105", "3105", "Google 로그인", "Google Login", ""));
			Rows.Add( new MNP_LocalizeRow("L3106", "3106", "[7D4700FF]테마 『[n]』을 클리어 했다냥!\n클리어 선물은 메일함을 열어보라냥! [-]", "[7D4700FF]You cleared Episode 『[n]』!\nClear bonus was sent to your \nmailbox! [-]", ""));
			Rows.Add( new MNP_LocalizeRow("L3107", "3107", "[FF6E00FF]하지만 다음 테마로 가기 위해서는 별을 33개 이상 모아야 한다냥~~[-]", "[FF6E00FF]But you need to collect 『33 stars』 to \nmove on tonext episode.[-]", ""));
			Rows.Add( new MNP_LocalizeRow("L3108", "3108", "[7D4700FF]테마 『[n]』에서 별을 33개 이상 모았냥!\n그러면 다음 테마로 진행한다냥![-]", "[7D4700FF]You collected more than 33 stars!\nSo move on to next episode![-]", ""));
			Rows.Add( new MNP_LocalizeRow("L3109", "3109", "[FF6E00FF]다음 테마는 업데이트를 기다려 달라냥! [-]", "[FF6E00FF]Next episode will be updated soon! [-]", ""));
			Rows.Add( new MNP_LocalizeRow("L3110", "3110", "닫기", "Close", ""));
			Rows.Add( new MNP_LocalizeRow("L3111", "3111", "무료 크레인을\n모두 사용하였습니다.\n\n매일 오전 7시에\n충전됩니다.", "It will charge at 22:00 (GMT).", ""));
			Rows.Add( new MNP_LocalizeRow("L3112", "3112", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L3113", "3113", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L3114", "3114", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L3115", "3115", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L3116", "3116", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L3117", "3117", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L3118", "3118", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L3119", "3119", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L3120", "3120", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L3121", "3121", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L3122", "3122", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L3123", "3123", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L3124", "3124", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L3125", "3125", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L3126", "3126", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L3127", "3127", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L3128", "3128", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L3129", "3129", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L3130", "3130", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L3131", "3131", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L3132", "3132", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L3137", "3137", "만나서 반갑다!\n게임의 안내를 맡은 '리본' 이다 냥!\n간단하게 게임 방법을 설명해주겠다 냐~", "Nyaice to meet you!\nMy name is ‘Ribbon’! I’m here to guide you through this game!\nMeow will briefly explain how to play this game for you!", "첫 시작 로비화면 튜토리얼"));
			Rows.Add( new MNP_LocalizeRow("L3138", "3138", "설명이 끝나면 특별히 '나'를 선물하기 때문에 잘 듣는게 좋다냥!", "When we’re done, you’ll get ME!! as a special reward, so make sure to keep your ears open!", "첫 시작 로비화면 튜토리얼"));
			Rows.Add( new MNP_LocalizeRow("L3139", "3139", "우선 첫 '밋치리네코'를 찾아보자\n'SHOP' 버튼을 터치해 보자 냥!", "First, let's look for the first ‘Mitchiri Neko’.\nTap on the ‘SHOP’ button!", "첫 시작 로비화면 튜토리얼"));
			Rows.Add( new MNP_LocalizeRow("L3140", "3140", "스페셜 크레인'을 터치해보자!\n처음엔 서비스로 무료다 냥!", "Tap on ‘Special Crane’!\nSince it's your first time, it’s on meow!", "첫 시작 로비화면 튜토리얼"));
			Rows.Add( new MNP_LocalizeRow("L3141", "3141", "레버를 움직이고 버튼을 누르면!\n누가 나올지는 나도 모른다냥~", "Move the lever and push the button...!\nNyaxciting! I wonder who’s going to come out~!", "첫 시작 로비화면 튜토리얼"));
			Rows.Add( new MNP_LocalizeRow("L3142", "3142", "당신의 '밋치리네코' 1호를 획득했다냥! \n이제 첫번째 스테이지를 시작해보자 냥~", "There, you have your very first ‘Mitchiri Neko’! \nLet's start the first stage!", "첫 시작 로비화면 튜토리얼"));
			Rows.Add( new MNP_LocalizeRow("L3143", "3143", "최대 3종류의 고양이와 함께 할 수 있다냥~ \n함께할 고양이를 설정해라 냐!", "You can take up to 3 types of cats with you!\nYou have to choose which ones to take!", "첫 시작 로비화면 튜토리얼"));
			Rows.Add( new MNP_LocalizeRow("L3144", "3144", "크레인에서 획득한 '밋치리네코'를 선택해보자", "Why don't you choose the ‘Mitchiri Neko’ you just picked out of the Crane machine?", "첫 시작 로비화면 튜토리얼"));
			Rows.Add( new MNP_LocalizeRow("L3145", "3145", "이제 준비 오케이냥! \n'게임시작'을 터치해라냥~~", "Nyow we're all set! \nTap on the \"Game Start” button~", "첫 시작 로비화면 튜토리얼"));
			Rows.Add( new MNP_LocalizeRow("L3146", "3146", "첫번째 스테이지를 클리어했다면\n이번에는 성장에 대한 설명이다냥!\n하단의 「성장」 버튼을 터치해봐라 냥!", "Now that you’re cleared the first stage,\nmeow is probably the good time to tell you about levels!\nTap on the “TRAIN” button on the bottom!", "첫 시작 로비화면 튜토리얼"));
			Rows.Add( new MNP_LocalizeRow("L3147", "3147", "이곳은 획득한 밋치리네코들의 공간이다 냥!\n'밋치리네코'를 선택해서 레벨을 올려보자!", "This is where you can see all of your Mitchiri Neko!\nChoose ‘Mitchiri Neko’ to level them up!", "첫 시작 로비화면 튜토리얼"));
			Rows.Add( new MNP_LocalizeRow("L3148", "3148", "냥이의 레벨이 올라가면 파워가 상승하고, \n파워가 높을수록 구출과 보스 스테이지를 \n쉽게 클리어 할 수 있다 냥!", "The higher their level, the more power they will have, and the more power they have, the easier your rescue and boss battle missions will be!", "첫 시작 로비화면 튜토리얼"));
			Rows.Add( new MNP_LocalizeRow("L3149", "3149", "마지막으로 ★ 랭크업에 대해 설명해 주겠다냥", "Last but not least ★ Let me tell you about ranking up!", "첫 시작 로비화면 튜토리얼"));
			Rows.Add( new MNP_LocalizeRow("L3150", "3150", "이 설명만 끝나면 실컷 즐길 수 있게 해주겠다냥!\n조금만 참아라 냐!!", "Don't worry, meow will let you have all the fun right after this!\nHang in there!!", "첫 시작 로비화면 튜토리얼"));
			Rows.Add( new MNP_LocalizeRow("L3151", "3151", "우리 냥이들은 생선을 많~~이 먹어야 \n진화를 할 수 있다냥!", "Us cats need to eat a looooooooooooooot of fish to evolve!", "첫 시작 로비화면 튜토리얼"));
			Rows.Add( new MNP_LocalizeRow("L3152", "3152", "생선의 종류에 따라 성장도가 다르니 \n더 비싼 생선을 달라냥!", "Don't forget! The more expensive the fish is, the faster we will grow!", "첫 시작 로비화면 튜토리얼"));
			Rows.Add( new MNP_LocalizeRow("L3153", "3153", "이제 게임 설명은 끝이다냥. \n스테이지를 진행하면서 밋치리네코를 구출하고\n나쁜 보스를 쓰러뜨려 달라냥!", "Well, that's it for the game. \nGo explore all the stages, rescue Mitchiri Nekos, and defeat the bad boss!", "첫 시작 로비화면 튜토리얼"));
			Rows.Add( new MNP_LocalizeRow("L3170", "3170", "현재 선택된 고양이는 성장 가능한 최고의 등급이기 때문에 성장 시킬 수 없습니다.", "The selected cat is already at the maximum level and can not be leveled up any more.", "고양이 랭크업 불가 안내1"));
			Rows.Add( new MNP_LocalizeRow("L3171", "3171", "다른 고양이를 얻을 때 물고기 주기 화면을 통해 고양이를 성장시켜 봅시다.", "Try to level up another cat through ‘feed fish’ screen.", "고양이 랭크업 불가 안내2"));
			Rows.Add( new MNP_LocalizeRow("L3180", "3180", "\"밋치리 빙고\"는 알고 있었냥?\n간단히 설명한다냥!", "Nyan! Have you ever heard of “Mitchiri Bingo”?\nI’ll explain briefly!", "빙고 튜토리얼 1"));
			Rows.Add( new MNP_LocalizeRow("L3181", "3181", "\"밋치리 빙고\"!\n각 번호에 해당 하는 미션을\n퍼즐을 즐기면서 클리어 하자냥!", "“Mitchiri Bingo”!\nClear the mission for each number while enjoying the puzzles!", "빙고 튜토리얼 2"));
			Rows.Add( new MNP_LocalizeRow("L3182", "3182", "빙고 설명은 \"옵션\"에서 볼 수 있다냥\n잊어버리면 보고 오자냐~", "You can read more about Bingo from “OPTIONS” menu!\nYou can always go back and read it again!", "빙고 튜토리얼 3"));
			Rows.Add( new MNP_LocalizeRow("L3183", "3183", "처음부터 어려운 것에 도전할 수 있지만\n우선은 쉽게 도전해보자냐!", "Challenging difficult missions is wonderful, but baby steps first!", "빙고 튜토리얼 4"));
			Rows.Add( new MNP_LocalizeRow("L3184", "3184", "미션 클리어를 축하한다냐~\n미션은 클리어 하는 것만으로도 보상을 준다냥", "Congratulations on clearing the mission~\nYou’ll get rewards for clearing missions!", "첫번째 빙고 미션 튜토리얼"));
			Rows.Add( new MNP_LocalizeRow("L3185", "3185", "미션을 하나 클리어 했을 때와 클리어한 미션들이 일렬로 갖추어지면\n더 멋진 보상을 받을 수 있다냥", "You’ll get even better rewards if you can form a line with missions you cleared!", "첫번째 빙고 미션 튜토리얼"));
			Rows.Add( new MNP_LocalizeRow("L3186", "3186", "모든 미션을 클리어 하면 \n빙고 한정 고양이를 받을 수 있으니 \n열심히 클리어 하라냥!", "Clear all missions to get the special cat that you can get during limited time only!", "첫번째 빙고 미션 튜토리얼"));
			Rows.Add( new MNP_LocalizeRow("L3200", "3200", "코인을 구입했습니다.", "Coin(s) purchased.", "코인 구매 안내"));
			Rows.Add( new MNP_LocalizeRow("L3201", "3201", "■주의 사항 ■\n\n우리 게임은 무료로 이용이 가능하지만\n\n일부 유료 아이템을 판매하고 있습니다.\n\n20세 미만이 구입하는 경우\n\n반드시 보호자의 허가를 받고 \n\n함께 구입을 부탁드립니다.", "■Notice■\n\nAnyone can enjoy this game for free,\n\nhowever we also offer some in-app purchase items.\n\nIf you are younger than 20 years old,\n\nplease ask permission of your guardian\n\nand make purchases under supervision.", "로고 화면 텍스트"));
			Rows.Add( new MNP_LocalizeRow("L3202", "3202", "메일함이 비어 있어요!", "Your mailbox is empty!", "메일함이 비어있다"));
			Rows.Add( new MNP_LocalizeRow("L3203", "3203", "하트가 이미 가득 차 있습니다.", "Heart is already full.", "하트 구매 → 이미 가득차 있는 상태"));
			Rows.Add( new MNP_LocalizeRow("L3204", "3204", "하트를 충전 했습니다.", "Heart has been recharged.", "하트 구매 완료"));
			Rows.Add( new MNP_LocalizeRow("L3205", "3205", "[ff8ab1]이 빙고에 도전 하시겠습니까？[-]\n\n[fff55a]※도중에 다른 빙고로 변경 할 수 있습니다。[-]", "[Ff8ab1] Would you like to challenge this bingo? [-]\n\n[fff55a] ※You may switch to a different bingo any time you wish. [-]", "빙고 도전"));
			Rows.Add( new MNP_LocalizeRow("L3206", "3206", "[ff8ab1]도전 중인 빙고를 그만하고\n이 빙고에 도전하시겠습니까？[-]\n\n[fff55a]※그만두어도 이어서 시작할 수 있습니다.[-]", "[ff8ab1] Would you like to pause the current bingo to challenge this bingo? [-]\n\n[fff55a] ※You may come back and finish this bingo any time you wish. [-]", "빙고 재도전"));
			Rows.Add( new MNP_LocalizeRow("L3207", "3207", "도전중인 빙고가 없습니다. \n도전 하시겠습니까?", "No bingo challenge is underway. \nWould you like to challenge?", "빙고 도전 필요 멘트"));
			Rows.Add( new MNP_LocalizeRow("L3208", "3208", "프리 티켓을 입수 했습니다!\n메일을 확인하십시오.", "You have a free ticket!\nCheck your mailbox.", "프리티켓 획득"));
			Rows.Add( new MNP_LocalizeRow("L3209", "3209", "레어 티켓을 입수 했습니다.\n메일을 확인하십시오.", "You have a rare ticket!\nCheck your mailbox.", "레어티켓 획득"));
			Rows.Add( new MNP_LocalizeRow("L3210", "3210", "[n1] [n2]를 획득했습니다.", "You have acquired [n1] [n2].", "코인[n1] 1000[n2] 을 획득했습니다. "));
			Rows.Add( new MNP_LocalizeRow("L3211", "3211", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L3212", "3212", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L3213", "3213", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L3214", "3214", "만료", "Expiration", "만료시간"));
			Rows.Add( new MNP_LocalizeRow("L3215", "3215", "[n]위", "Ranked [n]th", "1위, 2위 3위.."));
			Rows.Add( new MNP_LocalizeRow("L3216", "3216", "이전 순위 결과 [n]위! \n보상은 메일로 받아주세요.", "You have ranked [n]th place from the past ranking! \nPlease check your mailbox for rewards.", "랭킹 보상"));
			Rows.Add( new MNP_LocalizeRow("L3217", "3217", "프리 교환권", "Free Exchange Ticket", "프리 교환권"));
			Rows.Add( new MNP_LocalizeRow("L3218", "3218", "레어 교환권", "Rare Exchange Ticket", "레어 교환권"));
			Rows.Add( new MNP_LocalizeRow("L3219", "3219", "무지개 교환권", "Rainbow Exchange Ticket", "무지개 교환권"));
			Rows.Add( new MNP_LocalizeRow("L3220", "3220", "[ffef46]동영상 광고를 시청하면 '보석'과 '코인' 등의\n게임에 도움이 되는 선물을 받을 수 있습니다![-]", "[Ffef46] Watch video ads to get free gifts that will come in handy in the game! [-]", "네코 보너스 안내"));
			Rows.Add( new MNP_LocalizeRow("L3221", "3221", "★다음 중 하나를 받을 수 있습니다.★\n\n『보석×10』\n『코인×2,000』\n『연어×1』", "★You can get one of the following items★『Gem× 10』『Coin × 2,000』『Salmon × 1』", "네코 보너스 안내"));
			Rows.Add( new MNP_LocalizeRow("L3222", "3222", "[ffef46]★[n] 회 더 시청할 수 있습니다★[-]", "[ffef46] ★[n] more views available★ [-]", "네코 보너스 안내"));
			Rows.Add( new MNP_LocalizeRow("L3223", "3223", "[ff8ab1]※받는 선물은 동일한 확률로 랜덤입니다.\n※「아니오」를 선택하면 광고를 시청하지 않고\n『코인×1,000』이 선물됩니다.[-]", "[ff8ab1] ※Reward items are given at random chances with equal probability.\n※ You will get 『Coin × 1,000』 if you choose 「NO」 and decide not to watch an ad. [-]", "네코 보너스 안내"));
			Rows.Add( new MNP_LocalizeRow("L3224", "3224", "하트", "Heart", ""));
			Rows.Add( new MNP_LocalizeRow("L3225", "3225", "코인", "Coin", ""));
			Rows.Add( new MNP_LocalizeRow("L3226", "3226", "보석", "Gem", ""));
			Rows.Add( new MNP_LocalizeRow("L3227", "3227", "모두 받기", "Get Everything", "메일함 모두 받기 버튼"));
			Rows.Add( new MNP_LocalizeRow("L3228", "3228", "준비", "Ready", ""));
			Rows.Add( new MNP_LocalizeRow("L3229", "3229", "고양이 스킬", "Cat Skill", ""));
			Rows.Add( new MNP_LocalizeRow("L3230", "3230", "고양이의 선물", "Cat’s Gift", ""));
			Rows.Add( new MNP_LocalizeRow("L3231", "3231", "해제", "Remove", ""));
			Rows.Add( new MNP_LocalizeRow("L3232", "3232", "구매 하시겠습니까?", "Would you like to make purchase?", ""));
			Rows.Add( new MNP_LocalizeRow("L3233", "3233", "확인", "OK", "확인버튼, 영문에서는OK로 통일"));
			Rows.Add( new MNP_LocalizeRow("L3234", "3234", "일", "day(s)", "기한 표시 용도"));
			Rows.Add( new MNP_LocalizeRow("L3235", "3235", "시간", "hour(s)", "기한 표시 용도"));
			Rows.Add( new MNP_LocalizeRow("L3236", "3236", "레벨:", "Level:", "네코 성장 화면"));
			Rows.Add( new MNP_LocalizeRow("L3237", "3237", "파워:", "Power:", "네코 성장 화면"));
			Rows.Add( new MNP_LocalizeRow("L3238", "3238", "캐릭터 뽑기", "Character Gacha", "캐릭터 뽑기 화면(Crane)"));
			Rows.Add( new MNP_LocalizeRow("L3239", "3239", "히스토리", "History", ""));
			Rows.Add( new MNP_LocalizeRow("L3240", "3240", "메일함", "Mailbox", ""));
			Rows.Add( new MNP_LocalizeRow("L3241", "3241", "선물", "Gift", ""));
			Rows.Add( new MNP_LocalizeRow("L3242", "3242", "미션", "Mission", "미션창 "));
			Rows.Add( new MNP_LocalizeRow("L3243", "3243", "옵션", "Options", ""));
			Rows.Add( new MNP_LocalizeRow("L3244", "3244", "밋치리 보너스가 남아있지 않습니다. 하루에 2번 충전 됩니다.", "No more Mitchiri Bonus left for today. It will be charged twice every day.", "밋치리 보너스 모두 소진"));
			Rows.Add( new MNP_LocalizeRow("L3245", "3245", "조금 더 시간이 흐른 후에 밋치리 보너스를 받을 수 있습니다.", "You can receive Mitchiri Bonus after a little while.", ""));
			Rows.Add( new MNP_LocalizeRow("L3246", "3246", "일일미션", "Daily\nMissions", "미션창 상단 탭"));
			Rows.Add( new MNP_LocalizeRow("L3247", "3247", "주간미션", "Weekly\nMissions", "미션창 상단 탭"));
			Rows.Add( new MNP_LocalizeRow("L3248", "3248", "일일 미션은 15:00 (GMT) 에 초기화 됩니다.", "Daily Missions reset every day at 15:00 (GMT).", "미션창 하단 설명"));
			Rows.Add( new MNP_LocalizeRow("L3249", "3249", "스코어", "Score", ""));
			Rows.Add( new MNP_LocalizeRow("L3250", "3250", "주간 미션은 월요일 15:00 (GMT) 에 초기화 됩니다.", "Weekly Missions reset every Monday at 15:00 (GMT).", "미션창"));
			Rows.Add( new MNP_LocalizeRow("L3251", "3251", "랭킹 보상", "Ranking Reward", ""));
			Rows.Add( new MNP_LocalizeRow("L3252", "3252", "[n]위의 랭킹 보상을 획득했습니다.", "You have earned ranking reward for coming in [n]th place.", ""));
			Rows.Add( new MNP_LocalizeRow("L3253", "3253", "[n]개 획득!", "[n] obtained!", "미션 완료 연출 "));
			Rows.Add( new MNP_LocalizeRow("L3254", "3254", "금주의 스코어 랭킹 경쟁 장소는 스테이지 「[n1]」 ~ 「[n2]」 입니다.", "Compete for this week’s score ranking in stages 「[n1]」 through 「[n2]」.", ""));
			Rows.Add( new MNP_LocalizeRow("L3255", "3255", "언어", "Language", ""));
			Rows.Add( new MNP_LocalizeRow("L3288", "3288", "보너스", "Bonus", ""));
			Rows.Add( new MNP_LocalizeRow("L3289", "3289", "보석을 [n]개 받았습니다.", "You have received [n] Gem(s).", "보석를 n개 얻엇어요"));
			Rows.Add( new MNP_LocalizeRow("L3290", "3290", "낚시", "Fishing", ""));
			Rows.Add( new MNP_LocalizeRow("L3292", "3292", "프리 크레인", "Free Crane", ""));
			Rows.Add( new MNP_LocalizeRow("L3295", "3295", "스페셜 크레인", "Special Crane", ""));
			Rows.Add( new MNP_LocalizeRow("L3298", "3298", "고등어 [n] 마리 받았습니다", "You have received [n] Chub(s)", ""));
			Rows.Add( new MNP_LocalizeRow("L3299", "3299", "참치를 [n] 마리 받았습니다", "You have received [n] Tuna(s)", ""));
			Rows.Add( new MNP_LocalizeRow("L3300", "3300", "연어를 [n] 마리 받았습니다", "You have received [n] Salmon(s)", ""));
			Rows.Add( new MNP_LocalizeRow("L3301", "3301", "공유하기", "Share", "공유하기 SNS 버튼"));
			Rows.Add( new MNP_LocalizeRow("L3306", "3306", "★높은 등급 순", "★ From Highest Ranks", "★ 캐릭터 등급별 정렬 안내"));
			Rows.Add( new MNP_LocalizeRow("L3307", "3307", "종료시간", "End Time", "종료시간"));
			Rows.Add( new MNP_LocalizeRow("L3400", "3400", "고양이 세계에 접속 중입니다", "Welcome to the world of Mitchiri Neko!", "영문에서는 단순하게 Connecting으로 변경"));
			Rows.Add( new MNP_LocalizeRow("L3401", "3401", "잘못된 결제 프로세스입니다.", "Invalid billing process.", ""));
			Rows.Add( new MNP_LocalizeRow("L3402", "3402", "다음 스테이지", "Next Stage", ""));
			Rows.Add( new MNP_LocalizeRow("L3403", "3403", "종료", "Quit", ""));
			Rows.Add( new MNP_LocalizeRow("L3404", "3404", "세션의 연결이 끊어졌습니다.\n재접속 해주세요.", "Session Terminated.\nPlease reconnect.", ""));
			Rows.Add( new MNP_LocalizeRow("L3405", "3405", "언어를 변경 하시겠습니까?\n타이틀 화면으로 돌아갑니다.", "Would you like to change the language?\nReturning to title page.", ""));
			Rows.Add( new MNP_LocalizeRow("L3406", "3406", "1 레벨 업그레이드", "Level 1 Upgrade", ""));
			Rows.Add( new MNP_LocalizeRow("L3407", "3407", "5 레벨 업레이드", "Level 5 Upgrade", ""));
			Rows.Add( new MNP_LocalizeRow("L3408", "3408", "과일 업그레이드", "Fruit Upgrade", ""));
			Rows.Add( new MNP_LocalizeRow("L3409", "3409", "파워 업!", "Power Up!", ""));
			Rows.Add( new MNP_LocalizeRow("L3410", "3410", "하트를 더 이상 받을 수 없습니다.", "You may not receive any more Heart.", "하트를 더이상 받을 수 없습니다."));
			Rows.Add( new MNP_LocalizeRow("L3411", "3411", "게임시작", "Start Game", ""));
			Rows.Add( new MNP_LocalizeRow("L3412", "3412", "초", "sec", "시간단위 "));
			Rows.Add( new MNP_LocalizeRow("L3418", "3418", "레벨을 더 이상 올릴 수 없습니다.", "You may not level up any further.", "네코의 랭크업을 할 수 없다는 멘트"));
			Rows.Add( new MNP_LocalizeRow("L3419", "3419", "오늘의 동영상 시청 횟수가 한계에 도달 했습니다.", "You have reached the view limit for today.", "동영상 광고 부족 멘트"));
			Rows.Add( new MNP_LocalizeRow("L3420", "3420", "지난 랭킹 보상을 이미 받았거나, 이전 순위 정보가 없습니다.", "You have already received reward for the previous ranking, or do not have record of the previous score.", ""));
			Rows.Add( new MNP_LocalizeRow("L3421", "3421", "Facebook 친구를 불러오고 있습니다. 잠시 기다려주십시오.", "Importing Facebook friends list. Please wait.", ""));
			Rows.Add( new MNP_LocalizeRow("L3422", "3422", "이미 이 친구에게 하트를 보냈습니다.", "You have already sent a Heart to this friend.", "이미 당일 FB 친구에게 하트를 보냈을때 멘트"));
			Rows.Add( new MNP_LocalizeRow("L3423", "3423", "로그 아웃 하시겠습니까?", "Are you sure you want to log out?", "로그아웃 확인 멘트"));
			Rows.Add( new MNP_LocalizeRow("L3424", "3424", "생선창고", "Feed", ""));
			Rows.Add( new MNP_LocalizeRow("L3425", "3425", "고양이를 선택하십시오", "Choose a cat", "장착 고양이가 하나도 없을때 멘트"));
			Rows.Add( new MNP_LocalizeRow("L3426", "3426", "이미 이 패키지를 구입하였습니다.", "You have already purchased this package.", ""));
			Rows.Add( new MNP_LocalizeRow("L3427", "3427", "퍼즐에서 프리티켓을 획득했습니다!", "You received a free ticket from puzzle!", "메일함 퍼즐 프리티켓 획득 문구"));
			Rows.Add( new MNP_LocalizeRow("L3428", "3428", "코인 [n]으로 파워 업그레이드를 진행하시겠습니까?", "Would you like to upgrade power using [n] Coin(s)?", ""));
			Rows.Add( new MNP_LocalizeRow("L3429", "3429", "보석 [n]으로 파워 업그레이드를 진행하시겠습니까?", "Would you like to upgrade power using [n] Gem(s)?", ""));
			Rows.Add( new MNP_LocalizeRow("L3430", "3430", "광고 보고 무료 크레인!", "Watch a video to play Free Crane!", ""));
			Rows.Add( new MNP_LocalizeRow("L3431", "3431", "코인을 [n] 획득했습니다.", "You have earned [n] Coin(s).", "코인을 n개 얻었어요"));
			Rows.Add( new MNP_LocalizeRow("L3432", "3432", "하트를 받았습니다.", "Heart(s) received.", "하트를 우편함으로 받았어요"));
			Rows.Add( new MNP_LocalizeRow("L3433", "3433", "하트 충전", "Charge Heart", ""));
			Rows.Add( new MNP_LocalizeRow("L3434", "3434", "하트를 충전했습니다.", "Hearts have been charged.", ""));
			Rows.Add( new MNP_LocalizeRow("L3435", "3435", "고마워!", "Thank you!", "네코 구출 말풍선"));
			Rows.Add( new MNP_LocalizeRow("L3436", "3436", "에잇! 두고보자!", "Eeek! Better watch out next time!", "보스 네코 말풍선"));
			Rows.Add( new MNP_LocalizeRow("L3437", "3437", "일반 랭킹 보상", "General Ranking Reward", ""));
			Rows.Add( new MNP_LocalizeRow("L3438", "3438", "퇴치 보상", "Battle Reward", ""));
			Rows.Add( new MNP_LocalizeRow("L3439", "3439", "남은 시간", "Time remaining", ""));
			Rows.Add( new MNP_LocalizeRow("L3440", "3440", "항목을 선택하십시오", "Select", ""));
			Rows.Add( new MNP_LocalizeRow("L3441", "3441", "스페셜 크레인", "Special Crane", ""));
			Rows.Add( new MNP_LocalizeRow("L3442", "3442", "10회 크레인", "Crane (10 times)", ""));
			Rows.Add( new MNP_LocalizeRow("L3443", "3443", "낚시", "Fishing", ""));
			Rows.Add( new MNP_LocalizeRow("L3444", "3444", "랭킹보상", "Ranking Reward", ""));
			Rows.Add( new MNP_LocalizeRow("L3445", "3445", "홈 화면에 나오는 보상 아이콘을 탭하십시오.", "Tap on Reward icon that appears on the home screen.", ""));
			Rows.Add( new MNP_LocalizeRow("L3446", "3446", "주간 전체 순위는 일주일 마다 초기화 되고, 보상을 받을 수 있습니다.", "Weekly overall ranking resets every week. Don't forget to claim your reward!", ""));
			Rows.Add( new MNP_LocalizeRow("L3447", "3447", "기간 한정", "Limited Time Only", ""));
			Rows.Add( new MNP_LocalizeRow("L3448", "3448", "레벨업 보상", "Level Up Reward", ""));
			Rows.Add( new MNP_LocalizeRow("L3449", "3449", "고양이의 선물 보상", "Cat's Gift Reward", ""));
			Rows.Add( new MNP_LocalizeRow("L3450", "3450", "랭킹 보상", "Ranking Reward", ""));
			Rows.Add( new MNP_LocalizeRow("L3451", "3451", "SNS 업로드 보수", "SNS Share Reward", ""));
			Rows.Add( new MNP_LocalizeRow("L3452", "3452", "동영상 광고 시청 보상", "Video Ads Reward", ""));
			Rows.Add( new MNP_LocalizeRow("L3453", "3453", "신규 고양이 승리 보상", "New Cat Victory Reward", ""));
			Rows.Add( new MNP_LocalizeRow("L3454", "3454", "퓨전 불가능 보상", "Incompatible Fusion Reward", ""));
			Rows.Add( new MNP_LocalizeRow("L3455", "3455", "시리얼 코드 보수", "Serial Code Reward", ""));
			Rows.Add( new MNP_LocalizeRow("L3456", "3456", "친구 보너스", "Friends Bonus", ""));
			Rows.Add( new MNP_LocalizeRow("L3457", "3457", "선물 보너스", "Gift Bonus", ""));
			Rows.Add( new MNP_LocalizeRow("L3458", "3458", "로그인 보너스", "Login Bonus", ""));
			Rows.Add( new MNP_LocalizeRow("L3459", "3459", "이벤트 참여 보상", "Event Participation Reward", ""));
			Rows.Add( new MNP_LocalizeRow("L3460", "3460", "SNS 보상", "SNS Reward", ""));
			Rows.Add( new MNP_LocalizeRow("L3461", "3461", "평가보상", "Evaluation Reward", ""));
			Rows.Add( new MNP_LocalizeRow("L3462", "3462", "이벤트 보상", "Event Reward", ""));
			Rows.Add( new MNP_LocalizeRow("L3463", "3463", "버그 발생 보상", "Bug Compensation", ""));
			Rows.Add( new MNP_LocalizeRow("L3464", "3464", "운영 정책", "Operational Policy", ""));
			Rows.Add( new MNP_LocalizeRow("L3465", "3465", "하트 [n]개가 지급되었습니다.", "[n] Heart(s) received.", ""));
			Rows.Add( new MNP_LocalizeRow("L3466", "3466", "코인 [n]이 지급되었습니다.", "[n] Coin(s) received.", ""));
			Rows.Add( new MNP_LocalizeRow("L3467", "3467", "보석 [n]개가 지급되었습니다.", "[n] Gem(s) received.", ""));
			Rows.Add( new MNP_LocalizeRow("L3468", "3468", "빙고 보너스", "Bingo Bonus", "빙고 달성 보상 "));
			Rows.Add( new MNP_LocalizeRow("L3469", "3469", "패키지 구입 혜택입니다.", "Here’s your package purchase benefits.", "패키지 구입 혜택"));
			Rows.Add( new MNP_LocalizeRow("L3470", "3470", "레벨 10 달성을 축하합니다!", "Congratulations on reaching level 10!", "레벨 10 달성 보상"));
			Rows.Add( new MNP_LocalizeRow("L3471", "3471", "페이스북 연동에 감사드립니다!", "Thank you for connecting to Facebook!", "페이스북 연동 보상"));
			Rows.Add( new MNP_LocalizeRow("L3472", "3472", "레벨 10 달성을 축하합니다!\n메일로 선물을 보냈어요!", "Congratulations on reaching level 10!\nWe've sent a gift to your mailbox!", "레벨 10 달성 보상"));
			Rows.Add( new MNP_LocalizeRow("L3473", "3473", "페이스북 연동에 감사드립니다!\n메일로 선물을 보냈어요!", "Thank you for connecting to Facebook!\nWe've sent a gift to your mailbox!", "페이스북 연동 보상"));
			Rows.Add( new MNP_LocalizeRow("L3474", "3474", "밋치리 보너스", "Mitchiri Bonus", "밋치리 보너스"));
			Rows.Add( new MNP_LocalizeRow("L3475", "3475", "미션 보너스", "Mission Bonus", "미션 보너스"));
			Rows.Add( new MNP_LocalizeRow("L3476", "3476", "홈 화면으로 이동하여\n계속 설명해 준다 냥!", "We’ll go back to the Home screen and\ncontinue the explanation!", ""));
			Rows.Add( new MNP_LocalizeRow("L3477", "3477", "재시도", "Retry", ""));
			Rows.Add( new MNP_LocalizeRow("L3478", "3478", "클리어", "Clear", ""));
			Rows.Add( new MNP_LocalizeRow("L3479", "3479", "일시 정지", "Pause", ""));
			Rows.Add( new MNP_LocalizeRow("L3480", "3480", "계속하기", "Continue", ""));
			Rows.Add( new MNP_LocalizeRow("L3481", "3481", "종료", "Quit", ""));
			Rows.Add( new MNP_LocalizeRow("L3482", "3482", "\"※ 어떤 문제로 인해 게임 접속이 불가능합니다\n수고 스럽겠지만, 공식 HP의 문의 양식으로\n보고를 부탁드립니다 ※ \"", "“※ Due to some kind of problem, we are unable to connect you to the game. We're very sorry, but please contact us through the form on our official website. ※\"", "게임접속이 불가능합니다. (원인 모름)"));
			Rows.Add( new MNP_LocalizeRow("L3483", "3483", "밋치리네코팝", "Mitchiri Neko Pop", "게임 타이틀"));
			Rows.Add( new MNP_LocalizeRow("L3484", "3484", "서버와의 연결이 끊겼습니다\n통신 환경이 좋은 곳에서\n다시 연결을 시도해보십시오", "Disconnected from the Server\nPlease check your network connection and try again later", "서버 연결 실패(통신상태 이상)"));
			Rows.Add( new MNP_LocalizeRow("L3485", "3485", "※ 서버 점검을 실시하고 있습니다.\n자세한 내용은 공식 Facebook 혹은 \n커뮤니티 사이트를 확인하시기 바랍니다 ※", "※ Server Maintenance is underway.\nFor more information, please visit our official Facebook page. ※", "서버 점검 메세지"));
			Rows.Add( new MNP_LocalizeRow("L3486", "3486", "게임을 즐겁게 즐기고 계신다면, \"리뷰\" 해 주었으면 좋겠다 냥!", "Don't forget to leave meow a review if you're enjoying the game!", "평가 요청"));
			Rows.Add( new MNP_LocalizeRow("L3487", "3487", "※ 최신 업데이트 버전이 공개되어 있습니다\n  이용 저장소에서 업데이트를하십시오 ※ ", "※ There is an update released.\nPlease update the app from the store. ※ ", "업데이트 요청"));
			Rows.Add( new MNP_LocalizeRow("L3488", "3488", "구매 처리 오류가 발생했습니다. 시간을두고 다시 실행하십시오.", "An error has occurred during the purchase process. Please try again after a while.", "구매 실패 메세지(내용)"));
			Rows.Add( new MNP_LocalizeRow("L3489", "3489", "네트워크 오류", "Network Error", "구매 실패 메세지(타이틀)"));
			Rows.Add( new MNP_LocalizeRow("L3490", "3490", "리뷰", "Review", "평가 요청 팝업에 사용"));
			Rows.Add( new MNP_LocalizeRow("L3491", "3491", "하러가기", "Take me there", "평가 요청 팝업에 사용"));
			Rows.Add( new MNP_LocalizeRow("L3492", "3492", "나중에", "Later", "평가 요청 팝업에 사용"));
			Rows.Add( new MNP_LocalizeRow("L3493", "3493", "안한다", "Nope", "평가 요청 팝업에 사용"));
			Rows.Add( new MNP_LocalizeRow("L3494", "3494", "오늘도 밋치리네코와 함께 해주었으면 냐 ~ (= 'ω`=)", "Come play with meow~ (= 'ω`=)", "미접속 로컬 푸시 메세지"));
			Rows.Add( new MNP_LocalizeRow("L3495", "3495", "밋치리네코 하는 것을 잊지 않은가 냐 ...? (= 'ω`=)", "Did you forget to play Mitchiri Neko today...? (= 'ω`=)", "미접속 로컬 푸시 메세지"));
			Rows.Add( new MNP_LocalizeRow("L3496", "3496", "오랜만에 해 주었으면 냐 ~ (= 'ω`=) 기다리고있다 냥!", "It’s been quite a while... Nya~ (= 'ω`=) Meow are waiting for you...", "미접속 로컬 푸시 메세지"));
			Rows.Add( new MNP_LocalizeRow("L3497", "3497", "스페셜 크레인 도전 횟수가 재설정 되었다 냥! (= 'ω`=)", "Special Crane has been reset! (= 'ω`=))*exciting dance*((= 'ω`=)", "광고 리필 메세지"));
			Rows.Add( new MNP_LocalizeRow("L3498", "3498", "하트가 모두 회복되었다 냥 (= ω` =)", "Hearts are FULL again (= ω`=)...!", "하트 충전 완료"));
			Rows.Add( new MNP_LocalizeRow("L3500", "3500", "다시하기", "Retry", ""));
			Rows.Add( new MNP_LocalizeRow("L3501", "3501", "홈으로", "Home", ""));
			Rows.Add( new MNP_LocalizeRow("L3502", "3502", "[b]메달보너스[-]", "[b]Medal Bonus[-]", "결과창의 메달 보너스"));
			Rows.Add( new MNP_LocalizeRow("L3503", "3503", "[b]콤보보너스[-]", "[b]Combo Bonus[-]", "결과창의 콤보 보너스"));
			Rows.Add( new MNP_LocalizeRow("L3504", "3504", "성공", "Clear", "결과창의 미션들의 성공 안내"));
			Rows.Add( new MNP_LocalizeRow("L3505", "3505", "실패", "Fail", "결과창의 미션들의 실패 안내"));
			Rows.Add( new MNP_LocalizeRow("L3506", "3506", "코인 2배\n핫타임!!", "Coin\nHot Time!", ""));
			Rows.Add( new MNP_LocalizeRow("L3600", "3600", "게임 팁", "Game Tips", ""));
			Rows.Add( new MNP_LocalizeRow("L3601", "3601", "인계코드\n발행", "Issue\nTransfer Code", "옵션창의 코드 발급"));
			Rows.Add( new MNP_LocalizeRow("L3602", "3602", "인계코드\n입력", "Enter\nTransfer Code", "옵션창의 코드 입력"));
			Rows.Add( new MNP_LocalizeRow("L3603", "3603", "FAQ", "FAQ", ""));
			Rows.Add( new MNP_LocalizeRow("L3604", "3604", "설정", "Settings", ""));
			Rows.Add( new MNP_LocalizeRow("L3605", "3605", "기록", "Records", ""));
			Rows.Add( new MNP_LocalizeRow("L3606", "3606", "스코어", "Score", ""));
			Rows.Add( new MNP_LocalizeRow("L3607", "3607", "무료 크레인", "Free Crane", ""));
			Rows.Add( new MNP_LocalizeRow("L3608", "3608", "테마 구출, 보스 미션 완료 보상으로 \n보석30개를 보내드려요.\n메일함을 확인해주세요!", "Here are 30 Gem(s) as reward for completing the episode's rescue and boss missions.\nPlease check your Mailbox!", ""));
			Rows.Add( new MNP_LocalizeRow("L3609", "3609", "테마 구출, 보스 퇴치 보상", "Episode Rescue, Boss Defeat Reward", "메일함에서 사용"));
			Rows.Add( new MNP_LocalizeRow("L3610", "3610", "[FFE400FF]「[n]」 [-]을 입양하였습니다!", "[FFE400FF] 「[n]」 [-] adopted!", "메일함 고양이 획득"));
			Rows.Add( new MNP_LocalizeRow("L3611", "3611", "하트를 [n]개 받았습니다.", "[n] Heart(s) received!", ""));
			Rows.Add( new MNP_LocalizeRow("L3822", "3822", "매주 월요일 랭킹이 초기화 됩니다.", "The rankings reset every week on Mondays.", "랭킹 초기화 안내"));
			Rows.Add( new MNP_LocalizeRow("L3900", "3900", "생선을 선택하십시오", "Choose fish", "고양이 등급 튜토리얼"));
			Rows.Add( new MNP_LocalizeRow("L3901", "3901", "더 이상 ★성장시킬 수 없습니다.", "This cat can not be leveled up ★ anymore.", "고양이 등급 튜토리얼"));
			Rows.Add( new MNP_LocalizeRow("L3902", "3902", "고양이의 ★등급이 최대입니다. \n상한을 초과한 물고기는 반환되지 않습니다.", "This cat has already reached maximum rank ★ \nExcess fish will not be returned.", "고양이 등급 튜토리얼"));
			Rows.Add( new MNP_LocalizeRow("L3950", "3950", "스코어 [n]% 증가", "Scores [n]% increase", "고양이 스킬 설명"));
			Rows.Add( new MNP_LocalizeRow("L3951", "3951", "코인 [n]% 증가", "Coin [n]% increase", "고양이 스킬 설명"));
			Rows.Add( new MNP_LocalizeRow("L3952", "3952", "고양이 파워 [n]% 증가", "Cat Power [n]% increase", "고양이 스킬 설명"));
			Rows.Add( new MNP_LocalizeRow("L3953", "3953", "플레이 시간 [n]초 증가", "Play Time [n] increases in seconds", "고양이 스킬 설명"));
			Rows.Add( new MNP_LocalizeRow("L3954", "3954", "시작할 때 폭탄을 최대 [n]개 생성", "Generate up to [n] bombs at start", "고양이 스킬 설명"));
			Rows.Add( new MNP_LocalizeRow("L3955", "3955", "폭탄 생성에 필요한 블록을 [n]개 감소", "Reduce number of blocks required to generate a bomb by [n]", "고양이 스킬 설명"));
			Rows.Add( new MNP_LocalizeRow("L3956", "3956", "스킬 발동에 필요한 블록을 [n]개 감소", "Reduce number of blocks required to activate skill by [n]", "고양이 스킬 설명"));
			Rows.Add( new MNP_LocalizeRow("L3957", "3957", "스킬 발동시 랜덤한 폭탄이 최대 [n]개 생성", "Generate random bomb(s) up to [n] when skill is activated", "고양이 스킬 설명"));
			Rows.Add( new MNP_LocalizeRow("L3958", "3958", "스킬 발동시 빨강 폭탄이 최대 [n]개 생성", "Generate red bomb(s) up to [n] when skill is activated", "고양이 스킬 설명"));
			Rows.Add( new MNP_LocalizeRow("L3959", "3959", "스킬 발동시 파랑 폭탄이 최대 [n]개 생성", "Generate blue bomb(s) up to [n] when skill is activated", "고양이 스킬 설명"));
			Rows.Add( new MNP_LocalizeRow("L3960", "3960", "스킬 발동시 노랑 폭탄이 최대 [n]개 생성", "Generate yellow bomb(s) up to [n] when skill is activated", "고양이 스킬 설명"));
			Rows.Add( new MNP_LocalizeRow("L3961", "3961", "스킬 발동시 플레이 시간 [n]초 증가", "Increase play time by [n] sec(s) when skill is activated", "고양이 스킬 설명"));
			Rows.Add( new MNP_LocalizeRow("L3962", "3962", "고양이 스킬 포인트 획득이 [n] 쉬워집니다.", "Cat Skill point acquisition becomes easier by [n].", "고양이 스킬 설명"));
			Rows.Add( new MNP_LocalizeRow("L3963", "3963", "플레이어가 받는 경험치가 [n] 증가합니다.", "Exp earned by player increased by [n].", "고양이 스킬 설명"));
			Rows.Add( new MNP_LocalizeRow("L3964", "3964", "스킬을 사용하면 폭탄을 생성합니다. (최대 [n]개)", "Generate a bomb when skill is activated. (Up to [n])", "고양이 스킬 설명"));
			Rows.Add( new MNP_LocalizeRow("L3965", "3965", "스킬을 사용하면 [n] 초의 시간동안 콤보가 끊기지 않습니다.", "Combo will not break for [n] seconds when skill is activated.", "고양이 스킬 설명"));
			Rows.Add( new MNP_LocalizeRow("L3966", "3966", "스킬을 사용하면 피버 보인트가 [n] 늘어납니다.", "Fever point will be increased by [n] when skill is activated.", "고양이 스킬 설명"));
			Rows.Add( new MNP_LocalizeRow("L3967", "3967", "스킬을 사용하면 플레이 타임이 [n] 추가됩니다.", "Play time will br increased by [n] when skill is activated.", "고양이 스킬 설명"));
			Rows.Add( new MNP_LocalizeRow("L3968", "3968", "스킬을 사용하면 노랑 폭탄을 생성합니다. (최대 [n]개)", "Generate a yellow bomb when skill is activated. (Up to [n])", "고양이 스킬 설명"));
			Rows.Add( new MNP_LocalizeRow("L3969", "3969", "스킬을 사용하면 파랑 폭탄을 생성합니다. (최대 [n]개)", "Generate a blue bomb when skill is activated. (Up to [n])", "고양이 스킬 설명"));
			Rows.Add( new MNP_LocalizeRow("L3970", "3970", "스킬을 사용하면 빨강 폭탄을 생성합니다. (최대 [n]개)", "Generate a red bomb when skill is activated. (Up to [n])", "고양이 스킬 설명"));
			Rows.Add( new MNP_LocalizeRow("L3971", "3971", "스킬을 사용하면 검은 폭탄을 생성합니다. (최대 [n]개)", "Generate a black bomb when skill is activated. (Up to [n])", "고양이 스킬 설명"));
			Rows.Add( new MNP_LocalizeRow("L3972", "3972", "아주 조금", "Very little", "고양이 스킬의 강약 설명"));
			Rows.Add( new MNP_LocalizeRow("L3973", "3973", "약간", "Slightly", "고양이 스킬의 강약 설명"));
			Rows.Add( new MNP_LocalizeRow("L3974", "3974", "꽤", "Fairly", "고양이 스킬의 강약 설명"));
			Rows.Add( new MNP_LocalizeRow("L3975", "3975", "몹시", "Really", "고양이 스킬의 강약 설명"));
			Rows.Add( new MNP_LocalizeRow("L3976", "3976", "패시브", "Passive", "고양이 패시브 스킬"));
			Rows.Add( new MNP_LocalizeRow("L3977", "3977", "액티브", "Active", "고양이 엑티브 스킬"));
			Rows.Add( new MNP_LocalizeRow("L3978", "3978", "스코어", "Score", "준비화면 스킬 설명"));
			Rows.Add( new MNP_LocalizeRow("L3979", "3979", "코인", "Coin", "준비화면 스킬 설명"));
			Rows.Add( new MNP_LocalizeRow("L3980", "3980", "스킬파워", "Skill Power", "준비화면 스킬 설명"));
			Rows.Add( new MNP_LocalizeRow("L3981", "3981", "게임시간", "Game Time", "준비화면 스킬 설명"));
			Rows.Add( new MNP_LocalizeRow("L3982", "3982", "시작폭탄", "Start Bomb", "준비화면 스킬 설명"));
			Rows.Add( new MNP_LocalizeRow("L3983", "3983", "폭탄생성속도", "Bomb gen. speed", "준비화면 스킬 설명"));
			Rows.Add( new MNP_LocalizeRow("L3984", "3984", "스킬발동속도", "Skill act. speed", "준비화면 스킬 설명"));
			Rows.Add( new MNP_LocalizeRow("L3985", "3985", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L3986", "3986", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L3987", "3987", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L3988", "3988", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L3989", "3989", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L4100", "4100", "[n1] 『[n2]』을 받았습니다.", "You recieved [n2] [n1].", "ex) 고등어[n1] 3[n2] 을 받았습니다."));
			Rows.Add( new MNP_LocalizeRow("L4101", "4101", "게임에 도움이 되는 편리한 아이템 입니다.", "These are convenient items that will come in handy during game.", ""));
			Rows.Add( new MNP_LocalizeRow("L4102", "4102", "고등어", "Chub", ""));
			Rows.Add( new MNP_LocalizeRow("L4103", "4103", "참치", "Tuna", ""));
			Rows.Add( new MNP_LocalizeRow("L4104", "4104", "연어", "Salmon", ""));
			Rows.Add( new MNP_LocalizeRow("L4105", "4105", "보석은 무상 보석부터 소비됩니다.", "Free Gems will be used up first.", ""));
			Rows.Add( new MNP_LocalizeRow("L4106", "4106", "밋치리네코 보너스를 받았습니다!", "You have received Mitchiri Neko Bonus!", ""));
			Rows.Add( new MNP_LocalizeRow("L4107", "4107", "고양이의 레벨을 업그레이드 시키겠습니가?", "Would you like to level up the cat?", ""));
			Rows.Add( new MNP_LocalizeRow("L4108", "4108", "퍼즐 시간이나 고양이의 파워를 Up할 수 있습니다.", "You may power up the cat or increase the puzzle time.", ""));
			Rows.Add( new MNP_LocalizeRow("L4109", "4109", "패키지", "Package", ""));
			Rows.Add( new MNP_LocalizeRow("L4110", "4110", "파워", "Power", ""));
			Rows.Add( new MNP_LocalizeRow("L4111", "4111", "레벨이 오르면 파워가 크게 상승합니다.", "Level up to increase power.", ""));
			Rows.Add( new MNP_LocalizeRow("L4112", "4112", "구매 하시겠습니까?", "Would you like to make purchase?", ""));
			Rows.Add( new MNP_LocalizeRow("L4113", "4113", "유상 보석", "Paid Gem(s)", ""));
			Rows.Add( new MNP_LocalizeRow("L4114", "4114", "이미 보유한 고양이는 많은 생선으로 변경됩니다.", "If the cat is something that you already have, \nwe will pay you with a lot of fish to compensate.", ""));
			Rows.Add( new MNP_LocalizeRow("L4115", "4115", "생선을 선택하십시오", "Choose fish", ""));
			Rows.Add( new MNP_LocalizeRow("L4116", "4116", "고양이에게 생선을 주시겠습니까?", "Would you like to feed fish to your cat?", "물고기 먹이기 확인창"));
			Rows.Add( new MNP_LocalizeRow("L4117", "4117", "무상 보석", "Free Gem(s)", ""));
			Rows.Add( new MNP_LocalizeRow("L4118", "4118", "먹이주기", "Feeding", ""));
			Rows.Add( new MNP_LocalizeRow("L4119", "4119", "식사시간", "Meal Time", "고양이 생선 먹이기 버튼"));
			Rows.Add( new MNP_LocalizeRow("L4120", "4120", "파워 업그레이드를 진행할까요?", "Would you like to proceed with Power Upgrade?", ""));
			Rows.Add( new MNP_LocalizeRow("L4121", "4121", "더 이상 업그레이드 할 수 없습니다.", "Unable to upgrade anymore.", "패시브 레벨 MAX 경고"));
			Rows.Add( new MNP_LocalizeRow("L4122", "4122", "고양이 그룹 조건을 충족하지 못했어요.", "Did not meet the Cat Group criteria.", ""));
			Rows.Add( new MNP_LocalizeRow("L4123", "4123", "특정 상거래법 표기", "Notation of specific commercial transactions.", ""));
			Rows.Add( new MNP_LocalizeRow("L4124", "4124", "코인 [n]을 입수 했습니다.", "[n] Coin(s) received.", "코인 n개 획득(랭킹 보상)"));
			Rows.Add( new MNP_LocalizeRow("L4125", "4125", "1회 낚시", "Fish once", ""));
			Rows.Add( new MNP_LocalizeRow("L4126", "4126", "10회 낚시", "Fish 10 times", ""));
			Rows.Add( new MNP_LocalizeRow("L4127", "4127", "교환하기", "Exchange", ""));
			Rows.Add( new MNP_LocalizeRow("L4128", "4128", "고양이 갤러리", "Cat Gallery", "성장 타이틀"));
			Rows.Add( new MNP_LocalizeRow("L4129", "4129", "대표 고양이", "Representative Cat", "성장 화면"));
			Rows.Add( new MNP_LocalizeRow("L4130", "4130", "대표 고양이 설정", "Set a Representative Cat", "성장 화면"));
			Rows.Add( new MNP_LocalizeRow("L4131", "4131", "장착", "Set", "성장 화면"));
			Rows.Add( new MNP_LocalizeRow("L4132", "4132", "해제", "Remove", "성장 화면"));
			Rows.Add( new MNP_LocalizeRow("L4133", "4133", "고양이를 선택하세요.", "Choose a cat.", ""));
			Rows.Add( new MNP_LocalizeRow("L4134", "4134", "고양이 레벨업", "Cat Level Up", ""));
			Rows.Add( new MNP_LocalizeRow("L4135", "4135", "[ff8ab1]코인을　[ffef46][n][-]　사용해서\r\n고양이 레벨을 업그레이드 하겠습니까?[-]", "[Ff8ab1] Using [ffef46] [n] Coin(s) [-]\nWould you like to upgrade your cat? [-]", ""));
			Rows.Add( new MNP_LocalizeRow("L4136", "4136", "빙고!! 「[n]」을 획득했다 냥! （=´ω｀=）#밋치리네코팝 #밋치리네코 #고양이 #みっちりねこ", "Bingo!! You got 「[n]」!  （=´ω｀=）# MitchiriNekoPop # MitchiriNeko #Cat #みっちりねこ", "빙고 완료 후 SNS 공유 메세지"));
			Rows.Add( new MNP_LocalizeRow("L4137", "4137", "「[g]」 고양이를 사용해서", "Using [g] cat", "빙고 그룹 고양이 사용"));
			Rows.Add( new MNP_LocalizeRow("L4138", "4138", "업데이트 예정입니다.", "To be updated.", "빙고 제한 문구 "));
			Rows.Add( new MNP_LocalizeRow("L4139", "4139", "메일함에서 확인 할 수 있어요.", "Check your Maibox.", "빙고 보상창"));
			Rows.Add( new MNP_LocalizeRow("L4140", "4140", "밋치리빙고", "Mitchiri Bingo", "빙고 보상창"));
			Rows.Add( new MNP_LocalizeRow("L4141", "4141", "랭크 업그레이드", "Rank Upgrade", ""));
			Rows.Add( new MNP_LocalizeRow("L4142", "4142", "레벨 업그레이드", "Level Upgrade", "고양이 레벨업 화면"));
			Rows.Add( new MNP_LocalizeRow("L4143", "4143", "[ffef46] 고양이의 레벨을\n업그레이드 하시겠습니까?[-]\n\n[ff8ab1] ※ 레벨이 오르면 파워가 증가하여\n네코 구출 및 보스 퇴치가 쉬워집니다! [-]", "[ffef46] Would you like to level up your cat? [-]\n\n[ff8ab1] ※The higher the level, the more power you will have to rescue Neko friends and defeat the boss! [-] [-]", "고양이 레벨업 화면"));
			Rows.Add( new MNP_LocalizeRow("L4144", "4144", "Lv1 업", "Lv1 up", "고양이 레벨업 화면"));
			Rows.Add( new MNP_LocalizeRow("L4145", "4145", "Lv10 업", "Lv10 up", "고양이 레벨업 화면"));
			Rows.Add( new MNP_LocalizeRow("L4146", "4146", "고양이의 이름과 외모가 변경됩니다. \n그대로 진행하시겠습니까?", "The cat's name and appearance will change. \nWould you like to proceed?", "고양이 랭크업 확인"));
			Rows.Add( new MNP_LocalizeRow("L4147", "4147", "이전 테마에서 33개 이상의 별을 수집해야\n진행할 수 있습니다.", "You must collect more than 33 stars \nin the previous episode to proceed.", ""));
			Rows.Add( new MNP_LocalizeRow("L4148", "4148", "구입", "Purchase", ""));
			Rows.Add( new MNP_LocalizeRow("L4149", "4149", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L4150", "4150", "빙고 보너스", "Bingo Bonus", "서버 전송 메세지"));
			Rows.Add( new MNP_LocalizeRow("L4151", "4151", "패키지 구입 혜택입니다", "Here’s your package purchase benefits", "서버 전송 메세지"));
			Rows.Add( new MNP_LocalizeRow("L4152", "4152", "가을 대박 이벤트 선물!", "Meawesome Autumn Event!", "서버 전송 메세지"));
			Rows.Add( new MNP_LocalizeRow("L4153", "4153", "공식 Twitter 팔로워 2000명 돌파 기념!", "Thanks for 2,000 followers on Twitter!", "서버 전송 메세지"));
			Rows.Add( new MNP_LocalizeRow("L4154", "4154", "레벨 10 달성을 축하합니다!", "Congratulations on reaching level 10!", "서버 전송 메세지"));
			Rows.Add( new MNP_LocalizeRow("L4155", "4155", "페이스북 연동 감사합니다!", "Thank you for connecting to Facebook!", "서버 전송 메세지"));
			Rows.Add( new MNP_LocalizeRow("L4156", "4156", "튜토리얼 클리어 보너스!", "Tutorial Clear Bonus!", "서버 전송 메세지"));
			Rows.Add( new MNP_LocalizeRow("L4157", "4157", "사전 등록 보너스!", "Pre-registration Bonus!", "서버 전송 메세지"));
			Rows.Add( new MNP_LocalizeRow("L4158", "4158", "밋치리보너스", "Mitchiri Bonus", "서버 전송 메세지"));
			Rows.Add( new MNP_LocalizeRow("L4159", "4159", "키디랜드 상품 판매 기념!", "Commemoration for merch sale at Kiddy Land!", "서버 전송 메세지"));
			Rows.Add( new MNP_LocalizeRow("L4160", "4160", "LINE 초대 선물입니다!", "Thanks for sending out LINE invites!", "서버 전송 메세지"));
			Rows.Add( new MNP_LocalizeRow("L4161", "4161", "프리티켓을 입수했습니다!", "Free Ticket received!", "서버 전송 메세지"));
			Rows.Add( new MNP_LocalizeRow("L4162", "4162", "이벤트 보너스!", "Event Bonus!", "서버 전송 메세지"));
			Rows.Add( new MNP_LocalizeRow("L4163", "4163", "레벨 10 달성을 축하합니다!\n메일로 선물을 보냈어요!", "Congratulations on reaching level 10!\nWe've sent a gift to your mailbox!", ""));
			Rows.Add( new MNP_LocalizeRow("L4164", "4164", "페이스북 연동 감사합니다!\n메일로 선물을 보냈어요!", "Thank you for connecting to Facebook!\nWe've sent a gift to your mailbox!", ""));
			Rows.Add( new MNP_LocalizeRow("L4165", "4165", "푸시 메세지 보너스", "Push Message Bonus", "푸시 메세지 보상"));
			Rows.Add( new MNP_LocalizeRow("L4166", "4166", "하루에 한번, 밋치리네코를 알리고\n특별한 선물을 받을 수 있습니다.", "If you share MitchiriNeko, \nYou can get a special bonus\nonce a day.", ""));
			Rows.Add( new MNP_LocalizeRow("L4167", "4167", "[FFD800FF]※ 오늘 선물을 받을 수 있어요![-]", "[FFD800FF]※ You can get a special bonus today![-]", ""));
			Rows.Add( new MNP_LocalizeRow("L4168", "4168", "[FFDD00FF]★[-]3 출현!", "[FFDD00FF]★[-]3", ""));
			Rows.Add( new MNP_LocalizeRow("L4169", "4169", "[FFDD00FF]★[-]1 ~ [FFDD00FF]★[-]3 출현!", "[FFDD00FF]★[-]1 ~ [FFDD00FF]★[-]3", ""));
			Rows.Add( new MNP_LocalizeRow("L4170", "4170", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L4171", "4171", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L4172", "4172", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L4173", "4173", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L4174", "4174", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L4200", "4200", "업데이트 요청", "Update Request", "업데이트 요청(타이틀)"));
			Rows.Add( new MNP_LocalizeRow("L4201", "4201", "새로운 버전이 있습니다. \n업데이트를 하십시오.", "There is a new version available. \nPlease update.", "업데이트 요청(내용)"));
			Rows.Add( new MNP_LocalizeRow("L4202", "4202", "닉네임이 변경 되었습니다.", "Your nickname has been changed.", "닉네임 입력, 변경"));
			Rows.Add( new MNP_LocalizeRow("L4203", "4203", "※닉네임을 입력하십시오", "※Please enter a nickname", "닉네임 입력, 변경"));
			Rows.Add( new MNP_LocalizeRow("L4204", "4204", "※체크 표시를 하십시오", "※Please leave a check mark", "닉네임 입력, 변경"));
			Rows.Add( new MNP_LocalizeRow("L4205", "4205", "최대 8자까지", "Up to 8 characters", "닉네임 입력, 변경"));
			Rows.Add( new MNP_LocalizeRow("L4206", "4206", "닉네임이 올바르지 않습니다.", "Invalid Nickname", "닉네임 입력, 변경"));
			Rows.Add( new MNP_LocalizeRow("L4207", "4207", "※닉네임은 게임내에서 변경할 수 있습니다.", "※ You can change the nickname in the game.", ""));
			Rows.Add( new MNP_LocalizeRow("L4208", "4208", "이용약관에 동의합니다.", "I agree to the Terms and Conditions.", ""));
			Rows.Add( new MNP_LocalizeRow("L4209", "4209", "이용약관 확인", "Terms Confirm", ""));
			Rows.Add( new MNP_LocalizeRow("L4210", "4210", "알림", "Notification", ""));
			Rows.Add( new MNP_LocalizeRow("L4211", "4211", "붙잡은 횟수[n]회", "Captured [n] times", ""));
			Rows.Add( new MNP_LocalizeRow("L4212", "4212", "붙잡은 횟수", "Number of captures", ""));
			Rows.Add( new MNP_LocalizeRow("L4213", "4213", "닉네임 변경", "Change Nickname", ""));
			Rows.Add( new MNP_LocalizeRow("L4214", "4214", "보석은 메일로 보내드립니다\n화면을 탭하십시오", "Gem(s) will be sent via email.\nPlease tap the screen", ""));
			Rows.Add( new MNP_LocalizeRow("L4215", "4215", "레벨이 상승했습니다.", "Level Increased.", ""));
			Rows.Add( new MNP_LocalizeRow("L4216", "4216", "인계코드를 발행하였습니다.", "Transfer Code has been issued.", ""));
			Rows.Add( new MNP_LocalizeRow("L4217", "4217", "초대", "Invite", ""));
			Rows.Add( new MNP_LocalizeRow("L4218", "4218", "초대 방법", "How to invite", ""));
			Rows.Add( new MNP_LocalizeRow("L4219", "4219", "밋치리보너스", "Mitchiri Bonus", ""));
			Rows.Add( new MNP_LocalizeRow("L4220", "4220", "[b]기록[-]", "[b] Record [-]", ""));
			Rows.Add( new MNP_LocalizeRow("L4221", "4221", "[b]메달[-]", "[b] Medal [-]", ""));
			Rows.Add( new MNP_LocalizeRow("L4222", "4222", "[b]Collection[-]", "[b] Collection[-]", ""));
			Rows.Add( new MNP_LocalizeRow("L4223", "4223", "로그인 횟수", "Number of Login", ""));
			Rows.Add( new MNP_LocalizeRow("L4224", "4224", "퍼즐 횟수", "Number of Puzzles", ""));
			Rows.Add( new MNP_LocalizeRow("L4225", "4225", "최고점수", "Highest score", ""));
			Rows.Add( new MNP_LocalizeRow("L4226", "4226", "최고스테이지", "Highest Stage", ""));
			Rows.Add( new MNP_LocalizeRow("L4227", "4227", "최고콤보", "Max Combo", ""));
			Rows.Add( new MNP_LocalizeRow("L4228", "4228", "깨뜨린 블록 수", "Number of blocks popped", ""));
			Rows.Add( new MNP_LocalizeRow("L4229", "4229", "동영상 보고 무료 크레인!", "Watch an ad to play free crane!", ""));
			Rows.Add( new MNP_LocalizeRow("L4230", "4230", "무료\n[n]회", "FREE\n[n]times", ""));
			Rows.Add( new MNP_LocalizeRow("L4231", "4231", "★3\n고양이\n획득!", "★3\nCAT\nAcquired!", ""));
			Rows.Add( new MNP_LocalizeRow("L4232", "4232", "세일", "SALE", ""));
			Rows.Add( new MNP_LocalizeRow("L4233", "4233", "LINE 초대 감사합니다!\n메일로 선물을 보냈어요!", "Thank you for LINE invite!\nWe've sent a gift to your mailbox!", ""));
			Rows.Add( new MNP_LocalizeRow("L4234", "4234", "크레인에서 고양이를 획득했다 냥! (= 'ω`=) \n#밋치리네코 #고양이 #http://onelink.to/jb842h #https://www.facebook.com/mitchirinekopop.kr/", "I got a kitty from crane! (= 'ω`=) \n#MitchiriNeko #Cat #https://www.facebook.com/mitchirinekopop.kr/", ""));
			Rows.Add( new MNP_LocalizeRow("L4235", "4235", "내 도감을 자랑한다 냥!（=´ω｀=）\n#밋치리네코 #밋치리네코팝 #고양이 #http://onelink.to/jb842h  #https://www.facebook.com/mitchirinekopop.kr/", "Showing off my collection! Meow!（=´ω｀=）\n#MitchiriNeko #MitchiriNekoPop #Kitty https://www.facebook.com/mitchirinekopop.kr/", ""));
			Rows.Add( new MNP_LocalizeRow("L4236", "4236", "레벨보너스", "Level Bonus", ""));
			Rows.Add( new MNP_LocalizeRow("L4237", "4237", "레벨[n]이 되었다 냐~（=´ω｀=）\n#밋치리네코 #밋치리네코팝 #고양이 #https://mnpop.x-legend.co.jp/", "\"I became level [n] ~（=´ω｀=）\n#Mitchrineko #Mitchrinekopop #cat #https://mnpop.x-legend.co.jp/\"", ""));
			Rows.Add( new MNP_LocalizeRow("L4238", "4238", "밋치리네코팝 함께해요!", "Come play Mitchiri Neko Pop wirh me!", ""));
			Rows.Add( new MNP_LocalizeRow("L4239", "4239", "초대", "Invite", ""));
			Rows.Add( new MNP_LocalizeRow("L4240", "4240", "재시도", "Retry", ""));
			Rows.Add( new MNP_LocalizeRow("L4241", "4241", "결제 서비스를 초기화하고 있습니다.", "Initializing payment service.", ""));
			Rows.Add( new MNP_LocalizeRow("L4242", "4242", "잘못된 결제 프로세스입니다", "Invalid payment process", ""));
			Rows.Add( new MNP_LocalizeRow("L4243", "4243", "[ffef46] ★ 초대 방법 ★ [-]\n초대 화면에서 초대하고 싶은 앱을 탭하여 초대 할 수 있습니다.\n각 응용 프로그램의 자세한 내용은 아래와 같습니다.\n\n[ff8ab1] Facebook에서 초대 방법 [-]\n①Facebook의 아이콘을 눌러 연동\n②연동 후 '친구목록'에서 초대하고 싶은 친구를 선택\n③선택한 친구에게 초대메세지 전송\n\n[ff8ab1] Twitter에서 초대 방법 [-]\n① \"Twitter에서 초대\"를 터치\n② 트윗 화면이 표시되므로 트윗하기", "[Ffef46] ★ HOW TO INVITE ★  [-]\nYou can send out invites by choosing which app to use from the Invite page.\nPlease follow the steps below for each application!\n\n[ff8ab1] How to invite on Facebook [-]①Press Facebook icon and connect your account.  ②After connecting, choose a friend from the ‘Friends List’. ③Send invite message to selected friend.[ff8ab1] How to invite on Twitter [-]① Touch “Invite from Twitter” ② Tweet screen will be displayed, and all you have to do is tweet!", ""));
			Rows.Add( new MNP_LocalizeRow("L4244", "4244", "[n]회\n가능!", "[n] times available!", ""));
			Rows.Add( new MNP_LocalizeRow("L4245", "4245", "\"[ffe361]이 고양이는 이미 가지고 있습니다!\n교환하게 되면 생선으로 변경됩니다.\n\n교체 하시겠습니까? [-]\"", "\"[ffe361] You already own this cat!\nYou may exchange it to fish.\n\nWould you like to exchange? [-]\"", ""));
			Rows.Add( new MNP_LocalizeRow("L4246", "4246", "[ffa7d7][n][-][ffe361]고양이로 교환하시겠어요? [-]", "[ffa7d7][n][-][ffe361]Would you like to exchange with a cat? [-]", ""));
			Rows.Add( new MNP_LocalizeRow("L4247", "4247", "테마 클리어 보상을 받았습니다.", "Episode Clear Bonus", ""));
			Rows.Add( new MNP_LocalizeRow("L4248", "4248", "고양이들이 깨어나면서\n보석 [n]을 선물로 주었습니다! ", "As cats woke up, they gave you [n] Gem(s) as a present!", ""));
			Rows.Add( new MNP_LocalizeRow("L4249", "4249", "고양이들이 깨어나면서\n코인 [n]을 선물로 주었습니다! ", "As cats woke up, they gave you [n] Coin(s) as a present!", ""));
			Rows.Add( new MNP_LocalizeRow("L4250", "4250", "밋치리네코 팝! 함께 즐기자!（='ω`=） \n아래 URL에서 다운로드 해서 함께 플레이하자냥! http://onelink.to/jb842h \n공식HP는 이쪽!⇒https://www.facebook.com/mitchirinekopop.kr/", "Mitchiri Neko Pop! Mitchiri Neko pop! Come play with me! （='ω`=） Download it through URL below and let’s play together!  http://onelink.to/jb842h official website⇒ https://www.facebook.com/mitchirinekopop.kr/", ""));
			Rows.Add( new MNP_LocalizeRow("L4251", "4251", "구출 & 보스 스테이지가 어렵다면\n과일과 고양이를 업그레이드 해주세요!", "If you find rescue&boss stage too difficult, please upgrade fruit and cat!", ""));
			Rows.Add( new MNP_LocalizeRow("L4252", "4252", "고양이는 최대 3마리까지 함께 데려갈 수 있습니다.", "You can take up to 3 cats with you.", ""));
			Rows.Add( new MNP_LocalizeRow("L4253", "4253", "광고를 보면 개발자가 좋아합니다.\n（=´ω｀=）", "Developers will really appreciate it if you would watch ads...\n（=´ω｀=）", ""));
			Rows.Add( new MNP_LocalizeRow("L4254", "4254", "별을 33개이상 획득하면\n다음 테마로 넘어갈 수 있습니다.", "Get more than 33 stars to move on to the next episode.", ""));
			Rows.Add( new MNP_LocalizeRow("L4255", "4255", "주간 랭킹에 참여하는 것을 잊지마세요!", "Don’t forget to participate in the weekly rankings!", ""));
			Rows.Add( new MNP_LocalizeRow("L4256", "4256", "필드의 블록을 완전히 제거하면 \n2초의 시간 보너스가 주어집니다.", "Eliminate all blocks on the field to activate 2 seconds of bonus time!", ""));
			Rows.Add( new MNP_LocalizeRow("L4257", "4257", "미션 3개를 모두 클리어하면 하트가 \n소모되지 않습니다.", "Your heart won’t be consumed if you clear all 3 missions.", ""));
			Rows.Add( new MNP_LocalizeRow("L4258", "4258", "고양이를 많이 보유할수록, \n고양이의 레벨이 높을수록\n깨웠을때 더 많은 보상을 받습니다.", "The more cat you have and the higher their level are, the more reward you will get when you awake them.", ""));
			Rows.Add( new MNP_LocalizeRow("L4259", "4259", "페이스북 친구를 초대하면\n하트를 받을 수 있습니다.", "Get Hearts by inviting Facebook friends.", ""));
			Rows.Add( new MNP_LocalizeRow("L4260", "4260", "포스팅이 완료되었습니다. \n오늘의 공유하기 선물을 받았어요!", "Posting Completed!\nYou get today's share bonus!", ""));
			Rows.Add( new MNP_LocalizeRow("L4261", "4261", "#밋치리네코팝 #밋치리네코 #고양이 #http://onelink.to/jb842h #https://www.facebook.com/mitchirinekopop.kr/", "#MitchiriNekoPOP #MitchiriNeko #Cat #http://onelink.to/jb842h #https://www.facebook.com/mitchirinekopop.kr/", ""));
			Rows.Add( new MNP_LocalizeRow("L4262", "4262", "포스팅이 완료되었습니다.", "Post has completed successfully", ""));
			Rows.Add( new MNP_LocalizeRow("L4297", "4297", "★3리본", "★ 3 Ribbon", ""));
			Rows.Add( new MNP_LocalizeRow("L4298", "4298", "★3", "★3", ""));
			Rows.Add( new MNP_LocalizeRow("L4299", "4299", "★3ころっけ", "★ 3 Rolling", ""));
			Rows.Add( new MNP_LocalizeRow("L4300", "4300", "특별한 7일간의 로그인 보너스", "Special 7 days login bonus", ""));
			Rows.Add( new MNP_LocalizeRow("L4301", "4301", "탭 해서 게임 시작!", "Tab to start the game!", ""));
			Rows.Add( new MNP_LocalizeRow("L4302", "4302", "로그인 보너스", "Login Bonus", "로그인 보너스 타이틀"));
			Rows.Add( new MNP_LocalizeRow("L4303", "4303", "로그인 보너스는\n메일함으로 전송됩니다.", "Login bonus will be sent to your mailbox.", ""));
			Rows.Add( new MNP_LocalizeRow("L4304", "4304", "퍼즐", "Puzzle", ""));
			Rows.Add( new MNP_LocalizeRow("L4305", "4305", "Collection", "Collection", ""));
			Rows.Add( new MNP_LocalizeRow("L4306", "4306", "친구", "Friends", ""));
			Rows.Add( new MNP_LocalizeRow("L4307", "4307", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L4308", "4308", "옵션", "Options", ""));
			Rows.Add( new MNP_LocalizeRow("L4309", "4309", "잡은 횟수", "Number of times caught", ""));
			Rows.Add( new MNP_LocalizeRow("L4310", "4310", "레벨 업", "Level Up", ""));
			Rows.Add( new MNP_LocalizeRow("L4311", "4311", "고양이를 선택하십시오", "Choose a cat", ""));
			Rows.Add( new MNP_LocalizeRow("L4312", "4312", "레벨", "level", ""));
			Rows.Add( new MNP_LocalizeRow("L4313", "4313", "MAX", "MAX", ""));
			Rows.Add( new MNP_LocalizeRow("L4314", "4314", "친구초대", "Invite a friend", "하트충전창"));
			Rows.Add( new MNP_LocalizeRow("L4315", "4315", "재화내역", "Transaction History", ""));
			Rows.Add( new MNP_LocalizeRow("L4316", "4316", "(계정 연계를 위해 게임 센터 연동을 권장합니다.)", "(We recommend your account to be linked to Game Centerfor account transfers.)", ""));
			Rows.Add( new MNP_LocalizeRow("L4317", "4317", "항목 선택", "Selection", ""));
			Rows.Add( new MNP_LocalizeRow("L4318", "4318", "광고를 보면 \n밋치리네코 보너스를 받을 수 있습니다.", "Get Mitchiri Neko Bonus by watching Ads.", ""));
			Rows.Add( new MNP_LocalizeRow("L4319", "4319", "게임을 그만두시겠습니까?", "Are you sure you want to quit the game?", ""));
			Rows.Add( new MNP_LocalizeRow("L4320", "4320", "[n]마리를 낚았습니다.", "[n] fish obtained.", ""));
			Rows.Add( new MNP_LocalizeRow("L4321", "4321", "구입했습니다.", "Purchase has been made.", ""));
			Rows.Add( new MNP_LocalizeRow("L4322", "4322", "메일에 선물이 도착했습니다!\n화면을 탭하십시오", "You have a gift in your Mailbox!\nTap the screen", ""));
			Rows.Add( new MNP_LocalizeRow("L4323", "4323", "세트", "Set", ""));
			Rows.Add( new MNP_LocalizeRow("L4324", "4324", "돌아가기", "Go back", ""));
			Rows.Add( new MNP_LocalizeRow("L4325", "4325", "재시도", "Retry", ""));
			Rows.Add( new MNP_LocalizeRow("L4326", "4326", "광고를 보고 캐릭터 뽑기를 실행한다.", "Watch an ad to draw a character.", ""));
			Rows.Add( new MNP_LocalizeRow("L4327", "4327", "구매에 실패 했습니다.", "Failed to make purchase.", ""));
			Rows.Add( new MNP_LocalizeRow("L4328", "4328", "붙잡은고양이", "Cat caught", ""));
			Rows.Add( new MNP_LocalizeRow("L4329", "4329", "레벨\n보너스", "Level\nBonus", "결과화면"));
			Rows.Add( new MNP_LocalizeRow("L4330", "4330", "메달\n보너스", "Medal\nBonus", "결과화면"));
			Rows.Add( new MNP_LocalizeRow("L4331", "4331", "콤보\n보너스", "Combo\nBonus", "결과화면"));
			Rows.Add( new MNP_LocalizeRow("L4332", "4332", "아이템", "Item", "결과화면"));
			Rows.Add( new MNP_LocalizeRow("L4333", "4333", "주변랭킹", "Surrounding Ranking", "랭킹화면"));
			Rows.Add( new MNP_LocalizeRow("L4334", "4334", "점", "pts", "랭킹화면 스코어 표시 ex 300,000 점"));
			Rows.Add( new MNP_LocalizeRow("L4335", "4335", "자세한 사항은 배너를 탭!", "For more information, tap on the banner!", "공지사항"));
			Rows.Add( new MNP_LocalizeRow("L4336", "4336", "오늘 그만보기", "Enough for today", "공지사항"));
			Rows.Add( new MNP_LocalizeRow("L4337", "4337", "※로그인 보너스는 00:00 (15:00 GMT)에 재설정 됩니다.", "※ Login bonuses are reset at 00:00 (15:00 GMT).", ""));
			Rows.Add( new MNP_LocalizeRow("L4338", "4338", "퍼즐", "puzzle", "게임팁 화면 버튼"));
			Rows.Add( new MNP_LocalizeRow("L4339", "4339", "미션", "Mission", "게임팁 화면 버튼"));
			Rows.Add( new MNP_LocalizeRow("L4340", "4340", "도감", "Collection", "게임팁 화면 버튼"));
			Rows.Add( new MNP_LocalizeRow("L4341", "4341", "빙고", "Bingo", "게임팁 화면 버튼"));
			Rows.Add( new MNP_LocalizeRow("L4342", "4342", "업그레이드", "upgrade", "게임팁 화면 버튼"));
			Rows.Add( new MNP_LocalizeRow("L4343", "4343", "아이템 설명", "Item Description", "게임팁 화면 버튼"));
			Rows.Add( new MNP_LocalizeRow("L4344", "4344", "이상한 상자", "Strange box", "게임팁 화면 버튼"));
			Rows.Add( new MNP_LocalizeRow("L4345", "4345", "베스트 스코어", "Best score", "퍼즐 종료시 10초 더 플레이 화면"));
			Rows.Add( new MNP_LocalizeRow("L4346", "4346", "이번 게임 스코어", "The game scores", "퍼즐 종료시 10초 더 플레이 화면"));
			Rows.Add( new MNP_LocalizeRow("L4347", "4347", "보석 50개로 플레이타임을\n10초 늘릴 수 있어요", "Use 50 Gems to increase playtime by 10 seconds.", "퍼즐 종료시 10초 더 플레이 화면"));
			Rows.Add( new MNP_LocalizeRow("L4348", "4348", "보유 보석", "My Gem(s)", "퍼즐 종료시 10초 더 플레이 화면"));
			Rows.Add( new MNP_LocalizeRow("L4349", "4349", "계속해서 플레이 하시겠습니까?", "Would you like to continue playing?", "퍼즐 종료시 10초 더 플레이 화면"));
			Rows.Add( new MNP_LocalizeRow("L4350", "4350", "베스트 스코어를 거의 따라잡았어요!", "almost caught up with the best score!", "퍼즐 종료시 10초 더 플레이 화면"));
			Rows.Add( new MNP_LocalizeRow("L4351", "4351", "효과음 설정", "SFX Settings", "설정 화면 레이블, 버튼"));
			Rows.Add( new MNP_LocalizeRow("L4352", "4352", "BGM 설정", "BGM Settings", "설정 화면 레이블, 버튼"));
			Rows.Add( new MNP_LocalizeRow("L4353", "4353", "하트 회복 Push 알림", "Heart Recharge Push Notifications", "설정 화면 레이블, 버튼"));
			Rows.Add( new MNP_LocalizeRow("L4354", "4354", "무료 크레인  Push 알림", "Free Crane Push Notifications", "설정 화면 레이블, 버튼"));
			Rows.Add( new MNP_LocalizeRow("L4355", "4355", "공지사항 Push 알림", "Notifications Push notifications", "설정 화면 레이블, 버튼"));
			Rows.Add( new MNP_LocalizeRow("L4356", "4356", "퍼즐 가이드 설정", "Puzzle Guide Setting", "설정 화면 레이블, 버튼"));
			Rows.Add( new MNP_LocalizeRow("L4357", "4357", "전문가", "Expert", "설정 화면 레이블, 버튼"));
			Rows.Add( new MNP_LocalizeRow("L4358", "4358", "초보자", "Beginner", "설정 화면 레이블, 버튼"));
			Rows.Add( new MNP_LocalizeRow("L4359", "4359", "하니추천", "Honey Recommended", "스페셜 패키지"));
			Rows.Add( new MNP_LocalizeRow("L4360", "4360", "사랑가득패키지", "Love Filled package", "스페셜 패키지"));
			Rows.Add( new MNP_LocalizeRow("L4361", "4361", "1회 한정", "One time \nonly", "스페셜 패키지"));
			Rows.Add( new MNP_LocalizeRow("L4362", "4362", "하트 x20\n연어 x10", "Heart x20\nSalmon x10", "스페셜 패키지"));
			Rows.Add( new MNP_LocalizeRow("L4363", "4363", "[n]회\n가능!", "Free!", ""));
			Rows.Add( new MNP_LocalizeRow("L4364", "4364", "고양이들을 \n깨워주세요!\nZzz..", "Please Wake up\nthe 'cats'!\nZzz..", "고양이 유리병 안내 멘트"));
			Rows.Add( new MNP_LocalizeRow("L5050", "5050", "", "", "일일 미션"));
			Rows.Add( new MNP_LocalizeRow("L5051", "5051", "", "", "일일 미션"));
			Rows.Add( new MNP_LocalizeRow("L5052", "5052", "", "", "일일 미션"));
			Rows.Add( new MNP_LocalizeRow("L5053", "5053", "", "", "일일 미션"));
			Rows.Add( new MNP_LocalizeRow("L5054", "5054", "", "", "일일 미션"));
			Rows.Add( new MNP_LocalizeRow("L5055", "5055", "", "", "일일 미션"));
			Rows.Add( new MNP_LocalizeRow("L5056", "5056", "밋치리네코 보너스를 [n]회 받는다.", "Got [n] Mitchiri bonus.", "일일 미션"));
			Rows.Add( new MNP_LocalizeRow("L5057", "5057", "", "", "일일 미션"));
			Rows.Add( new MNP_LocalizeRow("L5058", "5058", "", "", "일일 미션"));
			Rows.Add( new MNP_LocalizeRow("L5059", "5059", "", "", "일일 미션"));
			Rows.Add( new MNP_LocalizeRow("L5060", "5060", "모든 미션을 완료", "Complete all missions", "일일 미션"));
			Rows.Add( new MNP_LocalizeRow("L5061", "5061", "게임을 [n]회 플레이", "Played game [n] times.", "일일 미션"));
			Rows.Add( new MNP_LocalizeRow("L5062", "5062", "프리크레인 [n]회 도전", "Played Free Crane [n] times.", "일일 미션"));
			Rows.Add( new MNP_LocalizeRow("L5150", "5150", "", "", "주간 미션"));
			Rows.Add( new MNP_LocalizeRow("L5151", "5151", "", "", "주간 미션"));
			Rows.Add( new MNP_LocalizeRow("L5152", "5152", "", "", "주간 미션"));
			Rows.Add( new MNP_LocalizeRow("L5153", "5153", "", "", "주간 미션"));
			Rows.Add( new MNP_LocalizeRow("L5154", "5154", "", "", "주간 미션"));
			Rows.Add( new MNP_LocalizeRow("L5155", "5155", "", "", "주간 미션"));
			Rows.Add( new MNP_LocalizeRow("L5156", "5156", "밋치리네코 보너스를 [n]회 받는다.", "Got [n] Mitchiri bonus.", "주간 미션"));
			Rows.Add( new MNP_LocalizeRow("L5157", "5157", "", "", "주간 미션"));
			Rows.Add( new MNP_LocalizeRow("L5158", "5158", "", "", "주간 미션"));
			Rows.Add( new MNP_LocalizeRow("L5159", "5159", "", "", "주간 미션"));
			Rows.Add( new MNP_LocalizeRow("L5160", "5160", "모든 미션을 완료", "Complete all missions", "주간 미션"));
			Rows.Add( new MNP_LocalizeRow("L5161", "5161", "게임을 [n]회 플레이", "Played game [n] times.", "주간 미션"));
			Rows.Add( new MNP_LocalizeRow("L5162", "5162", "무료 캐릭터 뽑기 [n]회 도전", "Played Free Crane [n] times.", "주간 미션"));
			Rows.Add( new MNP_LocalizeRow("L5300", "5300", "퍼즐을 [n]회 플레이 한다.", "Played puzzles [n] times.", "빙고 미션"));
			Rows.Add( new MNP_LocalizeRow("L5301", "5301", "퍼즐에서 스코어를 합계 [n] 달성한다.", "Scored total of [n] points in puzzle", "빙고 미션"));
			Rows.Add( new MNP_LocalizeRow("L5302", "5302", "퍼즐 한번으로 스코어 [n]을 달성한다.", "Scored [n] points in a single puzzle", "빙고 미션"));
			Rows.Add( new MNP_LocalizeRow("L5303", "5303", "퍼즐에서 코인을 합계 [n]을 획득한다.", "Earned [n] Coin(s) total in puzzle", "빙고 미션"));
			Rows.Add( new MNP_LocalizeRow("L5304", "5304", "퍼즐 한번으로 코인 [n]개를 획득한다.", "Earned [n] Coin(s) in a single puzzle", "빙고 미션"));
			Rows.Add( new MNP_LocalizeRow("L5305", "5305", "퍼즐에서 생선을 합계 [n]마리를 획득한다.", "Earned [n] Fish total in puzzle", "빙고 미션"));
			Rows.Add( new MNP_LocalizeRow("L5306", "5306", "퍼즐 한번으로 생선 [n]마리를 획득한다.", "Earned [n] Fish total in a single puzzle", "빙고 미션"));
			Rows.Add( new MNP_LocalizeRow("L5307", "5307", "퍼즐에서 빨강 블록을 합계 [n]개 깨뜨린다.", "Break total of [n] red blocks in puzzle.", "빙고 미션"));
			Rows.Add( new MNP_LocalizeRow("L5308", "5308", "퍼즐에서 파랑 블록을 합계 [n]개 깨뜨린다.", "Break total of [n] blue blocks in puzzle.", "빙고 미션"));
			Rows.Add( new MNP_LocalizeRow("L5309", "5309", "퍼즐에서 노랑 블록을 합계 [n]개 깨뜨린다.", "Break total of [n] yellow blocks in puzzle.", "빙고 미션"));
			Rows.Add( new MNP_LocalizeRow("L5310", "5310", "퍼즐 한번으로 빨강 블록을 [n]개 깨뜨린다.", "Break total of [n] red blocks in a single puzzle.", "빙고 미션"));
			Rows.Add( new MNP_LocalizeRow("L5311", "5311", "퍼즐 한번으로 파랑 블록을 [n]개 깨뜨린다.", "Break total of [n] blue blocks in a single puzzle.", "빙고 미션"));
			Rows.Add( new MNP_LocalizeRow("L5312", "5312", "퍼즐 한번으로 노랑 블록을 [n]개 깨뜨린다.", "Break total of [n] yellow blocks in a single puzzle.", "빙고 미션"));
			Rows.Add( new MNP_LocalizeRow("L5313", "5313", "퍼즐에서 블록을 합계 [n]개 깨뜨린다.", "Break [n] blocks total in puzzle", "빙고 미션"));
			Rows.Add( new MNP_LocalizeRow("L5314", "5314", "퍼즐에서 폭탄을 합계 [n]개 터뜨린다.", "Blow off [n] bombs total in puzzle", "빙고 미션"));
			Rows.Add( new MNP_LocalizeRow("L5315", "5315", "퍼즐 한번으로 빨강 폭탄을 합계 [n]개 터뜨린다.", "Blow off [n] red bombs total in a single puzzle", "빙고 미션"));
			Rows.Add( new MNP_LocalizeRow("L5316", "5316", "퍼즐 한번으로 파랑 폭탄을 합계 [n]개 터뜨린다.", "Blow off [n] blue bombs total in a single puzzle", "빙고 미션"));
			Rows.Add( new MNP_LocalizeRow("L5317", "5317", "퍼즐 한번으로 노랑 폭탄을 합계 [n]개 터뜨린다.", "Blow off [n] yellow bombs total in a single puzzle", "빙고 미션"));
			Rows.Add( new MNP_LocalizeRow("L5318", "5318", "퍼즐에서 콤보를 합계 [n]회 달성한다.", "Achieve total of [n] combos in puzzle", "빙고 미션"));
			Rows.Add( new MNP_LocalizeRow("L5319", "5319", "퍼즐 한번으로 최대 콤보 [n]을 달성한다.", "Achieve [n] combos in a single puzzle", "빙고 미션"));
			Rows.Add( new MNP_LocalizeRow("L5320", "5320", "부스트 아이템을 사용해서 퍼즐을 [n]회 플레이 한다.", "Play puzzle [n] times using boost item.", "빙고 미션"));
			Rows.Add( new MNP_LocalizeRow("L5321", "5321", "블록 3개 동시 깨뜨리기를 합계 [n]회 한다.", "Break 3 blocks at once [n] times in total", "빙고 미션"));
			Rows.Add( new MNP_LocalizeRow("L5322", "5322", "블록 4개 동시 깨뜨리기를 합계 [n]회 한다.", "Break 4 blocks at once [n] times in total", "빙고 미션"));
			Rows.Add( new MNP_LocalizeRow("L5323", "5323", "퍼즐 한번으로 블록 3개를 동시에 [n]회 깨뜨린다.", "Break 3 blocks at once [n] times in a single puzzle", "빙고 미션"));
			Rows.Add( new MNP_LocalizeRow("L5324", "5324", "퍼즐 한번으로 블록 4개를 동시에 [n]회 깨뜨린다.", "Break 4 blocks at once [n] times in a single puzzle", "빙고 미션"));
			Rows.Add( new MNP_LocalizeRow("L5325", "5325", "퍼즐에서 스페셜 어택을 합계 [n]번 발동한다.", "Trigger Special Attack [n] times in puzzles", "빙고 미션"));
			Rows.Add( new MNP_LocalizeRow("L5326", "5326", "퍼즐 한번에서 스페셜 어택을 [n]번 발동한다.", "Trigger Special Attack [n] times in a single puzzle", "빙고 미션"));
			Rows.Add( new MNP_LocalizeRow("L5327", "5327", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L5328", "5328", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L5329", "5329", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L5330", "5330", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L5331", "5331", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L5332", "5332", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L5333", "5333", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L5334", "5334", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L5335", "5335", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L5336", "5336", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L5337", "5337", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L5338", "5338", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L5339", "5339", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L5340", "5340", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L5341", "5341", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L5342", "5342", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L5343", "5343", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L5344", "5344", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L5345", "5345", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L5346", "5346", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L5347", "5347", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L5348", "5348", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L5349", "5349", "코인을 합계 [n]만큼 사용한다.", "Use [n] Coins in total", ""));
			Rows.Add( new MNP_LocalizeRow("L5350", "5350", "보석을 사용한다.", "Use any amount of Gem.", ""));
			Rows.Add( new MNP_LocalizeRow("L5351", "5351", "스페셜 크레인을 [n]회 실행한다.", "Play Special Crane [n] times.", ""));
			Rows.Add( new MNP_LocalizeRow("L5352", "5352", "낚시를 [n]회 실행한다.", "Fish [n] times", ""));
			Rows.Add( new MNP_LocalizeRow("L5353", "5353", "일일 미션을 모두 클리어 한다.", "Clear all Daily Missions", ""));
			Rows.Add( new MNP_LocalizeRow("L5354", "5354", "주간 미션을 모두 클리어 한다.", "Clear all Weekly Missions", ""));
			Rows.Add( new MNP_LocalizeRow("L5355", "5355", "브론즈 메달 [n]개를 모은다", "Collect [n] bronze medals", ""));
			Rows.Add( new MNP_LocalizeRow("L5356", "5356", "실버 메달 [n]개를 모은다", "Collect [n] silver medals", ""));
			Rows.Add( new MNP_LocalizeRow("L5357", "5357", "골드 메달 [n]개를 모은다.", "Collect [n] gold medals", ""));
			Rows.Add( new MNP_LocalizeRow("L5358", "5358", "밋치리 보너스를 [n]회 받는다.", "Get Mitchiri Bonus [n] times", ""));
			Rows.Add( new MNP_LocalizeRow("L5359", "5359", "출석체크를 [n]회 받는다.", "Receive Login bonus [n] times", ""));
			Rows.Add( new MNP_LocalizeRow("L5360", "5360", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L5361", "5361", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L5362", "5362", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L5363", "5363", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L5364", "5364", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L5365", "5365", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L5366", "5366", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6000", "6000", "스테이지를 도전해 보세요.", "Try challenging a stage.", "스테이지 미션"));
			Rows.Add( new MNP_LocalizeRow("L6001", "6001", "목표 점수 [n]점을 달성하세요.", "Reach [n] score.", "스테이지 미션"));
			Rows.Add( new MNP_LocalizeRow("L6002", "6002", "최대 콤보 [n]을 달성하세요.", "Reach max combo [n]", "스테이지 미션"));
			Rows.Add( new MNP_LocalizeRow("L6003", "6003", "블록을 [n]개 깨뜨리세요.", "Break [n] blocks", "스테이지 미션"));
			Rows.Add( new MNP_LocalizeRow("L6004", "6004", "스페셜 어택을 [n]번 해주세요.", "Use Special Attack [n] times", "스테이지 미션"));
			Rows.Add( new MNP_LocalizeRow("L6005", "6005", "한번의 퍼즐에서 폭탄 블록을 [n]회 사용하세요.", "Use bomb blocks [n] times in a single puzzle", "스테이지 미션"));
			Rows.Add( new MNP_LocalizeRow("L6006", "6006", "폭탄 블록을 [n]번 터뜨리세요.", "Blast bomb block [n] times.", "스테이지 미션"));
			Rows.Add( new MNP_LocalizeRow("L6007", "6007", "코인을 [n]개 획득하세요", "Earn [n] Coins", "스테이지 미션"));
			Rows.Add( new MNP_LocalizeRow("L6008", "6008", "생선을  [n]마리 획득하세요.", "Get [n] Fish", "스테이지 미션"));
			Rows.Add( new MNP_LocalizeRow("L6009", "6009", "고양이를 구출해주세요!", "Rescue a cat!", "스테이지 미션"));
			Rows.Add( new MNP_LocalizeRow("L6010", "6010", "나쁜 보스 고양이를 물리쳐주세요!", "Defeat the bad boss cat!", "스테이지 미션"));
			Rows.Add( new MNP_LocalizeRow("L6011", "6011", "모든 쿠키를 깨뜨려 주세요.", "Break all cookies.", "스테이지 미션"));
			Rows.Add( new MNP_LocalizeRow("L6012", "6012", "부스트 아이템을 사용해보세요.", "Try using a boost item.", "스테이지 미션"));
			Rows.Add( new MNP_LocalizeRow("L6013", "6013", "필드 완전 클리어(Perfect)를 [n]번 달성하세요.", "Completely clear (Perfect) the field [n] times.", "스테이지 미션"));
			Rows.Add( new MNP_LocalizeRow("L6014", "6014", "모든 바위를 깨뜨려 주세요.", "Break all the rocks", "스테이지 미션"));
			Rows.Add( new MNP_LocalizeRow("L6015", "6015", "블록 3개를 동시에 [n]번 깨뜨리세요.", "Break 3 blocks at once [n] times", "스테이지 미션"));
			Rows.Add( new MNP_LocalizeRow("L6016", "6016", "블록 4개를 동시에 [n]번 깨뜨리세요.", "Break 4 blocks at once [n] times", "스테이지 미션"));
			Rows.Add( new MNP_LocalizeRow("L6017", "6017", "Miss를 [n]번 미만으로 기록하세요.", "Get less than [n] Miss", "스테이지 미션"));
			Rows.Add( new MNP_LocalizeRow("L6018", "6018", "필드 클리어(Great)를 [n]번 달성하세요.", "Clear (Great) the field [n] times.", "스테이지 미션"));
			Rows.Add( new MNP_LocalizeRow("L6019", "6019", "[n]초 이내에 고양이를 구출하세요!", "Rescue cat within [n] seconds", "스테이지 미션"));
			Rows.Add( new MNP_LocalizeRow("L6020", "6020", "[n]초 이내에 나쁜 보스 고양이를 물리치세요!", "Defeat the bad boss cat in [n] seconds", "스테이지 미션"));
			Rows.Add( new MNP_LocalizeRow("L6021", "6021", "생선 [n]마리를 맛있게 튀겨주세요.", "Fry [n] fish!", "스테이지 미션"));
			Rows.Add( new MNP_LocalizeRow("L6022", "6022", "칼라풀냥 [n]마리를 집으로 보내주세요!", "Send [n] Colorful-mews home!", "스테이지 미션"));
			Rows.Add( new MNP_LocalizeRow("L6023", "6023", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6024", "6024", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6025", "6025", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6026", "6026", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6027", "6027", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6028", "6028", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6029", "6029", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6030", "6030", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6031", "6031", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6032", "6032", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6033", "6033", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6034", "6034", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6035", "6035", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6036", "6036", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6037", "6037", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6038", "6038", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6039", "6039", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6040", "6040", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6500", "6500", "\"꼬리가 보이는\" 고양이와 함께하세요.", "Take a cats with tails with you.", "고양이 그룹"));
			Rows.Add( new MNP_LocalizeRow("L6501", "6501", "\"좌우 귀의 색이 다른\" 고양이와 함께하세요.", "Take a cat with different ear colors with you.", "고양이 그룹"));
			Rows.Add( new MNP_LocalizeRow("L6502", "6502", "\"리본\" 고양이와 함께하세요.", "Take Ribbon with you.", "고양이 그룹"));
			Rows.Add( new MNP_LocalizeRow("L6503", "6503", "\"날개가 있는\" 고양이와 함께하세요.", "Take a winged cat with you", "고양이 그룹"));
			Rows.Add( new MNP_LocalizeRow("L6504", "6504", "\"폭탄을 만들어 주는\" 고양이와 함께하세요.", "Take a bomb-making cat with you", "고양이 그룹"));
			Rows.Add( new MNP_LocalizeRow("L6505", "6505", "\"우유병에 들어간 고양이\"와 함께하세요.", "Take a cat in the milk bottle with you", "고양이 그룹"));
			Rows.Add( new MNP_LocalizeRow("L6506", "6506", "미정", "Undefined", "고양이 그룹"));
			Rows.Add( new MNP_LocalizeRow("L6507", "6507", "\"신체에 호랑이 무늬가 있는\" 고양이와 함께하세요.", "Take a cat that looks like a tiger with you", "고양이 그룹"));
			Rows.Add( new MNP_LocalizeRow("L6508", "6508", "\"코인을 더 얻을 수 있게 해주는\" 고양이와 함께하세요.", "Take a cat that will let you earn more Coins with you", "고양이 그룹"));
			Rows.Add( new MNP_LocalizeRow("L6509", "6509", "\"플레이 타임을 늘려주는\" 고양이와 함께하세요.", "Take a cat that will extend your play time with you", "고양이 그룹"));
			Rows.Add( new MNP_LocalizeRow("L6510", "6510", "\"양손에 뭔가를 가지고 있는\" 고양이와 함께하세요.", "Take a cat that is holding something in both hands with you", "고양이 그룹"));
			Rows.Add( new MNP_LocalizeRow("L6511", "6511", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6512", "6512", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6513", "6513", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6514", "6514", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6515", "6515", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6516", "6516", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6517", "6517", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6518", "6518", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6519", "6519", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6520", "6520", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6600", "6600", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6601", "6601", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6602", "6602", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6603", "6603", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6604", "6604", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6605", "6605", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6606", "6606", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6607", "6607", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6608", "6608", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6609", "6609", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6610", "6610", "", "", ""));
			Rows.Add( new MNP_LocalizeRow("L6611", "6611", "", "", ""));
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
		public MNP_LocalizeRow GetRow(rowIds in_RowID)
		{
			MNP_LocalizeRow ret = null;
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
		public MNP_LocalizeRow GetRow(string in_RowString)
		{
			MNP_LocalizeRow ret = null;
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
