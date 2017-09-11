using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1FSMIdle : FSM_State<Enemy>
{

    static readonly Boss1FSMIdle instance = new Boss1FSMIdle();

    public static Boss1FSMIdle Instance
    {
        get
        {
            return instance;
        }
    }

    static Boss1FSMIdle() { }
    private Boss1FSMIdle() { }

    public override void EnterState(Enemy owner)
    {
        owner.m_skeletonAni.state.SetAnimation(0, "Idle", true);
    }

    public override void UpdateState(Enemy owner)
    {
        if (owner.m_IsObserve)
        {
            owner.ChangeState(Boss1FSMTrace.Instance);
        }
    }

    public override void ExitState(Enemy owner)
    {

    }
}
