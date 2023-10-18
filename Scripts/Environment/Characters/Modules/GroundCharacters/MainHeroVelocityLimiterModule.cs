using GameJam_Temple.Exceptions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

namespace GameJam_Temple.Characters.COP
{
    public sealed class MainHeroVelocityLimiterModule : Module
    {
        public enum LimitMode 
        {
            XY,
            X,
            Y
        }
        [SerializeField]
        private Rigidbody2D Rgbody;
        [SerializeField]
        private float LimitCalculationModifier=1;
        [SerializeField]
        private LimitMode Mode=LimitMode.XY;
        [SerializeField]
        private Component OwnerComponent;

        private IGroundMovingCharacter Owner;

        private void Awake()
        {
            if (Rgbody == null)
                throw GameJam_Exception.GetNullModuleInitialization("Rgbody");
            if (OwnerComponent == null)
                throw GameJam_Exception.GetNullModuleInitialization("OwnerComponent");

            Owner = OwnerComponent as IGroundMovingCharacter;
            if (Owner == null)
                throw GameJam_Exception.GetWrondModuleType<IGroundMovingCharacter>("Owner");
        }
        private void FixedUpdate()
        {
            float limit = Owner.MoveSpeed_ * LimitCalculationModifier;
            if (Mode == LimitMode.XY)
            {
                if (Rgbody.velocity.magnitude > limit)
                    Rgbody.velocity = Rgbody.velocity.normalized * limit;
            }
            else if (Mode == LimitMode.X)
            {
                if (Mathf.Abs(Rgbody.velocity.x) > limit)
                    Rgbody.velocity = new Vector2(limit * Mathf.Sign(Rgbody.velocity.x), Rgbody.velocity.y);
            }
            else
            {
                if (Mathf.Abs(Rgbody.velocity.y) > limit)
                    Rgbody.velocity = new Vector2(Rgbody.velocity.x, limit * Mathf.Sign(Rgbody.velocity.y));
            }
        }
        protected override bool CanTurnActivityFromOutside_ =>false;
    }
}
