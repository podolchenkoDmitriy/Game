using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScaleUp : MonoBehaviour
{
    // Start is called before the first frame update
    public Image[] _images;

    public float _scaleUpSpeed = 10f;
    void Start()
    {
        
    }

    // Update is called once per frame
    int _numOfImage = 0;
    public float _forceMultiplier = 1f;
    private void OnEnable()
    {
        StartCoroutine(UpToScale());
    }
    private void OnDisable()
    {
        _numOfImage = 0;
        foreach (var item in _images)
        {
            item.fillAmount = 0;
        }
    }
    IEnumerator UpToScale()
    {
        while (_images[_numOfImage].fillAmount < 1f)
        {
            _images[_numOfImage].fillAmount += Time.fixedDeltaTime * _scaleUpSpeed;
            _forceMultiplier = _numOfImage + _images[_numOfImage].fillAmount;
            yield return new WaitForFixedUpdate();
        }
        if (_numOfImage < _images.Length - 1)
        {
            _numOfImage++;
            
            yield return StartCoroutine(UpToScale());
        }
        else
        {
            yield return StartCoroutine(DownToScale());
        }
       
    }
    IEnumerator DownToScale()
    {
        while (_images[_numOfImage].fillAmount > Time.fixedDeltaTime)
        {
            _images[_numOfImage].fillAmount -=  Time.fixedDeltaTime * _scaleUpSpeed;
            _forceMultiplier = _numOfImage - (1- _images[_numOfImage].fillAmount);

            yield return new WaitForFixedUpdate();
        }
        if (_numOfImage > 0)
        {
            _numOfImage--;
            yield return StartCoroutine(DownToScale());
        }
        else
        {
            yield return StartCoroutine(UpToScale());
        }
    }
    void Update()
    {
        
    }
}
