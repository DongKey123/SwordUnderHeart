﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarFSMDeath : FSM_State<Enemy> {

    static readonly BoarFSMDeath instance = new BoarFSMDeath();

    public static BoarFSMDeath Instance
    {
        get
        {
            return instance;
        }
    }

    static BoarFSMDeath() { }
    private BoarFSMDeath() { }

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
