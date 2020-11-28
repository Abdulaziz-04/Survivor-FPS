using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enumeration to classify weapons 
public enum weaponFireType
{
    SINGLE,
    MULTIPLE,
}
public enum weaponLaunchItem
{
    BULLET,
    ARROW,
    SPEAR,
    NONE
}
public enum weaponAim
{
    SELF_AIM,
    AIM,
    NONE
}
public class WeaponHandler : MonoBehaviour
{
    //animator component
    private Animator weapon_animator;
    //enum types
    public weaponAim weapon_aim;
    public weaponFireType weapon_firetype;
    public weaponLaunchItem weapon_launchitem;
    //prefabs 
    [SerializeField] GameObject muzzle_flash;
    [SerializeField] AudioSource shoot_sound, reload_sound;
    [SerializeField] GameObject attack_point;

   
    private void Awake()
    {
        weapon_animator = GetComponent<Animator>();
    }
    //animation events based on code 
    public void shootAnimation()
    {
        weapon_animator.SetTrigger("attackTrigger");
    }
    public void reloadSound()
    {
        reload_sound.Play();
    }
    public void shootSound()
    {
        shoot_sound.Play();
    }
    public void Aim(bool aiming)
    {
        weapon_animator.SetBool("aim", aiming);
    }
    public void muzzleOn()
    {
        muzzle_flash.SetActive(true);
    }
    public void muzzleOff()
    {
        muzzle_flash.SetActive(false);
    }
    //setting the activitity of attack points which deal damage
    public void attPtON()
    {
        attack_point.SetActive(true);
    }
    public void attPtOFF()
    {
        if (attack_point.activeInHierarchy)
        {
            attack_point.SetActive(false);
        }
    }
    
}
