using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraAttach : MonoBehaviour
{

    public Animator anim,gun_anim;
    public GameObject cam, bullet, camera_place,bulettTrace;


    public bool trigger;

    public Vector3 start_pos, end_pos;
    public Quaternion start_angle, end_angle;

    public float time_slider;


    public void StartGame() 
    {

    }



    public void AttachWork() 
    {
        cam.transform.parent = bullet.transform;
        bullet.transform.parent = null;
        bullet.GetComponent<BulletController>().enabled = true;
       // if (gameObject.name == "Camera") { 
            start_pos = cam.transform.localPosition;
            end_pos = camera_place.transform.localPosition;
        start_angle = cam.transform.rotation;
        end_angle = camera_place.transform.rotation;


        bulettTrace.SetActive(true);
        trigger = true;
        //}
    }
    public void ActivateAnimatorOnGun() 
    {
        gun_anim.enabled = true;
    }
    public void DeactivateAnimator() 
    {
    
        anim.enabled = false;
    }

   
    // Start is called before the first frame update
    void Start()
    {
       
    }

    void FixedUpdate() 
    {
        if (trigger)
        {
            time_slider += 0.02f;
        }
    }
    // Update is called once per frame
    void Update()
    {  
        if (trigger) 
        {
            cam.transform.localPosition = Vector3.Lerp(start_pos, end_pos,time_slider);

            cam.transform.rotation = Quaternion.Lerp(start_angle, end_angle, time_slider);

        }

    }
}
