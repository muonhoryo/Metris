using System;
using UnityEngine;
using static GameJam_Temple.Characters.COP.IGroundMovingCharacter;
using static GameJam_Temple.Characters.COP.IGroundMovingCharacter.IFallingCheckingModule;

namespace GameJam_Temple.Characters.COP
{
    public interface IGroundMovingCharacter
    {
        public interface IMovingModule:IModule
        {
            public event Action<int> StartMovingEvent;
            public event Action StopMovingEvent;
            public bool IsMoving_ { get; }
            /// <summary>
            /// Setter sets locker, getter can return false even locker is inactive.
            /// </summary>
            public bool CanStartMoving_ { get; set; }
            /// <summary>
            /// Setter sets locker, getter can return false even locker is inactive.
            /// </summary>
            public bool CanStopMoving_ { get; set; }
            public void StartMoving();
            public void StopMoving();
        }
        public interface IMovingDirectionModule : IModule
        {
            public event Action<int> ChangeMovingDirectionEvent;
            public int MovingDirection_ { get; }
            /// <summary>
            /// Setter sets locker, getter can return false even locker is inactive.
            /// </summary>
            public bool CanChangeMovingDirection_ { get; set; }
            public void SetMovingDirection(int direction);
        }
        public interface IFiewDirectionModule : IModule
        {
            public event Action<int> ChangeFiewDirectionEvent;
            public int FiewDirection_ { get; }
            /// <summary>
            /// Setter sets locker, getter can return false even locker is inactive.
            /// </summary>
            public bool CanChangeFiewDirection_ { get; set; }
            public void SetFiewDirection(int direction);
        }
        public interface IFallingCheckingModule : IModule
        {
            public enum FallingState
            {
                StandingOnGround,
                GroundFreeRising,
                Falling
            }
            public struct LandingInfo
            {
                public LandingInfo(Vector2 LandingForce)
                {
                    this.LandingForce = LandingForce;
                }
                public Vector2 LandingForce;
            }
            public struct FallingStartInfo
            {
                public FallingStartInfo(bool wasGroundFreeRising)
                {
                    WasGroundFreeRising = wasGroundFreeRising;
                }
                public bool WasGroundFreeRising;
            }
            public struct GroundFreeRisingInfo
            {
                public bool WasFalling;

                public GroundFreeRisingInfo(bool wasFalling)
                {
                    WasFalling = wasFalling;
                }
            }

            public event Action<LandingInfo> LandingEvent;
            public event Action<FallingStartInfo> StartFallingEvent;
            public event Action<GroundFreeRisingInfo> StartRisingEvent;
            /// <summary>
            /// If true - character is rises.
            /// </summary>
            public event Action<bool> ChangeVerticalMovingDirectionEvent;
            public FallingState CurrentFallingState_ { get; }
            public bool IsUp_ { get; }
        }
        public interface IWallCheckingModule : IModule
        {
            public event Action FoundWallAtRightSideEvent;
            public event Action FoundWallAtLeftSideEvent;
            public event Action LostWallAtRightSideEvent;
            public event Action LostWallAtLeftSideEvent;
            public bool HasWallAtDirection(int direction);
        }

