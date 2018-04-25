using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class Start : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if (CrossPlatformInputManager.GetButton("Start"))     //this causes the ship to fire every time the space bar is pressed
        {
            SceneManager.LoadScene("Asset Play");
        }
    }
}
