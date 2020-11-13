using Exploder.Utils;
using System.Collections;
using UnityEngine;

public enum TypeOfObject
{
    Box,
    Person,
    Tower,
    None
}
public class DestroyTarget : MonoBehaviour
{
    public int points;
    public TypeOfObject typeOf = TypeOfObject.None;
    public GameObject _coinPrefab;
    readonly int _countOfCoins = 5;
    IEnumerator DrawCoins(Collision col)
    {
        for (int i = 0; i < _countOfCoins; i++)
        {
            Instantiate(_coinPrefab, col.contacts[0].point + new Vector3(Random.Range(-1f,1f), Random.Range(-1f, 1f)), Quaternion.identity);
            yield return new WaitForFixedUpdate();
        }
    }
    private void Destroyable(Collision col)
    {
        if (typeOf == TypeOfObject.Box)
        {
            ParticleHolder.instance.AddMoney(col.contacts[0].point);
            StartCoroutine(DrawCoins(col));
        }
        GameManager.instance.AddPoints(points);

        ExploderSingleton.Instance.ExplodeObject(gameObject);
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<PlayerController>())
        {
            Destroyable(collision);
        }

    }

}
