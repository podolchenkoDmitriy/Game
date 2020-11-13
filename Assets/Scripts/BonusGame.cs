using System.Collections;
using UnityEngine;

public class BonusGame : MonoBehaviour
{
    // Start is called before the first frame update
    public int _timesToShoot = 3;
    public RectTransform _power;
    public RectTransform _pointer;
    public GameObject _infinityAnim;
    private float _moveHeight = 0;
    private float _moveWidth = 0;
    public float _multiplacator = 0.01f;
    public float _moveSpeed = 10f;
    public Transform _cannon;
    public Transform _cannonCarriage;
    public Camera _cam;
    private void Awake()
    {
       // _simulation = _bullet.GetComponent<TrajectorySimulation>();
        _moveHeight = Screen.height * 0.25f;
        _moveWidth = Screen.width * 0.25f;
        _startPositionCamera = _cam.transform.position;
        angles = _bullet.transform.eulerAngles;
    }
    Vector3 _startPositionCamera;
    private bool next = false;
    public Rigidbody _bullet;
    public float _force;
    


    public Transform _startPoint;
    private void OnEnable()
    {
        StartAiming();
    }
    Vector3 angles;
    private void StartAiming()
    {
        StartCoroutine(WaitForTap());
        //_cannon.rotation = _cannon.parent.rotation;
        _bullet.transform.parent = _cannon;
        _bullet.transform.position = _startPoint.position;
        _bullet.constraints = RigidbodyConstraints.FreezeAll;
        _bullet.transform.rotation = _cannon.transform.rotation;
        _bullet.gameObject.SetActive(true);
        StartCoroutine(SetOffAnim());
    }
    IEnumerator SetOffAnim()
    {
        yield return new WaitForSeconds(1.2f);
        if (_cannon.GetComponentInParent<Animator>())
        {
            _cannon.GetComponentInParent<Animator>().enabled = false;
            //_cam.GetComponent<CameraFollow>().enabled = false;

        }
    }
    private IEnumerator StartPointerMoveByY()
    {
        _pointer.gameObject.SetActive(true);
        while (!next)
        {
            while (_pointer.anchoredPosition.y < _moveHeight)
            {
                _pointer.Translate(Vector3.up * _moveHeight * Time.fixedDeltaTime);
                _cannon.eulerAngles += Vector3.back * _moveHeight * Time.fixedDeltaTime * _multiplacator;

                yield return null;
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    next = true;

                    break;
                }
            }
            while (_pointer.anchoredPosition.y > -_moveHeight)
            {
                _pointer.Translate(Vector3.down * _moveHeight * Time.fixedDeltaTime);
                _cannon.eulerAngles += Vector3.forward * _moveHeight * Time.fixedDeltaTime * _multiplacator;

                yield return null;
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    next = true;

                    break;
                }
            }
        }

        _pointer.position = _pointer.position;
        yield return StartCoroutine(StartPointerMoveByX());
    }

    private IEnumerator StartPointerMoveByX()
    {

        next = false;
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        while (!next)
        {
            while (_pointer.anchoredPosition.x < _moveWidth)
            {
                _pointer.Translate(Vector3.right * _moveHeight * Time.fixedDeltaTime);
                _cannon.eulerAngles += Vector3.up * _moveHeight * Time.fixedDeltaTime * _multiplacator;

                yield return new WaitForFixedUpdate();
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    next = true;

                    break;
                }
            }
            while (_pointer.anchoredPosition.x > -_moveWidth)
            {
                _pointer.Translate(Vector3.left * _moveHeight * Time.fixedDeltaTime);
                _cannon.eulerAngles += Vector3.down * _moveHeight * Time.fixedDeltaTime * _multiplacator;

                yield return new WaitForFixedUpdate();
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    next = true;

                    break;
                }
            }
        }
        yield return StartCoroutine(WaitForPress());

    }

    private Vector3 _angles;
    public float _maxAngle = 60f;
    private Vector2 _tapPos;
    public void RestartAiming()
    {
        gameObject.SetActive(false);
        gameObject.SetActive(true);
        _power.gameObject.SetActive(false);
        _timesToShoot++;

    }

    private IEnumerator WaitForTap()
    {
        _infinityAnim.SetActive(true);

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _tapPos = new Vector2(-Camera.main.ScreenToViewportPoint(Input.mousePosition).y, Camera.main.ScreenToViewportPoint(Input.mousePosition).x);
                _timesToShoot--;
                break;
            }
            yield return null;
        }
        yield return StartCoroutine(MoveCannon());
    }

    private IEnumerator MoveCannon()
    {

        while (Input.GetKey(KeyCode.Mouse0))
        {
            _angles = (-_tapPos + new Vector2(-Camera.main.ScreenToViewportPoint(Input.mousePosition).y, Camera.main.ScreenToViewportPoint(Input.mousePosition).x)) * _maxAngle;

            _angles.x = Mathf.Clamp(_angles.x, -_maxAngle, _maxAngle);
            _angles.y = Mathf.Clamp(_angles.y, -_maxAngle, _maxAngle);
            _angles = new Vector3(_angles.x, _angles.y, 0);
            _cannon.eulerAngles = _angles + _cannon.parent.eulerAngles;
            print(_cannonCarriage.parent.eulerAngles);
            yield return new WaitForFixedUpdate();
        }
        yield return StartCoroutine(WaitForPress());

    }
    float force = 1;

    private IEnumerator WaitForPress()
    {
        _power.gameObject.SetActive(true);
        _infinityAnim.SetActive(false);
        if (_pointer != null)
        {
            _pointer.gameObject.SetActive(false);

        }

        while (gameObject.activeInHierarchy)
        {
            if (GetComponentInChildren<ScaleUp>())
            {
                force = GetComponentInChildren<ScaleUp>()._forceMultiplier*10f;

            }
            if (Input.GetKeyDown(KeyCode.Mouse0) && !PointerDown.buttonPressed)
            {
                _bullet.useGravity = false;

                _power.gameObject.SetActive(false);
                _bullet.GetComponent<Explousion>().InitExpl();
                _bullet.ResetCenterOfMass();
                _bullet.velocity = Vector3.zero;
                _bullet.transform.parent = null;
                _bullet.constraints = ~RigidbodyConstraints.FreezeAll;
                _bullet.useGravity = true;

                _bullet.AddForce(_bullet.transform.forward * (_force + force), ForceMode.VelocityChange);
               // _cam.GetComponent<CameraFollow>().enabled = true;

                StartCoroutine(MoveByPlayer());
                break;

            }

            yield return null;

        }
    }

    public Vector3 offset;
    IEnumerator MoveByPlayer()
    {
        while (true)
        {
            if (Explousion._collision)
            {
                MoveUp();
                //_cam.GetComponent<CameraFollow>().enabled = false;

                break;
            }
           

            // move the game piece
            _cam.transform.position = Vector3.Slerp(_cam.transform.position, _bullet.transform.position + offset, Time.fixedDeltaTime );

            Quaternion rotation = Quaternion.LookRotation( - _cam.transform.position + _bullet.transform.position);
            Quaternion smoothRotation = Quaternion.Slerp(_cam.transform.rotation, rotation, Time.fixedDeltaTime * _moveSpeed);
            _cam.transform.rotation = smoothRotation;
            yield return new WaitForFixedUpdate();
        }
    }
    private bool _moveUp = false;
    private void MoveUp()
    {
        destinationDone = false;
        _moveUp = true;
        Vector3 pos = _cam.transform.position + Vector3.up * 10f;

        StartCoroutine(MoveRoutine(_cam.transform, pos, 1f));


    }

    private bool destinationDone = false;

    private void MoveDown()
    {
        _moveUp = false;
        destinationDone = true;
        Vector3 pos = _cam.transform.position - Vector3.up * 10f;

        StartCoroutine(MoveRoutine(_cam.transform, pos, 1.5f));
    }
    private IEnumerator MoveRoutine(Transform _transformObj, Vector3 destination, float timeToMove)
    {
        Vector3 startPosition = _transformObj.position;

        bool reachedDestination = false;

        float elapsedTime = 0f;


        while (!reachedDestination)
        {
            // if we are close enough to destination
            if (Vector3.Distance(_transformObj.position, destination) < 0.01f)
            {
                reachedDestination = true;
                break;
            }
            // track the total running time
            elapsedTime += Time.fixedUnscaledDeltaTime;


            // calculate the Lerp value
            float t = Mathf.Clamp(elapsedTime / timeToMove, 0f, 1f);

            t = t * t * t * (t * (t * 6 - 15) + 10);


            // move the game piece
            _transformObj.position = Vector3.Lerp(startPosition, destination, t);

            // wait until next frame
            yield return null;
        }
        if (destinationDone)
        {
            if (_timesToShoot > 0)
            {
                StartCoroutine(MoveBack(_cam.transform, _startPositionCamera, 1f));
                
            }
        }
        if (_moveUp)
        {
            MoveDown();

        }
    }


    private float currentAmount = 0f;
    private float maxAmount = 5f;
    private bool _startCheckingTime = false;

    private IEnumerator UnscaleTime()
    {
        if (!_startCheckingTime)
        {
            yield return new WaitForSecondsRealtime(1.5f);
            Explousion._collision = true;
            _startCheckingTime = true;
        }
        else
        {
            _startCheckingTime = false;
        }

      

    }
  
    // Update is called once per frame
    private void Update()
    {
       

        if (Explousion._collision)
        {
            Explousion._collision = false;

            StartCoroutine(UnscaleTime());



            if (Time.timeScale == 1.0f)
                Time.timeScale = 0.3f;

            else

                Time.timeScale = 1.0f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }


        if (Time.timeScale == 0.03f)
        {

            currentAmount += Time.deltaTime;
        }

        if (currentAmount > maxAmount)
        {

            currentAmount = 0f;
            Time.timeScale = 1.0f;

        }

    }


    private IEnumerator MoveBack(Transform _transformObj, Vector3 destination, float timeToMove)
    {
        Vector3 startPosition = _transformObj.position;

        bool reachedDestination = false;

        float elapsedTime = 0f;


        while (!reachedDestination)
        {
            // if we are close enough to destination
            if (Vector3.Distance(_transformObj.position, destination) < 0.01f)
            {
                reachedDestination = true;
                gameObject.SetActive(false);
                gameObject.SetActive(true);
                break;
            }
            // track the total running time
            elapsedTime += Time.fixedDeltaTime;

            _transformObj.LookAt(_cannon.transform);
            // calculate the Lerp value
            float t = Mathf.Clamp(elapsedTime / timeToMove, 0f, 1f);

            t = t * t * t * (t * (t * 6 - 15) + 10);


            // move the game piece
            _transformObj.position = Vector3.Lerp(startPosition, destination, t);

            // wait until next frame
            yield return null;
        }
      
    }
}
