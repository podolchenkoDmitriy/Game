using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customExplosion : MonoBehaviour
{
    public GameObject cracked, non_cracked,fx,explo_wave;

    public bool trigger_explo,triggered;
    private void OnTriggerEnter(Collider other)
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (trigger_explo) 
        {
            if (triggered == false) 
            {
                triggered = true;
                non_cracked.SetActive(false);
                cracked.SetActive(true);
                explo_wave.SetActive(true);


                var child_list = GetComponentsInChildren<Rigidbody>();
                foreach (Rigidbody rb in child_list) 
                {
                    rb.AddExplosionForce(15f, transform.position, 10f);
                }
                Destroy(Instantiate(fx, transform.position, transform.rotation),5f);


            }
        }

    }
}
