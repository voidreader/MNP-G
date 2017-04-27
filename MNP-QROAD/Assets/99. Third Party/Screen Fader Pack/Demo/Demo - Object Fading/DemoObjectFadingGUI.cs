//
// This file is a part of Screen Fader asset
// For any help, support or documentation
// follow www.patico.pro
//

using UnityEngine;
using System.Collections;
using System.Text;

[ExecuteInEditMode]
public class DemoObjectFadingGUI : MonoBehaviour
{
	public GameObject character;
    public Texture damageImage;
    public Texture[] linesImages;

    Rect codeWindowRectangle = new Rect(300, (Screen.height - 575) / 2, Screen.width - 350, 575);
    bool showCode = false;

    string exampleCode = "";
    string exampleCodeFormatted = "";
    bool copy;

    void LateUpdate()
    {
        codeWindowRectangle = new Rect(300, (Screen.height - 575) / 2, Screen.width - 350, 575);
    }

	void OnGUI () {

        GUILayout.Space((Screen.height- 300)/2);

        GUILayout.BeginHorizontal();
        GUILayout.Space(50);

        GUILayout.BeginVertical();

        switch (pGUI.Button("Default Fading", "Setup"))
        {
            case pResult.NONE: break;
            case pResult.BUTN: Fader.SetupAsDefaultFader(); Fader.Instance.SetColor(Color.black).FadeIn().Pause().FadeOut(); break;
            case pResult.CODE: CreateDefaultFadeScreenCode(); ShowCode(); break;
        }

        switch (pGUI.Button("Squared Effect", "Setup"))
        {
            case pResult.NONE: break;
            case pResult.BUTN: Fader.SetupAsSquaredFader(10); Fader.Instance.SetColor(Color.black).FadeIn().Pause().FadeOut(); break;
            case pResult.CODE: CreateSquaredFadeScreenCode(); ShowCode(); break;
        }

        switch (pGUI.Button("Striped Effect", "Setup"))
        {
            case pResult.NONE: break;
            case pResult.BUTN: Fader.SetupAsStripesFader(10); Fader.Instance.SetColor(Color.black).FadeIn().Pause().FadeOut(); break;
            case pResult.CODE: CreateStripesFadeScreenCode(); ShowCode(); break;
        }

        switch (pGUI.Button("[new] Lines Effect", "Setup"))
        {
            case pResult.NONE: break;
            case pResult.BUTN: Fader.SetupAsLinesFader(LinesScreenFader.Direction.IN_UP_DOWN, linesImages); Fader.Instance.FadeIn(0.5f).Pause(3).FadeOut(0.25f); break;
            case pResult.CODE: CreateLinesCode(); ShowCode(); break;
        }
        switch (pGUI.Button("[new] Image Effect", "Setup"))
        {
            case pResult.NONE: break;
            case pResult.BUTN: Fader.SetupAsImageFader(damageImage); Fader.Instance.SetColor(Color.red).FadeIn(0.05f).Pause(0.05f).FadeOut(0.15f); break;
            case pResult.CODE: break;
        }

        GUILayout.Space(10);

        switch (pGUI.Button("Flash"))
        {
            case pResult.NONE: break;
            case pResult.BUTN: Fader.SetupAsDefaultFader(); Color darkRed = new Color(1, 0.5f, 0.5f); Fader.Instance.SetColor(darkRed).Flash(); break;
            case pResult.CODE: CreateFlashCode(); ShowCode(); break;
        }

        switch (pGUI.Button("Object Fading"))
        {
            case pResult.NONE: break;
            case pResult.BUTN: Fader.Instance.FadeIn(character).Pause(1).FadeOut(character, 0.25f); break;
            case pResult.CODE: CreateFadeObjectCode(); ShowCode(); break;
        }

        switch (pGUI.Button("Load Level Fading"))
        {
            case pResult.NONE: break;
            case pResult.BUTN:

                if (Fader.Instance is ImageScreenFader)
                    Fader.SetupAsDefaultFader();

                Fader.Instance.FadeIn().Pause().LoadLevel(GetLevelIndex()).FadeOut(2); break;
            case pResult.CODE: CreateLoadLevelCode(); ShowCode(); break;
        }

        switch (pGUI.Button("Change Colours"))
        {
            case pResult.NONE: break;
            case pResult.BUTN: Fader.Instance.SetColor(GetRandomColor()).FadeIn().Pause().FadeOut(); break;
            case pResult.CODE: CreateSetColourScreenCode(); ShowCode(); break;
        }

        switch (pGUI.Button("Start Coroutine"))
        {
            case pResult.NONE: break;
            case pResult.BUTN: Fader.Instance.StartCoroutine(this, "pCoroutine").Pause(3).FadeIn().FadeOut(); break;
            case pResult.CODE: CreateStartCoroutineScreenCode(); ShowCode(); break;
        }

        // switch (pGUI.Button("Start Action"))
        // {
        //     case pResult.NONE: break;
        //     case pResult.BUTN: break;
        //     case pResult.CODE: CreateStartActionScreenCode(); ShowCode(); break;
        // }

        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        if (showCode)
        {
            GUI.Window(0, codeWindowRectangle, DoWindow, "Code (C#)");
        }
	}

