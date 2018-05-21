using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Script d'utilisation des items par le joueur et à placer sur le préfab du Player
//par NICOLAS FINOUX

public class Use_Item : MonoBehaviour {
    
    //Public
    public GameObject[] arrItem;//list des items qui peuvent être instantié par la Player
    public GameObject fleche;//gameObject fleche
    public float distJump;//distance de Téléportation vers la direction du Player
    public float distWall;//distance ou sera instancié l'item mur par rapport au joueur
    
    //Private
    private string dep;//la direction du joueur
    private Vector3 placeWall;//position du mur
    private Transform item_tag;//la tag du fils du Player "user_item" qui contient le tag du l'item qu'il a en sa possession
    private bool canJump;//booleen pour savoir si le joueur peux utiliser la téléportation ou non
	// Use this for initialization
	void Start ()
    {
        item_tag = gameObject.transform.GetChild(0);//on récupère l'item fils du player contenant le tag de l'item
        fleche = GameObject.Instantiate(fleche, new Vector3 (0,0,-1000), Quaternion.identity);//On instantie la fleche loin.
    }
	
	// Update is called once per frame
	void Update ()
    {
        dep = gameObject.GetComponent<deplacement_script>().dep;//On récupère la variable publique de la direction du joueur
        RaycastHit hit;//on instantie un raycast qui permettra de détecter les murs devant le joueur
        canJump = true;
        
        //On vérifie que le joueur ne se trouve pas prêt du bord du labyronthe et donc l'empêche d'utiliser le saut
        if (item_tag.tag.Contains("jump"))
        {
            //Selon la direction dans laquelle se déplace le joueur on envoi un raycast à la distance d'un saut que le joueur peut réaliser
            //Si il détecte un mur contenant le tag "border" alors la variable canJump est mit à false
            //de plus on met le fleche au niveau de la distance de saut du joueur comme indicateur
            if (dep == "left")
            {
                fleche.transform.position = new Vector3(gameObject.transform.position.x - distJump, transform.position.y, transform.position.z);
                if (Physics.Raycast(transform.position, Vector3.left, out hit, distJump) && hit.transform.tag == "border")
                    canJump = false;
            }
            if (dep == "right")
            {
                fleche.transform.position = new Vector3(gameObject.transform.position.x + distJump, transform.position.y, transform.position.z);
                if (Physics.Raycast(transform.position, Vector3.right, out hit, distJump) && hit.transform.tag == "border")
                    canJump = false;
            }
            if (dep == "up")
            {
                fleche.transform.position = new Vector3(gameObject.transform.position.x, transform.position.y, transform.position.z + distJump);
                if (Physics.Raycast(transform.position, Vector3.forward, out hit, distJump) && hit.transform.tag == "border")
                    canJump = false;
            }    
            if (dep == "down")
            {
                fleche.transform.position = new Vector3(gameObject.transform.position.x, transform.position.y, transform.position.z - distJump);
                if (Physics.Raycast(transform.position, -Vector3.forward, out hit, distJump) && hit.transform.tag == "border")
                    canJump = false;
            }
                
        }
        
        
        //Utilisation des différents items
        if (Input.GetButton("Fire1"))
        {
            //Si le joueur possede une bombe l'instantie à sa position
            if (item_tag.tag.Contains("bomb"))
            {
                PhotonNetwork.Instantiate(arrItem[0].name, gameObject.transform.position, Quaternion.identity, 0);
                item_tag.tag = "Untagged";
            }
                

            if (item_tag.tag.Contains("wall")&& dep != "stop" )
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
                item_tag.tag = "Untagged";
            }
            //Si le joueur a l'item de saut et que la variable canJump est à true, alors le téléporte à la distance de saut rentré en variable public
            //et dans la direction dans laquelle il se déplace
            if (item_tag.tag.Contains("jump") && canJump && dep != "stop")
            {
               
                if (dep == "left")
                    transform.Translate(new Vector3(-distJump, 0, 0));
                if (dep == "right")
                    transform.Translate(new Vector3(distJump, 0, 0));
                if (dep == "up")
                    transform.Translate(new Vector3(0, 0, distJump));
                if (dep == "down")
                    transform.Translate(new Vector3(0, 0, -distJump));

                fleche.transform.position = new Vector3(0, 0, -1000);
                item_tag.tag = "Untagged";
            }

            //dans tout les cas, une fois que l'item est utilisé, alors le tag est retiré au joueur l'mpechant de réutiliser de nouveau l'item
        }
	}
}
