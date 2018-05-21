using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Script pour "synchroniser" le départ de tout les joueurs que l'on place sur la porte qui vérouille l'entrée au labyrinthe
//par NICOLAS FINOUX
public class synchro_start : MonoBehaviour
{
    //public
    public int timeToStart; //Variable public de la durée du compte à rebour avant que la partie commence
    public int ReadyTime; //variable du temps avant que le compte à rebour se lance quand tout les joueurs sont instantié au départ
    
    //private
    private bool ready; 
    private float realTime;//temps réel
    private int nb_player_max;//nombre de joueur max du serveur
    private TextMesh txt;//composant Text Mesh
    private DisplayTimer dt;//script du timer de la partie

	// Use this for initialization
	void Start ()
    {
        //On initialise les différentes variables et on réucupère les composants voulu
        ready = false;
        realTime = ReadyTime;
        nb_player_max = PhotonNetwork.room.MaxPlayers;//On récupère le nombre max de joueur que peut contenir la room configuré dans le lobby
        txt = GameObject.Find("Text_countdown").GetComponent<TextMesh>();//On récupère le composant qui va afficher le compte à rebour
        txt.text = "";
        dt = GameObject.Find("Scripts").GetComponent<DisplayTimer>();
        dt.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Quand le nombre max de joueur du serveur est atteind le processus se lance
        if (PhotonNetwork.room.playerCount == nb_player_max)
        {
            //on décompte le temps qui passe selon la variable associé
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
                //On affiche seulement les secondes du compte à rebour
                txt.text = (realTime % 60).ToString("00");

                if ((realTime % 60) <= 0)
                {
                    Destroy(gameObject); //On détruit la porte
                    dt.enabled = true;//on lance le timer de la partie
                }
            }
        }      
	}
}
