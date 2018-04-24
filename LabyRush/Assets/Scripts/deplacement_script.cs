using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deplacement_script : MonoBehaviour {

    public float speed;
    public int seconde;
    public int nbBoost;
    public string dep;

    private int boost;
    private float timeBoost;
    private Rigidbody rb;
    private bool pause;
    private PhotonView Pv;
    private deplacement_script ds;
    private void Start()
    {
        Pv = gameObject.GetComponent<PhotonView>();
        ds = gameObject.GetComponent<deplacement_script>();
        boost = 1;
        timeBoost = 0;
        pause = true;
        //rb = GetComponent<Rigidbody>();
        ds.enabled = Pv.isMine; 

    }

    void FixedUpdate()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");
        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        //rb.AddForce(movement * speed);

        if (pause != true) timeBoost += Time.deltaTime;
        if (timeBoost > seconde & boost <= nbBoost)
        {
            this.boost++;
            this.timeBoost = 0;
        }

        if (Input.anyKey) pause = false;
        //tourner à gauche
        //if (Input.GetKey(KeyCode.LeftArrow)) dep = "left";
        if (Input.GetButton("Horizontal") && Input.GetAxis("Horizontal") < 0) dep = "left";
        if (dep == "left") transform.Translate(new Vector3(-speed*boost, 0, 0)*Time.deltaTime);

        //tourner à droite
        //if (Input.GetKey(KeyCode.RightArrow)) dep = "right";
        if (Input.GetButton("Horizontal") && Input.GetAxis("Horizontal") > 0) dep = "right";
        if (dep == "right") transform.Translate(new Vector3(speed * boost, 0, 0) * Time.deltaTime);
        //allez en haut
       // if (Input.GetKey(KeyCode.UpArrow)) dep = "up";
        if (Input.GetButton("Vertical") && Input.GetAxis("Vertical") > 0) dep = "up";
        if (dep == "up") transform.Translate(new Vector3(0, 0, speed * boost) * Time.deltaTime);
        //allez en bas
        //if (Input.GetKey(KeyCode.DownArrow)) dep = "down";
        if (Input.GetButton("Vertical") && Input.GetAxis("Vertical") < 0) dep = "down";
        if (dep == "down") transform.Translate(new Vector3(0, 0, -speed * boost) * Time.deltaTime);
        //stopper
        if (Input.GetKey(KeyCode.Space)) dep = "stop" ;
        if (dep == "stop") transform.Translate(new Vector3(0, 0, 0));

        
    }

    public void OnCollisionEnter(Collision collision)
    {
        this.boost = 1;
        this.pause = true;
        transform.Translate(new Vector3(0, 0, 0));
        this.timeBoost = 0;
    }

}
