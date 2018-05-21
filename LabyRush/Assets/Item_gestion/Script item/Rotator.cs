using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Script par Nicolas FINOUX
//Pour faire tourner un itel sur lui même
public class Rotator : MonoBehaviour {

	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(new Vector3(0, 50, 0) * Time.deltaTime);
    }
}
