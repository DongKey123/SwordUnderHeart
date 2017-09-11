using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Boss : Enemy {

    public GameObject m_prefabRock;
    public Transform m_rockPos;

    public GameObject m_effect;

    public float m_JumpPower = 3.0f;

    // Use this for initialization
    void Start () {
        m_skeletonAni = this.GetComponent<SkeletonAnimation>();
        m_stateMachine = new StateMachine<Enemy>();
        m_stateMachine.Initial_Setting(this, Boss1FSMIdle.Instance);
	}
	
	// Update is called once per frame
	void Update () {
        if(this.m_CurHp <= 0)
        {
            ChangeState(Boss1FSMDeath.Instance);
        }

        m_stateMachine.Update();
    }

    public override void ChangeState(FSM_State<Enemy> _State)
    {
        m_stateMachine.ChangeState(_State);
    }

    public override void Hit()
    {
        base.Hit();

        if (m_HitOn)
        {
            
            m_stateMachine.ChangeState(Boss1FSMHit.Instance);
        }
    }
}
