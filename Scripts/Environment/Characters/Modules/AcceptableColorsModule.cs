using MuonhoryoLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameJam_Temple.Characters.COP.IColorChangableCharacter;

namespace GameJam_Temple.Characters.COP
{
    public sealed class AcceptableColorsModule : Module,IAcceptableColorsModule
    {
        public event Action CollectColorEvent = delegate { };
        private ColorModule.Color? [] AcceptedColors = new ColorModule.Color?[]
        {
            ColorModule.Color.Cyan,
            null,
            null,
            null,
            null,
            null,
            null
        };
        public bool IsAcceptableColor(ColorModule.Color color)
        {
            return AcceptedColors.Contains(color);
        }
        public void ResetList()
        {
            throw new System.NotImplementedException();
        }
        public void AddColor(ColorModule.Color newColor)
        {
            if (AcceptedColors.Contains(newColor))
                throw new Exceptions.GameJam_Exception($"{newColor} is already exists.");
            else
                AcceptedColors[(int)newColor] = newColor;
            CollectColorEvent();
        }
    }
}
