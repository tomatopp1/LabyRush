using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Script à appliquer sur la bombe qui sera instantié pour exploser
//Par NICOLAS FINOUX
public class user_bomb_script : MonoBehaviour {
    
    //public
    public int life_Time; //durée de vie de la bombe
    public GameObject bomb_effect;
    //Privée
    private float realTime; //temps reel de la bombe
    private float freq; //la frequence de clignotement
    private float lastTime;//
    private MeshRenderer meshBomb; //ce qui affiche la bombe
    private bool isVisible; //indique si la bombe est entrain de clignoter
    private bool just; // quand la bombe vient juste d'apparaitre ou disparaitre apres un clignotement
    private SphereCollider colliBomb;
	
    // Use this for initialization
	void Start ()
    {
        //init
        meshBomb = gameObject.GetComponentInChildren<MeshRenderer>();
        colliBomb = gameObject.GetComponent<SphereCollider>();
        realTime = 0f;
        colliBomb.enabled = false;
        isVisible = true;
        just = true;
	}
	
    //fonction de clignotement (false pour éteindre, true pour allumer)
    void Clignotement(bool clign)
    {
        if (just)
        {
            freq = 1 - 1 / (life_Time - realTime);
            just = false;
            lastTime = realTime;
        }
        else
        {
            if (realTime > (lastTime + freq))
            {
                meshBomb.enabled = clign;
                isVisible = clign;
                just = true;
            }
        }
    }
	// Update is called once per frame
	void Update ()
    {
        //temps reel
        realTime += Time.deltaTime;

        //clignotement de la bombe
        if (isVisible)
            Clignotement(false);
        else
            Clignotement(true);

        //destruction de la bombe
        if(realTime > life_Time)
        {
            colliBomb.enabled = true;
            //Destroy(gameObject);
        }
        
    }
    
    //Quand la bombe arrive à la fin du compte à rebour
    //on fait apparaitre le fx d'explosion détruit tout les objets comprit dans le collider sauf le sol et le joueur
    //et détruit la bombe
    private void OnTriggerEnter(Collider c)
    {

        GameObject.Instantiate(bomb_effect, gameObject.transform.position, Quaternion.identity);
        if (c.gameObject.tag != "Player" && c.gameObject.tag != "sol")
        {
            Destroy(c.gameObject);
        }
        Destroy(gameObject);

    }


}
