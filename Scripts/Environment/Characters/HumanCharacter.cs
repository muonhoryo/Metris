using GameJam_Temple.Characters.COP;
using GameJam_Temple.Exceptions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameJam_Temple.Characters.COP.IGroundMovingCharacter;
using static GameJam_Temple.Characters.COP.IJumpingCharacter;

namespace GameJam_Temple.Characters
{
    public interface IHumanCharacter : IJumpingCharacter, IBlockDestroyingCharacter, IColorChangableCharacter,
        IPickUpingCharacter,IDyingCharacter{ }
    public sealed class HumanCharacter : MonoBehaviour, IHumanCharacter
    {
        [SerializeField]
        private Component MovingModuleComponent;
        [SerializeField]
        private Component MovingDirectionModuleComponent;
        [SerializeField]
        private Component FiewDirectionModuleComponent;
        [SerializeField]
        private Component FallingCheckerComponent;
        [SerializeField]
        private Component WallCheckerComponent;
        [SerializeField]
        private Component SpeedModuleComponent;
        [SerializeField]
        private Component JumpingModuleComponent;
        [SerializeField]
        private Component BlockDestroyingComponent;
        [SerializeField]
        private Component ColorChangingModuleComponent;
        [SerializeField]
        private Component PickUpingModuleComponent;
        [SerializeField]
        private Component DyingModuleComponent;

        private IMovingModule MovingModule;
        private IMovingDirectionModule MovingDirectionModule;
        private IFiewDirectionModule FiewDirectionModule;
        private IFallingCheckingModule FallingChecker;
        private IWallCheckingModule WallChecker;
        private ISpeedModule SpeedModule;
        private IJumpingModule JumpingModule;
        private IBlockDestroyingCharacter.IBlockDestroyingModule BlockDestroingModule;
        private IColorChangableCharacter.IColorChangingModule ColorChangingModule;
        private IPickUpingCharacter.IPickUpingModule PickUpingModule;
        private IDyingCharacter.IDyingModule DyingModule;

        IMovingModule IGroundMovingCharacter.MovingModule_ => MovingModule;
        IMovingDirectionModule IGroundMovingCharacter.MovingDirChangingModule_ => MovingDirectionModule;
        IFiewDirectionModule IGroundMovingCharacter.FiewDirectionChangingModule_ => FiewDirectionModule;
        IFallingCheckingModule IGroundMovingCharacter.FallingCheckingModule_ => FallingChecker;
        IWallCheckingModule IGroundMovingCharacter.WallChecker_ => WallChecker;
        ISpeedModule IGroundMovingCharacter.SpeedModule_ => SpeedModule;
        IJumpingModule IJumpingCharacter.JumpingModule_ => JumpingModule;
        IBlockDestroyingCharacter.IBlockDestroyingModule IBlockDestroyingCharacter.BlockDestroyingModule_ =>
            BlockDestroingModule;
        IColorChangableCharacter.IColorChangingModule IColorChangableCharacter.ColorChangingModule_ =>
            ColorChangingModule;
        IPickUpingCharacter.IPickUpingModule IPickUpingCharacter.PickUpingModule_ =>
            PickUpingModule;
        IDyingCharacter.IDyingModule IDyingCharacter.DyingModule_ => DyingModule;


        private void Awake()
        {
            if (MovingModuleComponent == null)
                throw GameJam_Exception.GetNullModuleInitialization("MovingModuleComponent");
            if (MovingDirectionModuleComponent == null)
                throw GameJam_Exception.GetNullModuleInitialization("MovingDirectionModuleComponent");
            if (FiewDirectionModuleComponent == null)
                throw GameJam_Exception.GetNullModuleInitialization("FiewDirectionModuleComponent");
            if (FallingCheckerComponent == null)
                throw GameJam_Exception.GetNullModuleInitialization("FallingCheckerComponent");
            if (WallCheckerComponent == null)
                throw GameJam_Exception.GetNullModuleInitialization("WallCheckerComponent");
            if (SpeedModuleComponent == null)
                throw GameJam_Exception.GetNullModuleInitialization("SpeedModuleComponent");
            if (JumpingModuleComponent == null)
                throw GameJam_Exception.GetNullModuleInitialization("JumpingModuleComponent");
            if (DyingModuleComponent == null)
                throw GameJam_Exception.GetNullModuleInitialization("DyingModuleComponent");

            MovingModule = MovingModuleComponent as IMovingModule;
            if (MovingModule == null)
                throw GameJam_Exception.GetWrondModuleType<IMovingModule>("MovingModule");
            MovingDirectionModule = MovingDirectionModuleComponent as IMovingDirectionModule;
            if (MovingDirectionModule == null)
                throw GameJam_Exception.GetWrondModuleType<IMovingDirectionModule>("MovingDirectionModule");
            FiewDirectionModule = FiewDirectionModuleComponent as IFiewDirectionModule;
            if (FiewDirectionModule == null)
                throw GameJam_Exception.GetWrondModuleType<IFiewDirectionModule>("FiewDirectionModule");
            FallingChecker = FallingCheckerComponent as IFallingCheckingModule;
            if (FallingChecker == null)
                throw GameJam_Exception.GetWrondModuleType<IFallingCheckingModule>("FallingChecker");
            WallChecker = WallCheckerComponent as IWallCheckingModule;
            if (WallChecker == null)
                throw GameJam_Exception.GetWrondModuleType<IWallCheckingModule>("WallChecker");
            SpeedModule = SpeedModuleComponent as ISpeedModule;
            if (SpeedModule == null)
                throw GameJam_Exception.GetWrondModuleType<ISpeedModule>("SpeedModule");
            JumpingModule = JumpingModuleComponent as IJumpingModule;
            if (JumpingModule == null)
                throw GameJam_Exception.GetWrondModuleType<IJumpingModule>("JumpingModule");

            BlockDestroingModule = BlockDestroyingComponent as IBlockDestroyingCharacter.IBlockDestroyingModule;
            ColorChangingModule = ColorChangingModuleComponent as IColorChangableCharacter.IColorChangingModule;
            PickUpingModule = PickUpingModuleComponent as IPickUpingCharacter.IPickUpingModule;
            DyingModule = DyingModuleComponent as IDyingCharacter.IDyingModule;
        }
    }
}
