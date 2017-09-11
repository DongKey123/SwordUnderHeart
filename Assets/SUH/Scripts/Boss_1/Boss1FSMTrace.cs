using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1FSMTrace : FSM_State<Enemy>
{

    static readonly Boss1FSMTrace instance = new Boss1FSMTrace();

    public static Boss1FSMTrace Instance
    {
        get
        {
            return instance;
        }
    }

    static Boss1FSMTrace() { }
    private Boss1FSMTrace() { }

    bool ck;
    float time;

    public override void EnterState(Enemy owner)
    {
        Debug.Log("Boss Move");
        time = 0;
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
        if (ck)
        {
            owner.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * owner.m_moveSpeed;
        }
        else
        {
            owner.transform.position += new Vector3(1, 0, 0) * Time.deltaTime * owner.m_moveSpeed;
        }

        time += Time.deltaTime;
        if(time > 0.7f)
        {
            owner.ChangeState(Boss1FSMAttack.Instance);
        }
    }

    public override void ExitState(Enemy owner)
    {
        owner.m_IsObserve = false;
    }
}
