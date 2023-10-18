using GameJam_Temple.Characters.COP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam_Temple.Characters
{
    public static class CharactersExtensions
    {
        public static bool IsInAir(this IGroundMovingCharacter.IFallingCheckingModule.FallingState fallingState) =>
            fallingState != IGroundMovingCharacter.IFallingCheckingModule.FallingState.StandingOnGround;
    }
}
