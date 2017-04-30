using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CoutToon : MonoBehaviour ,IPointerClickHandler {

    AutoFlip flip;
    Book book;
    public AudioSource flipsound;
    public float speed  = 5f;

	// Use this for initialization
	void Start () {
        flip = this.GetComponent<AutoFlip>();
        book = this.GetComponent<Book>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        if(book.currentPage == 5)
        {
            StartCoroutine("MoveBook");
        }
        else
        {
            if(flip.IsFlipping == false)
            {
                flipsound.Play();
                flip.FlipRightPage();
            }
        }
    }

    IEnumerator MoveBook()
    {
        while(true)
        {
            this.transform.position -= new Vector3(0, 1, 0) * Time.deltaTime * speed;
            if( this.transform.position.y < -350)
            {
                Destroy(this.gameObject);
                break;
            }
            yield return null;
        }
        yield return null;
    }
}
