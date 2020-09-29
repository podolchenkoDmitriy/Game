using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletController : MonoBehaviour
{
    public Rigidbody bullet;
    public Vector3 direction_velocity;
    public float fly_speed;


    [Header("PowerUp Vars")]
    public float speedup_timer;



    [Header("System Vars")]
   // public Vector2 startPos;
    public Vector2 direction;
    public Vector2 keyboard_input;
    //public FloatingJoystick joy;
    float t = 0.0f;//camera lerper


    [Header("Debug Vars")]
    public Vector2 touchDelta;
    public Text DebugConsole;

    public float smooth_rotation_speed;
    public float swipe_speed;





    GameObject visual_model;

    // Start is called before the first frame update
    void Start()
    {

        visual_model = GameObject.Find("visualBullet");
        bullet = GetComponent<Rigidbody>();
       // joy = GameObject.Find("Floating Joystick").GetComponent<FloatingJoystick>();
    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") 
        {
            print("KABOOOM!");
            Camera.main.transform.parent = null;
            Destroy(gameObject);
            //TODO restart level
        }
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
            Camera.main.fieldOfView = Mathf.Lerp(60, 70, t); 
        }



        if (Input.touchCount > 0)
        {
            touchDelta = Input.GetTouch(0).deltaPosition;
            keyboard_input.x = Input.GetTouch(0).deltaPosition.x * Time.deltaTime;
            keyboard_input.y = Input.GetTouch(0).deltaPosition.y * Time.deltaTime;
          
           
        }
        else 
        {
            keyboard_input.x = Input.GetAxis("Horizontal");
            keyboard_input.y = Input.GetAxis("Vertical");
            
           
        }
        
     



        direction_velocity.z = fly_speed;
        direction_velocity.x = keyboard_input.x * swipe_speed;
        direction_velocity.y = keyboard_input.y* swipe_speed;
       

        
  


    }
}
