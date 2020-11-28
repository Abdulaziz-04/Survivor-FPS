using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //get all weapons 
    [SerializeField] WeaponHandler[] weapons;
    //set indices
    private int weapon_index = 0;
    //set visibility 
    GameObject crosshair;
    private void Start()
    {
        //get components
        weapons[weapon_index].gameObject.SetActive(true);
        crosshair = GameObject.FindWithTag("CROSSHAIR_TAG");
        crosshair.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            activateWeapon(0);

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            activateWeapon(1);

        }if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            activateWeapon(2);

        }if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            activateWeapon(3);

        }if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            activateWeapon(4);

        }if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            activateWeapon(5);

        }

    }
    void activateWeapon(int ind)
    {
        //set  visibility of croshair
        if (ind!= 0)
        {
            crosshair.gameObject.SetActive(true);
        }
        else
        {
            crosshair.gameObject.SetActive(false);
        }
        
        //same weapon selected
        if (weapon_index == ind)
        {
            return;
        }
        //new weapon selected
        weapons[weapon_index].gameObject.SetActive(false);
        weapons[ind].gameObject.SetActive(true);
        weapon_index = ind;
        

    }
   public WeaponHandler getActiveWeapon()
    {
        return weapons[weapon_index];

    }
    
}
