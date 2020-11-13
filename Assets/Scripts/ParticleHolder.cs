using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHolder : MonoBehaviour
{
    public static ParticleHolder instance;
    public Particle _collisionEffect;
    public Particle _speedUpExplousion;
    public Particle _moneyParticle;
    public Particle[] _exploudPart;
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
    public void Explousion(Vector3 pos, Transform parent )
    {
        Instantiate(_speedUpExplousion, pos, parent.rotation );
       
    }

    public void AddMoney(Vector3 pos)
    {
        Instantiate(_moneyParticle, pos, Quaternion.identity);

    }

    public void CollisionInst(Vector3 pos )
    {
        int i = Random.Range(0, _exploudPart.Length);
        Instantiate(_exploudPart[i], pos, Quaternion.identity, transform);
    }

}
