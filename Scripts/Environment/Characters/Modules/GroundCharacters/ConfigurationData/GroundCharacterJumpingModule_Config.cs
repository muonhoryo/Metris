using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam_Temple.Characters.COP
{
    [Serializable]
    [CreateAssetMenu]
    public sealed class GroundCharacterJumpingModule_Config : ScriptableObject
    {
        public float BeforeFallingDisallowingDelay;
        public float BeforeAirJumpingDelay;
        public float AfterLandingDelay;
        public float JumpForce;
    }
}
