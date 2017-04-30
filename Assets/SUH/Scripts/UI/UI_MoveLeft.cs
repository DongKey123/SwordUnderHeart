using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_MoveLeft : MonoBehaviour ,IPointerUpHandler , IPointerDownHandler{

    public Knight knight;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerDown(PointerEventData eventData)
    {
        knight.m_RBtnDown = false;
        knight.m_LBtnDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        knight.m_LBtnDown = false;
    }
}
