using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHolder : MonoBehaviour
{
    public static ParticleHolder instance;
    public Particle _collisionEffect;
    public Particle _speedUpExplousion;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(this);
        }

    }

    public void CollisionEffect(Vector3 pos)
    {
        Instantiate(_collisionEffect, pos, Quaternion.identity);
    }
    public void Explousion(Vector3 pos)
    {
        Instantiate(_speedUpExplousion, pos, Quaternion.identity);

    }

}
