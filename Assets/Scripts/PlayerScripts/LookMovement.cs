using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookMovement : MonoBehaviour
{
    private Vector2 look_movement;
    private Vector2 look_angle;
    [SerializeField] Transform player_character;
    [SerializeField] Transform player_camera;
    [SerializeField] bool invert=false;
    [SerializeField] float sensitivity = 5f;
    [SerializeField] private Vector2 look_limits = new Vector2(-70f, 80f);
    // Start is called before the first frame update
    // Update is called once per frame
    private void Start()
    {
        //locks cursor to center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        // handleCursor to press "ESC" to get out of locked mode
        handleCursor();
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            LookAround();
        }

    }
    void handleCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Lock and unlock cursor based on key press
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;

            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

    }


    void LookAround()
    {
        //According to the co-ordinate axes , X-axis changes on left-right movement
        //Similarly Y-axis on up-down movement
        //So vector2 is assigned (y,x) to avoid rotational conflicts
        look_movement = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
        //In terms of rotational movement w.r.t. x, it is y-axis
        look_angle.x += look_movement.x * sensitivity * (invert ? 1f : -1f);
        //In terms of rotational movement w.r.t. y,it is x-axis
        look_angle.y += look_movement.y * sensitivity  ;
        look_angle.x = Mathf.Clamp(look_angle.x, look_limits.x, look_limits.y);
        player_camera.localRotation = Quaternion.Euler(look_angle.x, 0f, 0f);
        //main character's Y-coords are changed because on normal movement the rotational factor must be taken in to consideration,so Y-coords are applied on player_character
        player_character.localRotation = Quaternion.Euler(0f, look_angle.y, 0f);

    }

}
