using UnityEngine;
using System.Collections;

public class KnightFSMIdle : FSM_State<Knight> {

    static readonly KnightFSMIdle instance = new KnightFSMIdle();

    public static KnightFSMIdle Instance
    {
        get
        {
            return instance;
        }
    }

    static KnightFSMIdle() { }

    private KnightFSMIdle() { }

    public override void EnterState(Knight owner)
    {
        owner.m_skeletonAni.state.ClearTrack(0);
        owner.m_skeletonAni.state.SetAnimation(0, "Idle", true);
        //owner.m_skeletonAni.state.End += State_End;
        owner.m_JumpCount = 2;
    }

    public override void UpdateState(Knight owner)
    {
        if (owner.m_LBtnDown || owner.m_RBtnDown)
        {
            owner.ChangeState(KnightFSMMove.Instance);
        }

        if (owner.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            owner.ChangeState(KnightFSMDrop.Instance);
        }

        if (owner.m_ExistEnemyInRange)
        {
            owner.ChangeState(KnightFSMAttack.Instance);
        }


    }

    public override void ExitState(Knight owner)
    {

    }

    //private void State_End(Spine.AnimationState state, int trackIndex)
    //{
    //    //Debug.Log(state + "  End");
    //    throw new System.NotImplementedException();
    //}
}
