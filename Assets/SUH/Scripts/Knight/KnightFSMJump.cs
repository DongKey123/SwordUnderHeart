using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightFSMJump : FSM_State<Knight>
{

    static readonly KnightFSMJump instance = new KnightFSMJump();

    public static KnightFSMJump Instance
    {
        get
        {
            return instance;
        }
    }

    static KnightFSMJump() { }

    private KnightFSMJump() { }

    public override void EnterState(Knight owner)
    {
        owner.GetComponent<Rigidbody2D>().AddForce(Vector2.up * owner.m_jumpPower,ForceMode2D.Impulse);
        owner.m_skeletonAni.state.SetAnimation(0, "Jump_up", false);
        owner.m_JumpCount = 1;
    }

    public override void UpdateState(Knight owner)
    {
        if (owner.m_LBtnDown)
        {
            owner.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * owner.m_moveSpeed * 0.9f;
            owner.LookLeft();
        }
        else if (owner.m_RBtnDown)
        {
            owner.transform.position += new Vector3(1, 0, 0) * Time.deltaTime * owner.m_moveSpeed * 0.9f;
            owner.LookRight();
        }
        else
        {
            
        }

        if (owner.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            owner.ChangeState(KnightFSMDrop.Instance);
        }
    }

    public override void ExitState(Knight owner)
    {

    }
}
