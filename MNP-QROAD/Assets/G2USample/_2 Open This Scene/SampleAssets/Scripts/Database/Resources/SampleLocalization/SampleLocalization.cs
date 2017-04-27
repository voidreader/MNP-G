//----------------------------------------------
//    Google2u: Google Doc Unity integration
//         Copyright © 2015 Litteratus
//
//        This file has been auto-generated
//              Do not manually edit
//----------------------------------------------

namespace G2USample
{
    using UnityEngine;
    using Google2u;

	[System.Serializable]
	public class SampleLocalizationRow : IGoogle2uRow
	{
		public string _en;
		public string _es;
		public string _fr;
		public SampleLocalizationRow(string __GOOGLEFU_ID, string __en, string __es, string __fr) 
		{
			_en = __en.Trim();
			_es = __es.Trim();
			_fr = __fr.Trim();
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
					ret = _en.ToString();
					break;
				case 1:
					ret = _es.ToString();
					break;
				case 2:
					ret = _fr.ToString();
					break;
			}

			return ret;
		}

		public string GetStringData( string colID )
		{
			var ret = System.String.Empty;
			switch( colID.ToLower() )
			{
				case "en":
					ret = _en.ToString();
					break;
				case "es":
					ret = _es.ToString();
					break;
				case "fr":
					ret = _fr.ToString();
					break;
			}

			return ret;
		}
		public override string ToString()
		{
			string ret = System.String.Empty;
			ret += "{" + "en" + " : " + _en.ToString() + "} ";
			ret += "{" + "es" + " : " + _es.ToString() + "} ";
			ret += "{" + "fr" + " : " + _fr.ToString() + "} ";
			return ret;
		}
	}
	public sealed class SampleLocalization : IGoogle2uDB
	{
		public enum rowIds {
			STR_MINE_DMG_0, STR_MINE_DMG_1, STR_BUZZ_DMG_0, STR_BUZZ_DMG_1, STR_MINE_DST_0, STR_MINE_DST_1, STR_BUZZ_DST_0, STR_BUZZ_DST_1, STR_NAME, STR_LEVEL, STR_HEALTH, STR_WEAPON, STR_SPAWN, STR_ENGLISH, STR_SPANISH, STR_FRENCH, STR_SELFDESTRUCT, STR_LASER
		};
		public string [] rowNames = {
			"STR_MINE_DMG_0", "STR_MINE_DMG_1", "STR_BUZZ_DMG_0", "STR_BUZZ_DMG_1", "STR_MINE_DST_0", "STR_MINE_DST_1", "STR_BUZZ_DST_0", "STR_BUZZ_DST_1", "STR_NAME", "STR_LEVEL", "STR_HEALTH", "STR_WEAPON", "STR_SPAWN", "STR_ENGLISH", "STR_SPANISH", "STR_FRENCH", "STR_SELFDESTRUCT", "STR_LASER"
		};
		public System.Collections.Generic.List<SampleLocalizationRow> Rows = new System.Collections.Generic.List<SampleLocalizationRow>();

		public static SampleLocalization Instance
		{
			get { return NestedSampleLocalization.instance; }
		}

		private class NestedSampleLocalization
		{
			static NestedSampleLocalization() { }
			internal static readonly SampleLocalization instance = new SampleLocalization();
		}

		private SampleLocalization()
		{
			Rows.Add( new SampleLocalizationRow("STR_MINE_DMG_0", "Ouch", "¡Ay", "Aie"));
			Rows.Add( new SampleLocalizationRow("STR_MINE_DMG_1", "Stop That", "Stop That", "Arrête Ça"));
			Rows.Add( new SampleLocalizationRow("STR_BUZZ_DMG_0", "No", "No", "Aucun"));
			Rows.Add( new SampleLocalizationRow("STR_BUZZ_DMG_1", "Wait", "Esperar", "Attendez"));
			Rows.Add( new SampleLocalizationRow("STR_MINE_DST_0", "Destroy!", "Destruye!", "Destroy!"));
			Rows.Add( new SampleLocalizationRow("STR_MINE_DST_1", "Self Destruct!", "Auto Destrucción!", "Self Destruct!"));
			Rows.Add( new SampleLocalizationRow("STR_BUZZ_DST_0", "Failing", "Defecto", "Défaut"));
			Rows.Add( new SampleLocalizationRow("STR_BUZZ_DST_1", "Does Not Compute", "No computa", "Ne calcule pas"));
			Rows.Add( new SampleLocalizationRow("STR_NAME", "Name", "Nombre", "Nom"));
			Rows.Add( new SampleLocalizationRow("STR_LEVEL", "Level", "Nivel", "Niveau"));
			Rows.Add( new SampleLocalizationRow("STR_HEALTH", "Health", "Salud", "Santé"));
			Rows.Add( new SampleLocalizationRow("STR_WEAPON", "Weapon", "Arma", "Arme"));
			Rows.Add( new SampleLocalizationRow("STR_SPAWN", "Spawn", "Desovar", "Frai"));
			Rows.Add( new SampleLocalizationRow("STR_ENGLISH", "English", "Inglés", "Anglais"));
			Rows.Add( new SampleLocalizationRow("STR_SPANISH", "Spanish", "Español", "Espagnol"));
			Rows.Add( new SampleLocalizationRow("STR_FRENCH", "French", "Francés", "Français"));
			Rows.Add( new SampleLocalizationRow("STR_SELFDESTRUCT", "Self-Destruct", "Auto Destrucción", "Self-Destruct"));
			Rows.Add( new SampleLocalizationRow("STR_LASER", "Laser", "Láser", "Laser"));
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
		public SampleLocalizationRow GetRow(rowIds in_RowID)
		{
			SampleLocalizationRow ret = null;
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
		public SampleLocalizationRow GetRow(string in_RowString)
		{
			SampleLocalizationRow ret = null;
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
