using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintCrouch : MonoBehaviour
{
    [SerializeField] float sprint_speed=10f;
    [SerializeField] float crouch_speed = 2f;
    [SerializeField] Transform player_camera;
    [SerializeField] float crouch_height = 1f;
    [SerializeField] float stand_height = 1.6f;
    PlayerStats player_stats;
    private float stamina=100f;
    private float sprint_threshold=10f;
    PlayerMovement playerMovement;
    float moveSpeed = 5f;
    public bool crouching = false;
    public bool sprinting = false;
    // Start is called before the first frame update
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        player_stats = GetComponent<PlayerStats>();
        
    }
        
    // Update is called once per frame
    void Update()
    {
        Sprint();
        Crouch();
        
    }
    void Sprint()
    {
        //changes speed of the player
        if (stamina> 0f)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !crouching)
            {
                playerMovement.speed = sprint_speed;
                sprinting = true;
            }

        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && !crouching)
        {
            playerMovement.speed = moveSpeed;
            sprinting = false;
        }
        if(Input.GetKey(KeyCode.LeftShift) && !crouching && GetComponent<CharacterController>().velocity.sqrMagnitude>0)
        {
            stamina-= sprint_threshold * Time.deltaTime;
            if (stamina<= 0f)
            {
                stamina= 0f;
                playerMovement.speed = moveSpeed;
                sprinting = false;

            }
            player_stats.displayStamina(stamina);
        }
        else
        {
            if (stamina != 100f)
            {
                stamina += sprint_threshold / 2 * Time.deltaTime;
                player_stats.displayStamina(stamina);
                if (stamina > 100f)
                {
                    stamina = 100f;
                }
            }

        }


    }
    void Crouch()
    {
        //changes height of transform element and its velocity as well
        if (Input.GetKeyDown(KeyCode.C))
        {
            crouching = !crouching;
            player_camera.localPosition = new Vector3(0f, crouching ? crouch_height : stand_height, 0f);
            playerMovement.speed = crouching ? crouch_speed : moveSpeed;
        }

    }
}
