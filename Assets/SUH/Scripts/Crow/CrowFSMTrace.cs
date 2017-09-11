using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowFSMTrace : FSM_State<Enemy>
{
    static readonly CrowFSMTrace instance = new CrowFSMTrace();

    public static CrowFSMTrace Instance
    {
        get
        {
            return instance;
        }
    }

    static CrowFSMTrace() { }
    private CrowFSMTrace() { }

    bool ck;
    // Use this for initialization
    public override void EnterState(Enemy owner)
    {
        owner.m_skeletonAni.state.SetAnimation(0, "Idle", true);
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
        Vector2 knight;
        Vector2 crow;
        knight = owner.m_Knight.transform.position;
        crow = owner.transform.position;
        if(Vector2.Distance(knight,crow) > 3)
        {
            if (ck)
            {
                owner.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * owner.m_moveSpeed;
            }
            else
            {
                owner.transform.position += new Vector3(1, 0, 0) * Time.deltaTime * owner.m_moveSpeed;
            }
        }
        else
        {
            
        }
        

        if (Vector3.Distance(owner.transform.position, owner.m_Knight.transform.position) > 15)
        {
            owner.ChangeState(CrowFSMIdle.Instance);
        }
    }

    public override void ExitState(Enemy owner)
    {
        owner.m_IsObserve = false;
    }
}
