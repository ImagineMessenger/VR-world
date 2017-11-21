using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class BackAndroid : MonoBehaviour {

	public Text text;
	string arguments;



	// Use this for initialization
	void Start () {

		arguments = "";
		text.text = "pronto";

		AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
		AndroidJavaObject currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

		AndroidJavaObject intent = currentActivity.Call<AndroidJavaObject>("getIntent");

		bool hasExtra = intent.Call<bool> ("hasExtra", "arguments");

		if (hasExtra) {

			AndroidJavaObject extras = intent.Call<AndroidJavaObject> ("getExtras");
			arguments = extras.Call<string> ("getString", "arguments");
		}


	}
	
	void Update(){

			
		text.text = arguments;
	
	}
}
