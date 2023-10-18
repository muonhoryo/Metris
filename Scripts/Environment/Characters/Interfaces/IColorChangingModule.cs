using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam_Temple.Characters.COP
{

    public interface IColorChangableCharacter
    {
        public interface IAcceptableColorsModule
        {
            public event Action CollectColorEvent;
            public bool IsAcceptableColor(ColorModule.Color color);
            public void ResetList();
            public void AddColor(ColorModule.Color newColor);
        }
        public interface IColorChangingModule
        {
            public event Action<ColorModule.Color> ChangeColorEvent;

            public ColorModule.Color Color_ => ColorModule_.color;

            public void ChangeColor();

            protected IAcceptableColorsModule AcceptableColorsModule_ { get; }
            protected ColorModule ColorModule_ { get; }
        }
        public event Action<ColorModule.Color> ChangeColorEvent_
        {
            add { ColorChangingModule_.ChangeColorEvent += value; }
            remove { ColorChangingModule_.ChangeColorEvent -= value;}
        }

        public ColorModule.Color Color_ { get=>ColorChangingModule_.Color_; }

        public void ChangeColor()
        {
            ColorChangingModule_.ChangeColor();
        }

        protected IColorChangingModule ColorChangingModule_ { get; }
    }
}
