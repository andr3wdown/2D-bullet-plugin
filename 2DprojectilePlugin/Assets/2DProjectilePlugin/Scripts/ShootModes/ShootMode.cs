using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UpGamesWeapon2D.Weapons
{
    public abstract class ShootMode : ScriptableObject
    {
        public abstract void Shoot(Transform barrel, Weapon2DPreset weaponPreset, BulletPoolInstance bulletPool);
    }
}

