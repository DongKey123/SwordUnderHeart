using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Camera : MonoBehaviour {

    public Transform m_Player;

    private bool _Topside = true;
    [SerializeField]
    private float _TopMinX = 0f;
    [SerializeField]
    private float _TopMaxX = 0f;
    [SerializeField]
    private float _TopMinY = 0f;
    [SerializeField]
    private float _TopMaxY = 0f;

    private bool _Bottomside = true;
    [SerializeField]
    private float _BottomMinX = 0f;
    [SerializeField]
    private float _BottomMaxX = 0f;
    [SerializeField]
    private float _BottomMinY = 0f;
    [SerializeField]
    private float _BottomMaxY = 0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(_Topside)
        {
            float x = Mathf.Clamp(m_Player.position.x, _TopMinX, _TopMaxX);
            float y = Mathf.Clamp(m_Player.position.y, _TopMinY, _TopMaxY);
            this.transform.position = new Vector3(x, y, this.transform.position.z);
        }
	}
}
