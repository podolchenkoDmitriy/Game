using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI tmp;

    public int score;

    public  void AddPoints(int points)
    {
      score+=points;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void UI_Update()
    {
      tmp.text = score.ToString();

    }
    // Update is called once per frame
    void Update()
    {
      UI_Update();




    }
}
