﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemy_1 extends the Enemy class
public class Enemy_1 : Enemy {                                   //As an extension of the Enemy class, Enemy)1 inherits the public speed, fireRate, health, and score fields as well as the public pos property and the public Move() method. however, it does not inherti the private bndCheck field

    [Header("Set in Inspector: Enemy_1")]
    //# seconds for a full sine wave
    public float waveFrequency = 2;
    //sine wave width in meters
    public float waveWidth = 4;
    public float waveRotY = 45;

    private float x0; //the initial x value of pos                                
    private float birthTime;
    //private BoundsCheck bndCheck;

    //Start works well because its not used by the Enemy superclass
	// Use this for initialization
	void Start () {
        //set x0 to the initial x position of Enemy_1
        x0 = pos.x;

        birthTime = Time.time;
	}

    //override the move function of Enemy
    public override void Move()
    {
        //Because pos is a peroperty, you can't directly set pos.x
        //so get the pos as an editable Vector3

        Vector3 tempPos = pos;
        //theta adjusts based on time
        float age = Time.time - birthTime;
        float theta = Mathf.PI * 2 * age / waveFrequency;
        float sin = Mathf.Sin(theta);
        tempPos.x = x0 + waveWidth * sin;
        pos = tempPos;

        //rotate a bit about y'
        Vector3 rot = new Vector3(0, sin * waveRotY, 0);
        this.transform.rotation = Quaternion.Euler(rot);

        //base.Move() still handles the movement down in y
        base.Move();

        //print(bndCheck.isOnScreen);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}