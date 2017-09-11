using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Knight m_knight;
    public float m_moveSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    	
        if(m_knight.m_state == KnightState.MOVE)
        {
            if(m_knight.m_LookLeft)
            {
                this.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * m_moveSpeed;
            }
            else
            {
                this.transform.position += new Vector3(1, 0, 0) * Time.deltaTime * m_moveSpeed;
            } 
        }
	}


}
