using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour {

    private Knight _knight;

	// Use this for initialization
	void Start () {
        _knight = this.GetComponentInParent<Knight>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Enemy")
        {
            _knight.m_ExistEnemyInRange = true;
            _knight.m_curAttackEnemy = col.GetComponent<Enemy>();
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            _knight.m_ExistEnemyInRange = true;
            _knight.m_curAttackEnemy = col.GetComponent<Enemy>();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            _knight.m_ExistEnemyInRange = false;
            if (col.GetComponent<Enemy>() == _knight.m_curAttackEnemy)
            {
                _knight.m_curAttackEnemy = null;
            }
        }
    }
}
