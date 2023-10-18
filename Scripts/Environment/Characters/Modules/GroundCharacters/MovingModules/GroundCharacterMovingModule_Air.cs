using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameJam_Temple.Characters.COP.IGroundMovingCharacter;

namespace GameJam_Temple.Characters.COP 
{
    public sealed class GroundCharacterMovingModule_Air : GroundCharacterMovingModule
    {
        [SerializeField]
        private float MoveSpeedModifier=1;

        protected override float MovingSpeedModifier_ => MoveSpeedModifier;
        protected override Vector2 GetMovingDirection() => Vector2.right;
        protected override void MovingAction(Vector2 direction, int horizontalDirection, float speed, float speedModifier)
        {
            Rigidbody_.AddForce(speed * direction * speedModifier * horizontalDirection * MovingSpeedModifier_,
                ForceMode2D.Force);
        }
        protected override bool CanStartMoving_AdditionalConditions =>
            !Owner.HasWallAtDirection(Owner.MovingDirection_)
            && Owner.CurrentFallingState_.IsInAir();
        private void Start()
        {
            StopMovingEvent += () =>
            {
                Owner.LandingEvent_ -= StopMovingAction_Land;
                Rigidbody_.velocity=new Vector2(0,Rigidbody_.velocity.y);
            };
            StartMovingEvent += (direction) =>
            {
                SubscribeOn_FallingChecker();
            };
        }
        private void SubscribeOn_FallingChecker()
        {
            Owner.LandingEvent_ += StopMovingAction_Land;
        }
        private void StopMovingAction_Land(IFallingCheckingModule.LandingInfo i) =>
            StopMoving();
    }
}

