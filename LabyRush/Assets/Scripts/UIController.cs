using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public GUIStyle customStyle;
    private float time = 0;

    // count time
    void Update()
    {
        time += Time.deltaTime;
    }

    // display timer
    void OnGUI()
    {
        string minutes = Mathf.Floor(time / 60).ToString("00");
        string seconds = (time % 60).ToString("00");

        GUI.Label(new Rect((Screen.width / 2), 25, 100, 30), minutes + ":" + seconds, customStyle);
    }
}
