
using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class FPSCounter : MonoBehaviour { 
	 float updateInterval = 0.5f;
 
	private float accum = 0.0f; // FPS accumulated over the interval
	private float frames = 0f; // Frames drawn over the interval
	private float timeleft  ; // Left time for current interval
    //private ulong roleId = 9999;

	void Start()
	{
		if( !this.GetComponent<Text>() )
	    {
	        print ("FramesPerSecond needs a GUIText component!");
			this.enabled = false;
	        return;
	    }
	    timeleft = updateInterval;  
	}
 
	void Update()
	{
	    timeleft -= Time.deltaTime;
	    accum += Time.timeScale/Time.deltaTime;
	    ++frames;

//        if (GameUser.getInstance() != null && GameUser.getInstance().currentRole != null)
//        {
//            roleId = GameUser.getInstance().currentRole.roleId;
//        }
	 
	    // Interval ended - update GUI text and start new interval
	    if( timeleft <= 0.0f )
	    {
	        // display two fractional digits (f2 format)
			this.GetComponent<Text>().text = "FPS - " + (accum/frames).ToString("f2");
            //this.GetComponent<Text>().text += "\n"+"角色ID: " + roleId.ToString();
	        timeleft = updateInterval;
	        accum = 0.0f;
	        frames = 0f;
	    }
	}
}