using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam_Temple.Characters.COP
{
    public sealed class RotationAnimationModule : Module
    {
        [SerializeField]
        private float MinRotationSpeed;
        [SerializeField]
        private float RotationModifier;
        [SerializeField]
        private Transform BaseSprite;
        [SerializeField]
        private Rigidbody2D Rgbody;
        [SerializeField]
        private Component FallingCheckerModuleComponent;

        private IGroundMovingCharacter.IFallingCheckingModule FallingChecker;

        private int RotationSide = 1;
        private void Update()
        {
            if (Rgbody.velocity.x != 0)
            {
                float sign = Mathf.Sign(Rgbody.velocity.x);
                int intSign = sign > 0 ? 1 : -1;
                if (intSign != RotationSide)
                {
                    RotationSide=intSign;
                }
            }
            BaseSprite.transform.eulerAngles = new Vector3(0, 0,
                BaseSprite.transform.eulerAngles.z + 
                    ((MinRotationSpeed + Mathf.Abs(Rgbody.velocity.x)* RotationModifier) * -RotationSide));
        }
        private void OnDisable()
        {
            BaseSprite.transform.eulerAngles = new Vector3(0, 0,Mathf.Round(BaseSprite.transform.eulerAngles.z/90)*90);
        }
        private void Awake()
        {
            FallingChecker = FallingCheckerModuleComponent as IGroundMovingCharacter.IFallingCheckingModule;
            FallingChecker.StartFallingEvent += (i) => { if (!enabled) enabled = true; };
            FallingChecker.StartRisingEvent += (i) => { if (!enabled) enabled = true; };
            FallingChecker.LandingEvent += (i) => { if (enabled) enabled = false; };
        }
        protected override bool CanTurnActivityFromOutside_ => false;
    }
}
