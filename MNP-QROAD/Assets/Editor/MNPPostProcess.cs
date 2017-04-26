using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.XCodeAPITheNextFlow;
using System.IO;
using System.Collections;

public class MNPPostProcess{


    [PostProcessBuild(300)]
    public static void OnPostProcessBuild(BuildTarget target, string path) {
        if (target != BuildTarget.iOS) {
            return;
        }


        // Get plist
        string plistPath = path + "/Info.plist";
        PlistDocument plist = new PlistDocument();
        plist.ReadFromString(File.ReadAllText(plistPath));

        // Get root
        PlistElementDict rootDict = plist.root;
        PlistElementArray pArray = rootDict.CreateArray("Required background modes");
        pArray.AddString("App downloads content in response to push notifications");

        // Write to file
        File.WriteAllText(plistPath, plist.WriteToString());

    }

}
