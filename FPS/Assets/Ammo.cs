using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    public Transform weaponholder;
    public Text MaxAmmoText;

    // Update is called once per frame
    void Update()
    {
        int selectedweapon = weaponholder.GetComponent<WeaponSwitching>().selectedWeapon;
        string current_ammo =  weaponholder.GetChild(selectedweapon).GetComponent<Gun>().currAmmo.ToString();
        string max_ammo = weaponholder.GetChild(selectedweapon).GetComponent<Gun>().maxAmmo.ToString();


        MaxAmmoText.text = (current_ammo + "/" + max_ammo);
            

    }
}
