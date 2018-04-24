using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_item : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider c)
    {
        
        Debug.Log(c.gameObject.name);
        if (c.gameObject.tag == "Player")
        {
            if (c.gameObject.transform.GetChild(0).tag.Contains("Untagged") != false)
            {
                c.gameObject.transform.GetChild(0).tag = gameObject.tag; ;
                Destroy(gameObject);
            }
            //c.gameObject.tag = gameObject.tag;
            //GameObject.Instantiate(Enemy, transform.position, Quaternion.identity);
        }

    }
}
