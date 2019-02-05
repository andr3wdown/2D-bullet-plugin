using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UpGamesWeapon2D.Weapons;

public class RaycastShootMode : ShootMode {
    public float range = Mathf.Infinity;
    public LayerMask hittable;
    
    public override void Shoot(Transform barrel, Weapon2DPreset weaponPreset, BulletPoolInstance bulletPool)
    {
        if (barrel.gameObject.activeSelf)
        {
            RaycastHit2D hit = Physics2D.Raycast(barrel.position, barrel.up, range, hittable);
            if(hit.transform != null)
            {
                //hit;
            }

        }
    }
}
