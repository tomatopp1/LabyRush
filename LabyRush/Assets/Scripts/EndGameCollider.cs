using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCollider : MonoBehaviour {
    //initialisation des variables
    public GUIStyle customStyle;
    public int finalTime;

    private float realtime;
    private string[] list;
    private List<string> display;
    private string tempName;
    private int classement;

    private bool finDuGame;
    private GameObject exit_door;
    private TextMesh final_text;

    // Use this for initialization
    void Start ()
    {
        finDuGame = false;
        realtime = finalTime;
        classement = 0;
        list = new string[PhotonNetwork.room.MaxPlayers];
        final_text = GameObject.Find("final_countdown").GetComponent<TextMesh>();
        final_text.text = "";
        exit_door = GameObject.Find("Exit_Door");
        exit_door.SetActive(false);

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

    void OnGUI()
    {
        int dep = 0;
        foreach(string s in display)
        {
            GUI.Label(new Rect((Screen.width - Screen.width / 3), 100 + dep, 100, 30), s, customStyle);
            dep += 20;
        }    
    }

    private void OnTriggerEnter(Collider c)
    {
        tempName = c.gameObject.GetComponent<PhotonView>().owner.NickName;
        if (c.gameObject.GetComponent<PhotonView>().isMine) exit_door.SetActive(true);
        list[classement] = tempName + list[classement];
        Debug.Log(list[classement]);
        display.Add(list[classement]);
        classement++;

    }
}
