//
// This file is a part of Screen Fader asset
// For any help, support or documentation
// follow www.patico.pro
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class LinesScreenFader : Fader
{
    public int numberOfStripes = 10;
    public int space = 10;
    public Direction direction = Direction.IN_FROM_RIGHT;
    
    public enum Direction 
    { 
        IN_FROM_LEFT, 
        IN_FROM_RIGHT,
        IN_UP_DOWN
    }

    private Color last_color = Color.black;
    private int last_numberOfStripes = 10;

    TextureCollection textures = new TextureCollection();
    AnimRect[] rects = null;

    public void AddTextures(Texture[] images)
    {
        for (int i = 0; i < images.Length; i++)
        {
            textures[i] = images[i];
        }
    }

    protected override void Init()
    {
        base.Init();

        textures.SetDefaultTexture(base.GetTextureFromColor(color), numberOfStripes);
        rects = new AnimRect[numberOfStripes];

        int rectW = Screen.width / numberOfStripes;

        for (int i = 0; i < rects.Length; i++)
        {
            int extraWidth = 0; // additional space (couple of pixels) for last rectangle
            if (i == rects.Length -1)
                extraWidth = Screen.width - (rectW * rects.Length);

            rects[i] = CreateRect(rectW, i, extraWidth);
        }

        last_color = color;
        last_numberOfStripes = numberOfStripes;
    }

    private AnimRect CreateRect(int rectW, int index, int extra)
    {
        Vector2 offsetStart = GetStartOffset(direction, rectW, index);
        Vector2 offsetFinal = GetFinalOffset(direction, rectW, index);

        // change order for IN_FROM_LEFT
        int order = direction == Direction.IN_FROM_LEFT ? rects.Length - index : index;
        
        return new AnimRect(
            new Rect(rectW * order + offsetStart.x, offsetStart.y, rectW + extra, Screen.height),
            new Rect(rectW * order + offsetFinal.x, offsetFinal.y, rectW + extra, Screen.height));
    }
    protected override void Update()
    {
        if (color != last_color | numberOfStripes != last_numberOfStripes)
            Init();

        base.Update();
    }
    protected override void DrawOnGUI()
    {
        for (int i = 0; i < rects.Length; i++)
        {
            float t = base.fadeBalance;
            GUI.DrawTexture(rects[i].GetRect(t), textures[i]);
        }
    }

    protected Vector2 GetStartOffset(Direction direction, int lineWidth, int index)
    {
        Vector2 result = Vector2.zero;

        int spaceOffset = (int)(lineWidth * index * (float)Mathf.Sqrt(index * space));

        switch (direction)
        {
            case Direction.IN_FROM_LEFT:
                result = new Vector2(-Screen.width - lineWidth - spaceOffset - 1, 0);
                break;
            case Direction.IN_FROM_RIGHT:
                result = new Vector2(Screen.width + spaceOffset + 1, 0);
                break;
            case Direction.IN_UP_DOWN:
                result = new Vector2(0, (Screen.height + 1) * (index % 2 == 1 ? 1 : -1));
                break;
            default:
                result = Vector2.zero;
                break;
        }

        return result;
    }
    protected Vector2 GetFinalOffset(Direction direction, int lineWidth, int index)
    {
        Vector2 result = Vector2.zero;
       
        switch (direction)
        {
            case Direction.IN_FROM_LEFT:
                result = new Vector2(-lineWidth, 0);
                break;
            case Direction.IN_FROM_RIGHT:
            default:
                result = Vector2.zero;
                break;
        }

        return result;
    }

    struct AnimRect
    {
        private Rect rectStart;
        private Rect rectFinal;

        public AnimRect(Rect rectStart, Rect rectFinal)
        {
            this.rectStart = rectStart;
            this.rectFinal = rectFinal;
        }

        public Rect GetRect(float time)
        {
            if (time >= 1)
                return rectFinal;

            else if (time > 0)
                return GetRectByT(time);

            else
                return rectStart;
        }

        Rect GetRectByT(float time)
        {
            return ScreenFaderUtility.Lerp(rectStart, rectFinal, time);
        }
    }

    class TextureCollection
    {
        Dictionary<int, Texture> textures = new Dictionary<int, Texture>();

        public void SetDefaultTexture(Texture defaultTexture, int count)
        {
            for (int i = 0; i < count; i++)
            {
                if (!textures.ContainsKey(i))
                    textures.Add(i, defaultTexture);
            }
        }
        public Texture this[int index]
        {
            get { return textures[index]; }
            set
            {
                if (textures.ContainsKey(index))
                    textures[index] = value;
                else
                    textures.Add(index, value);
            }
        }
    }
}

public static class ScreenFaderUtility
{
    public static Rect Lerp(Rect from, Rect to, float t)
    {
        return new Rect(Mathf.Lerp(from.xMin, to.xMin, t),
                        Mathf.Lerp(from.yMin, to.yMin, t),
                        Mathf.Lerp(from.width, to.width, t),
                        Mathf.Lerp(from.height, to.height, t));
    }
}