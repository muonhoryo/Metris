using GameJam_Temple.Characters.COP;
using MuonhoryoLibrary.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam_Temple.Level
{
    public sealed class Bullet : MonoBehaviour
    {
        public float BulletSpeed;
        private Vector2 Direction;
        public GameObject Owner;
        [SerializeField]
        private CapsuleCollider2D Collider;
        private void Awake()
        {
            Direction = transform.eulerAngles.z.DirectionOfAngle();
        }
        private void FixedUpdate()
        {
            var cast = Physics2D.CapsuleCastAll(transform.position, Collider.size, Collider.direction,
                transform.eulerAngles.z, Direction,BulletSpeed,GlobalConstants.Config.BulletCollisionLayerMask);
            if (cast.Length>0)
            {
                bool isDestroy = false;
                foreach (var hit in cast)
                {
                    if (hit.collider.gameObject != Owner)
                    {
                        isDestroy = true;
                        if (hit.collider.gameObject.TryGetComponent(out IDyingCharacter chr))
                        {
                            chr.Death();
                            transform.position = hit.point;
                            break;
                        }
                    }
                }
                if (isDestroy)
                {
                    Destroy(gameObject);
                    return;
                }
            }
            transform.position += (Vector3)(Direction * BulletSpeed);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.TryGetComponent(out IDyingCharacter chr))
            {
                chr.Death();
                Destroy(gameObject);
            }
        }
    }
}
