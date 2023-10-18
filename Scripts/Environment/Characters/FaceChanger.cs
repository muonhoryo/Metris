using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam_Temple.Characters.COP
{
    public sealed class FaceChanger : Module
    {
        [SerializeField]
        private Sprite[] Faces;
        [SerializeField]
        private SpriteRenderer BaseSprite;
        private void ChangeFace()
        {
            BaseSprite.sprite = Faces[Random.Range(0, Faces.Length)];
        }
        private void Start()
        {
            (Registry.Maincharacter as IDyingCharacter).DeathEvent_ += ChangeFace;
            ChangeFace();
        }
        protected override bool CanTurnActivityFromOutside_ =>false;
    }
}
