using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceManager : MonoBehaviour
{
  public float all_distance;
  public float current_bullet_distance;
  public GameObject bullet,start_point,end_point;
  public float percent_value;
  public Slider display_path_length;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

      all_distance = Vector3.Distance(start_point.transform.position, end_point.transform.position);
      current_bullet_distance = Vector3.Distance(bullet.transform.position, end_point.transform.position);

      percent_value = (100*current_bullet_distance)/all_distance ;

      display_path_length.value = 100-percent_value;

    }
}
