using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightFSMSliding : FSM_State<Knight>
{

    static readonly KnightFSMSliding instance = new KnightFSMSliding();

    public static KnightFSMSliding Instance
    {
        get
        {
            return instance;
        }
    }

    static KnightFSMSliding() { }

    private KnightFSMSliding() { }

    public override void EnterState(Knight owner)
    {
        owner.m_state = KnightState.SLIDING;
        owner.m_skeletonAni.state.SetAnimation(0, "Move", true);
    }

    int eventCk = 0;

    public override void UpdateState(Knight owner)
    {
        if(eventCk == 0)
        {
            if(owner.transform.position.x < 37.3)
            {
                owner.transform.position += new Vector3(1, 0, 0) * Time.deltaTime * owner.m_moveSpeed;
                
            }
            else
            {
                eventCk = 1;
            }
        }
        else if (eventCk ==1)
        {
            owner.m_skeletonAni.state.SetAnimation(0, "Jump_up", false);
            Vector2 dir = new Vector2(0.25f, 1f);
            owner.GetComponent<Rigidbody2D>().AddForce(dir * owner.m_jumpPower, ForceMode2D.Impulse);
            eventCk = 2;
        }
        else if (eventCk == 2)
        {
            RaycastHit2D hit = Physics2D.Linecast(owner.transform.position, owner.transform.position + Vector3.down*1f);
            if (hit.transform != null)
            {
                if (hit.transform.tag == "Ground" && owner.GetComponent<Rigidbody2D>().velocity.y < 0f) 
                {
                    Debug.Log("Slide");
                    owner.m_skeletonAni.state.SetAnimation(0, "Slide", true);
                    owner.LookLeft();
                    eventCk = 3;
                }
            }
        }
        else if(eventCk == 3)
        {
            RaycastHit2D hit = Physics2D.Linecast(owner.transform.position, owner.transform.position + Vector3.down * 1f);
            if (hit.transform != null)
            {
                if (hit.transform.tag == "Ground" && owner.GetComponent<Rigidbody2D>().velocity.y == 0f && owner.transform.position.y < -8)
                {
                    Debug.Log("Slide");
                    owner.m_skeletonAni.state.SetAnimation(0, "Idle", true);
                    owner.LookLeft();
                    eventCk = 4;
                }
            }
        }
        else if(eventCk == 4)
        {
            owner.m_cam.GetComponent<Stage1Camera>().m_cameraEvent = CameraEvent.AfterSlidingPlayer;
            eventCk = 5;
        }

    }

    public override void ExitState(Knight owner)
    {

    }
}
