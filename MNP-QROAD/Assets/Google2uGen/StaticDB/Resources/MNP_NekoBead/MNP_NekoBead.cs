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
	public class MNP_NekoBeadRow : IGoogle2uRow
	{
		public int _BeadMin;
		public int _BeadStack;
		public int _ChangeBead;
		public int _Bead_Max;
		public MNP_NekoBeadRow(string __id, string __BeadMin, string __BeadStack, string __ChangeBead, string __Bead_Max) 
		{
			{
			int res;
				if(int.TryParse(__BeadMin, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_BeadMin = res;
				else
					Debug.LogError("Failed To Convert _BeadMin string: "+ __BeadMin +" to int");
			}
			{
			int res;
				if(int.TryParse(__BeadStack, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_BeadStack = res;
				else
					Debug.LogError("Failed To Convert _BeadStack string: "+ __BeadStack +" to int");
			}
			{
			int res;
				if(int.TryParse(__ChangeBead, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_ChangeBead = res;
				else
					Debug.LogError("Failed To Convert _ChangeBead string: "+ __ChangeBead +" to int");
			}
			{
			int res;
				if(int.TryParse(__Bead_Max, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
					_Bead_Max = res;
				else
					Debug.LogError("Failed To Convert _Bead_Max string: "+ __Bead_Max +" to int");
			}
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
					ret = _BeadMin.ToString();
					break;
				case 1:
					ret = _BeadStack.ToString();
					break;
				case 2:
					ret = _ChangeBead.ToString();
					break;
				case 3:
					ret = _Bead_Max.ToString();
					break;
			}

			return ret;
		}

		public string GetStringData( string colID )
		{
			var ret = System.String.Empty;
			switch( colID )
			{
				case "BeadMin":
					ret = _BeadMin.ToString();
					break;
				case "BeadStack":
					ret = _BeadStack.ToString();
					break;
				case "ChangeBead":
					ret = _ChangeBead.ToString();
					break;
				case "Bead_Max":
					ret = _Bead_Max.ToString();
					break;
			}

			return ret;
		}
		public override string ToString()
		{
			string ret = System.String.Empty;
			ret += "{" + "BeadMin" + " : " + _BeadMin.ToString() + "} ";
			ret += "{" + "BeadStack" + " : " + _BeadStack.ToString() + "} ";
			ret += "{" + "ChangeBead" + " : " + _ChangeBead.ToString() + "} ";
			ret += "{" + "Bead_Max" + " : " + _Bead_Max.ToString() + "} ";
			return ret;
		}
	}
	public sealed class MNP_NekoBead : IGoogle2uDB
	{
		public enum rowIds {
			grade1, grade2, grade3, grade4, grade5
		};
		public string [] rowNames = {
			"grade1", "grade2", "grade3", "grade4", "grade5"
		};
		public System.Collections.Generic.List<MNP_NekoBeadRow> Rows = new System.Collections.Generic.List<MNP_NekoBeadRow>();

		public static MNP_NekoBead Instance
		{
			get { return NestedMNP_NekoBead.instance; }
		}

		private class NestedMNP_NekoBead
		{
			static NestedMNP_NekoBead() { }
			internal static readonly MNP_NekoBead instance = new MNP_NekoBead();
		}

		private MNP_NekoBead()
		{
			Rows.Add( new MNP_NekoBeadRow("grade1", "0", "49", "0", "0"));
			Rows.Add( new MNP_NekoBeadRow("grade2", "50", "149", "0", "50"));
			Rows.Add( new MNP_NekoBeadRow("grade3", "150", "299", "0", "100"));
			Rows.Add( new MNP_NekoBeadRow("grade4", "300", "599", "0", "150"));
			Rows.Add( new MNP_NekoBeadRow("grade5", "600", "599", "0", "300"));
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
		public MNP_NekoBeadRow GetRow(rowIds in_RowID)
		{
			MNP_NekoBeadRow ret = null;
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
		public MNP_NekoBeadRow GetRow(string in_RowString)
		{
			MNP_NekoBeadRow ret = null;
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
