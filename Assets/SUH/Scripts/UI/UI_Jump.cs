using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Jump : MonoBehaviour  , IPointerClickHandler{

    public Knight m_knight;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        if(m_knight.m_JumpCount == 2)
        {
            m_knight.ChangeState(KnightFSMJump.Instance);
        }
        else if (m_knight.m_JumpCount ==1)
        {
            m_knight.ChangeState(KnightFSMDoubleJump.Instance);
        }
    }
}
