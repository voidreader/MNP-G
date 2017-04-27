#define DISABLED


////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Deploy
// @author Stanislav Osipov (Stan's Assets) 
// @support support@stansassets.com
//
////////////////////////////////////////////////////////////////////////////////


#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Reflection;
using System;


namespace SA.IOSDeploy {

	[CustomEditor(typeof(ISD_Settings))]
	public class SettingsEditor : UnityEditor.Editor {
		
		private static string newFramework 			= string.Empty;
		private static string NewLibrary 				= string.Empty;
		private static string NewLinkerFlag 			= string.Empty;
		private static string NewCompilerFlag 	= string.Empty;
		private static string NewPlistValueName = string.Empty;
		private static string NewLanguage 			= string.Empty;
		private static string NewValueName = string.Empty;


		private static GUIContent SdkVersion   = new GUIContent("Plugin Version [?]", "This is Plugin version.  If you have problems or compliments please include this so we know exactly what version to look out for.");

		public override void OnInspectorGUI () {
			GUI.changed = false;
			EditorGUILayout.LabelField("IOS Deploy Settings", EditorStyles.boldLabel);
			EditorGUILayout.Space();

			#if DISABLED
			GUI.enabled = false;
			#endif

			BuildSettings ();
			EditorGUILayout.Space ();
			Frameworks();
			EditorGUILayout.Space();
			Library ();
			EditorGUILayout.Space();
			LinkerFlags();
			EditorGUILayout.Space();
			CompilerFlags();
			EditorGUILayout.Space();
			PlistValues ();
			EditorGUILayout.Space();
			LanguageValues();
			EditorGUILayout.Space();
			AboutGUI();

			if(GUI.changed) {
				DirtyEditor();
			}
		}

		public static void BuildSettings(){
			ISD_Settings.Instance.IsBuildSettingsOpen = EditorGUILayout.Foldout(ISD_Settings.Instance.IsBuildSettingsOpen, "Build Settins");

			if (ISD_Settings.Instance.IsBuildSettingsOpen) {
				
				EditorGUI.indentLevel++;
				EditorGUILayout.BeginVertical (GUI.skin.box);
				EditorGUILayout.BeginHorizontal();
				//EditorGUILayout.LabelField("Bitcode");


				//bool t = ISD_Settings.Instance.enableBitCode;
				ISD_Settings.Instance.enableBitCode = EditorGUILayout.Toggle ("ENABLE_BITCODE" ,ISD_Settings.Instance.enableBitCode);
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.BeginHorizontal();
				ISD_Settings.Instance.enableTestability = EditorGUILayout.Toggle ("ENABLE_TESTABILITY" ,ISD_Settings.Instance.enableTestability);
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.BeginHorizontal();
				ISD_Settings.Instance.generateProfilingCode = EditorGUILayout.Toggle ("GENERATE_PROFILING_CODE" ,ISD_Settings.Instance.generateProfilingCode);	

				EditorGUILayout.EndHorizontal();
				EditorGUILayout.EndVertical ();
									

//				Dictionary<string, object> dict = ISD_Settings.Instance.buildSettings;
//				Debug.Log ("cound, " + dict.Count);
//				for (int i = 0; i < dict.Count; i++) {
//					Debug.Log ("Pair");
//					EditorGUILayout.BeginVertical (GUI.skin.box);
//					EditorGUILayout.BeginHorizontal();
//					EditorGUILayout.LabelField("Bitcode");
//					if (dict.ContainsKey("ENABLE_BITCODE")) {
//						//EditorGUILayout.Toggle window = (EditorGUILayout.Toggle)EditorWindow.GetWindow (typeof(EditorGUILayoutToggle), true, "My Empty Window");
//
//						
//
//					}
//
//					EditorGUILayout.EndHorizontal();
//					EditorGUILayout.EndVertical ();
//				}

				EditorGUI.indentLevel--;
			}

		}

