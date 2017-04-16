using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.LiveOpsEditor;
using UnityEditor.XCodeEditor;

namespace UnityEditor.LiveOpsEditor
{
	public static class XCodePostProcess
	{
		[PostProcessBuild(100)]
		public static void OnPostProcessBuild(BuildTarget target, string path)
		{

			if (target == BuildTarget.iOS)
			{
				UnityEditor.XCodeEditor.XCProject project = new UnityEditor.XCodeEditor.XCProject(path);
				
				// Find and run through all projmods files to patch the project
				
				string projModPath = System.IO.Path.Combine(Application.dataPath, "LiveOps/Editor/iOS");
				var files = System.IO.Directory.GetFiles(projModPath, "*.projmods", System.IO.SearchOption.AllDirectories);
				foreach (var file in files)
				{
					project.ApplyMod(Application.dataPath, file);
				}
				project.Save();
				
//				PlistMod.UpdatePlist(path, FBSettings.AppId);
//				FixupFiles.FixSimulator(path);
				
//				FixupFiles.AddVersionDefine(path);
			}
			
			if (target == BuildTarget.Android)
			{

			}
		}
	}
}