    private void DoWindow(int id)
    {
        if (GUILayout.Button("Copy"))
        {
            copy = !copy;
            if (copy)
            {
                Utility.SendToClipboard(exampleCode);
            }
        }

        if (!copy)
            GUILayout.Label(exampleCodeFormatted);
        else
            GUILayout.TextArea(exampleCode);

        GUI.DragWindow();
    }

    public IEnumerator pCoroutine()
    {
        GameObject moon = GameObject.Find("Test_Moon");
        Transform moontransform = moon.GetComponent<Transform>();
        Vector3 p1 = new Vector3(moontransform.position.x - 5, moontransform.position.y, moontransform.position.z);
        while (moontransform.position != p1)
        {
            moontransform.position = Vector3.Lerp(moontransform.position, p1, Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        yield break;
    }
    private Color GetRandomColor()
    {
        return new Color(
            Random.Range(0.25f, 0.75f), 
            Random.Range(0.25f, 0.75f), 
            Random.Range(0.25f, 0.75f));
    }
    private int GetLevelIndex()
    {
        return Application.loadedLevel == 0 ? 1 : 0;
    }
    private void ShowCode()
    {
        showCode = !showCode;
    }

    private void CreateSetColourScreenCode()
    {
        ExampleCodeBuilder cb = new ExampleCodeBuilder();
        cb.AddOnGUI_SetColour();
        exampleCode = cb.GetExampleCode();
        exampleCodeFormatted = cb.GetFormattedExampleCode();
    }
    private void CreateDefaultFadeScreenCode()
    {
        ExampleCodeBuilder cb = new ExampleCodeBuilder();
        cb.AddOnGUI_FadeScreen();
        exampleCode = cb.GetExampleCode();
        exampleCodeFormatted = cb.GetFormattedExampleCode();
    }
    private void CreateLoadLevelCode()
    {
        ExampleCodeBuilder cb = new ExampleCodeBuilder();
        cb.AddOnGUI_LoadLevel();
        exampleCode = cb.GetExampleCode();
        exampleCodeFormatted = cb.GetFormattedExampleCode();
    }
    private void CreateFadeObjectCode()
    {
        ExampleCodeBuilder cb = new ExampleCodeBuilder();
        cb.AddOnGUI_FadeObject();
        exampleCode = cb.GetExampleCode();
        exampleCodeFormatted = cb.GetFormattedExampleCode();
    }
    private void CreateStartActionScreenCode()
    {
        ExampleCodeBuilder cb = new ExampleCodeBuilder();
        cb.AddOnGUI_StartAction();
        cb.AddMethod_StartAction();
        exampleCode = cb.GetExampleCode();
        exampleCodeFormatted = cb.GetFormattedExampleCode();
    }
    private void CreateStartCoroutineScreenCode()
    {
        ExampleCodeBuilder cb = new ExampleCodeBuilder();
        cb.AddOnGUI_StartCoroutine();
        cb.AddMethod_StartCoroutine();
        exampleCode = cb.GetExampleCode();
        exampleCodeFormatted = cb.GetFormattedExampleCode();
    }
    private void CreateFlashCode()
    {
        ExampleCodeBuilder cb = new ExampleCodeBuilder();
        cb.AddOnGUI_Flash();
        exampleCode = cb.GetExampleCode();
        exampleCodeFormatted = cb.GetFormattedExampleCode();
    }
    private void CreateStripesFadeScreenCode()
    {
        ExampleCodeBuilder cb = new ExampleCodeBuilder();
        cb.AddOnGUI_StripesFade();
        exampleCode = cb.GetExampleCode();
        exampleCodeFormatted = cb.GetFormattedExampleCode();
    }
    private void CreateLinesCode()
    {
        ExampleCodeBuilder cb = new ExampleCodeBuilder();
        cb.AddOnGUI_LinesFade();
        exampleCode = cb.GetExampleCode();
    }
    private void CreateSquaredFadeScreenCode()
    {
        ExampleCodeBuilder cb = new ExampleCodeBuilder();
        cb.AddOnGUI_SquaredFade();
        exampleCode = cb.GetExampleCode();
        exampleCodeFormatted = cb.GetFormattedExampleCode();
    }
}

class pGUI
{
    static int w = 150;
    static int w2 = 50;
    static int h = 25;

    public static pResult Button(string text, string text2 = "Code")
    {
        pResult result = pResult.NONE;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button(text, GUILayout.Width(w), GUILayout.Height(h)))
        {
            result = pResult.BUTN;
        }

        if (GUILayout.Button(text2, GUILayout.Width(w2), GUILayout.Height(h)))
        {
            result = pResult.CODE;
        }
        GUILayout.EndHorizontal();

        return result;
    }
}


enum pResult : byte { NONE = 0, BUTN = 1, CODE = 2 }

class ExampleCodeBuilder
{
    StringBuilder sb_fields = new StringBuilder();
    StringBuilder sb_OnGUI = new StringBuilder();
    StringBuilder sb_methods = new StringBuilder();

