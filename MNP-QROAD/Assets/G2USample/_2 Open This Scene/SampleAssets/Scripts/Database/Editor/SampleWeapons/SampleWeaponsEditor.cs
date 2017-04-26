using UnityEngine;
using UnityEditor;

namespace Google2u
{
	[CustomEditor(typeof(SampleWeapons))]
	public class SampleWeaponsEditor : Editor
	{
		public int Index = 0;
		public override void OnInspectorGUI ()
		{
			SampleWeapons s = target as SampleWeapons;
			SampleWeaponsRow r = s.Rows[ Index ];

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
			GUILayout.Label( "_Damage", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.FloatField( (float)r._Damage );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "_Speed", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.FloatField( (float)r._Speed );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "_Cooldown", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.FloatField( (float)r._Cooldown );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "_AccuracyInDegrees", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.IntField( r._AccuracyInDegrees );
			}
			EditorGUILayout.EndHorizontal();

		}
	}
}
