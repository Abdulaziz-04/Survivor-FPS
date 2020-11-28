using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

//For Arrow/Spear Projectile
public class Projectile: MonoBehaviour
{
    //the body to be used
    private Rigidbody arrow_body;
    //properties to be set
    public float speed = 30f;
    public float delay_timer = 3f;
    public float damage = 50f;
    private void Awake()
    {
        //get the component
        arrow_body = GetComponent<Rigidbody>();
        
    }
    private void Start()
    {
        //invokes the function every particular instance of delay_timer 
        //to avoid spawning new arrows constantly
        Invoke("deactivateObject", delay_timer);
    }
    void deactivateObject()
    {
        //if object is active ,deactivate it 
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider target)
    {
        if (target.CompareTag("ENEMY_TAG"))
        {
            target.GetComponent<HealthMgmt>().dealDamage(damage);
            gameObject.SetActive(false);
        }
    }
    public void LaunchObject()
    {
        //add velocity to the object in direction of main camera
        arrow_body.velocity = Camera.main.transform.forward * speed;
        //add rotation to the rigid body
        transform.LookAt(transform.position + arrow_body.velocity);
    }
   


}
