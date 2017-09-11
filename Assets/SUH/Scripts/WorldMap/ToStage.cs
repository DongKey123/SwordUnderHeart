using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ToStage : MonoBehaviour , IPointerClickHandler {

    Vector3 ray;
    public int stageNum = 1;

    public GameObject m_panel;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero);

            if(hit.transform != null)
            {
                if (hit.transform.tag == "stage")
                {
                    m_panel.SetActive(true);
                }
            }
            
        }
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
    }

}
