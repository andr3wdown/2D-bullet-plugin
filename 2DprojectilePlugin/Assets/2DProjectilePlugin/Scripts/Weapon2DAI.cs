using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UpGamesWeapon2D.TypesAndSettings;

namespace UpGamesWeapon2D.Weapons
{
    public class Weapon2DAI : Weapon2DBase
    {
        //Make a script that calls the AIShoot() method and place it on the same game object
        //Get refrence by GetComponent<Weapon2DAI>() 
        public bool infiniteAmmo = true;        
        BulletPoolInstance bulletPool;
        Cooldown cooldownTimer;
        public AudioClip shootsound;
        AudioSource ac;
        ShootMode shootMode;
        private void Start()
        {
            heldWeapons = new Dictionary<string, Weapon2DPreset>();
            ac = gameObject.AddComponent<AudioSource>();
            InitWeaponPreset(weaponPreset);
        }
        public void InitWeaponPreset(Weapon2DPreset preset, bool isNewWeapon=false)
        {
            if (preset != null)
            {
                if (heldWeapons.ContainsKey(preset.name))
                {
                    if (!isNewWeapon)
                    {
                        weaponPreset = heldWeapons[preset.name];
                    }
                    else
                    {
                        heldWeapons[preset.name] = preset.Copy();
                        weaponPreset = heldWeapons[preset.name];
                    }
                }
                else
                {
                    heldWeapons.Add(preset.name, preset.Copy());
                    weaponPreset = heldWeapons[preset.name];
                }
                weaponPreset.SetEnemyWeapon();
                switch (weaponPreset.shootMode)
                {
                    case Mode.normal:
                        shootMode = (ShootMode)ScriptableObject.CreateInstance("NormalShoot");
                        break;
                    case Mode.arcShot:
                        shootMode = (ShootMode)ScriptableObject.CreateInstance("ArcShootMode");
                        break;
                    default:
                        shootMode = (ShootMode)ScriptableObject.CreateInstance("ArcShootMode");
                        break;

                }
                if (weaponPreset.weaponAmmo == null)
                {

                    weaponPreset.weaponAmmo = new WeaponAmmo(weaponPreset.clipSize, weaponPreset.startingAmmo);
                }
                cooldownCheckValue = weaponPreset.cooldown;
                cooldownTimer = new Cooldown(d: weaponPreset.cooldown);
                gameObject.name = weaponPreset.weaponName;
                if (bulletPool != null)
                {
                    bulletPool.DestructionSequence();
                }
                bulletPool = new BulletPoolInstance(weaponPreset.bulletPrefab, 0, this, weaponPreset.weaponName);
            }
           
        }
        public void AIShoot()
        {
            if (this.enabled)
            {
                Shoot();
                if (infiniteAmmo)
                {
                    weaponPreset.weaponAmmo.AddAmmo(weaponPreset.ammoUsagePerShot);
                }
            }
      
            
        }
        float cooldownCheckValue;
        void CooldownCheck()
        {
            if (cooldownCheckValue != weaponPreset.cooldown)
            {
                cooldownTimer = new Cooldown(d: weaponPreset.cooldown);
                cooldownCheckValue = weaponPreset.cooldown;
            }
        }
        private void Shoot()
        {
            cooldownTimer.cooldown -= Time.deltaTime;


            if (cooldownTimer.cooldown <= 0)
            {
                if (!reloading)
                {
                    switch (weaponPreset.reloadType)
                    {
                        case ReloadType.none:
                            HandleShooting();
                            break;
                        case ReloadType.clip:
                            bool succesShooting = false;
                            weaponPreset.weaponAmmo.ShootAmmo(out succesShooting, weaponPreset.ammoUsagePerShot);
                            if (succesShooting)
                            {
                                HandleShooting();
                            }
                            else
                            {
                                ReloadWeapon();
                            }
                            break;
                    }
                }
                cooldownTimer.cooldown = cooldownTimer.delay;
            }
        }
        private void HandleShooting()
        {
            ac.pitch = Random.Range(0.95f, 1.05f);
            ac.PlayOneShot(shootsound, 0.5f);
            if(this.enabled)
            shootMode.Shoot(barrel, weaponPreset, bulletPool);
        }
        private void ReloadWeapon()
        {
            if (!reloading)
            {

                if (weaponPreset.weaponAmmo.needsReload)
                {
                    bool canReload = false;
                    weaponPreset.weaponAmmo.Reload(out canReload);
                    if (canReload)
                    {
                        StartCoroutine(ReloadDelay(weaponPreset.reloadTime));
                        //TODO: PLAY RELOAD SOUND!
                    }
                    else
                    {
                        //Debug.Log(weaponPreset.weaponName + " is out of ammo!");
                    }
                }
            }
        }
        IEnumerator ReloadDelay(float d)
        {
            reloading = true;

            yield return new WaitForSeconds(d);

            reloading = false;
        }
    }
}

