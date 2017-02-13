using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour
{
    public Vector3 m_dir;
    public Vector3 m_pos;
    public Vector3 m_target;

    float m_last_frame_ms;
    float m_accu_ms;
    float m_accu_rate;

    bool m_die;
    float m_die_time;

    // Use this for initialization
    void Start ()
    {
        m_last_frame_ms = Time.time;
        m_accu_ms = 0;
        m_accu_rate = 0;

        gameObject.transform.position = m_pos;

        gameObject.GetComponent<Renderer>().sortingOrder = 15;

        m_die = false;
        m_die_time = 0.0f;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float ms = Time.time;

        m_accu_ms += ms - m_last_frame_ms;

        float accu_rate = m_accu_ms * map.BULLET_SPEED;

        float diff_rate = accu_rate - m_accu_rate;

        m_pos = gameObject.transform.position + m_dir * diff_rate;

        clamp(ref m_target, ref m_pos);

        gameObject.transform.position = m_pos;

        m_accu_rate = accu_rate;
        m_last_frame_ms = ms;

        if (m_accu_ms >= 1.0f)
        {
            m_accu_ms = 0.0f;
            m_accu_rate = 0.0f;
        }
    }

    bool clamp(ref Vector3 lf, ref Vector3 rh)
    {
        bool x = false;
        bool z = false;

        if(m_dir.x > 0)
        {
            if (rh.x > lf.x)
            {
                rh.x = lf.x;
                x = true;
            }
        }
        else if(m_dir.x < 0)
        {
            if(rh.x < lf.x)
            {
                rh.x = lf.x;
                x = true;
            }
        }
        else
        {
            x = true;
        }

        if (m_dir.z > 0)
        {
            if (rh.z > lf.z)
            {
                rh.z = lf.z;
                z = true;
            }
        }
        else if (m_dir.z < 0)
        {
            if (rh.z < lf.z)
            {
                rh.z = lf.z;
                z = true;
            }
        }
        else
        {
            z = true;
        }

        return (x && z);
    }

    public void set_data(ref Vector3 target, ref Vector3 pos, ref Vector3 dir)
    {
        m_target = target;
        m_pos = pos;
        m_dir = dir;
    }

    public void die()
    {
        m_die = true;
        m_die_time = Time.time + 2;
    }

    public bool done()
    {
        if(!m_die)
        {
            return false;
        }

        if(Time.time < m_die_time)
        {
            return false;
        }

        return true;
    }
}