    public void AddOnGUI_FadeScreen()
    {
        sb_OnGUI.Append("		if( GUILayout.Button( \"Fade Screen\") )\r\n" +
                        "		{\r\n"+
                        "            Fader.SetupAsDefaultFader();\r\n" +
                        "            Fader.Instance.FadeIn().Pause(1).FadeOut();\r\n" +
                        "		}\r\n");
    }   
    public void AddOnGUI_FadeObject()
    {
        sb_fields.Append("	public GameObject character;\r\n");
        sb_OnGUI.Append("        if ( GUILayout.Button( \"Fade Object\") )\r\n" +
                        "		{\r\n" +
                        "            Fader.Instance.FadeIn(character).Pause().FadeOut(character, 0.25f);\r\n" +
                        "		}\r\n");
    }
    public void AddOnGUI_LoadLevel()
    {
        sb_OnGUI.Append("		if (GUILayout.Button(\"Load Scene\"))\r\n" +
                        "		{\r\n" +
                        "		    Fader.Instance.FadeIn().Pause().LoadLevel(\"Level2\").FadeOut(2);\r\n" +
                        "		}\r\n");
    }
    public void AddOnGUI_SetColour()
    {
        sb_OnGUI.Append("        if (GUILayout.Button(\"Set Color\") )\r\n" +
                        "		{\r\n" +
                        "           Color color = GetRandomColor();\r\n" +
                        "           Fader.Instance.SetColor(color).FadeIn().Pause().FadeOut();\r\n" +
                        "		}\r\n");
        sb_methods.Append(
                        "\r\n   private Color GetRandomColor()\r\n" +
                        "	{\r\n"+
                        "	    return new Color(\r\n"+
                        "	        Random.Range(0.25f, 0.75f), \r\n"+
                        "	        Random.Range(0.25f, 0.75f), \r\n"+
                        "	        Random.Range(0.25f, 0.75f));\r\n"+
                        "	}");
    }
    public void AddOnGUI_SquaredFade()
    {
        sb_OnGUI.Append("        if ( GUILayout.Button( \"Squared Fading\") )\r\n" +
                "		{\r\n" +
                "            Fader.SetupAsSquaredFader(10); // 10 - is a number of squares in the row\r\n" +
                "            Fader.Instance.SetColor(Color.black).FadeIn().Pause().FadeOut();\r\n" +
                "		}\r\n");
    }
    public void AddOnGUI_StripesFade()
    {
        sb_OnGUI.Append("        if ( GUILayout.Button( \"Stripes Fading\") )\r\n" +
                "		{\r\n" +
                "            Fader.SetupAsStripesFader(10); // 10 - is a number of stripes\r\n" +
                "            Fader.Instance.SetColor(Color.black).FadeIn().Pause().FadeOut();\r\n" +
                "		}\r\n");
    }
    internal void AddOnGUI_LinesFade()
    {
        sb_OnGUI.Append("        if ( GUILayout.Button( \"Stripes Fading\") )\r\n" +
                "		{\r\n" +
                "            // linesImages - array of Texture objects\r\n" +
                "            Fader.SetupAsLinesFader(LinesScreenFader.Direction.IN_FROM_RIGHT, linesImages);\r\n" +
                "            Fader.Instance.SetColor(Color.black).FadeIn().Pause().FadeOut();\r\n" +
                "		}\r\n");
    }
    public void AddOnGUI_Flash()
    {
        sb_OnGUI.Append("        if ( GUILayout.Button( \"Quick Flash\") )\r\n" +
                "		{\r\n" +
                "            Color darkRed = new Color(1, 0.5f, 0.5f);\r\n" +
                "            Fader.Instance.SetColor(darkRed).Flash();\r\n" +
                "		}\r\n");
    }
    public void AddOnGUI_StartCoroutine()
    {
        sb_OnGUI.Append("        if ( GUILayout.Button( \"Start Coroutine\") )\r\n" +
                "		{\r\n" +
                "            Fader.Instance.StartCoroutine(this, \"pCoroutine\").Pause(3).FadeIn().FadeOut();\r\n" +
                "		}\r\n");
    }
    public void AddOnGUI_StartAction()
    {
        sb_OnGUI.Append("        if ( GUILayout.Button( \"StartAction\") )\r\n" +
                "		{\r\n" +
                "            \r\n" +
                "		}\r\n");
    }
    public void AddMethod_StartCoroutine()
    {
        sb_methods.Append(
                          "  \r\n"
                        + "  \r\n"
                        + "  /// THIS SIMPLE COROUTINE JUST MOVES A MOON\r\n"
                        + "  public IEnumerator pCoroutine()\r\n"
                        + "  {\r\n"
                        + "      GameObject moon = GameObject.Find(\"Test_Moon\");\r\nTransform moontransform = moon.GetComponent<Transform>();\r\n"
                        + "      Vector3 p1 = new Vector3(moontransform.position.x - 5, moontransform.position.y, moontransform.position.z);\r\n"
                        + "      while (moontransform.position != p1)\r\n"
                        + "      {\r\n"
                        + "          moontransform.position = Vector3.Lerp(moontransform.position, p1, Time.deltaTime);\r\n"
                        + "          yield return new WaitForEndOfFrame();\r\n"
                        + "      }\r\n"
                        + "  \r\n"
                        + "      yield break;\r\n"
                        + "  }\r\n");
    }
    public void AddMethod_StartAction()
    {
        
    }

