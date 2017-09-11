using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowFSMDeath : FSM_State<Enemy>
{

    static readonly CrowFSMDeath instance = new CrowFSMDeath();

    public static CrowFSMDeath Instance
    {
        get
        {
            return instance;
        }
    }

    static CrowFSMDeath() { }
    private CrowFSMDeath() { }

    public override void EnterState(Enemy owner)
    {
        Crow crow = owner.GetComponent<Crow>();
        GameObject obj = GameObject.Instantiate(crow.m_DeathEffect, owner.transform.position, Quaternion.identity);
        GameObject.Destroy(obj, 1f);

        owner.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        owner.GetComponent<BoxCollider2D>().enabled = false;
        owner.m_skeletonAni.state.SetAnimation(0, "Dead", false);
        Object.Destroy(owner.gameObject, 1.5f);
        crow.m_DeadSound.Play();
    }

    Color color = new Color(1, 1, 1, 1);

    public override void UpdateState(Enemy owner)
    {
        owner.transform.position += Vector3.down * Time.deltaTime; 
        color.a -= Time.deltaTime * 2 / 3;
        owner.m_skeletonAni.skeleton.SetColor(color);

    }

    public override void ExitState(Enemy owner)
    {

    }
}
