using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credit_logo : MonoBehaviour {

    private GameObject lum;
    private float realtime;

	// Use this for initialization
	void Start ()
    {
        lum = GameObject.Find("light_credit");
        lum.transform.localPosition = new Vector3(0, 60, 51);
	}
	
	// Update is called once per frame
	void Update ()
    {
        lum.transform.Translate(0, -10* Time.deltaTime, 0);
		if(lum.transform.localPosition.y<=-60) lum.transform.localPosition= new Vector3(0, 60, 51);
    }
}
