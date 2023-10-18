using GameJam_Temple.Exceptions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameJam_Temple.Characters.COP.IGroundMovingCharacter;

namespace GameJam_Temple.Characters.COP
{
    public sealed class GroundCharacterMovingModule_Ground : GroundCharacterMovingModule
    {
        [SerializeField]
        private float MoveSpeedModifier=1;

        protected override Vector2 GetMovingDirection() =>Vector2.right;
        protected override float MovingSpeedModifier_ => MoveSpeedModifier;
        protected override void MovingAction(Vector2 direction, int horizontalDirection, float speed,
            float speedModifier)
        {
            Rigidbody_.AddForce(speed * horizontalDirection * speedModifier * direction,ForceMode2D.Force);
        }
        protected override bool CanStartMoving_AdditionalConditions=> 
            !Owner.CurrentFallingState_.IsInAir() &&
                    !Owner.HasWallAtDirection(Owner.MovingDirection_);
        private void Start()
        {
            StopMovingEvent += () =>
            {
                Owner.StartFallingEvent_ -= StopMovingAction_Falling;
                Owner.StartRisingEvent_ -= StopMovingAction_Rising;
                if (IsActive_)
                {
                    Rigidbody_.velocity = Vector2.zero;
                }
            };
            StartMovingEvent += (int i) =>
            {
                SubscribeOn_FallingChecker();
            };
            Owner.LandingEvent_ += (i) =>
                { if (!IsMoving_) Rigidbody_.velocity = Vector2.zero; };
        }
        private void SubscribeOn_FallingChecker()
        {
            Owner.StartFallingEvent_ += StopMovingAction_Falling;
            Owner.StartRisingEvent_ += StopMovingAction_Rising;
        }
        private void StopMovingAction_Falling(IFallingCheckingModule.FallingStartInfo i) =>
            StopMoving();
        private void StopMovingAction_Rising(IFallingCheckingModule.GroundFreeRisingInfo i) =>
            StopMoving();
    }
}
