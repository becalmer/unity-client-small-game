using UnityEngine;
using System.Collections;

public class smoke : MonoBehaviour {

    float m_deadline = 0.0f;

    tank m_tank;

	// Use this for initialization
	void Start () {

        m_deadline = Time.time + 2.0f;

        if (gameObject.GetComponent<Renderer>() != null)
        {
            gameObject.GetComponent<Renderer>().sortingOrder = 20;
        }

        m_tank = null;

    }
	
	// Update is called once per frame
	void Update () {

        if(Time.time >= m_deadline)
        {
            if(m_tank != null)
            {
                m_tank.smoke_die();
            }

            Destroy(gameObject);
        }
	
	}

    public void attach_tank(ref tank tk)
    {
        m_tank = tk;
    }
}
