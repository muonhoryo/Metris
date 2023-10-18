using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam_Temple.Level
{
    public sealed class Trampoline : MonoBehaviour
    {
        [SerializeField]
        private float ForceLevel;
        [SerializeField]
        private float CoolDownDelay;
        [SerializeField]
        private float MaxForceLevel;
        [SerializeField]
        private float MinForceLevel;

        private Vector2 AddedForce;
        private bool CanTramp = true;
        private void Start()
        {
            AddedForce = Vector2.up * ForceLevel;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(CanTramp&&collision.gameObject.TryGetComponent(out Rigidbody2D rgBody))
            {
                Vector2 force = new Vector2(AddedForce.x,
                    Mathf.Clamp(AddedForce.y + Mathf.Abs(rgBody.velocity.y), MinForceLevel, MaxForceLevel));
                rgBody.velocity = new Vector2(rgBody.velocity.x,0);
                rgBody.AddForce(force,ForceMode2D.Impulse);
                StartCoroutine(CoolDown());
            }
        }
        IEnumerator CoolDown()
        {
            CanTramp = false;
            yield return new WaitForSeconds(CoolDownDelay);
            CanTramp = true;
        }
    }
}
