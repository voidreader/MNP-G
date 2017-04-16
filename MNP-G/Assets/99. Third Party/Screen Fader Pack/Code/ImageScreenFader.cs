//
// This file is a part of Screen Fader asset
// For any help, support or documentation
// follow www.patico.pro
//

using UnityEngine;

/// <summary>
/// 
/// </summary>
class ImageScreenFader : Fader
{
    public Texture image = null;
    protected Color last_fadeColor = Color.black;
    [Range(0, 1)]
    public float maxDensity = 1;
    protected Texture colorTexture;

    protected override void Init()
    {
        instance = this;
        colorTexture = image;
        last_fadeColor = color;
    }
    protected override void DrawOnGUI()
    {
        GUI.color = color;
        if (image != null)
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), image, ScaleMode.StretchToFill, true);
        else
            Debug.Log("ImageScreenFader: image is null");
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