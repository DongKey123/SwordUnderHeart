using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBoar : MonoBehaviour {

    SkeletonAnimation m_skeletonAni;
    public AudioSource m_DeathSound;
    
	// Use this for initialization
	void Start () {
        m_skeletonAni = this.GetComponent<SkeletonAnimation>();
        m_skeletonAni.state.SetAnimation(0, "fall", true);
    }
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(this.transform.position.y < -4.9)
        {
            if(col.tag == "Ground")
            {
                m_DeathSound.Play();
                m_skeletonAni.state.SetAnimation(0, "Dead", false);
                this.GetComponent<BoxCollider2D>().isTrigger = false;
            }
        }
    }
}
