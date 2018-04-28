using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayStockItem : MonoBehaviour
{
    public Texture bomb;
    public Texture wall;
    public Texture jump;
    public Texture empty;

    private Texture item;

    // start with an empty stock
    void Start () {
        item = empty;
	}
	
    // handle picks up and drops down item
	void Update () {
        //missing texture
        if (!bomb || !wall || !jump || !empty) Debug.LogError("Missing texture, assign a texture in the inspector");

        if (gameObject.transform.GetChild(0).tag == "bomb") item = bomb;
        else if (gameObject.transform.GetChild(0).tag == "wall") item = wall;
        else if (gameObject.transform.GetChild(0).tag == "jump") item = jump;
        else item = empty;
    }

    // display in bag item
    void OnGUI() {
        GUILayout.BeginArea(new Rect((Screen.width/2), (Screen.height-50), 50, 50));
        GUILayout.Box(item);
        GUILayout.EndArea();
    }
}
