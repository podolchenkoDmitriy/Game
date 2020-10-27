using System.Collections;
using TMPro;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI _scoreText;

    public int score;
    public static GameManager instance;

    [Header("UIPanels")]
    [Space]
    public Transform winPanel;
    public Transform losePanel;

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
            _scoreText.text = score.ToString();

            yield return new WaitForFixedUpdate();
        }
    }
}
