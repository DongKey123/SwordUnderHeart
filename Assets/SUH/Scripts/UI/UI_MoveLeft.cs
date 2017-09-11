using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_MoveLeft : MonoBehaviour ,IPointerUpHandler , IPointerDownHandler{

    public Knight m_knight;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!m_knight.m_Interactive)
        {
            m_knight.m_LBtnDown = false;
            return;
        }

        m_knight.m_RBtnDown = false;
        m_knight.m_LBtnDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        m_knight.m_LBtnDown = false;
    }
}
