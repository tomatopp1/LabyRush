using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script de déplacement du joueur à placer sur le préfab du Player
//Par NICOLAS FINOUX
public class deplacement_script : MonoBehaviour {

    public float speed;//la vitesse de base du joueur
    public int seconde;//Nb de seconde entre chaque Boost
    public int nbBoost;//Nb de boost que le joueur peut atteindre
    public string dep;//Variable public qui indiquera le déplacement actuel du joueur, appelé par le script des items

    private int boost;
    private float timeBoost;
    private bool pause;//si le joueur a décidé d'arreter de bouger en appuyant sur espace
    private PhotonView Pv;//script Photon View
    private deplacement_script ds;//script de déplacement
    private Use_Item Ui;//script des items

    private void Start()
    {
        //On récupère les différents script associé au joueur possédant se script
        Pv = gameObject.GetComponent<PhotonView>(); 
        ds = gameObject.GetComponent<deplacement_script>();
        Ui = gameObject.GetComponent<Use_Item>();
        //On vérifie avec la avriable isMine si le sur lequel est associé le script 
        //est bien celui entrain de jouer et on désactive les scripts si ce n'est pas le cas
        ds.enabled = Pv.isMine;
        Ui.enabled = Pv.isMine;
        //initialisation des différentes variables
        boost = 1;
        timeBoost = 0;
        pause = true;
    }

    void FixedUpdate()
    {
        //On incrémente le temps entre chaque boost
        if (pause != true) timeBoost += Time.deltaTime;
        //On augmente le boost du joueur a chaque fois que timeBoost à atteind le temps rentré en variable public
        if (timeBoost >= seconde & boost <= nbBoost)
        {
            this.boost++;
            this.timeBoost = 0;
        }
        
        if (Input.anyKey) pause = false;//permet de relancer le compteur de boost

        //Les différents déplacement sont de simple translation
        //Je passe par un une variable string pour tester kes différents cas et 
        //je peux ainsi l'utiliser par d'autre script pour connaitre la direction du joueur
        
        //Aller à gauche
        if (Input.GetButton("Horizontal") && Input.GetAxis("Horizontal") < 0) dep = "left";
        if (dep == "left") transform.Translate(new Vector3(-speed*boost, 0, 0)*Time.deltaTime);
        //Aller à droite
        if (Input.GetButton("Horizontal") && Input.GetAxis("Horizontal") > 0) dep = "right";
        if (dep == "right") transform.Translate(new Vector3(speed * boost, 0, 0) * Time.deltaTime);
        //Aller en haut
        if (Input.GetButton("Vertical") && Input.GetAxis("Vertical") > 0) dep = "up";
        if (dep == "up") transform.Translate(new Vector3(0, 0, speed * boost) * Time.deltaTime);
        //Aller en bas
        if (Input.GetButton("Vertical") && Input.GetAxis("Vertical") < 0) dep = "down";
        if (dep == "down") transform.Translate(new Vector3(0, 0, -speed * boost) * Time.deltaTime);
        //stopper
        if (Input.GetKey(KeyCode.Space)) dep = "stop" ;//La barre d'espace stop le joueur mais ne reinitialise pas ses nombre de boost
        if (dep == "stop") transform.Translate(new Vector3(0, 0, 0));

        
    }
    //Quand le joueur rentre dans un mur, il perd ses boosts
    public void OnCollisionEnter(Collision collision)
    {
        this.boost = 1;
        this.pause = true;
        transform.Translate(new Vector3(0, 0, 0));
        this.timeBoost = 0;
    }

}
