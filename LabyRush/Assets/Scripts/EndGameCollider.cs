using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Script de détection des joueurs à l'arrivé et de la fin de partie, à placer sur la zone de fin du niveau contenant le trigger
//par Nicolas FINOUX
public class EndGameCollider : MonoBehaviour {
    //initialisation des variables
    public GUIStyle customStyle;
    public int finalTime;//temps public pour que le niveau se stoppe quand tout les joueurs sont arrivés

    private float realtime;//temps reel
    private string[] list;//list qui contiendra les phrases d'ordre de victoire
    private List<string> display;//les phrases qui seront affiché
    private string tempName;
    private int classement;//numéro de classement qui sera incrémenté à chaque fois qu'un jour atteind l'arrivée

    private bool finDuGame;//Quand tout les joueurs ont fini le niveau
    private GameObject exit_door;
    private TextMesh final_text;

    // Use this for initialization
    void Start ()
    {
        //Initialisation des différentes variable et récupération des composants utiles
        finDuGame = false;
        realtime = finalTime;
        classement = 0;
        list = new string[PhotonNetwork.room.MaxPlayers];
        final_text = GameObject.Find("final_countdown").GetComponent<TextMesh>();
        final_text.text = "";
        exit_door = GameObject.Find("Exit_Door");
        exit_door.SetActive(false);
        //On initialise les futurs phrases de victoire
        for (int i = 0; i < list.Length; i++)
        {
            list[i] = " est numéro " + (i + 1) + ".";
        }
        display = new List<string>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (display.Count >= PhotonNetwork.room.PlayerCount) finDuGame = true;
        if(finDuGame)
        {
            realtime -= Time.deltaTime;
            final_text.text = (realtime % 60).ToString("00");
            if (realtime <= 0) PhotonNetwork.LeaveRoom(true);

        }
	}
    //Pour afficher la liste des gagnants au fur et à mesure qu'il finisse le niveau
    void OnGUI()
    {
        int dep = 0;
        foreach(string s in display)
        {
            GUI.Label(new Rect((Screen.width - Screen.width / 3), 100 + dep, 100, 30), s, customStyle);
            dep += 20;
        }    
    }
    //Quand un joueur arrive dans la zone d'arrivé
    private void OnTriggerEnter(Collider c)
    {
        tempName = c.gameObject.GetComponent<PhotonView>().owner.NickName;
        if (c.gameObject.GetComponent<PhotonView>().isMine) exit_door.SetActive(true);
        c.gameObject.transform.GetChild(0).tag = "Untagged";
        list[classement] = tempName + list[classement];
        Debug.Log(list[classement]);
        display.Add(list[classement]);
        classement++;

    }
}
