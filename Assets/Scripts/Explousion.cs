using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explousion : MonoBehaviour
{

    Rigidbody _rb;

    public float _force;
    public float _radius;
    public static bool _collision = false;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
       
    }

    public void InitExpl()
    {
        ParticleHolder.instance.Explousion(transform.position, transform);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Rigidbody>())
        {
            print(collision.collider.name);
            if (gameObject.activeInHierarchy)
            {

                StartCoroutine(Explode(transform.position));
                _collision = true;

                Camera.main.GetComponent<SmoothFollow>().Shake(0.2f, 0.2f);
                gameObject.SetActive(false);
                _rb.velocity = Vector3.zero;
                
            }

        }

    }
    IEnumerator Explode(Vector3 pos)
    {

        Collider[] hitColliders = Physics.OverlapSphere(pos, _radius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider !=null)
            {
                if (hitCollider.attachedRigidbody != null)
                {
                    if (hitCollider.attachedRigidbody.constraints == RigidbodyConstraints.FreezeAll)
                    {

                        hitCollider.attachedRigidbody.constraints = ~RigidbodyConstraints.FreezeAll;

                    }
                }
            }
        }

        _rb.AddExplosionForce(_force, pos, _radius, 100, ForceMode.Force);

        yield return null;

    }

    
    public float speed;
   
}
