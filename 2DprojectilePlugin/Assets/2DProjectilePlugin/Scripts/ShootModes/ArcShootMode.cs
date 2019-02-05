using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UpGamesWeapon2D.Weapons
{
    public class ArcShootMode : ShootMode
    {
        public override void Shoot(Transform barrel, Weapon2DPreset weaponPreset, BulletPoolInstance bulletPool)
        {
            if (barrel.gameObject.activeSelf)
            {
                for (int i = -weaponPreset.shotCount / 2; i <= weaponPreset.shotCount / 2; i++)
                {
                    float zRot = (weaponPreset.spread / weaponPreset.shotCount) * i;
                    Quaternion bulletRot = Quaternion.Euler(0, 0, barrel.rotation.eulerAngles.z + zRot);
                    GameObject bullet = bulletPool.SpawnObjectAt(barrel, bulletRot);
                    if (bullet.GetComponent<Projectile2D>() != null)
                    {
                        float variation1 = 1;
                        float variation2 = 1;
                        if (weaponPreset.speedVariation)
                        {
                            variation1 = 1 - weaponPreset.variationAmount;
                            variation2 = 1 + weaponPreset.variationAmount;
                        }

                        float randSpeed = (weaponPreset.speedVariation) ? Random.Range(weaponPreset.projectileSpeed * variation1, weaponPreset.projectileSpeed * variation2) : weaponPreset.projectileSpeed;
                        bullet.GetComponent<Projectile2D>().bulletSpeed = randSpeed;
                    }
                    else
                    {
                        Debug.LogError("Instantiated bullet doesn't have BasicProjectile2D.cs component or something that derives from it!");
                    }
                }
            }
           
        }
    }
}

