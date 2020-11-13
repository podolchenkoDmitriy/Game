using System.Collections;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    private PlayerController bullet;
    public float speed = 5;

    // Start is called before the first frame update
    private void Awake()
    {
        bullet = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
        StartCoroutine(RotateObj());
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.activeInHierarchy)
        {

            if (collision.collider.GetComponent<PlayerController>())
            {
                Destroying();
                if (bullet._flyightMoveSpeed < bullet._maxSpeed)
                {
                    bullet._flyightMoveSpeed += 0.05f;

                }
            }
        }
    }
    private void Destroying()
    {
        ParticleHolder.instance.Explousion(transform.position, bullet.transform);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    private IEnumerator RotateObj()
    {
        while (true)
        {
            transform.eulerAngles += new Vector3(0, speed, speed);
            yield return new WaitForFixedUpdate();
        }
    }

}
