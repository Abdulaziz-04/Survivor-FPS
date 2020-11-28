using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Video;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager weapon_manager;
    [SerializeField] public float damage ;
    //properties
    public float fire_rate = 15f;
    private float fire_delay;
    //for zoom animations
    private Animator zoomCam;
    //to set visibility of crosshair
    private GameObject crosshair;
    //for arrow/spear to attack if aiming
    bool is_aiming = true;
    //to set the animation event with object instantiation
    private float arrow_time=0.4f,spear_time=0.2f;
    //adding prefabs to use accordingly
    [SerializeField] private GameObject arrow_prefab,spear_prefab;
    [SerializeField] private Transform launch_position;
    private void Awake()
    {
        //get all components
        weapon_manager = GetComponent<WeaponManager>();
        zoomCam = transform.Find("FPP(Pers)").transform.Find("Weapon Cam").GetComponent<Animator>();
        crosshair = GameObject.FindWithTag("CROSSHAIR_TAG");
    }
    void Update()
    {
        zoomInOut();
        weaponShoot();
        
    }
    void zoomInOut()
    {
        //if weapon has category of aiming use the animation of zoomin/out
        //and set the crosshair accordingly
        if (weapon_manager.getActiveWeapon().weapon_aim == weaponAim.AIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                zoomCam.Play("ZoomIn");
                crosshair.SetActive(false);
            }
            if (Input.GetMouseButtonUp(1))
            {
                zoomCam.Play("ZoomOut");
                crosshair.SetActive(true);
            }

        }
        //if it is the self-aim weapon launch the aim animation trigger
        if(weapon_manager.getActiveWeapon().weapon_aim == weaponAim.SELF_AIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                weapon_manager.getActiveWeapon().Aim(true);
                is_aiming = true;
            }
            if (Input.GetMouseButtonUp(1))
            {
                weapon_manager.getActiveWeapon().Aim(false);
                is_aiming = false;
            }

        }

    }
    //set a coroutine to match the instantiation and animation time events
    IEnumerator throwProjectile(bool arrow_bool)
    {
        if (arrow_bool)
        {
            yield return new WaitForSeconds(arrow_time);
            //instantiate and set position and speed 
            GameObject arrow = Instantiate(arrow_prefab);
            arrow.transform.position = launch_position.position;
            arrow.GetComponent<Projectile>().LaunchObject();

        }
        else
        {
            yield return new WaitForSeconds(spear_time);
            //instantiate and set position and speed
            GameObject spear= Instantiate(spear_prefab);
            spear.transform.position = launch_position.position;
            spear.GetComponent<Projectile>().LaunchObject();



        }

    }
    void weaponShoot()
    {
        //For assault rifle
        if (weapon_manager.getActiveWeapon().weapon_firetype == weaponFireType.MULTIPLE)
        {
            //for press and hold event 
            if (Input.GetMouseButton(0)&&Time.time>fire_delay)
            {
                fire_delay = Time.time + 1f / fire_rate;
                weapon_manager.getActiveWeapon().shootAnimation();
                fireBullets();

            }


        }
        //for other weapons
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Axe Weapon
                if (weapon_manager.getActiveWeapon().CompareTag("AXE_TAG"))
                {
                    weapon_manager.getActiveWeapon().shootAnimation();

                }
                //for shotgun,revolver
                else if (weapon_manager.getActiveWeapon().weapon_launchitem == weaponLaunchItem.BULLET)
                {
                    weapon_manager.getActiveWeapon().shootAnimation();
                    fireBullets();
                }
                //for arrow/spear
                else
                {
                    //check if aiming then attack animation can be played
                    if (is_aiming)
                    {
                        weapon_manager.getActiveWeapon().shootAnimation();
                        if (weapon_manager.getActiveWeapon().weapon_launchitem == weaponLaunchItem.ARROW)
                        {
                            StartCoroutine(throwProjectile(true));

                        }
                        else
                        {
                            StartCoroutine(throwProjectile(false));

                        }
                    }

                }
                
            }

        }

    }
    void fireBullets()
    {
        RaycastHit hit;
        //out gives reference to the object which gets hit 
        if (Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward,out hit))
        {

         if(hit.transform.gameObject.GetComponent<HealthMgmt>())
         {
                hit.transform.GetComponent<HealthMgmt>().dealDamage(damage);

         }


        }
    }
}
