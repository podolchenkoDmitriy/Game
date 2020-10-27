using System.Collections;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    private PlayerController bullet;
    public float speed = 5;

    // Start is called before the first frame update
    private void Start()
    {
        bullet = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
        StartCoroutine(RotateObj());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.activeInHierarchy)
        {

            if (other.GetComponent<PlayerController>())
            {
                Destroying();

                bullet._flyightMoveSpeed += 0.25f;
            }
        }
    }

    private void Destroying()
    {
        ParticleHolder.instance.Explousion(transform.position);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    private IEnumerator RotateObj()
    {
        while (true)
        {
            transform.eulerAngles += new Vector3(0, 0, speed);
            yield return new WaitForFixedUpdate();
        }
    }

}
