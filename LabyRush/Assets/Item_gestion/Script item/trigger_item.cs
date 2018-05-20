using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Script de récupération d'item à placer sur les items nommés "item_*"
//Par NICOLAS FINOUX
public class trigger_item : MonoBehaviour {

    private WallController wc;//script de génération du labyrinthe et du placement des items sur la map

    // Use this for initialization
    void Start ()
    {
        //On récupère le script de génération du labyrinthe pour avoir accès à la fonction d'instantiation des items sur la map
        wc = GameObject.Find("Wall").GetComponent<WallController>();
	}

    public void OnTriggerEnter(Collider c)
    {
        
        //Debug.Log(c.gameObject.name);
        if (c.gameObject.tag == "Player")//On vérifie que le gameobject qui traverse le trigger est bien le joueur
        {
            //On vérifie que le fils "user_item" du Player ne contient pas de tag (ou du moins le tag Untagged)
            if (c.gameObject.transform.GetChild(0).tag.Contains("Untagged") != false)
            {
                //Sinon user_item obtient le tag de l'item qui l'a traversé
                c.gameObject.transform.GetChild(0).tag = gameObject.tag; ;
                wc.PopItem();//On appelle la fonction pour faire pop un item sur la map
                Destroy(gameObject);//on détruit l'item sur lequel ce script est associé
                
            }
        }

    }
}
