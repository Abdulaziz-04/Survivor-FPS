using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{

    private AudioSource audioplayer;

    [SerializeField]
    private AudioClip[] footstep_sounds;

    private CharacterController player_controller;

    [HideInInspector]
    public float min_volume, max_volume;

    private float covered_distance;

    [HideInInspector]
    public float step_distance;
    SprintCrouch player_footstats;

    void Awake()
    {
        audioplayer= GetComponent<AudioSource>();
        player_controller= GetComponentInParent<CharacterController>();
        player_footstats = GetComponentInParent<SprintCrouch>();
    }

    void Update()
    {
        playfootSteps();
    }

    void playfootSteps()
    {

        // if we are NOT on the ground
        if (!player_controller.isGrounded)
            return;


        //if we are moving 
        if (player_controller.velocity.sqrMagnitude > 0)
        {
            //if crouch detected
            if (player_footstats.crouching == true)
            {
                min_volume = 0.2f;
                max_volume = 0.3f;
                step_distance = 0.5f;
            }
            //if sprint detected
            else if (player_footstats.sprinting == true)
            {
                min_volume = 0.3f;
                max_volume = 0.6f;
                step_distance = 0.25f;
            }
            //else walking
            else
            {
                min_volume = 0.3f;
                max_volume = 0.6f;
                step_distance = 0.4f;
            }
            //distance covered per frame-rate

           covered_distance += Time.deltaTime;

            if (covered_distance> step_distance)
            {

                //play audio
                audioplayer.volume = Random.Range(min_volume, max_volume);
                audioplayer.clip = footstep_sounds[Random.Range(0, footstep_sounds.Length)];
                audioplayer.Play();
                //reset values

                covered_distance= 0f;

            }

        }
        else
        {
            //reset values
            covered_distance= 0f;
        }


    }



} // class





