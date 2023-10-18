using GameJam_Temple.Characters.COP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam_Temple.Level
{
    public sealed class Spikes : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.TryGetComponent(out IDyingCharacter chr))
            {
                chr.Death();
            }
        }
    }
}
