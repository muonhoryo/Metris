using GameJam_Temple.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static GameJam_Temple.Characters.COP.IGroundMovingCharacter;

namespace GameJam_Temple.Characters.COP
{
    public sealed class MainHeroFiewDirectionModule_Simple : Module,IFiewDirectionModule
    {
        public event Action<int> ChangeFiewDirectionEvent = delegate { };

        [SerializeField]
        private SpriteRenderer BaseSprite;

        private int FiewDirection = 1;
        private bool CanChangeFiewDirection = true;

        int IFiewDirectionModule.FiewDirection_ => FiewDirection;
        public bool CanChangeFiewDirection_
        {
            get => CanChangeFiewDirection;
            set => CanChangeFiewDirection = value;
        }

        public void SetFiewDirection(int direction)
        {
            if (CanChangeFiewDirection)
            {
                if (direction == 0)
                    throw new GameJam_Exception("FiewDirection cannot be zero.");

                direction = direction.Sign();
                if (FiewDirection != direction)
                {
                    FiewDirection = direction;
                    BaseSprite.flipX = direction < 0;
                    ChangeFiewDirectionEvent(direction);
                }
            }
        }
        private void Awake()
        {
            if (BaseSprite == null)
                throw GameJam_Exception.GetNullModuleInitialization("BaseSprite");
        }

        protected override bool CanTurnActivityFromOutside_ => false;
    }
}
