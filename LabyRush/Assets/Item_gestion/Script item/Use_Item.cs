using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Use_Item : MonoBehaviour {

    public GameObject[] arrItem;

    public GameObject fleche;
    public float distJump;

    public float distWall;

    private string dep;
    private Vector3 placeWall;
    private Transform item_tag;
	// Use this for initialization
	void Start ()
    {
        item_tag = gameObject.transform.GetChild(0);
        fleche = GameObject.Instantiate(fleche, gameObject.transform.position, Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update ()
    {
        dep = gameObject.GetComponent<deplacement_script>().dep;

        if (item_tag.tag.Contains("jump"))
        {
            
            //fleche.transform.position = gameObject.transform.position;
            if (dep == "left")
                fleche.transform.position = new Vector3(gameObject.transform.position.x - distJump, transform.position.y, transform.position.z);
            if (dep == "right")
                fleche.transform.position = new Vector3(gameObject.transform.position.x + distJump, transform.position.y, transform.position.z);
            if (dep == "up")
                fleche.transform.position = new Vector3(gameObject.transform.position.x, transform.position.y, transform.position.z + distJump);
            if (dep == "down")
                fleche.transform.position = new Vector3(gameObject.transform.position.x, transform.position.y, transform.position.z - distJump);
        }
        
        

        if (Input.GetButton("Fire1"))
        {
            

            if (item_tag.tag.Contains("bomb"))
                PhotonNetwork.Instantiate(arrItem[0].name, gameObject.transform.position, Quaternion.identity,0);
            if (item_tag.tag.Contains("wall"))
            {
                if (dep == "left")
                    placeWall = new Vector3(gameObject.transform.position.x + distWall, gameObject.transform.position.y, gameObject.transform.position.z);
                if (dep == "right")
                    placeWall = new Vector3(gameObject.transform.position.x - distWall, gameObject.transform.position.y, gameObject.transform.position.z);
                if (dep == "up")
                    placeWall = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - distWall);
                if (dep == "down")
                    placeWall = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + distWall);

                PhotonNetwork.Instantiate(arrItem[1].name, placeWall, Quaternion.identity, 0);
            }
            

            if (item_tag.tag.Contains("jump"))
            {
               
                if (dep == "left")
                    transform.Translate(new Vector3(-distJump, 0, 0));
                if (dep == "right")
                    transform.Translate(new Vector3(distJump, 0, 0));
                if (dep == "up")
                    transform.Translate(new Vector3(0, 0, distJump));
                if (dep == "down")
                    transform.Translate(new Vector3(0, 0, -distJump));
                Destroy(fleche);
            }
            item_tag.tag = "Untagged";
            
        }
	}
}
