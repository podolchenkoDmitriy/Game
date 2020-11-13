using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallandChain : MonoBehaviour
{
    // Start is called before the first frame update
    public float _speed = 10f;
    public float _bound = 60f;
    void Start()
    {
        StartCoroutine(Rotating());
    }
    bool right = false;
    IEnumerator Rotating()
    {
        while (true)
        {
            if (!right)
            {
                transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + _speed), Time.fixedDeltaTime * _speed);

            }
            else
            {
                transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - _speed), Time.fixedDeltaTime * _speed);

            }
            if (transform.eulerAngles.z > _bound && !right && transform.eulerAngles.z < 360f - _bound)
            {
                right = true;
            }
            else if (transform.eulerAngles.z < 360f -_bound && right && transform.eulerAngles.z > _bound)
            {
                right = false;

            }
            yield return new WaitForFixedUpdate();
        }
    }
}
