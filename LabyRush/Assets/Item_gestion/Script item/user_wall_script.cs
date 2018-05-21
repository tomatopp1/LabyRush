using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class user_wall_script : MonoBehaviour {

    //public
    public int life_Time; //durée de vie de la bombe
    public GameObject destroyEffect;

    //Privée
    private float realTime; //temps reel de la bombe
    private float freq; //la frequence de clignotement
    private float lastTime;//
    private MeshRenderer meshWall; //ce qui affiche la bombe
    private bool isVisible; //indique si la bombe est entrain de clignoter
    private bool just; // quand la bombe vient juste d'apparaitre ou disparaitre apres un clignotement

    // Use this for initialization
    void Start()
    {
        //init
        meshWall = gameObject.GetComponentInChildren<MeshRenderer>();
        realTime = 0f;
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
                meshWall.enabled = clign;
                isVisible = clign;
                just = true;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        //temps reel
        realTime += Time.deltaTime;

        //clignotement de la bombe
        if (isVisible)
            Clignotement(false);
        else
            Clignotement(true);

        //destruction du mur
        if (realTime > life_Time)
        {
            GameObject.Instantiate(destroyEffect, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}
