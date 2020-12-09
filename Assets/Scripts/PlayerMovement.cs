using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //Tutorial found here: https://www.youtube.com/watch?v=Sqb-Ue7wpsI

    private CharacterController character_Controller;

    private Vector3 move_Direction;

    public float normalSpeed = 5f;
    public float speed = 5f;
    private float normalGravity = 20f;
    private float gravity = 20f;

    public float jump_Force = 10f;

    private float vertical_Velocity;


    void Awake()
    {

        character_Controller = GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        ApplyCheats();
        MoveThePlayer();
    }

    void MoveThePlayer()
    {
        move_Direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL));

        move_Direction = transform.TransformDirection(move_Direction);
        move_Direction *= speed * Time.deltaTime;

        ApplyGravity();

        character_Controller.Move(move_Direction);

    }

    void ApplyGravity()
    {
        if (character_Controller.isGrounded)
        {
            vertical_Velocity -= gravity * Time.deltaTime;

            PlayerJump();
        }
        else
        {
            vertical_Velocity -= gravity * Time.deltaTime;
        }

        move_Direction.y = vertical_Velocity * Time.deltaTime;
    }

    void PlayerJump()
    {
        if (character_Controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            vertical_Velocity = jump_Force;
        }
    }

    void ApplyCheats()
    {
        LowGravity();
        DoubleSpeed();
        QuadSpeed();
        OctaSpeed();
    }

    void LowGravity()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (gravity == normalGravity / 4)
            {
                gravity = normalGravity;
            }
            else
            {
                gravity = normalGravity / 4;
            }
        }
    }

    void DoubleSpeed()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            if (speed == 2 * normalSpeed)
            {
                speed = normalSpeed;
            }
            else
            {
                speed = 2 * normalSpeed;
            }
        }
    }

    void QuadSpeed()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            if (speed == 4 * normalSpeed)
            {
                speed = normalSpeed;
            }
            else
            {
                speed = 4 * normalSpeed;
            }
        }
    }

    void OctaSpeed()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (speed == 8 * normalSpeed)
            {
                speed = normalSpeed;
            }
            else
            {
                speed = 8 * normalSpeed;
            }
        }
    }
}
