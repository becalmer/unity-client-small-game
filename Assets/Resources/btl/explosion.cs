using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour {

    float m_deadline = 0.0f;

	// Use this for initialization
	void Start () {

        m_deadline = Time.time + 2.0f;

        if(gameObject.GetComponent<Renderer>() != null)
        {
            gameObject.GetComponent<Renderer>().sortingOrder = 15;
        }

    }
	
	// Update is called once per frame
	void Update () {

        if(Time.time >= m_deadline)
        {
            Destroy(gameObject);
        }
	
	}
}
