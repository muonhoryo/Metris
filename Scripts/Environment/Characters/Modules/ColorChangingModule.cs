using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameJam_Temple.Characters.COP.IColorChangableCharacter;

namespace GameJam_Temple.Characters.COP
{
    public sealed class ColorChangingModule : Module, IColorChangingModule
    {
        public event Action<ColorModule.Color> ChangeColorEvent = delegate { };

        [SerializeField]
        private Component AcceptableColorsModuleComponent;
        [SerializeField]
        private SpriteRenderer BaseSprite;

        private IAcceptableColorsModule AcceptableColorsModule;
        IAcceptableColorsModule IColorChangingModule.AcceptableColorsModule_ => AcceptableColorsModule;
        ColorModule IColorChangingModule.ColorModule_ => colorModule;

        private ColorModule.Color SelectNextColor(ColorModule.Color oldColor)
        {
            int color = (int)oldColor;
            if (oldColor == ColorModule.Color.Red)
                color = 0;
            else
                color++;
            return (ColorModule.Color)color;
        }
        void IColorChangingModule.ChangeColor()
        {
            ColorModule.Color color = colorModule.color;
            ColorModule.Color nextColor = SelectNextColor(color);
            while (!AcceptableColorsModule.IsAcceptableColor(nextColor))
            {
                color = nextColor;
                nextColor = SelectNextColor(color);
            }
            colorModule.SetColor(nextColor);
            BaseSprite.color = colorModule.color.GetUnityColor();
        }

        [SerializeField]
        private ColorModule colorModule;

        private void Awake()
        {
            AcceptableColorsModule = AcceptableColorsModuleComponent as IAcceptableColorsModule;
        }

        private void Start()
        {
            BaseSprite.color = colorModule.color.GetUnityColor();
        }
    }
}
