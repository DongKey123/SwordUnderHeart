using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightFSMJumpAttack : FSM_State<Knight>
{

    static readonly KnightFSMJumpAttack instance = new KnightFSMJumpAttack();

    public static KnightFSMJumpAttack Instance
    {
        get
        {
            return instance;
        }
    }

    static KnightFSMJumpAttack() { }

    private KnightFSMJumpAttack() { }

    int atkNum;
    bool atkCk = false;
    bool enemyEfCk = false;

    public override void EnterState(Knight owner)
    {
        owner.m_state = KnightState.JUMPATTACK;
        atkNum = Random.Range(0, 2);
        atkCk = true;
        enemyEfCk = true;
        owner.m_AtkSound.PlayOneShot(owner.m_AtkSound.clip);
        if (atkNum == 0)
        {
            owner.m_skeletonAni.state.SetAnimation(0, "Jump_Atk", false);
        }
        else
        {
            owner.m_skeletonAni.state.SetAnimation(0, "Jump_Atk2", false);
        }
    }

    public override void UpdateState(Knight owner)
    {
        if (owner.m_skeletonAni.state.GetCurrent(0) != null)
        {
            if (owner.m_skeletonAni.state.GetCurrent(0).time > 0.2f)
            {
                if (atkCk)
                {
                    atkCk = false;
                    owner.m_AtkEffect[atkNum].GetComponent<Animator>().Play("AtkEffect");
                }
                if (owner.m_curAttackEnemy != null)
                {
                    if (enemyEfCk)
                    {
                        enemyEfCk = false;
                        GameObject effect = GameObject.Instantiate(owner.m_AttackEnemyEffect, owner.m_AttackEnemyEffectTR.position, Quaternion.identity);
                        GameObject.Destroy(effect, 1f);
                    }
                    owner.m_curAttackEnemy.Hit();
                }
            }
        }


        if (!owner.m_ExistEnemyInRange)//적이 존재하지 않을때
        {
            if (owner.m_skeletonAni.state.GetCurrent(0) == null)
            {
                owner.ChangeState(KnightFSMDrop.Instance);
            }
        }
        else //적이 존재할때
        {
            if (owner.m_skeletonAni.state.GetCurrent(0) != null)
            {
                if (owner.m_skeletonAni.state.GetCurrent(0).time > 0.99f && owner.GetComponent<Rigidbody2D>().velocity.y < 0)
                {
                    owner.m_skeletonAni.state.SetAnimation(0, "Jump_Atk2", false);
                    atkCk = true;
                    enemyEfCk = true;
                    return;
                }
            }
            else
            {

                if (owner.GetComponent<Rigidbody2D>().velocity.y < 0)
                {
                    owner.ChangeState(KnightFSMDrop.Instance);
                }
                else
                {
                    owner.m_skeletonAni.state.SetAnimation(0, "Jump_Atk2", false);
                    atkCk = true;
                    enemyEfCk = true;
                }
                return;
            }
        }

        
    }

    public override void ExitState(Knight owner)
    {

    }
}
