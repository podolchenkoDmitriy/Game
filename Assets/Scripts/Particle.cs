using System.Collections;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public float _LifeTime;

    private IEnumerator DestroyParticle()
    {
        yield return new WaitForSeconds(_LifeTime);
        gameObject.SetActive(false);
    }
    private void Start()
    {
        StartCoroutine(DestroyParticle());
    }
}