		public static void Frameworks() {
			ISD_Settings.Instance.IsfwSettingOpen = EditorGUILayout.Foldout(ISD_Settings.Instance.IsfwSettingOpen, "Frameworks");

			if(ISD_Settings.Instance.IsfwSettingOpen) {
				if (ISD_Settings.Instance.Frameworks.Count == 0) {

					EditorGUILayout.HelpBox("No Frameworks added", MessageType.None);
				}

				EditorGUI.indentLevel++; {	
					foreach(Framework framework in ISD_Settings.Instance.Frameworks) {
						EditorGUILayout.BeginVertical (GUI.skin.box);

						EditorGUILayout.BeginHorizontal();
						framework.IsOpen = EditorGUILayout.Foldout(framework.IsOpen, framework.Name);
						
						if(framework.IsOptional) {
							EditorGUILayout.LabelField("(Optional)");
						}
						bool ItemWasRemoved = DrawSortingButtons((object) framework, ISD_Settings.Instance.Frameworks);
						if(ItemWasRemoved) {
							return;
						}

						EditorGUILayout.EndHorizontal();
						if(framework.IsOpen) {
							EditorGUI.indentLevel++; {
								EditorGUILayout.BeginHorizontal();
								EditorGUILayout.LabelField("Optional");
								framework.IsOptional = EditorGUILayout.Toggle (framework.IsOptional);
								EditorGUILayout.EndHorizontal();
							}EditorGUI.indentLevel--;
						}
						EditorGUILayout.EndVertical ();
					}
				} EditorGUI.indentLevel--;
				EditorGUILayout.Space();

				EditorGUILayout.BeginVertical (GUI.skin.box);
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("Add New Framework", GUILayout.Width(120));
				newFramework = EditorGUILayout.TextField(newFramework);
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.EndVertical ();

				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.Space();

				if(GUILayout.Button("Add",  GUILayout.Width(100))) {
					if(!ISD_Settings.Instance.ContainsFreamworkWithName(newFramework) && newFramework.Length > 0) {
						Framework f =  new Framework();
						f.Name = newFramework;
						ISD_Settings.Instance.Frameworks.Add(f);
						newFramework = string.Empty;
					}				
				}
				EditorGUILayout.EndHorizontal();
			}
		}

		public static void Library () {
			ISD_Settings.Instance.IsLibSettingOpen = EditorGUILayout.Foldout(ISD_Settings.Instance.IsLibSettingOpen, "Libraries");

			if(ISD_Settings.Instance.IsLibSettingOpen){
				if (ISD_Settings.Instance.Libraries.Count == 0) {
					EditorGUILayout.HelpBox("No Libraries added", MessageType.None);
				}

				EditorGUI.indentLevel++; {
					foreach(Lib lib in ISD_Settings.Instance.Libraries) {	
						EditorGUILayout.BeginVertical (GUI.skin.box);
						
						EditorGUILayout.BeginHorizontal();
						lib.IsOpen = EditorGUILayout.Foldout(lib.IsOpen, lib.Name);
						if(lib.IsOptional) {
							EditorGUILayout.LabelField("(Optional)");
						}
			
						bool ItemWasRemoved = DrawSortingButtons((object) lib, ISD_Settings.Instance.Libraries);
						if(ItemWasRemoved) {
							return;
						}					
						EditorGUILayout.EndHorizontal();
						if(lib.IsOpen) {						
							EditorGUI.indentLevel++; {							
								EditorGUILayout.BeginHorizontal();
								EditorGUILayout.LabelField("Optional");
								lib.IsOptional = EditorGUILayout.Toggle (lib.IsOptional);
								EditorGUILayout.EndHorizontal();						
							}EditorGUI.indentLevel--;
						}
						EditorGUILayout.EndVertical ();					
					}
				}EditorGUI.indentLevel--;
				
				EditorGUILayout.Space();

				EditorGUILayout.BeginVertical (GUI.skin.box);
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("Add New Library", GUILayout.Width(120));
				NewLibrary = EditorGUILayout.TextField(NewLibrary);
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.EndVertical ();

				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.Space();
				if(GUILayout.Button("Add",  GUILayout.Width(100))) {
					if(!ISD_Settings.Instance.ContainsLibWithName(NewLibrary) && NewLibrary.Length > 0 ) {
						Lib lib = new Lib();
						lib.Name = NewLibrary;
						ISD_Settings.Instance.Libraries.Add(lib);
						NewLibrary = string.Empty;
					}
				}
				EditorGUILayout.EndHorizontal();
			}
		}


