using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Enemy {

	// Use this for initialization
	void Start () {
        m_skeletonAni = this.GetComponent<SkeletonAnimation>();
        m_stateMachine = new StateMachine<Enemy>();
        m_stateMachine.Initial_Setting(this,BoarFSMIdle.Instance);
	}
	
	// Update is called once per frame
	void Update () {
        if(m_CurHp <= 0)
        {
            ChangeState(BoarFSMDeath.Instance);
        }

        m_stateMachine.Update();
    }

    public override void ChangeState(FSM_State<Enemy> _State)
    {
        base.ChangeState(_State);
    }

    public override void Hit()
    {
        base.Hit();

        if(m_HitOn)
        {
            m_stateMachine.ChangeState(BoarFSMHit.Instance);
        }
    }
}
