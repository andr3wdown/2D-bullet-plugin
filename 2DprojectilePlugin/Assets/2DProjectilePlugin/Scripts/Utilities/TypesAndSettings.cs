using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UpGamesWeapon2D.TypesAndSettings
{
    public enum Mode
    {
        normal,
        arcShot
    }
    public enum ReloadType
    {
        none,
        clip
    }
    public class WeaponAmmo
    {
        public int maxAmmo;
        public int ammoInClip;
        public int ammoInReserve;
        public WeaponAmmo(int max, int reserve)
        {
            maxAmmo = max;
            ammoInClip = max;
            ammoInReserve = reserve;
        }
        public void Reload(out bool reloaded)
        {
            if (ammoInReserve > 0)
            {
                reloaded = true;
                int neededAmmo = maxAmmo - ammoInClip;
                int gotAmmo = ammoInReserve - neededAmmo;
                if (gotAmmo > 0)
                {
                    ammoInReserve = gotAmmo;
                    ammoInClip = maxAmmo;
                }
                else
                {
                    ammoInClip += ammoInReserve;
                    ammoInReserve = 0;
                }
            }
            else
            {
                reloaded = false;
            }
        }
        public void ShootAmmo(out bool shot, int bulletAmount)
        {
            if (ammoInClip >= bulletAmount)
            {
                shot = true;
                ammoInClip -= bulletAmount;
            }
            else
            {
                shot = false;
            }
        }
        public void AddAmmo(int amount)
        {
            ammoInReserve += amount;
        }
        public int ammoCount
        {
            get
            {
                return ammoInReserve + ammoInClip;
            }
        }
        public int reserveAmmoCount
        {
            get
            {
                return ammoInReserve;
            }
        }
        public bool needsReload
        {
            get
            {
                return ammoInClip != maxAmmo;
            }
        }
    }
    public struct Cooldown
    {
        public float cooldown;
        public float delay;
        public Cooldown(float d, float c = 0)
        {
            cooldown = c;
            delay = d;
        }
    }
    public struct AmmoPair
    {
        public int clipAmmo;
        public int reserveAmmo;
        public AmmoPair(int clip, int reserve)
        {
            clipAmmo = clip;
            reserveAmmo = reserve;
        }
    }
}
