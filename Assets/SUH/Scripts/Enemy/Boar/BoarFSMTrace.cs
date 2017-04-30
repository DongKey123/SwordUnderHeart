using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarFSMTrace : FSM_State<Enemy>
{

    static readonly BoarFSMTrace instance = new BoarFSMTrace();

    public static BoarFSMTrace Instance
    {
        get
        {
            return instance;
        }
    }

    static BoarFSMTrace() { }
    private BoarFSMTrace() { }

    bool ck;

    public override void EnterState(Enemy owner)
    {
        owner.m_skeletonAni.state.SetAnimation(0, "Move", true);
        if (owner.m_Knight.transform.position.x - owner.transform.position.x < 0)
        {
            ck = true;
        }
        else
        {
            ck = false;
        }
    }
    

    public override void UpdateState(Enemy owner)
    {
        if(ck)
        {
            owner.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * owner.m_moveSpeed;
        }
        else
        {
            owner.transform.position += new Vector3(1, 0, 0) * Time.deltaTime * owner.m_moveSpeed;
        }

        if(Vector3.Distance(owner.transform.position,owner.m_Knight.transform.position) > 15)
        {
            owner.ChangeState(BoarFSMIdle.Instance);
        }
    }

    public override void ExitState(Enemy owner)
    {
        owner.m_IsObserve = false;
    }
}
