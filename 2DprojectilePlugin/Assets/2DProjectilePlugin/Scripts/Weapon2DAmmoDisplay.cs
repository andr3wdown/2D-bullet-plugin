using UnityEngine;
using UnityEngine.UI;
using UpGamesWeapon2D.Weapons;
using UpGamesWeapon2D.TypesAndSettings;

public class Weapon2DAmmoDisplay : MonoBehaviour
{
    public Weapon2DBase weapon;
    public Text 
        clipAmmo, 
        reserveAmmo;
    float t = 0;
    public Text reloadingText;
	void Update ()
    {
        if (!weapon.IsReloading())
        {
            t = 0;
            reloadingText.gameObject.SetActive(false);
            DisplayText();
        }           
        else
        {
            t += Time.deltaTime * 10;
            reloadingText.gameObject.SetActive(true);
            Color c = reloadingText.color;
            c.a = Mathf.Sin(t);
            reloadingText.color = c;
        }
	}
    void DisplayText()
    {
        AmmoPair ammo = weapon.GetAmmo();
        clipAmmo.text = ammo.clipAmmo.ToString();
        reserveAmmo.text = ammo.reserveAmmo.ToString();
    }
}
