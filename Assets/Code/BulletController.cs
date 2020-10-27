using UnityEngine;
using UnityStandardAssets.Utility;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    public static bool _isMousePressed = false;
    public Vector3 dir;
    public Transform _head;
    public float moveSpeedInput = 60f;


    // Start is called before the first frame update


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            print("KABOOOM!");
            Camera.main.transform.parent = null;
            Destroy(gameObject);
            //TODO restart level




        }


        ParticleHolder.instance.CollisionEffect(collision.contacts[0].point);

        Camera.main.GetComponent<SmoothFollow>().Shake(0.2f, 0.3f);
    }
    private void OnTriggerEnter(Collider other)
    {
    }

    private void FixedUpdate()
    {

        dir = new Vector2(Input.GetAxis("Mouse X") * moveSpeedInput * Time.fixedDeltaTime, Input.GetAxis("Mouse Y") * moveSpeedInput * Time.fixedDeltaTime);

        transform.position = Vector2.Lerp(transform.position, transform.position + dir , moveSpeedInput * Time.fixedDeltaTime);

        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles,  dir, 0.1f);

        // transform.position = Vector3.Lerp(transform.position, _head.position, moveSpeedInput * Time.fixedDeltaTime);

    }

    // Update is called once per frame
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
        /*  t = Mathf.Clamp(t, 0, 1);
          if (speedup_timer >= 0)
          {
              t += 0.5f * Time.deltaTime;
              speedup_timer -= Time.deltaTime;

              fly_speed = cyrcle_speed;
              Camera.main.fieldOfView = Mathf.Lerp(60, 70, t); 

          }
          else
          {
              t -= 0.5f * Time.deltaTime;
              fly_speed =default_speed;
              Camera.main.fieldOfView = Mathf.Lerp(60, 70, t); 
          }



          if (Input.touchCount > 0)
          {
              touchDelta = Input.GetTouch(0).deltaPosition;
              keyboard_input.x = Input.GetTouch(0).deltaPosition.x * Time.deltaTime;
              keyboard_input.y = Input.GetTouch(0).deltaPosition.y * Time.deltaTime;


          }
          else 
          {
              keyboard_input.x = Input.GetAxis("Horizontal");
              keyboard_input.y = Input.GetAxis("Vertical");


          }





          direction_velocity.z = fly_speed;
          direction_velocity.x = keyboard_input.x * swipe_speed;
          direction_velocity.y = keyboard_input.y* swipe_speed;



    */


    }
}
