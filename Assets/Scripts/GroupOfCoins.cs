using System.Collections;
using UnityEngine;

public class GroupOfCoins : MonoBehaviour
{
    // Start is called before the first frame update
    private Coin[] _coins;
    private Transform _player;
    private float _moveSpeed = 2f;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>().transform;
    }

    private void Start()
    {
        _coins = GetComponentsInChildren<Coin>();
        MoveUP();

    }

    private IEnumerator StartMove()
    {
        foreach (Coin item in _coins)
        {
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(RotatingRoutine(item.transform));

        }
    }

    private void MoveUP()
    {

        StartCoroutine(StartMove());


    }
    
    private IEnumerator RotatingRoutine(Transform target)
    {
        bool change = target.GetComponent<Coin>()._change;
        while (true)
        {

            if (target.localPosition.y < 1f &&  !change)
            {
                target.Translate(Vector3.up * _moveSpeed * Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();

            }
            else
            {
                change = true;
            }
            if (target.localPosition.y > -1f  && change)
            {
                target.Translate(Vector3.down * _moveSpeed * Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();

            }
            else 
            {
                change = false;
            }
            yield return new WaitForFixedUpdate();

            if (_player != null)
            {

                target.LookAt(_player);

            }
        }
       

    }
}