		public static void LinkerFlags() {
			ISD_Settings.Instance.IslinkerSettingOpne = EditorGUILayout.Foldout(ISD_Settings.Instance.IslinkerSettingOpne, "Linker Flags");
			
			if(ISD_Settings.Instance.IslinkerSettingOpne) {
				if (ISD_Settings.Instance.linkFlags.Count == 0) {				
					EditorGUILayout.HelpBox("No Linker Flags added", MessageType.None);
				}

				foreach(string flasg in ISD_Settings.Instance.linkFlags) {			
					EditorGUILayout.BeginVertical (GUI.skin.box);				
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.SelectableLabel(flasg, GUILayout.Height(18));
					EditorGUILayout.Space();
					
					bool pressed  = GUILayout.Button("x",  EditorStyles.miniButton, GUILayout.Width(20));
					if(pressed) {
						ISD_Settings.Instance.linkFlags.Remove(flasg);
						return;
					}
					EditorGUILayout.EndHorizontal();
					
					EditorGUILayout.EndVertical ();				
				}

				EditorGUILayout.Space();
				EditorGUILayout.BeginHorizontal();
				
				EditorGUILayout.LabelField("Add New Flag");
				NewLinkerFlag = EditorGUILayout.TextField(NewLinkerFlag, GUILayout.Width(200));
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.Space();
				if(GUILayout.Button("Add",  GUILayout.Width(100))) {
					if(!ISD_Settings.Instance.linkFlags.Contains(NewLinkerFlag) && NewLinkerFlag.Length > 0) {
						ISD_Settings.Instance.linkFlags.Add(NewLinkerFlag);
						NewLinkerFlag = string.Empty;
					}
				}
				EditorGUILayout.EndHorizontal();
			}
		}

		public static void CompilerFlags() {
			ISD_Settings.Instance.IscompilerSettingsOpen = EditorGUILayout.Foldout(ISD_Settings.Instance.IscompilerSettingsOpen, "Compiler Flags");
			
			if(ISD_Settings.Instance.IscompilerSettingsOpen) {
				if (ISD_Settings.Instance.compileFlags.Count == 0) {
					EditorGUILayout.HelpBox("No Linker Flags added", MessageType.None);
				}

				foreach(string flasg in ISD_Settings.Instance.compileFlags) {
					EditorGUILayout.BeginVertical (GUI.skin.box);
					
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.SelectableLabel(flasg, GUILayout.Height(18));
					
					EditorGUILayout.Space();
					
					bool pressed  = GUILayout.Button("x",  EditorStyles.miniButton, GUILayout.Width(20));
					if(pressed) {
						ISD_Settings.Instance.compileFlags.Remove(flasg);
						return;
					}
					EditorGUILayout.EndHorizontal();
					EditorGUILayout.EndVertical ();
				}

				EditorGUILayout.Space();
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("Add New Flag");
				NewCompilerFlag = EditorGUILayout.TextField(NewCompilerFlag, GUILayout.Width(200));
				EditorGUILayout.EndHorizontal();
				
				EditorGUILayout.BeginHorizontal();
				
				EditorGUILayout.Space();
				
				if(GUILayout.Button("Add",  GUILayout.Width(100))) {
					if(!ISD_Settings.Instance.compileFlags.Contains(NewCompilerFlag) && NewCompilerFlag.Length > 0) {
						ISD_Settings.Instance.compileFlags.Add(NewCompilerFlag);
						NewCompilerFlag = string.Empty;
					}
				}
				EditorGUILayout.EndHorizontal();
			}
		}

