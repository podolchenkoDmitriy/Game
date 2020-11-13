using System.Collections;
using UnityEngine;
using UnityStandardAssets.Water;

public class PlayerController : MonoBehaviour
{

    [Header("Crosshair movement")]
    [SerializeField] private float _movingSpeed = 5f;
    public float _flyightMoveSpeed = 10f;
    public static bool _isMousePressed = false;
    public Vector3 startDir;

    public float _smooth = 0.25f;
    public float _radius = 10f;
    private CameraFollow _camera;
    Vector2 _angles;
    public float _maxAngle = 60f;
    public Particle _waterCollisionEffect;
    public float _maxSpeed;
    private bool tracing = false;
    private void Start()
    {
        StartCoroutine(Moving());
        _camera = Camera.main.GetComponent<CameraFollow>();
    }
    private void ClampPosition()
    {
        //player's boundaries in case of camera viewport
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, 0.01f, 0.99f);
        pos.y = Mathf.Clamp(pos.y, 0.1f, 0.9f);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    private Vector2 _tapPos;
    private Vector2 _lastAngle;
    public float _horRay = 0;
    public float _vertRay = 0;
    private IEnumerator Moving()
    {
        while (true)
        {
            //ClampPosition();
            if (_isMousePressed)
            {
                startDir = -new Vector3(_tapPos.y, -_tapPos.x, 0) + Camera.main.ScreenToViewportPoint(Input.mousePosition);

                _angles = _lastAngle + (-_tapPos + new Vector2(-Camera.main.ScreenToViewportPoint(Input.mousePosition).y, Camera.main.ScreenToViewportPoint(Input.mousePosition).x)) * _maxAngle;

                _angles.x = Mathf.Clamp(_angles.x, -_maxAngle, _maxAngle);
                _angles.y = Mathf.Clamp(_angles.y, -_maxAngle, _maxAngle);

                transform.eulerAngles = _angles;

            }
            transform.position = Vector3.Lerp(transform.position, transform.position + startDir, _movingSpeed * Time.fixedDeltaTime);

            transform.Translate(Vector3.forward * _flyightMoveSpeed, Space.Self);


            yield return new WaitForFixedUpdate();
        }
    }
    public LayerMask mask;
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


        RightRay();
        LeftRay();
        TopRay();
        BottomRay();

        tracing = false;

        if (Physics.Raycast(_botomRay, out RaycastHit _bottomHit, _rayDist, mask))
        {
            dist = Vector3.Distance(_botomRay.origin, _bottomHit.point);

            _vertRay = (_botomRay.origin - _bottomHit.point).y * (_rayDist - dist);
            tracing = true;

        }



        if (Physics.Raycast(_topRay, out RaycastHit _topHit, _rayDist, mask))
        {
            dist = Vector3.Distance(_topRay.origin, _topHit.point);

            _vertRay = (_topRay.origin - _topHit.point).y * (_rayDist - dist);
            tracing = true;

        }
        if (Physics.Raycast(_rightRay, out RaycastHit _rightHit, _rayDist, mask))
        {
            dist = Vector3.Distance(_rightRay.origin, _rightHit.point);


            _horRay = (_rightRay.origin - _rightHit.point).x * (_rayDist - dist);
            tracing = true;

        }

        if (Physics.Raycast(_leftRay, out RaycastHit _leftHit, _rayDist, mask))
        {
            dist = Vector3.Distance(_leftRay.origin, _leftHit.point);

            _horRay = (_leftRay.origin - _leftHit.point).x * (_rayDist - dist);
            tracing = true;
        }

        dir = new Vector3(_horRay, _vertRay);
        if (!tracing)
        {
            dir = Vector3.Lerp(dir, Vector3.zero, _smooth);
        }
    }

    private float dist = 10;
    public Vector3 dir = new Vector3();
    private bool _canCollision = true;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Coin>()  || collision.collider.GetComponent<SpeedUp>())
        {
        }
        else
        {
            if (_canCollision)
            {
                _canCollision = false;
                StartCoroutine(WaitForSecond());
                ParticleHolder.instance.CollisionEffect(collision.contacts[0].point);
                ParticleHolder.instance.CollisionInst(collision.contacts[0].point);
                Camera.main.GetComponent<SmoothFollow>().Shake(0.1f, 0.2f);

            }
        }

    }

    private Ray _rightRay;
    private Ray _leftRay;
    private Ray _topRay;
    private Ray _botomRay;
    public float _rayDist;

    private Ray TopRay()
    {
        Ray hit = new Ray(transform.position, transform.up * _rayDist);
        Debug.DrawRay(hit.origin, hit.direction * _rayDist, Color.red);
        _topRay = hit;
        return _topRay;
    }

    private Ray RightRay()
    {
        Ray hit = new Ray(transform.position, transform.right * _rayDist);
        Debug.DrawRay(hit.origin, hit.direction * _rayDist, Color.yellow);
        _rightRay = hit;
        return _rightRay;
    }

    private Ray LeftRay()
    {
        Ray hit = new Ray(transform.position, -transform.right * _rayDist);
        Debug.DrawRay(hit.origin, hit.direction * _rayDist, Color.green);
        _leftRay = hit;
        return _leftRay;
    }

    private Ray BottomRay()
    {
        Ray hit = new Ray(transform.position, -transform.up * _rayDist);
        Debug.DrawRay(hit.origin, hit.direction * _rayDist, Color.blue);
        _botomRay = hit;
        return _botomRay;
    }
    bool closest = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<WaterTile>())
        {
            if (!_waterCollisionEffect.gameObject.activeInHierarchy)
            {
                _waterCollisionEffect.gameObject.SetActive(true);
            }

        }
        else if (other.GetComponent<Bound>())
        {
            if (_camera != null)
            {
                if (!closest)
                {
                    closest = true;
                }

                if (_camera.offset.y > 0)
                {
                    _camera.offset.y -= Time.fixedDeltaTime * 10f;

                }
            }
        }

       
        if(!other.GetComponent<RotationDetector>())
        {
            if (rotating)
            {
                rotating = false;
                StartCoroutine(MoveBackRotate());

            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RotationDetector>())
        {

            _horRay = other.GetComponent<RotationDetector>().rotatingVector.y;
            if (other.GetComponent<RotationDetector>().rotatingVector.x != 0)
            {
                _vertRay = other.GetComponent<RotationDetector>().rotatingVector.x;

            }
            if (!rotating)
            {
                rotating = true;
            }
        }
    }
    bool rotating = false;
    private void OnTriggerExit(Collider other)
    {

        if (_waterCollisionEffect.gameObject.activeInHierarchy)
        {
            _waterCollisionEffect.gameObject.SetActive(false);
        }

        if (closest)
        {
            StartCoroutine(UpCamera());
        }
        if (rotating)
        {
            rotating = false;
            StartCoroutine(MoveBackRotate());

        }

    }
    IEnumerator UpCamera()
    {
        closest = false;

        while (_camera.offset.y < 5f)
        {
            _camera.offset.y += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

    }

    IEnumerator MoveBackRotate()
    {
        rotating = false;

        while (_horRay> 1f)
        {
            _horRay  -= 5*Time.fixedDeltaTime;
            _vertRay -= 5*Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
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
            _lastAngle = _angles;
            _isMousePressed = false;
        }
    }
}


