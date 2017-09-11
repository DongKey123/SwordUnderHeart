using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KngihtFSMHit : FSM_State<Knight>
{

    static readonly KngihtFSMHit instance = new KngihtFSMHit();

    public static KngihtFSMHit Instance
    {
        get
        {
            return instance;
        }
    }

    static KngihtFSMHit() { }

    private KngihtFSMHit() { }

    int attacknum = 1;

    public override void EnterState(Knight owner)
    {
        owner.m_state = KnightState.HIT;
        owner.m_CurHP--;
        owner.m_HitOn = false;
        owner.HitDelay(owner.m_HitDelay);
        owner.m_skeletonAni.state.SetAnimation(0, "Hit", false);
    }


    public override void UpdateState(Knight owner)
    {
        if (owner.m_LookLeft)
        {
            owner.transform.position += new Vector3(1, 0, 0) * Time.deltaTime * owner.m_knockbackSpeed;
        }
        else
        {
            owner.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * owner.m_knockbackSpeed;
        }

        if (owner.m_skeletonAni.state.GetCurrent(0) != null)
        {
            if (owner.m_skeletonAni.state.GetCurrent(0).time > 0.99f && owner.m_HitOn)
            {
                owner.ChangeState(KnightFSMIdle.Instance);
            }
        }
        else
        {
            if (owner.m_HitOn)
            {
                owner.ChangeState(KnightFSMIdle.Instance);
            }

        }
    }

    public override void ExitState(Knight owner)
    {
        owner.m_InvincibleTime = 1.0f;
    }
}