		public static void PlistValues ()	{
			ISD_Settings.Instance.IsPlistSettingsOpen = EditorGUILayout.Foldout(ISD_Settings.Instance.IsPlistSettingsOpen, "Plist values");
			
			if(ISD_Settings.Instance.IsPlistSettingsOpen) {
				if (ISD_Settings.Instance.PlistVariables.Count == 0) {
					EditorGUILayout.HelpBox("No Plist values added", MessageType.None);
				}
				EditorGUI.indentLevel++; {	
					foreach(Variable var in ISD_Settings.Instance.PlistVariables) {
						EditorGUILayout.BeginVertical (GUI.skin.box);
						
						EditorGUILayout.BeginHorizontal();
						var.IsOpen = EditorGUILayout.Foldout(var.IsOpen, var.Name);

						EditorGUILayout.LabelField("(" + var.Type.ToString() +  ")");
						
						bool ItemWasRemoved = DrawSortingButtons((object) var, ISD_Settings.Instance.PlistVariables);
						if(ItemWasRemoved) {
							return;
						}
						EditorGUILayout.EndHorizontal();
						if(var.IsOpen) {						
							EditorGUI.indentLevel++; {
								
								EditorGUILayout.BeginHorizontal();
								EditorGUILayout.LabelField("Type");
								if (var.PlistVariables.Count > 0) {
									GUI.enabled = false;
									var.Type = (PlistValueTypes)EditorGUILayout.EnumPopup (var.Type);
									GUI.enabled = true;
								} else {
									var.Type = (PlistValueTypes)EditorGUILayout.EnumPopup (var.Type);
								}
								//var.Type = (PlistValueTypes) EditorGUILayout.EnumPopup (var.Type);
								EditorGUILayout.EndHorizontal();

								if(var.Type == PlistValueTypes.String || var.Type == PlistValueTypes.Integer || var.Type == PlistValueTypes.Float || var.Type == PlistValueTypes.Boolean) {
									EditorGUILayout.BeginHorizontal();
									EditorGUILayout.LabelField("Value");								
									switch(var.Type) {
									case PlistValueTypes.Boolean:
										var.BooleanValue	 = EditorGUILayout.Toggle (var.BooleanValue);
										break;									
									case PlistValueTypes.Float:
										var.FloatValue = EditorGUILayout.FloatField(var.FloatValue);
										break;									
									case PlistValueTypes.Integer:
										var.IntegerValue = EditorGUILayout.IntField (var.IntegerValue);
										break;									
									case PlistValueTypes.String:
										var.StringValue = EditorGUILayout.TextField (var.StringValue);
										break;
									}
									EditorGUILayout.EndHorizontal();
								}
								if(var.Type == PlistValueTypes.Array) {
									DrawArrayValues(var);
								}
								if(var.Type == PlistValueTypes.Dictionary) {
									DrawDictionaryValues(var);
								}
							}EditorGUI.indentLevel--;
						}
						EditorGUILayout.EndVertical ();
					}
					EditorGUILayout.Space();
				} EditorGUI.indentLevel--;

				EditorGUILayout.BeginVertical (GUI.skin.box);
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.PrefixLabel("Add New Variable");
				NewPlistValueName = EditorGUILayout.TextField(NewPlistValueName);
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.Space();
				if(GUILayout.Button("Add",  GUILayout.Width(100))) {
					if (NewPlistValueName.Length > 0) {
						Variable var = new Variable ();
						var.Name = NewPlistValueName;
						ISD_Settings.Instance.PlistVariables.Add (var);
					}
					NewPlistValueName = string.Empty;
				}
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.Space();
				EditorGUILayout.EndVertical ();
			}
		}


