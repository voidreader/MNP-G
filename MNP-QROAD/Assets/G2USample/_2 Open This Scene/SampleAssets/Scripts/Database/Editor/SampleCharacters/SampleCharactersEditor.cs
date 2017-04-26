using UnityEngine;
using UnityEditor;

namespace Google2u
{
	[CustomEditor(typeof(SampleCharacters))]
	public class SampleCharactersEditor : Editor
	{
		public int Index = 0;
		public override void OnInspectorGUI ()
		{
			SampleCharacters s = target as SampleCharacters;
			SampleCharactersRow r = s.Rows[ Index ];

			EditorGUILayout.BeginHorizontal();
			if ( GUILayout.Button("<<") )
			{
				Index = 0;
			}
			if ( GUILayout.Button("<") )
			{
				Index -= 1;
				if ( Index < 0 )
					Index = s.Rows.Count - 1;
			}
			if ( GUILayout.Button(">") )
			{
				Index += 1;
				if ( Index >= s.Rows.Count )
					Index = 0;
			}
			if ( GUILayout.Button(">>") )
			{
				Index = s.Rows.Count - 1;
			}

			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "ID", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.LabelField( s.rowNames[ Index ] );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "_Name", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.TextField( r._Name );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "_Prefab", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.TextField( r._Prefab );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "_Rotation", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.TextField( r._Rotation );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "_Level", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.IntField( r._Level );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "_CanFly", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.Toggle( System.Convert.ToBoolean( r._CanFly ) );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "_Weapon", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.TextField( r._Weapon );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "_Dialog", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.TextField( r._Dialog );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "_Health", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.FloatField( (float)r._Health );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "_Speed", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.FloatField( (float)r._Speed );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "_Scale", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.FloatField( (float)r._Scale );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.Vector3Field( "_Offset", r._Offset );
			EditorGUILayout.EndHorizontal();

		}
	}
}
