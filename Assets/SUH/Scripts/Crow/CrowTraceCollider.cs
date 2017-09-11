using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowTraceCollider : MonoBehaviour {

    private Crow _crow;

	// Use this for initialization
	void Start () {
        _crow = this.GetComponentInParent<Crow>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Knight")
        {
            _crow.m_IsObserve = true;
            _crow.m_Knight = col.GetComponent<Knight>();
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Knight")
        {
            _crow.m_IsObserve = true;
            _crow.m_Knight = col.GetComponent<Knight>();
        }
    }

    void OnTriggerEnterExit(Collider2D col)
    {
        if (col.tag == "Knight")
        {
            _crow.m_IsObserve = false;
            _crow.m_Knight = col.GetComponent<Knight>();
        }
    }
}
