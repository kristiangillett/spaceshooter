 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {
    static public Main S;

    [Header("Set in Inspector")]

    public GameObject[] prefabEnemies; //Array of Enemy prefab
    public float enemySpawnPerSecond = 0.5f;
    public float enemyDefaultPadding = 1.5f;


    private BoundsCheck bndCheck;

    void Awake()
    {
        S = this;
        //Set bndCheck to reference the BoundsCheck component on this GameObject
        bndCheck = GetComponent<BoundsCheck>();
        //Invoke SpawnEnemy() once (in 2 seconds, based on default values)
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond); //a             this Invoke() function calls the SpawnEnemy() method in 1/0.5 seconds (ie 2 seconds) based on default values.
    }

    public void SpawnEnemy()
    {
        //pick a random enemy prefa to instansiate
        int ndx = Random.Range(0, prefabEnemies.Length);          // b                       based on the length of the array prefabEnemies, ths chooses a random number between 0 and one less than prefabEnemies.Length, so if four prefabs are in the prefabEnemies array, it will return 0,1,2, or 3. the int version of Random.Range() will never return a number as hgh as the max (i.e, second) integer passed in. the float version is able to return the max number.
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]); //c    the random ndx generated is used to select a GameObject prefab from prefabEnemies

        //position the Enemy above the screen with a rando x position
        float enemyPadding = enemyDefaultPadding;   // d                              the enemyPadding is initially set to the enemyDefaultPadding set in the inspector.
        if(go.GetComponent<BoundsCheck>() != null)           //e               However, if the selected enemy prefab has a BoundsCheck component, you instead read the radius from that. the absolute value of the radius is taken because seomtimes the radius is set to a negative value so that the GameObject must be entirely ff screen before registering as IsOnScreen = false, as is the case for Enemy_0

        {
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }

        //Set the initial position for the spawned enemy                          //f                This section of the code sets an initial position for the enemy hat was instantiated. it uses the BoundsCheck on this _MainCamera GameObject to get the camWidth and camHeight and chooses an x position where the spawned enemy is entirely on screen horizontally. it then chooses a y position where the enemy is just above the screen.


        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidth + enemyPadding;
        float xMax = bndCheck.camWidth - enemyPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyPadding;
        go.transform.position = pos;

        //invoke spawnenemy again
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
      

    }
    public void DelayedRestart(float delay)
    {
        //invoking the Restart method in delay seconds
        Invoke("Restart", delay);
    }
    public void Restart()
    {
        //Reload_Scene_0 to restart the game
        SceneManager.LoadScene("Asset Play");
    }


	// Use this for initialization
	void Start () {


        
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
