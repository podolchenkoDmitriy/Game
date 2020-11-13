using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private float _moveSpeed = 2f;


    public bool _change = false;
    private bool moved = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<PlayerController>())
        {
            if (!moved)
            {
                moved = true;
                _moveSpeed += _moveSpeed / collision.collider.GetComponent<PlayerController>()._flyightMoveSpeed;
                StartCoroutine(MoveToTop());

            }
        }
    }

 

    private float _elapsed = 0;
    private readonly float _timeToDetect = 0.3f;

    private IEnumerator MoveToTop()
    {
        while (gameObject.activeInHierarchy)
        {
            Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0.9f, 0.9f));

            if (Vector3.Distance(pos, transform.position) > 1f)
            {


                Vector3 targ = pos;


                transform.position = Vector3.MoveTowards(transform.position, targ, 10f * _moveSpeed * Time.fixedDeltaTime);

                if (transform.localScale.x > 0.3f)
                {
                    transform.localScale -= _moveSpeed * Vector3.one * Time.fixedDeltaTime;
                }

                yield return new WaitForFixedUpdate();
            }
            else
            {
                Vector3 targ = pos;

                if (transform.localScale.x > 0.2f)
                {
                    transform.localScale -= _moveSpeed * Vector3.one * Time.fixedDeltaTime;
                }
                transform.position = Vector3.MoveTowards(transform.position, targ, 5f * _moveSpeed * Time.fixedDeltaTime);

                yield return new WaitForFixedUpdate();

                _elapsed += Time.fixedDeltaTime;

                if (_elapsed > _timeToDetect)
                {
                    gameObject.SetActive(false);

                }

            }
        }

    }
}

