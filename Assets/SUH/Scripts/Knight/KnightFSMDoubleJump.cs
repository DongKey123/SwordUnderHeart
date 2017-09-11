using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightFSMDoubleJump : FSM_State<Knight> {

    static readonly KnightFSMDoubleJump instance = new KnightFSMDoubleJump();

    public static KnightFSMDoubleJump Instance
    {
        get
        {
            return instance;
        }
    }

    static KnightFSMDoubleJump() { }

    private KnightFSMDoubleJump() { }

    private bool Ck = true;

    public override void EnterState(Knight owner)
    {
        owner.m_state = KnightState.DOUBLEJUMP;
        owner.GetComponent<Rigidbody2D>().velocity = new Vector2();
        owner.GetComponent<Rigidbody2D>().AddForce(Vector2.up * owner.m_jumpPower, ForceMode2D.Impulse);
        owner.m_skeletonAni.state.SetAnimation(0, "Jump_double", false);
        owner.m_JumpCount = 0;
    }

    public override void UpdateState(Knight owner)
    {
        if (owner.m_ExistEnemyInRange)
        {
            owner.ChangeState(KnightFSMJumpAttack.Instance);
            return;
        }

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

        if (owner.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            owner.ChangeState(KnightFSMDrop.Instance);
        }
    }

    public override void ExitState(Knight owner)
    {

    }
}
