using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    [Header("Set in Inspector: Enemy")]
    public float speed = 10f;
    public float fireRate = 0.3f;
    public float health = 10;
    static public int score = 0;
    protected BoundsCheck bndCheck;
	[Header("Set Dynamically")]

	public Text scoreGT;

	void Start()
	{
		GameObject scoreGO = GameObject.Find ("ScoreCounter");
		scoreGT = scoreGO.GetComponent<Text>();
		//scoreGT.text =  "0";
		//scoreGT.text = score.ToString ();
	}

    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }
    public Vector3 pos
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


	
	
	// Update is called once per frame
	void Update () {
        Move();
        if (bndCheck != null && bndCheck.offDown)
        {
            if (pos.y < bndCheck.camHeight - bndCheck.radius)
            {
                Destroy(gameObject);
				//scoreGT.text = score.ToString ();
            }
        }
	}

    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    void OnCollisionEnter(Collision coll)
    {
        GameObject otherGO = coll.gameObject;
		if (otherGO.tag == "ProjectileHero") {
			Destroy (otherGO);
			Destroy (gameObject);
			//int score = int.Parse (scoreGT.text);
			score += 100;
			scoreGT.text = "Score: "+score.ToString ();

			//scoreGT.text = score.ToString ();

		
		} 

		if (score > HighScore.score); {
			
			HighScore.score = score;
		}
	}
}
