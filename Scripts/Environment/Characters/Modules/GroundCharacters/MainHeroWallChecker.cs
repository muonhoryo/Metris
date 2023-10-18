using MuonhoryoLibrary.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameJam_Temple.Characters.COP.IGroundMovingCharacter;

namespace GameJam_Temple.Characters.COP
{
    public sealed class MainHeroWallChecker : Module,IWallCheckingModule
    {
        public event Action FoundWallAtRightSideEvent = delegate { };
        public event Action FoundWallAtLeftSideEvent = delegate { };
        public event Action LostWallAtRightSideEvent = delegate { };
        public event Action LostWallAtLeftSideEvent = delegate { };

        [SerializeField]
        private float WallDetectionMinCos;
        [SerializeField]
        private float WallDetectionMinWallHeight;

        public bool IsThereLeftWall_ { get; private set; } = false;
        public bool IsThereRightWall_ { get; private set; } = false;
        private bool WasCollisedByRight = false;
        private bool WasCollisedByLeft = false;

        public bool HasWallAtDirection(int direction) =>
             direction > 0 ? IsThereRightWall_ : IsThereLeftWall_;

        private void FixedUpdate()
        {
            if (IsThereLeftWall_ != WasCollisedByLeft)
            {
                if (WasCollisedByLeft)
                    FoundWallAtLeftSideEvent();
                else
                    LostWallAtLeftSideEvent();

                IsThereLeftWall_ = WasCollisedByLeft;
            }
            if (IsThereRightWall_ != WasCollisedByRight)
            {
                if (WasCollisedByRight)
                    FoundWallAtRightSideEvent();
                else
                    LostWallAtRightSideEvent();

                IsThereRightWall_ = WasCollisedByRight;
            }
            if (WasCollisedByLeft)
                WasCollisedByLeft = false;
            if (WasCollisedByRight)
                WasCollisedByRight = false;
        }
        /// <summary>
        /// Return 1, if collised with a wall on right side. -1, if on left side.
        /// Return 0, if collised with not a wall.
        /// </summary>
        /// <param name="collision"></param>
        /// <returns></returns>
        private int WasCollisedWithAWall(Collision2D collision)
        {
            foreach (var contact in collision.contacts)
            {
                Vector2 dir = contact.point.x < transform.position.x ? Vector2.left : Vector2.right;
                float dot = Math.Abs(Vector2.Dot(contact.normal, dir));
                if (dot > WallDetectionMinCos&&
                    transform.position.y-contact.point.y< WallDetectionMinWallHeight)
                {
                    if (dir.x > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            return 0;
        }
        private void OnCollisionStay2D(Collision2D collision)
        {
            if ((!WasCollisedByLeft || !WasCollisedByRight) &&
                collision.collider.gameObject.layer.IsInLayerMask(GlobalConstants.Config.GroundLayerMask))
            {
                int dir = WasCollisedWithAWall(collision);
                if (dir > 0 && !WasCollisedByRight)
                {
                    WasCollisedByRight = true;
                }
                else if (dir < 0 && !WasCollisedByLeft)
                {
                    WasCollisedByLeft = true;
                }
            }
        }
        private void Awake()
        {
            if (!enabled)
                enabled = true;
        }
        private void OnDisable()
            => enabled = true;

        protected sealed override bool CanTurnActivityFromOutside_ => false;
    }
}