		public static void DrawArrayValues (Variable varr)
		{
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Values Type");
			PlistValueTypes type = PlistValueTypes.String;
			if (varr.ArrayValue.Count > 0 || varr.PlistVariables.Count > 0) {
				GUI.enabled = false;
				type = (PlistValueTypes)EditorGUILayout.EnumPopup (varr.ArrayType);
				GUI.enabled = true;
			} else {
				type = (PlistValueTypes)EditorGUILayout.EnumPopup (varr.ArrayType);
			}


			varr.ArrayType = type;

			EditorGUILayout.EndHorizontal ();
			varr.IsListOpen = EditorGUILayout.Foldout (varr.IsListOpen, "Array Values");

			if (varr.IsListOpen) {
				if (type != PlistValueTypes.Dictionary && type != PlistValueTypes.Array) {
					foreach (VariableListed v  in varr.ArrayValue) {
						EditorGUILayout.BeginHorizontal ();
						GUI.enabled = false;
						v.Type = (PlistValueTypes)EditorGUILayout.EnumPopup (varr.ArrayType);
						GUI.enabled = true;
						DrawValueFiled (v);

						bool removed = DrawSortingButtons ((object)v, varr.ArrayValue);
						if (removed) {
							return;
						}
						EditorGUILayout.EndHorizontal (); 
					}
				} else if (type == PlistValueTypes.Array) {//show list of Arrays
					foreach (Variable v in varr.PlistVariables) {
						EditorGUILayout.BeginVertical (GUI.skin.box); 

						EditorGUILayout.BeginHorizontal ();   
						string dictKey = "" + v.Name;
						v.IsOpen = EditorGUILayout.Foldout (v.IsOpen, dictKey);

						EditorGUILayout.LabelField ("(" + v.Type.ToString () + ")");

						bool ItemWasRemoved = DrawSortingButtons ((object)v, ISD_Settings.Instance.PlistVariables);
						if (ItemWasRemoved) {
							return;
						}
						EditorGUILayout.EndHorizontal (); //3h 
						if (v.IsOpen) {						
							EditorGUI.indentLevel++;
							{

								EditorGUILayout.BeginHorizontal ();
								EditorGUILayout.LabelField ("Type");
								GUI.enabled = false;
								v.Type = (PlistValueTypes)EditorGUILayout.EnumPopup (v.Type);
								GUI.enabled = true;
								EditorGUILayout.EndHorizontal ();

								if (v.Type == PlistValueTypes.String || v.Type == PlistValueTypes.Integer || v.Type == PlistValueTypes.Float || v.Type == PlistValueTypes.Boolean) {
									EditorGUILayout.BeginHorizontal ();
									EditorGUILayout.LabelField ("Value");								
									switch (v.Type) {
									case PlistValueTypes.Boolean:
										v.BooleanValue = EditorGUILayout.Toggle (v.BooleanValue);
										break;									
									case PlistValueTypes.Float:
										v.FloatValue = EditorGUILayout.FloatField (v.FloatValue);
										break;									
									case PlistValueTypes.Integer:
										v.IntegerValue = EditorGUILayout.IntField (v.IntegerValue);
										break;									
									case PlistValueTypes.String:
										v.StringValue = EditorGUILayout.TextField (v.StringValue);
										break;
									}
									EditorGUILayout.EndHorizontal ();
								} else if (v.Type == PlistValueTypes.Array) {
									DrawArrayValues (v);
								} else if (v.Type == PlistValueTypes.Dictionary) {
									DrawDictionaryValues (v);
								}

							}
							EditorGUI.indentLevel--;

						}
						EditorGUILayout.EndVertical ();  
					}

				} else if (type == PlistValueTypes.Dictionary) {//show list of dictionaries
					foreach (Variable v in varr.PlistVariables) {
						EditorGUILayout.BeginVertical (GUI.skin.box); 

						EditorGUILayout.BeginHorizontal ();   
						string dictKey = "Key: " + v.Name;
						v.IsOpen = EditorGUILayout.Foldout (v.IsOpen, dictKey);

						EditorGUILayout.LabelField ("(" + v.Type.ToString () + ")");

						bool ItemWasRemoved = DrawSortingButtons ((object)v, varr.PlistVariables);
						if (ItemWasRemoved) {
							//Debug.Log ("Removing dictionary");
							return;
						}
						EditorGUILayout.EndHorizontal ();  
						if (v.IsOpen) {						
							EditorGUI.indentLevel++; {

								EditorGUILayout.BeginHorizontal ();
								EditorGUILayout.LabelField ("Type");
								GUI.enabled = false;
								v.Type = (PlistValueTypes)EditorGUILayout.EnumPopup (v.Type);
								GUI.enabled = true;
								EditorGUILayout.EndHorizontal ();

								if (v.Type == PlistValueTypes.String || v.Type == PlistValueTypes.Integer || v.Type == PlistValueTypes.Float) {
									EditorGUILayout.BeginHorizontal ();
									EditorGUILayout.LabelField ("Value");								
									switch (v.Type) {								
									case PlistValueTypes.Float:
										v.FloatValue = EditorGUILayout.FloatField (v.FloatValue);
										break;									
									case PlistValueTypes.Integer:
										v.IntegerValue = EditorGUILayout.IntField (v.IntegerValue);
										break;									
									case PlistValueTypes.String:
										v.StringValue = EditorGUILayout.TextField (v.StringValue);
										break;
									}
									EditorGUILayout.EndHorizontal ();
								} else if (v.Type == PlistValueTypes.Boolean) {
									v.BooleanValue = SA.Common.Editor.Tools.YesNoFiled ("Value", v.BooleanValue);
								} else if (v.Type == PlistValueTypes.Array) {
									DrawArrayValues (v);
								} else if (v.Type == PlistValueTypes.Dictionary) {
									DrawDictionaryValues (v);
								}

							}
							EditorGUI.indentLevel--;

						}
						EditorGUILayout.EndVertical ();  
					}
				}
				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.Space ();
				if (GUILayout.Button ("Add New Value To Array", GUILayout.Width (150))) {
					if (type == PlistValueTypes.String || type == PlistValueTypes.Integer || type == PlistValueTypes.Float || type == PlistValueTypes.Boolean) {
						varr.ArrayValue.Add(new VariableListed());
					} else if (type == PlistValueTypes.Array) {
						Variable newVar = new Variable ();
						newVar.Name = NewPlistValueName;
						newVar.Type = PlistValueTypes.Array;
						varr.PlistVariables.Add (newVar);
					} else if (type == PlistValueTypes.Dictionary) {
						Variable newVar = new Variable ();
						newVar.Name = NewPlistValueName;
						newVar.Type = PlistValueTypes.Dictionary;
						varr.PlistVariables.Add (newVar);
					}
						
				}
				EditorGUILayout.EndHorizontal ();
			} 
		}

