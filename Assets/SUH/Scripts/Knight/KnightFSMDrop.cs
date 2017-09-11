using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightFSMDrop : FSM_State<Knight>
{

    static readonly KnightFSMDrop instance = new KnightFSMDrop();

    public static KnightFSMDrop Instance
    {
        get
        {
            return instance;
        }
    }

    static KnightFSMDrop() { }

    private KnightFSMDrop() { }

    private bool Ck = true;

    public override void EnterState(Knight owner)
    {
        owner.m_state = KnightState.DROP;
        //owner.GetComponent<Rigidbody2D>().AddForce(Vector2.up * owner.m_jumpPower);
        owner.m_skeletonAni.state.SetAnimation(0, "Drop", false);
        Ck = true;
    }

    public override void UpdateState(Knight owner)
    {
        if (owner.m_LBtnDown)
        {
            owner.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * owner.m_moveSpeed * 0.8f;
            owner.LookLeft();
        }
        else if (owner.m_RBtnDown)
        {
            owner.transform.position += new Vector3(1, 0, 0) * Time.deltaTime * owner.m_moveSpeed * 0.8f;
            owner.LookRight();
        }

        RaycastHit2D hit = Physics2D.Linecast(owner.transform.position, owner.transform.position + Vector3.down);
        if (hit.transform != null)
        {
            if (hit.transform.tag == "Ground")
            {
                owner.m_skeletonAni.state.SetAnimation(0, "Jump_down", false);
            }
            else
            {

            }
        }

        if (owner.m_IsGround || owner.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            owner.ChangeState(KnightFSMIdle.Instance);
        }
    }

    public override void ExitState(Knight owner)
    {

    }
}
