using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarFSMIdle : FSM_State<Enemy> {

    static readonly BoarFSMIdle instance = new BoarFSMIdle();

    public static BoarFSMIdle Instance
    {
        get
        {
            return instance;
        }
    }

    static BoarFSMIdle() { }
    private BoarFSMIdle() { }

    public override void EnterState(Enemy owner)
    {
        owner.m_skeletonAni.state.SetAnimation(0, "Idle2", true);
    }

    public override void UpdateState(Enemy owner)
    {
       if(owner.m_IsObserve)
       {
            owner.ChangeState(BoarFSMTrace.Instance);
       }
    }

    public override void ExitState(Enemy owner)
    {

    }
}