		public static void DrawDictionaryValues (Variable varr)
		{
			varr.IsListOpen = EditorGUILayout.Foldout (varr.IsListOpen, "Dictionary Values");

			if (varr.IsListOpen) {

				foreach (Variable var in varr.PlistVariables) {
					EditorGUILayout.BeginVertical (GUI.skin.box); 

					EditorGUILayout.BeginHorizontal ();   
					string dictKey = "Key: " + var.Name;
					var.IsOpen = EditorGUILayout.Foldout (var.IsOpen, dictKey);

					EditorGUILayout.LabelField ("(" + var.Type.ToString () + ")");
					bool ItemWasRemoved = DrawSortingButtons ((object)var, varr.PlistVariables);
					if (ItemWasRemoved) {
						return;
					}
					EditorGUILayout.EndHorizontal ();  
					if (var.IsOpen) {						
						EditorGUI.indentLevel++;
						{

							EditorGUILayout.BeginHorizontal ();
							EditorGUILayout.LabelField ("Type");


							var.Type = (PlistValueTypes)EditorGUILayout.EnumPopup (var.Type);
							EditorGUILayout.EndHorizontal ();

							if (var.Type == PlistValueTypes.String || var.Type == PlistValueTypes.Integer || var.Type == PlistValueTypes.Float || var.Type == PlistValueTypes.Boolean) {
								EditorGUILayout.BeginHorizontal ();
								EditorGUILayout.LabelField ("Value");								
								switch (var.Type) {
								case PlistValueTypes.Boolean:
									var.BooleanValue = EditorGUILayout.Toggle (var.BooleanValue);
									break;									
								case PlistValueTypes.Float:
									var.FloatValue = EditorGUILayout.FloatField (var.FloatValue);
									break;									
								case PlistValueTypes.Integer:
									var.IntegerValue = EditorGUILayout.IntField (var.IntegerValue);
									break;									
								case PlistValueTypes.String:
									var.StringValue = EditorGUILayout.TextField (var.StringValue);
									break;
								}
								EditorGUILayout.EndHorizontal ();
							} else if (var.Type == PlistValueTypes.Array) {
								DrawArrayValues (var);
							} else if (var.Type == PlistValueTypes.Dictionary) {
								DrawDictionaryValues (var);
							}

						}
						EditorGUI.indentLevel--;

					}
					EditorGUILayout.EndVertical ();  

				}
				EditorGUILayout.Space ();
			} 

			EditorGUILayout.BeginVertical (GUI.skin.box);
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.PrefixLabel ("New Key");
			NewValueName = EditorGUILayout.TextField (NewValueName);
			EditorGUILayout.EndHorizontal ();

			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.Space ();
			if (GUILayout.Button ("Add New Dictionary", GUILayout.Width (150))) {
				if (NewValueName.Length > 0) {
					Variable var = new Variable ();
					var.Name = NewValueName;
					varr.PlistVariables.Add (var);
				}
				NewValueName = string.Empty;
			}
			/////////// ---------------------------------------------------------------------
			foreach (KeyValuePair<string, VariableListed> pair  in varr.DictionaryValue) {
				EditorGUI.indentLevel++;
				{					
					EditorGUILayout.BeginHorizontal ();
					VariableListed v = pair.Value;
					string dictKey = "Key: " + v.DictKey;
					v.IsOpen = EditorGUILayout.Foldout (v.IsOpen, dictKey);
					bool removed = DrawSortingButtons ((object)v, varr.ArrayValue);
					if (removed) {
						return;
					}


					if (v.IsOpen) {
						//EditorGUILayout.BeginHorizontal();
						v.Type = (PlistValueTypes)EditorGUILayout.EnumPopup (v.Type);
						Debug.Log ("v.Type " + v.Type);
						DrawValueFiled (v);   //DRAW VALUE
					}
					EditorGUILayout.EndHorizontal ();

				}
				EditorGUI.indentLevel--;
			}


			EditorGUILayout.EndHorizontal ();
			EditorGUILayout.EndVertical ();
		}


