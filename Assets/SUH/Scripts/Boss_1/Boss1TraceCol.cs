using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1TraceCol : MonoBehaviour {

    private Stage1Boss _boss;

    // Use this for initialization
    void Start () {
        _boss = this.GetComponentInParent<Stage1Boss>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Knight")
        {
            _boss.m_IsObserve = true;
            _boss.m_Knight = col.GetComponent<Knight>();
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Knight")
        {
            _boss.m_IsObserve = true;
            _boss.m_Knight = col.GetComponent<Knight>();
        }
    }

    void OnTriggerEnterExit(Collider2D col)
    {
        if (col.tag == "Knight")
        {
            _boss.m_IsObserve = false;
            _boss.m_Knight = col.GetComponent<Knight>();
        }
    }
}
