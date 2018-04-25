using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Hero : MonoBehaviour {

    static public Hero S; //Singleton                                                      //a The singleton for the hero class. the code in awake() shows an error if you try to set Hero.S after it has already been set.

    [Header("Set in Inspector")]
    // These fields control the movement of the ship

    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 2f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;


    [SerializeField]
    private float _shieldLevel = 1;


    [Header("Set Dynamically")]

    //public float shieldLevel = 1;
    //This variable holds a reference to the last triggering GameObject
    private GameObject lastTriggerGo = null; 


    private void Awake()
    {
        if(S == null)
        {
            S = this; //set the singleton
        }
        else
        {
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.S!"); //a
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //Pull in information from the Input class
        float xAxis = CrossPlatformInputManager.GetAxis("Horizontal");
        float yAxis = CrossPlatformInputManager.GetAxis("Vertical"); //b                               These two lines use Unity's input class to pull information from the Unity InputManager 


        //Change transform.position based on the axes
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;


        //Rotate the ship to make it feel more dynamic //c
        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);       //c      the transform.rotation line below this comment is used to give the ship a bit of rotation based on the speed at which it is moving, which can make the ship feel more reactive and juicy
		
        //allow the ship to fire
        if (CrossPlatformInputManager.GetButton("Shoot"))     //this causes the ship to fire every time the space bar is pressed
        {
            TempFire();   
        }
	}

    void TempFire()   // This method is named TempFire because you will be replacing it in the next chapter
    {
        GameObject projGO = Instantiate<GameObject>(projectilePrefab);
        projGO.transform.position = transform.position;
        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
        rigidB.velocity = Vector3.up * projectileSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        print("Triggered: " + go.name);



        if(go == lastTriggerGo)
        {
            return;
        }
        lastTriggerGo = go;

        if(go.tag == "Enemy")  //if the shield was triggered by an ememy
        {
            shieldLevel--;   //Decrease the level of the shield by 1
            GetComponent<AudioSource>().Play();
            Destroy(go);   //and destroy the enemy



        }
        else
        {
            print("Triggered by non-Enemy: " + go.name);
        }


       
    }

    public float shieldLevel
    {
        get
        {
            return(_shieldLevel);
        }
        set
        {
            _shieldLevel = Mathf.Min(value, 4);
            //if the shield is going to be set to less than zero
            if (value < 0)
            {
                GetComponent<AudioSource>().Play();
                Destroy(this.gameObject);
                //Tell main.s to restart the game after a delay
                Main.S.DelayedRestart(gameRestartDelay);
            }
        }
        
    }
}
