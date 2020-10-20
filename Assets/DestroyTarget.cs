using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DestroyTarget : MonoBehaviour
{
    public GameObject[] Rubish;
    public int points;
    public GameManager gm;
    public GameObject textPoints;
    public void InstantRubish()
    {
      //TODO
    }



    public void OnTriggerEnter(Collider col)
    {

      if(col.gameObject.tag=="Bullet")
      {

        gm.AddPoints(points);
        InstantRubish();
        var coins_text = Instantiate(textPoints,col.gameObject.transform.position,textPoints.transform.rotation);
        coins_text.transform.parent = null;
        coins_text.GetComponent<TextMeshPro>().text = points.ToString();
        coins_text.GetComponent<Rigidbody>().velocity  = new Vector3(0,5,10);        
        Destroy(coins_text,2f);
        Destroy(gameObject);

      }

    }
    // Start is called before the first frame update
    void Start()
    {
      gm  = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
