using System.Collections;
using UnityEngine;

public class SceneChangeAnim : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        startPos = transform.position;
    }
    public GameObject _particle;
    public InterpType interpolation = InterpType.SmootherStep;
    public bool m_isMoving = false;
    Vector3 startPos;
    public enum InterpType
    {
        Linear,
        EaseOut,
        EaseIn,
        SmoothStep,
        SmootherStep
    };
    public float forwardTime = 1.5f;
    public float backTime = 1f;
    public bool reachedDestination = false;

    public void Move(Vector3 dest, float time)
    {
        StartCoroutine(MoveRoutine(dest, time));
    }
    public Vector3 dest;
    public void StartMove()
    {
        reachedDestination = false;

        Move(dest, forwardTime);
    }
    public void BackMove()
    {
        reachedDestination = false;
        Move(startPos, backTime);

    }
    private IEnumerator MoveRoutine(Vector3 destination, float timeToMove)
    {
        _particle.SetActive(false);

        Vector3 startPosition = transform.position;


        float elapsedTime = 0f;

        m_isMoving = true;

        while (!reachedDestination)
        {
            // if we are close enough to destination
            if (Vector3.Distance(transform.position, destination) < 0.01f)
            {
                _particle.SetActive(true);
                reachedDestination = true;
                break;
            }
            // track the total running time
            elapsedTime += Time.fixedDeltaTime;

            // calculate the Lerp value
            float t = Mathf.Clamp(elapsedTime / timeToMove, 0f, 1f);

            switch (interpolation)
            {
                case InterpType.Linear:
                    break;
                case InterpType.EaseOut:
                    t = Mathf.Sin(t * Mathf.PI * 0.5f);
                    break;
                case InterpType.EaseIn:
                    t = 1 - Mathf.Cos(t * Mathf.PI * 0.5f);
                    break;
                case InterpType.SmoothStep:
                    t = t * t * (3 - 2 * t);
                    break;
                case InterpType.SmootherStep:
                    t = t * t * t * (t * (t * 6 - 15) + 10);
                    break;
            }

            // move the game piece
            transform.position = Vector3.Lerp(startPosition, destination, t);

            // wait until next frame
            yield return null;



        }

        m_isMoving = false;


    }
}
