using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class synchro_start : MonoBehaviour
{
    public int timeToStart;
    public int ReadyTime;
    private bool ready;

    private float realTime;
    private int nb_player_max;
    private TextMesh txt;
    private DisplayTimer dt;

	// Use this for initialization
	void Start ()
    {
        ready = false;
        realTime = ReadyTime;
        nb_player_max = PhotonNetwork.room.MaxPlayers;
        txt = GameObject.Find("Text_countdown").GetComponent<TextMesh>();
        txt.text = "";
        dt = GameObject.Find("Scripts").GetComponent<DisplayTimer>();
        dt.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (PhotonNetwork.room.playerCount == nb_player_max)
        {
            realTime -= Time.deltaTime;
            if(ready != true)
            {
                if (realTime <= 0)
                {
                    ready = true;
                    realTime = timeToStart;
                }
            }
            else
            {
                txt.text = (realTime % 60).ToString("00");

                if ((realTime % 60) <= 0)
                {
                    Destroy(gameObject);
                    dt.enabled = true;
                }
            }
        }      
	}
}
