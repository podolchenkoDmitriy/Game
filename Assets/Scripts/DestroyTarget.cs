using Exploder.Utils;
using UnityEngine;

public class DestroyTarget : MonoBehaviour
{
    public int points;

    private void Destroyable()
    {
        GameManager.instance.AddPoints(points);

        ExploderSingleton.Instance.ExplodeObject(gameObject);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<PlayerController>())
        {
            Destroyable();
        }

    }

}
