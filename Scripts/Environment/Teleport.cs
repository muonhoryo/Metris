using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam_Temple.Level
{
    public sealed class Teleport : MonoBehaviour
    {
        [SerializeField]
        private Vector2 TargetPoint;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == Registry.Maincharacter.gameObject)
            {
                Registry.Maincharacter.transform.position = TargetPoint;
            }
        }
    }
}
