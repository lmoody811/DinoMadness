using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    [SerializeField]
    private WeaponHandler[] weapons;

    private int current_Weapon_Index;


    // Start is called before the first frame update
    void Start()
    {
        current_Weapon_Index = 0;
        weapons[current_Weapon_Index].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            TurnOnSelectedWeapon(0);
        }

        //add more of these if I add more weapons.. Alpha1 is 1 on the keyboard.. Alpha2 is 2 on the keyboard.. etc.
    }

    void TurnOnSelectedWeapon(int weaponIndex)
    {
        if(current_Weapon_Index == weaponIndex)
        {
            return;
        }
        weapons[current_Weapon_Index].gameObject.SetActive(false);

        weapons[weaponIndex].gameObject.SetActive(true);

        current_Weapon_Index = weaponIndex;
    }

    public WeaponHandler GetCurrentSelectedWeapon()
    {
        return weapons[current_Weapon_Index];
    }
}
