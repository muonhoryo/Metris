using GameJam_Temple.Characters.COP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam_Temple.Characters
{
    public sealed class Enemy : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D Rgbody;
        [SerializeField]
        private ColorModule.Color Color;
        [SerializeField]
        private float MoveSpeed;
        [SerializeField]
        private SpriteRenderer ColorSprite;

        private int MovingDirection=1;
        private void Start()
        {
            ColorSprite.color = Color.GetUnityColor();
        }
        private void FixedUpdate()
        {
            MovingDirection = Registry.Maincharacter.transform.position.x > transform.position.x?1:-1;
            Rgbody.AddForce(Vector2.right*MovingDirection*MoveSpeed, ForceMode2D.Force);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.TryGetComponent(out IHumanCharacter chr))
            {
                if (chr.Color_ == Color)
                    Destroy(gameObject);
                else
                    chr.Death();
            }
        }
    }
}
