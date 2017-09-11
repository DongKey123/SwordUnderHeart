using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Rock : MonoBehaviour {

    public bool m_LeftTurn;
    public float m_TurnSpeed =3f;

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 3f);
	}
	
	// Update is called once per frame
	void Update () {
		if(m_LeftTurn)
        {
            transform.position += new Vector3(-1, 0, 0) * m_TurnSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, m_TurnSpeed);
        }
        else
        {
            transform.position += new Vector3(1, 0, 0) * m_TurnSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, m_TurnSpeed);
        }
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.tag == "Knight")
        {
            Debug.Log("Knight");
            this.GetComponent<Collider2D>().isTrigger = true;
            this.GetComponent<Rigidbody2D>().gravityScale = 0f;

            col.transform.GetComponent<Knight>().ChangeState(KngihtFSMHit.Instance);

            Destroy(this.gameObject);
        }
    }
}
