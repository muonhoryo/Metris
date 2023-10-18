using MuonhoryoLibrary.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam_Temple.Level
{
    public sealed class Turret : MonoBehaviour
    {
        [SerializeField]
        private GameObject BulletPrefab;
        [SerializeField]
        private float ShootingDelay;
        [SerializeField]
        private Vector2 BulletStart;
        [SerializeField]
        private float BulletSpeed;

        private void Start()
        {
            StartCoroutine(ShootCycle());
        }
        IEnumerator ShootCycle()
        {
            while (true)
            {
                var bullet = Instantiate(BulletPrefab,
                    transform.position+ (Vector3)(BulletStart.AngleOffset(transform.eulerAngles.z)),
                    Quaternion.Euler(0, 0, transform.eulerAngles.z));
                var bulletComp = bullet.GetComponent<Bullet>();
                bulletComp.BulletSpeed=BulletSpeed;
                bulletComp.Owner = gameObject;
                yield return new WaitForSeconds(ShootingDelay);
            }
        }
    }
}
