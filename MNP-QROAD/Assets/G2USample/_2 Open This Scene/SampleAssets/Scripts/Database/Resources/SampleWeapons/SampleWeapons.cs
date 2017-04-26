//----------------------------------------------
//    Google2u: Google Doc Unity integration
//         Copyright © 2015 Litteratus
//
//        This file has been auto-generated
//              Do not manually edit
//----------------------------------------------

using UnityEngine;

namespace Google2u
{
	[System.Serializable]
	public class SampleWeaponsRow : IGoogle2uRow
	{
		public string _Name;
		public float _Damage;
		public float _Speed;
		public float _Cooldown;
		public int _AccuracyInDegrees;
		public SampleWeaponsRow(string __GOOGLEFU_ID, string __Name, string __Damage, string __Speed, string __Cooldown, string __AccuracyInDegrees) 
		{
			_Name = __Name.Trim();
			{
			float res;
				if(float.TryParse(__Damage, out res))
					_Damage = res;
				else
					Debug.LogError("Failed To Convert _Damage string: "+ __Damage +" to float");
			}
			{
			float res;
				if(float.TryParse(__Speed, out res))
					_Speed = res;
				else
					Debug.LogError("Failed To Convert _Speed string: "+ __Speed +" to float");
			}
			{
			float res;
				if(float.TryParse(__Cooldown, out res))
					_Cooldown = res;
				else
					Debug.LogError("Failed To Convert _Cooldown string: "+ __Cooldown +" to float");
			}
			{
			int res;
				if(int.TryParse(__AccuracyInDegrees, out res))
					_AccuracyInDegrees = res;
				else
					Debug.LogError("Failed To Convert _AccuracyInDegrees string: "+ __AccuracyInDegrees +" to int");
			}
		}

		public int Length { get { return 5; } }

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
					ret = _Name.ToString();
					break;
				case 1:
					ret = _Damage.ToString();
					break;
				case 2:
					ret = _Speed.ToString();
					break;
				case 3:
					ret = _Cooldown.ToString();
					break;
				case 4:
					ret = _AccuracyInDegrees.ToString();
					break;
			}

			return ret;
		}

		public string GetStringData( string colID )
		{
			var ret = System.String.Empty;
			switch( colID.ToLower() )
			{
				case "Name":
					ret = _Name.ToString();
					break;
				case "Damage":
					ret = _Damage.ToString();
					break;
				case "Speed":
					ret = _Speed.ToString();
					break;
				case "Cooldown":
					ret = _Cooldown.ToString();
					break;
				case "AccuracyInDegrees":
					ret = _AccuracyInDegrees.ToString();
					break;
			}

			return ret;
		}
		public override string ToString()
		{
			string ret = System.String.Empty;
			ret += "{" + "Name" + " : " + _Name.ToString() + "} ";
			ret += "{" + "Damage" + " : " + _Damage.ToString() + "} ";
			ret += "{" + "Speed" + " : " + _Speed.ToString() + "} ";
			ret += "{" + "Cooldown" + " : " + _Cooldown.ToString() + "} ";
			ret += "{" + "AccuracyInDegrees" + " : " + _AccuracyInDegrees.ToString() + "} ";
			return ret;
		}
	}
	public class SampleWeapons :  Google2uComponentBase, IGoogle2uDB
	{
		public enum rowIds {
			WP_SELFDESTRUCT, WP_LASER
		};
		public string [] rowNames = {
			"WP_SELFDESTRUCT", "WP_LASER"
		};
		public System.Collections.Generic.List<SampleWeaponsRow> Rows = new System.Collections.Generic.List<SampleWeaponsRow>();
		public override void AddRowGeneric (System.Collections.Generic.List<string> input)
		{
			Rows.Add(new SampleWeaponsRow(input[0],input[1],input[2],input[3],input[4],input[5]));
		}
		public override void Clear ()
		{
			Rows.Clear();
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
		public SampleWeaponsRow GetRow(rowIds in_RowID)
		{
			SampleWeaponsRow ret = null;
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
		public SampleWeaponsRow GetRow(string in_RowString)
		{
			SampleWeaponsRow ret = null;
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
