using UnityEngine;
using System.Collections;

public class CS_Screenshot : MonoBehaviour 
{   
    void Update () 
    {
        if ( Input.GetKeyUp( KeyCode.F3 ) ) 
        {
            Debug.Log("Screen captured.");
            ScreenCapture.CaptureScreenshot("./Assets/Screenshot/" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png");
        } 
    }		
}