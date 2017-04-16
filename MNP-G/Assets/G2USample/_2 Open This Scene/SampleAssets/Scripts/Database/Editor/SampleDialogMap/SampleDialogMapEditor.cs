using UnityEngine;
using UnityEditor;

namespace Google2u
{
	[CustomEditor(typeof(SampleDialogMap))]
	public class SampleDialogMapEditor : Editor
	{
		public int Index = 0;
		public int _OnDamaged_Index = 0;
		public int _OnDestroyed_Index = 0;
		public override void OnInspectorGUI ()
		{
			SampleDialogMap s = target as SampleDialogMap;
			SampleDialogMapRow r = s.Rows[ Index ];

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
			if ( r._OnDamaged.Count == 0 )
			{
			    GUILayout.Label( "_OnDamaged", GUILayout.Width( 150.0f ) );
			    {
			    	EditorGUILayout.LabelField( "Empty Array" );
			    }
			}
			else
			{
			    GUILayout.Label( "_OnDamaged", GUILayout.Width( 130.0f ) );
			    if ( _OnDamaged_Index >= r._OnDamaged.Count )
				    _OnDamaged_Index = 0;
			    if ( GUILayout.Button("<", GUILayout.Width( 18.0f )) )
			    {
			    	_OnDamaged_Index -= 1;
			    	if ( _OnDamaged_Index < 0 )
			    		_OnDamaged_Index = r._OnDamaged.Count - 1;
			    }
			    EditorGUILayout.LabelField(_OnDamaged_Index.ToString(), GUILayout.Width( 15.0f ));
			    if ( GUILayout.Button(">", GUILayout.Width( 18.0f )) )
			    {
			    	_OnDamaged_Index += 1;
			    	if ( _OnDamaged_Index >= r._OnDamaged.Count )
		        		_OnDamaged_Index = 0;
				}
				EditorGUILayout.TextField( r._OnDamaged[_OnDamaged_Index] );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			if ( r._OnDestroyed.Count == 0 )
			{
			    GUILayout.Label( "_OnDestroyed", GUILayout.Width( 150.0f ) );
			    {
			    	EditorGUILayout.LabelField( "Empty Array" );
			    }
			}
			else
			{
			    GUILayout.Label( "_OnDestroyed", GUILayout.Width( 130.0f ) );
			    if ( _OnDestroyed_Index >= r._OnDestroyed.Count )
				    _OnDestroyed_Index = 0;
			    if ( GUILayout.Button("<", GUILayout.Width( 18.0f )) )
			    {
			    	_OnDestroyed_Index -= 1;
			    	if ( _OnDestroyed_Index < 0 )
			    		_OnDestroyed_Index = r._OnDestroyed.Count - 1;
			    }
			    EditorGUILayout.LabelField(_OnDestroyed_Index.ToString(), GUILayout.Width( 15.0f ));
			    if ( GUILayout.Button(">", GUILayout.Width( 18.0f )) )
			    {
			    	_OnDestroyed_Index += 1;
			    	if ( _OnDestroyed_Index >= r._OnDestroyed.Count )
		        		_OnDestroyed_Index = 0;
				}
				EditorGUILayout.TextField( r._OnDestroyed[_OnDestroyed_Index] );
			}
			EditorGUILayout.EndHorizontal();

		}
	}
}
