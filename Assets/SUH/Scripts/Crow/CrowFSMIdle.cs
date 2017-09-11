using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowFSMIdle : FSM_State<Enemy> {

    static readonly CrowFSMIdle instance = new CrowFSMIdle();

    public static CrowFSMIdle Instance
    {
        get
        {
            return instance;
        }
    }

    static CrowFSMIdle() { }
    private CrowFSMIdle() { }

    public override void EnterState(Enemy owner)
    {
        owner.m_skeletonAni.state.SetAnimation(0, "Idle", true);
    }

    public override void UpdateState(Enemy owner)
    {
        if (owner.m_IsObserve)
        {
            owner.ChangeState(CrowFSMTrace.Instance);
        }
    }

    public override void ExitState(Enemy owner)
    {

    }
}
