using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow : Enemy
{
    public GameObject m_DeathEffect;
    public AudioSource m_HitSound;
    public AudioSource m_DeadSound;

	// Use this for initialization
	void Start () {
        m_skeletonAni = this.GetComponent<SkeletonAnimation>();
        m_stateMachine = new StateMachine<Enemy>();
        m_stateMachine.Initial_Setting(this, CrowFSMIdle.Instance);
	}
	
	// Update is called once per frame
	void Update () {
        if (m_CurHp <= 0)
        {
            ChangeState(CrowFSMDeath.Instance);
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

        if (m_HitOn)
        {
            m_stateMachine.ChangeState(CrowFSMHit.Instance);
        }
    }
}
