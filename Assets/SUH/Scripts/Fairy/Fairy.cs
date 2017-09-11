using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FairyType
{
    Fairy1,
    Fairy2,
    Fairy3
}


public class Fairy : MonoBehaviour {

    public FairyType m_Type;

    private SkeletonAnimation _skeletonAni;

    public AudioSource m_ResqueAudio;
    public AudioSource m_ThanksAudio;

    private bool _resqueCk = false;
    

	// Use this for initialization
	void Start () {
        _skeletonAni = this.GetComponent<SkeletonAnimation>();

		switch(m_Type)
        {
            case FairyType.Fairy1:
                {
                    _skeletonAni.state.SetAnimation(0, "1_Idle", true);
                }
                break;
            case FairyType.Fairy2:
                {
                    _skeletonAni.state.SetAnimation(0, "2_Idle", true);
                }
                break;
            case FairyType.Fairy3:
                {
                    _skeletonAni.state.SetAnimation(0, "3_Idle", true);
                }
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
        
        #region Move Ani
        if (_resqueCk)
        {
            if (_skeletonAni.state.GetCurrent(0) != null)
            {
                if (_skeletonAni.state.GetCurrent(0).time > 0.99f)
                {
                    StartCoroutine(Die());
                    switch (m_Type)
                    {
                        case FairyType.Fairy1:
                            {
                                _skeletonAni.state.SetAnimation(0, "1_Move", true);
                            }
                            break;
                        case FairyType.Fairy2:
                            {
                                _skeletonAni.state.SetAnimation(0, "2_Move", true);
                            }
                            break;
                        case FairyType.Fairy3:
                            {
                                _skeletonAni.state.SetAnimation(0, "3_Move", true);
                            }
                            break;
                    }
                }
            }
            else
            {
                StartCoroutine(Die());
                switch (m_Type)
                {
                    case FairyType.Fairy1:
                        {
                            _skeletonAni.state.SetAnimation(0, "1_Move", true);
                        }
                        break;
                    case FairyType.Fairy2:
                        {
                            _skeletonAni.state.SetAnimation(0, "2_Move", true);
                        }
                        break;
                    case FairyType.Fairy3:
                        {
                            _skeletonAni.state.SetAnimation(0, "3_Move", true);
                        }
                        break;
                }
            }
        }
        #endregion


    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Knight")
        {
            Debug.Log("Knight");
            if(!_resqueCk )
            {
                if(this.m_Type == FairyType.Fairy1)
                {
                    col.GetComponent<Knight>().ChangeState(KnightFSMSliding.Instance);
                }
                else if (this.m_Type == FairyType.Fairy2)
                {
                    Camera.main.GetComponent<Stage1Camera>().m_cameraEvent = CameraEvent.DropBoar;
                    
                }
                StartCoroutine(Resque());
                _resqueCk = true;
            }
            
        }
    }

    IEnumerator Resque()
    {
        switch (m_Type)
        {
            case FairyType.Fairy1:
                {
                    
                    _skeletonAni.state.SetAnimation(0, "1_Free", false);
                }
                break;
            case FairyType.Fairy2:
                {
                    _skeletonAni.state.SetAnimation(0, "2_Free", false);
                }
                break;
            case FairyType.Fairy3:
                {
                    _skeletonAni.state.SetAnimation(0, "3_Free", false);
                }
                break;
        }

        m_ResqueAudio.Play();
        m_ThanksAudio.PlayDelayed(0.5f);
        yield return null;
    }

    IEnumerator Die()
    {
        Color color = new Color(1, 1, 1, 1);
        Destroy(this.gameObject, 1.5f);
        while (true)
        {
            color.a -= Time.deltaTime * 2 / 3;
            this._skeletonAni.skeleton.SetColor(color);

            if(color.a <=0)
            {
                break;
            }
            yield return null;
        }
        yield return null;
    }
}
