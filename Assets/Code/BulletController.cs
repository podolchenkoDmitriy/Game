using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Rigidbody bullet;
    public Vector3 direction_velocity;
    public float fly_speed;


    [Header("PowerUp Vars")]
    public float speedup_timer;



    [Header("System Vars")]
    public Vector2 startPos;
    public Vector2 direction;
    public Vector2 keyboard_input;
    public FloatingJoystick joy;
    float t = 0.0f;//camera lerper

    // Start is called before the first frame update
    void Start()
    {
    
        bullet = GetComponent<Rigidbody>();
        joy = GameObject.Find("Floating Joystick").GetComponent<FloatingJoystick>();
    
    }

    private void FixedUpdate()
    {
    
        bullet.velocity = direction_velocity;
    
    }

    // Update is called once per frame
    void Update()
    {
        t = Mathf.Clamp(t, 0, 1);
        if (speedup_timer >= 0)
        {
            t += 0.5f * Time.deltaTime;
            speedup_timer -= Time.deltaTime;
          
            fly_speed = 25;
            Camera.main.fieldOfView = Mathf.Lerp(60, 70, t); 

        }
        else
        {
            t -= 0.5f * Time.deltaTime;
            fly_speed = 10;
            Camera.main.fieldOfView = Mathf.Lerp(60, 70, t); ;
        }

        keyboard_input.x = Input.GetAxis("Horizontal");
        keyboard_input.y = Input.GetAxis("Vertical");
        if (keyboard_input.x == 0) keyboard_input.x = joy.Horizontal;
        if (keyboard_input.y == 0) keyboard_input.y = joy.Vertical;




        direction_velocity.z = fly_speed;
        direction_velocity.x = keyboard_input.x * fly_speed;
        direction_velocity.y = keyboard_input.y*fly_speed;



        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Handle finger movements based on TouchPhase
            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    // Record initial touch position.
                    startPos = touch.position;
                    break;

                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    // Determine direction by comparing the current touch position with the initial one
                    direction = touch.position - startPos;
                    break;

                case TouchPhase.Ended:
                    // Report that the touch has ended when it ends
                    break;
            }
        }


    }
}
