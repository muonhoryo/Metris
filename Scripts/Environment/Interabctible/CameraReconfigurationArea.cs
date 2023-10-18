using GameJam_Temple.Characters.COP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam_Temple.Level
{
    public sealed class CameraReconfigurationArea : MonoBehaviour
    {
        [SerializeField]
        private Rect MoveLimit;
        [SerializeField]
        private Vector2 MoveTriggerSize;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.TryGetComponent(out IPickUpingCharacter chr))
            {
                MainCameraBehaviour.Instance_.SetMoveTriggerSize(MoveTriggerSize);
                MainCameraBehaviour.Instance_.SetMoveLimit(MoveLimit);
            }
        }
    }
}
