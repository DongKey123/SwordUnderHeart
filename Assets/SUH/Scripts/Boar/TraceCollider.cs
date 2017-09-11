using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceCollider : MonoBehaviour {

    private Boar _boar;

	// Use this for initialization
	void Start () {
        _boar = this.GetComponentInParent<Boar>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Knight")
        {
            _boar.m_IsObserve = true;
            _boar.m_Knight = col.GetComponent<Knight>();
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Knight")
        {
            _boar.m_IsObserve = true;
        }
    }

    void OnTriggerEnterExit(Collider2D col)
    {
        if (col.tag == "Knight")
        {
            _boar.m_IsObserve = false;
            _boar.m_Knight = col.GetComponent<Knight>();
        }
    }
}
