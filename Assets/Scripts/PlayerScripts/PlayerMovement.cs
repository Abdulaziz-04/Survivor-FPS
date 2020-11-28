using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //character controller is used as the major component here
    private CharacterController main_controller;
    // basic properties required
    private Vector3 move_direction;
    [SerializeField] public float speed = 5f;
    [SerializeField] private float jump_force=10f;
    [SerializeField] private float gravity=20f;
    [SerializeField] private float vertical_velocity;
    private void Awake()
    {
        //setting up the required components
        main_controller = GetComponent<CharacterController>();
    }
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        
    }
    void MovePlayer()
    {
        //get the move direction ans set it to transform with speed and frame-rate
        move_direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move_direction = transform.TransformDirection(move_direction);
        move_direction *= speed * Time.deltaTime;

        //applying gravitaional effect
        GravityEffect();
        main_controller.Move(move_direction);

    }
    void GravityEffect()
    {
        //if player jumps,subtract a constant  value gravity constantly
        vertical_velocity -= gravity * Time.deltaTime;
        PlayerJump();
        // just smoothens the fall
        move_direction.y = vertical_velocity * Time.deltaTime;


    }
    void PlayerJump()
    {
        //if player is on ground and presses the space key,apply vertical velocity
        if(main_controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            vertical_velocity = jump_force;
        }

    }
}
