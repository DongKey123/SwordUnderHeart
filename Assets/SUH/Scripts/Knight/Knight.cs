using UnityEngine;
using System.Collections;

public enum KnightState
{
    IDLE,
    MOVE,
    JUMP,
    DOUBLEJUMP,
    DROP,
    ATTACK,
    JUMPATTACK,
    SLIDING,
    HIT,
    DEATH
}
public class Knight : MonoBehaviour {

    public KnightState m_state;

    public int m_JumpCount = 2;

    public int m_CurHP = 5;
    public bool m_HitOn = true;
    public float m_HitDelay = 1.2f;
    public float m_InvincibleTime = 0.5f;

    public bool m_LookLeft = true;
    public bool m_IsGround;

    public bool m_LBtnDown = false;
    public bool m_RBtnDown = false;

    public float m_knockbackSpeed = 1.0f;
    public float m_moveSpeed = 3.0f;
    public float m_jumpPower = 3.0f;

    public bool m_ExistEnemyInRange = false;
    public bool m_Interactive = true;

    public AudioSource m_AtkSound;
    public GameObject[] m_AtkEffect;

    public GameObject m_AttackEnemyEffect;
    public Transform m_AttackEnemyEffectTR;

    [HideInInspector]
    public Enemy m_curAttackEnemy = null;

    [HideInInspector]
    public SkeletonAnimation m_skeletonAni;
    [HideInInspector]
    public Animator m_Animator;

    public Camera m_cam;

    protected StateMachine<Knight> m_stateMachine;


	// Use this for initialization
	void Start () {
        m_skeletonAni = this.GetComponent<SkeletonAnimation>();
        m_stateMachine = new StateMachine<Knight>();
        m_stateMachine.Initial_Setting(this, KnightFSMIdle.Instance);
        
    }


    // Update is called once per frame
    void Update () {

        RaycastHit2D hit = Physics2D.Linecast(this.transform.position, this.transform.position + Vector3.down*0.2f);
        if(hit.transform != null)
        {
            if (hit.transform.tag == "Ground")
            {
                m_IsGround = true;
            }
            else
            {
                m_IsGround = false;
            }
        }
        else
        {
            m_IsGround = false;
        }
        m_InvincibleTime -= Time.deltaTime;
        m_stateMachine.Update();
	}

    public void ChangeState(FSM_State<Knight> _State)
    {
        if(_State == KngihtFSMHit.Instance)
        {
            if(this.m_InvincibleTime < 0)
            {
                m_stateMachine.ChangeState(_State);
            }
        }
        else
        {
            m_stateMachine.ChangeState(_State);
        }
    }

    public void LookLeft()
    {
        this.m_LookLeft = true;
        this.transform.localScale = new Vector3(1, 1, 1);
    }

    public void LookRight()
    {
        this.m_LookLeft = false;
        this.transform.localScale = new Vector3(-1,1,1);
    }

    public void HitDelay(float time)
    {
        StartCoroutine(Cor_HitDelay(time));
    }

    IEnumerator Cor_HitDelay(float time)
    {
        yield return new WaitForSeconds(time);
        m_HitOn = true;
        yield return null;
    }
}
