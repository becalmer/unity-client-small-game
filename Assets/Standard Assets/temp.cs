using UnityEngine;
using System.Collections;

public class temp_t
{
    static public int __RATE__ = 100;

    public temp_t()
    {
        m_key = 0;

        m_enter_sight_roles = null;
        m_leave_sight_roles = null;
    }

    public static void convert(ref proto.vector2_t l, ref Vector3 r)
    {
        r.x = ((float)l.x) / __RATE__;
        r.y = 0.0f;
        r.z = ((float)l.z) / __RATE__;
    }

    public static void convert(ref Vector3 l, ref proto.vector2_t r)
    {
        r.x = (int)(l.x * __RATE__);
        r.z = (int)(l.z * __RATE__);
    }

    public static float clamp(float x)
    {
        int y = (int)(x * __RATE__);
        x = ((float)y) / __RATE__;
        return x;
    }

    public void set_enter_sight_roles(ref System.Collections.Generic.List<proto.role_data_t> list)
    {
        if(m_enter_sight_roles == null)
        {
            m_enter_sight_roles = new System.Collections.Generic.List<proto.role_data_t>();
        }
        
        for(int x = 0; x < list.Count; ++ x)
        {
            int y = 0;
            for(; y < m_enter_sight_roles.Count; ++ y)
            {
                if(list[x].m_role_id == m_enter_sight_roles[y].m_role_id)
                {
                    break;
                }
            }

            if(y == m_enter_sight_roles.Count)
            {
                m_enter_sight_roles.Add(list[x]);
            }
        }
    }

    public void set_leave_sight_roles(ref System.Collections.Generic.List<uint> list)
    {
        if(m_leave_sight_roles == null)
        {
            m_leave_sight_roles = new System.Collections.Generic.List<uint>();
        }
        
        for (int x = 0; x < list.Count; ++x)
        {
            int y = 0;
            for(; y < m_leave_sight_roles.Count; ++ y)
            {
                if (list[x] == m_leave_sight_roles[y])
                {
                    break;
                }
            }
            if(y == m_leave_sight_roles.Count)
            {
                m_leave_sight_roles.Add(list[x]);
            }
        }
    }

    public void set_role_pos(ref System.Collections.Generic.List<proto.role_pos_t> list)
    {
        if(m_role_pos == null)
        {
            m_role_pos = new System.Collections.Generic.List<proto.role_pos_t>();
        }
        
        for (int x = 0; x < list.Count; ++x)
        {
            int y = 0;
            for(; y < m_role_pos.Count; ++ y)
            {
                if(list[x].m_role_id == m_role_pos[y].m_role_id)
                {
                    m_role_pos[y] = list[x];
                    break;
                }
            }
            if(y == m_role_pos.Count)
            {
                m_role_pos.Add(list[x]);
            }
        }
    }

    public void set_role_dir(ref System.Collections.Generic.List<proto.role_dir_t> list)
    {
        if (m_role_dir == null)
        {
            m_role_dir = new System.Collections.Generic.List<proto.role_dir_t>();
        }

        for (int x = 0; x < list.Count; ++x)
        {
            int y = 0;
            for (; y < m_role_dir.Count; ++y)
            {
                if (list[x].m_role_id == m_role_dir[y].m_role_id)
                {
                    break;
                }
            }
            if (y == m_role_dir.Count)
            {
                m_role_dir.Add(list[x]);
            }
        }
    }

    public uint m_key;

    public string m_user;
    public string m_password;

    public System.Collections.Generic.List<proto.role_data_t> m_enter_sight_roles;
    public System.Collections.Generic.List<uint> m_leave_sight_roles;
    public System.Collections.Generic.List<proto.role_pos_t> m_role_pos;
    public System.Collections.Generic.List<proto.role_dir_t> m_role_dir;
}
