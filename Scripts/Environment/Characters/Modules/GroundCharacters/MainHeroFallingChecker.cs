using MuonhoryoLibrary.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GameJam_Temple.Characters.COP.IGroundMovingCharacter;

namespace GameJam_Temple.Characters.COP
{
    public sealed class MainHeroFallingChecker : Module,IFallingCheckingModule
    {
        public event Action<IFallingCheckingModule.LandingInfo> LandingEvent = delegate { };
        public event Action<IFallingCheckingModule.FallingStartInfo> StartFallingEvent = delegate { };
        public event Action<IFallingCheckingModule.GroundFreeRisingInfo> StartRisingEvent = delegate { };
        public event Action<bool> ChangeVerticalMovingDirectionEvent = delegate { };
        public IFallingCheckingModule.FallingState CurrentFallingState_ { get; private set; }
        public bool IsUp_ { get; private set; } = false;

        private float PrevHeight = 0;
        private bool WasCollisedGround = false;
        private float MinCollisionPointDistance = 10000;
        [SerializeField]
        private Rigidbody2D RGBody;
        [SerializeField]
        private Collider2D GroundSubChecker;
        [SerializeField]
        private float GroundDetectionMinCos;

        private void FoundGround()
        {
            CurrentFallingState_ = IFallingCheckingModule.FallingState.StandingOnGround;
            GroundCheckingAction = GroundStayUpdAction;
            LandingEvent(new(RGBody.velocity));
        }
        private void LostGround()
        {
            void ChangeDirectionAction(bool hasStartRising)
            {
                ChangeDirection(hasStartRising, true);
            }
            void ChangeDirection(bool hasStartRising, bool wasNotStayOnGround)
            {
                if (hasStartRising)
                {
                    CurrentFallingState_ = IFallingCheckingModule.FallingState.GroundFreeRising;
                    StartRisingEvent(new IFallingCheckingModule.GroundFreeRisingInfo(wasNotStayOnGround));
                }
                else
                {
                    CurrentFallingState_ = IFallingCheckingModule.FallingState.Falling;
                    StartFallingEvent(new IFallingCheckingModule.FallingStartInfo(wasNotStayOnGround));
                }
            }
            ChangeVerticalMovingDirectionEvent += ChangeDirectionAction;
            void ResetEvent(IFallingCheckingModule.LandingInfo i)
            {
                ChangeVerticalMovingDirectionEvent -= ChangeDirectionAction;
                LandingEvent -= ResetEvent;
            }
            LandingEvent += ResetEvent;
            GroundCheckingAction = FallingFixedUpdateAction;
            ChangeDirection(IsUp_, false);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.collider.gameObject.layer.IsInLayerMask(GlobalConstants.Config.GroundLayerMask))
            {
                if (WasCollisedWithAGround(collision))
                {
                    if (!WasCollisedGround)
                        WasCollisedGround = true;
                    float distance = collision.contacts.Min((contact) => MathF.Abs(transform.position.x - contact.point.x));
                    if (distance < MinCollisionPointDistance)
                    {
                        MinCollisionPointDistance = distance;
                    }
                }
            }
        }
        /// <summary>
        /// If collised with a ground, return true.
        /// </summary>
        /// <param name="collision"></param>
        /// <returns></returns>
        private bool WasCollisedWithAGround(Collision2D collision)
        {
            foreach (var contact in collision.contacts)
            {
                float groundDot = Vector2.Dot(contact.normal, Vector2.up);
                if (groundDot > GroundDetectionMinCos)
                {
                    return true;
                }
            }
            return false;
        }
        private bool HaveAnyGround()
        {
            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(GlobalConstants.Config.GroundLayerMask);
            filter.useTriggers = false;
            Collider2D[] i = new Collider2D[1];
            return Physics2D.OverlapCollider(GroundSubChecker, filter, i) > 0;
        }
        private void FixedUpdate()
        {
            float currentHeight = transform.position.y;
            HeightHandlingAction(currentHeight);
            PrevHeight = transform.position.y;

            GroundCheckingAction();
            WasCollisedGround = false;
            MinCollisionPointDistance = 10000;
        }

        private Action<float> HeightHandlingAction;
        private void RiseHandling(float currentHeight)
        {
            if (currentHeight < PrevHeight)
            {
                IsUp_ = false;
                ChangeVerticalMovingDirectionEvent(false);
                HeightHandlingAction = DescentHandling;
            }
        }
        private void DescentHandling(float currentHeight)
        {
            if (currentHeight > PrevHeight)
            {
                IsUp_ = true;
                ChangeVerticalMovingDirectionEvent(true);
                HeightHandlingAction = RiseHandling;
            }
        }

        private Action GroundCheckingAction;
        private void GroundStayUpdAction()
        {
            if (!WasCollisedGround &&!HaveAnyGround())
            {
                LostGround();
            }
        }
        private void FallingFixedUpdateAction()
        {
            if (WasCollisedGround && HaveAnyGround())
            {
                FoundGround();
            }
        }

        private void Awake()
        {
            if (RGBody == null)
                throw Exceptions.GameJam_Exception.GetNullModuleInitialization("RGBody");
            if (GroundSubChecker == null)
                throw Exceptions.GameJam_Exception.GetNullModuleInitialization("GroundSubChecker");

            HeightHandlingAction = DescentHandling;
            GroundCheckingAction = FallingFixedUpdateAction;

            if (!enabled)
                enabled = true;
        }
        private void Start()
        {
            PrevHeight = transform.position.y;
        }
        private void OnDisable()
        {
            enabled = true;
        }

        protected sealed override bool CanTurnActivityFromOutside_ => false;
    }
}
