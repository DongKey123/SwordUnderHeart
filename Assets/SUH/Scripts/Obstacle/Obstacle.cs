using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.tag == "Knight")
        {
            col.transform.GetComponent<Knight>().ChangeState(KngihtFSMHit.Instance);
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.transform.tag == "Knight")
        {
            Knight knight = col.transform.GetComponent<Knight>();
            knight.ChangeState(KngihtFSMHit.Instance);
        }
    }
}
