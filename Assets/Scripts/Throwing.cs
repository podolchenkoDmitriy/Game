using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : MonoBehaviour
{
    // Start is called before the first frame update
    Animator _anim;
    GameObject _player;
    public GameObject _axe;
    public Transform _arm;

    [SerializeField] private float _throwForce = 3500f;

    [SerializeField] private float _enemiesSearchRadius = 50f;
    private bool _enemyFound = false;

    void Start()
    {
        if (GetComponent<Animator>())
        {
            _anim = GetComponent<Animator>();

        }
        StartCoroutine(Finding());
    }
    IEnumerator Finding()
    {
        while (!_enemyFound)
        {
            FindPlayer();
            yield return new WaitForSeconds(0.5f);
        }
        yield return StartCoroutine(StartThrow());

    }

    IEnumerator StartThrow()
    {
        if (_anim !=null)
        {
            _anim.SetBool("Attack", true);

        }

        while (_player !=null)
        {
            yield return new WaitForSeconds(1.1f);

            GameObject axe = Instantiate(_axe, _arm);

            Rigidbody rb = axe.GetComponent<Rigidbody>();

            axe.transform.parent = null;

            axe.transform.localScale = transform.localScale*0.5f;

            rb.AddForce(transform.forward*_throwForce, ForceMode.Force);
        }
    }
    private void FixedUpdate()
    {
        if (_player != null)
        {
            transform.LookAt(_player.transform);
        }
    }
    private GameObject FindPlayer()
    {
        if (!_enemyFound)
        {
            Collider[] hitCol = Physics.OverlapSphere(transform.position, _enemiesSearchRadius);
            if (hitCol != null)
            {
                for (int i = 0; i < hitCol.Length; i++)
                {
                    if (hitCol[i].GetComponent<PlayerController>())
                    {
                        _enemyFound = true;
                        _player = hitCol[i].gameObject;

                    }
                }

            }
        }
        return _player;
    }
}
