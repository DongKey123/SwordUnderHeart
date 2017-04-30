using UnityEngine;
using System.Collections;

public class Knight : MonoBehaviour {

    public int m_JumpCount = 2;

    public bool m_LookLeft = true;
    public bool m_IsGround;

    public bool m_LBtnDown = false;
    public bool m_RBtnDown = false;

    public float m_moveSpeed = 3.0f;
    public float m_jumpPower = 3.0f;

    public bool m_ExistEnemyInRange = false;

    [HideInInspector]
    public Enemy m_curAttackEnemy = null;

    [HideInInspector]
    public SkeletonAnimation m_skeletonAni;
    [HideInInspector]
    public Animator m_Animator;


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

            }
        }
        else
        {
            m_IsGround = false;
        }
        

        m_stateMachine.Update();
	}

    public void ChangeState(FSM_State<Knight> _State)
    {
        m_stateMachine.ChangeState(_State);
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
}
