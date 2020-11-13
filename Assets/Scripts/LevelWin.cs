using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWin : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerController _player;
    public Camera _cam;
    public Transform _cannon;
    public Transform _cannonBullet;

    public SceneChangeAnim _fadeImage;

    public BonusGame _bonusGame;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            _player = other.GetComponent<PlayerController>();
            _player.enabled = false;
            StartCoroutine(MoveToCenter());

            GameManager.instance.SecondGameLaunch();
            _fadeImage.StartMove();
            StartCoroutine(WaitForFadeImage());
        }
    }
    IEnumerator MoveToCenter()
    {
        while (true)
        {
            _player.transform.Translate((_player.transform.position - transform.position).normalized * Time.fixedDeltaTime * 20f);

            yield return new WaitForFixedUpdate();

            if (Vector3.Distance(transform.position, _player.transform.position)< 0.1f)
            {
                yield return new WaitForFixedUpdate();

                break;
            }
        }
    }
    IEnumerator WaitForFadeImage()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();

            if (_fadeImage.reachedDestination)
            {
                _fadeImage.BackMove();
                _cam.gameObject.SetActive(false);
                StartCoroutine(WaitForStart());
                yield return new WaitForFixedUpdate();

                break;
            }
        }
    }
    IEnumerator WaitForStart()
    {
        _cannon.gameObject.SetActive(true);

        while (true)
        {
            if (_fadeImage.reachedDestination)
            {
                _cannon.GetComponent<Animator>().enabled = true;
                yield return new WaitForSeconds(1f);
                _bonusGame.gameObject.SetActive(true);

                break;
            }
            yield return new WaitForFixedUpdate();

        }
    }
}
