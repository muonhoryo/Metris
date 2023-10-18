using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameJam_Temple.Characters.COP.IGroundMovingCharacter;
using GameJam_Temple.Exceptions;
using System;

namespace GameJam_Temple.Characters.COP
{
    public sealed class GroundCharacterMovingModule_FallingDependence : Module,IMovingModule
    {
        public event Action<int> StartMovingEvent=delegate { };
        public event Action StopMovingEvent=delegate { };

        [SerializeField]
        private Component GroundMovingModuleComponent;
        [SerializeField]
        private Component AirMovingModuleComponent;
        [SerializeField]
        private Component OwnerComponent;

        private IMovingModule GroundMovingModule;
        private IMovingModule AirMovingModule;
        private IGroundMovingCharacter Owner;

        private IMovingModule ActiveModule;
        private bool CanStartMoving=true;
        private bool CanStopMoving=true;

        private void ActivateModule(bool isFalling)
        {
            void ActivateModule_(IMovingModule module)
            {
                void StartMovingAction(int direction)
                {
                    StartMovingEvent(direction);
                }
                void StopMovingAction()
                {
                    StopMovingEvent();
                }
                ActiveModule.StartMovingEvent -= StartMovingAction;
                ActiveModule.StopMovingEvent -= StopMovingAction;
                ActiveModule.IsActive_ = false;
                ActiveModule = module;
                ActiveModule.StartMovingEvent += StartMovingAction;
                ActiveModule.StopMovingEvent += StopMovingAction;
                ActiveModule.IsActive_ = true;
            }
            if (isFalling)
            {
                if (ActiveModule != AirMovingModule)
                    ActivateModule_(AirMovingModule);
            }
            else
            {
                if (ActiveModule != GroundMovingModule)
                    ActivateModule_(GroundMovingModule);
            }
        }
        private void Awake()
        {
            if (GroundMovingModuleComponent == null)
                throw GameJam_Exception.GetNullModuleInitialization("GroundMovingModuleComponent");
            if (AirMovingModuleComponent == null)
                throw GameJam_Exception.GetNullModuleInitialization("AirMovingModuleComponent");
            if (OwnerComponent == null)
                throw GameJam_Exception.GetNullModuleInitialization("OwnerComponent");


            GroundMovingModule = GroundMovingModuleComponent as IMovingModule;
            if (GroundMovingModule==null)
                throw GameJam_Exception.GetWrondModuleType<IMovingModule>("GroundMovingModule");
            AirMovingModule = AirMovingModuleComponent as IMovingModule;
            if (AirMovingModule==null)
                throw GameJam_Exception.GetWrondModuleType<IMovingModule>("AirMovingModule");
            Owner = OwnerComponent as IGroundMovingCharacter;
            if (Owner == null)
                throw GameJam_Exception.GetWrondModuleType<IGroundMovingCharacter>("Owner");

            ActiveModule = GroundMovingModule;
            ActivateModule(true);
        }
        private void Start()
        {
            Owner.StartFallingEvent_ += (i) => ActivateModule(true);
            Owner.StartRisingEvent_ += (i) => ActivateModule(true);
            Owner.LandingEvent_ += (i) => ActivateModule(false);
        }

        public bool IsMoving_ => ActiveModule.IsMoving_;
        
        private bool CanStartMoving_ 
        { get => !IsMoving_ && CanStartMoving && ActiveModule.CanStartMoving_;
            set => CanStartMoving=value;
        }
        bool IMovingModule.CanStartMoving_
        {
            get => CanStartMoving_;
            set => CanStartMoving_ = value;
        }
        private bool CanStopMoving_
        { get => IsMoving_ && CanStopMoving && ActiveModule.CanStopMoving_;
            set => CanStopMoving = value;
        }
        bool IMovingModule.CanStopMoving_
        {
            get => CanStopMoving_;
            set => CanStopMoving_ = value;
        }

        void IMovingModule.StartMoving()
        {
            if (CanStartMoving_)
            {
                ActiveModule.StartMoving();
            }
        }

        void IMovingModule.StopMoving()
        {
            if (CanStopMoving_)
                ActiveModule.StopMoving();
        }
    }
}
