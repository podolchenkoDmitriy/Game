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

    public SFXManager s_manager;
    public GameObject bul;
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
         
            var my_tag = gameObject.tag;
           
            switch (my_tag)
            {
                case "tree":

                    s_manager.source.PlayOneShot(s_manager.woods[Random.Range(0, s_manager.woods.Length)]);
                    Destroy(Instantiate(s_manager.dust,bul.transform.position,bul.transform.rotation),2f);
                    break;
                case "burrel":

                  
                    s_manager.source.PlayOneShot(s_manager.explo);
                    Destroy(Instantiate(s_manager.explosion, bul.transform.position, bul.transform.rotation), 2f);


                    //Console.WriteLine("Case 2");
                    break;

                default:
                    //Console.WriteLine("Default case");
                    break;
            }


        }

    }
    // Start is called before the first frame update
    void Start()
    {
      gm  = GameObject.Find("GameManager").GetComponent<GameManager>();
      s_manager = GameObject.Find("Camera").GetComponent<SFXManager>();
        bul = GameObject.Find("Bullet");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
