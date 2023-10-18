
using GameJam_Temple.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static GameJam_Temple.Characters.COP.IJumpingCharacter;

namespace GameJam_Temple.Characters.COP
{
    public sealed class MainHeroJumpingModule : Module,IJumpingModule
    {
        [SerializeField]
        private Rigidbody2D Rgbody;
        [SerializeField]
        private Component OwnerComponent;
        [SerializeField]
        private GroundCharacterJumpingModule_Config Config;

        private IJumpingCharacter Owner;

        private bool IsAllowedJump = true;
        private bool IsJump = false;
        private bool IsSecondJumped = false;
        private bool CanJump__ = true;
        public bool CanJump_
        {
            get => IsAllowedJump && CanJump__;
            set => CanJump__ = value;
        }

        public event Action JumpEvent = delegate { };
        public event Action JumpingHasBeenAcceptedEvent = delegate { };

        private Coroutine CurrentJumpDelay;


        private IEnumerator FallingDisallowing()
        {
            void JumpAction()
            {
                JumpEvent -= JumpAction;
                StopCoroutine(CurrentJumpDelay);
            }
            JumpEvent += JumpAction;
            yield return new WaitForSeconds(Config.BeforeFallingDisallowingDelay);
            JumpEvent-= JumpAction;
            IsAllowedJump = false;
            IsSecondJumped = false;
        }
        private void StartFallingDelay()
        {
            if (!IsJump)
            {
                if (CurrentJumpDelay != null)
                    StopCoroutine(CurrentJumpDelay);
                CurrentJumpDelay = StartCoroutine(FallingDisallowing());
            }
        }
        private IEnumerator JumpDelay()
        {
            IsAllowedJump = false;
            yield return new WaitForSeconds(Config.BeforeFallingDisallowingDelay);
            RequestJumpAccepting();
        }
        private void StartJumpDelay()
        {
            if (CurrentJumpDelay != null)
                StopCoroutine(CurrentJumpDelay);
            CurrentJumpDelay = StartCoroutine(JumpDelay());

        }
        private IEnumerator LandingDelay()
        {
            IsAllowedJump = false;
            yield return new WaitForSeconds(Config.AfterLandingDelay);
            RequestJumpAccepting();
        }
        private void StartLandingDelay()
        {
            if (CurrentJumpDelay != null)
                StopCoroutine(CurrentJumpDelay);
            CurrentJumpDelay = StartCoroutine(LandingDelay());
        }
        private void RequestJumpAccepting()
        {
            bool canJump = true;
            if (Owner.CurrentFallingState_.IsInAir())
            {
                if (IsSecondJumped)
                {
                    canJump = false;
                }
            }
            else
            {
                IsSecondJumped = false;
                IsJump = false;
            }

            if (canJump)
                AcceptJump();
        }
        private void AcceptJump()
        {
            IsAllowedJump = true;
            JumpingHasBeenAcceptedEvent();
        }

        private void InternalJump()
        {
            JumpEvent();
            if (Owner.CurrentFallingState_.IsInAir())
                IsSecondJumped = true;
            else
                IsJump = true;
            StartJumpDelay();
            Rgbody.velocity = new Vector2(Rgbody.velocity.x, 0);
            Rgbody.AddForce(Config.JumpForce *Vector2.up, ForceMode2D.Impulse);
        }

        private void Awake()
        {
            if (OwnerComponent == null)
                throw GameJam_Exception.GetNullModuleInitialization("OwnerComponent");
            if (Rgbody == null)
                throw GameJam_Exception.GetNullModuleInitialization("Rgbody");
            if (Config == null)
                throw GameJam_Exception.GetNullModuleInitialization("Config");

            Owner = OwnerComponent as IJumpingCharacter;
            if (Owner == null)
                throw GameJam_Exception.GetWrondModuleType<IJumpingCharacter>("Owner");
        }
        private void Start()
        {
            Owner.StartFallingEvent_ += (i) => StartFallingDelay();
            Owner.StartRisingEvent_ += (i) => StartFallingDelay();
            Owner.LandingEvent_ += (i) => StartLandingDelay();
        }

        public void Jump()
        {
            if (CanJump_)
                InternalJump();
        }
    }
}
