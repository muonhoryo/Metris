using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GameJam_Temple.Characters.COP
{
    public sealed class ColorModule : Module
    {
        public event Action<Color> ColorChangedEvent=delegate { };
        public enum Color
        {
            Cyan,
            Blue,
            Orange,
            Yellow,
            Green,
            Purple,
            Red
        }
        public Color color { get; private set; } = Color.Cyan;
        public void SetColor(Color color)
        {
            this.color= color;
            ColorChangedEvent(color);
        }
    }
    public static class ColorModuleExtensions
    {
        public static Color GetUnityColor(this ColorModule.Color color)
        {
            switch (color)
            {
                case ColorModule.Color.Cyan:
                    return GlobalConstants.Config.CyanColor;
                case ColorModule.Color.Blue:
                    return GlobalConstants.Config.BlueColor;
                case ColorModule.Color.Orange:
                    return GlobalConstants.Config.OrangeColor;
                case ColorModule.Color.Yellow:
                    return GlobalConstants.Config.YellowColor;
                case ColorModule.Color.Green:
                    return GlobalConstants.Config.GreenColor;
                case ColorModule.Color.Purple:
                    return GlobalConstants.Config.PurpleColor;
                case ColorModule.Color.Red:
                    return GlobalConstants.Config.RedColor;
            }
            throw new Exceptions.GameJam_Exception("Color choising error");
        }
    }
}
