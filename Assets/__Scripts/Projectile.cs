using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    private BoundsCheck bndCheck;


    void Awake ()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (bndCheck.offUp)     //If the projectile goes off the top of the screen, destory it

        {
            Destroy(gameObject);
        }
		
	}
}
