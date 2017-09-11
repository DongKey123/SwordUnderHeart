using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraEvent
{
    Event,
    TopSide,
    SlidingPlayer,
    AfterSlidingPlayer,
    BottomSide,
    DropBoar,
    AfterDropBoar,
    DownObstacle
}

public class Stage1Camera : MonoBehaviour {

    public Transform m_Player;
    public Fairy[] m_fairys;
    public GameObject m_FallBoar;
    public GameObject m_Dust;

    public GameObject[] m_Ground;
    public GameObject[] m_ChangeGround;

    public GameObject m_Obstacle;

    public float m_MoveSpeed = 10.0f;

    public CameraEvent m_cameraEvent = CameraEvent.TopSide;

    public float m_Shake = 0f;
    public float m_ShakeAmount = 0.7f;
    public float m_decreaseFactor = 1.0f;
    public float m_ShakeTime = 1f;
    

    [SerializeField]
    private float _TopMinX = 0f;
    [SerializeField]
    private float _TopMaxX = 0f;
    [SerializeField]
    private float _TopMinY = 0f;
    [SerializeField]
    private float _TopMaxY = 0f;

    private bool _Bottomside = true;
    [SerializeField]
    private float _BottomMinX = 0f;
    [SerializeField]
    private float _BottomMaxX = 0f;
    [SerializeField]
    private float _BottomMinY = 0f;
    [SerializeField]
    private float _BottomMaxY = 0f;

    Vector3 _originalPos;

