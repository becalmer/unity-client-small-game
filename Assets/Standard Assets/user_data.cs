using UnityEngine;
using System.Collections;

public class user_data_t
{

	public user_data_t()
    {
        m_nick = "";
        m_role_id = 0;

        m_nth = 0;
    }

    public Vector3 m_pos;
    public Vector3 m_dir;

    public string m_nick;
    public uint m_role_id;

    public uint m_nth;
}
