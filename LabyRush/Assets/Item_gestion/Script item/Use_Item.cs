using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Use_Item : MonoBehaviour {

    public GameObject[] arrItem;
    public int distJump;

    private string dep;
    private Transform item_tag;
	// Use this for initialization
	void Start ()
    {
        item_tag =gameObject.transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetButton("Fire1"))
        {
                        
            if (item_tag.tag.Contains("bomb"))
                GameObject.Instantiate(arrItem[0], gameObject.transform.position, Quaternion.identity);
            if (item_tag.tag.Contains("wall"))
                GameObject.Instantiate(arrItem[1], gameObject.transform.position, Quaternion.identity);

            if (item_tag.tag.Contains("jump"))
            {
                dep = gameObject.GetComponent<deplacement_script>().dep;
                if (dep == "left")
                    transform.Translate(new Vector3(-distJump, 0, 0));
                if (dep == "right")
                    transform.Translate(new Vector3(distJump, 0, 0));
                if (dep == "up")
                    transform.Translate(new Vector3(0, 0, distJump));
                if (dep == "down")
                    transform.Translate(new Vector3(0, 0, -distJump));
            }
            item_tag.tag = "Untagged";
            
        }
	}
}