    // Use this for initialization
    void Start () {
        //StartCoroutine(CameraShake());
        _originalPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {	
        switch(m_cameraEvent)
        {
            case CameraEvent.TopSide:
                {
                    float x = Mathf.Clamp(m_Player.position.x, _TopMinX, _TopMaxX);
                    float y = Mathf.Clamp(m_Player.position.y, _TopMinY, _TopMaxY);
                    this.transform.position = new Vector3(x, y, this.transform.position.z);
                    
                    if(m_Player.transform.position.x > 37.3)
                    {
                        this.m_cameraEvent = CameraEvent.SlidingPlayer;
                    }
                }
                break;
            case CameraEvent.SlidingPlayer:
                {
                    float x = Mathf.Clamp(m_Player.position.x, _BottomMinX, _BottomMaxX);
                    float y = Mathf.Clamp(m_Player.position.y, _BottomMinY, _BottomMaxY);
                    this.transform.position = new Vector3(x, y, this.transform.position.z);
                }
                break;
            case CameraEvent.AfterSlidingPlayer:
                {
                    this.m_cameraEvent = CameraEvent.Event;
                    StartCoroutine(AfterSliding());
                }
                break;
            case CameraEvent.BottomSide:
                {
                    float x = Mathf.Clamp(m_Player.position.x, _BottomMinX, _BottomMaxX);
                    float y = Mathf.Clamp(m_Player.position.y, _BottomMinY, _BottomMaxY);
                    this.transform.position = new Vector3(x, y, this.transform.position.z);
                }
                break;
            case CameraEvent.DropBoar:
                {
                    this.m_cameraEvent = CameraEvent.Event;
                    StartCoroutine(DropBoar());
                }
                break;
            case CameraEvent.AfterDropBoar:
                {
                    float x = Mathf.Clamp(m_Player.position.x, _BottomMinX, _BottomMaxX);
                    float y = Mathf.Clamp(m_Player.position.y, _BottomMinY, _BottomMaxY);
                    this.transform.position = new Vector3(x, y, this.transform.position.z);
                    if(m_Player.position.x > 38)
                    {
                        this.m_cameraEvent = CameraEvent.Event;
                        StartCoroutine(DownObstacle());
                    }
                }
                break;
            case CameraEvent.DownObstacle:
                {
                    float x = Mathf.Clamp(m_Player.position.x, _BottomMinX, _BottomMaxX);
                    float y = Mathf.Clamp(m_Player.position.y, _BottomMinY, _BottomMaxY);
                    this.transform.position = new Vector3(x, y, this.transform.position.z);
                }
                break;
        }

	}

    public void ChangeCameraEvent()
    {

    }

    IEnumerator AfterSliding()
    {
        int Ck = 0;
        while(true)
        {
            if(Ck ==0)
            {
                if (this.transform.position.x > 12.8)
                {
                    this.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * m_MoveSpeed;
                    if(this.transform.position.x <= 12.8)
                    {
                        this.transform.position = new Vector3(12.8f, this.transform.position.y, this.transform.position.z);
                        Ck = 1;
                    }
                }
            }
            else if (Ck ==1)
            {
                yield return new WaitForSeconds(1.5f);
                Ck = 2;
            }
            else if (Ck ==2)
            {
                if (this.transform.position.x < m_Player.transform.position.x)
                {
                    this.transform.position += new Vector3(1, 0, 0) * Time.deltaTime * m_MoveSpeed;
                    if (this.transform.position.x >= m_Player.transform.position.x)
                    {
                        float x = Mathf.Clamp(m_Player.position.x, _BottomMinX, _BottomMaxX);
                        float y = Mathf.Clamp(m_Player.position.y, _BottomMinY, _BottomMaxY);
                        this.transform.position = new Vector3(x,y,this.transform.position.z);
                        m_Player.GetComponent<Knight>().ChangeState(KnightFSMIdle.Instance);
                        this.m_cameraEvent = CameraEvent.BottomSide;
                        break;
                    }
                }
            }
            yield return null;
        }
        yield return null;
    }

    IEnumerator DropBoar()
    {
        int Ck = 0;

        m_Player.GetComponent<Knight>().m_Interactive = false;

        while (true)
        {
            if (Ck == 0)
            {
                this.transform.position += new Vector3(1, 0, 0) * Time.deltaTime * m_MoveSpeed;
                if (this.transform.position.x >= 30)
                {
                    this.transform.position = new Vector3(30, this.transform.position.y, this.transform.position.z);
                    m_Dust.GetComponent<Animator>().Play("Dust");
                    Ck = 1;
                }
            }
            else if (Ck == 1)
            {
                StartCoroutine(CameraShake());
                yield return new WaitForSeconds(1.0f);
                for (int i=0;i<2;i++)
                {
                    this.m_Ground[i].SetActive(false);
                    this.m_ChangeGround[i].SetActive(true);
                }
                m_FallBoar.SetActive(true);
                yield return new WaitForSeconds(2.0f);
                Ck = 2;
            }
            else if (Ck==2)
            {
                this.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * m_MoveSpeed;
                if (this.transform.position.x <= m_Player.position.x)
                {
                    float x = Mathf.Clamp(m_Player.position.x, _BottomMinX, _BottomMaxX);
                    float y = Mathf.Clamp(m_Player.position.y, _BottomMinY, _BottomMaxY);
                    this.transform.position = new Vector3(x, y, this.transform.position.z);
                    m_Player.GetComponent<Knight>().ChangeState(KnightFSMIdle.Instance);
                    this.m_cameraEvent = CameraEvent.AfterDropBoar;
                    Ck = 3;
                    Debug.Log("ckkkk");
                    m_Player.GetComponent<Knight>().m_Interactive = true;
                    break;
                }
             }
            yield return null;
        }
        yield return null;
    }

    IEnumerator CameraShake()
    {
        float time = 0;
        Debug.Log("Shake");
        _originalPos = this.transform.position;
        float Minx = _originalPos.x - 0.5f;
        float Maxx = _originalPos.x + 0.5f;
        float Miny = _originalPos.y - 0.5f;
        float Maxy = _originalPos.y + 0.5f;
        while (time < m_ShakeTime)
        {
            time += Time.deltaTime;
            if (m_Shake > 0)
            {
                this.transform.localPosition += Random.insideUnitSphere * m_ShakeAmount;
                float x = Mathf.Clamp(this.transform.localPosition.x, Minx, Maxx);
                float y = Mathf.Clamp(this.transform.localPosition.y, Miny, Maxy);
                this.transform.localPosition = new Vector3(x, y, -10);
                m_Shake -= Time.deltaTime * m_decreaseFactor;
            }
            else
            {
                m_Shake = 0f;
                this.transform.position = _originalPos;
            }
            yield return null;
        }
        this.transform.position = _originalPos;
        Debug.Log("Shake End");
        yield return null;
    }

    IEnumerator DownObstacle()
    {
        int Ck = 0;

        m_Player.GetComponent<Knight>().m_Interactive = false;

        while (true)
        {
            if (Ck == 0)
            {
                this.transform.position += new Vector3(1, 0, 0) * Time.deltaTime * m_MoveSpeed;
                if(this.transform.position.x > 40)
                {
                    Ck = 1;
                }
            }
            else if (Ck == 1)
            {
                float random = Random.Range(-1, 1);
                this.m_Obstacle.transform.position += new Vector3(random*0.1f, -1, 0) * Time.deltaTime;
                if( this.m_Obstacle.transform.position.y < -11)
                {
                    Ck = 2;
                }
            }
            else if (Ck == 2)
            {
                this.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * m_MoveSpeed;
                if (this.transform.position.x <= this.m_Player.position.x)
                {
                    
                    m_cameraEvent = CameraEvent.DownObstacle;
                    m_Player.GetComponent<Knight>().m_Interactive = true;
                    break;
                }
            }
            yield return null;
        }

        yield return null;
    }
}
