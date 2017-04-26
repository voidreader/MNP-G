//
// This file is a part of Screen Fader asset
// For any help, support or documentation
// follow www.patico.pro
//

using UnityEngine;
using System.Collections;
using System;

public class DefaultScreenFader : Fader
{
    protected Color last_fadeColor = Color.black;
    [Range(0, 1)]
    public float maxDensity = 1;
    protected Texture colorTexture;

    protected override void Init()
    {
        base.Init();

        colorTexture = base.GetTextureFromColor(color);
        last_fadeColor = color;
    }

    protected override void DrawOnGUI()
    {
        GUI.color = color;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), colorTexture, ScaleMode.StretchToFill, true);
    }
    protected override void Update()
    {
        if (color != last_fadeColor)
            Init();

        color.a = GetLinearBalance();
        base.Update();
    }

    protected virtual float GetLinearBalance()
    {
        return fadeBalance < maxDensity ? fadeBalance : maxDensity;
    }
    
}