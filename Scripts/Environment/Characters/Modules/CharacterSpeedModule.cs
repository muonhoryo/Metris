using MuonhoryoLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam_Temple.Characters.COP
{
    public sealed class CharacterSpeedModule : Module,ISpeedModule
    {
        [SerializeField]
        private float DefaultSpeed;

        private CompositeParameter Speed;
        CompositeParameter ISpeedModule.Speed_ => Speed;
        protected override bool CanTurnActivityFromOutside_ =>false;

        private void Awake()
        {
            Speed = new CompositeParameter(DefaultSpeed);
        }
    }
}
