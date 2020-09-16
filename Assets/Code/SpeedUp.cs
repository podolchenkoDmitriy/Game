using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    public BulletController bullet;

    // Start is called before the first frame update
    void Start()
    {
        bullet = GameObject.Find("Bullet").GetComponent<BulletController>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet") 
        {
            bullet.speedup_timer += 5;

            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
