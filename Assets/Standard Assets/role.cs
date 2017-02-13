using UnityEngine;
using System.Collections;

public class role_t
{
    public role_t()
    {
        m_gameobject = null;
        m_role_id = 0;
    }

    public GameObject m_gameobject;
    public uint m_role_id;
    public Vector3 m_pos;
}
