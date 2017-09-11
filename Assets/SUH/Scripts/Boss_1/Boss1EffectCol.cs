using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1EffectCol : MonoBehaviour {

    public bool m_OnTriggerPlayer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Knight")
        {
            Debug.Log("Knight Triiger Enter");
            m_OnTriggerPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Knight")
        {
            Debug.Log("Knight Triiger Exit");
            m_OnTriggerPlayer = true;
        }
    }
}
