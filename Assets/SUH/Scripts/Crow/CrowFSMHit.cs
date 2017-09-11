using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowFSMHit : FSM_State<Enemy>
{

    static readonly CrowFSMHit instance = new CrowFSMHit();

    public static CrowFSMHit Instance
    {
        get
        {
            return instance;
        }
    }

    static CrowFSMHit() { }
    private CrowFSMHit() { }

    public override void EnterState(Enemy owner)
    {
        owner.m_CurHp--;
        owner.m_HitOn = false;
        owner.m_skeletonAni.state.SetAnimation(0, "Hit", false);
        owner.HitDelay(owner.m_hitDelay);
        owner.GetComponent<Crow>().m_HitSound.Play();
    }

    public override void UpdateState(Enemy owner)
    {
        if (owner.m_LookLeft)
        {
            owner.transform.position += new Vector3(1, 0, 0) * Time.deltaTime * owner.m_knockBackSpeed;
        }
        else
        {
            owner.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * owner.m_knockBackSpeed;
        }

        if (owner.m_skeletonAni.state.GetCurrent(0) != null)
        {
            if (owner.m_skeletonAni.state.GetCurrent(0).time > 0.99f && owner.m_HitOn)
            {
                owner.ChangeState(CrowFSMIdle.Instance);
            }
        }
        else
        {
            if (owner.m_HitOn)
            {
                owner.ChangeState(CrowFSMIdle.Instance);
            }

        }

    }

    public override void ExitState(Enemy owner)
    {

    }
}