		public static void DrawValue() {
			
		}



		public static void DrawValueFiled(VariableListed var, string caption = "") {
			switch(var.Type) {
				case PlistValueTypes.Boolean: 
					var.BooleanValue	 = EditorGUILayout.Toggle (var.BooleanValue);
					break;				
				case PlistValueTypes.Float:
					var.FloatValue = EditorGUILayout.FloatField("Test", var.FloatValue);
					break;				
				case PlistValueTypes.Integer:
					var.IntegerValue = EditorGUILayout.IntField (var.IntegerValue);
					break;				
				case PlistValueTypes.String:
					var.StringValue = EditorGUILayout.TextField (var.StringValue);
					break;
				//Creating array and dictionary for info.plist
				case PlistValueTypes.Array:
				//TODO
					break;
				case PlistValueTypes.Dictionary:
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField("something");
					EditorGUILayout.EndHorizontal();

				//TODO
					break;
			}
		}

		public static void AboutGUI() {
			GUI.enabled = true;
			EditorGUILayout.HelpBox("About the Plugin", MessageType.None);
			EditorGUILayout.Space();
		

			SA.Common.Editor.Tools.SelectableLabelField(SdkVersion,   ISD_Settings.VERSION_NUMBER);
			SA.Common.Editor.Tools.SupportMail();

			SA.Common.Editor.Tools.DrawSALogo();
			#if DISABLED
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Note: This version of IOS Deploy designed for Stan's Assets");
			EditorGUILayout.LabelField("plugins internal use only. If you want to use IOS Deploy  ");
			EditorGUILayout.LabelField("for your project needs, please, ");
			EditorGUILayout.LabelField("purchase a copy of IOS Deploy plugin.");
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.Space();
			
			if(GUILayout.Button("Documentation",  GUILayout.Width(150))) {
				Application.OpenURL("https://goo.gl/sOJFXJ");
			}
			
			if(GUILayout.Button("Purchase",  GUILayout.Width(150))) {
				Application.OpenURL("https://goo.gl/Nqbuuv");
			}		
			EditorGUILayout.EndHorizontal();
			#endif
		}
		
