using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStones : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] _points;
    public float _speed = 10f;
    int numOfPoint = 0;
    Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        StartCoroutine(MoveToPoint());
    }
    IEnumerator MoveToPoint()
    {
        float dist = 100;
        while (dist > 1.5f)
        {
             dist = Vector3.Distance(transform.position, _points[numOfPoint].position);

            _rb.AddForce((-_rb.position.normalized + _points[numOfPoint].position.normalized) * _speed * Time.fixedDeltaTime, ForceMode.VelocityChange);

            yield return new WaitForFixedUpdate();
        }
        if (numOfPoint == _points.Length - 1)
        {
            numOfPoint--;
        }
        else
        {
            numOfPoint++;
        }
        yield return StartCoroutine(MoveToPoint());

    }

    
}