    public string GetExampleCode()
    {
        string result =
            "using UnityEngine;\r\n"
            + "\r\n"
            +"/// \r\n"
            +"/// TEST SCRIPT FOR SCREEN FADER\r\n"
            +"/// \r\n"
            +"public class ScreenFaderTest : MonoBehaviour\r\n"
            + "{\r\n"
            + " " + sb_fields.ToString()
            +"\r\n"
            + "	void OnGUI () \r\n "
            + "   { \r\n "
            + sb_OnGUI.ToString()
            + "   }"
            + sb_methods.ToString()
            + "\r\n}";

        return result;
    }

    public string GetFormattedExampleCode()
    {
        string result = GetExampleCode();

        result = result.Replace("Fader.", "<color=red><b>Fader</b>.</color>");
        result = result.Replace("FadeIn", "<color=orange>FadeIn</color>");
        result = result.Replace("FadeOut", "<color=orange>FadeOut</color>");
        result = result.Replace("Pause", "<color=orange>Pause</color>");
        result = result.Replace(".Flash", ".<color=orange>Flash</color>");
        result = result.Replace("SetColor", "<color=orange>SetColor</color>");
        result = result.Replace("StartCoroutine", "<color=orange>StartCoroutine</color>");
        result = result.Replace("StartAction", "<color=orange>StartAction</color>");
        result = result.Replace("LoadLevel", "<color=orange>LoadLevel</color>");

        return result;
    }
}

public class Utility
{
    public static void SendToClipboard(string text)
    {
        TextEditor te = new TextEditor();
        te.content = new GUIContent(text);
        te.SelectAll();
        te.Copy();
    }

    public static string ReceiveFromClipboard()
    {
        TextEditor te = new TextEditor();
        te.content = new GUIContent("");
        te.SelectAll();
        te.Paste();
        return te.content.text;
    }
}