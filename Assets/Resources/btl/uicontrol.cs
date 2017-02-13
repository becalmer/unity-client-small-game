using UnityEngine;
using System.Collections;

public class uicontrol : MonoBehaviour {

    static uicontrol m_ins;

    public UISprite m_btl_time;
    public UISprite m_rank;
    public UISprite m_skill_1;
    public UISprite m_skill_2;
    public UISprite m_skill_3;

    public Camera m_camera;

    public static uicontrol ins()
    {
        return m_ins;
    }

	// Use this for initialization
	void Start () {
        m_ins = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    bool Hit(Vector3 v, Vector3 u, Vector2 s)
    {
        Vector3 x = m_camera.WorldToScreenPoint(u);

        if(v.x < (x.x - s.x / 2))
        {
            return false;
        }

        if (v.x > (x.x + s.x / 2))
        {
            return false;
        }

        if (v.y < (x.y - s.y / 2))
        {
            return false;
        }

        if (v.y > (x.y + s.y / 2))
        {
            return false;
        }

        return true;
    }

    public bool HitTest(Vector3 v)
    {
        if (Hit(v, m_rank.gameObject.transform.position, m_rank.localSize))
        {
            return true;
        }

        if (Hit(v, m_btl_time.gameObject.transform.position, m_btl_time.localSize))
        {
            return true;
        }

        if (Hit(v, m_skill_1.gameObject.transform.position, m_skill_1.gameObject.GetComponent<BoxCollider>().size))
        {
            return true;
        }

        if (Hit(v, m_skill_2.gameObject.transform.position, m_skill_2.gameObject.GetComponent<BoxCollider>().size))
        {
            return true;
        }

        if (Hit(v, m_skill_3.gameObject.transform.position, m_skill_3.gameObject.GetComponent<BoxCollider>().size))
        {
            return true;
        }

        return false;
    }
}
