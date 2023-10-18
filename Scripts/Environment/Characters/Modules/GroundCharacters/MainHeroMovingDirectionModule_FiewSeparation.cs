using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuonhoryoLibrary;
using static GameJam_Temple.Characters.COP.IGroundMovingCharacter;
using GameJam_Temple.Exceptions;

namespace GameJam_Temple.Characters.COP
{
    public sealed class MainHeroMovingDirectionModule_FiewSeparation : Module,IMovingDirectionModule
    {
        public event Action<int> ChangeMovingDirectionEvent = delegate { };
        int IMovingDirectionModule.MovingDirection_ => MovingDirection;
        public bool CanChangeMovingDirection_
        {
            get => CanChangeMovingDirection;
            set => CanChangeMovingDirection = value;
        }
        private int MovingDirection = 1;
        private bool CanChangeMovingDirection = true;

        public void SetMovingDirection(int direction)
        {
            if (CanChangeMovingDirection)
            {
                if (direction == 0)
                    throw new GameJam_Exception("MovingDirection cannot be zero.");

                direction = direction.Sign();
                if (MovingDirection != direction)
                {
                    MovingDirection = direction;
                    ChangeMovingDirectionEvent(direction);
                }
            }
        }

        protected override bool CanTurnActivityFromOutside_ => false;
    }
}
