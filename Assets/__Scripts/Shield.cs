using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    [Header("Set in Inspector")]
    public float rotationsPerSecond = 0.1f;

    [Header("Set Dynamically")]
    public int levelShown = 0;

    //This nonpublic variable will not appear in the Inspector
    Material mat; //a                                                                  //aThe Material field mat is NOT declared public, so it will not be visible in the inspector, and it will not be able to be accessed outside of this Shield class

	// Use this for initialization
	void Start () {
        mat = GetComponent<Renderer>().material;    //b                                                             in Start(), mat is defined as the material of the Renderer component on this GameObject (Shield in the Hierarchy). this allows you to quickly set the texture offset in the line marked //d
		
	}
	
	// Update is called once per frame
	void Update () {

        //Read the current shield level from the Hero Singleton
        int currLevel = Mathf.FloorToInt(Hero.S.shieldLevel);  //c                currLevel is set to the floor of the current Hero.S.shieldLevel float. flooring the shieldLevel ensures that the shield jumps to the new X Offset rather than show an Offset between two shield icons


        if(levelShown!= currLevel){
            levelShown = currLevel;
            //Adjust the texture offset to show different shield level
            mat.mainTextureOffset = new Vector2(0.2f * levelShown, 0); //d                             this line adjusts the X Offset of Mat_Shield to show the proper shield level


        }
        //Rotate the shield a bit every frame in a time-based way
        float rZ = -(rotationsPerSecond * Time.time * 360) % 360f;      //e                         this line and the next cause the Shield GameObject to rotate slowly about the z axis
        transform.rotation = Quaternion.Euler(0, 0, rZ); //the line that actually rotates the shield
	}
}
