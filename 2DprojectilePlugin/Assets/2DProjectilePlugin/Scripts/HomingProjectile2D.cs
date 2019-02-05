using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UpGamesWeapon2D.Math;

namespace UpGamesWeapon2D.Weapons
{
    public class HomingProjectile2D : Projectile2D
    {
        Transform target;
        public float homingRange;
        public float homingAccuracy;
        [Tooltip("Projectiles lifetime in seconds!")]
        public float projectileLifetime = 10f;
        float destroyTimer;
        public LayerMask targetLayer;
        public override void Move()
        {
            base.Move();
            if(target == null)
            {
                StartCoroutine(FindTarget());
            }
            else
            {
                StopCoroutine(FindTarget());
                transform.rotation = Quaternion.Slerp(transform.rotation, MathOperations.LookAt2D(target.position, transform.position, -90), homingAccuracy * Time.deltaTime);
            }
            destroyTimer -= Time.deltaTime;
            if(destroyTimer <= 0)
            {
                Destruction();
            }
        }
        IEnumerator FindTarget()
        {           
            Collider2D[] possibleTargets = Physics2D.OverlapCircleAll(transform.position, homingRange, targetLayer);
            if (possibleTargets.Length > 0)
            {
                target = MathOperations.FindClosestTarget(possibleTargets, transform);
            }
            yield return new WaitForSeconds(1.5f);
        }
        private void OnDrawGizmos()
        {
            Color c = Color.white;
            c.a = 0.1f;
            Gizmos.color = c; 
            Gizmos.DrawWireSphere(transform.position, homingRange);
        }
        public override void OnEnable()
        {
            base.OnEnable();
            if (GetComponent<TrailRenderer>() != null)
                GetComponent<TrailRenderer>().Clear();
            destroyTimer = projectileLifetime;
        }
        public void OnDisable()
        {
            target = null;
            if(GetComponent<TrailRenderer>() != null)
                GetComponent<TrailRenderer>().Clear();
        }
    }  
}

