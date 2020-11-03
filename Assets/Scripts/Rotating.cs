using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    // Start is called before the first frame update
    public float _rotatingSpeed = 10f;
    public float _moveSpeed = 10f;
    Vector3 topRight;
    private void Start()
    {
        topRight = new Vector3(Screen.width, Screen.height, 0);

        StartCoroutine(RotatingRoutine());
    }
    IEnumerator RotatingRoutine()
    {
        while (Vector3.Distance(transform.position, Camera.main.ViewportToScreenPoint(topRight)) > 10f)
        {
            transform.RotateAround(transform.position, Vector3.up, _rotatingSpeed * Time.fixedDeltaTime);
            transform.position = Vector3.MoveTowards(transform.position, Camera.main.ViewportToScreenPoint(topRight), _moveSpeed);
            yield return new WaitForFixedUpdate();
        }
    }
    
}
