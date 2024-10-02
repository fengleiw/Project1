using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canonScript : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bullet;
    float timebetween;
    public float starttimebetween;
    DetectionZone zone;
    // Start is called before the first frame update
    void Start()
    {
        timebetween = starttimebetween;   
        zone = GetComponent<DetectionZone>();
    }

    // Update is called once per frame
    void Update()
    {
        if(zone.detectedColliders.Count > 0)
        {
            Debug.Log("1");
        }
        //if(timebetween <= 0)
        //{
        //    Instantiate(bullet, firepoint.position, firepoint.rotation);
        //    timebetween = starttimebetween;
        //}
        //else
        //{
        //    timebetween -= Time.deltaTime;
        //}
    }
}
