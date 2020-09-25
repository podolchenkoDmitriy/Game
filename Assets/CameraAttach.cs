using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraAttach : MonoBehaviour
{

    public Animator anim,gun_anim;
    public GameObject cam,bullet;
    
    public void StartGame() 
    {

    }



    public void AttachWork() 
    {
        cam.transform.parent = bullet.transform;
        bullet.transform.parent = null;
        bullet.GetComponent<BulletController>().enabled = true;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
