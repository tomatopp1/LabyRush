using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayStockItem : MonoBehaviour
{
    public Texture bomb;
    public Texture wall;
    public Texture jump;
    public Texture empty;
    public Texture item;

    void Start () {
        item = empty;
	}
	
	void Update () {
        if (gameObject.transform.GetChild(0).tag == "bomb") item = bomb;
        else if (gameObject.transform.GetChild(0).tag == "wall") item = wall;
        else if (gameObject.transform.GetChild(0).tag == "jump") item = jump;
        else item = empty;
    }

    void OnGUI() {
        GUILayout.BeginArea(new Rect((Screen.width/2), (Screen.height-50), 50, 50));
        GUILayout.Box(item);
        GUILayout.EndArea();
    }
}
