using System.Collections;
using UnityEngine;
public class GameManager : MonoBehaviour
{

    public int score;
    public static GameManager instance;

    [Header("UIPanels")]
    [Space]
    public Transform winPanel;
    public Transform losePanel;


    public Transform startPanel;
    public Transform gamePanel;
    public GameObject _bullet;

    public void SecondGameLaunch()
    {
        startPanel.gameObject.SetActive(false);
        gamePanel.gameObject.SetActive(false);
    }
    public void StartGame()
    {
        _bullet.GetComponent<PlayerController>().enabled = true;
        Camera.main.GetComponent<CameraFollow>().enabled = true;
        ParticleHolder.instance._speedUpExplousion.transform.localScale = Vector3.one * 5f;
        ParticleHolder.instance.Explousion(_bullet.transform.position, _bullet.transform);
        ParticleHolder.instance._speedUpExplousion.transform.localScale = Vector3.one;
        startPanel.gameObject.SetActive(false);
        gamePanel.gameObject.SetActive(true);

    }
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
    public void AddPoints(int points)
    {
        StartCoroutine(CountScore(points));
    }

    private IEnumerator CountScore(int points)
    {
        int currentScore = score + points;

        for (int i = score; i < currentScore; i+=10)
        {
            score += 10;
            //_scoreText.text = score.ToString();

            yield return new WaitForFixedUpdate();
        }
    }
    
}
