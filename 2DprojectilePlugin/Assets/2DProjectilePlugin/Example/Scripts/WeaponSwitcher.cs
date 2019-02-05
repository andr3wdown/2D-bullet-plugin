using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UpGamesWeapon2D.Weapons;

namespace UpGamesWeapon2D.Example
{
    [RequireComponent(typeof(Weapon2D))]
    public class WeaponSwitcher : MonoBehaviour
    {
        Weapon2D controlledWeapon;
        public KeyCode positiveScroll = KeyCode.E;
        public KeyCode negativeScroll = KeyCode.Q;
        [Tooltip("List of carried weapons")]
        public List<Weapon2DPreset> weapons;
        int weaponIndex = 0;
        private void Start()
        {
            if(controlledWeapon == null)
            {
                controlledWeapon = GetComponent<Weapon2D>();
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(positiveScroll))
            {
                ChangeWeapon(1);
            }
            if (Input.GetKeyDown(negativeScroll))
            {
                ChangeWeapon(-1);
            }
        }

        public void ChangeWeapon(int increment)
        {
            weaponIndex+=increment;
            if (weaponIndex >= weapons.Count)
            {
                weaponIndex = 0;
            }
            if (weaponIndex < 0)
            {
                weaponIndex = weapons.Count-1;
            }
            controlledWeapon.InitWeaponPreset(weapons[weaponIndex]);
        }

    }
}

