using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayExitDirection : MonoBehaviour {

    public GUIStyle customStyle;
    public Transform exit;
    public GameObject arrow;
	
	// display the arrow toward the exit
	void Update () {
        arrow.transform.LookAt(exit);
    }
}
