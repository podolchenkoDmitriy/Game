using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explousion : MonoBehaviour
{

    Rigidbody _rb;

    public float _force;

    public float _radius;
    public GameObject _nova;
    public Transform _wave;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void InitializeExplousion()
    {
        StartCoroutine(Explode(transform.position));
    }
    IEnumerator Explode(Vector3 pos)
    {

        Collider[] hitColliders = Physics.OverlapSphere(pos, _radius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider !=null)
            {
                if (hitCollider.attachedRigidbody.constraints == RigidbodyConstraints.FreezeAll)
                {
                    hitCollider.attachedRigidbody.constraints = ~RigidbodyConstraints.FreezeAll;
                }
            }
        }

        _rb.AddExplosionForce(_force, pos, _radius, 100, ForceMode.Force);


        gameObject.GetComponent<SphereCollider>().isTrigger = true;

        while (transform.localScale.magnitude < 100f)
        {
            yield return new WaitForFixedUpdate();

            transform.localScale += Vector3.one * _radius * 0.1f;
        }

        _nova.SetActive(true);
        hitColliders = Physics.OverlapSphere(_wave.position, _radius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider != null)
            {
                if (hitCollider.attachedRigidbody.constraints == RigidbodyConstraints.FreezeAll)
                {
                    hitCollider.attachedRigidbody.constraints = ~RigidbodyConstraints.FreezeAll;
                }
                hitCollider.attachedRigidbody.AddExplosionForce(_force, hitCollider.attachedRigidbody.transform.position, _radius, 100, ForceMode.Force);
            }
        }
        while (transform.position.z > -100f)
        {
            yield return new WaitForFixedUpdate();


            transform.position -= Vector3.forward*Time.fixedDeltaTime * speed;
        }
        yield return null;

    }
    public float speed;
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_wave.position, _radius);
    }
}
