using System.Collections;
using UnityEngine;
using UnityStandardAssets.Utility;

public class PlayerController : MonoBehaviour
{

    [Header("Crosshair movement")]
    [SerializeField] private float _movingSpeed = 5f;
    public float _flyightMoveSpeed = 10f;
    public static bool _isMousePressed = false;
    public Vector3 startDir;

    public float moveSpeedInput = 60f;
    public float _smooth = 0.25f;
    public float _radius = 10f;
    private SmoothFollow _camera;
    private float _constHeight;
    private void Start()
    {
        StartCoroutine(Moving());
        _camera = Camera.main.GetComponent<SmoothFollow>();
        _constHeight = _camera.height;
    }
    private void ClampPosition()
    {
        //player's boundaries in case of camera viewport
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, 0.1f, 0.9f);
        pos.y = Mathf.Clamp(pos.y, 0.1f, 0.9f);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    private IEnumerator Moving()
    {
        while (true)
        {
            ClampPosition();
            if (_isMousePressed)
            {
                startDir = -new Vector3(0.5f, 0.5f, -_flyightMoveSpeed) + Camera.main.ScreenToViewportPoint(Input.mousePosition);
            }
            else
            {
                startDir = Vector3.forward * _flyightMoveSpeed;
            }

            transform.position = Vector3.Lerp(transform.position, transform.position + startDir, _movingSpeed * Time.fixedDeltaTime);

            yield return new WaitForFixedUpdate();
        }
    }
    private void FixedUpdate()
    {


        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _radius);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<FlashingController>())
            {
                hitCollider.GetComponent<FlashingController>().StartHighlight();
            }
        }
    }

    private bool _canCollision = true;
    private void OnCollisionEnter(Collision collision)
    {

        if (_canCollision)
        {
            _canCollision = false;
            StartCoroutine(WaitForSecond());
            ParticleHolder.instance.CollisionEffect(collision.contacts[0].point);

            Camera.main.GetComponent<SmoothFollow>().Shake(0.1f, 0.2f);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (_camera.height == _constHeight)
        {
            _camera.height = 0;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_camera.height != _constHeight)
        {
            _camera.height = _constHeight;
        }
    }

    private IEnumerator WaitForSecond()
    {
        yield return new WaitForSeconds(1f);
        _canCollision = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            _isMousePressed = true;

        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {

            _isMousePressed = false;
        }
    }
}


