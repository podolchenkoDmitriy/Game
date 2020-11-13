using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject fire;
    public float _delay;

    IEnumerator StartThrow()
    {
        while (true)
        {
            fire.SetActive(true);
            yield return new WaitForSeconds(_delay);

            fire.SetActive(false);

        }
    }
    void Start()
    {
        StartCoroutine(StartThrow());
    }

    
}
