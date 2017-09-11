using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1FSMDeath : FSM_State<Enemy>
{

    static readonly Boss1FSMDeath instance = new Boss1FSMDeath();

    public static Boss1FSMDeath Instance
    {
        get
        {
            return instance;
        }
    }

    static Boss1FSMDeath() { }
    private Boss1FSMDeath() { }

    public override void EnterState(Enemy owner)
    {
        owner.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        owner.GetComponent<BoxCollider2D>().enabled = false;
        owner.m_skeletonAni.state.SetAnimation(0, "Dead", false);
        Object.Destroy(owner.gameObject, 1.5f);
    }

    Color color = new Color(1, 1, 1, 1);

    public override void UpdateState(Enemy owner)
    {
        color.a -= Time.deltaTime * 2 / 3;
        owner.m_skeletonAni.skeleton.SetColor(color);

    }

    public override void ExitState(Enemy owner)
    {

    }
}
