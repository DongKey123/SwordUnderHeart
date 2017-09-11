using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightFSMAttack : FSM_State<Knight> {

    static readonly KnightFSMAttack instance = new KnightFSMAttack();

    public static KnightFSMAttack Instance
    {
        get
        {
            return instance;
        }
    }

    static KnightFSMAttack() { }

    private KnightFSMAttack() { }

    int attacknum = 1;
    bool atkCk = false;
    bool enemyEfCk = false;

    public override void EnterState(Knight owner)
    {
        Debug.Log("Attack");
        owner.m_state = KnightState.ATTACK;
        attacknum = Random.Range(1, 3);
        
        owner.m_skeletonAni.state.SetAnimation(0, "Atk" + attacknum, false);
        atkCk = true;
        enemyEfCk = true;
        owner.m_AtkSound.PlayOneShot(owner.m_AtkSound.clip);
        //owner.m_AtkEffect.GetComponent<Animator>().Play("AtkEffect");
    }


    public override void UpdateState(Knight owner)
    {
        if(owner.m_skeletonAni.state.GetCurrent(0) != null)
        {
            if (owner.m_skeletonAni.state.GetCurrent(0).time > 0.35f)
            {
                if(atkCk)
                {
                    atkCk = false;
                    owner.m_AtkEffect[attacknum - 1].GetComponent<Animator>().Play("AtkEffect");
                }
                if (owner.m_curAttackEnemy != null)
                {
                    if(enemyEfCk)
                    {
                        enemyEfCk = false;
                        GameObject effect = GameObject.Instantiate(owner.m_AttackEnemyEffect, owner.m_AttackEnemyEffectTR.position, Quaternion.identity);
                        GameObject.Destroy(effect, 1f);
                    }
                    owner.m_curAttackEnemy.Hit();
                }
            }
        }
        

        if(!owner.m_ExistEnemyInRange)//적이 존재하지 않을때
        {
            if(owner.m_skeletonAni.state.GetCurrent(0) == null)
            {
                if (owner.m_LBtnDown) //왼쪽을 눌렀으면
                {
                    owner.LookRight();
                    owner.ChangeState(KnightFSMMove.Instance);
                }
                else if (owner.m_RBtnDown) //오른쪽을 눌렀으면
                {
                    owner.LookLeft();
                    owner.ChangeState(KnightFSMMove.Instance);
                }
                else
                {
                    owner.ChangeState(KnightFSMIdle.Instance);
                }
            }
        }
        else //적이 존재할때
        {
            if(owner.m_skeletonAni.state.GetCurrent(0) != null)
            {
                if (owner.m_skeletonAni.state.GetCurrent(0).time > 0.99f)
                {
                    attacknum = Random.Range(1, 3);
                    owner.m_skeletonAni.state.SetAnimation(0, "Atk" + attacknum, false);
                    atkCk = true;
                    enemyEfCk = true;
                    owner.m_AtkSound.PlayOneShot(owner.m_AtkSound.clip);
                    return;
                }
            }
            else
            {
                attacknum = Random.Range(1, 3);
                owner.m_skeletonAni.state.SetAnimation(0, "Atk" + attacknum, false);
                atkCk = true;
                enemyEfCk = true;
                owner.m_AtkSound.PlayOneShot(owner.m_AtkSound.clip);
                return;
            }
            

            if(owner.m_LookLeft)//왼쪽을 바라볼때
            {
                if(owner.m_skeletonAni.state.GetCurrent(0).time < 0.3 && owner.m_RBtnDown) //오른쪽을 눌렀으면
                {
                    owner.LookRight();
                    owner.ChangeState(KnightFSMMove.Instance);
                }
            }
            else
            {
                if (owner.m_skeletonAni.state.GetCurrent(0).time < 0.3 && owner.m_LBtnDown)
                {
                    owner.LookLeft();
                    owner.ChangeState(KnightFSMMove.Instance);
                }
            }
        }
    }

    public override void ExitState(Knight owner)
    {

    }
}
