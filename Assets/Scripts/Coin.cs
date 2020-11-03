using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    public float _rotatingSpeed = 10f;

    private void Start()
    {
        StartCoroutine(RotatingRoutine());
    }
    IEnumerator RotatingRoutine()
    {
        while (true)
        {
            transform.RotateAround(transform.position, Vector3.up, _rotatingSpeed * Time.fixedDeltaTime);

            yield return new WaitForFixedUpdate();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<PlayerController>())
        {
            ParticleHolder.instance.AddMoney(transform.position);
            gameObject.SetActive(false);
        }
    }
}
