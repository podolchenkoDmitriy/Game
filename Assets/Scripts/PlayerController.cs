using System.Collections;
using UnityEngine;
using UnityStandardAssets.Utility;
using UnityStandardAssets.Water;

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
    public Vector2 _angles;
    public float _maxAngle = 60f;
    public Particle _waterCollisionEffect;
    float _speed;
    private void Start()
    {
        StartCoroutine(Moving());
        _camera = Camera.main.GetComponent<SmoothFollow>();
        _constHeight = _camera.height;
        _speed = _flyightMoveSpeed;
    }
    private void ClampPosition()
    {
        //player's boundaries in case of camera viewport
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, 0.1f, 0.9f);
        pos.y = Mathf.Clamp(pos.y, 0.1f, 0.9f);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
    Vector2 _tapPos;
    private IEnumerator Moving()
    {
        while (true)
        {
            //ClampPosition();
            if (_isMousePressed)
            {
                startDir = - new Vector3(_tapPos.y, -_tapPos.x, 0) + Camera.main.ScreenToViewportPoint(Input.mousePosition);

                _angles = (- _tapPos + new Vector2(-Camera.main.ScreenToViewportPoint(Input.mousePosition).y , Camera.main.ScreenToViewportPoint(Input.mousePosition).x))* _maxAngle;

                _angles.x = Mathf.Clamp(_angles.x, -_maxAngle, _maxAngle);
                _angles.y = Mathf.Clamp(_angles.y, -_maxAngle, _maxAngle);

                transform.eulerAngles = _angles;

            }
            else
            {
                //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, 1);
                _angles = Vector2.zero;
            }

            transform.position = Vector3.Lerp(transform.position, transform.position + startDir, _movingSpeed * Time.fixedDeltaTime);

            transform.Translate(Vector3.forward * _flyightMoveSpeed, Space.Self);


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

            if (_flyightMoveSpeed > _speed)
            {
                _flyightMoveSpeed -= 0.1f;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (_camera.height == _constHeight)
        {
            _camera.height = _constHeight*0.5f;
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<WaterTile>())
        {
            if (!_waterCollisionEffect.gameObject.activeInHierarchy)
            {
                _waterCollisionEffect.gameObject.SetActive(true);
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_camera.height != _constHeight)
        {
            _camera.height = _constHeight;
        }
        if (_waterCollisionEffect.gameObject.activeInHierarchy)
        {
            _waterCollisionEffect.gameObject.SetActive(false);
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
            _tapPos = new Vector2(-Camera.main.ScreenToViewportPoint(Input.mousePosition).y, Camera.main.ScreenToViewportPoint(Input.mousePosition).x);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {

            _isMousePressed = false;
        }
    }
}


