using UnityEngine;
using System.Collections;

public class ScrollWorldMap : MonoBehaviour {

    private float minX = 0;
    private float maxX = 10.8f;
    public float speed = 0.1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Debug.Log("touch");
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(-touchDeltaPosition.x * speed, 0, 0);
            if(transform.position.x < minX)
            {
                transform.position = new Vector3(minX, 0,-10);
            }

            if (transform.position.x > maxX)
            {
                transform.position = new Vector3(maxX, 0, -10);
            }
        }
    }
}
