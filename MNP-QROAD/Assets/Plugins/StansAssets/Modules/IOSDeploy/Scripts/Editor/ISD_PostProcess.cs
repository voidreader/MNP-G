////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Deploy
// @author Stanislav Osipov (Stan's Assets) 
// @support support@stansassets.com
//
////////////////////////////////////////////////////////////////////////////////


#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
//using UnityEditor.iOS.Xcode;
using System.Collections;
using System.Diagnostics;
using System.Xml;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace SA.IOSDeploy {

	public class PostProcess  {
		


		[PostProcessBuild(100)]
		public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {
			#if UNITY_IPHONE &&  UNITY_EDITOR_WIN
			UnityEngine.Debug.LogWarning("ISD Postprocess is not avaliable for Win");
			#endif


			#if UNITY_IPHONE && UNITY_EDITOR_OSX


			UnityEngine.Debug.Log("SA.IOSDeploy.PostProcess Started");

			Process myCustomProcess = new Process();		
			myCustomProcess.StartInfo.FileName = "python";

			List<string> frmwrkWithOpt = new List<string>();

			foreach(Framework framework in ISD_Settings.Instance.Frameworks) {
				string optional = "|0";
				if(framework.IsOptional) {
					optional = "|1";
				}

				frmwrkWithOpt.Add (framework.Name + optional);
			}



			List<string> libWithOpt = new List<string>();

			foreach(Lib lib in ISD_Settings.Instance.Libraries) { 
				string optional = "|0";
				if(lib.IsOptional) {
					optional = "|1";
				}

				libWithOpt.Add (lib.Name + optional);
			}

			foreach(string fileName in ISD_Settings.Instance.langFolders)
			{
				string tempPath = Path.Combine (pathToBuiltProject, fileName + ".lproj");
				if(!Directory.Exists (tempPath))
				{
					Directory.CreateDirectory (tempPath);
				}
			}


			string frameworks 		= string.Join(" ", frmwrkWithOpt.ToArray());
			string libraries 		= string.Join(" ", libWithOpt.ToArray());
			string linkFlags 		= string.Join(" ", ISD_Settings.Instance.linkFlags.ToArray());
			string compileFlags 	= string.Join(" ", ISD_Settings.Instance.compileFlags.ToArray());
			string languageFolders  = string.Join (" ", ISD_Settings.Instance.langFolders.ToArray ());




			if(BuildTarget.iOS == target){
				UnityEditor.iOS.Xcode.PBXProject proj = new UnityEditor.iOS.Xcode.PBXProject();
				string projPath = Path.Combine(pathToBuiltProject, "Unity-iPhone.xcodeproj/project.pbxproj");
				proj.ReadFromString(File.ReadAllText(projPath));
				string targetGUID = proj.TargetGuidByName("Unity-iPhone");
				if(ISD_Settings.Instance.enableBitCode){
					proj.SetBuildProperty(targetGUID, "ENABLE_BITCODE", "YES");
				}else{
					proj.SetBuildProperty(targetGUID, "ENABLE_BITCODE", "NO");
				}
				if(ISD_Settings.Instance.enableTestability){
					proj.SetBuildProperty(targetGUID, "ENABLE_TESTABILITY", "YES");
				}else{
					proj.SetBuildProperty(targetGUID, "ENABLE_TESTABILITY", "NO");
				}
				if(ISD_Settings.Instance.generateProfilingCode){
					proj.SetBuildProperty(targetGUID, "GENERATE_PROFILING_CODE", "YES");
				}else{
					proj.SetBuildProperty(targetGUID, "GENERATE_PROFILING_CODE", "NO");
				}

				File.WriteAllText(projPath, proj.WriteToString());
			}





			myCustomProcess.StartInfo.Arguments = string.Format("Assets/" + SA.Common.Config.MODULS_PATH + "IOSDeploy/Scripts/Editor/post_process.py \"{0}\" \"{1}\" \"{2}\" \"{3}\" \"{4}\" \"{5}\"", new object[] { pathToBuiltProject, frameworks, libraries, compileFlags, linkFlags, languageFolders });
			myCustomProcess.StartInfo.UseShellExecute = false;
			myCustomProcess.StartInfo.RedirectStandardOutput = true;
			myCustomProcess.Start(); 
			myCustomProcess.WaitForExit();

			XmlDocument document = new XmlDocument();
			string filePath = Path.Combine (pathToBuiltProject, "Info.plist");
			document.Load (filePath);
			document.PreserveWhitespace = true;


			foreach(Variable var in ISD_Settings.Instance.PlistVariables)	{
				XmlNode temp = document.SelectSingleNode( "/plist/dict/key[text() = '" + var.Name + "']" );
				if(temp == null)	{
					XmlNode valNode = null;
					switch(var.Type)	{
					case PlistValueTypes.Array:
						valNode = document.CreateElement("array");
						AddArrayToPlist(var, valNode, document);
						break;

					case PlistValueTypes.Boolean:
						valNode = document.CreateElement(var.BooleanValue.ToString ().ToLower ());
						break;

					case PlistValueTypes.Dictionary:
						valNode = document.CreateElement("dict");
						AddDictionaryToPlist(var, valNode, document);
						break;

					case PlistValueTypes.Float:
						valNode = document.CreateElement("real");
						valNode.InnerText = var.FloatValue.ToString ();
						break;

					case PlistValueTypes.Integer:
						valNode = document.CreateElement("integer");
						valNode.InnerText = var.IntegerValue.ToString ();
						break;

					case PlistValueTypes.String:
						valNode = document.CreateElement("string");
						valNode.InnerText = var.StringValue;
						break;
					}
					XmlNode keyNode = document.CreateElement ("key");
					keyNode.InnerText = var.Name;
					document.DocumentElement.FirstChild.AppendChild (keyNode);
					document.DocumentElement.FirstChild.AppendChild (valNode);
				}
			}


			XmlWriterSettings settings  = new XmlWriterSettings {
				Indent = true,
				IndentChars = "\t",
				NewLineHandling = NewLineHandling.None
			};
			XmlWriter xmlwriter = XmlWriter.Create (filePath, settings );
			document.Save (xmlwriter);
			xmlwriter.Close ();

			System.IO.StreamReader reader = new System.IO.StreamReader(filePath);
			string textPlist = reader.ReadToEnd();
			reader.Close ();

			//strip extra indentation (not really necessary)
			textPlist = (new Regex("^\\t",RegexOptions.Multiline)).Replace(textPlist,"");

			//strip whitespace from booleans (not really necessary)
			textPlist = (new Regex("<(true|false) />",RegexOptions.IgnoreCase)).Replace(textPlist,"<$1/>");

			int fixupStart = textPlist.IndexOf("<!DOCTYPE plist PUBLIC");


			if(fixupStart >= 0) {
				int fixupEnd = textPlist.IndexOf('>', fixupStart);
				if(fixupEnd >= 0) {
					string fixedPlist = textPlist.Substring(0, fixupStart);
					fixedPlist += "<!DOCTYPE plist PUBLIC \"-//Apple//DTD PLIST 1.0//EN\" \"http://www.apple.com/DTDs/PropertyList-1.0.dtd\">";
					fixedPlist += textPlist.Substring(fixupEnd+1);

					textPlist = fixedPlist;
				}
			}



			System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath, false);
			writer.Write(textPlist);
			writer.Close ();

			UnityEngine.Debug.Log("SA.IOSDeploy.PostProcess Finished.");

			#endif
		}

		static void AddArrayToPlist (Variable var, XmlNode node, XmlDocument doc)
		{
			XmlDocument document = doc;
			switch (var.ArrayType) 
			{
			case PlistValueTypes.Array:
				
			if(var.PlistVariables.Count > 0)
				foreach(Variable v in var.PlistVariables)	{
					//XmlNode temp = document.SelectSingleNode( "/plist/dict/key[text() = '" + v.Name + "']" );
						if(true)	{
						//	UnityEngine.Debug.Log ("temp == null");
						XmlNode valNode = null;
						switch(v.Type)	{
							case PlistValueTypes.Array:
							UnityEngine.Debug.Log ("v.Type == Array");
							valNode = document.CreateElement("array");
							AddArrayToPlist(v, valNode, document);
							break;

						case PlistValueTypes.Boolean:
							valNode = document.CreateElement(v.BooleanValue.ToString ().ToLower ());
							break;

						case PlistValueTypes.Dictionary:
							valNode = document.CreateElement("dict");
							AddDictionaryToPlist(v, valNode, document);
							break;

						case PlistValueTypes.Float:
							valNode = document.CreateElement("real");
							valNode.InnerText = v.FloatValue.ToString ();
							break;

						case PlistValueTypes.Integer:
							valNode = document.CreateElement("integer");
							valNode.InnerText = v.IntegerValue.ToString ();
							break;

						case PlistValueTypes.String:
							valNode = document.CreateElement("string");
							valNode.InnerText = v.StringValue;
							break;
						}
						XmlNode keyNode = document.CreateElement ("key");
						keyNode.InnerText = var.Name;
						//node.AppendChild (keyNode);
						node.AppendChild (valNode);
					}
				}
				break;
			case PlistValueTypes.Dictionary:
				
				if(var.PlistVariables.Count > 0)
					foreach(Variable v in var.PlistVariables)	{
					//	XmlNode temp = doc.SelectSingleNode( "/plist/dict/key[text() = '" + v.Name + "']" );
						if(true)	{
//							UnityEngine.Debug.Log ("Creating Dictionary... Variable type = " + v.ArrayType.ToString());
							XmlNode valNode = null;
							switch(v.Type)	{
							case PlistValueTypes.Array:
								UnityEngine.Debug.Log ("v.Type == Array");
								valNode = document.CreateElement("array");
								AddArrayToPlist(v, valNode, document);
								break;

							case PlistValueTypes.Boolean:
								valNode = document.CreateElement(v.BooleanValue.ToString ().ToLower ());
								break;

							case PlistValueTypes.Dictionary:
								valNode = document.CreateElement("dict");
								AddDictionaryToPlist(v, valNode, document);
								break;

							case PlistValueTypes.Float:
								valNode = document.CreateElement("real");
								valNode.InnerText = v.FloatValue.ToString ();
								break;

							case PlistValueTypes.Integer:
								valNode = document.CreateElement("integer");
								valNode.InnerText = v.IntegerValue.ToString ();
								break;

							case PlistValueTypes.String:
								valNode = document.CreateElement("string");
								valNode.InnerText = v.StringValue;
								break;
							}
							XmlNode keyNode = document.CreateElement ("key");
							keyNode.InnerText = var.Name;
							//node.AppendChild (keyNode);
							node.AppendChild (valNode);
						}
					}

				break;
			default:
				//BackUp 
				if(var.ArrayValue.Count == 0) return;

				foreach(VariableListed varArray in var.ArrayValue)
				{
					switch(var.ArrayType)
					{
					case PlistValueTypes.Boolean:
						XmlNode tempBoolNode = doc.CreateElement (varArray.BooleanValue.ToString ().ToLower ());
						node.AppendChild (tempBoolNode);
						break;

					case PlistValueTypes.Float:
						XmlNode tempFloatNode = doc.CreateElement ("real");
						tempFloatNode.InnerText = varArray.FloatValue.ToString ();
						node.AppendChild (tempFloatNode);
						break;

					case PlistValueTypes.Integer:
						XmlNode tempIntegerNode = doc.CreateElement ("integer");
						tempIntegerNode.InnerText = varArray.IntegerValue.ToString ();
						node.AppendChild (tempIntegerNode);
						break;

					case PlistValueTypes.String:
						XmlNode tempStringNode = doc.CreateElement ("string");
						tempStringNode.InnerText = varArray.StringValue;
						node.AppendChild (tempStringNode);
						break;
					}
				}
				//BackUp End
				break;
			}

		}

		static void AddDictionaryToPlist (Variable var, XmlNode node, XmlDocument docc)
		{
			if (var.PlistVariables.Count > 0) {
				UnityEngine.Debug.Log ("Dictionary with variables");
				XmlDocument doc = docc;
				foreach (Variable v in var.PlistVariables) {
					XmlNode valNode = null;
					switch (v.Type) {
					case PlistValueTypes.Dictionary:
						UnityEngine.Debug.Log ("Dict Type");
						valNode = doc.CreateElement ("dict");
						AddDictionaryToPlist (v, valNode, doc);
						break;
					case PlistValueTypes.Array:
						UnityEngine.Debug.Log ("Array Type");
						valNode = doc.CreateElement ("array");
						AddArrayToPlist (v, valNode, doc);
						break;
					case PlistValueTypes.Boolean:
						valNode = doc.CreateElement (v.BooleanValue.ToString ().ToLower ());
						break;
					case PlistValueTypes.Float:
						valNode = doc.CreateElement ("real");
						valNode.InnerText = v.FloatValue.ToString ();
						break;
					case PlistValueTypes.Integer:
						valNode = doc.CreateElement ("integer");
						valNode.InnerText = v.IntegerValue.ToString ();
						break;

					case PlistValueTypes.String:
						valNode = doc.CreateElement ("string");
						valNode.InnerText = v.StringValue;
						break;
					default:
						UnityEngine.Debug.Log ("default Type");
						if (var.DictValues.Count == 0)
							return;
						break;
					}
					XmlNode tempKeyNode = docc.CreateElement ("key");
					tempKeyNode.InnerText = v.Name;
					node.AppendChild (tempKeyNode);
					node.AppendChild (valNode);
				}



			} else {
				/////////////////////////////////////////////----------------------
				if (var.DictValues.Count == 0)
					return;
				UnityEngine.Debug.Log ("Normal dictionary");
				foreach (VariableListed varDict in var.DictValues) {
					XmlNode tempNode = null;
					switch (varDict.Type) {
					case PlistValueTypes.Boolean:
						tempNode = docc.CreateElement (varDict.BooleanValue.ToString ().ToLower ());
						break;

					case PlistValueTypes.Float:
						tempNode = docc.CreateElement ("real");
						tempNode.InnerText = varDict.FloatValue.ToString ();
						break;

					case PlistValueTypes.Integer:
						tempNode = docc.CreateElement ("integer");
						tempNode.InnerText = varDict.IntegerValue.ToString ();
						break;

					case PlistValueTypes.String:
						tempNode = docc.CreateElement ("string");
						tempNode.InnerText = varDict.StringValue;
						break;
					}
					XmlNode tempKeyNode = docc.CreateElement ("key");
					tempKeyNode.InnerText = varDict.DictKey;
					node.AppendChild (tempKeyNode);
					node.AppendChild (tempNode);
				}
			}
		}
	}

}
#endif