using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UpGamesWeapon2D.TypesAndSettings;
namespace UpGamesWeapon2D.Weapons
{
    public class Weapon2DBase : MonoBehaviour
    {
        public Weapon2DPreset weaponPreset;
        [Tooltip("Point where the bullet is spawned!")]
        public Transform barrel;
        [HideInInspector]
        public bool reloading = false;
        public Dictionary<string, Weapon2DPreset> heldWeapons = new Dictionary<string, Weapon2DPreset>();
        //returns a ammopair for remaining ammo!
        public AmmoPair GetAmmo()
        {
            return new AmmoPair(weaponPreset.weaponAmmo.ammoInClip, weaponPreset.weaponAmmo.ammoInReserve);
        }
        //returns true if the gun is currently reloading! useful for displaying animations etc!
        public bool IsReloading()
        {
            return reloading;
        }
    }
}