		public static void SelectableLabelField(GUIContent label, string value) {
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(label, GUILayout.Width(180), GUILayout.Height(16));
			EditorGUILayout.SelectableLabel(value, GUILayout.Height(16));
			EditorGUILayout.EndHorizontal();
		}

		public static bool DrawSortingButtons(object currentObject, IList ObjectsList) {		
			int ObjectIndex = ObjectsList.IndexOf(currentObject);
			if(ObjectIndex == 0) {
				GUI.enabled = false;
			} 

			bool up 		= GUILayout.Button("↑", EditorStyles.miniButtonLeft, GUILayout.Width(20));
			if(up) {
				object c = currentObject;
				ObjectsList[ObjectIndex]  		= ObjectsList[ObjectIndex - 1];
				ObjectsList[ObjectIndex - 1] 	=  c;
			}
			
			if(ObjectIndex >= ObjectsList.Count -1) {
				GUI.enabled = false;
			} else {
				GUI.enabled = true;
			}
			
			bool down = GUILayout.Button("↓", EditorStyles.miniButtonMid, GUILayout.Width(20));
			if(down) {
				object c = currentObject;
				ObjectsList[ObjectIndex] =  ObjectsList[ObjectIndex + 1];
				ObjectsList[ObjectIndex + 1] = c;
			}

			GUI.enabled = true;
			bool r = GUILayout.Button("-", EditorStyles.miniButtonRight, GUILayout.Width(20));
			if(r) {
				Debug.Log ("remove " + currentObject.ToString());
				ObjectsList.Remove(currentObject);
			}
			
			return r;
		}

		public static void LanguageValues ()	{
			ISD_Settings.Instance.IsLanguageSettingOpen = EditorGUILayout.Foldout(ISD_Settings.Instance.IsLanguageSettingOpen, "Languages");
			
			if(ISD_Settings.Instance.IsLanguageSettingOpen)	 {
				if (ISD_Settings.Instance.langFolders.Count == 0)	{
					EditorGUILayout.HelpBox("No Languages added", MessageType.None);
				}

				foreach(string lang in ISD_Settings.Instance.langFolders) 	{
					EditorGUILayout.BeginVertical (GUI.skin.box);
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.SelectableLabel(lang, GUILayout.Height(18));
					EditorGUILayout.Space();
					
					bool pressed  = GUILayout.Button("x",  EditorStyles.miniButton, GUILayout.Width(20));
					if(pressed) 	{
						ISD_Settings.Instance.langFolders.Remove(lang);
						return;
					}
					
					EditorGUILayout.EndHorizontal();
					EditorGUILayout.EndVertical ();
				}
				EditorGUILayout.Space();

				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("Add New Language");
				NewLanguage = EditorGUILayout.TextField(NewLanguage, GUILayout.Width(200));
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.BeginHorizontal();			
				EditorGUILayout.Space();
				
				if(GUILayout.Button("Add",  GUILayout.Width(100)))	{
					if(!ISD_Settings.Instance.langFolders.Contains(NewLanguage) && NewLanguage.Length > 0)	{
						ISD_Settings.Instance.langFolders.Add(NewLanguage);
						NewLanguage = string.Empty;
					}				
				}
				EditorGUILayout.EndHorizontal();
			}
		}

		private static void DirtyEditor() {
			#if UNITY_EDITOR
			EditorUtility.SetDirty(ISD_Settings.Instance);
			#endif
		}
	}

}
#endif