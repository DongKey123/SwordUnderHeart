using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowAttackCollider : MonoBehaviour {

    private Crow _crow;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        _crow = this.GetComponentInParent<Crow>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Knight")
        {
            _crow.m_AttackOn = true;
            _crow.m_Knight = col.GetComponent<Knight>();
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Knight")
        {
            _crow.m_AttackOn = true;
            _crow.m_Knight = col.GetComponent<Knight>();
        }
    }

    void OnTriggerEnterExit(Collider2D col)
    {
        if (col.tag == "Knight")
        {
            _crow.m_AttackOn = false;
            _crow.m_Knight = col.GetComponent<Knight>();
        }
    }
}
