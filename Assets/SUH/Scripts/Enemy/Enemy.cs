using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    IDLE,
    TRACE,
    ATTACK,
    DEATH
};

public class Enemy : MonoBehaviour {

    public EnemyState m_state;
    public int m_CurHp = 5;
    public float m_moveSpeed = 2.0f;
    public float m_knockBackSpeed = 1.0f;
    public float m_hitDelay = 1.2f;
    public bool m_IsObserve = false;
    public bool m_AttackOn = false;

    public bool m_LookLeft = true;
    public bool m_HitOn = true;

    public Knight m_Knight;

    [HideInInspector]
    public SkeletonAnimation m_skeletonAni;
    protected StateMachine<Enemy> m_stateMachine;

	// Use this for initialization
	void Start () {
        
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void ChangeState(FSM_State<Enemy> _State)
    {
        m_stateMachine.ChangeState(_State);
    }

    public virtual void Hit()
    {
    }

    public void LookLeft()
    {
        this.m_LookLeft = true;
        this.transform.localScale = new Vector3(1, 1, 1);
    }

    public void LookRight()
    {
        this.m_LookLeft = false;
        this.transform.localScale = new Vector3(-1, 1, 1);
    }

    public void HitDelay(float time)
    {
        StartCoroutine(Cor_HitDelay(time));
    }

    IEnumerator Cor_HitDelay(float time)
    {
        yield return new WaitForSeconds(time);
        m_HitOn = true;
        yield return null;
    }
}