        public event Action<int> StartMovingEvent_
        {
            add { MovingModule_.StartMovingEvent += value; }
            remove { MovingModule_.StartMovingEvent-= value; }
        }
        public event Action StopMovingEvent_
        {
            add { MovingModule_.StopMovingEvent += value; }
            remove { MovingModule_.StopMovingEvent -= value; }
        }
        public event Action<int> ChangeMovingDirectionEvent_
        {
            add { MovingDirChangingModule_.ChangeMovingDirectionEvent += value; }
            remove { MovingDirChangingModule_.ChangeMovingDirectionEvent -= value; }
        }
        public event Action<int> ChangeFiewDirectionEvent_
        {
            add { FiewDirectionChangingModule_.ChangeFiewDirectionEvent += value; }
            remove { FiewDirectionChangingModule_.ChangeFiewDirectionEvent -= value; }
        }
        public event Action<LandingInfo> LandingEvent_
        {
            add { FallingCheckingModule_.LandingEvent += value; }
            remove { FallingCheckingModule_.LandingEvent -= value; }
        }
        public event Action<FallingStartInfo> StartFallingEvent_
        {
            add { FallingCheckingModule_.StartFallingEvent += value; }
            remove { FallingCheckingModule_.StartFallingEvent -= value; }
        }
        public event Action<GroundFreeRisingInfo> StartRisingEvent_
        {
            add { FallingCheckingModule_.StartRisingEvent += value; }
            remove { FallingCheckingModule_.StartRisingEvent -= value; }
        }
        public event Action<bool> ChangeVerticalMovingDirectionEvent_
        {
            add { FallingCheckingModule_.ChangeVerticalMovingDirectionEvent += value; }
            remove { FallingCheckingModule_.ChangeVerticalMovingDirectionEvent -= value; }
        }
        public event Action FoundWallAtRightSideEvent_
        {
            add { WallChecker_.FoundWallAtRightSideEvent += value; }
            remove { WallChecker_.FoundWallAtRightSideEvent -= value; }
        }
        public event Action FoundWallAtLeftSideEvent_
        {
            add { WallChecker_.FoundWallAtLeftSideEvent += value; }
            remove { WallChecker_.FoundWallAtLeftSideEvent -= value; }
        }
        public event Action LostWallAtRightSideEvent_
        {
            add { WallChecker_.LostWallAtRightSideEvent += value; }
            remove { WallChecker_.LostWallAtRightSideEvent -= value; }
        }
        public event Action LostWallAtLeftSideEvent_
        {
            add { WallChecker_.LostWallAtLeftSideEvent += value; }
            remove { WallChecker_.LostWallAtLeftSideEvent -= value; }
        }
        public float MoveSpeed_ => SpeedModule_.Speed_.CurrentValue;
        public bool IsMoving_ => MovingModule_.IsMoving_;
        public bool CanStartMoving_
        { get => MovingModule_.CanStartMoving_ ; set => MovingModule_.CanStartMoving_ = value; }
        public bool CanStopMoving_
        { get => MovingModule_.CanStopMoving_; set => MovingModule_.CanStopMoving_ = value; }
        public int MovingDirection_ => MovingDirChangingModule_.MovingDirection_;
        public bool CanSetMovingDirection_
        { get => MovingDirChangingModule_.CanChangeMovingDirection_ ;
            set => MovingDirChangingModule_.CanChangeMovingDirection_ = value; }
        public int FiewDirection_ => FiewDirectionChangingModule_.FiewDirection_;
        public bool CanSetFiewDirection_
        { get => FiewDirectionChangingModule_.CanChangeFiewDirection_; 
            set => FiewDirectionChangingModule_.CanChangeFiewDirection_ = value; }
        public FallingState CurrentFallingState_ => FallingCheckingModule_.CurrentFallingState_;
        public bool IsUp_ => FallingCheckingModule_.IsUp_;
        public void StartMoving()
        {
            if (CanStartMoving_)
                MovingModule_.StartMoving();
        }
        public void StopMoving()
        {
            if (CanStopMoving_)
                MovingModule_.StopMoving();
        }
        public void SetDirection(int direction)
        {
            if (CanSetMovingDirection_)
                MovingDirChangingModule_.SetMovingDirection(direction);
        }
        public void SetFiewDirection(int direction)
        {
            if (CanSetFiewDirection_)
                FiewDirectionChangingModule_.SetFiewDirection(direction);
        }
        public bool HasWallAtDirection(int direction) => WallChecker_.HasWallAtDirection(direction);

        protected IMovingModule MovingModule_ { get; }
        protected IMovingDirectionModule MovingDirChangingModule_ { get; }
        protected IFiewDirectionModule FiewDirectionChangingModule_ { get; }
        protected IFallingCheckingModule FallingCheckingModule_ { get; }
        protected IWallCheckingModule WallChecker_ { get; }
        protected ISpeedModule SpeedModule_ { get; }
    }
}
