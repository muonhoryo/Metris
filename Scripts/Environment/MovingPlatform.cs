using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam_Temple.Level
{
    public sealed class MovingPlatform : MonoBehaviour
    {
        private List<GameObject> StandingOnObjects=new List<GameObject>();
        [SerializeField]
        private float MovingSpeed;
        [SerializeField]
        private Vector2[] MovingPath = new Vector2[2];

        private Vector2 NextPoint;
        private Vector2 MovingDirection;
        private int CurrentTargetIndex;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.TryGetComponent(out Rigidbody2D rgbody)&&
                !StandingOnObjects.Contains(collision.gameObject))
            {
                StandingOnObjects.Add(collision.gameObject);
                collision.gameObject.transform.SetParent(transform);
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.gameObject.TryGetComponent(out Rigidbody2D rgbody)&&
                StandingOnObjects.Contains(collision.gameObject))
            {
                StandingOnObjects.Remove(collision.gameObject);
                collision.gameObject.transform.SetParent(Registry.Scenetransform);
            }
        }
        private void FixedUpdate()
        {
            float distance = (NextPoint - (Vector2)transform.position).magnitude;
            if (distance < MovingSpeed)
            {
                transform.position = NextPoint;
                UpdateTarget(CurrentTargetIndex == 0 ? 1 : 0);
            }
            else
            {
                transform.position += (Vector3)(MovingDirection * MovingSpeed);
            }
        }
        private void UpdateTarget(int index)
        {
            NextPoint = MovingPath[index];
            MovingDirection = (NextPoint - (Vector2)transform.position).normalized;
            CurrentTargetIndex = index;
        }
        private void Start()
        {
            UpdateTarget(0);
        }
    }
}
