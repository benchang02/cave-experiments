using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OSCEventListener : MonoBehaviour {
	
	PointerControl pointerRoot;
	ViewUpdate projectionRoot;

	private static OSCEventListener osc;
	public static OSCEventListener OSC
	{
		get
		{
			if(osc == null)
				osc = GameObject.FindObjectOfType<OSCEventListener>();
			return osc;
		}
	}

	// Use this for initialization
	void Start () 
	{

		OSCHandler.Instance.Init(); //init OSC
		projectionRoot = GameObject.Find("ProjectionRoot").GetComponent<ViewUpdate>();
		pointerRoot = GameObject.Find("PointerRoot").GetComponent<PointerControl>();
		DontDestroyOnLoad(gameObject);
	}

	public void ResetReferences()
	{
		projectionRoot = GameObject.Find("ProjectionRoot").GetComponent<ViewUpdate>();
		pointerRoot = GameObject.Find("PointerRoot").GetComponent<PointerControl>();
	}

	// Update is called once per frame
	void Update () {

		if (projectionRoot.whichCameraToViewFrom == ViewUpdate.viewType.center){
			
			OSCHandler.Instance.UpdateLogs();

			List<UnityOSC.OSCPacket> packets=OSCHandler.Instance.Servers["TrackerData"].packets;

			foreach (UnityOSC.OSCPacket p in packets)
			{
				Debug.Log(p.Address + " " + p.Data[0].GetType());
				float f = (float) p.Data[0];

				if (p.Address=="/visor")
				{
					projectionRoot.UpdateTrackerPosition((float)p.Data[0],(float)p.Data[2],(float)p.Data[1]);
				}
				else if (p.Address=="/wand")
				{
					if (pointerRoot)
					{
						pointerRoot.UpdatePointer((float)p.Data[0],(float)p.Data[2],(float)p.Data[1],(float)p.Data[4],(float)p.Data[3],(float)p.Data[5]);
					}
				}

			}



			//OSCHandler.Instance.UpdateLogs();
			/*
			List<string> server_messages = OSCHandler.Instance.Servers["TrackerData"].log;		
			foreach (string msg in server_messages){
				//Debug.Log (msg);
				//parse message and update tracker position
				string[] words = msg.Split(' ');
				// format:
				// date	time	
				
				//convert Vicon coordinates to Unity coordinates
				projectionRoot.UpdateTrackerPosition(float.Parse(words[5]),float.Parse(words[7]), float.Parse(words[6]));

				//14, 15, 16 = H, P, R
				// rotate X = pitch
				// rotate Y = heading
				// rotate Z = roll
				if (pointerRoot)
					pointerRoot.UpdatePointer (float.Parse(words[11]), float.Parse(words[13]), float.Parse(words[12]), float.Parse(words[15]), float.Parse(words[14]), float.Parse(words[16]));
			}
			*/
			
			
		}
	}
}
 