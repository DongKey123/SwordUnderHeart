using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1FSMAttack : FSM_State<Enemy>
{

    static readonly Boss1FSMAttack instance = new Boss1FSMAttack();

    public static Boss1FSMAttack Instance
    {
        get
        {
            return instance;
        }
    }

    static Boss1FSMAttack() { }
    private Boss1FSMAttack() { }

    bool ck;
    int aniNum;
    float time;

    public override void EnterState(Enemy owner)
    {
        time = 0;
        aniNum = Random.Range(1, 3);
        rockCk = false;
        clampCk = false;

        Stage1Boss boss = owner.GetComponent<Stage1Boss>();

        if (owner.m_Knight.transform.position.x - owner.transform.position.x < 0)
        {
            ck = true;
        }
        else
        {
            ck = false;
        }

        switch (aniNum)
        {
            case 1: //점프공격
                {
                    Debug.Log(boss.m_JumpPower);
                    owner.GetComponent<Rigidbody2D>().AddForce(Vector2.up * boss.m_JumpPower, ForceMode2D.Impulse);
                }
                break;
            case 2: //돌굴리기
                {

                }
                break;
        }
        owner.m_skeletonAni.state.SetAnimation(0, "Atk"+ aniNum, false);
    }

    public bool clampCk = false;
    public bool rockCk = false;

    public override void UpdateState(Enemy owner)
    {
        Stage1Boss boss = owner.GetComponent<Stage1Boss>();

        if (boss.m_skeletonAni.state.GetCurrent(0) != null) //애니메이션 null이 아닐때
        {
            if (owner.m_skeletonAni.state.GetCurrent(0).time > 0.99f)
            {
                //애니메이션 종료
            }

            switch (aniNum)
            {
                case 1: //점프공격
                    {
                        if (ck)
                        {
                            owner.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * owner.m_moveSpeed * 0.1f;
                        }
                        else
                        {
                            owner.transform.position += new Vector3(1, 0, 0) * Time.deltaTime * owner.m_moveSpeed * 0.1f;
                        }

                        if (owner.m_skeletonAni.state.GetCurrent(0).time > 0.92f)
                        {                           
                            boss.m_effect.GetComponent<Animator>().Play("boss_1_effect");
                            if(boss.m_effect.GetComponent<Boss1EffectCol>().m_OnTriggerPlayer && !clampCk)
                            {
                                owner.m_Knight.ChangeState(KngihtFSMHit.Instance);
                                clampCk = true;
                            }
                            else
                            {
                                Debug.Log("Boss1 not Knight Attack");
                            }
                        }
                    }
                    break;
                case 2: //돌굴리기
                    {
                        if (owner.m_skeletonAni.state.GetCurrent(0).time > 0.8f && !rockCk)
                        {
                            CreateRock(boss);
                            rockCk = true;
                        }
                    }
                    break;
            }
        }
        else //애니메이션이 진행줄일때
        {
            time += Time.deltaTime;
            if (time > 2f)
            {
                owner.ChangeState(Boss1FSMTrace.Instance);
            }
        }
    }

    public override void ExitState(Enemy owner)
    {
        owner.m_IsObserve = false;
    }

    void CreateRock(Stage1Boss boss)
    {
        GameObject obj = GameObject.Instantiate(boss.m_prefabRock);
        Boss1Rock rock = obj.GetComponent<Boss1Rock>();

        rock.transform.position = boss.m_rockPos.position;
        rock.m_LeftTurn = boss.m_LookLeft;
    }
}
