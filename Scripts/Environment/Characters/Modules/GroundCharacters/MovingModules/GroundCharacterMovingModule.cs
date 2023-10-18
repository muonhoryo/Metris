using GameJam_Temple.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameJam_Temple.Characters.COP.IGroundMovingCharacter;

namespace GameJam_Temple.Characters.COP
{
    public abstract class GroundCharacterMovingModule : Module,IMovingModule
    {
        public event Action<int> StartMovingEvent = delegate { };
        public event Action StopMovingEvent = delegate { };
        public float MoveSpeed_ => Owner.MoveSpeed_;
        public bool IsMoving_ { get; private set; } = false;
        public bool CanStartMoving_
        {
            get=> CanStartMoving && !IsMoving_ && CanStartMoving_AdditionalConditions;
            set => CanStartMoving = value;
        }
        protected virtual bool CanStartMoving_AdditionalConditions { get => true; }
        public bool CanStopMoving_
        {
            get => CanStopMoving && IsMoving_;
            set => CanStopMoving = value;
        }

        [SerializeField]
        private Component OwnerComponent;
        [SerializeField]
        private Rigidbody2D Rigidbody;

        protected IGroundMovingCharacter Owner { get; private set; }
        protected Rigidbody2D Rigidbody_ => Rigidbody;
        private bool CanStartMoving = true;
        private bool CanStopMoving = true;

        public void StartMoving()
        {
            if (CanStartMoving_)
            {
                InternalStartMoving();
            }
        }
        public void StopMoving()
        {
            if (CanStopMoving_)
            {
                InternalStopMoving();
            }
        }
        private void InternalStartMoving()
        {
            IsMoving_ = true;
            enabled = true;
        }
        private void InternalStopMoving()
        {
            IsMoving_ = false;
            enabled = false;
        }

        private void Awake()
        {
            Owner = OwnerComponent as IGroundMovingCharacter;
            if (Owner == null)
                throw GameJam_Exception.GetWrondModuleType<IGroundMovingCharacter>("Owner");

            if (Rigidbody == null)
                throw GameJam_Exception.GetNullModuleInitialization("Rigidbody");

            DeactivateEvent += StopMoving;

            AwakeAction();
        }
        protected virtual void AwakeAction() { }
        private void FixedUpdate()
        {
            MovingAction(GetMovingDirection(),Owner.MovingDirection_,Owner.MoveSpeed_,
                MovingSpeedModifier_);
        }
        protected abstract void MovingAction(Vector2 direction, int horizontalDirection, float speed, float speedModifier);
        protected abstract Vector2 GetMovingDirection();
        protected abstract float MovingSpeedModifier_ { get; }
        private void OnEnable()
        {
            if (IsMoving_)
                StartMovingEvent(Owner.MovingDirection_);
            else
                enabled = false;
        }
        private void OnDisable()
        {
            if (!IsMoving_)
                StopMovingEvent();
            else
                enabled = true;
        }

        protected sealed override bool CanTurnActivityFromOutside_ => true;
    }
}
