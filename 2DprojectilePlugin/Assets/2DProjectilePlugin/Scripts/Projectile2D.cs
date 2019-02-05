using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UpGamesWeapon2D.Math;

namespace UpGamesWeapon2D.Weapons
{
    public class Projectile2D : MonoBehaviour
    {
        public GameObject bulletHitEffect;
        [HideInInspector]
        public float bulletSpeed;
        public bool destroyInvisibleBullet = true;
        public bool destroyOnImpact = true;
        public bool spawnHitEffectOnDestruction = true;
        public LayerMask hittable;

        [HideInInspector]
        public Rigidbody2D rb;
        public virtual void OnEnable()
        {          
            if(rb == null)
            {
                rb = gameObject.AddComponent<Rigidbody2D>();
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.gravityScale = 0;
            }          
        }
        public void Update()
        {
            Move();
        }
        public virtual void Move()
        {
            Vector2 pos = transform.position + transform.up * bulletSpeed * Time.deltaTime;
            transform.position = pos;
        }
        public void OnBecameInvisible()
        {
            if (destroyInvisibleBullet)
            {
                gameObject.SetActive(false);
            }
        }
        public virtual void Destruction()
        {
            if (spawnHitEffectOnDestruction && this.enabled)
            {
                Instantiate(bulletHitEffect, transform.position, transform.rotation);
            }          
            gameObject.SetActive(false);           
        }
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (destroyOnImpact)
            {
                if (rb.IsTouchingLayers(hittable))
                {
                    Destruction();
                }
            }                                
        }
    }
}

