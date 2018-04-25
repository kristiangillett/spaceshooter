using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [Header("Set in Inspector: Enemy")]
    public float speed = 10f; //the speed in m/s
    public float fireRate = 0.3f; //Seconds/shot (Unused)
    public float health = 10; 
    public int score = 100; //Points earned for destroying this

    protected BoundsCheck bndCheck;


    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }


    //this is a Property: a method that acts like a field
    public Vector3 pos    //a                               As was discussed in chapter 26, "Classes," a property is a function masquerading as a field. this means that you can get and set the value of pos as if it were a class variable of enemyS
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Move();

        if(bndCheck != null && bndCheck.offDown)
        {
          //we're of to the bottom so destroy the object
          Destroy(gameObject);
        }
		
	}
    public virtual void Move()  //b                                The Move() method gets the current position of this Enemy_0, moves it in the downward y direction, and assigns it back to pos(setting the position of the GameObject).

    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }
    void OnCollisionEnter(Collision coll)
    {
        GameObject otherGO = coll.gameObject;
        if(otherGO.tag == "ProjetileHero")                                 //get the GameObject of the collider that was hit in the collision
                                                                                
        {
            GetComponent<AudioSource>().Play();
            
            Destroy(otherGO);  //Destroy the Projectile
            Destroy(gameObject); //Destroy this enemy gameobject
        }
        else
        {
            print("Enemy hit by nonProjectile Hero: " + otherGO.name);

        }
    }
}
