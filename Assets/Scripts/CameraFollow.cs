using UnityEngine;
using System.Collections;
public class CameraFollow : MonoBehaviour
{
    public Camera _cam;
    public Transform target;
    public Vector3 offset;
    public Vector3 viewPointOffset;
    public float speed;

    public bool ActivateSmoothRotation;

    void FixedUpdate()
    {
        if (_cam && target)
        {
            Vector3 v3TargetOffset = target.position;
            v3TargetOffset += (offset.z * target.transform.forward);
            v3TargetOffset += (offset.y * target.transform.up);
            v3TargetOffset += (offset.x * target.transform.right);

            Vector3 v3viewPointOffset = target.position;
            v3viewPointOffset += (viewPointOffset.z * target.transform.forward);
            v3viewPointOffset += (viewPointOffset.y * target.transform.up);
            v3viewPointOffset += (viewPointOffset.x * target.transform.right);

            if (ActivateSmoothRotation)
            {
                Quaternion rotation = Quaternion.identity;
                if (target.GetComponent<PlayerController>())
                {
                    rotation = Quaternion.LookRotation(v3viewPointOffset - _cam.transform.position - target.GetComponent<PlayerController>().dir * 0.1f);
                }
                else
                {

                    rotation = Quaternion.LookRotation(v3viewPointOffset - _cam.transform.position);

                }
                Quaternion smoothRotation = Quaternion.Slerp(_cam.transform.rotation, rotation, Time.fixedDeltaTime * speed);
                _cam.transform.rotation = smoothRotation;
            }

            _cam.transform.position = Vector3.Lerp(_cam.transform.position, v3TargetOffset , Time.fixedDeltaTime * speed);

            if (!ActivateSmoothRotation)
                _cam.transform.LookAt(v3viewPointOffset);
        }
    }
}