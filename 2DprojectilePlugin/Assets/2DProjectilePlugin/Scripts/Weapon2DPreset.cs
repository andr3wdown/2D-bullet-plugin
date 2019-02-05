using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UpGamesWeapon2D.TypesAndSettings;

namespace UpGamesWeapon2D.Weapons
{
    [CreateAssetMenu(fileName = "New Weapon2D Preset", menuName = "Weapon2D/Weapon2D Preset", order = 0)]
    public class Weapon2DPreset : ScriptableObject
    {
        public Weapon2DPreset Copy()
        {
            Weapon2DPreset c = (Weapon2DPreset)CreateInstance("Weapon2DPreset");
            
            c.weaponName = weaponName;
            c.clipSize = clipSize;
            c.startingAmmo = startingAmmo;
            c.ammoUsagePerShot = ammoUsagePerShot;
            c.shootMode = shootMode;
            c.reloadType = reloadType;
            c.cooldown = cooldown;
            c.reloadTime = reloadTime;
            c.projectileSpeed = projectileSpeed;
            c.spread = spread;
            c.shotCount = shotCount;
            c.speedVariation = speedVariation;
            c.variationAmount = variationAmount;
            c.bulletPrefab = bulletPrefab;
            c.enemyBulletPrefab = enemyBulletPrefab;
            return c;
        }
        
        public string weaponName;
        [Tooltip("Size of weapon clip")]
        public int clipSize;
        [Tooltip("Amount of starting extra ammunition!")]
        public int startingAmmo;
        [Tooltip("Amount of ammo used when shooting!")]
        public int ammoUsagePerShot;
        [Tooltip("Mode of shooting!")]
        public Mode shootMode;
        [Tooltip("Reload type!")]
        public ReloadType reloadType;
        [Tooltip("Cooldown between bullets!")]
        [Range(0.000f, 4.000f)]
        public float cooldown;
        [Tooltip("Time it takes to reload the weapon!")]
        public float reloadTime;
        [Tooltip("Speed of fired projectile!")]
        [Range(0, 150)]
        public float projectileSpeed;
        [Tooltip("Spread in degrees!")]
        [Range(0, 360)]
        public float spread;
        [Tooltip("Amount of bullets shot in arc mode!")]
        public int shotCount;
        [Tooltip("Varying speed for spawned bullets?")]
        public bool speedVariation;
        [Tooltip("Amount of speed variation!")]
        [Range(0.0001f, 1.0000f)]
        public float variationAmount = 0.2f;
        [Tooltip("Ammo! see Readme for instructions!")]
        public GameObject bulletPrefab;
        public GameObject enemyBulletPrefab;
        public void SetEnemyWeapon()
        {
            bulletPrefab = enemyBulletPrefab;
        }

        [HideInInspector]
        public WeaponAmmo weaponAmmo;
       
    }
}

