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
	public class SampleCharactersRow : IGoogle2uRow
	{
		public string _Name;
		public string _Prefab;
		public string _Rotation;
		public int _Level;
		public bool _CanFly;
		public string _Weapon;
		public string _Dialog;
		public float _Health;
		public float _Speed;
		public float _Scale;
		public Vector3 _Offset;
		public SampleCharactersRow(string __GOOGLEFU_ID, string __Name, string __Prefab, string __Rotation, string __Level, string __CanFly, string __Weapon, string __Dialog, string __Health, string __Speed, string __Scale, string __Offset) 
		{
			_Name = __Name.Trim();
			_Prefab = __Prefab.Trim();
			_Rotation = __Rotation.Trim();
			{
			int res;
				if(int.TryParse(__Level, out res))
					_Level = res;
				else
					Debug.LogError("Failed To Convert _Level string: "+ __Level +" to int");
			}
			{
			bool res;
				if(bool.TryParse(__CanFly, out res))
					_CanFly = res;
				else
					Debug.LogError("Failed To Convert _CanFly string: "+ __CanFly +" to bool");
			}
			_Weapon = __Weapon.Trim();
			_Dialog = __Dialog.Trim();
			{
			float res;
				if(float.TryParse(__Health, out res))
					_Health = res;
				else
					Debug.LogError("Failed To Convert _Health string: "+ __Health +" to float");
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
				if(float.TryParse(__Scale, out res))
					_Scale = res;
				else
					Debug.LogError("Failed To Convert _Scale string: "+ __Scale +" to float");
			}
			{
				string [] splitpath = __Offset.Split(",".ToCharArray(),System.StringSplitOptions.RemoveEmptyEntries);
				if(splitpath.Length != 3)
					Debug.LogError("Incorrect number of parameters for Vector3 in " + __Offset );
				float []results = new float[splitpath.Length];
				for(int i = 0; i < 3; i++)
				{
					float res;
					if(float.TryParse(splitpath[i], out res))
					{
						results[i] = res;
					}
					else 
					{
						Debug.LogError("Error parsing " + __Offset + " Component: " + splitpath[i] + " parameter " + i + " of variable _Offset");
					}
				}
				_Offset.x = results[0];
				_Offset.y = results[1];
				_Offset.z = results[2];
			}
		}

		public int Length { get { return 11; } }

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
					ret = _Prefab.ToString();
					break;
				case 2:
					ret = _Rotation.ToString();
					break;
				case 3:
					ret = _Level.ToString();
					break;
				case 4:
					ret = _CanFly.ToString();
					break;
				case 5:
					ret = _Weapon.ToString();
					break;
				case 6:
					ret = _Dialog.ToString();
					break;
				case 7:
					ret = _Health.ToString();
					break;
				case 8:
					ret = _Speed.ToString();
					break;
				case 9:
					ret = _Scale.ToString();
					break;
				case 10:
					ret = _Offset.ToString();
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
				case "Prefab":
					ret = _Prefab.ToString();
					break;
				case "Rotation":
					ret = _Rotation.ToString();
					break;
				case "Level":
					ret = _Level.ToString();
					break;
				case "CanFly":
					ret = _CanFly.ToString();
					break;
				case "Weapon":
					ret = _Weapon.ToString();
					break;
				case "Dialog":
					ret = _Dialog.ToString();
					break;
				case "Health":
					ret = _Health.ToString();
					break;
				case "Speed":
					ret = _Speed.ToString();
					break;
				case "Scale":
					ret = _Scale.ToString();
					break;
				case "Offset":
					ret = _Offset.ToString();
					break;
			}

			return ret;
		}
		public override string ToString()
		{
			string ret = System.String.Empty;
			ret += "{" + "Name" + " : " + _Name.ToString() + "} ";
			ret += "{" + "Prefab" + " : " + _Prefab.ToString() + "} ";
			ret += "{" + "Rotation" + " : " + _Rotation.ToString() + "} ";
			ret += "{" + "Level" + " : " + _Level.ToString() + "} ";
			ret += "{" + "CanFly" + " : " + _CanFly.ToString() + "} ";
			ret += "{" + "Weapon" + " : " + _Weapon.ToString() + "} ";
			ret += "{" + "Dialog" + " : " + _Dialog.ToString() + "} ";
			ret += "{" + "Health" + " : " + _Health.ToString() + "} ";
			ret += "{" + "Speed" + " : " + _Speed.ToString() + "} ";
			ret += "{" + "Scale" + " : " + _Scale.ToString() + "} ";
			ret += "{" + "Offset" + " : " + _Offset.ToString() + "} ";
			return ret;
		}
	}
	public class SampleCharacters :  Google2uComponentBase, IGoogle2uDB
	{
		public enum rowIds {
			AI_MINEBOT_LIGHT, AI_MINEBOT_HEAVY, AI_BUZZERBOT_LIGHT, AI_BUZZERBOT_HEAVY
		};
		public string [] rowNames = {
			"AI_MINEBOT_LIGHT", "AI_MINEBOT_HEAVY", "AI_BUZZERBOT_LIGHT", "AI_BUZZERBOT_HEAVY"
		};
		public System.Collections.Generic.List<SampleCharactersRow> Rows = new System.Collections.Generic.List<SampleCharactersRow>();
		public override void AddRowGeneric (System.Collections.Generic.List<string> input)
		{
			Rows.Add(new SampleCharactersRow(input[0],input[1],input[2],input[3],input[4],input[5],input[6],input[7],input[8],input[9],input[10],input[11]));
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
		public SampleCharactersRow GetRow(rowIds in_RowID)
		{
			SampleCharactersRow ret = null;
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
		public SampleCharactersRow GetRow(string in_RowString)
		{
			SampleCharactersRow ret = null;
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
