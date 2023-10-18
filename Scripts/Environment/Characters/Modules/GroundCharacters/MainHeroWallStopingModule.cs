using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam_Temple.Characters.COP
{
    public sealed class MainHeroWallStopingModule : Module
    {
        [SerializeField]
        private Component OwnerComponent;

        private IGroundMovingCharacter Owner;

        private void FoundWallAction() 
        {
            Owner.StopMoving();
        }
        private void SubscribeOnWallFinding(int direction)
        {
            if (direction > 0)
            {
                Owner.FoundWallAtRightSideEvent_ += FoundWallAction;
            }
            else
            {
                Owner.FoundWallAtLeftSideEvent_ += FoundWallAction;
            }
        }
        private void UnsubscribeFromWallFinding(int direction)
        {
            if (direction > 0)
            {
                Owner.FoundWallAtRightSideEvent_-= FoundWallAction;
            }
            else
            {
                Owner.FoundWallAtLeftSideEvent_-= FoundWallAction;
            }
        }
        private void ChangeMovingDirectionAction(int newDirection)
        {
            SubscribeOnWallFinding(newDirection);
            UnsubscribeFromWallFinding(newDirection * -1);
        }

        private void Awake()
        {
            if (OwnerComponent == null)
                throw Exceptions.GameJam_Exception.GetNullModuleInitialization("OwnerComponent");

            Owner=OwnerComponent as IGroundMovingCharacter;
            if (Owner == null)
                throw Exceptions.GameJam_Exception.GetWrondModuleType<IGroundMovingCharacter>("Owner");
        }
        private void Start()
        {
            Owner.StartMovingEvent_ += (direction) => SubscribeOnWallFinding(direction);
            Owner.StartMovingEvent_ += (i) => Owner.ChangeMovingDirectionEvent_ += ChangeMovingDirectionAction;
            Owner.StopMovingEvent_ += () => Owner.ChangeMovingDirectionEvent_ -= ChangeMovingDirectionAction;
        }
    }
}
