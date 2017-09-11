using UnityEngine;
using System.Collections;

public class KnightFSMMove : FSM_State<Knight>
{
    static readonly KnightFSMMove instance = new KnightFSMMove();

    public static KnightFSMMove Instance
    {
        get
        {
            return instance;
        }
    }

    static KnightFSMMove() { }

    private KnightFSMMove() { }

    public override void EnterState(Knight owner)
    {
        owner.m_state = KnightState.MOVE;
        owner.m_skeletonAni.state.SetAnimation(0, "Move", true);
        owner.m_JumpCount = 2;
    }

    public override void UpdateState(Knight owner)
    {
        if (owner.m_ExistEnemyInRange)
        {
            owner.ChangeState(KnightFSMAttack.Instance);
            return;
        }

        if (owner.GetComponent<Rigidbody2D>().velocity.y < -2.0f)
        {
            owner.ChangeState(KnightFSMDrop.Instance);
            return;
        }

        if (owner.m_LBtnDown)
        {
            owner.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * owner.m_moveSpeed;
            owner.LookLeft();
        }
        else if (owner.m_RBtnDown)
        {
            owner.transform.position += new Vector3(1, 0, 0) * Time.deltaTime * owner.m_moveSpeed;
            owner.LookRight();
        }
        else
        {
            owner.ChangeState(KnightFSMIdle.Instance);
        }
    }

    public override void ExitState(Knight owner)
    {

    }
}
